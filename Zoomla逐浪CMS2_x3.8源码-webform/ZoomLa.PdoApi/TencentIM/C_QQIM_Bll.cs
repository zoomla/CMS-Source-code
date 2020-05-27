using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.PdoApi.TencentIM
{
    public class C_QQIM_Bll
    {
        //根据需要访问的按口,返回格式化后的API Url
        public string GetAPIUrl(string api)
        {
            string url = C_QQIM_Contasnt.baseurl + api + "?usersig=" + C_QQIM_Contasnt.appadminSign + "&identifier=" + C_QQIM_Contasnt.appadmin + "&sdkappid=" + C_QQIM_Contasnt.appid + "&contenttype=json";
            return url;
        }
        /// <summary>
        /// 查询用户在线状态
        /// </summary>
        public string QueryState(params string[] unames)
        {
            //["id1", "id2", "id2"]
            string api = GetAPIUrl("v4/openim/querystate");
            string uarr = "";
            foreach (string u in unames)
            {
                uarr += "\"" + u + "\",";
            }
            string json = "{\"To_Account\": [" + uarr.TrimEnd(',') + "]}";
            return APIHelper.GetWebResult(api, "POST", json);
        }
        /// <summary>
        /// 用户注册后,或进行聊天操作时,生成对应的用户名,并写入表中
        /// 导入新账户,导入的账户在后台管理是看不到的,但可登录与通过API查询到
        /// </summary>
        public string Account_Import(string uname, string nick, string face)
        {
            string json = "{\"Identifier\":\"" + HttpUtility.UrlEncode(uname) + "\",\"Nick\":\"" + HttpUtility.UrlEncode(nick) + "\",\"FaceUrl\":\"" + HttpUtility.UrlEncode(face) + "\"}";
            string api = GetAPIUrl("v4/im_open_login_svc/account_import");
            return APIHelper.GetWebResult(api, "POST", json);
        }
        //接口调用是否成功
        private bool Account_Import_IsOK(string result)
        {
            JObject jobj = JsonConvert.DeserializeObject<JObject>(result);
            if (jobj["ActionStatus"] != null && jobj["ActionStatus"].ToString().Equals("OK", StringComparison.CurrentCultureIgnoreCase)) { return true; }
            else { return false; }
        }
        //----------------------------------------------------
        private M_QQIM_User initMod = new M_QQIM_User();
        private string TbName = "ZL_API_QQIMUser", PK = "ID";
        public M_QQIM_User SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public M_QQIM_User SelModelByUid(int uid)
        {
            string where = "UserID=" + uid;
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public int Insert(M_QQIM_User model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_QQIM_User model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        //----------------------------------------------------
        /// <summary>
        /// 根据用户信息,返回有效的签名,再使用签名完成登录
        /// </summary>
        public M_QQIM_User GetSignByUser(M_UserInfo mu)
        {
            //为空时提交会死循环
            if (mu == null || mu.IsNull || string.IsNullOrEmpty(mu.UserName)) { throw new Exception("用户信息为空,无法生成IM签名"); }
            M_QQIM_User model = SelModelByUid(mu.UserID);
            if (model == null)
            {
                string identity = "zl" + function.GetRandomString(10);
                string nick = string.IsNullOrEmpty(mu.HoneyName) ? mu.UserName : mu.HoneyName;
                string face = string.IsNullOrEmpty(mu.UserFace) ? "" : SiteConfig.SiteInfo.SiteUrl + mu.UserFace;
                string result = Account_Import(identity, nick, face);
                if (Account_Import_IsOK(result))
                {
                    model = new M_QQIM_User();
                    model.UserID = mu.UserID;
                    model.IM_Identity = identity;
                    model.Sign = new C_QQIM_Sign().GetUserSign(model.IM_Identity);
                    model.SignDate = DateTime.Now;
                    model.ID = Insert(model);
                }
                else { ZLLog.L("IM导入用户失败,原因:" + result); }
            }
            else if ((DateTime.Now - model.SignDate).TotalDays > 178) { model.Sign = new C_QQIM_Sign().GetUserSign(model.IM_Identity); UpdateByID(model); }
            return model;
        }
    }
    public class C_QQIM_Contasnt
    {
        public const int appid = 1400012582;
        public const int accountType = 6551;
        public const string baseurl = "https://console.tim.qq.com/";
        public const string pri_key_path = @"C:\web\im\private_key";// @"D:\Code\ZoomlaCMS2\ZoomLa.WebSite\test\private_key";
        public const string pub_key_path = @"C:\web\im\public_key"; //@"D:\Code\ZoomlaCMS2\ZoomLa.WebSite\test\public_key";
        public const string dllPath = @"C:\web\im\sigcheck32.dll";
        public const string appadmin = "admin1";//管理员名称
        public const string appadminSign = "eJxljl1PgzAUhu-5FaS3GsNXcZh4AbNGUzbdEEy8IUhbdrJSCOsWnPG-O3GJTTy3z-Oe9-20bNtGL2l2VdV1t1e61B89R-aNjRx0*Qf7HlhZ6dIf2D-Ixx4GXlZC82GCLsbYcxzTAcaVBgFno2ItKNfgO7Ytp5LfB8Ep7Xp45pkKNBNckHz*SF5p1y4Pd080eWjq5-UoGx-T*3ofbSIoSJ6kkS6KJluFTQwknkH2LqSiWMckOUqSJmIDavTlPMkW14y*iTxslxcyOG5vjUoNLT8PCiPsuDgwNx-4sINOTYJ3gq7nOz*HrC-rG2MGXT8_";
    }
    public class C_QQIM_Sign
    {
        //根据用户信息与appid生成签名(可存表中,单独为其建立,免重生成),再根据签名登录IM服务器
        public string GetUserSign(string identity)
        {
            string pri_key_path = C_QQIM_Contasnt.pri_key_path;
            FileStream f = new FileStream(pri_key_path, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(f);
            byte[] b = new byte[f.Length];
            reader.Read(b, 0, b.Length);
            string pri_key = Encoding.Default.GetString(b);

            StringBuilder sig = new StringBuilder(4096);
            StringBuilder err_msg = new StringBuilder(4096);
            int ret = sigcheck.tls_gen_sig_ex(C_QQIM_Contasnt.appid, identity, sig, 4096, pri_key, (UInt32)pri_key.Length, err_msg, 4096);
            if (0 != ret)
            {
                ZLLog.L("IM签名生成失败,原因:" + err_msg);
            }
            return sig.ToString();
        }
        //class dllpath
        //{
        //    //IIS需要改为32位,或X86平台
        //    //public const string DllPath = @"D:\Code\ZoomlaCMS2\ZoomLa.WebSite\test\sigcheck64.dll";
        //    // 64 位
        //    // 如果选择 Any CPU 平台，默认加载 32 位 dll
        //    public const string DllPath = @"D:\Code\ZoomlaCMS2\ZoomLa.WebSite\test\sigcheck32.dll";  // 32 位
        //}
        class sigcheck
        {
            [DllImport(C_QQIM_Contasnt.dllPath, EntryPoint = "tls_gen_sig", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public extern static int tls_gen_sig(
                UInt32 expire,
                string appid3rd,
                UInt32 sdkappid,
                string identifier,
                UInt32 acctype,
                StringBuilder sig,
                UInt32 sig_buff_len,
                string pri_key,
                UInt32 pri_key_len,
                StringBuilder err_msg,
                UInt32 err_msg_buff_len
            );

            [DllImport(C_QQIM_Contasnt.dllPath, EntryPoint = "tls_vri_sig", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public extern static int tls_vri_sig(
                string sig,
                string pub_key,
                UInt32 pub_key_len,
                UInt32 acctype,
                string appid3rd,
                UInt32 sdkappid,
                string identifier,
                StringBuilder err_msg,
                UInt32 err_msg_buff_len
            );

            [DllImport(C_QQIM_Contasnt.dllPath, EntryPoint = "tls_gen_sig_ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public extern static int tls_gen_sig_ex(
                UInt32 sdkappid,
                string identifier,
                StringBuilder sig,
                UInt32 sig_buff_len,
                string pri_key,
                UInt32 pri_key_len,
                StringBuilder err_msg,
                UInt32 err_msg_buff_len
            );

            [DllImport(C_QQIM_Contasnt.dllPath, EntryPoint = "tls_vri_sig_ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public extern static int tls_vri_sig_ex(
                string sig,
                string pub_key,
                UInt32 pub_key_len,
                UInt32 sdkappid,
                string identifier,
                ref UInt32 expire_time,
                ref UInt32 init_time,
                StringBuilder err_msg,
                UInt32 err_msg_buff_len
            );
        }
    }
}
