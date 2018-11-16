using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using YYLog.ClassLibrary;

namespace Common.Redis
{
    /// <summary>
    /// Redis操作类，内部采用了连接池
    /// </summary>
    public class Redis
    {
        RedisPools _pool = null;
        private int dbIndex = 1;

        public int DbIndex
        {
            set
            {
                dbIndex = value;
            }
        }

        public Redis()
        {
            _pool = RedisPools.GetInstance("TestRedisPool");
        }

        public Redis(string poolName)
        {
            _pool = RedisPools.GetInstance(poolName);
        }

        public long HashDelete(RedisKey key, RedisValue hashField)
        {
            return HashDelete(key, new RedisValue[] { hashField });
        }

        public long HashDelete(RedisKey key, RedisValue[] hashField)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                IDatabase db = conn.GetDatabase(dbIndex);

                return db.HashDelete(key, hashField);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashExists", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return -1;
        }

        public long KeyDelete(RedisKey key)
        {
            return KeyDelete(new RedisKey[] { key });
        }

        public long KeyDelete(RedisKey[] key)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                IDatabase db = conn.GetDatabase(dbIndex);

                return db.KeyDelete(key);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashExists", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return -1;
        }

        public bool StringSet(string key, RedisValue val)
        {
            return StringSet(key, val, TimeSpan.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="t">失效时间，单位秒</param>
        /// <returns></returns>
        public bool StringSet(string key, RedisValue val, int t)
        {
            TimeSpan ts = new TimeSpan(0, 0, t);
            return StringSet(key, val, ts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public bool StringSet(string key, RedisValue val, TimeSpan ts)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();

                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::StringSet", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.StringSet(key, val, ts);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::StringSet", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return false;
        }

        public bool KeyExists(string key)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::KeyExists", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);

                    return db.KeyExists(key);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::KeyExists", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return false;
        }

        public bool HashExists(string key, RedisValue val)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::HashExists", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);

                    return db.HashExists(key, val);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashExists", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public RedisValue[] HashKeys(string key, CommandFlags flags = CommandFlags.None)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::HashKeys", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.HashKeys(key, flags);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashKeys", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return null;
        }

        /// <summary>
        /// 返回名称为key的hash中field对应的value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public RedisValue HashGet(RedisKey key, RedisValue field, CommandFlags flags = CommandFlags.None)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::HashGet", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.HashGet(key, field, flags);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashGet", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return RedisValue.Null;
        }

        /// <summary>
        /// 向名称为key的hash中添加元素field<—>value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        public void HashSet(RedisKey key, HashEntry[] fields)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::HashSet", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    db.HashSet(key, fields);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashSet", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }
        }

        /// <summary>
        /// 向名称为key的hash中添加元素field<—>value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        public void HashSet(RedisKey key, RedisValue field, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::HashSet", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    db.HashSet(key, field, value, when, flags);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashSet", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }
        }

        public IEnumerable<HashEntry> HashScan(RedisKey key, RedisValue pattern, int pageSize, CommandFlags flags = CommandFlags.None)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::HashScan", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.HashScan(key, pattern, pageSize, flags);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashScan", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return null;
        }
        public RedisValue StringIncrement(string key, RedisValue val)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::StringIncrement", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);

                    return db.HashGet(key, val);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::StringIncrement", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return 0;
        }

        public double StringIncrement(string key, double d)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::StringIncrement", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.StringIncrement(key, d);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::StringIncrement", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return -1;
        }

        public RedisValue HashGet(RedisKey key, RedisValue hashFeld)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::HashGet", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.HashGet(key, hashFeld);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashGet", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return RedisValue.EmptyString;
        }

        public RedisValue[] HashGet(RedisKey key, RedisValue[] hashFelds)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::HashGet", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.HashGet(key, hashFelds);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::HashGet", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return new RedisValue[] { };
        }

        public RedisValue StringGet(string key)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::StringGet", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.StringGet(key);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::StringGet", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// 分步式锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds">锁定时间</param>
        /// <returns></returns>
        public bool LockTake(RedisKey key, int seconds)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::LockTake", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.LockTake(key, Environment.MachineName, TimeSpan.FromSeconds(seconds));
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::LockTake", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return false;
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool LockRelease(RedisKey key)
        {
            ConnectionMultiplexer conn = null;

            try
            {
                conn = _pool.GetConnection();
                if (null == conn)
                {
                    Log.WriteErrorLog("Redis::LockRelease", "获取连接返回为空。");
                }
                else
                {
                    IDatabase db = conn.GetDatabase(dbIndex);
                    return db.LockRelease(key, Environment.MachineName);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLog("Redis::LockRelease", ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    _pool.ReleaseConnection(conn);
                }
            }

            return false;
        }
    }
}
