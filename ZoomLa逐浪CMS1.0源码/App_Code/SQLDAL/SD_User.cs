using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.IDAL;
using ZoomLa.Model;
using System.Data.SqlClient;
using ZoomLa.Common;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.SQLDAL
{
    /// <summary>
    /// SD_User 的摘要说明
    /// </summary>
    ///
    public class SD_User : ID_User
    {
        /// <summary>
        ///　增加会员
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool Add(M_UserInfo UserInfo)
        {
            string strSql = "PR_User_AddUpdate";
            SqlParameter[] parameter = GetParameters(UserInfo);            
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public SD_User()
        { 
        }
        /// <summary>
        /// 根据ID删除会员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUserById(int userId)
        {
            string sqlStr = "DELETE FROM ZL_User WHERE UserID=@UserID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            cmdParams[0].Value = userId;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        //锁定会员并记录时间
        public bool UpUserLock(int userID,DateTime lockoutTime)
        {
            SqlParameter[] cmdParams = new SqlParameter[2];
            cmdParams[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            cmdParams[0].Value = userID;
            cmdParams[1] = new SqlParameter("@LockoutTime", SqlDbType.DateTime);
            cmdParams[1].Value = lockoutTime;
            return SqlHelper.ExecuteProc("PR_UserLock", cmdParams);
        }
        //解锁（正常）
        public bool UpUserUnLock(int userID)
        {
            string sqlStr = "update ZL_User set Status=0 where UserID=@UserID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            cmdParams[0].Value = userID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        //未通过管理员认证
        public bool UpUserTrueFroM(int userID)
        {
            string sqlStr = "update ZL_User set Status=4 where UserID=@UserID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            cmdParams[0].Value = userID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        //未通过邮件认证
        public bool UpUserTrueFroE(int userID)
        {
            string sqlStr = "update ZL_User set Status=2 where UserID=@UserID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            cmdParams[0].Value = userID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        /// <summary>
        /// 更新会员
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool UpDate(M_UserInfo userInfo)
        {
            string strSql = "PR_User_AddUpdate";
            SqlParameter[] parameter = GetParameters(userInfo);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        /// <summary>
        /// 根据会员ＩＤ＼姓名判断新会员是否存在
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsExit(int userID)
        {
            string strSql = "SELECT count(UserID) FROM ZL_User WHERE UserID=@UserID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int) };
            cmdParams[0].Value = userID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams))>0;
        }
        public bool IsExit(string userName)
        {
            string strSql = "SELECT COUNT(UserID) FROM ZL_User WHERE UserName=@UserName";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 20) };
            cmdParams[0].Value = userName;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams)) > 0;
        }
        /// <summary>
        /// 根据会员id＼姓名查询会员信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public M_UserInfo SeachByID(int userID)
        {
            string sqlStr = "SELECT * FROM ZL_User WHERE 1=1";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            if (userID > 0)
            {
                sqlStr = sqlStr + " AND UserID=@UserID ";
                cmdParams[0].Value = userID;
            }
            else
            {
                return new M_UserInfo(true);
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetUserInfoFromReader(reader);
                }
                else
                    return new M_UserInfo(true);
            }
        }
        public M_UserInfo SeachByName(string userName)
        {
            string sqlStr = "SELECT * FROM ZL_User WHERE 1=1";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 50) };
            if (!string.IsNullOrEmpty(userName))
            {
                sqlStr = sqlStr + " AND UserName=@UserName ";
                cmdParams[0].Value = userName;
            }
            else
            {
                return new M_UserInfo(true);
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetUserInfoFromReader(reader);
                }
                else
                    return new M_UserInfo(true);
            }
        }
        /// <summary>
        /// 查询所有会员信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserAll()
        {
            string sqlStr = "select UserID,UserName,RegTime,LastLoginIP,LoginTimes,LastLockTime,Status from ZL_User";
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null); 
        }
        /// <summary>
        /// 传参
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private SqlParameter[] GetParameters(M_UserInfo userInfo)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@UserID", SqlDbType.Int),
                new SqlParameter("@UserName", SqlDbType.NVarChar, 20),
                new SqlParameter("@UserPwd", SqlDbType.NVarChar, 255),
                new SqlParameter("@Email", SqlDbType.NVarChar, 255),
                new SqlParameter("@Question", SqlDbType.NVarChar, 255),
                new SqlParameter("@Answer", SqlDbType.NVarChar, 255),
                new SqlParameter("@UserFace", SqlDbType.NVarChar, 255),
                new SqlParameter("@FaceHeight", SqlDbType.Int),
                new SqlParameter("@FaceWidth", SqlDbType.Int),
                new SqlParameter("@RegTime", SqlDbType.DateTime),
                new SqlParameter("@Sign", SqlDbType.NText),
                new SqlParameter("@PrivacySetting", SqlDbType.Int),
                new SqlParameter("@LoginTimes", SqlDbType.Int),
                new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
                new SqlParameter("@LastLoginIP", SqlDbType.NVarChar, 50),
                new SqlParameter("@LastPwdChangeTime", SqlDbType.DateTime),
                new SqlParameter("@LastLockTime", SqlDbType.DateTime),
                new SqlParameter("@CheckNum", SqlDbType.NVarChar,50),
                new SqlParameter("@Status", SqlDbType.Int)
            };
            parameter[0].Value = userInfo.UserID;
            parameter[1].Value = userInfo.UserName;            
            parameter[2].Value = userInfo.UserPwd;            
            parameter[3].Value = userInfo.Email;            
            parameter[4].Value = userInfo.Question;            
            parameter[5].Value = userInfo.Answer;            
            parameter[6].Value = userInfo.UserFace;            
            parameter[7].Value = userInfo.FaceHeight;            
            parameter[8].Value = userInfo.FaceWidth;            
            parameter[9].Value = userInfo.RegTime;            
            parameter[10].Value = userInfo.Sign;            
            parameter[11].Value = userInfo.PrivacySetting;
            parameter[12].Value = userInfo.LoginTimes;
            parameter[13].Value = userInfo.LastLoginTimes;
            parameter[14].Value = userInfo.LastLoginIP;
            parameter[15].Value = userInfo.LastPwdChangeTime;
            parameter[16].Value = userInfo.LastLockTime;
            parameter[17].Value = userInfo.CheckNum;
            parameter[18].Value = userInfo.Status;
            return parameter;
        }
        /// <summary>
        /// 从Sqldatareader读取会员信息
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private M_UserInfo GetUserInfoFromReader(SqlDataReader reader)
        {
            M_UserInfo info = new M_UserInfo();
            info.UserID = DataConverter.CLng(reader["UserID"].ToString());
            info.UserName = reader["UserName"].ToString();
            info.UserPwd = reader["UserPwd"].ToString();
            info.Email = reader["Email"].ToString();
            info.Question = reader["Question"].ToString();
            info.Answer = reader["Answer"].ToString();
            info.UserFace = reader["UserFace"].ToString();
            info.FaceHeight = DataConverter.CLng(reader["FaceHeight"].ToString());
            info.FaceWidth = DataConverter.CLng(reader["FaceWidth"].ToString());
            info.RegTime = DataConverter.CDate(reader["RegTime"].ToString());
            info.Sign = reader["Sign"].ToString();
            info.PrivacySetting = DataConverter.CLng(reader["PrivacySetting"]);
            info.GroupID = DataConverter.CLng(reader["GroupID"].ToString());
            info.LastLockTime = DataConverter.CDate(reader["LastLockTime"].ToString());
            info.LastLoginIP = reader["LastLoginIP"].ToString();
            info.LastLoginTimes = DataConverter.CDate(reader["LastLoginTime"].ToString());
            info.LastPwdChangeTime = DataConverter.CDate(reader["LastPwdChangeTime"].ToString());
            info.LoginTimes = DataConverter.CLng(reader["LoginTimes"].ToString());
            info.Status = DataConverter.CLng(reader["Status"].ToString());
            info.CheckNum = reader["CheckNum"].ToString();            
            reader.Close();
            return info;
        }
        /// <summary>
        /// 已登录记录的用户名和加密密码读取用户信息
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="loginpass"></param>
        /// <returns></returns>
        public M_UserInfo GetLoginUser(string loginname, string loginpass)
        {
            string UserPass = loginpass;
            string strSql = "select * from ZL_User where UserName=@UserName and UserPwd=@UserPwd";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar, 255),
                new SqlParameter("@UserPwd", SqlDbType.NVarChar, 255)
            };
            cmdParam[0].Value = loginname;
            cmdParam[1].Value = UserPass;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParam))
            {
                if (reader.Read())
                    return GetUserInfoFromReader(reader);
                else
                    return new M_UserInfo(true);
            }
        }
        /// <summary>
        /// 登录读取信息进行判断
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public M_UserInfo LoginUser(string loginname, string pass)
        {
            string UserPass = StringHelper.MD5(pass);
            string strSql = "select * from ZL_User where UserName=@UserName and UserPwd=@UserPwd";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar, 255),
                new SqlParameter("@UserPwd", SqlDbType.NVarChar, 255)
            };
            cmdParam[0].Value = loginname;
            cmdParam[1].Value = UserPass;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParam))
            {
                if (reader.Read())
                    return GetUserInfoFromReader(reader);
                else
                    return new M_UserInfo(true);
            }
        }
        public IList<string[]> GetUserNameAndEmailList(int num, string text)
        {
            IList<string[]> list = new List<string[]>();
            string strSql = "select UserName,UserEmail From ZL_User ";
            if (num == 0)
            {
                
            }
            if (num == 2)
            {
                string[] strArray = text.Split(new char[] { ',' });
                StringBuilder builder = new StringBuilder("");
                builder.Append(" where UserName in (");
                for (int i = 0; i < strArray.Length; i++)
                {
                    builder.Append("'" + strArray[i] + "',");
                }
                strSql = strSql + builder.ToString().TrimEnd(new char[] { ',' }) + ")";
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, null))
            {
                while (reader.Read())
                {
                    list.Add(new string[] { reader["UserName"].ToString(), reader["Email"].ToString() });
                }
            }
            return list;
        }
        public IList<string> GetUserNameList(int startRowIndexId, int maxiNumRows,string keyword)
        {
            IList<string> list = new List<string>();
            string strSql = "PR_Common_List2";
            SqlParameter[] cmdParam = new SqlParameter[] { 
                new SqlParameter("@Tables",SqlDbType.NVarChar),       //表名，可以是多个表，但不能用别名
                new SqlParameter("@Identity",SqlDbType.NVarChar),       //主键，可以为空，但@Order为空时该值不能为空
                new SqlParameter("@Fields",SqlDbType.NVarChar),           //要取出的字段，可以是多个表的字段，可以为空，为空表示select *
                new SqlParameter("@PageSize",SqlDbType.Int),              //每页记录数
                new SqlParameter("@Page",SqlDbType.Int),           //当前页，0表示第1页
                                                                          
                new SqlParameter("@Filter",SqlDbType.NVarChar),           //条件，可以为空，不用填 where
                new SqlParameter("@Group",SqlDbType.NVarChar),            //分组依据，可以为空，不用填 group by
                new SqlParameter("@Order",SqlDbType.NVarChar)            //排序，可以为空，为空默认按主键升序排列，不用填 order by
            };
            cmdParam[0].Value = "ZL_User";
            cmdParam[1].Value = "UserID";
            cmdParam[2].Value = "UserName";
            cmdParam[3].Value = maxiNumRows;
            cmdParam[4].Value = startRowIndexId - 1;
            cmdParam[5].Value = "UserName like '%" + keyword + "%'";            
            cmdParam[6].Value = "";
            cmdParam[7].Value = "UserID ASC";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, strSql, cmdParam))
            {
                while (reader.Read())
                {
                    list.Add(reader["UserName"].ToString());
                }
            }
            return list;
        }

        public int GetUserNameListTotal(string keyword)
        {
            string strSql = "Select count(*) from ZL_User";
            strSql = strSql + " where UserName like @UserName";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@UserName", SqlDbType.NVarChar)
            };
            cmdParam[0].Value="%" + keyword + "%";

            object objA = SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam);
            return (object.Equals(objA, null) ? 0 : Convert.ToInt32(objA));
        }
        public DataTable GetQuestion(string question)
        {
            string sqlStr = "select count(Answer),Answer from ZL_User where Question=@Question";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@Question", SqlDbType.NVarChar, 20) };
            cmdParams[0].Value = question;
            return SqlHelper.ExecuteTable(CommandType.Text,sqlStr,cmdParams);
        }
   
    }
}