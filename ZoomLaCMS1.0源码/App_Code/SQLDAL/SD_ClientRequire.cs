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
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using System.Collections;
    using ZoomLa.Common;
    /// <summary>
    /// SD_ClientRequire 的摘要说明
    /// </summary>
    public class SD_ClientRequire : ID_ClientRequire
    {
        public SD_ClientRequire()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_ClientRequire ClientRequireinfo)
        {
            string strSql = "PR_ClientRequire_Add";
            SqlParameter[] parameter = GetParameters(ClientRequireinfo);
            return SqlHelper.ExecuteProc(strSql, parameter);
        }
        public bool DeleteByID(int clientrequireId)
        {

            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int,4);
            cmdParams[0].Value = clientrequireId;
            return SqlHelper.ExecuteProc("PR_ClientRequire_Delete", cmdParams);
        }
        public bool Update(M_ClientRequire ClientRequireinfo)
        {
            SqlParameter[] cmdParams = GetParameters(ClientRequireinfo); ;
            return SqlHelper.ExecuteProc("PR_ClientRequire_Update", cmdParams);
        }
        private SqlParameter[] GetParameters(M_ClientRequire ClientRequireinfo)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@RequireID", SqlDbType.Int),
                new SqlParameter("@UserID", SqlDbType.Int),
                new SqlParameter("@Require", SqlDbType.NText,16),          
                new SqlParameter("@ReuqireDate", SqlDbType.DateTime,8),
            };
            parameter[0].Value = ClientRequireinfo.RequireID;
            parameter[1].Value = ClientRequireinfo.UserID;
            parameter[2].Value = ClientRequireinfo.Require;
            parameter[3].Value = ClientRequireinfo.ReuqireDate;
            return parameter;
         }
        public DataTable GetClientRequireAll()
        {
            string strSql = "select * from ZL_ClientRequire order by(RequireID) asc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
    }
}
