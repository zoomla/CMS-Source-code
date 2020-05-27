using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{
    public class B_Plat_TaskMsg:ZL_Bll_InterFace<M_Plat_TaskMsg>
    {
        public string TbName, PK;
        public M_Plat_TaskMsg initMod = new M_Plat_TaskMsg();
        public B_Plat_TaskMsg() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Plat_TaskMsg model)
        {
            return Sql.insert(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_Plat_TaskMsg model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_Plat_TaskMsg SelReturnModel(int ID)
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
        public DataTable SelByTask(int taskid)
        {
            string sql = "Select A.*,B.UserFace From " + TbName + " A Left Join ZL_User_PlatView B ON A.UserID=B.UserID Where TaskID=" + taskid + " Order By CreateTime Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
    }
}
