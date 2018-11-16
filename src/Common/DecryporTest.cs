using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class DecryporTest
    {
        //<4、 解密操作 >  
        //解密操作解密上面步骤生成的密文byte[]，需要使用到加密步骤使用的同一组Key和IV。  


        /// <summary>  
        /// 将一个加密后的二进制数据流进行解密，产生一个明文的二进制数据流  
        /// </summary>  
        /// <param name="EncryptedDataArray">加密后的数据流</param>  
        /// <param name="Key"></param>  
        /// <param name="IV"></param>  
        /// <returns>一个已经解密的二进制流</returns>  
        public static byte[] DecryptTextFromMemory(byte[] EncryptedDataArray, byte[] Key, byte[] IV)
        {

            // 建立一个MemoryStream，这里面存放加密后的数据流  

            MemoryStream msDecrypt = new MemoryStream(EncryptedDataArray);

            // 使用MemoryStream 和key、IV新建一个CryptoStream 对象  
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV), CryptoStreamMode.Read);

            // 根据密文byte[]的长度（可能比加密前的明文长），新建一个存放解密后明文的byte[]  
            byte[] DecryptDataArray = new byte[EncryptedDataArray.Length];

            // 把解密后的数据读入到DecryptDataArray  
            csDecrypt.Read(DecryptDataArray, 0, DecryptDataArray.Length);

            msDecrypt.Close();

            csDecrypt.Close();

            return DecryptDataArray;

        }
    }
}
