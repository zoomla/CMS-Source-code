using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/*
 * Base64,Des,Aes,Rsa
 * 应转为base64或16进制输出，避免乱码
 */ 
public class EncryptHelper
{
    //-----------------------Base64
    public static string Base64Encrypt(string str)
    {
        byte[] bytes = Encoding.Default.GetBytes(str);
        return Convert.ToBase64String(bytes);
    }
    public static string Base64Decrypt(string str)
    {
        byte[] outputb = Convert.FromBase64String(str);
        string orgStr = Encoding.Default.GetString(outputb);
        return orgStr;
    }
    //-----------------------RSA(公钥与私钥)
    /// <summary>
    /// 返回RSA公钥与私钥,建议存为Xml
    /// </summary>
    /// <returns></returns>
    public static void GetRsaKey(ref string publicKey,ref string privateKey) 
    {
        RSACryptoServiceProvider rsaProvider;
        rsaProvider = new RSACryptoServiceProvider(1024);
        publicKey = rsaProvider.ToXmlString(false); //将RSA算法的公钥导出到字符串PublicKey中，参数为false表示不导出私钥
        privateKey = rsaProvider.ToXmlString(true);//将RSA算法的私钥导出到字符串PrivateKey中，参数为true表示导出私钥
    }
    public static string RsaEncrypt(string encryptString, string key)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        byte[] cipherbytes;
        rsa.FromXmlString(key);
        cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(encryptString), false);
        return Convert.ToBase64String(cipherbytes);
    }
    public static string RsaDecrypt(string decryptString, string key)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        byte[] cipherbytes;
        rsa.FromXmlString(key);
        cipherbytes = rsa.Decrypt(Convert.FromBase64String(decryptString), false);
        return Encoding.UTF8.GetString(cipherbytes);
    }
    //-----------------------DES(建议用DES而非TripleDES)
    /*
     * DES使用的密钥key为8字节，初始向量IV也是8字节。
     * TripleDES使用24字节的key，初始向量IV也是8字节。
     *使用DES吧Triple需要三次，消耗性能是Des的三倍，密钥长度长
     *使用:cryHelper.DesInit();string es = cryHelper.DesEncrypt(NeedEncryptStr);string ds = cryHelper.DesDecrypt(es);
     */
    private static readonly string TripleDesKey = "AdGDE=GHIeKLMs==";//TripleDes的初始向量和密钥,不能少于这个位数
    private static readonly string DesKey = "seKLMs==";//向量与密钥均使用其
    private ICryptoTransform encryptor;     // 加密器对象
    private ICryptoTransform decryptor;     // 解密器对象
    private const int BufferSize = 1024;
    //构造，初始化加密与解密容器，获取向量与密钥,
    //无参构造，默认使用TripleDES算法,其提供的key位数更多，加密可靠性更高。
    public void DesInit(string algorithmName = "DES") //TripleDES
    {
        SymmetricAlgorithm provider = SymmetricAlgorithm.Create(algorithmName);
        //provider.IV = Convert.FromBase64String(IV);
        switch (algorithmName)
        {
            case "DES":
                provider.Key = Encoding.UTF8.GetBytes(DesKey);
                break;
            case "TripleDES":
                provider.Key = Encoding.UTF8.GetBytes(TripleDesKey);
                break;
            default:
                throw new Exception("选择不正确,DES||TripleDES");
        }
        //密钥和初始向量
        provider.IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        encryptor = provider.CreateEncryptor();
        decryptor = provider.CreateDecryptor();
    }
    //-----Des加解密，直解用
    public static string DesEncrypt(string clearText)
    {
        try
        {
            //定义DES加密服务提供类
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //加密字符串转换为byte数组
            byte[] inputByte = System.Text.ASCIIEncoding.UTF8.GetBytes(clearText);
            //加密密匙转化为byte数组
            byte[] key = Encoding.ASCII.GetBytes(DesKey); //DES密钥(必须8字节)
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
    public static string DesDecrypt(string encryptedText)
    {
        try
        {
            //定义DES加密解密服务提供类
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //加密密匙转化为byte数组
            byte[] key = Encoding.ASCII.GetBytes(DesKey);
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
    //-----------------------AES(推荐用AES替代DES,可写Java兼容互转,详见Sohu畅言项目,16进制输出)
    //注意经过其加密后解密的字符,与原字符已经不一样了,虽然输出值相同,但Length与实际存储的方式已经完全更入了
    //DES,Base64同样不能直接比较,需要将字符加密后再比较
    //默认密钥向量 
    private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    private static string aesKey = "eeeezinzlia=xiny";//密钥,16*8=128位
    /// <summary>
    /// AES加密算法
    /// </summary>
    /// <param name="plainText">明文字符串</param>
    /// <param name="strKey">密钥</param>
    /// <returns>返回加密后的密文字节数组</returns>
    public static string AESEncrypt(string plainText, byte[] key =null)
    {
        if (key == null) { key = _key1; }
        //分组加密算法
        SymmetricAlgorithm des = Rijndael.Create();
        byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组	
        //设置密钥及密钥向量
        des.Key = Encoding.UTF8.GetBytes(aesKey);
        des.IV = key;
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        cs.Close();
        ms.Close();
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            //向可变字符串追加转换成十六进制数字符串的加密后byte数组。
            ret.AppendFormat("{0:X2}", b);
        }
        return ret.ToString();
    }
    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="cipherText">密文字节数组</param>
    /// <param name="strKey">密钥</param>
    /// <returns>返回解密后的字符串</returns>
    public static string AESDecrypt(string cryedText, byte[] key = null)
    {
        if (key == null) { key = _key1; }
        int length = (cryedText.Length / 2);
        byte[] cipherText = new byte[length];
        for (int index = 0; index < length; index++)
        {
            string substring = cryedText.Substring(index * 2, 2);
            cipherText[index] = Convert.ToByte(substring, 16);
        }
        SymmetricAlgorithm des = Rijndael.Create();
        des.Key = Encoding.UTF8.GetBytes(aesKey);
        des.IV = key;
        byte[] decryptBytes = new byte[cipherText.Length];
        MemoryStream ms = new MemoryStream(cipherText);
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
        cs.Read(decryptBytes, 0, decryptBytes.Length);
        cs.Close();
        ms.Close();
        return System.Text.Encoding.UTF8.GetString(decryptBytes);
    }
    //-----------------------SHA哈希加密(SHA1,SHA256主用于网上API接口)
    public static string SHA256(string str)
    {
        //如果str有中文，不同Encoding的sha是不同的！！
        byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
        SHA256Managed Sha256 = new SHA256Managed();
        byte[] by = Sha256.ComputeHash(SHA256Data);
        return BitConverter.ToString(by).Replace("-", "").ToLower(); //64
        //return Convert.ToBase64String(by);                         //44
    }
    public static string SHA1(string str)
    {
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
    }
}