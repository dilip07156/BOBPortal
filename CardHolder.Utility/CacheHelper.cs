using System;
using System.Web;

namespace CardHolder.Utility
{

    /// <summary>
    /// Cache helper class with session key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks></remarks>
    public static class CacheHelperBySession<T>
    {
        //public static string TopMasterKey = "";

        /// <summary>
        /// Masters the cache key array.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string[] MasterCacheKeyArray()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
                return new string[] { HttpContext.Current.Session.SessionID.ToString() + typeof(T).ToString() };
            else
                return new string[] { typeof(T).ToString() };
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray()[0], "-", cacheKey);
        }

        /// <summary>
        /// Invalidates the cache.
        /// </summary>
        /// <remarks></remarks>
        public static void InvalidateCache()
        {
            HttpRuntime.Cache.Remove(MasterCacheKeyArray()[0]);
        }

        /// <summary>
        /// Gets the cache item.
        /// </summary>
        /// <param name="rawKey">The raw key.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T GetCacheItem(string rawKey)
        {
            return (T)HttpRuntime.Cache[GetCacheKey(rawKey)];
        }

        /// <summary>
        /// Adds the cache item.
        /// </summary>
        /// <param name="rawKey">The raw key.</param>
        /// <param name="value">The value.</param>
        /// <param name="ExpirationTime">The expiration time.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T AddCacheItem(string rawKey, object value, DateTime ExpirationTime)
        {
            System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

            if (DataCache[MasterCacheKeyArray()[0]] == null)
                DataCache[MasterCacheKeyArray()[0]] = DateTime.Now;

            System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray());
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, ExpirationTime, System.Web.Caching.Cache.NoSlidingExpiration);

            return (T)value;
        }

        /// <summary>
        /// Adds the cache item.
        /// </summary>
        /// <param name="rawKey">The raw key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T AddCacheItem(string rawKey, object value)
        {
            System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

            if (DataCache[MasterCacheKeyArray()[0]] == null)
                DataCache[MasterCacheKeyArray()[0]] = DateTime.Now;

            System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray());
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddMinutes(60), System.Web.Caching.Cache.NoSlidingExpiration);

            return (T)value;
        }
    }


    #region commentPArt
    ///// <summary>
    ///// Cache helper class.
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <remarks></remarks>
    //public static class CacheHelper<T>
    //{
    //    /// <summary>
    //    /// Master cache key array.
    //    /// </summary>
    //    public static string[] MasterCacheKeyArray = { typeof(T).ToString() };

    //    /// <summary>
    //    /// Gets the cache key.
    //    /// </summary>
    //    /// <param name="cacheKey">The cache key.</param>
    //    /// <returns>Return cache key string.</returns>
    //    /// <remarks></remarks>
    //    public static string GetCacheKey(string cacheKey)
    //    {
    //        return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
    //    }

    //    /// <summary>
    //    /// Invalidates the cache.
    //    /// </summary>
    //    /// <remarks></remarks>
    //    public static void InvalidateCache()
    //    {
    //        HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
    //    }

    //    /// <summary>
    //    /// Gets the cache item.
    //    /// </summary>
    //    /// <param name="rawKey">The raw key.</param>
    //    /// <returns>Return cache object of type T.</returns>
    //    /// <remarks></remarks>
    //    public static T GetCacheItem(string rawKey)
    //    {
    //        return (T)HttpRuntime.Cache[GetCacheKey(rawKey)];
    //    }

    //    /// <summary>
    //    /// Adds the cache item.
    //    /// </summary>
    //    /// <param name="rawKey">The raw key.</param>
    //    /// <param name="value">The value.</param>
    //    /// <returns>Return Cache object of type T.</returns>
    //    /// <remarks></remarks>
    //    public static T AddCacheItem(string rawKey, object value)
    //    {
    //        System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

    //        if (DataCache[MasterCacheKeyArray[0]] == null)
    //            DataCache[MasterCacheKeyArray[0]] = DateTime.Now;

    //        System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
    //        DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddDays(60), System.Web.Caching.Cache.NoSlidingExpiration);

    //        return (T)value;
    //    }
    //}
    #endregion
}