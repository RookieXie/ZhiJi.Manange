using Common;
using Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Redis
{
    /// <summary>
    /// redis缓存操作类
    /// </summary>
    public static class RedisExtend
    {




        public static bool LockKey(this IDatabase redis, string key)
        {
            return redis.LockTake(key,1,new TimeSpan(0,0,10));
        }
        public static bool LockRelease(this IDatabase redis, string key)
        {
            return redis.LockRelease(key);
        }
        ///// <summary>
        ///// 获取redis缓存操作单例对象
        ///// </summary>
        ///// <returns></returns>
        //public static redisHelper GetredisHelper()
        //{
        //    return new redisHelper();
        //    //if (redisHelper == null)
        //    //{

        //    //    redisHelper = new redisHelper();
        //    //}
        //    //return redisHelper;
        //}
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool SetCache(this IDatabase redis, string key, string str)
        {
            return redis.StringSet(key, str);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="str"></param>
        /// <param name="t">过期时间</param>
        /// <returns></returns>
        public static bool SetCache(this IDatabase redis, string key, string str, int t)
        {
            return redis.StringSet(key, str, new TimeSpan(0,0,t));
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="str"></param>
        /// <param name="t">过期时间</param>
        /// <returns></returns>
        public static bool SetCache(this IDatabase redis, string key, string str, TimeSpan t)
        {
            return redis.StringSet(key, str, t);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="t">过期时间</param>
        /// <returns></returns>
        public static bool SetCache(this IDatabase redis, string key, object obj, int t)
        {
            string json = JsonHelper.SerializeObject(obj);
            return redis.StringSet(key, json, new TimeSpan(0, 0, t));
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="t">过期时间</param>
        /// <returns></returns>
        public static bool SetCache(this IDatabase redis, string key, object obj, TimeSpan t)
        {
            string json = JsonHelper.SerializeObject(obj);
            return redis.StringSet(key, json, t);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool SetCache(this IDatabase redis, string key, object obj)
        {
            string json = JsonHelper.SerializeObject(obj);
            return redis.StringSet(key, json);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        public static T GetCache<T>(this IDatabase redis, string key) where T : class, new()
        {
            RedisValue json = redis.StringGet(key);

            if (json.IsNullOrEmpty)
            {
                return null;
            }
            if (typeof(T) == typeof(RedisValue))
            {
                return json as T;
            }

            return JsonHelper.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        public static string GetCache(this IDatabase redis, string key)
        {
            return redis.StringGet(key);

        }
        public static bool HashDelete(this IDatabase redis, RedisKey key, RedisValue hashField)
        {
            return redis.HashDelete(key.ToString(), hashField);
        }
        /// <summary>
        /// 自增count，返回自增后的值
        /// </summary>
        public static double StringIncrement(this IDatabase redis, string key, double count)
        {
            return redis.StringIncrement(key, count);
        }
        /// <summary>
        /// 
        /// </summary>
        public static RedisValue StringIncrement(this IDatabase redis, string key, RedisValue val)
        {
            return redis.StringIncrement(key, val);
        }
        public static bool HashExists(this IDatabase redis, string key, RedisValue val)
        {
            return redis.HashExists(key, val);
        }
        public static RedisValue HashGet(this IDatabase redis, RedisKey key, RedisValue hashFeld)
        {
            return redis.HashGet((key.ToString()), hashFeld);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        public static bool DeleteCache(this IDatabase redis, string key)
        {

            return redis.KeyDelete(key);
        }

        public static long DeleteCache(this IDatabase redis, string[] keys)
        {
            var redisKeys = new List<RedisKey>();
            foreach (var it in keys)
            {
                redisKeys.Append(it);
            }
            return redis.KeyDelete(redisKeys.ToArray()) ;
        }

        public static void HashSet(this IDatabase redis, RedisKey key, HashEntry[] fields)
        {
            redis.HashSet(key, fields);
        }
        public static void HashSet(this IDatabase redis, RedisKey key, RedisValue field, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            redis.HashSet(key, field, value, when, flags);
        }
        /// <summary>
        /// 获取缓存用户票证信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="versions">版本</param>
        /// <returns></returns>
        public static string GetSecretKeyCache(this IDatabase redis, string userid, string versions)
        {
            return redis.StringGet("SecretKey_" + userid + versions.Replace(".", ""));
        }
        /// <summary>
        /// 设置用户票证缓存
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="versions">版本</param>
        /// <param name="key">票证值</param>
        /// <returns></returns>
        public static bool SetSecretKeyCache(this IDatabase redis, string userid, string versions, string key)
        {
            return redis.StringSet("SecretKey_" + userid + versions.Replace(".", ""), key, new TimeSpan(24, 0, 0));
        }
    }
}
