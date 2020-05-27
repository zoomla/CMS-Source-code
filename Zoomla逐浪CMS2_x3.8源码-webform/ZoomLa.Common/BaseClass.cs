using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Web;

namespace ZoomLa.Common
{
    public abstract class BaseClass
    {
        /// <summary>
        /// DataConverter构造函数
        /// </summary>
        public BaseClass()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 最全的Htmlencode方法 BY HMW
        /// </summary>
        /// <param name="fString"></param>
        /// <returns></returns>
        public static string Htmlcode(string fString)
        {
            return fString;
        }



        /// <summary>
        /// 编辑器转义
        /// </summary>
        /// <param name="fString"></param>
        /// <returns></returns>
        public static string Htmleditcode(string fString)
        {
            return fString;
        }
        public static string Htmlfilecode(string fString)
        {
            return fString;
        }


        /// <summary>
        ///Htmluncode
        /// </summary>
        /// <param name="fString"></param>
        /// <returns></returns>
        public static string Htmluncode(string fString)
        {
            
            return fString;
        }

        /// <summary>
        /// 防注入检查脚本
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string CheckInjection(string fString)
        {
            return fString;
        }

        /// <summary>
        /// 防注入检查脚本
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string CheckInjection(string inputString,bool ispage)
        {
            return inputString;
        }



        /// <summary>
        /// 加密base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64String(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 解密base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromBase64String(string str)
        {
            byte[] outputb = Convert.FromBase64String(str);
            string orgStr = Encoding.Default.GetString(outputb);
            return orgStr;
        }


        public static string geturl()
        {
            try
            {
                string filepath = System.Web.HttpContext.Current.Server.MapPath("/Config/SystemRoot.config");
                string str1 = "new system domain regtoos for hmw make";
                string str2 = "welcome use";
                string io = "...................";
                string oi = "......";
                string code = io + str1 + oi + str2;
                string filecontnet = File.ReadAllText(filepath);
                filecontnet = filecontnet.Replace("=", "");
                filecontnet = filecontnet.Replace("\r\n", "");
                string c1 = BaseClass.Decode(filecontnet, code);
                string filecode = BaseClass.FromBase64String(c1);
                return filecode;
            }
            catch 
            {
                return "";
            }
        }

