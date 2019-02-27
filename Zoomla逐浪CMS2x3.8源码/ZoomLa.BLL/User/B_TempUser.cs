using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLa.BLL.User
{
    /*
     * 游客支持类库
     */
    public class B_TempUser
    {
        private B_User buser = new B_User();
        private HttpRequest curReq = HttpContext.Current.Request;
        /// <summary>
        /// 如果用户已登录返回用户,否则返回游客记录(无则新建)
        /// </summary>
        public M_UserInfo GetLogin(string uname="游客")
        {
            M_UserInfo mu = buser.GetLogin();
            if (mu.IsNull)
            {
                mu = new M_UserInfo();
                if (curReq.Cookies["TempUser"] == null)
                {
                    mu.UserName = uname;
                    mu.UserID = C_UserID();
                    mu.WorkNum = function.GetRandomString(10);
                    mu.RegTime = DateTime.Now;
                    C_NewUser(mu.UserID, mu.UserName, mu.WorkNum);
                }
                else
                {
                    mu.UserID = DataConverter.CLng(curReq.Cookies["TempUser"]["UserID"]);
                    mu.UserName = HttpUtility.UrlDecode(curReq.Cookies["TempUser"]["LoginName"]);
                    mu.WorkNum = curReq.Cookies["TempUser"]["WorkNum"];
                    mu.RegTime = DataConverter.CDate(curReq.Cookies["TempUser"]["RegTime"]);
                }
                mu.IsTemp = true;
                return mu;//游客
            }
            return mu;//正常已登录用户
        }
        /// <summary>
        /// 创建一个新的游客用户,写入Cookies
        /// </summary>
        public void C_NewUser(int uid,string uname,string worknum) 
        {
            HttpResponse rep = HttpContext.Current.Response;
            rep.Cookies["TempUser"]["UserID"] = uid.ToString();
            rep.Cookies["TempUser"]["LoginName"] = HttpUtility.UrlEncode(uname);
            rep.Cookies["TempUser"]["WorkNum"] = worknum;//游客唯一身份认证
            rep.Cookies["TempUser"]["RegTime"] = DateTime.Now.ToString();
        }
        private int C_UserID() 
        {
            return new Random().Next(Int32.MinValue, -1);
        }
    }
    
}
