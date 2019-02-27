using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Exam
{
    public class B_Publish_Node:ZL_Bll_InterFace<M_Publish_Node>
    {
        public string TbName, PK;
        public M_Publish_Node initMod = new M_Publish_Node();
        public B_Publish_Node()
        {
            this.TbName = initMod.TbName;
            this.PK = initMod.PK;
        }
        public int Insert(M_Publish_Node model)
        {
            return Sql.insert(model.TbName,model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_Publish_Node model)
        {
            return Sql.UpdateByID(model.TbName, PK, model.ID, BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_Publish_Node SelReturnModel(int ID)
        {
            using (SqlDataReader sdr=Sql.SelReturnReader(TbName,PK,ID))
            {
                if (sdr.Read())
                {
                    return initMod.GetModelFromReader(sdr);
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable SelByPid(int pid)
        {
            string sql = "Select * from " + TbName + " Where Pid=" + pid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }

        public System.Data.DataTable Sel()
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
