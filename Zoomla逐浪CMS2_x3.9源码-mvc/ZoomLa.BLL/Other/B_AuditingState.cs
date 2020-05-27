using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Data;
using ZoomLa.Model;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using System.Linq;

namespace ZoomLa.BLL
{
    public class B_AuditingState
    {
        private string TbName, PK;
        private M_AuditingState initMod = new M_AuditingState();
        public DataTable dt = null;
        IList<M_AuditingState> codeList = new List<M_AuditingState>();
        public B_AuditingState() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
            M_AuditingState mas1 = new M_AuditingState();
            mas1.StateCode = 0;
            mas1.StateName = "待审核";
            mas1.StateType = "系统";
            M_AuditingState mas2 = new M_AuditingState();
            mas2.StateCode = 99;
            mas2.StateName = "已审核";
            mas2.StateType = "系统";
            M_AuditingState mas3 = new M_AuditingState();
            mas3.StateCode = -2;
            mas3.StateName = "回收站";
            mas3.StateType = "系统";
            codeList.Add(mas1);
            codeList.Add(mas2);
            codeList.Add(mas3);
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_AuditingState SelReturnModel(int ID)
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
        private M_AuditingState SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
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
            return Sql.Sel(TbName);
        }
        /// <summary>
        /// 根据状态码，获取模型实例
        /// </summary>
        public M_AuditingState SelByStatus(int status)
        {
            return GetAuditingStateAll().First(p => p.StateCode == status);
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_AuditingState model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.StateCode.ToString(), model.GetFieldAndPara(), model.GetParameters());
        }
        public bool Update( string StateName, string StateType, string StateCode)
        {
            SqlParameter[] sp = new SqlParameter[]{ 
                new SqlParameter("StateName",StateName),
                new SqlParameter("StateType",StateType),
                new SqlParameter("StateCode",StateCode)
            };
            string strSet = "stateName=@StateName,stateType=@StateType";
            string strWhere= "stateCode=@StateCode";
            return Sql.Update(TbName, strSet, strWhere, sp);
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_AuditingState model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 创建审核状态数据访问层
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 查询所有审核状态信息
        /// </summary>
        public IList<M_AuditingState> GetAuditingStateAll()
        {
            foreach (DataRow row in Sel().Rows)
            {
                M_AuditingState mas = new M_AuditingState();
                mas.StateCode = Convert.ToInt32(row["StateCode"]);
                mas.StateName = row["StateName"].ToString();
                mas.StateType = row["StateType"].ToString();
                codeList.Add(mas);
            }
            return codeList.OrderBy(p => p.StateCode).ToList();
        }
        /// <summary>
        /// 查询所有可执行的审核状态信息
        /// </summary>
        /// <returns></returns>
        public IList<M_AuditingState> GetExecutableState()
        {
            foreach (DataRow row in Sel().Rows)
            {
                M_AuditingState mas = new M_AuditingState();
                mas.StateCode = Convert.ToInt32(row["StateCode"]);
                mas.StateName = row["StateName"].ToString();
                mas.StateType = row["StateType"].ToString();
                codeList.Add(mas);
            }
            return codeList.OrderBy(p => p.StateCode).ToList();
        }
        /// <summary>
        /// 查询所有审核通过的状态信息
        /// </summary>
        /// <returns></returns>
        public IList<M_AuditingState> GetPassState()
        {
            foreach (DataRow row in Sel().Rows)
            {
                M_AuditingState mas = new M_AuditingState();
                mas.StateCode = Convert.ToInt32(row["StateCode"]);
                mas.StateName = row["StateName"].ToString();
                mas.StateType = row["StateType"].ToString();
                codeList.Add(mas);
            }
            return codeList.OrderBy(p => p.StateCode).ToList();
        }
        /// <summary>
        /// 查询所有审核未通过的状态信息
        /// </summary>
        /// <returns></returns>
        public IList<M_AuditingState> GetNoPassState()
        {
            IList<M_AuditingState> list = new List<M_AuditingState>();
            M_AuditingState mas1 = new M_AuditingState();
            mas1.StateCode = 0;
            mas1.StateName = "待审核";
            mas1.StateType = "系统";
            M_AuditingState mas3 = new M_AuditingState();
            mas3.StateCode = -2;
            mas3.StateName = "回收站";
            mas3.StateType = "系统";
            list.Add(mas1);
            list.Add(mas3);
            foreach (DataRow row in Sel().Rows)
            {
                M_AuditingState mas = new M_AuditingState();
                mas.StateCode = Convert.ToInt32(row["StateCode"]);
                mas.StateName = row["StateName"].ToString();
                mas.StateType = row["StateType"].ToString();
                list.Add(mas);
            }
            return list;
        }
        /// <summary>
        /// 获得审核状态码
        /// </summary>
        public IList<int> GetStateCode()
        {
            IList<int> list = new List<int>();
            IList<int> list1 = new List<int>();
            foreach (DataRow row in GetStateCodeInfo().Rows)
            {
                list.Add(Convert.ToInt32(row["stateCode"].ToString()));
            }
            list.Add(0);
            list.Add(99);
            list.Add(-2);
            for (int i = -3; i < 101; i++)
            {
                bool fal = false;
                foreach (int j in list)
                {
                    if (i == j)
                    {
                        fal = false;
                        break;
                    }
                    else
                    {
                        fal = true;
                    }
                }
                if (fal)
                {

                    list1.Add(i);
                }
            }
            return list1;
        }
        #region 删除

        /// <summary>
        /// 删除审核状态信息
        /// </summary>
        /// <returns></returns>
        public bool DelAuditingStateByStateCode(M_AuditingState model)
        {
            string strSql = "delete from ZL_AuditingState where stateCode=" + model.StateCode;
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, null);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DelBystateCode(int ID)
        {
            string strSql = "delete from ZL_AuditingState where stateCode=" + ID;
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, null);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// 获取审核状态码
        /// </summary>
        /// <returns></returns>
        public DataTable GetStateCodeInfo()
        {
            string strSql = "select distinct(stateCode) from ZL_AuditingState";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 增加审核状态
        /// </summary>
        /// <param name="auditingstate"></param>
        /// <returns></returns>
        public bool AddAudtingState(M_AuditingState model)
        {
            string strSql = "insert into ZL_AuditingState(stateCode,stateName,stateType) values(@stateCode,@stateName,@stateType)";
            SqlParameter[] param = new SqlParameter[] {new SqlParameter("@stateCode",model.StateCode),
                                                       new SqlParameter("@stateName",model.StateName),
                                                       new SqlParameter("@stateType",model.StateType)};
            if (SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, param) > 0)
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