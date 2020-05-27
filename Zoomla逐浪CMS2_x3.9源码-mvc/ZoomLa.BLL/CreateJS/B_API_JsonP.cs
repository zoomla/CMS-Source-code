using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.CreateJS;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.CreateJS
{
    public class B_API_JsonP:ZL_Bll_InterFace<M_API_JsonP>
    {
        private string PK, TbName = "";
        private M_API_JsonP initMod=new M_API_JsonP();
        public B_API_JsonP()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_API_JsonP model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_API_JsonP model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_API_JsonP SelReturnModel(int ID)
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
        public M_API_JsonP SelModelByAlias(string alias)
        {
            if (string.IsNullOrEmpty(alias.Trim())) return null;
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("alias",alias) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName," WHERE Alias=@alias AND MyState=1",sp))
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
        public System.Data.DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CDate Desc");
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM "+TbName+" WHERE ID IN ("+ids+")";
            return SqlHelper.ExecuteSql(sql);
        }
    }
}
