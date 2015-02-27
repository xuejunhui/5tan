using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web;
using System.IO;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using WTAN.CommonUtility;

namespace WTAN.CommonUtility
{

    public class Crawler
    {
        public string SourceUrl { get; set; }
        public string ServerUrl { get; set; }
        public string State { get; set; }

        private HttpServerUtilityBase Server { get; set; }


        public Crawler(string sourceUrl, HttpServerUtilityBase server)
        {
            this.SourceUrl = sourceUrl;
            this.Server = server;
        }

        public Crawler Fetch(String dir, String fileName, Double swidth = 200, Double sheight = 200)
        {
            var request = HttpWebRequest.Create(this.SourceUrl) as HttpWebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    State = "Url returns " + response.StatusCode + ", " + response.StatusDescription;
                    return this;
                }
                if (response.ContentType.IndexOf("image") == -1)
                {
                    State = "Url is not an image";
                    return this;
                }
                ServerUrl = "/" + dir.RemoveStartWith("/") + fileName;
                var savePath = Server.MapPath("~" + ServerUrl);
                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }
                try
                {
                    var stream = response.GetResponseStream();
                    var reader = new BinaryReader(stream);
                    byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        byte[] buffer = new byte[4096];
                        int count;
                        while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            ms.Write(buffer, 0, count);
                        }
                        bytes = ms.ToArray();
                    }
                    File.WriteAllBytes(savePath, bytes);
                    UploadHelper.AddSmall(null, Path.GetDirectoryName(savePath), fileName, swidth, sheight, savePath, true);
                    State = "SUCCESS";
                }
                catch (Exception e)
                {
                    State = "抓取错误：" + e.Message;
                }
                return this;
            }
        }
    }

    public class UploadInfo
    {
        public String State { get; set; }
        public String URL { get; set; }
        public String CurrentType { get; set; }
        public String FileName { get; set; }
        public String OriginalName { get; set; }
        /// <summary>
        /// 文件大小 单位KB
        /// </summary>
        public int Size { get; set; }
        public String FilePath { get; set; }
    }

    public static class UploadHelper
    {

        public static void DelFolder(this String dir)
        {
            dir = HttpContext.Current.Server.MapPath("~/" + dir.RemoveStartWith("/").RemoveEndsWith("/"));
            dir.DeleteFolder();
        }

        private static void DeleteFolder(this string dir)
        {
            if (!Directory.Exists(dir))
                return;
            // 循环文件夹里面的内容
            foreach (string f in Directory.GetFileSystemEntries(dir))
            {
                // 如果是文件存在
                if (File.Exists(f))
                    File.Delete(f);
                else
                    DeleteFolder(f);
            }
            // 删除已空文件夹 
            Directory.Delete(dir);
        }

        /// <summary>
        /// 刪除Upload下所有空文件夾
        /// </summary>
        public static void DeleteEmptyFolder()
        {
            String path = HttpContext.Current.Server.MapPath("~/Upload/");
            List<String> allDir = new List<String>();
            GetAllFoolder(path,allDir);
            foreach(string s in allDir)
            {
                if (Directory.GetFileSystemEntries(s).Count() == 0)
                {
                    Directory.Delete(s);
                }

            }
        }

        public static void GetAllFoolder(String path,List<String> list)
        {
            if (!Directory.Exists(path))
                return;
            foreach (string f in Directory.GetFileSystemEntries(path))
            {
                if (!File.Exists(f))
                {
                    GetAllFoolder(f, list);
                }
            }
            list.Add(path);

        }

        public static void DelFile(this String filepath)
        {
            try
            {
                String path = HttpContext.Current.Server.MapPath("~/" + filepath.RemoveStartWith("/"));
                if (File.Exists(path))
                    File.Delete(path);
                String smallpath = HttpContext.Current.Server.MapPath("~/" + filepath.GetSmallUrl().RemoveStartWith("/"));
                if (File.Exists(smallpath))
                    File.Delete(smallpath);
            }
            catch (Exception ex)
            {
                ex.AddLog("UploadHelper", String.Format("DelFile:刪除文件{0}", filepath));
            }
        }

        public static String GetSmallUrl(this String url)
        {
            if (String.IsNullOrEmpty(url))
                return String.Empty;
            url = url.Insert(url.LastIndexOf("/"), "/small");
            return url.StartsWith(AppSettings.ManageDomain) ? url : AppSettings.ManageDomain + url;
        }

        public static String GetFileExtension(this String filename)
        {
            return filename.Substring(filename.LastIndexOf('.'));
        }

        public static void AddSmall(this HttpPostedFileBase file, String imgPath, String fileName, Double Width, Double Height, String filePath = "", Boolean isSmall = false)
        {
            String dir = isSmall ? (imgPath + "\\small\\") : imgPath + "\\";
            String strFile = dir + fileName;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            //从文件取得图片对象
            System.Drawing.Image image = null;
            if (file != null)
                image = System.Drawing.Image.FromStream(file.InputStream, true);
            if (filePath != "")
                image = System.Drawing.Image.FromFile(filePath, true);
            System.Double NewWidth, NewHeight;
            if (image.Width > Width || image.Height > Height) //图片有超出
            {
                if (image.Width > image.Height)
                {
                    NewWidth = Width;
                    NewHeight = image.Height * (NewWidth / image.Width);
                }
                else
                {
                    NewHeight = Height;
                    NewWidth = (NewHeight / image.Height) * image.Width;
                }

                if (NewWidth > Width)
                {
                    NewWidth = Width;
                }
                if (NewHeight > Height)
                {
                    NewHeight = Height;
                }
            }
            else
            {
                NewWidth = image.Width;
                NewHeight = image.Height;
            }
            System.Drawing.Size size = new Size((int)NewWidth, (int)NewHeight); // 图片大小
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(size.Width, size.Height); //新建bmp图片
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap); //新建画板
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //设置高质量插值法
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //设置高质量,低速度呈现平滑程度
            graphics.Clear(Color.White); //清空画布
            //在指定位置画图
            graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
            System.Drawing.GraphicsUnit.Pixel);
            bitmap.Save(strFile, System.Drawing.Imaging.ImageFormat.Jpeg);
            bitmap.Dispose();
        }

        /// <summary>
        /// 上傳文件到服務器
        /// </summary>
        /// <param name="file">上傳的文件</param>
        /// <param name="dir">保存文件的位置</param>
        /// <param name="dir">保存文件的文件名</param>
        /// <param name="fileTypes">能上傳的文件類型</param>
        /// <param name="maxSize">最大上傳文件大小,單位KB</param>
        /// <returns></returns>
        public static UploadInfo SaveAs(this HttpPostedFileBase file, String dir, String fileName, String[] fileTypes, int maxSize = 1024, Boolean isAddSmallImg = false, int smallImgWidth = 100, int smallImgHeight = 100, Boolean isAddWaterText = false, float diaphaneity = (float)0.3, String waterTxt = "", Boolean isAddWaterImg = false, String waterImgUrl = "")
        {
            UploadInfo info = new UploadInfo();
            info.State = "SUCCESS";
            info.FileName = fileName;
            info.OriginalName = file.FileName;
            info.CurrentType = info.OriginalName.GetFileExtension().ToUpper();
            info.Size = file.ContentLength / 1024;//单位KB
            if (!fileTypes.Contains(info.CurrentType.RemoveStartWith(".")))
            {
                info.State = "您上传的文件不合法,必须为：（" + fileTypes.Merge("|") + ")";
            }
            if (file.ContentLength > maxSize * 1024)
            {
                //文件大小超出网站限制
                info.State = "您上传的文件大小超出网站限制";
            }
            if (info.State == "SUCCESS")
            {
                try
                {
                    String savepath = HttpContext.Current.Server.MapPath("~/" + dir.RemoveStartWith("/"));
                    if (!Directory.Exists(savepath))
                        Directory.CreateDirectory(savepath);
                    info.URL = dir.Replace("\\", "/") + info.FileName;
                    info.FilePath = dir.Replace("/", "\\") + info.FileName;

                    //不保存原图 进行缩略保存
                    file.AddSmall(savepath, info.FileName, 800, 800, "", false);
                    //file.SaveAs(savepath + info.FileName);


                    //添加圖片水印 如果有設置圖片水印則圖片水印優先
                    String result = String.Empty;
                    if (isAddWaterImg)
                    {
                        WaterMarkHelper.ImageWatermarkParameters imgwater = new WaterMarkHelper.ImageWatermarkParameters(HttpContext.Current.Server.MapPath("~/" + info.FilePath.RemoveStartWith("/")), HttpContext.Current.Server.MapPath("~/" + waterImgUrl.RemoveStartWith("/")), diaphaneity, WaterMarkHelper.ImagePosition.RigthBottom);
                        WaterMarkHelper.DrawImage(imgwater, out result);
                        info.State = result == "" ? "SUCCESS" : result;
                    }
                    else if (isAddWaterText)
                    {

                        Font font = new Font("arial", 20, FontStyle.Strikeout);
                        WaterMarkHelper.TextWatermarkParameters txtwater = new WaterMarkHelper.TextWatermarkParameters(HttpContext.Current.Server.MapPath("~/" + info.FilePath.RemoveStartWith("/")), waterTxt, diaphaneity, WaterMarkHelper.ImagePosition.RigthBottom, font);
                        WaterMarkHelper.DrawText(txtwater, out result);//給圖片加水印文字
                        info.State = result == "" ? "SUCCESS" : result;

                    }
                    if (isAddSmallImg)//添加缩略图
                    {
                        file.AddSmall(savepath, info.FileName, smallImgWidth, smallImgHeight, "", true);
                    }

                }
                catch (Exception ex)
                {
                    info.State = "文件上传时发送错误.";
                    info.URL = String.Empty;
                    ex.AddLog("UploadHelper", "SaveAs");
                }
            }
            return info;
        }


    }
}
