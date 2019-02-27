using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_Process
    {
        private string strTableName, PK;
        private M_Process initMod = new M_Process();
        public B_Process()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Process SelReturnModel(int ID)
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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelByNodeID(int nodeID)
        {
            string sql = "Select a.*,b.NodeID From " + strTableName + " as a Left Join ZL_NodeBindDroit as b on a.PFlowID=b.FID Where b.NodeID=" + nodeID + " And b.Fid > 0";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 根据NodeID,获取其所绑定的工作流信息,如果无数据,则返回默认表,仅用于添加与修改文章页面
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public DataTable SelByNodeID2(int nodeID)
        {
            DataTable dt = SelByNodeID(nodeID);
            if (dt.Rows.Count < 1)
            {
                DataRow dr = dt.NewRow();
                dr["PName"] = "待审";
                dr["PPassCode"] = "0";
                DataRow dr1 = dt.NewRow();
                dr1["PName"] = "已审核";
                dr1["PPassCode"] = "99";
                DataRow dr2 = dt.NewRow();
                dr2["PName"] = "回收站";
                dr2["PPassCode"] = "-1";
                dt.Rows.Add(dr); dt.Rows.Add(dr1); dt.Rows.Add(dr2);
                dt.TableName = "Default";
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["PName"] = "待审";
                dr["PPassCode"] = "0";
                dt.Rows.InsertAt(dr, 0);
                dt.TableName = "WorkFlow";
            }
            return dt;
        }
        public bool UpdateByID(M_Process model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Process model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        #region 查询
        /// <summary>
        /// 查询已有该流程的角色
        /// </summary>
        /// <param name="flowId">流程id</param>
        /// <returns></returns>
        public IList<int> GetRoleIdByFlowId(int flowId)
        {
            IList<int> list = new List<int>();
            string strSql = "select PRole from ZL_Process where PFlowId=@flowId";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@flowId", flowId) };
            DataTable listtable = SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
            if (listtable != null)
            {
                foreach (DataRow row in listtable.Rows)
                {
                    object obj = row["PRole"];
                    if (!object.Equals(null, obj) && !object.Equals(DBNull.Value, obj))
                    {
                        char[] ch = new char[] { ',' };
                        string[] roleId = obj.ToString().Split(ch, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string id in roleId)
                        {
                            if (id != null && !"".Equals(id))
                                list.Add(Convert.ToInt32(id));
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 查询该用户通过审核的状态码
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public int GetPassCode(int NodeID, int RoleID)
        {
            int passCode = 0;
            string sql = null;
            if (RoleID == 0)
            {
                sql = "SELECT PPassCode FROM ZL_Process WHERE PFlowId =(SELECT FID FROM ZL_NodeBindDroit WHERE NodeID=@NodeID) ORDER BY PCode DESC";
            }
            else
            {
                sql = "SELECT PPassCode FROM ZL_Process WHERE PFlowId =(SELECT FID FROM ZL_NodeBindDroit WHERE NodeID=@NodeID) AND  " +
                 "PRole like '" + RoleID + "%' ORDER BY PCode DESC ";
            }

            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@NodeID", NodeID) };
            DataTable newrow = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            if (newrow != null)
            {
                foreach (DataRow row in newrow.Rows)
                {
                    object obj = row["PPassCode"];
                    if (!object.Equals(null, obj) && !object.Equals(DBNull.Value, obj))
                    {
                        passCode = Convert.ToInt32(obj);
                    }
                }
            }
            return passCode;
        }
        /// <summary>
        /// 查询该用户未通过审核的状态码
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public int GetNoPassCode(int NodeID, int RoleID)
        {
            int passCode = 0;
            string sql = null;

            if (RoleID == 0)
            {
                sql = "SELECT PNoPassCode FROM ZL_Process WHERE PFlowId =(SELECT FID FROM ZL_NodeBindDroit WHERE NodeID=@NodeID) ORDER BY PCode DESC";
            }
            else
            {
                sql = "SELECT PNoPassCode FROM ZL_Process WHERE PFlowId =(SELECT FID FROM ZL_NodeBindDroit WHERE NodeID=@NodeID) AND  " +
                 "PRole like '" + RoleID + "%' ORDER BY PCode DESC ";
            }

            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@NodeID", NodeID) };
            DataTable newrow = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            if (newrow != null)
            {
                foreach (DataRow row in newrow.Rows)
                {
                    object obj = row["PNoPassCode"];
                    if (!object.Equals(null, obj) && !object.Equals(DBNull.Value, obj))
                    {
                        passCode = Convert.ToInt32(obj);
                    }
                }
            }
            return passCode;
        }
        /// <summary>
        /// 查询该角色拥有的审核信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataTable GetProcessByRoleId(int roleId)
        {
            string strSql = "select * from ZL_Process where PRole like '@RoleID%'";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@RoleId", roleId) };
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public DataTable GetProcessByCommnetID(int generalID)
        {
            string strSql = "select top 1* from zl_process where [id]>(select [id] from zl_process where ppasscode=(select status from zl_commonmodel where generalID=@GeneralID))";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@GeneralID", generalID) };
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 查询当前步骤是否存在
        /// </summary>
        /// <param name="pcode"></param>
        /// <returns></returns>
        public DataTable GetPCodeByPCode(int pcode, int flowId)
        {
            string strSql = "select * from zl_process where pcode=@PCode and PFlowId=@PFlowId";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@PCode", pcode),
                                                      new SqlParameter("@PFlowId", flowId)};
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 查询除了本身已有该流程的角色
        /// </summary>
        /// <param name="flowId">流程id</param>
        /// <returns></returns>
        public IList<int> GetRoleIdByFlowId(int id, int flowId)
        {
            IList<int> list = new List<int>();
            string strSql = "select * from ZL_Process where PFlowId=@flowId and [id]!= @Id";
            SqlParameter[] sp = new SqlParameter[] {
                                            new SqlParameter("@Id", id),
                                            new SqlParameter("@flowId", flowId)};
            DataTable ilistable = SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
            if (ilistable != null)
            {
                foreach (DataRow row in ilistable.Rows)
                {
                    object obj = row["PRole"];
                    if (!object.Equals(null, obj) && !object.Equals(DBNull.Value, obj))
                    {
                        char[] ch = new char[] { ',' };
                        string[] roleId = obj.ToString().Split(ch);
                        foreach (string rid in roleId)
                        {
                            if (rid != null && !"".Equals(rid))
                                list.Add(Convert.ToInt32(rid));
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 查询所有步骤
        /// </summary>
        /// <returns></returns>
        public DataTable GetProcessAll()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public M_Process GetProcessById(int id)
        {
            string strSql = "select *  from ZL_Process where id=" + id;
            using (DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null))
            {
                M_Process mp = new M_Process();
                foreach (DataRow row in dt.Rows)
                {
                    mp.Id = Convert.ToInt32(row["Id"]);
                    mp.PName = row["PName"].ToString();
                    mp.PDepcit = row["PDepcit"].ToString();
                    mp.PCode = Convert.ToInt32(row["PCode"]);
                    mp.PNoPassCode = Convert.ToInt32(row["PNoPassCode"]);
                    mp.PNoPassName = row["PNoPassName"].ToString();
                    mp.PPassCode = Convert.ToInt32(row["PPassCode"]);
                    mp.PPassName = row["PPassName"].ToString();
                    mp.PRole = row["PRole"].ToString();
                    mp.PFlowId = Convert.ToInt32(row["PFlowId"]);
                }
                return mp;
            }

        }
        /// <summary>
        /// 根据流程编号查询步骤信息
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public DataTable GetProcessByFlowId(int flowId)
        {
            string strSql = "select *  from ZL_Process where  PFlowId=@PFlowId order by Pcode asc";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@PFlowId", flowId) };
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public DataTable GetPassCodeByPCodeAndRole(int pCode, int roleId)
        {
            string strSql = "select ppassCode from zl_process where pcode=@PCode and pRole like '@PRole%'";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@PCode", pCode),
                                                      new SqlParameter("@PRole", roleId)};
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        #endregion
        #region 增加
        /// <summary>
        /// 增加流程步骤
        /// </summary>
        /// <param name="mp"></param>
        /// <returns></returns>
        public bool AddProcess(M_Process mp)
        {
            if (Add_Process(mp) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region 删除
        /// <summary>
        /// 根据id删除流程步骤
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelProcessById(int id)
        {
            string strSql = "delete ZL_Process where id=" + id;
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, null);

            if (result > 0)
                return true;
            else
                return false;
        }
        #endregion
        #region 修改
        /// <summary>
        /// 根据Id编号修改流程步骤
        /// </summary>
        /// <param name="mp"></param>
        /// <returns></returns>
        //public bool ModifyProcessById(M_Process mp)
        //{
        //    //string strSql = "update  ZL_Process set PName=@PName,PDepcit=@PDepcit,PRole=@PRole,PCode=@PCode,PPassName=@PPassName,PPassCode=@PPassCode,PNoPassName=@PNoPassName,PNoPassCode=@PNoPassCode,PFlowId=@PFlowId where id=@Id";
        //    //SqlParameter[] sp = new SqlParameter[] { 
        //    //                              new SqlParameter("@PName", mp.PName),
        //    //                              new SqlParameter("@PDepcit", mp.PDepcit),
        //    //                              new SqlParameter("@PRole", mp.PRole),
        //    //                              new SqlParameter("@PCode", mp.PCode), 
        //    //                              new SqlParameter("@PPassName", mp.PPassName),
        //    //                              new SqlParameter("@PPassCode", mp.PPassCode), 
        //    //                              new SqlParameter("@PNoPassName", mp.PNoPassName),                  
        //    //                              new SqlParameter("@PNoPassCode", mp.PNoPassCode),
        //    //                              new SqlParameter("@PFlowId", mp.PFlowId),
        //    //                              new SqlParameter("@Id",mp.Id)};
        //    //int result = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, sp);

        //    //if (result > 0)
        //    //{
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //}
        #endregion
        #endregion

        /// <summary>
        /// 增加流程步骤
        /// </summary>
        /// <param name="mp"></param>
        /// <returns></returns>
        public int Add_Process(M_Process mp)
        {
            string strSql = null;
            SqlParameter[] sp = null;
            if (mp.PDepcit == null || "".Equals(mp.PDepcit))
            {
                strSql = "insert into ZL_Process(PName,PRole,PCode,PPassName,PPassCode,PNoPassName,PNoPassCode,PFlowId,NeedCode) values(@PName,@PRole,@PCode,@PPassName,@PPassCode,@PNoPassName,@PNoPassCode,@PFlowId,@NeedCode)";
                sp = new SqlParameter[] { new SqlParameter("@PName", mp.PName),
                                          new SqlParameter("@PRole", mp.PRole),
                                          new SqlParameter("@PCode", mp.PCode),
                                          new SqlParameter("@PPassName", mp.PPassName),
                                          new SqlParameter("@PPassCode", mp.PPassCode),
                                          new SqlParameter("@PNoPassName", mp.PNoPassName),
                                          new SqlParameter("@PNoPassCode", mp.PNoPassCode),
                                          new SqlParameter("@PFlowId", mp.PFlowId),
                                          new SqlParameter("@NeedCode", mp.NeedCode)};
            }
            else
            {
                strSql = "insert into ZL_Process(PName,PDepcit,PRole,PCode,PPassName,PPassCode,PNoPassName,PNoPassCode,PFlowId,NeedCode) values(@PName,@PDepcit,@PRole,@PCode,@PPassName,@PPassCode,@PNoPassName,@PNoPassCode,@PFlowId,@NeedCode)";
                sp = new SqlParameter[] { new SqlParameter("@PName", mp.PName),
                                          new SqlParameter("@PDepcit", mp.PDepcit),
                                          new SqlParameter("@PRole", mp.PRole),
                                          new SqlParameter("@PCode", mp.PCode),
                                          new SqlParameter("@PPassName", mp.PPassName),
                                          new SqlParameter("@PPassCode", mp.PPassCode),
                                          new SqlParameter("@PNoPassName", mp.PNoPassName),
                                          new SqlParameter("@PNoPassCode", mp.PNoPassCode),
                                          new SqlParameter("@PFlowId", mp.PFlowId),
                                          new SqlParameter("@NeedCode", mp.NeedCode)};
            }
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 更新文章的状态码为下一步状态
        /// </summary>
        public void UpdateStatus()
        {

        }
    }
}
