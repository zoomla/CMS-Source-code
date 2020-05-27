using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ZoomLa.SQLDAL;
using ZoomLa.Model;
using System.Data.SqlClient;
namespace ZoomLa.BLL
{
    public class B_MisApproval
    {
        public B_MisApproval()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string PK, strTableName;
        private M_MisApproval initMod = new M_MisApproval();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_MisApproval SelReturnModel(int ID)
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
        private M_MisApproval SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
        public DataTable Sel(string Inputer = "", int Results = -100, string Approver = "", string Send = "")
        {
            string where = "1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(Inputer))
            {
                where += " and Inputer=@Inputer";
                sp.Add(new SqlParameter("Inputer", Inputer));
            }
            if (Results != -100) { where += " and Results=" + Results; }
            if (!string.IsNullOrEmpty(Approver))
            {
                where += " Approver like @Approver";
                sp.Add(new SqlParameter("Approver", "%"+Approver+"%"));
            }
            if (!string.IsNullOrEmpty(Send))
            {
                where += " Send like @Send";
                sp.Add(new SqlParameter("Send", "%"+Send+"%"));
            }
            return Sql.Sel(strTableName, where, PK + " DESC", sp.ToArray());
        }
        public bool UpdateByID(M_MisApproval model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), initMod.GetFieldAndPara(), initMod.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_MisApproval model)
        {
            return Sql.insert(strTableName, initMod.GetParameters(), initMod.GetParas(), initMod.GetFields());

        }
    }
}
