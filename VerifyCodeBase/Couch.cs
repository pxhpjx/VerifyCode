using Couchbase;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VerifyCodeBase
{
    public class Couch
    {
        private static CouchbaseClient cc = new CouchbaseClient();


        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Add(string key, object value, DateTime dateTime)
        {
            var result = cc.Store(StoreMode.Set, key, value, dateTime);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        public static object Get(string key)
        {
            var result = cc.Get(key);
            return result;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            var result = cc.Remove(key);
        }
    }
}