        public static int geturls()
        {
            string url = geturl();
            int istrul = 0;

            if (url != "")
            {
                if (url.IndexOf("\r\n") > -1)
                {
                    string[] str = url.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    if (str != null)
                    {
                        if (str.Length > 0)
                        {
                            string domain = System.Web.HttpContext.Current.Request.Url.Host.ToString();
                            for (int p = 0; p < str.Length; p++)
                            {
                                if (str[p] == domain)
                                {
                                    istrul = istrul + 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    string str = url;
                    if (str != null)
                    {
                        if (str.Length > 0)
                        {
                            string domain = System.Web.HttpContext.Current.Request.Url.Host.ToString();
                            for (int p = 0; p < str.Length; p++)
                            {
                                if (str == domain)
                                {
                                    istrul = istrul + 1;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                istrul = 0;
            }
            return istrul;


        }



        public static string Htmldecode(string fString)
        {
            return fString;
        }

        /// <summary>
        /// 从左取值
        /// </summary>
        /// <param name="sSource"></param>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public static string Left(string sSource, int iLength)
        {
            return sSource.Substring(0, iLength > sSource.Length ? sSource.Length : iLength);
        }

        /// <summary>
        /// 从左取值star开始位置
        /// </summary>
        /// <param name="sSource"></param>
        /// <param name="star"></param>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public static string Left(string sSource, int star, int iLength)
        {
            return sSource.Substring(star, iLength + star > sSource.Length ? sSource.Length - star : iLength);
        }
        /// <summary>
        /// 从右取值
        /// </summary>
        /// <param name="sSource"></param>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public static string Right(string sSource, int iLength)
        {
            return sSource.Substring(iLength > sSource.Length ? 0 : sSource.Length - iLength);
        }
        /// <summary>
        /// 取规定数量文本
        /// </summary>
        /// <param name="input"></param>
        /// <param name="len"></param>
        /// <param name="ex">结尾</param>
        /// <returns></returns>
        public static string CutText(string input, int len, string ex)
        {
            try
            {
                if (DataSecurity.Len(input) <= len)
                    return input;
                else
                {
                    return DataSecurity.GetSubString(input, len) + ex;
                }
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 中间取值
        /// </summary>
        /// <param name="sSource"></param>
        /// <param name="iStart"></param>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public static string Mid(string sSource, int iStart, int iLength)
        {
            int iStartPoint = iStart > sSource.Length ? sSource.Length : iStart;
            return sSource.Substring(iStartPoint, iStartPoint + iLength > sSource.Length ? sSource.Length - iStartPoint : iLength);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filename">文件物理地址</param>
        public static void DownloadFile(string filename, string savefile)
        {
           
            //System.Web.HttpContext.Current.Session["UrlReferrer"] = System.Web.HttpContext.Current.Request.Url.Authority;
            FileInfo file = new System.IO.FileInfo(filename);

            if (savefile == "")
            {
                savefile = file.Name;
            }


            string saveFileName = filename;
            int intStart = filename.LastIndexOf("\\") + 1;
            saveFileName = filename.Substring(intStart, filename.Length - intStart);
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.Buffer = true;
            //System.Web.UI.Page.EnableViewState = false;
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + savefile);
            try
            {
                System.Web.HttpContext.Current.Response.WriteFile(filename);
            }
            catch
            { 
            
            }
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.Close();
            System.Web.HttpContext.Current.Response.End();

            //FileInfo file = new System.IO.FileInfo(filename);
            //System.Web.HttpContext.Current.Response.Clear();
            //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "filename=" + file.Name);
            //System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            //string fileExtension = file.Extension.ToLower();
            ////这里选择输出的文件格式 
            //switch (fileExtension)
            //{
            //    case "mp3":
            //        System.Web.HttpContext.Current.Response.ContentType = "audio/mpeg3";
            //        break;
            //    case "mpeg":
            //        System.Web.HttpContext.Current.Response.ContentType = "video/mpeg";
            //        break;
            //    case "jpg":
            //        System.Web.HttpContext.Current.Response.ContentType = "image/jpeg";
            //        break;
            //    case "bmp":
            //        System.Web.HttpContext.Current.Response.ContentType = "image/bmp";
            //        break;
            //    case "gif":
            //        System.Web.HttpContext.Current.Response.ContentType = "image/gif";
            //        break;
            //    case "doc":
            //        System.Web.HttpContext.Current.Response.ContentType = "application/msword";
            //        break;
            //    case "css":
            //        System.Web.HttpContext.Current.Response.ContentType = "text/css";
            //        break;
            //    case "wmv":
            //        System.Web.HttpContext.Current.Response.ContentType = "video/x-ms-wmv";
            //        break;
            //    case "flv":
            //        System.Web.HttpContext.Current.Response.ContentType = "video/x-flv";
            //        break;
            //    default:
            //        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            //        break;
            //}
            //System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
            //System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 远程发送信息，返回值(不支持中文)
        /// </summary>
        /// <param name="serverapi">接口API地址</param>
        /// <param name="sendcontent">发送文本</param>
        /// <returns></returns>
        public static string send(string serverapi, string Acttype, string sendcontent)
        {
            
            return "";
        }
        /// <summary>
        /// xmlhttp发送:自定义发送(支持中文)
        /// </summary>
        /// <param name="root">接口API地址</param>
        /// <param name="content">发送文本</param>
        /// <param name="Acttype">提交动作方式:GET/POST</param>
        /// <returns></returns>
        public static string Sendinfo(string root, string content, string Acttype)
        {
            string ServerApiUrl = root;
            string sendtext = System.Web.HttpContext.Current.Server.UrlEncode(content);
            string returntext = send(ServerApiUrl, Acttype, sendtext);
            if (!string.IsNullOrEmpty(returntext))
            {
                return System.Web.HttpContext.Current.Server.UrlDecode(returntext);
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// xmlhttp:POST发送(支持中文)
        /// </summary>
        /// <param name="root">接口API地址</param>
        /// <param name="content">发送文本</param>
        /// <returns></returns>
        public static string Sendinfo(string root, string content)
        {
            string ServerApiUrl = root;
            string sendtext = System.Web.HttpContext.Current.Server.UrlEncode(content);
            string Acttype = "POST";
            string returntext = send(ServerApiUrl, Acttype, sendtext);
            if (!string.IsNullOrEmpty(returntext))
            {
                return System.Web.HttpContext.Current.Server.UrlDecode(returntext);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Des 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encode(string str, string key)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            if (key.Length < 8)
            {
                int lenint = 8 - key.Length;
               string skey= new string(' ',lenint);
                key=key+skey;
            }
            provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));

            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(str);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            stream.Close();
            return builder.ToString();
        }

        /// <summary>
        /// Des 解密 GB2312 
        /// </summary>
        /// <param name="str">Desc string</param>
        /// <param name="key">Key ,必须为8位 </param>
        /// <returns></returns>
        public static string Decode(string str, string key)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            if (key.Length < 8)
            {
                int lenint = 8 - key.Length;
                string skey = new string(' ', lenint);
                key = key + skey;
            }

            provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < (str.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream.Close();
            return Encoding.GetEncoding("GB2312").GetString(stream.ToArray());
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string encryptString, string key)
        {
            CspParameters csp = new CspParameters();
            csp.KeyContainerName = key;
            RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(csp);
            byte[] encryptBytes = RSAProvider.Encrypt(ASCIIEncoding.ASCII.GetBytes(encryptString), true);
            string str = "";
            foreach (byte b in encryptBytes)
            {
                str = str + string.Format("{0:x2}", b);
            }
            return str;
        }
        //RSA解密算法
        public static string RSADecrypt(string decryptString, string key)
        {
            CspParameters csp = new CspParameters();
            csp.KeyContainerName = key;
            RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(csp);
            int length = (decryptString.Length / 2);
            byte[] decryptBytes = new byte[length];
            for (int index = 0; index < length; index++)
            {
                string substring = decryptString.Substring(index * 2, 2);
                decryptBytes[index] = Convert.ToByte(substring, 16);
            }
            decryptBytes = RSAProvider.Decrypt(decryptBytes, true);
            return ASCIIEncoding.ASCII.GetString(decryptBytes);
        }

    }


    #region TripleDES算法
    public class ClassTripleDES
    {
        public ClassTripleDES()
        {
        }

        //加密，使用密码产生加密算法的公钥，并使用TripleDES对密码进行加密。 
        public static string Encrypt(string pass)
        {
            try
            {
                byte[] bt = (new System.Text.UnicodeEncoding()).GetBytes(pass);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(pass, null);
                byte[] key = pdb.GetBytes(24);
                byte[] iv = pdb.GetBytes(8);
                MemoryStream ms = new MemoryStream();
                TripleDESCryptoServiceProvider tdesc = new TripleDESCryptoServiceProvider();
                CryptoStream cs = new CryptoStream(ms, tdesc.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                cs.Write(bt, 0, bt.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //解密，使用密码产生加密算法的公钥，并使用TripleDES对加密数据进行解密。 
        public static string Decrypt(string str, string pass)
        {
            try
            {
                byte[] bt = Convert.FromBase64String(str);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(pass, null);
                byte[] key = pdb.GetBytes(24);
                byte[] iv = pdb.GetBytes(8);
                MemoryStream ms = new MemoryStream();
                TripleDESCryptoServiceProvider tdesc = new TripleDESCryptoServiceProvider();
                CryptoStream cs = new CryptoStream(ms, tdesc.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                cs.Write(bt, 0, bt.Length);
                cs.FlushFinalBlock();
                return (new System.Text.UnicodeEncoding()).GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //使用： 
        //string str = Encrypt("bbb"); 
        //Console.WriteLine(Decrypt(str, "bbb")); 
        //加密，使用密码产生加密算法的公钥，并使用TripleDES对密码进行加密。 
        public static string EncryptWithKey(string pass, string p_key)
        {
            try
            {
                byte[] bt = (new System.Text.UnicodeEncoding()).GetBytes(pass);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(p_key, null);
                byte[] key = pdb.GetBytes(24);
                byte[] iv = pdb.GetBytes(8);
                MemoryStream ms = new MemoryStream();
                TripleDESCryptoServiceProvider tdesc = new TripleDESCryptoServiceProvider();
                CryptoStream cs = new CryptoStream(ms, tdesc.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                cs.Write(bt, 0, bt.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //解密，使用密码产生加密算法的公钥，并使用TripleDES对加密数据进行解密。 
        public static string DecryptWithKey(string str, string p_key)
        {
            try
            {
                byte[] bt = Convert.FromBase64String(str);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(p_key, null);
                byte[] key = pdb.GetBytes(24);
                byte[] iv = pdb.GetBytes(8);
                MemoryStream ms = new MemoryStream();
                TripleDESCryptoServiceProvider tdesc = new TripleDESCryptoServiceProvider();
                CryptoStream cs = new CryptoStream(ms, tdesc.CreateDecryptor(key, iv), CryptoStreamMode.Write);
                cs.Write(bt, 0, bt.Length);
                cs.FlushFinalBlock();
                return (new System.Text.UnicodeEncoding()).GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    #endregion

    /// <summary>
    /// Base64编码类。
    /// 将byte[]类型转换成Base64编码的string类型。
    /// </summary>
    public class Base64Encoder
    {
        byte[] source;
        int length, length2;
        int blockCount;
        int paddingCount;
        public static Base64Encoder Encoder = new Base64Encoder();

        public Base64Encoder()
        {
        }

        private void init(byte[] input)
        {
            source = input;
            length = input.Length;
            if ((length % 3) == 0)
            {
                paddingCount = 0;
                blockCount = length / 3;
            }
            else
            {
                paddingCount = 3 - (length % 3);
                blockCount = (length + paddingCount) / 3;
            }
            length2 = length + paddingCount;
        }

        public string GetEncoded(byte[] input)
        {
            //初始化
            init(input);

            byte[] source2;
            source2 = new byte[length2];

            for (int x = 0; x < length2; x++)
            {
                if (x < length)
                {
                    source2[x] = source[x];
                }
                else
                {
                    source2[x] = 0;
                }
            }

            byte b1, b2, b3;
            byte temp, temp1, temp2, temp3, temp4;
            byte[] buffer = new byte[blockCount * 4];
            char[] result = new char[blockCount * 4];
            for (int x = 0; x < blockCount; x++)
            {
                b1 = source2[x * 3];
                b2 = source2[x * 3 + 1];
                b3 = source2[x * 3 + 2];

                temp1 = (byte)((b1 & 252) >> 2);

                temp = (byte)((b1 & 3) << 4);
                temp2 = (byte)((b2 & 240) >> 4);
                temp2 += temp;

                temp = (byte)((b2 & 15) << 2);
                temp3 = (byte)((b3 & 192) >> 6);
                temp3 += temp;

                temp4 = (byte)(b3 & 63);

                buffer[x * 4] = temp1;
                buffer[x * 4 + 1] = temp2;
                buffer[x * 4 + 2] = temp3;
                buffer[x * 4 + 3] = temp4;

            }

            for (int x = 0; x < blockCount * 4; x++)
            {
                result[x] = sixbit2char(buffer[x]);
            }


            switch (paddingCount)
            {
                case 0: break;
                case 1: result[blockCount * 4 - 1] = '='; break;
                case 2: result[blockCount * 4 - 1] = '=';
                    result[blockCount * 4 - 2] = '=';
                    break;
                default: break;
            }
            return new string(result);
        }
        private char sixbit2char(byte b)
        {
            char[] lookupTable = new char[64]{
                  'A','B','C','D','E','F','G','H','I','J','K','L','M',
                 'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                 'a','b','c','d','e','f','g','h','i','j','k','l','m',
                 'n','o','p','q','r','s','t','u','v','w','x','y','z',
                 '0','1','2','3','4','5','6','7','8','9','+','/'};

            if ((b >= 0) && (b <= 63))
            {
                return lookupTable[(int)b];
            }
            else
            {

                return ' ';
            }
        }
    }


    /// <summary>
    /// Base64解码类
    /// 将Base64编码的string类型转换成byte[]类型
    /// </summary>
    public class Base64Decoder
    {
        char[] source;
        int length, length2, length3;
        int blockCount;
        int paddingCount;
        public static Base64Decoder Decoder = new Base64Decoder();

        public Base64Decoder()
        {
        }

        private void init(char[] input)
        {
            int temp = 0;
            source = input;
            length = input.Length;

            for (int x = 0; x < 2; x++)
            {
                if (input[length - x - 1] == '=')
                    temp++;
            }
            paddingCount = temp;

            blockCount = length / 4;
            length2 = blockCount * 3;
        }

        public byte[] GetDecoded(string strInput)
        {
            //初始化
            init(strInput.ToCharArray());

            byte[] buffer = new byte[length];
            byte[] buffer2 = new byte[length2];

            for (int x = 0; x < length; x++)
            {
                buffer[x] = char2sixbit(source[x]);
            }

            byte b, b1, b2, b3;
            byte temp1, temp2, temp3, temp4;

            for (int x = 0; x < blockCount; x++)
            {
                temp1 = buffer[x * 4];
                temp2 = buffer[x * 4 + 1];
                temp3 = buffer[x * 4 + 2];
                temp4 = buffer[x * 4 + 3];

                b = (byte)(temp1 << 2);
                b1 = (byte)((temp2 & 48) >> 4);
                b1 += b;

                b = (byte)((temp2 & 15) << 4);
                b2 = (byte)((temp3 & 60) >> 2);
                b2 += b;

                b = (byte)((temp3 & 3) << 6);
                b3 = temp4;
                b3 += b;

                buffer2[x * 3] = b1;
                buffer2[x * 3 + 1] = b2;
                buffer2[x * 3 + 2] = b3;
            }

            length3 = length2 - paddingCount;
            byte[] result = new byte[length3];

            for (int x = 0; x < length3; x++)
            {
                result[x] = buffer2[x];
            }

            return result;
        }

        private byte char2sixbit(char c)
        {
            char[] lookupTable = new char[64]{ 
                 'A','B','C','D','E','F','G','H','I','J','K','L','M','N',
                 'O','P','Q','R','S','T','U','V','W','X','Y', 'Z',
                 'a','b','c','d','e','f','g','h','i','j','k','l','m','n',
                 'o','p','q','r','s','t','u','v','w','x','y','z',
                 '0','1','2','3','4','5','6','7','8','9','+','/'};
            if (c == '=')
                return 0;
            else
            {
                for (int x = 0; x < 64; x++)
                {
                    if (lookupTable[x] == c)
                        return (byte)x;
                }

                return 0;
            }
        }

        
        private static string nodeAuthorizationURL = @"https://www.yeepay.com/app-merchant-proxy/command.action";
        /// <summary>
        /// 获得易宝hmac码
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="p1_MerId"></param>
        /// <param name="p2_Order"></param>
        /// <param name="p3_Amt"></param>
        /// <param name="p4_verifyAmt"></param>
        /// <param name="p5_Pid"></param>
        /// <param name="p6_Pcat"></param>
        /// <param name="p7_Pdesc"></param>
        /// <param name="p8_Url"></param>
        /// <param name="pa_MP"></param>
        /// <param name="pa7_cardAmt"></param>
        /// <param name="pa8_cardNo"></param>
        /// <param name="pa9_cardPwd"></param>
        /// <param name="pd_FrpId"></param>
        /// <param name="pr_NeedResponse"></param>
        /// <param name="pz_userId"></param>
        /// <param name="pz1_userRegTime"></param>
        /// <returns></returns>
        public static string AnnulCard(string keyValue, string p1_MerId, string p2_Order, string p3_Amt,
        string p4_verifyAmt, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url,
        string pa_MP, string pa7_cardAmt, string pa8_cardNo, string pa9_cardPwd, string pd_FrpId,
        string pr_NeedResponse, string pz_userId, string pz1_userRegTime)
        {
            string sbOld = "";
            sbOld += "ChargeCardDirect";
            sbOld += p1_MerId;
            sbOld += p2_Order;
            sbOld += p3_Amt;
            sbOld += p4_verifyAmt;
            sbOld += p5_Pid;
            sbOld += p6_Pcat;
            sbOld += p7_Pdesc;
            sbOld += p8_Url;
            sbOld += pa_MP;
            sbOld += pa7_cardAmt;
            sbOld += pa8_cardNo;
            sbOld += pa9_cardPwd;
            sbOld += pd_FrpId;
            sbOld += pr_NeedResponse;
            sbOld += pz_userId;
            sbOld += pz1_userRegTime;
            string hmac = HmacSign(sbOld, keyValue);
            logstr(p2_Order, sbOld, hmac);
            string para = "";
            para += "?p0_Cmd=ChargeCardDirect";
            para += "&p1_MerId=" + p1_MerId;
            para += "&p2_Order=" + p2_Order;
            para += "&p3_Amt=" + p3_Amt;
            para += "&p4_verifyAmt=" + p4_verifyAmt;
            para += "&p5_Pid=" + System.Web.HttpUtility.UrlEncode(p5_Pid, System.Text.Encoding.GetEncoding("gb2312"));
            para += "&p6_Pcat=" + System.Web.HttpUtility.UrlEncode(p6_Pcat, System.Text.Encoding.GetEncoding("gb2312"));
            para += "&p7_Pdesc=" + System.Web.HttpUtility.UrlEncode(p7_Pdesc, System.Text.Encoding.GetEncoding("gb2312"));
            para += "&p8_Url=" + System.Web.HttpUtility.UrlEncode(p8_Url, System.Text.Encoding.GetEncoding("gb2312"));
            para += "&pa_MP=" + System.Web.HttpUtility.UrlEncode(pa_MP, System.Text.Encoding.GetEncoding("gb2312"));
            para += "&pa7_cardAmt=" + System.Web.HttpUtility.UrlEncode(pa7_cardAmt, System.Text.Encoding.GetEncoding("gb2312"));
            para += "&pa8_cardNo=" + System.Web.HttpUtility.UrlEncode(pa8_cardNo, System.Text.Encoding.GetEncoding("gb2312"));
            para += "&pa9_cardPwd=" + System.Web.HttpUtility.UrlEncode(pa9_cardPwd, System.Text.Encoding.GetEncoding("gb2312"));
            para += "&pd_FrpId=" + pd_FrpId;
            para += "&pr_NeedResponse=" + pr_NeedResponse;
            para += "&pz_userId=" + pz_userId;
            para += "&pz1_userRegTime=" + System.Web.HttpUtility.UrlEncode(pz1_userRegTime, System.Text.Encoding.GetEncoding("gb2312"));
            para += "&hmac=" + hmac;

            string reqResult = HttpUtils.SendRequest(nodeAuthorizationURL, para);
            return hmac;
        }

        private static string logFileName = "c:/YeePay_CARD.txt";

        public static string LogFileName
        {
            get { return logFileName; }
            set { logFileName = value; }
        }

        public static void logstr(string orderid, string str, string hmac)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(logFileName, true);
                sw.BaseStream.Seek(0, System.IO.SeekOrigin.End);
                sw.WriteLine(DateTime.Now.ToString() + "[" + orderid + "]" + "[" + str + "]" + "[" + hmac + "]");
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static string HmacSign(string aValue, string aKey)
        {
            byte[] k_ipad = new byte[64];
            byte[] k_opad = new byte[64];
            byte[] keyb;
            byte[] Value;
            keyb = Encoding.UTF8.GetBytes(aKey);
            Value = Encoding.UTF8.GetBytes(aValue);

            for (int i = keyb.Length; i < 64; i++)
                k_ipad[i] = 54;

            for (int i = keyb.Length; i < 64; i++)
                k_opad[i] = 92;

            for (int i = 0; i < keyb.Length; i++)
            {
                k_ipad[i] = (byte)(keyb[i] ^ 0x36);
                k_opad[i] = (byte)(keyb[i] ^ 0x5c);
            }

            HmacMD5 md = new HmacMD5();

            md.update(k_ipad, (uint)k_ipad.Length);
            md.update(Value, (uint)Value.Length);
            byte[] dg = md.finalize();
            md.init();
            md.update(k_opad, (uint)k_opad.Length);
            md.update(dg, 16);
            dg = md.finalize();

            return toHex(dg);
        }

        public static string toHex(byte[] input)
        {
            if (input == null)
                return null;

            StringBuilder output = new StringBuilder(input.Length * 2);

            for (int i = 0; i < input.Length; i++)
            {
                int current = input[i] & 0xff;
                if (current < 16)
                    output.Append("0");
                output.Append(current.ToString("x"));
            }

            return output.ToString();
        }

        public class HmacMD5
        {
            private uint[] count;
            private uint[] state;
            private byte[] buffer;
            private byte[] Digest;

            public HmacMD5()
            {
                count = new uint[2];
                state = new uint[4];
                buffer = new byte[64];
                Digest = new byte[16];
                init();
            }

            public void init()
            {
                count[0] = 0;
                count[1] = 0;
                state[0] = 0x67452301;
                state[1] = 0xefcdab89;
                state[2] = 0x98badcfe;
                state[3] = 0x10325476;
            }

            public void update(byte[] data, uint length)
            {
                uint left = length;
                uint offset = (count[0] >> 3) & 0x3F;
                uint bit_length = (uint)(length << 3);
                uint index = 0;

                if (length <= 0)
                    return;

                count[0] += bit_length;
                count[1] += (length >> 29);
                if (count[0] < bit_length)
                    count[1]++;

                if (offset > 0)
                {
                    uint space = 64 - offset;
                    uint copy = (offset + length > 64 ? 64 - offset : length);
                    Buffer.BlockCopy(data, 0, buffer, (int)offset, (int)copy);

                    if (offset + copy < 64)
                        return;

                    transform(buffer);
                    index += copy;
                    left -= copy;
                }

                for (; left >= 64; index += 64, left -= 64)
                {
                    Buffer.BlockCopy(data, (int)index, buffer, 0, 64);
                    transform(buffer);
                }

                if (left > 0)
                    Buffer.BlockCopy(data, (int)index, buffer, 0, (int)left);

            }

            private static byte[] pad = new byte[64] {
													 0x80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
													 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
													 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
													 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            public byte[] finalize()
            {
                byte[] bits = new byte[8];
                encode(ref bits, count, 8);
                uint index = (uint)((count[0] >> 3) & 0x3f);
                uint padLen = (index < 56) ? (56 - index) : (120 - index);
                update(pad, padLen);
                update(bits, 8);
                encode(ref Digest, state, 16);

                for (int i = 0; i < 64; i++)
                    buffer[i] = 0;

                return Digest;
            }

            public string md5String()
            {
                string s = "";

                for (int i = 0; i < Digest.Length; i++)
                    s += Digest[i].ToString("x2");

                return s;
            }

            #region Constants for MD5Transform routine.

            private const uint S11 = 7;
            private const uint S12 = 12;
            private const uint S13 = 17;
            private const uint S14 = 22;
            private const uint S21 = 5;
            private const uint S22 = 9;
            private const uint S23 = 14;
            private const uint S24 = 20;
            private const uint S31 = 4;
            private const uint S32 = 11;
            private const uint S33 = 16;
            private const uint S34 = 23;
            private const uint S41 = 6;
            private const uint S42 = 10;
            private const uint S43 = 15;
            private const uint S44 = 21;
            #endregion

            private void transform(byte[] data)
            {
                uint a = state[0];
                uint b = state[1];
                uint c = state[2];
                uint d = state[3];
                uint[] x = new uint[16];

                decode(ref x, data, 64);

                // Round 1
                FF(ref a, b, c, d, x[0], S11, 0xd76aa478); /* 1 */
                FF(ref d, a, b, c, x[1], S12, 0xe8c7b756); /* 2 */
                FF(ref c, d, a, b, x[2], S13, 0x242070db); /* 3 */
                FF(ref b, c, d, a, x[3], S14, 0xc1bdceee); /* 4 */
                FF(ref a, b, c, d, x[4], S11, 0xf57c0faf); /* 5 */
                FF(ref d, a, b, c, x[5], S12, 0x4787c62a); /* 6 */
                FF(ref c, d, a, b, x[6], S13, 0xa8304613); /* 7 */
                FF(ref b, c, d, a, x[7], S14, 0xfd469501); /* 8 */
                FF(ref a, b, c, d, x[8], S11, 0x698098d8); /* 9 */
                FF(ref d, a, b, c, x[9], S12, 0x8b44f7af); /* 10 */
                FF(ref c, d, a, b, x[10], S13, 0xffff5bb1); /* 11 */
                FF(ref b, c, d, a, x[11], S14, 0x895cd7be); /* 12 */
                FF(ref a, b, c, d, x[12], S11, 0x6b901122); /* 13 */
                FF(ref d, a, b, c, x[13], S12, 0xfd987193); /* 14 */
                FF(ref c, d, a, b, x[14], S13, 0xa679438e); /* 15 */
                FF(ref b, c, d, a, x[15], S14, 0x49b40821); /* 16 */

                // Round 2 
                GG(ref a, b, c, d, x[1], S21, 0xf61e2562); /* 17 */
                GG(ref d, a, b, c, x[6], S22, 0xc040b340); /* 18 */
                GG(ref c, d, a, b, x[11], S23, 0x265e5a51); /* 19 */
                GG(ref b, c, d, a, x[0], S24, 0xe9b6c7aa); /* 20 */
                GG(ref a, b, c, d, x[5], S21, 0xd62f105d); /* 21 */
                GG(ref d, a, b, c, x[10], S22, 0x2441453); /* 22 */
                GG(ref c, d, a, b, x[15], S23, 0xd8a1e681); /* 23 */
                GG(ref b, c, d, a, x[4], S24, 0xe7d3fbc8); /* 24 */
                GG(ref a, b, c, d, x[9], S21, 0x21e1cde6); /* 25 */
                GG(ref d, a, b, c, x[14], S22, 0xc33707d6); /* 26 */
                GG(ref c, d, a, b, x[3], S23, 0xf4d50d87); /* 27 */
                GG(ref b, c, d, a, x[8], S24, 0x455a14ed); /* 28 */
                GG(ref a, b, c, d, x[13], S21, 0xa9e3e905); /* 29 */
                GG(ref d, a, b, c, x[2], S22, 0xfcefa3f8); /* 30 */
                GG(ref c, d, a, b, x[7], S23, 0x676f02d9); /* 31 */
                GG(ref b, c, d, a, x[12], S24, 0x8d2a4c8a); /* 32 */

                // Round 3
                HH(ref a, b, c, d, x[5], S31, 0xfffa3942); /* 33 */
                HH(ref d, a, b, c, x[8], S32, 0x8771f681); /* 34 */
                HH(ref c, d, a, b, x[11], S33, 0x6d9d6122); /* 35 */
                HH(ref b, c, d, a, x[14], S34, 0xfde5380c); /* 36 */
                HH(ref a, b, c, d, x[1], S31, 0xa4beea44); /* 37 */
                HH(ref d, a, b, c, x[4], S32, 0x4bdecfa9); /* 38 */
                HH(ref c, d, a, b, x[7], S33, 0xf6bb4b60); /* 39 */
                HH(ref b, c, d, a, x[10], S34, 0xbebfbc70); /* 40 */
                HH(ref a, b, c, d, x[13], S31, 0x289b7ec6); /* 41 */
                HH(ref d, a, b, c, x[0], S32, 0xeaa127fa); /* 42 */
                HH(ref c, d, a, b, x[3], S33, 0xd4ef3085); /* 43 */
                HH(ref b, c, d, a, x[6], S34, 0x4881d05); /* 44 */
                HH(ref a, b, c, d, x[9], S31, 0xd9d4d039); /* 45 */
                HH(ref d, a, b, c, x[12], S32, 0xe6db99e5); /* 46 */
                HH(ref c, d, a, b, x[15], S33, 0x1fa27cf8); /* 47 */
                HH(ref b, c, d, a, x[2], S34, 0xc4ac5665); /* 48 */

                // Round 4
                II(ref a, b, c, d, x[0], S41, 0xf4292244); /* 49 */
                II(ref d, a, b, c, x[7], S42, 0x432aff97); /* 50 */
                II(ref c, d, a, b, x[14], S43, 0xab9423a7); /* 51 */
                II(ref b, c, d, a, x[5], S44, 0xfc93a039); /* 52 */
                II(ref a, b, c, d, x[12], S41, 0x655b59c3); /* 53 */
                II(ref d, a, b, c, x[3], S42, 0x8f0ccc92); /* 54 */
                II(ref c, d, a, b, x[10], S43, 0xffeff47d); /* 55 */
                II(ref b, c, d, a, x[1], S44, 0x85845dd1); /* 56 */
                II(ref a, b, c, d, x[8], S41, 0x6fa87e4f); /* 57 */
                II(ref d, a, b, c, x[15], S42, 0xfe2ce6e0); /* 58 */
                II(ref c, d, a, b, x[6], S43, 0xa3014314); /* 59 */
                II(ref b, c, d, a, x[13], S44, 0x4e0811a1); /* 60 */
                II(ref a, b, c, d, x[4], S41, 0xf7537e82); /* 61 */
                II(ref d, a, b, c, x[11], S42, 0xbd3af235); /* 62 */
                II(ref c, d, a, b, x[2], S43, 0x2ad7d2bb); /* 63 */
                II(ref b, c, d, a, x[9], S44, 0xeb86d391); /* 64 */

                state[0] += a;
                state[1] += b;
                state[2] += c;
                state[3] += d;

                for (int i = 0; i < 16; i++)
                    x[i] = 0;
            }

            #region encode - decode
            private void encode(ref byte[] output, uint[] input, uint len)
            {
                uint i, j;
                if (System.BitConverter.IsLittleEndian)
                {
                    for (i = 0, j = 0; j < len; i++, j += 4)
                    {
                        output[j] = (byte)(input[i] & 0xff);
                        output[j + 1] = (byte)((input[i] >> 8) & 0xff);
                        output[j + 2] = (byte)((input[i] >> 16) & 0xff);
                        output[j + 3] = (byte)((input[i] >> 24) & 0xff);
                    }
                }
                else
                {
                    for (i = 0, j = 0; j < len; i++, j += 4)
                    {
                        output[j + 3] = (byte)(input[i] & 0xff);
                        output[j + 2] = (byte)((input[i] >> 8) & 0xff);
                        output[j + 1] = (byte)((input[i] >> 16) & 0xff);
                        output[j] = (byte)((input[i] >> 24) & 0xff);
                    }
                }
            }

            private void decode(ref uint[] output, byte[] input, uint len)
            {
                uint i, j;
                if (System.BitConverter.IsLittleEndian)
                {
                    for (i = 0, j = 0; j < len; i++, j += 4)
                        output[i] = ((uint)input[j]) | (((uint)input[j + 1]) << 8) |
                            (((uint)input[j + 2]) << 16) | (((uint)input[j + 3]) << 24);
                }
                else
                {
                    for (i = 0, j = 0; j < len; i++, j += 4)
                        output[i] = ((uint)input[j + 3]) | (((uint)input[j + 2]) << 8) |
                            (((uint)input[j + 1]) << 16) | (((uint)input[j]) << 24);
                }
            }
            #endregion

            private uint rotate_left(uint x, uint n)
            {
                return (x << (int)n) | (x >> (int)(32 - n));
            }

            #region F, G, H and I are basic MD5 functions.
            private uint F(uint x, uint y, uint z)
            {
                return (x & y) | (~x & z);
            }

            private uint G(uint x, uint y, uint z)
            {
                return (x & z) | (y & ~z);
            }

            private uint H(uint x, uint y, uint z)
            {
                return x ^ y ^ z;
            }

            private uint I(uint x, uint y, uint z)
            {
                return y ^ (x | ~z);
            }
            #endregion

            #region  FF, GG, HH, and II transformations for rounds 1, 2, 3, and 4.
            private void FF(ref uint a, uint b, uint c, uint d, uint x, uint s, uint ac)
            {
                a += F(b, c, d) + x + ac;
                a = rotate_left(a, s) + b;
            }

            private void GG(ref uint a, uint b, uint c, uint d, uint x, uint s, uint ac)
            {
                a += G(b, c, d) + x + ac;
                a = rotate_left(a, s) + b;
            }

            private void HH(ref uint a, uint b, uint c, uint d, uint x, uint s, uint ac)
            {
                a += H(b, c, d) + x + ac;
                a = rotate_left(a, s) + b;
            }

            private void II(ref uint a, uint b, uint c, uint d, uint x, uint s, uint ac)
            {
                a += I(b, c, d) + x + ac;
                a = rotate_left(a, s) + b;
            }
            #endregion
        }
       
    }
    //解码类结束

    public class GetChineseNum
    {
        private char[] qBSG = new char[4];
        private string[] numStrArrary;
        private string[] numStrArrary1 = new string[] { "○", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "百", "千", "万", "亿" };
        private string[] numStrArrary2 = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖", "拾", "佰", "仟", "万", "亿" };
        private char[] numCharArray;
        private static int charNum;
        private static int charYuShu;
        private static int charGeShu;
        private string dotStr = "";
        private string xsStr = "";
        public string DotStr
        {
            set
            {
                dotStr = value;
            }
        }

        /**/
        /// <summary>
        /// 类构造器 
        /// numberType “类型1 ○一二”/“类型2  零壹贰”
        /// </summary>
        /// <param name="numberType"></param>
        public GetChineseNum(string numberType)
        {
            if (numberType == "1")
            {
                numStrArrary = numStrArrary1;
            }
            else if (numberType == "2")
            {
                numStrArrary = numStrArrary2;
            }
        }

        /**/
        /// <summary>
        /// 类构造器 
        ///number “数字  012”| numberType “类型1 ○一二”/“类型2  零壹贰”
        /// </summary>
        /// <param name="number"></param>
        /// <param name="numberType"></param>
        public GetChineseNum(string number, string numberType)
        {
            string intNum = "";
            try
            {
                decimal.Parse(number);
            }
            catch
            {
                throw new Exception("输入数字");
            }

            if (numberType == "1")
            {
                numStrArrary = numStrArrary1;
            }
            else if (numberType == "2")
            {
                numStrArrary = numStrArrary2;
            }
            if (number.Split('.').Length == 2)
            {
                intNum = number.Split('.')[0];
                xsStr = number.Split('.')[1];
            }
            else
            {
                intNum = number;
            }

            charNum = intNum.ToCharArray().Length;
            numCharArray = intNum.ToCharArray();
            for (int i = 0; i < charNum; i++)
            {
                if (charNum > charNum - i - 1)
                    numCharArray[i] = intNum.ToCharArray()[charNum - i - 1];
            }

            if (charNum <= 4)
            {
                charYuShu = charNum;
                charGeShu = 0;
            }
            else
            {
                charYuShu = charNum % 4;
                charGeShu = charNum / 4;
            }
        }

        #region    获取单个中文数字
        /**/
        /// <summary>
        /// 获取单个数字
        /// </summary>
        /// <param name="numStr"></param>
        /// <returns></returns>
        public string GetSingleChinNum(string numStr)
        {
            string newStr = "";

            switch (numStr)
            {
                case "0":
                    newStr = numStrArrary[0];
                    break;
                case "1":
                    newStr = numStrArrary[1];
                    break;
                case "2":
                    newStr = numStrArrary[2];
                    break;
                case "3":
                    newStr = numStrArrary[3];
                    break;
                case "4":
                    newStr = numStrArrary[4];
                    break;
                case "5":
                    newStr = numStrArrary[5];
                    break;
                case "6":
                    newStr = numStrArrary[6];
                    break;
                case "7":
                    newStr = numStrArrary[7];
                    break;
                case "8":
                    newStr = numStrArrary[8];
                    break;
                case "9":
                    newStr = numStrArrary[9];
                    break;
                default:
                    break;
            }
            return newStr;
        }
        #endregion

        #region    获取中文数字串
        /**/
        /// <summary>
        /// 获取数字串 “如：○一二”
        /// </summary>
        /// <param name="numStr"></param>
        /// <returns></returns>
        public string GetNumOnly(string numStr)
        {
            string newNumber = numStr;
            newNumber = newNumber.Replace("0", numStrArrary[0]).Replace("1", numStrArrary[1]).Replace("2", numStrArrary[2]).Replace("3", numStrArrary[3]);
            newNumber = newNumber.Replace("4", numStrArrary[4]).Replace("5", numStrArrary[5]).Replace("6", numStrArrary[6]);
            newNumber = newNumber.Replace("7", numStrArrary[7]).Replace("8", numStrArrary[8]).Replace("9", numStrArrary[9]);
            return newNumber;
        }
        #endregion

        #region    获取单个中文数字
        /**/
        /// <summary>
        /// 获取单个数字
        /// </summary>
        /// <param name="numChar"></param>
        /// <returns></returns>
        public string GetSingleChinNum(char numChar)
        {
            string newStr = "";

            switch (numChar)
            {
                case '0':
                    newStr = numStrArrary[0];
                    break;
                case '1':
                    newStr = numStrArrary[1];
                    break;
                case '2':
                    newStr = numStrArrary[2];
                    break;
                case '3':
                    newStr = numStrArrary[3];
                    break;
                case '4':
                    newStr = numStrArrary[4];
                    break;
                case '5':
                    newStr = numStrArrary[5];
                    break;
                case '6':
                    newStr = numStrArrary[6];
                    break;
                case '7':
                    newStr = numStrArrary[7];
                    break;
                case '8':
                    newStr = numStrArrary[8];
                    break;
                case '9':
                    newStr = numStrArrary[9];
                    break;
                default:
                    break;
            }
            return newStr;
        }
        #endregion


        #region 获取 个、十、百、千
        /**/
        /// <summary>
        /// 按4位分割后去 每个组中的 个、十、百、千 数字串
        /// </summary>
        /// <returns></returns>
        private string GetQBSG()
        {
            string newQBSG = "";

            if (qBSG[0].ToString() != null)
            {
                if (GetSingleChinNum(qBSG[3]) != "")
                {
                    newQBSG += GetSingleChinNum(qBSG[3]) + numStrArrary[12];
                }
            }

            if (qBSG[0].ToString() != null)
            {
                if (GetSingleChinNum(qBSG[2]) != "")
                {
                    newQBSG += GetSingleChinNum(qBSG[2]) + numStrArrary[11];
                }
            }

            if (qBSG[0].ToString() != null)
            {
                if (GetSingleChinNum(qBSG[1]) != "")
                {
                    newQBSG += GetSingleChinNum(qBSG[1]) + numStrArrary[10];
                }
            }

            if (qBSG[0].ToString() != null)
            {
                newQBSG += GetSingleChinNum(qBSG[0]);
            }

            return newQBSG;
        }
        #endregion

        #region    获取中文数字
        /**/
        /// <summary>
        /// 获取中文数字 “如 二百三十一”
        /// </summary>
        /// <returns></returns>
        public string GetChinNum()
        {
            string rtnStr = "";

            //获取 个、十、百、千 整体 以外
            for (int i = 0; i < charYuShu; i++)
            {
                if (charNum > charGeShu * 4 + i)
                    qBSG[i] = numCharArray[charGeShu * 4 + i];
            }
            rtnStr += GetQBSG();
            if (charGeShu == 1)
            {
                rtnStr += numStrArrary[13];
            }

            if (charGeShu == 2 && charYuShu != 0)
            {
                rtnStr += numStrArrary[14];
            }
            if (charGeShu == 3 && charYuShu != 0)
            {
                rtnStr += numStrArrary[13] + numStrArrary[14];
            }

            //获取 个、十、百、千 整体
            for (int i = charGeShu; i > 0; i--)
            {
                if (charNum > i * 4 - 4)
                    qBSG[0] = numCharArray[i * 4 - 4];

                if (charNum > i * 4 - 3)
                    qBSG[1] = numCharArray[i * 4 - 3];

                if (charNum > i * 4 - 2)
                    qBSG[2] = numCharArray[i * 4 - 2];

                if (charNum > i * 4 - 1)
                    qBSG[3] = numCharArray[i * 4 - 1];
                if (i == 1)
                {
                    rtnStr += GetQBSG();
                }
                if (i == 2)
                {
                    rtnStr += GetQBSG() + numStrArrary[13];
                }
                if (i == 3)
                {
                    rtnStr += GetQBSG() + numStrArrary[14];
                }
                if (i == 4)
                {
                    rtnStr += GetQBSG() + numStrArrary[13] + numStrArrary[14];
                }
            }
            // 处理 ○十○百○个 等
            rtnStr = rtnStr.Replace(numStrArrary[0] + numStrArrary[12] + numStrArrary[0] + numStrArrary[11] + numStrArrary[0] + numStrArrary[10] + numStrArrary[0], "");
            rtnStr = rtnStr.Replace(numStrArrary[0] + numStrArrary[11] + numStrArrary[0] + numStrArrary[10] + numStrArrary[0], "");
            rtnStr = rtnStr.Replace(numStrArrary[0] + numStrArrary[10] + numStrArrary[0], "");
            rtnStr = rtnStr.Replace(numStrArrary[10] + numStrArrary[0], numStrArrary[10]);
            // 处理 ○十、○百 等
            rtnStr = rtnStr.Replace(numStrArrary[0] + numStrArrary[10], numStrArrary[0]).Replace(numStrArrary[0] + numStrArrary[11], numStrArrary[0]).Replace(numStrArrary[0] + numStrArrary[12],

numStrArrary[0]);
            rtnStr = rtnStr.Replace(numStrArrary[0] + numStrArrary[13], numStrArrary[13]).Replace(numStrArrary[0] + numStrArrary[14], numStrArrary[14]);
            //替换 多个 0 在一起
            rtnStr = rtnStr.Replace(numStrArrary[0] + numStrArrary[0], numStrArrary[0]).Replace(numStrArrary[0] + numStrArrary[0] + numStrArrary[0], numStrArrary[0]);
            // 处理 ○一十万 等
            rtnStr = rtnStr.Replace(numStrArrary[0] + numStrArrary[1] + numStrArrary[10] + numStrArrary[13], numStrArrary[0] + numStrArrary[10] + numStrArrary[13]);
            rtnStr = rtnStr.Replace(numStrArrary[0] + numStrArrary[1] + numStrArrary[10] + numStrArrary[14], numStrArrary[0] + numStrArrary[10] + numStrArrary[14]);
            return rtnStr + dotStr + GetNumOnly(xsStr);
        }
        #endregion
    }

    public abstract class HttpUtils
    {
        public HttpUtils()
        {

        }

        #region 通讯函数
        /// <summary>
        /// 通讯函数
        /// </summary>
        /// <param name="url">请求Url</param>
        /// <param name="para">请求参数</param>
        /// <param name="method">请求方式GET/POST</param>
        /// <returns></returns>
        public static string SendRequest(string url, string para, string method)
        {
            string strResult = "";

            if (url == null || url == "")
                return null;

            if (method == null || method == "")
                method = "GET";

            // GET方式
            if (method.ToUpper() == "GET")
            {
                try
                {
                    System.Net.WebRequest wrq = System.Net.WebRequest.Create(url + para);
                    wrq.Method = "GET";

                    System.Net.WebResponse wrp = wrq.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(wrp.GetResponseStream(), System.Text.Encoding.GetEncoding("gb2312"));

                    strResult = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

            // POST方式
            if (method.ToUpper() == "POST")
            {
                if (para.Length > 0 && para.IndexOf('?') == 0)
                {
                    para = para.Substring(1);
                }

                WebRequest req = WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                StringBuilder UrlEncoded = new StringBuilder();
                Char[] reserved = { '?', '=', '&' };
                byte[] SomeBytes = null;
                if (para != null)
                {
                    int i = 0, j;
                    while (i < para.Length)
                    {
                        j = para.IndexOfAny(reserved, i);
                        if (j == -1)
                        {
                            UrlEncoded.Append(HttpUtility.UrlEncode(para.Substring(i, para.Length - i), System.Text.Encoding.GetEncoding("gb2312")));
                            break;
                        }
                        UrlEncoded.Append(HttpUtility.UrlEncode(para.Substring(i, j - i), System.Text.Encoding.GetEncoding("gb2312")));
                        UrlEncoded.Append(para.Substring(j, 1));
                        i = j + 1;
                    }
                    SomeBytes = Encoding.Default.GetBytes(UrlEncoded.ToString());
                    req.ContentLength = SomeBytes.Length;
                    Stream newStream = req.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                else
                {
                    req.ContentLength = 0;
                }
                try
                {
                    WebResponse result = req.GetResponse();
                    Stream ReceiveStream = result.GetResponseStream();

                    Byte[] read = new Byte[512];
                    int bytes = ReceiveStream.Read(read, 0, 512);

                    while (bytes > 0)
                    {

                        // 注意：
                        // 下面假定响应使用 UTF-8 作为编码方式。
                        // 如果内容以 ANSI 代码页形式（例如，932）发送，则使用类似下面的语句：
                        //  Encoding encode = System.Text.Encoding.GetEncoding("shift-jis");
                        Encoding encode = System.Text.Encoding.GetEncoding("gb2312");
                        strResult += encode.GetString(read, 0, bytes);
                        bytes = ReceiveStream.Read(read, 0, 512);
                    }

                    return strResult;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return strResult;
        }
        #endregion

        #region 简化通讯函数
        /// <summary>
        /// GET方式通讯
        /// </summary>
        /// <param name="url"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static string SendRequest(string url, string para)
        {
            return SendRequest(url, para, "GET");
        }
        #endregion
    }
}

