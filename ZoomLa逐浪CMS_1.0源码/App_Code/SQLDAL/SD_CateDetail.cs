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
    /// SD_Dic1 的摘要说明
    /// </summary>
    public class SD_CateDetail:ID_CateDetail
    {
        public SD_CateDetail()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public bool Add(M_CateDetail catedetail) 
        {
            string sqlStr = "INSERT INTO ZL_CateDetail(CateDetailName,CateID) VALUES(@CateDetailName,@CateID)";
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CateDetailName", SqlDbType.NVarChar, 50);
            parameter[0].Value = catedetail.CateDetailName;
            parameter[1]=new SqlParameter("@CateID",SqlDbType.Int);
            parameter[1].Value=catedetail.CateID;
            return SqlHelper.ExecuteSql(sqlStr, parameter);
        }
        public bool Update(M_CateDetail catedetail)
        {
            string sqlStr = "UPDATE ZL_CateDetail SET CateDetailName=@CateDetailName where CateDetailID=@CateDetailID";
            SqlParameter[] cmdParams = GetParameters(catedetail);
           int temp=SqlHelper.ExecuteNonQuery(CommandType.Text,sqlStr, cmdParams);
           if (temp == 1)
               return true;
           else
               return false;
        }
        private static SqlParameter[] GetParameters(M_CateDetail catedetail)
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CateDetailName", SqlDbType.NVarChar,50);
            parameter[0].Value = catedetail.CateDetailName;
            parameter[1] = new SqlParameter("@CateDetailID", SqlDbType.Int);
            parameter[1].Value = catedetail.CateDetailID;
            return parameter;
        }
        public bool DeleteByID(int catedetailID)
        {
            string sqlStr = "delete from ZL_CateDetail where CateDetailID=@CateDetailID";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@CateDetailID", SqlDbType.Int);
            parameter[0].Value = catedetailID;
            return SqlHelper.ExecuteSql(sqlStr,parameter);
        }
        public DataTable SeachCateDetailAll(int cateID)
        {
            string sqlStr = "select CateDetailID,CateDetailName from ZL_CateDetail where cateID="+cateID;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, null);       
            
        }
        public M_CateDetail GetcatedetailById(int catedetailid)
        {
            M_CateDetail tempCateDetail=new M_CateDetail();
            string sqlstr = "select CateDetailid,catedetailname from zl_catedetail where catedetailid=@catedetailid";
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@catedetailid", DbType.Int32);
            parameter[0].Value = catedetailid;
            
            DataTable dt=SqlHelper.ExecuteTable(CommandType.Text, sqlstr,parameter);
            if (dt.Rows.Count != 1) return null;
            else
            {
                tempCateDetail.CateDetailName = (dt.Rows[0]["catedetailname"]).ToString();
                tempCateDetail.CateDetailID = Convert.ToInt32(dt.Rows[0]["catedetailid"]);
            }

            return tempCateDetail;
                
        }

        
    }
}