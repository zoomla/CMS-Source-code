using System;
using System.Text;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZoomLa.BLL
{
   public class B_MisSign
    {
        public B_MisSign()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string PK, strTableName;
        private M_MisSign initMod = new M_MisSign();
       public DataTable Sel(int ID)
       {
           return Sql.Sel(strTableName, PK, ID);
       }
       public M_MisSign SelReturnModel(int ID)
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
       public List<M_MisSign> GetAllLastestMemos()
       {
           string sql = String.Format("select * from {0} where [Type] = 4 order by {1} desc", strTableName, PK);
           return GetMemos(sql, CommandType.Text);
       }
       public List<M_MisSign> GetUserMemos(string uname)
       {
           string sql = String.Format("select * from {0} where [Type] = 4 and Inputer = @name order by {1} desc", strTableName, PK);
           SqlParameter pram = new SqlParameter("@name", uname);
           return GetMemos(sql, CommandType.Text, pram);
       }
        private List<M_MisSign> GetMemos(string sql, CommandType stype, params SqlParameter[] prams)
        {
            List<M_MisSign> lstMemos = new List<M_MisSign>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(stype, sql, prams))
            {
                while (reader.Read())
                {
                    lstMemos.Add(initMod.GetModelFromReader(reader));
                }
            }
            return lstMemos;
        }
        public List<M_MisSign> GetSharedMemos(string uname)
       {
           return null;
       }
       private M_MisSign SelReturnModel(string strWhere)
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
       public bool UpdateByID(M_MisSign model)
       {
           return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), initMod.GetFieldAndPara(), initMod.GetParameters());
       }
       public bool Del(int ID)
       {
           return Sql.Del(strTableName, ID);
       }
       public int insert(M_MisSign model)
       {
           return Sql.insert(strTableName, initMod.GetParameters(), initMod.GetParas(), initMod.GetFields());
       }
    }
}
