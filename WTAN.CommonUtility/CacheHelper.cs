using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace WTAN.CommonUtility
{
    public class CacheHelper
    {
        /// <summary>
        /// 清除服务器的所有缓存
        /// </summary>
        /// <returns></returns>
        public static void ClearServerCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            foreach (string key in al)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 清除服务器某个缓存
        /// </summary>
        /// <param name="cachekey"></param>
        public static void ClearServerCache(String cachekey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(cachekey);
        }

        /// <summary>
        /// 创建缓存
        /// </summary>
        /// <param name="cacheKey">取缓存的KEY</param>
        /// <param name="data">缓存的数据</param>
        /// <param name="cacheDuration">缓存时间长度的数字 单位M</param>
        public static void CreateServerCache(string cacheKey, object data, int cacheDuration = 0)
        {
            if (AppSettings.IsStartCache && data != null)
            {
                DateTime d = DateTime.Now.AddMinutes(cacheDuration == 0 ? AppSettings.CacheTime : cacheDuration);//不设置时长时 默认使用配置文件中缓存时长
                HttpRuntime.Cache.Insert(cacheKey, data, null, d, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
        }

        /// <summary>
        /// 保存系統配置緩存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="data"></param>
        public static void CreateSystemServerCache(string cacheKey, object data)
        {
            DateTime d = DateTime.Now.AddDays(1);
            HttpRuntime.Cache.Insert(cacheKey, data, null, d, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        /// <summary>
        /// 创建有数据表依赖项的缓存
        /// </summary>
        /// <param name="cacheKey">取缓存的KEY</param>
        /// <param name="aggregatecachedependency">缓存依赖项，当该依赖项关联的表发生变化后，缓存就发生变化</param>
        /// <param name="data">缓存的数据</param>
        /// <param name="cacheDuration">缓存时间长度的数字</param>
        public static void CreateServerCache(string cacheKey, AggregateCacheDependency aggregatecachedependency, object data, int cacheDuration = 0)
        {
            if (AppSettings.IsStartCache && data != null)
            {
                DateTime d = DateTime.Now.AddMinutes(cacheDuration == 0 ? AppSettings.CacheTime : cacheDuration);//不设置时长时 默认使用配置文件中缓存时长
                HttpRuntime.Cache.Add(cacheKey, data, aggregatecachedependency, d, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
        }

        /// <summary>
        /// 根据缓存key得到缓存
        /// </summary>
        /// <param name="cacheKey">缓存key</param>
        /// <returns></returns>
        public static object ReadServerCache(string cacheKey)
        {
            object data = null;
            if (AppSettings.IsStartCache)
            {
                data = HttpRuntime.Cache[cacheKey];
            }
            return data;
        }

        /// <summary>
        /// 根据缓存key得到缓存，返回字符串
        /// </summary>
        /// <param name="cacheKey">缓存key</param>
        /// <returns></returns>
        public static string ReadServerCacheText(string cacheKey)
        {
            object data = null;
            if (AppSettings.IsStartCache)
            {
                data = HttpRuntime.Cache[cacheKey];
            }

            if (data != null)
                return data.ToString();
            else
                return String.Empty;
        }
    }
}
