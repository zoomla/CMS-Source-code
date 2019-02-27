using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Content
{
    public class B_Pub_Excel : ZL_Bll_InterFace<M_Pub_Excel>
    {
        private string TbName, PK;
        private M_Pub_Excel initMod = new M_Pub_Excel();
        public B_Pub_Excel()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }

        public int Insert(M_Pub_Excel model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public bool UpdateByID(M_Pub_Excel model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_Pub_Excel SelReturnModel(int ID)
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
        public M_Pub_Excel SelByTbName(string tbname)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("TableName",tbname) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " Where TableName=@TableName", sp))
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
