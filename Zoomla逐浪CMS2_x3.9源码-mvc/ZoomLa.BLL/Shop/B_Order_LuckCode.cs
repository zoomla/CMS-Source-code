using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

/*
 * 服务于云购,幸运码逻辑类
 */
namespace ZoomLa.BLL
{
    public class B_Order_LuckCode
    {
        public string TbName = "";
        public string PK = "";
        public DataTable dt = null;
        private M_Order_LuckCode model = new M_Order_LuckCode();
        public B_Order_LuckCode()
        {
            TbName = model.TbName;
            PK = model.PK;
        }
        //-----------------Insert
        public int Insert(M_Order_LuckCode model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //-----------------Retrieve
        public DataTable SelAll()
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Order By CreateTime Desc");
        }
        public M_Order_LuckCode SelReturnModel(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, "Where ID = @id", sp))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else return null;
            }
        }
        /// <summary>
        /// 获取指定商品最大的一个幸运数，便于生成幸运码,此处也是随机码的书写规范入口
        /// </summary>
        public int GetMaxLuckCode(int prodID) 
        {
            int result = 10000000;
            string sql = "Select Max(Code) as Code From "+TbName+" Where ProID="+prodID;
            dt = SqlHelper.ExecuteTable(CommandType.Text,sql);
            if (dt != null && dt.Rows.Count > 0&&dt.Rows[0]["Code"]!=DBNull.Value)
            { 
                result=Convert.ToInt32(dt.Rows[0]["Code"]);
            }
            return result;
        }
        //------------Update
        public bool UpdateModel(M_Order_LuckCode model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        //------------Del
        public bool DelByID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, "Delete " + TbName + " Where id=@id", sp) > 0);
        }
        public bool BatDelByID(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return Sql.Del(TbName, "ID in (" + ids + ")");
        }
    }
}
