using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.User
{
    public class B_User_API
    {
        M_UserInfo initMod = new M_UserInfo();
        string TbName = "ZL_User";
        public M_UserInfo Login(string uname, string upwd, bool ismd5 = false)
        {
            if (StrHelper.StrNullCheck(uname, upwd)) { return new M_UserInfo(); }
            uname = uname.Trim(); upwd = upwd.Trim();
            if (ismd5 == false) { upwd = StringHelper.MD5(upwd); }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uname", uname), new SqlParameter("upwd", upwd) };
            M_UserInfo mu = SelReturnModel("UserName=@uname AND UserPwd=@upwd", sp);
            mu.OpenID = CreateOpenID(mu);
            return mu;
        }
        /// <summary>
        /// 根据openid,获取用户
        /// (未指定openid则查询网站用户,用于微信公众号内)
        /// </summary>
        public static M_UserInfo GetLogin(string openid)
        {
            //未输入OpenID则读取网站用户(微信公众号)
            if (string.IsNullOrEmpty(openid)) { return new B_User().GetLogin(); }
            //------------APP使用
            B_User_API buapi = new B_User_API();
            M_UserInfo mu = new M_UserInfo();
            string uname = "", upwd = "";
            buapi.DeOpenID(openid, ref uname, ref upwd);
            mu = buapi.Login(uname, upwd, true);
            mu.OpenID = openid;
            return mu;
        }
        //----------------Tools
        public string CreateOpenID(M_UserInfo mu)
        {
            if (mu.IsNull || string.IsNullOrEmpty(mu.UserName) || string.IsNullOrEmpty(mu.UserPwd)) { throw new Exception("OpenID用户信息不正确"); }
            return EncryptHelper.AESEncrypt(mu.UserName + ":::" + mu.UserPwd);
        }
        public void DeOpenID(string openid, ref string uname, ref string upwd)
        {
            string deStr = EncryptHelper.AESDecrypt(openid);
            if (!deStr.Contains(":::")) { return; }
            uname = Regex.Split(deStr, ":::")[0];
            upwd = Regex.Split(deStr, ":::")[1];
        }
        private M_UserInfo SelReturnModel(string where, SqlParameter[] sp)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE " + where, sp))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_UserInfo(true);
                }
            }
        }
    }
}
