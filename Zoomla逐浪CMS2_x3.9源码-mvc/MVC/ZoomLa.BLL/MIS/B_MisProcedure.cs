using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZoomLa.BLL
{
   public class B_MisProcedure
    {
        public string strTableName,PK ;
        public DataTable dt = null;
        public M_MisProcedure model = new M_MisProcedure();

        public B_MisProcedure() 
        {
            strTableName = model.TbName;
            PK = model.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_MisProcedure SelReturnModel(int ID)
        {
            if (ID == 0) 
            {
                M_MisProcedure freeMod = new M_MisProcedure();
                freeMod.TypeID = (int)M_MisProcedure.ProTypes.Free;
                freeMod.ProcedureName = "自由流程";
                return freeMod;
            }
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public List<M_MisProcedure> GetAllLastestMemos()
        {
            string sql = String.Format("select * from {0} where [Type] = 4 order by {1} desc", strTableName, PK);
            return GetMemos(sql, CommandType.Text);
        }
        public List<M_MisProcedure> GetUserMemos(string uname)
        {
            string sql = String.Format("select * from {0} where [Type] = 4 and Inputer = @name order by {1} desc", strTableName, PK);
            SqlParameter pram = new SqlParameter("@name", uname);
            return GetMemos(sql, CommandType.Text, pram);
        }
        public List<M_MisProcedure> GetSharedMemos(string uname)
        {
            return null;
        }
        public List<M_MisProcedure> GetMemos(string sql, CommandType stype, params SqlParameter[] prams)
        {
            List<M_MisProcedure> lstMemos = new List<M_MisProcedure>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(stype, sql, prams))
            {
                while (reader.Read())
                {
                    lstMemos.Add(model.GetModelFromReader(reader));
                }
            }
            return lstMemos;
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable Sel(string strWhere, string strOrderby,SqlParameter[] sp)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby,sp);
        }
        public DataTable Sel(string strWhere, string strOrderby)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby);
        }
        /// <summary>
        /// 获取用户有权限发起的流程
        /// </summary>
        public DataTable SelByUser(int userID,int GroupID)
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where Sponsor Like '%," + userID + ",%' OR SponsorGroup Like '%," + GroupID + ",%' OR ((Sponsor is null OR Sponsor ='') And (SponsorGroup is null OR SponsorGroup =''))");
        }
        public bool UpdateByID(M_MisProcedure model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }
        public bool Update(string strSet, string strWhere,SqlParameter[] sp)
        {
            return Sql.Update(strTableName, strSet, strWhere, sp);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
        public int insert(M_MisProcedure Mpro)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
       //---------------Tool
       /// <summary>
       /// 用户是否有发起权限
       /// </summary>
       public bool CheckSponsor(int proID, int userID)
        {
            bool flag = false;
            model = SelReturnModel(proID);
            if (string.IsNullOrEmpty(model.Sponsor) || model.Sponsor.Contains("," + userID + ","))
            {
                flag = true;
            }
            return flag;
        }
    }
}
