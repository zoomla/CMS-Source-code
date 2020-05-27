using System;
using System.Data;
using System.Configuration;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_Author
    {
        private string strTableName, PK;
        private M_Author initMod = new M_Author();
        public B_Author()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Author SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public bool UpdateByID(M_Author model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Author model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_Author model)
        {
            return insert(model) > 0;
        }
        public bool Update_Author_ByID(M_Author model)
        {
            return UpdateByID(model);
        }
        public bool DeleteByID(string ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        public M_Author GetAuthorByid(int ID)
        {
            return SelReturnModel(ID);
        }
        //sql分页
        public DataTable GetAuthorPage(int Cpage, int PageSize)
        {
            string strSql = "select * from ZL_Author order by(ID) asc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        public DataTable GetSourceAll()
        {
            string strSql = "select * from ZL_Author order by(ID) asc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public int IsAuthor(int userID, DataTable authorDT)
        {
            int result = -1;
            if (authorDT == null || authorDT.Rows.Count < 1) return result;
            authorDT.DefaultView.RowFilter = "UserID = " + userID;
            if (authorDT.DefaultView.ToTable().Rows.Count > 0)
                result = DataConvert.CLng(authorDT.DefaultView.ToTable().Rows[0]["ID"]);
            return result;
        }
        /// <summary>
        /// 当前用户是否绑定了作者
        /// </summary>
        public bool IsBindAuthor(int userID)
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where UserID=" + userID).Rows.Count > 0;
        }
        public DataTable GetAuthorByName(string name)
        {
            string strSql = "Select * From " + strTableName + " where Name Like @Name";
            SqlParameter parame = new SqlParameter("Name", "%" + name + "%");
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, parame);
        }
        public bool CheckAuthorName(string name, int uid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name), new SqlParameter("uid", uid) };
            string strSql = "Select * From " + strTableName + " Where Name=@name And ID<>@uid";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
            return dt.Rows.Count > 0;
        }
    }
}
