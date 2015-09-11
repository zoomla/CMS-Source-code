
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
    using System.Collections;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.Common;
    using ZoomLa.IDAL;
    using ZoomLa.BLL;
    /// <summary>
    /// SD_WorkRole 的摘要说明
    /// </summary>
    public class SD_WorkRole:ID_WorkRole
    {
        public SD_WorkRole()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool AddWorkRole(M_WorkRole m_workrole)
        {
            string sqlStr = "INSERT INTO ZL_WorkRole(WorkID,RoleID) VALUES(@WorkID,@RoleID)";
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@WorkID", SqlDbType.Int,4);
            parameter[0].Value = m_workrole.WorkID;
            parameter[1] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameter[1].Value = m_workrole.RoleID;
            return SqlHelper.ExecuteSql(sqlStr, parameter);
        }
        public bool UpdateWorkRole(M_WorkRole m_workrole)
        {
            string sqlStr = "Update ZL_WorkRole set WorkID=@WorkID,RoleID=@RoleID where ID=@ID";
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@WorkID", SqlDbType.Int, 4);
            parameter[0].Value = m_workrole.WorkID;
            parameter[1] = new SqlParameter("@RoleID", SqlDbType.Int, 4);
            parameter[1].Value = m_workrole.RoleID;
            parameter[2] = new SqlParameter("@ID", SqlDbType.Int, 4);
            parameter[2].Value = m_workrole.ID;
            return SqlHelper.ExecuteSql(sqlStr, parameter);
        }
        public ArrayList GetWorkRole(int WorkID)
        {
            ArrayList arraylistrole=new ArrayList();
            string sqlStr = "select RoleID From ZL_WorkRole where WorkID=@WorkID";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@WorkID", SqlDbType.Int, 4);
            parameter[0].Value = WorkID;
            SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, parameter);
            while(dr.Read())
            {
                arraylistrole.Add(dr["RoleID"]);
            }
            return arraylistrole;
        }
        public bool DelWorkRole(int WorkID)
        {
            string sqlStr = "delete from ZL_WorkRole where WorkID=@WorkID";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@WorkID", SqlDbType.Int, 4);
            parameter[0].Value =WorkID;
            return SqlHelper.ExecuteSql(sqlStr, parameter);
        }
    }
}
