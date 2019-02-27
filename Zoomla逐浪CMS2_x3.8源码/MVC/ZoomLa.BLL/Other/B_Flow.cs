using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_Flow
    {
        private string PK, strTableName;
        private M_Flow initMod = new M_Flow();
        public B_Flow()
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Flow SelReturnModel(int ID)
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
        private M_Flow SelReturnModel(string strWhere)
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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public bool UpdateByID(M_Flow model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Flow model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public DataTable GetFlowAll()
        {
            return Sel();
        }
        public M_Flow GetFlowById(int id)
        {
            foreach (DataRow row in Sel(id).Rows)
            {
                M_Flow mf = new M_Flow();
                mf.Id = Convert.ToInt32(row["id"]);
                mf.FlowName = row["flowName"].ToString();
                mf.FlowDepict = row["flowDepict"].ToString();
                return mf;
            }
            return null;
        }
        /// <summary>
        /// 添加流程
        /// </summary>
        /// <param name="flowName"></param>
        /// <param name="flowDepict"></param>
        /// <returns></returns>
        public bool AddFlow(string flowName, string flowDepict)
        {
            string strSql = "insert into ZL_Flow(flowName,flowDepict) values(@flowName,@flowDepict)";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@flowName", flowName),
                                                    new SqlParameter("@flowDepict",flowDepict) };
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, sp);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DelFlowById(int id)
        {
            string strSql = "delete ZL_Flow where id=@id";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@id", id) };
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, sp);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ModifyFlowById(M_Flow flows)
        {
            string strSql = "update ZL_Flow set flowName=@flowName,flowDepict=@flowDepict where id=@id";
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("@flowName",flows.FlowName),
                                                    new SqlParameter("@flowDepict",flows.FlowDepict),
                                                    new SqlParameter("@id",flows.Id)};
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, sp);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
