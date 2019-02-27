using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Site
{
    public class B_IDC_DBList
    {
        public string TbName = "";
        public string PK = "";
        public DataTable dt = null;
        private M_IDC_DBList model = new M_IDC_DBList();
        public B_IDC_DBList()
        {
            TbName = model.TbName;
            PK = model.PK;
        }
        //-----------------Insert
        public int Insert(M_IDC_DBList model)
        {
            return Sql.insert(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //-----------------Retrieve
        public DataTable SelAll()
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Order By CreateTime Desc");
        }
        public DataTable SelByKeyWord(string keyword) 
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("KeyWord", "%"+keyword+"%") };
            dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Where DBName Like @KeyWord OR UserName Like @KeyWord OR SiteName Like @KeyWord Order By CreateTime Desc",sp);
            return dt;
        }
        public DataTable SelByUserID(int userID,string keyWord="") 
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("userID", userID) };
            dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Where UserID =@userID Order By CreateTime Desc",sp);
            if (!string.IsNullOrEmpty(keyWord.Trim()))
            {
                keyWord = "'%"+keyWord+"%'";
                dt.DefaultView.RowFilter = "DBName Like " + keyWord + " OR UserName Like " + keyWord + " OR SiteName Like " + keyWord + "";
                dt = dt.DefaultView.ToTable();
            }
            return dt;
        }
        public M_IDC_DBList SelReturnModel(string id)
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
        //------------Update
        public bool UpdateModel(M_IDC_DBList model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
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
