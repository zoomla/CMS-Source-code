namespace ZoomLa.DZNT
{
    using System;
    using System.Data;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using ZoomLa.Common;

    /// <summary>
    /// UserSql 的摘要说明
    /// </summary>
    public class UserSql
    {
        public UserSql()
        {
            
        }
        public static UserInfo GetUserByName(string username)
        {
            string strSql = "select * from dnt_users where username=@username";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@username",SqlDbType.NChar,20)
            };
            sp[0].Value = username;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                if (dr.Read())
                {
                    UserInfo uinfo = new UserInfo();
                    uinfo.uid = DataConverter.CLng(dr["uid"]);
                    uinfo.username = dr["username"].ToString();
                    uinfo.gender = DataConverter.CLng(dr["gender"]);
                    uinfo.email = dr["email"].ToString();
                    uinfo.groupid = DataConverter.CLng(dr["groupid"]);
                    uinfo.password = dr["password"].ToString();
                    uinfo.regip = dr["regip"].ToString();
                    uinfo.lastip = dr["lastip"].ToString();
                    return uinfo;
                }
                else
                {
                    return new UserInfo(true);
                }
            }
        }
        public static int CreateUser(UserInfo user1)
        {
            string strSql = "INSERT INTO dnt_users([username],[password],[gender],[groupid],[regip],[lastip],[extcredits1],[extcredits2],[email]) values (@username,@password,@gender,@groupid,@regip,@lastip,10,1,@email);select @@IDENTITY AS newID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@username",SqlDbType.NChar,20),
                new SqlParameter("@password",SqlDbType.Char,32),
                new SqlParameter("@gender",SqlDbType.Int),
                new SqlParameter("@groupid",SqlDbType.Int),
                new SqlParameter("@regip",SqlDbType.Char,15),
                new SqlParameter("@lastip",SqlDbType.Char,15),                
                new SqlParameter("@email",SqlDbType.Char,50)
            };
            sp[0].Value = user1.username;
            sp[1].Value = user1.password;
            sp[2].Value = user1.gender;
            sp[3].Value = user1.groupid;
            sp[4].Value = user1.regip;
            sp[5].Value = user1.lastip;
            sp[6].Value = user1.email;
            int uid = SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
            string strSql2 = "UPDATE [dnt_statistics] SET [totalusers]=[totalusers] + 1,[lastusername]=@username,[lastuserid]=@uid";
            SqlParameter[] sp2 = new SqlParameter[] {
                new SqlParameter("@username",SqlDbType.NChar,20),              
                new SqlParameter("@uid",SqlDbType.Int)
            };
            sp2[0].Value = user1.username;
            sp2[1].Value = uid;
            bool i = SqlHelper.ExecuteSql(strSql2, sp2);
            string strSql3 = "INSERT INTO dnt_userfields ([uid],[avatar]) values("+uid.ToString()+",'avatars\\common\\0.gif')";
            i = SqlHelper.ExecuteSql(strSql3);
            return uid;
        }
        public static bool ChgUserPsw(UserInfo user1)
        {
            string strSql = "Update dnt_users set password=@password where uid=@uid";
            SqlParameter[] sp = new SqlParameter[] {                
                new SqlParameter("@password",SqlDbType.Char,32),
                new SqlParameter("@uid",SqlDbType.Int)
            };
            sp[0].Value = user1.password;
            sp[1].Value = user1.uid;
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        public static bool DelUser(UserInfo user1)
        {
            string strSql = "delete from dnt_users where uid=@uid";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@uid",SqlDbType.Int)
            };            
            sp[0].Value = user1.uid;
            return SqlHelper.ExecuteSql(strSql, sp);
        }
    }
}