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

namespace ZoomLa.SQLDAL
{

    /// <summary>
    /// SD_ProjectDiscuss 的摘要说明
    /// </summary>
    public class SD_ProjectDiscuss : ID_ProjectDiscuss
    {
        public SD_ProjectDiscuss()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private SqlParameter[] GetParameters(M_ProjectDiscuss ProjectDiscuss)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@DiscussID", SqlDbType.Int,4),
                new SqlParameter("@ProjectID", SqlDbType.Int,4),
                new SqlParameter("@WorkID", SqlDbType.Int,4),
                new SqlParameter("@UserID", SqlDbType.Int,4),
                new SqlParameter("@Content", SqlDbType.NText),  
                new SqlParameter("@DiscussDate",SqlDbType.DateTime,8)
            };
            parameter[0].Value = ProjectDiscuss.DiscussID;
            parameter[1].Value = ProjectDiscuss.ProjectID;
            parameter[2].Value = ProjectDiscuss.WorkID;
            parameter[3].Value = ProjectDiscuss.UserID;
            parameter[4].Value = ProjectDiscuss.Content;
            parameter[5].Value = ProjectDiscuss.DiscussDate;
            return parameter;
        }
        public bool AddProjectDiscuss(M_ProjectDiscuss ProjectDiscuss)
        {
            string strSql = "PR_ProjectDiscuss_Add";
            SqlParameter[] parameter = GetParameters(ProjectDiscuss);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public bool DelProjectDiscuss(int DiscussID)
        {         
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int,4);
            cmdParams[0].Value = DiscussID;
            return SqlHelper.ExecuteProc("PR_ProjectDiscuss_Delete", cmdParams);
        }
        public bool UpdateProjectDiscuss(M_ProjectDiscuss ProjectDiscuss)
        {
            string strSql = "PR_ProjectDiscuss_Update";
            SqlParameter[] cmdParams = GetParameters(ProjectDiscuss);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        public DataTable GetDiscussByWid(int WId)
        {
            string strSql = "PR_ProjectDiscuss_SelByWid";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@Wid", SqlDbType.Int,4);
            cmdParams[0].Value = WId;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure,strSql, cmdParams);
        }
        public DataTable GetDiscussAll()
        {
            string strsql = "SELECt *  FROM ZL_ProjectDiscuss";
            return SqlHelper.ExecuteTable(CommandType.Text, strsql, null);
            //string strSql = "PR_ProjectDiscuss_GetAll";           
            //return SqlHelper.ExecuteTable(CommandType.StoredProcedure, strSql,null);
        }
        public int CountDisByWid(int Wid,int Pid)
        {
            string strsql = "select Count(*) from ZL_ProjectDiscuss where WorkID=@wid and ProjectID=@pid";
            SqlParameter[] cmdParams = new SqlParameter[2];
            cmdParams[0] = new SqlParameter("@wid", SqlDbType.Int, 4);
            cmdParams[0].Value = Wid;
            cmdParams[1] = new SqlParameter("@pid", SqlDbType.Int, 4);
            cmdParams[1].Value = Pid;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strsql, cmdParams));
        }        
    }
}
