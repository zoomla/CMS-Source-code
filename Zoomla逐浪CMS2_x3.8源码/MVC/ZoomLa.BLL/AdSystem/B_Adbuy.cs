using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model.AdSystem;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.AdSystem
{
    public class B_Adbuy
    {
        private string strTableName, PK;
        private M_Adbuy initMod = new M_Adbuy();
        public B_Adbuy()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable SelectByID(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        public DataTable SelectByID(M_Adbuy mAdbuy)
        {
            string strSql = "select * from [ZL_Adbuy] where ID=@ID";
            SqlParameter[] sp = mAdbuy.GetParameters();
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public M_Adbuy Select(int ID)
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
        private M_Adbuy SelReturnModel(string strWhere)
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
        public bool Update(M_Adbuy model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Delete(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Adbuy model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int Add(M_Adbuy mAdbuy)
        {
            string strSql = "INSERT INTO ZL_Adbuy(ADID,UID,Time,ShowTime,Scale,Ptime,Content,Files,State,Price,Audit)";
            strSql += "values(@ADID,@UID,@Time,@ShowTime,@Scale,@Ptime,@Content,@Files,@State,@Price,@Audit)";
            SqlParameter[] sp = mAdbuy.GetParameters();
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
        /// <summary>
        /// 查询所有记录
        /// </summary> 
        public DataTable SelectAdbuy()
        {
            return Sql.Sel(strTableName);
        }
        public M_Adbuy SelectId(int id)
        {
            using (SqlDataReader reader = Sql.SelReturnReader("ZL_Adbuy", "ID", id))
            {
                if (reader.Read())
                {
                    return new M_Adbuy().GetModelFromReader(reader);
                }
                else
                {
                    return new M_Adbuy();
                }
            }
        }
        public static bool Adbuy_SetAudit(string strId)
        {
            SafeSC.CheckIDSEx(strId);
            return SqlHelper.ExecuteSql("update ZL_Adbuy set Audit=1 where ID  in (" + strId + ")");
        }
        public static bool Adbuy_CancelAudit(string strId)
        {
            SafeSC.CheckIDSEx(strId);
            return SqlHelper.ExecuteSql("update ZL_Adbuy set Audit=0 where ID in (" + strId + ")");
        }
        public static bool Adbuy_Delete(string strId)
        {
            string strSql = "delete from ZL_Adbuy where ID=" + Convert.ToInt32(strId);
            return SqlHelper.ExecuteSql(strSql);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}