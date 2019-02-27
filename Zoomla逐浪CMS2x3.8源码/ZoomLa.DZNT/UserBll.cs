namespace ZoomLa.DZNT
{
    using System;
    using System.Data;
    using System.Configuration;
    using ZoomLa.Common;
    using ZoomLa.Model;

    /// <summary>
    /// UserBll 的摘要说明
    /// </summary>
    public class UserBll
    {
        public UserBll()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public static int AddDZUser(M_UserInfo user1,bool Sex)
        {
            UserInfo uinfo = new UserInfo();
            uinfo.username = user1.UserName;
            uinfo.gender = Sex ? 0 : 1;
            uinfo.password = StringHelper.MD5DZ(user1.UserPwd);
            uinfo.regip = user1.LastLoginIP;
            uinfo.lastip = user1.LastLoginIP;
            uinfo.email = user1.Email;
            return UserSql.CreateUser(uinfo);
        }
        public static bool ChgPsw(UserInfo user1)
        {
            return UserSql.ChgUserPsw(user1);
        }
        public static bool DelUser(UserInfo user1)
        {
            return UserSql.DelUser(user1);
        }
        public static UserInfo GetUserByName(string username)
        {
            return UserSql.GetUserByName(username);
        }
    }
}