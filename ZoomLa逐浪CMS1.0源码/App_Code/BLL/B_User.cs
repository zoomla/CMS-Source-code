namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Web;
    using ZoomLa.DALFactory;
    using System.Globalization;
using System.Collections.Generic;   

    /// <summary>
    /// B_User 的摘要说明
    /// </summary>
    public class B_User
    {
        private static readonly ID_User userMethod = IDal.CreateUser();
        public B_User()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //获取会员所有信息
        public bool Add(M_UserInfo userInfo)
        {
            return userMethod.Add(userInfo);
        }
        public DataView GetUserInfo()
        {
            return userMethod.GetUserAll().DefaultView;
        }
        public DataTable GetUserInfos()
        {
            return userMethod.GetUserAll();
        }
        //根据会员ＩＤ获取会员信息
        public bool DelUserById(int userID)
        {
            return userMethod.DeleteUserById(userID);
        }
        //跟新会员信息
        public bool UpDateUser(M_UserInfo userInfo)
        {
            return userMethod.UpDate(userInfo);
        }
        //根据会员ＩＤ判断该会员是否存在
        public bool IsExit(int userID)
        {
            return userMethod.IsExit(userID);
        }
        //根据会员名判断会员是否存在
        public bool IsExit(string userName)
        {
            return userMethod.IsExit(userName);
        }
        //根据会员id获取该会员所有信息
        public M_UserInfo SeachByID(int userID)
        {
            return userMethod.SeachByID(userID);
        }
        //更新会员，将其设为锁定状态并记录当前锁定时间
        public bool UpUserLock(int userID, DateTime lockoutTime)
        {
            return userMethod.UpUserLock(userID,lockoutTime);
        }
        //更新会员，状态设为正常
        public bool UpUserUnLock(int userID)
        {
            return userMethod.UpUserUnLock(userID);
        }
        //判断该会员是否通过管理员认证
        public bool UpUserTrueFroM(int userID)
        {
            return userMethod.UpUserTrueFroM(userID);
        }
        //判断该会员是否通过邮件认证
        public bool UpUserTrueFroE(int userID)
        {
            return userMethod.UpUserTrueFroE(userID);
        }
        public DataTable GetAnswers(string question)
        {
            return userMethod.GetQuestion(question);
        }

        public M_UserInfo GetUserByName(string UserName)
        {
            return userMethod.SeachByName(UserName);
        }
        /// <summary>
        /// 已登录记录的用户名和加密密码读取用户信息
        /// </summary>
        public M_UserInfo GetLoginUser(string loginname, string loginpass)
        {
            return userMethod.GetLoginUser(loginname, loginpass);
        }
        public M_UserInfo LoginUser(string loginname, string pass)
        {
            return userMethod.LoginUser(loginname, pass);
        }
        /// <summary>
        /// 检测是否登录了
        /// </summary>
        public void CheckIsLogin()
        {            
            if (HttpContext.Current.Request.Cookies["UserState"] == null)
            {
                HttpContext.Current.Response.Redirect("~/User/Login.aspx");
            }
            else
            {
                string loginName = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                string password = HttpContext.Current.Request.Cookies["UserState"]["Password"];
                if (GetLoginUser(loginName, password).IsNull)
                {
                    HttpContext.Current.Response.Redirect("~/User/Login.aspx");
                }
            }
        }
        /// <summary>
        /// 清除Cookie
        /// </summary>
        public void ClearCookie()
        {
            HttpContext.Current.Response.Cookies["UserState"].Expires = DateTime.Now.AddDays(-1.0);
        }
        public bool CheckLogin()
        {
            if (HttpContext.Current.Request.Cookies["UserState"] == null)
            {
                return false;
            }
            else
            {
                string loginName = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                string password = HttpContext.Current.Request.Cookies["UserState"]["Password"];
                if (GetLoginUser(loginName, password).IsNull)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 用户登录操作
        /// </summary>
        public M_UserInfo AuthenticateUser(string UserName, string Password)
        {
            M_UserInfo user = GetUserByName(UserName);
            if (!user.IsNull)
            {
                string str = StringHelper.MD5(Password);
                if (!StringHelper.ValidateMD5(user.UserPwd, str))
                {
                    return new M_UserInfo(true);
                }
                user.LastLoginIP = HttpContext.Current.Request.UserHostAddress;
                user.LastLoginTimes = DateTime.Now;
                user.LoginTimes++;
                UpDateUser(user);
            }
            return user;
        }
        /// <summary>
        /// 设定登录状态
        /// </summary>
        /// <param name="model"></param>
        public void SetLoginState(M_UserInfo model)
        {
            HttpContext.Current.Response.Cookies["UserState"]["UserID"] = model.UserID.ToString();
            HttpContext.Current.Response.Cookies["UserState"]["LoginName"] = model.UserName;
            HttpContext.Current.Response.Cookies["UserState"]["Password"] = model.UserPwd;            
        }
        public IList<string[]> GetUserNameAndEmailList(int num, string text)
        {
            return userMethod.GetUserNameAndEmailList(num, text);
        }
        public int GetUserNameListTotal(string keyword)
        {
            return userMethod.GetUserNameListTotal(keyword);
        }
        public IList<string> GetUserNameList(int startRowIndexId, int maxiNumRows, string keyword)
        {
            return userMethod.GetUserNameList(startRowIndexId, maxiNumRows, keyword);
        }
    }
}