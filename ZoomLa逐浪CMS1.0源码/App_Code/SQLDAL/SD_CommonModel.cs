using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using ZoomLa.IDAL;
using ZoomLa.Model;
using System.Data.SqlClient;
using ZoomLa.Common;
namespace ZoomLa.SQLDAL
{
    /// <summary>
    /// SD_CommonModel 的摘要说明
    /// </summary>
    public class SD_CommonModel : ID_CommonModel
    {
        public SD_CommonModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region ID_CommonModel 成员
        /// <summary>
        /// 读取内容列表
        /// </summary>
        /// <param name="NodelID"></param>
        /// <param name="PageSize"></param>
        /// <param name="CPage"></param>
        /// <returns></returns>
        DataTable ID_CommonModel.ContentList(int NodeID, string flag)
        {
            string strSql = "select * from ZL_CommonModel";
            string filter = "";
            if (!string.IsNullOrEmpty(flag))
            {
                if (flag == "Audit")
                    filter = "Status=99";
                else
                {
                    if (flag == "Elite")
                        filter = "EliteLevel>0";
                    else
                        filter = "Status<>99 and Status<>-2";
                }
            }
            else
            {
                filter = "Status<>-2";
            }

            if (NodeID > 0)
                filter = filter + " and (NodeID=" + NodeID + " or NodeID in (select NodeID from ZL_Node where ParentID="+NodeID+"))";
            strSql += " Where " + filter + "Order by GeneralID Desc";

            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        int ID_CommonModel.GetListCount(int NodeID, string flag)
        {
            string strSql = "select Count(GeneralID) from ZL_CommonModel";
            string filter = "";
            if (!string.IsNullOrEmpty(flag))
            {
                if (flag == "Audit")
                    filter = "Status=99";
                else
                {
                    if (flag == "Elite")
                        filter = "EliteLevel>0";
                    else
                        filter = "Status<>99 and Status<>-2";
                }
            }
            else
            {
                filter = "Status<>-2";
            }
            if (NodeID > 0)
                filter = filter + " and NodeID=" + NodeID;
            strSql += " Where " + filter;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, null));
        }
        /// <summary>
        /// 添加内容到数据库
        /// </summary>
        /// <param name="FieldList"></param>
        /// <param name="FieldValue"></param>
        /// <param name="CommonData"></param>
        int ID_CommonModel.AddContent(string FieldList, string FieldValue, M_CommonData CommonData)
        {
            string strSql = "Insert Into " + CommonData.TableName + " (" + FieldList + ") values(" + FieldValue + ");select @@IDENTITY AS newID";
            //SqlHelper.ExecuteSql(strSql);            
            int ItemID = SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, null));
            string strSql2 = "Insert Into ZL_CommonModel (NodeID,ModelID,ItemID,TableName,Title,Inputer,EliteLevel,InfoID,SpecialID,Template,Status) Values (" + CommonData.NodeID + "," + CommonData.ModelID + "," + ItemID + ",'" + CommonData.TableName + "','" + CommonData.Title + "','" + CommonData.Inputer + "'," + CommonData.EliteLevel + ",'" + CommonData.InfoID + "','" + CommonData.SpecialID + "','" + CommonData.Template + "'," + CommonData.Status + ")";
            SqlHelper.ExecuteSql(strSql2);
            return ItemID;
        }
        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="FieldSet"></param>
        /// <param name="CommonData"></param>
        void ID_CommonModel.UpdateContent(string FieldSet, M_CommonData CommonData)
        {
            string strSql = "Update " + CommonData.TableName + " Set " + FieldSet + " Where [ID]=" + CommonData.ItemID;
            SqlHelper.ExecuteSql(strSql);
            string strSql2 = "Update ZL_CommonModel Set Title='" + CommonData.Title + "',EliteLevel=" + CommonData.EliteLevel + ",InfoID='" + CommonData.InfoID + "',SpecialID='" + CommonData.SpecialID + "',Template='" + CommonData.Template + "' Where GeneralID=" + CommonData.GeneralID;
            SqlHelper.ExecuteSql(strSql2);
        }
        /// <summary>
        /// 彻底删除内容数据
        /// </summary>
        /// <param name="CommonData"></param>
        void ID_CommonModel.DelContent(M_CommonData CommonData)
        {
            string strSql = "PR_Content_Del";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@TableName",SqlDbType.NVarChar),
                new SqlParameter("@ItemID",SqlDbType.Int),
                new SqlParameter("@GeneralID",SqlDbType.Int)
            };
            cmdParam[0].Value = CommonData.TableName;
            cmdParam[1].Value = CommonData.ItemID;
            cmdParam[2].Value = CommonData.GeneralID;
            SqlHelper.ExecuteProc(strSql, cmdParam);
        }
        bool ID_CommonModel.MoveContent(string ContentIDS, int NodeID)
        {
            string strSql = "Update ZL_CommonModel set NodeID=@NodeID where GeneralID in ("+ContentIDS+")";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@NodeID",SqlDbType.Int)
            };
            sp[0].Value = NodeID;
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// 获取内容信息不含公用信息
        /// </summary>
        /// <param name="GeneralID"></param>
        /// <returns></returns>
        DataTable ID_CommonModel.GetContent(M_CommonData CommonData)
        {
            string strSql = "PR_Content_Read";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@TableName",SqlDbType.NVarChar),
                new SqlParameter("@ItemID",SqlDbType.Int)
            };
            cmdParam[0].Value = CommonData.TableName;
            cmdParam[1].Value = CommonData.ItemID;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, strSql, cmdParam);
        }
        /// <summary>
        /// 获取内容公用信息
        /// </summary>
        /// <param name="GeneralID"></param>
        /// <returns></returns>
        M_CommonData ID_CommonModel.GetCommonData(int GeneralID)
        {
            string strSql = "PR_Content_Common";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@GeneralID",SqlDbType.Int)
            };
            cmdParam[0].Value = GeneralID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, strSql, cmdParam))
            {
                if (reader.Read())
                {
                    M_CommonData info = new M_CommonData();
                    info.GeneralID = GeneralID;
                    info.NodeID = DataConverter.CLng(reader["NodeID"]);
                    info.ModelID = DataConverter.CLng(reader["ModelID"]);
                    info.ItemID = DataConverter.CLng(reader["ItemID"]);
                    info.TableName = reader["TableName"].ToString();
                    info.Title = reader["Title"].ToString();
                    info.Inputer = reader["Inputer"].ToString();
                    info.EliteLevel = DataConverter.CLng(reader["EliteLevel"]);
                    info.InfoID = reader["InfoID"].ToString();
                    info.SpecialID = reader["SpecialID"].ToString();
                    info.IsCreate = DataConverter.CLng(reader["IsCreate"].ToString());
                    info.HtmlLink = reader["HtmlLink"].ToString();
                    info.Hits = DataConverter.CLng(reader["Hits"].ToString());
                    info.Template = reader["Template"].ToString();
                    info.Status = DataConverter.CLng(reader["Status"].ToString());
                    return info;
                }
                else
                    return new M_CommonData(true);
            }
        }
        /// <summary>
        /// 读取回收站内的内容列表
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="CPage"></param>
        /// <returns></returns>
        DataTable ID_CommonModel.ContentRecycle(int NodeID)
        {
            string strSql = "select * from ZL_CommonModel where Status=-2";
            if (NodeID != 0)
                strSql += " And NodeID=" + NodeID.ToString();
            strSql += " order by GeneralID DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 搜索内容
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="PageSize"></param>
        /// <param name="CPage"></param>
        /// <returns></returns>
        DataSet ID_CommonModel.ContentSearch(string filter, int PageSize, int CPage)
        {
            string strSql = "PR_GetRecordFromPage";
            SqlParameter[] cmdParam = new SqlParameter[] { 
                new SqlParameter("@TableName",SqlDbType.VarChar),               //表名，可以是多个表 
                new SqlParameter("@Identity",SqlDbType.VarChar),
                new SqlParameter("@Fields",SqlDbType.VarChar),          //要取出的字段，可以是多个表的字段，可以为空，为空表示select *
                new SqlParameter("@sqlWhere",SqlDbType.VarChar),        //条件，可以为空，不用填 where                        
                new SqlParameter("@OrderField",SqlDbType.VarChar),      //排序字段，可以为空，为空默认按主键升序排列，不用填 order by
                new SqlParameter("@pageSize",SqlDbType.Int),            //每页记录数
                new SqlParameter("@pageIndex",SqlDbType.Int)            //当前页，0表示第1页
            };
            cmdParam[0].Value = "ZL_CommonModel";
            cmdParam[1].Value = "GeneralID";
            cmdParam[2].Value = "*";
            cmdParam[3].Value = filter;
            cmdParam[4].Value = "GeneralID DESC";
            cmdParam[5].Value = PageSize;
            cmdParam[6].Value = CPage - 1;
            return SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, strSql, cmdParam);
        }
        /// <summary>
        /// 搜索符合条件数据总数
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int ID_CommonModel.CountSearch(string filter)
        {
            string strSql = "select count(GeneralID) from ZL_CommonModel where " + filter;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, null));
        }
        /// <summary>
        /// 将内容设定删除标志放入回收站
        /// </summary>
        /// <param name="GeneralID"></param>
        void ID_CommonModel.SetDel(int GeneralID)
        {
            string strSql = "PR_Content_SetDel";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@GeneralID",SqlDbType.Int)
            };
            cmdParam[0].Value = GeneralID;
            SqlHelper.ExecuteProc(strSql, cmdParam);
        }

        void ID_CommonModel.SetAudit(int GeneralID)
        {
            string strSql = "PR_Content_SetAudit";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@GeneralID",SqlDbType.Int)
            };
            cmdParam[0].Value = GeneralID;
            SqlHelper.ExecuteProc(strSql, cmdParam);
        }

        void ID_CommonModel.Reset(int GeneralID)
        {
            string strSql = "PR_Content_Reset";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@GeneralID",SqlDbType.Int)
            };
            cmdParam[0].Value = GeneralID;
            SqlHelper.ExecuteProc(strSql, cmdParam);
        }
        void ID_CommonModel.ResetAll()
        {
            string strSql = "PR_Content_ResetAll";
            
            SqlHelper.ExecuteProc(strSql, null);
        }        
        DataTable ID_CommonModel.GetCreateAllList()
        {
            string strSql = "PR_Content_CreateAll";
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, strSql, null);
        }

        DataTable ID_CommonModel.GetCreateIDList(int startID, int endID)
        {
            string strSql = "PR_Content_CreateID";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@startID",SqlDbType.Int),
                new SqlParameter("@endID",SqlDbType.Int)
            };
            cmdParam[0].Value = startID;
            cmdParam[1].Value = endID;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, strSql, cmdParam);
        }

        DataTable ID_CommonModel.GetCreateDateList(DateTime startTime, DateTime endTime)
        {
            string strSql = "PR_Content_CreateDate";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@startTime",SqlDbType.DateTime),
                new SqlParameter("@endTime",SqlDbType.DateTime)
            };
            cmdParam[0].Value = startTime;
            cmdParam[1].Value = endTime;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, strSql, cmdParam);
        }

        DataTable ID_CommonModel.GetCreateNodeList(string NodeArr)
        {
            string strSql = "select * from ZL_CommonModel Where NodeID in (" + NodeArr + ") order by GeneralID Desc";

            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        } 

        DataTable ID_CommonModel.GetCreateCountList(int Count)
        {
            string strSql = "PR_Content_CreateCount";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@Count",SqlDbType.Int)
            };
            cmdParam[0].Value = Count;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, strSql, cmdParam);
        }


        void ID_CommonModel.UpdateCreate(int GeneralID, string Template)
        {
            string strSql = "Update ZL_CommonModel Set IsCreate=1,HtmlLink=@Template Where GeneralID=@GeneralID";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@Template",SqlDbType.NVarChar),
                new SqlParameter("@GeneralID",SqlDbType.Int)
            };
            cmdParam[0].Value = Template;
            cmdParam[1].Value = GeneralID;
            SqlHelper.ExecuteSql(strSql, cmdParam);
        }

        DataTable ID_CommonModel.ContentListUser(int NodeID, string flag, string inputer)
        {
            string strSql = "Select * from ZL_CommonModel Where Inputer='"+inputer+"'";
            string sqlWhere = "";
            if (NodeID != 0)
                sqlWhere = sqlWhere + " and NodeID=" + NodeID;
            if (!string.IsNullOrEmpty(flag))
            {
                if (flag == "UnAudit")
                    sqlWhere = sqlWhere + " and Status=0";
                if(flag=="Audit")
                    sqlWhere = sqlWhere + " and Status=99";
                if (flag == "Reject")
                    sqlWhere = sqlWhere + " and Status=-1";
            }
            strSql = strSql + sqlWhere + " Order by GeneralID Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }

        int ID_CommonModel.GetPreID(int InfoID,int NodeID)
        {
            string strSql = "select top 1 GeneralID from ZL_CommonModel where NodeID=@NodeID and GeneralID<@InfoID order by GeneralID Desc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@NodeID",SqlDbType.Int,4),
                new SqlParameter("@InfoID",SqlDbType.Int,4)
            };
            cmdParam[0].Value = NodeID;
            cmdParam[1].Value = InfoID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }

        int ID_CommonModel.GetNextID(int InfoID, int NodeID)
        {
            string strSql = "select top 1 GeneralID from ZL_CommonModel where NodeID=@NodeID and GeneralID>@InfoID order by GeneralID Asc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@NodeID",SqlDbType.Int,4),
                new SqlParameter("@InfoID",SqlDbType.Int,4)
            };
            cmdParam[0].Value = NodeID;
            cmdParam[1].Value = InfoID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }

        void ID_CommonModel.UpHits(int GeneralID)
        {
            string strSql = "Update ZL_CommonModel set Hits=Hits+1 where GeneralID=@InfoID";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@InfoID",SqlDbType.Int,4)
            };
            cmdParam[0].Value = GeneralID;
            SqlHelper.ExecuteSql(strSql, cmdParam);
        }

        #endregion
    }
}