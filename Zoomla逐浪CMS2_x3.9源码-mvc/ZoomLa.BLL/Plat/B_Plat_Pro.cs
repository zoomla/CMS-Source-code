using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Plat;
using System.Data.SqlClient;

namespace ZoomLa.BLL.Plat
{
    public class B_Plat_Pro : ZL_Bll_InterFace<M_Plat_Pro>
    {
        string TbName, PK;
        M_Plat_Pro initMod = new M_Plat_Pro();
        public B_Plat_Pro()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Plat_Pro model)
        {
            return Sql.insert(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_Plat_Pro model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_Plat_Pro SelReturnModel(int ID)
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

        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "");
        }
        /// <summary>
        /// 是否有指定的权限
        /// </summary>
        public bool HasAuth(int uid, int proid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("suid", "%," + uid + ",%") };
            string sql = "Select UserID From " + TbName + " Where LeaderIDS Like @suid OR ParterIDS Like @suid OR UserID=" + uid + " And ID=" + proid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp).Rows.Count > 0;
        }
    }
}
