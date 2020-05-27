using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.User
{
    public class B_Temp:ZoomLa.BLL.ZL_Bll_InterFace<M_Temp>
    {
        public string TbName, PK;
        public M_Temp initMod = new M_Temp();
        public B_Temp() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Temp model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Temp model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CDate Desc");
        }
        public DataTable SelByType(int usetype)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE UseType=" + usetype;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public M_Temp SelReturnModel(int ID)
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
        public M_Temp SelModelByUid(int uid,int usetype)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE UserID=" + uid + " AND UseType=" + usetype))
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
        public M_Temp SelModelByStr1(string str1)
        {
            if (string.IsNullOrEmpty(str1)) { return null; }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("str1", str1) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE str1=@str1", sp))
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
        public DataTable SelByUid(int uid) 
        {
            string sql = "SELECT * FROM "+TbName+" WHERE UserID="+uid+" ORDER BY CDate DESC";
            return SqlHelper.ExecuteTable(CommandType.Text,sql);
        }
        /// <summary>
        /// 批量确认提现
        /// </summary>
        /// <returns></returns>
        public bool CheckByIds(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Update "+TbName+" Set Str3=99 Where ID IN("+ids+") And Str3!='-1'";
            return SqlHelper.ExecuteSql(sql);
        }
    }
}
