using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Exam
{
    public class B_EDU_Subject:ZL_Bll_InterFace<M_EDU_Subject>
    {
        public string PK, TbName;
        public M_EDU_Subject initmod = new M_EDU_Subject();
        public B_EDU_Subject()
        {
            PK = initmod.PK;
            TbName = initmod.TbName;
        }
        public int Insert(M_EDU_Subject model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_EDU_Subject model)
        {
            return Sql.UpdateByID(TbName, PK, model.ID, BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_EDU_Subject SelReturnModel(int ID)
        {
            using (SqlDataReader rdr=Sql.SelReturnReader(TbName,PK,ID))
            {
                if (rdr.Read())
                {
                    return initmod.GetModelFromReader(rdr);
                }
                else
                {
                    return null;
                }
            }
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
        /// <summary>
        /// 查询学科组列表
        /// </summary>
        /// <returns></returns>
        public DataTable SelByGroup()
        {
            string sql = "Select distinct Subject From "+TbName;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
    }
}
