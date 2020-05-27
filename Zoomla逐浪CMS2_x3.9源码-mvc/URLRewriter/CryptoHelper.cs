using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace URLRewriter
{
    /* 
     *功 能:将字符串加密解密 
     *原 理:使用对称密钥加密解密 
     * 
     */
    public class CryptoHelper
    {
        //--------TripleDes
        //private readonly string IV = "SuFjcEmp/TE=";//TripleDes的初始向量和密钥,不能少于这个位数
        private readonly string TripleDesKey = "AdGDE=GHIeKLMs==";
        private ICryptoTransform encryptor;     // 加密器对象
        private ICryptoTransform decryptor;     // 解密器对象
        private const int BufferSize = 1024;
        //---------Des
        private string _DESKey = "SDZE=FX=";//向量与密钥均使用这个吧

        //构造，初始化加密与解密容器，获取向量与密钥
        public CryptoHelper(string algorithmName)//TripleDES
        {
            SymmetricAlgorithm provider = SymmetricAlgorithm.Create(algorithmName);
            //provider.IV = Convert.FromBase64String(IV);
            //密钥和初始向量
            provider.Key = Encoding.UTF8.GetBytes(TripleDesKey);
            provider.IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            encryptor = provider.CreateEncryptor();
            decryptor = provider.CreateDecryptor();
        }

        //无参构造，默认使用TripleDES算法,其提供的key位数更多，加密可靠性更高。
        public CryptoHelper(): this("TripleDES")
        {}
        //-----Des加解密，直解用
        public string Encrypt(string clearText)
        {
            try
            {
                //定义DES加密服务提供类
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //加密字符串转换为byte数组
                byte[] inputByte = System.Text.ASCIIEncoding.UTF8.GetBytes(clearText);
                //加密密匙转化为byte数组
                byte[] key = Encoding.ASCII.GetBytes(_DESKey); //DES密钥(必须8字节)
                des.Key = key;
                des.IV = key;
                //创建其支持存储区为内存的流
                MemoryStream ms = new MemoryStream();
                //定义将数据流链接到加密转换的流
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByte, 0, inputByte.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    //向可变字符串追加转换成十六进制数字符串的加密后byte数组。
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch { return clearText; }

        }
        public string Decrypt(string encryptedText)
        {
            try
            {
                //定义DES加密解密服务提供类
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //加密密匙转化为byte数组
                byte[] key = Encoding.ASCII.GetBytes(_DESKey);
                des.Key = key;
                des.IV = key;
                //将被解密的字符串每两个字符以十六进制解析为byte类型，组成byte数组
                int length = (encryptedText.Length / 2);
                byte[] inputByte = new byte[length];
                for (int index = 0; index < length; index++)
                {
                    string substring = encryptedText.Substring(index * 2, 2);
                    inputByte[index] = Convert.ToByte(substring, 16);
                }
                //创建其支持存储区为内存的流
                MemoryStream ms = new MemoryStream();
                //定义将数据流链接到加密转换的流
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByte, 0, inputByte.Length);
                cs.FlushFinalBlock();

                return ASCIIEncoding.UTF8.GetString((ms.ToArray()));
            }
            catch { return encryptedText; }
        }
        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="clearText">需加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public string TripleDesEncrypt(string clearText)
        {
            try
            {
                // 创建明文流
                byte[] clearBuffer = Encoding.UTF8.GetBytes(clearText);
                MemoryStream clearStream = new MemoryStream(clearBuffer);

                // 创建空的密文流
                MemoryStream encryptedStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write);

                // 将明文流写入到buffer中
                // 将buffer中的数据写入到cryptoStream中
                int bytesRead = 0;
                byte[] buffer = new byte[BufferSize];
                do
                {
                    bytesRead = clearStream.Read(buffer, 0, BufferSize);
                    cryptoStream.Write(buffer, 0, bytesRead);
                } while (bytesRead > 0);

                cryptoStream.FlushFinalBlock();

                // 获取加密后的文本
                buffer = encryptedStream.ToArray();
                string encryptedText = Convert.ToBase64String(buffer);
                return encryptedText;
            }
            catch { return clearText; }
        }
       /// <summary>
       /// 解密算法
       /// </summary>
       /// <param name="encryptedText">需解密的字符串</param>
       /// <returns>解密后的字符串</returns>
        public string TripleDesDecrypt(string encryptedText)
        {
            try
            {
                byte[] encryptedBuffer = Convert.FromBase64String(encryptedText);

                Stream encryptedStream = new MemoryStream(encryptedBuffer);

                MemoryStream clearStream = new MemoryStream();
                CryptoStream cryptoStream =
                    new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read);

                int bytesRead = 0;
                byte[] buffer = new byte[BufferSize];

                do
                {
                    bytesRead = cryptoStream.Read(buffer, 0, BufferSize);
                    clearStream.Write(buffer, 0, bytesRead);
                } while (bytesRead > 0);

                buffer = clearStream.GetBuffer();
                string clearText =
                    Encoding.UTF8.GetString(buffer, 0, (int)clearStream.Length);

                return clearText;
            }
            catch { return encryptedText; }
        }
    }
}
#region 扩展与静态方法
   // /// <summary>
   ///// 自定义密钥加密文本
   ///// </summary>
   ///// <param name="clearText">字符串</param>
   ///// <param name="key">密钥</param>
   ///// <returns></returns>
   // public static string Encrypt(string clearText, string key)
   // {
   //     CryptoHelper helper = new CryptoHelper(key);
   //     return helper.Encrypt(clearText);
   // }
   // /// <summary>
   // /// 解密自定义密钥文本
   // /// </summary>
   // /// <param name="clearText">字符串</param>
   // /// <param name="key">密钥</param>
   // /// <returns></returns>
   // public static string Decrypt(string encryptedText, string key)
   // {
   //     CryptoHelper helper = new CryptoHelper(key);
   //     return helper.Decrypt(encryptedText);
   // }


/*
 * 扩展:
 * DES使用的密钥key为8字节，初始向量IV也是8字节。
 * TripleDES使用24字节的key，初始向量IV也是8字节。
 *使用DES吧Triple需要三次，消耗性能是Des的三倍，密钥长度长，我们安全没这么高需求 
 */
#endregion