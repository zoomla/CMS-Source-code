using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_User_Temp : ZL_Bll_InterFace<M_User_Temp>
    {
        //UseType
        //1,邀请码
        string TbName, PK;
        M_User_Temp initMod = new M_User_Temp();
        public B_User_Temp()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_User_Temp model)
        {
            return DBCenter.Insert(model);
        }

        public bool UpdateByID(M_User_Temp model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool DelByUid(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Delete From " + TbName + " Where ID in(" + ids + ") And UserID=" + uid;
            return SqlHelper.ExecuteSql(sql);
        }

        public M_User_Temp SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
        public bool UpdateByField(int id, string fname, string fvalue)
        {
            SafeSC.CheckDataEx(fname);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("fvalue", fvalue) };
            string sql = "Update " + TbName + " Set " + fname + " =@fvalue Where ID=" + id;
            return SqlHelper.ExecuteSql(sql, sp);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
