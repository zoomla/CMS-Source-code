namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.Common;
    using ZoomLa.IDAL;

    /// <summary>
    /// SD_Message 的摘要说明
    /// </summary>
    public class SD_Message:ID_Message
    {
        public SD_Message()
        {
        }
        /// <summary>
        /// 发送(增加)信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns>返回True/False</returns>
        public bool Add(M_Message message)
        {
            string sqlStr = "INSERT INTO ZL_Message(Title,Content,Sender,Incept,status,PostDate) VALUES(@Title,@Content,@Sender,@Incept,@status,@PostDate)";
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar,100);
            parameter[0].Value = message.Title;
            parameter[1] = new SqlParameter("@Content", SqlDbType.NText);
            parameter[1].Value = message.Content;
            parameter[2] = new SqlParameter("@Sender", SqlDbType.NVarChar);
            parameter[2].Value = message.Sender;
            parameter[3] = new SqlParameter("@Incept", SqlDbType.NVarChar);
            parameter[3].Value = message.Incept;
            parameter[4] = new SqlParameter("@status", SqlDbType.Bit,1);
            parameter[4].Value = message.status;
            parameter[5] = new SqlParameter("@PostDate", SqlDbType.DateTime);
            parameter[5].Value = message.PostDate;
            return SqlHelper.ExecuteSql(sqlStr, parameter);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <returns></returns>
        public bool Update(M_Message message)
        {
            string sqlStr = "UPDATE ZL_Message SET Title=@Title,Content=@Content,Sender=@Sender,Incept=@Incept,status=@status,PostDate=@PostDate Where MsgID=@MsgID";
            SqlParameter[] cmdParams = GetParameters(message);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        /// <summary>
        /// 传参
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_Message message)
        {
            SqlParameter[] parameter = new SqlParameter[7];
            parameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
            parameter[0].Value = message.Title;
            parameter[1] = new SqlParameter("@Content", SqlDbType.NVarChar, 255);
            parameter[1].Value = message.Content;
            parameter[2] = new SqlParameter("@Sender", SqlDbType.NVarChar, 255);
            parameter[2].Value = message.Sender;
            parameter[3] = new SqlParameter("@Incept", SqlDbType.NVarChar);
            parameter[3].Value = message.Incept;
            parameter[4] = new SqlParameter("@status", SqlDbType.Bit,1);
            parameter[4].Value = message.status;
            parameter[5] = new SqlParameter("@PostDate", SqlDbType.DateTime);
            parameter[5].Value = message.PostDate;
            parameter[6] = new SqlParameter("@MsgID", SqlDbType.Int);
            parameter[6].Value = message.MsgID;
            return parameter;
        }
        /// <summary>
        /// 根据ID删除信息
        /// </summary>
        /// <returns></returns>
        public bool DeleteById(int msgID)
        {
            string sqlStr = "DELETE FROM ZL_Message WHERE MsgID=@MsgID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@MsgID", SqlDbType.Int, 4);
            cmdParams[0].Value = msgID;
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        /// <summary>
        /// 依据信息ID\信息标题判断当前信息是否存在
        /// </summary>
        /// <returns>返回TRUE/FALUSE</returns>
        public bool IsExit(int msgID)
        {
            string sqlStr = "SELECT count(*) FROM ZL_Message WHERE MsgID=@MsgID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@MsgID", SqlDbType.Int, 4) };
            cmdParams[0].Value = msgID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text,sqlStr, cmdParams))>0;
        }
        public bool IsExit(string title)
        {
            string sqlStr = "SELECT count(*) FROM ZL_Message WHERE Title=@Title";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@Title", SqlDbType.Int) };
            cmdParams[0].Value = title;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams)) > 0;
        }
        /// <summary>
        /// 依据ID查询信息
        /// </summary>
        /// <param name="msgID"></param>
        /// <returns></returns>
        public M_Message SeachById(int msgID)
        {
            string sqlStr = "SELECT * FROM ZL_Message WHERE 1=1";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@MsgID", SqlDbType.Int, 4) };
            if (msgID > 0)
            {
                sqlStr = sqlStr + " AND MsgID=@MsgID ";
                cmdParams[0].Value = msgID;
            }
            else
            {
                return new M_Message(true);
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetMessageFromReader(reader);
                }
                else
                    return new M_Message(true);
            }
        }

        public DataTable SeachByUser(string UserName)
        {
            string sqlStr = "select * from ZL_Message where Incept=@UserName Order by PostDate desc";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar) };
            cmdParams[0].Value = UserName;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cmdParams);
        }

        public void DeleteByDate(int datediff)
        {
            string sqlStr = "delete from ZL_Message where datediff('day',getdate(),PostDate)>@diff and status=1";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@diff", SqlDbType.Int) };
            cmdParams[0].Value = datediff;
            SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        public void DeleteByUser(string UserName)
        {
            string sqlStr = "delete from ZL_Message where Incept=@Incept and status=1";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@Incept", SqlDbType.NVarChar) };
            cmdParams[0].Value = UserName;
            SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        private M_Message GetMessageFromReader(SqlDataReader reader)
        {
            M_Message message = new M_Message();
            message.MsgID = DataConverter.CLng(reader["MsgID"].ToString());
            message.Title = reader["Title"].ToString();
            message.Content = reader["Content"].ToString();
            message.Sender = reader["Sender"].ToString();
            message.Incept = reader["Incept"].ToString();
            message.status = DataConverter.CLng(reader["status"].ToString());
            message.PostDate = DataConverter.CDate(reader["PostDate"].ToString());
            reader.Close();
            return message;
        }
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns>返回DataTable</returns>
        public DataTable SeachMessageAll()
        {
            string sqlStr = "select * from ZL_Message Order by PostDate desc";
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sqlStr, null);
            DataTable dt = ds.Tables[0];
            return dt;
        }
        public int UserMessCount(string UserName)
        {
            string sqlStr = "select Count(MsgID) from ZL_Message Where Incept=@Incept and status=0";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@Incept", SqlDbType.NVarChar) };
            cmdParams[0].Value = UserName;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }
    }
}