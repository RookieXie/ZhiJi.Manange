using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Redis
{
    public class RedisCore
    {
        private readonly string _connectionString;
        private readonly int _dbIndex;



        public RedisCore(string connectionString, int dbIndex)
        {
            _connectionString = connectionString;
            _dbIndex = dbIndex;
            //var conn = ConnectionMultiplexer.Connect(_connectionString);
            //_redisDB = conn.GetDatabase(_dbIndex);
        }



        public IDatabase RedisDB
        {
            get
            {
                var conn = ConnectionMultiplexer.Connect(_connectionString);
                return conn.GetDatabase(_dbIndex);
            }
        }
    }
}
