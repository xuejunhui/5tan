using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.Model.DModel;
using WTAN.CommonUtility;
using WTAN.IDAL;

namespace WTAN.SQLServerDAL
{
    public class FriendLinkServer : IFriendLink
    {
        public Boolean DelFriendLink(String id)
        {
            if (id.Equals("del"))
            {
                String sql = "delete FriendLink where Enable=0 and webname<>'tancms'";
                return sql.ExecuteNoneQuery() > 0;
            }
            else
            {
                String sql = @"delete FriendLink 
                        where autokey in ({0})";//防注入写法
                String ids = String.Empty;
                List<String> list = new List<String>();
                for (int i = 0; i < id.Split(',').Count(); i++)
                {
                    ids += (ids.IsNullOrEmpty() ? "" : ",") + String.Format("@id{0}", i);
                    list.Add(String.Format("id{0}", i));
                    list.Add(id.Split(',')[i]);
                }
                sql = String.Format(sql, ids);
                return sql.ExecuteNoneQuery(list.ToArray()) > 0;
            }
        }

        public Boolean UpdateFriendLinkState(String id, int state)
        {
            String sql = "update FriendLink set Enable=@Enable where autokey in ({0})";
            String ids = String.Empty;
            List<String> list = new List<String>() { 
                "Enable",state.ToString()
            };
            for (int i = 0; i < id.Split(',').Count(); i++)
            {
                ids += (ids.IsNullOrEmpty() ? "" : ",") + String.Format("@id{0}", i);
                list.Add(String.Format("id{0}", i));
                list.Add(id.Split(',')[i]);
            }
            sql = String.Format(sql, ids);
            return sql.ExecuteNoneQuery(list.ToArray()) > 0;
        }

        public int SaveFriendLink(FriendLinkTB f)
        {
            String sql = String.Empty;
            List<String> list = new List<String>() { 
                "LinkName",f.LinkName,
                "LinkUrl",f.LinkUrl,
                "Note", f.Note,
                "Enable",f.Enable?"1":"0",
                "WebName",f.WebName.ToString(),
                "AutoKey",f.AutoKey.ToString()
            };
            if (f.AutoKey > 0)
            {
                sql = @"update FriendLink set 
                        LinkName=@LinkName,
                        LinkUrl=@LinkUrl, 
                        Note=@Note, 
                        Enable=@Enable,
                        WebName=@WebName
                        where autokey=@AutoKey";
                if (sql.ExecuteNoneQuery(list.ToArray()) > 0)
                    return f.AutoKey;
            }
            else
            {
                sql = @"insert into FriendLink( LinkName, LinkUrl, Note, CreateTime, Enable,WebName) 
                        values(@LinkName, @LinkUrl, @Note, getdate(), @Enable,@WebName) 
                        select isnull(SCOPE_IDENTITY(),0) as ID";
                return sql.ExecuteScalarInt(list.ToArray()).ToInt32Value();
            }
            return 0;
        }

        public FriendLinkTB GetFriendLink(int autokey)
        {
            return GetFriendLink("autokey", autokey.ToString());
        }

        public FriendLinkTB GetFriendLink(String colname,String colvalue)
        {
            String sql = String.Format("select * from FriendLink where {0}=@{0}", colname);
            return sql.ExecuteOneRecord<FriendLinkTB>(colname, colvalue);
        }

        public List<FriendLinkTB> GetFriendLinks(WebName webname)
        {
            String cachekey = "GetFriendLinks";
            List<FriendLinkTB> result = CacheHelper.ReadServerCache(cachekey) as List<FriendLinkTB>;
            if (result == null)
            {
                String sql = "select * from FriendLink where Enable=1 and webname=@webname order by autokey asc";
                result = sql.ExecuteRecords<FriendLinkTB>("webname", webname.ToString());
                CacheHelper.CreateServerCache(cachekey, result);
            }
            return result;
        }

        public List<FriendLinkTB> GetFriendLinks(String keyword, int pageSize, out int rowCount, int pageIndex, String IsEnable)
        {
            String orderby = "autokey desc";
            String where = "";
            List<String> arr = new List<String>();

            if (!keyword.IsNullOrEmpty())
            {
                where += " (LinkName like @keyword or LinkUrl like @keyword or Note like @keyword)";
                arr.Add("keyword");
                arr.Add("%" + keyword + "%");
            }

            if (!IsEnable.IsNullOrEmpty())
            {
                where += (where.IsNullOrEmpty() ? " " : " and ") + "Enable=@Enable";
                arr.Add("Enable");
                arr.Add(IsEnable);
            }

            return CurrentDataServer.GetPagingData<FriendLinkTB>("FriendLink", "*", orderby, where, pageSize, out rowCount, pageIndex, arr.ToArray());
        }
    }
}
