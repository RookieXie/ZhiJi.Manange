using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

using Encryption;

using System.Runtime.CompilerServices;
using System.Threading;
using YYLog.ClassLibrary;

namespace DBMonoUtility
{
    public class DataBasePool : IDisposable
    {
        //private IDbConnection mConnection;
        private static IniFile mIniFile = new IniFile();
        private static Hashtable mConnectionTable = new Hashtable();

        private static int _maxIdle = 5 * 60 * 1000;
        private static Hashtable _pools = new Hashtable();
        private static Hashtable _busyPools = new Hashtable();
        private static int _minConns = 5;
        private static int _maxConns = 50;
        private static Thread _checkThread = null;
        private static bool _isBreak = false;
        public DataBasePool()
        {
        }

        private static string getInitConnectionString(string poolName)
        {
            string val = mIniFile.GetInitPramas(poolName, "DBCONNECTIONSTRING");
            return val;
        }

        private static string getInitVector(string poolName)
        {
            string val = mIniFile.GetInitPramas(poolName, "INITVECTOR");
            return val;
        }

      

        public static string AddDataBaseConnectionString(string poolName, string publicKey, int minConns, int initConns)
        {
            _minConns = minConns;
            poolName = poolName.Trim().ToLower();
            string connectionString = getInitConnectionString(poolName);
            string initVector = getInitVector(poolName);

            //test-----------
            var initvec = Convert.FromBase64String(initVector);
            var destBytes =  Common.DecryporTest.DecryptTextFromMemory(Convert.FromBase64String(connectionString), Encoding.ASCII.GetBytes(publicKey), Convert.FromBase64String(initVector));
            //-----------------
            var destStr = Encoding.ASCII.GetString(destBytes);
            //if (String.IsNullOrEmpty(connectionString) || String.IsNullOrEmpty(initVector))
            //{
            //    return "config exception";
            //}
            //Decryptor dec = new Decryptor(EncryptionAlgorithm.TripleDes);
            //dec.InitVec = Convert.FromBase64String(initVector);
            //byte[] plain = dec.Decrypt(Convert.FromBase64String(connectionString), Encoding.ASCII.GetBytes(publicKey));
            //destStr = Encoding.ASCII.GetString(plain);
            //if (!mConnectionTable.ContainsKey(poolName))
            //{
            //    mConnectionTable.Add(poolName, destStr);
            //}
            //else
            //{
            //    mConnectionTable[poolName] = destStr;
            //}

            return destStr;
        }

        public void Dispose()
        {
            //Close ();
            GC.SuppressFinalize(this);
        }

     

       
       

        public static void ReleaseConnection(string poolName, IDbConnection conn)
        {
            if (null == conn) return;
            poolName = poolName.Trim().ToLower();
            lock (_busyPools)
            {
                _busyPools.Remove(conn);
            }

            Hashtable conns = null;
            lock (_pools)
            {
                conns = _pools[poolName] as Hashtable;

                if (null != conns)
                {
                    conns[conn] = DateTime.Now;
                    _pools[poolName] = conns;
                }
            }
        }

        
    }
}


