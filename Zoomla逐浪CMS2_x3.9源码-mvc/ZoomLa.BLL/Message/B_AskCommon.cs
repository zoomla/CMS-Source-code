using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.BLL
{
    public class B_AskCommon
    {
        public B_AskCommon()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string PK, strTableName;
        private M_AskCommon initMod = new M_AskCommon();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aswId"></param>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable Sel(int AswId, int Type, int UserId)
        {
            string where = string.Format("AswId={0} and type={1} and UserId={2}", AswId, Type, UserId);
            return Sql.Sel(strTableName,where,PK+" DESC");
        }
        public M_AskCommon SelReturnModel(int ID)
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
        public int getnum()
        {
            return Sql.getnum(strTableName);
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public bool UpdateByID(M_AskCommon model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), initMod.GetFieldAndPara(), initMod.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public DataTable SelfieldOrd(string strField, int num)
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "select top " + num + " " + strField + " from " + strTableName + " Group By " + strField + " Order By " + strField, null);
        }
        public int insert(M_AskCommon model)
        {
            return Sql.insert(strTableName, initMod.GetParameters(model), initMod.GetParas(), initMod.GetFields());
        }
        public DataTable U_SelByAnswer(int uid, int answerID, int type)
        {
            string where = "AswID=" + answerID + " AND Type=0 AND UserID=" + uid + " AND Type=" + type;
            return DBCenter.Sel(strTableName, where, PK + " DESC");
        }
    }
}


