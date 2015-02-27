using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using WTAN.BLL;
using Newtonsoft.Json;
using WTAN.CommonUtility;
using WTAN.Model.DModel;

namespace Demo.Controllers
{

    /// <summary>
    /// 文本編輯器配置
    /// </summary>
    internal class EditConfig
    {
        private static JObject _Items;
        public static JObject Items
        {
            get
            {
                if (_Items == null)
                {
                    MenusBLL bll = new MenusBLL();
                    _Items = JObject.Parse(bll.EditorConfigString());
                }
                return _Items;
            }
        }

        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }
        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }

    [UserSystemAuthorize]
    public class UploadController : Controller
    {
        public String UploadServer(String guid, UploadPage dir)
        {
            if (String.IsNullOrEmpty(guid))
            {
                return WriteJson(new
                {
                    state = "编辑器参数错误"
                });
            }
            String result = String.Empty;
            switch (Request["action"])
            {
                case "config":
                    MenusBLL bll = new MenusBLL();
                    result = WriteJson(JObject.Parse(bll.EditorConfigString()));
                    break;
                case "listimage":
                    result = ListImage(guid, dir, UploadType.Img);
                    break;
                case "listfile":
                    result = ListImage(guid, dir, UploadType.File);
                    break;
                case "remoteimage":
                    result = RemoteIMG(guid, dir);
                    break;
                case "uploadimage":
                    result = UploadArticleImg(guid, dir);
                    break;
                case "delimg":
                    result = DelImg();
                    break;
                case "setdef":
                    result = SetDef();
                    break;
                case "uploadfile":
                    result = UploadFile(guid, dir);
                    break;
            }

            return result;
        }

        private String WriteJson(object response)
        {

            string jsonpCallback = Request["callback"],
                json = JsonConvert.SerializeObject(response);
            if (String.IsNullOrWhiteSpace(jsonpCallback))
            {
                Response.AddHeader("Content-Type", "text/plain");
                return json;
            }
            else
            {
                Response.AddHeader("Content-Type", "application/javascript");
                return String.Format("{0}({1});", jsonpCallback, json);
            }
        }

        public String GetUploadType(UploadPage page, UploadType type)
        {
            return page.ToString() + "_" + type.ToString();
        }

        public String GetUploadDir(String guid, String uploadType)
        {
            return String.Format("{0}{1}/{2}/", AppSettings.EditUploadDir, uploadType, guid);
        }

        private String UploadFile(String id,UploadPage type)
        {
            String uploadType = GetUploadType(type, UploadType.File);
            var file = Request.Files[EditConfig.GetString("imageFieldName")];
            String state = String.Empty;
            if (file != null)
            {
                String dir = GetUploadDir(id, uploadType);
                String[] filetype = AppSettings.UplaodFileType;//允许上传的文件类型
                String fileName = String.Format("{0}{1}", Guid.NewGuid(), file.FileName.GetFileExtension());
                UploadInfo info = file.SaveAs(dir, fileName, filetype, AppSettings.UploadFileSize);
                if (info.State.Equals("SUCCESS"))
                {
                    Sys_FilesTB tb = new Sys_FilesTB()
                    {
                        Enable = true,
                        FileName = fileName,
                        FilePath = info.FilePath,
                        FileSize = info.Size,
                        FileType = info.CurrentType,
                        FileURL = info.URL,
                        RelatedGUID = id,
                        Sort = 1,
                        UploadType = uploadType,
                    };
                    SysFileBLL bll = new SysFileBLL();
                    int autokey = bll.SaveFileInfo(tb);
                    if (autokey > 0)
                    {
                        return WriteJson(new
                        {
                            url = AppSettings.ManageDomain + info.URL,
                            title = info.OriginalName,
                            original = info.OriginalName,
                            state = info.State,
                            error = info.State
                        });
                    }
                    else
                    {
                        state = "文件上传成功但文件信息保存至资料库失败";
                    }
                }
                else
                    state = info.State;
            }

            return WriteJson(new
            {
                url = "",
                title = "",
                original = "",
                state = state,
                error = state
            });
        }

        private String DelImg()
        {
            int autokey = Request["delimgguid"].ToInt32Value();
            if (autokey < 1)
            {
                return WriteJson(new
                {
                    state = "参数错误,删除失败."
                });
            }
            else
            {
                SysFileBLL bll = new SysFileBLL();
                if (bll.DelFileInfo(autokey))
                {
                    return WriteJson(new
                    {
                        state = "SUCCESS"
                    });
                }
                else
                {
                    return WriteJson(new
                    {
                        state = "很抱歉,图片删除失败."
                    });
                }
            }
        }

        private String SetDef()
        {
            int autokey = Request["imgguid"].ToInt32Value();
            String setstate = Request["setstate"];
            if (autokey > 0)
            {
                SysFileBLL bll = new SysFileBLL();
                if (bll.UploadFileSortInfo(setstate, autokey))
                {
                    return WriteJson(new
                    {
                        state = "SUCCESS",
                        cls = setstate.Equals("0") ? "defbut def" : "defbut"
                    });
                }
                else
                {
                    return WriteJson(new
                    {
                        state = "设置失败."
                    });
                }
            }
            return WriteJson(new
            {
                state = "参数错误,设置失败."
            });
        }

        private String RemoteIMG(String id, UploadPage type)
        {
            String uploadType = GetUploadType(type, UploadType.Img);
            String[] Sources = Request.Form.GetValues("source[]");
            if (Sources == null || Sources.Length == 0)
            {
                return WriteJson(new
                {
                    state = "参数错误：没有指定抓取源"
                });

            }
            List<Crawler> crawlers = new List<Crawler>();
            string d = GetUploadDir(id, uploadType); ;
            int index = 1;
            foreach (String s in Sources)
            {
                String fileName = String.Format("Re_{0}{1}", Guid.NewGuid().ToString(), s.GetFileExtension());
                Crawler c = new Crawler(s, Server).Fetch(d, fileName);
                Sys_FilesTB file = new Sys_FilesTB()
                {
                    FileName = fileName,
                    FilePath = d.Replace("/", "\\") + fileName,
                    FileURL = d.Replace("\\", "/") + fileName,
                    FileType = fileName.GetFileExtension(),
                    UploadType = uploadType,
                    RelatedGUID = id,
                    Sort = 2,
                    Enable = true,
                };
                SysFileBLL bll = new SysFileBLL();
                int autokey = bll.SaveFileInfo(file);
                crawlers.Add(c);
                index++;
            }

            return WriteJson(new
            {
                state = "SUCCESS",
                list = crawlers.Select(x => new
                {
                    state = x.State,
                    source = AppSettings.ManageDomain + x.SourceUrl,
                    url = AppSettings.ManageDomain + x.ServerUrl
                })
            });
        }

        private String ListImage(String id, UploadPage page, UploadType type)
        {
            String uploadType = GetUploadType(page, type);
            int count = 0;
            String state = "SUCCESS";
            try
            {
                int start = String.IsNullOrEmpty(Request["start"]) ? 0 : Convert.ToInt32(Request["start"]);
                int size = String.IsNullOrEmpty(Request["size"]) ? 20 : Convert.ToInt32(Request["size"]);
                SysFileBLL bll = new SysFileBLL();
                List<Sys_FilesTB> list = bll.GetFilesList(start, size, id, uploadType, out count);
                var flist = from f in list
                            select new
                            {
                                //返回添加缩略图
                                smallurl = type == UploadType.Img ? f.FileURL.GetSmallUrl() : f.FileURL,
                                guid = f.AutoKey,
                                url = AppSettings.ManageDomain + f.FileURL,
                                sort = f.Sort
                            };

                return WriteJson(new
                {
                    state = state,
                    list = flist,
                    start = start,
                    size = size,
                    total = count
                });
            }
            catch (Exception ex)
            {
                state = ex.Message;
            }

            return String.Empty;
        }

        private String UploadArticleImg(String id, UploadPage type)
        {
            String uploadType = GetUploadType(type, UploadType.Img);
            String state = String.Empty;
            if (Request.Files.Count == 1)
            {
                var file = Request.Files[0];
                String dir = GetUploadDir(id, uploadType);//給每個用戶都建立一個文件夾進行保存
                string[] filetype = AppSettings.UplaodImgType;
                String fileName = String.Format("{0}{1}", Guid.NewGuid(), file.FileName.GetFileExtension());
                //图片上传
                UploadInfo info = file.SaveAs(dir, fileName, filetype, AppSettings.UplaodImgSize, true, 320, 320, true, (float)0.5, AppSettings.WaterMarkText);
                if (info.State.Equals("SUCCESS"))
                {
                    Sys_FilesTB tb = new Sys_FilesTB()
                    {
                        Enable = true,
                        FileName = fileName,
                        FilePath = info.FilePath,
                        FileSize = info.Size,
                        FileType = info.CurrentType,
                        FileURL = info.URL,
                        RelatedGUID = id,
                        Sort = 1,
                        UploadType = uploadType,
                    };
                    SysFileBLL bll = new SysFileBLL();
                    int autokey = bll.SaveFileInfo(tb);
                    if (autokey > 0)
                    {
                        return WriteJson(new
                        {
                            url = AppSettings.ManageDomain + info.URL,
                            title = info.OriginalName,
                            original = info.OriginalName,
                            state = info.State,
                            error = info.State
                        });
                    }
                    else
                    {
                        state = "文件上传成功但文件信息保存至资料库失败";
                    }
                }
                else
                    state = info.State;
            }
            else
                state = "未接收到上传文件";
            return WriteJson(new
            {
                url = "",
                title = "",
                original = "",
                state = state,
                error = state
            });
        }
    }
}
