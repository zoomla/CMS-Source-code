using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_MisProcedure
    {
        public string strTableName, PK;
        private M_MisProcedure model = new M_MisProcedure();

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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        /// <summary>
        /// 获取用户有权限发起的流程
        /// </summary>
        public DataTable SelByUser(int userID, int GroupID)
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where Sponsor Like '%," + userID + ",%' OR SponsorGroup Like '%," + GroupID + ",%' OR ((Sponsor is null OR Sponsor ='') And (SponsorGroup is null OR SponsorGroup =''))");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public int insert(M_MisProcedure model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_MisProcedure model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
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
