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
    using ZoomLa.BLL;

    /// <summary>
    /// SD_Dic 的摘要说明
    /// </summary>
    public class SD_Cate : ID_Cate
    {
        public SD_Cate()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public M_Cate GetCateByid(int cateId)
        {
            string sqlStr = "select * from ZL_Cate where cateid="+cateId;

            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sqlStr, null);
            M_Cate temp = new M_Cate();
            if (ds.Tables[0].Rows.Count == 0) return null;
            temp.CateID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            temp.CateName=Convert.ToString(ds.Tables[0].Rows[0][1]);
            return temp;
        }
        public bool Add(M_Cate cate) 
        {
            string sqlStr = "INSERT INTO ZL_Cate(CateName) VALUES(@CateName)";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@CateName", SqlDbType.NVarChar, 50);
            parameter[0].Value = cate.CateName;
            return SqlHelper.ExecuteSql(sqlStr, parameter);
        }
        public bool Update(M_Cate cate)
        {
            string sqlStr = "UPDATE ZL_Cate SET CateName=@CateName where CateID=@CateID";
            SqlParameter[] cmdParams = GetParameters(cate);

            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }
        private static SqlParameter[] GetParameters(M_Cate cate)
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CateName", SqlDbType.NVarChar,50);
            parameter[0].Value = cate.CateName;
            parameter[1] = new SqlParameter("@CateID", SqlDbType.Int);
            parameter[1].Value = cate.CateID;
            return parameter;
        }
        public bool DeleteByID(int cateID)
        {
            string sqlStr = "delete from ZL_Cate where CateID=@CateID;delete from ZL_CateDetail where CateID=@CateID";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@CateID", SqlDbType.Int);
            parameter[0].Value = cateID;
            return SqlHelper.ExecuteSql(sqlStr,parameter);
        }
      
        public DataTable SeachCateAll()
        {
            string sqlStr = "select CateID,CateName from ZL_Cate";
            
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null); 
            
        }
        private static SqlParameter[] GetParameterss(M_Cate cate)
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@CateID", SqlDbType.Int);
            parameter[0].Value =cate.CateID;
            return parameter;
        }
    }
}
