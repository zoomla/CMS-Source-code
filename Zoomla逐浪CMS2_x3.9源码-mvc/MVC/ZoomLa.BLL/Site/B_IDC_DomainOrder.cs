using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Site
{
   public class B_IDC_DomainOrder
    {
        public enum OrderType { Total, Detail };
        //M_IDC_DomainOrder orderModel = new M_IDC_DomainOrder();
        public string strTableName,PK;
        public DataTable dt = null;
        public B_IDC_DomainOrder() 
        {
            //strTableName = orderModel.TbName;
            //PK=orderModel.PK;
        }
        //-----------------Retrieve
        public DataTable Sel()
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName);
        }
        public DataTable SelByID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ID", id) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ID=" + id, sp);
        }
        //-----------------Insert
        //public int Insert(M_IDC_DomainOrder model)
        //{
        //    return Sql.insert(strTableName, model.GetParameters(model), model.GetParas(), model.GetFields());
        //}
        //-----------------Update
        //允许更改UserID
        public void UpdateByID(string tempName, string value, string id)
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("TempName",tempName),
                new SqlParameter("TempValue",value),
                new SqlParameter("id",id)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, "Update " + strTableName + " Set TempName=@TempName,TempValue=@TempValue Where ID=@id", sp);
        }
       /// <summary>
       /// 生成订单号
       /// </summary>
       /// <returns></returns>
        public string GenerateCodeNo() 
        {
            return "DO"+function.GetFileName();
        }
    }
}
