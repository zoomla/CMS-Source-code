namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.Common;

    /// <summary>
    /// 节点访问的Sql Server操作方法
    /// </summary>
    public class SD_Node : ID_Node
    {
        public SD_Node()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region ID_Node 成员

        int ID_Node.AddNode(M_Node NodeInfo)
        {
            string strSql = "PR_Nodes_Add";
            SqlParameter[] cmdParams = GetParameters(NodeInfo);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure,strSql,cmdParams));
        }

        bool ID_Node.DelNode(int NodeID)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@NodeID", SqlDbType.Int,4) };
            cmdParams[0].Value = NodeID;
            return (SqlHelper.ExecuteProc("PR_Node_Del", cmdParams));
        }

        void ID_Node.DelChildNode(int ParentID)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@NodeID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ParentID;
            SqlHelper.ExecuteProc("PR_Node_DelChild", cmdParams);
        }

        void ID_Node.SetChild(int ParentID)
        {
            string strSql = "Update ZL_Node set Child=Child-1 where NodeID=@ParentID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ParentID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ParentID;
            SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, cmdParams);
        }

        bool ID_Node.UpdateNode(M_Node NodeInfo)
        {
            string strSql = "PR_Nodes_Update";
            SqlParameter[] cmdParams = GetParameters(NodeInfo);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        M_Node ID_Node.GetNode(int NodeID)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@NodeID", SqlDbType.Int,4) };
            cmdParams[0].Value = NodeID;
            string strSql = "select * from ZL_Node where NodeID=@NodeID";
            using (SqlDataReader drd = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (drd.Read())
                {
                    return GetNodeFromReader(drd);
                }
                else
                {
                    return new M_Node(true);
                }
            }            
        }
        int ID_Node.GetFirstNode(int ParentID)
        {
            string strSql = "select Min(NodeID) from ZL_Node where ParentID=@ParentID and NodeType=1";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ParentID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ParentID;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams));
        }
        int ID_Node.GetMaxOrder(int ParentID)
        {
            string strSql = "select Max(OrderID) from ZL_Node where ParentID=@ParentID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ParentID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ParentID;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams));
        }
        int ID_Node.GetMinOrder(int ParentID)
        {
            string strSql = "select Min(OrderID) from ZL_Node where ParentID=@ParentID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ParentID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ParentID;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams));
        }
        int ID_Node.GetPreNode(int ParentID, int CurrentID)
        {
            string strSql = "select top 1 NodeID from ZL_Node where ParentID=@ParentID and OrderID<@CurrentID order by OrderId Desc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@ParentID",SqlDbType.Int),
                new SqlParameter("@CurrentID",SqlDbType.Int)
            };
            cmdParam[0].Value = ParentID;
            cmdParam[1].Value = CurrentID;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }
        int ID_Node.GetNextNode(int ParentID, int CurrentID)
        {
            string strSql = "select top 1 NodeID from ZL_Node where ParentID=@ParentID and OrderID>@CurrentID order by OrderId Asc";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@ParentID",SqlDbType.Int),
                new SqlParameter("@CurrentID",SqlDbType.Int)
            };
            cmdParam[0].Value = ParentID;
            cmdParam[1].Value = CurrentID;
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParam));
        }
        int ID_Node.GetDepth(int ParentID)
        {
            int Depth=0;
            if (ParentID == 0)
                Depth = 1;
            else
            {
                string strSql = "select Depth from ZL_Node where NodeID=@ParentID";
                SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ParentID", SqlDbType.Int, 4) };
                cmdParams[0].Value = ParentID;
                Depth = DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, cmdParams))+1;
            }
            return Depth;
        }
        void ID_Node.SetChildAdd(int ParentID)
        {
            string strSql = "Update ZL_Node set Child=Child+1 where NodeID=@ParentID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ParentID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ParentID;
            SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, cmdParams);
        }
        DataSet ID_Node.GetNodeList(int ParentID)
        {
            string strSql = "select * from ZL_Node where ParentID=@ParentID Order by OrderID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ParentID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ParentID;
            return SqlHelper.ExecuteDataSet(CommandType.Text, strSql, cmdParams);
        }
        DataSet ID_Node.GetNodeListContain(int ParentID)
        {
            string strSql = "select * from ZL_Node where ParentID=@ParentID and NodeType=1 Order by OrderID";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@ParentID", SqlDbType.Int, 4) };
            cmdParams[0].Value = ParentID;
            return SqlHelper.ExecuteDataSet(CommandType.Text, strSql, cmdParams);
        }
        DataTable ID_Node.GetSingleList()
        {
            string strSql = "select * from ZL_Node where NodeType=2 Order by OrderID";            
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        DataTable ID_Node.GetCreateAllList()
        {
            string strSql = "select * from ZL_Node where NodeType=1 Order by OrderID";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        DataTable ID_Node.GetCreateListByID(string IDArr)
        {
            string strSql = "select * from ZL_Node where NodeType=1 and NodeID in ("+IDArr+") Order by OrderID";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        DataTable ID_Node.GetCreateSingleByID(string IDArr)
        {
            string strSql = "select * from ZL_Node where NodeType=2 and NodeID in (" + IDArr + ") Order by OrderID";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        void ID_Node.AddModelTemplate(int NodeID, int ModelID, string ModelTemplate)
        {
            string strSql = "Insert into ZL_Node_ModelTemplate (NodeID,ModelID,Template) values(@NodeID,@ModelID,@Template)";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@NodeID",SqlDbType.Int),
                new SqlParameter("@ModelID",SqlDbType.Int),
                new SqlParameter("@Template",SqlDbType.NVarChar,255)
            };
            sp[0].Value = NodeID;
            sp[1].Value = ModelID;
            sp[2].Value = ModelTemplate;
            SqlHelper.ExecuteSql(strSql, sp);
        }
        void ID_Node.DelModelTemplate(int NodeID, string ModelIDArr)
        {
            string strSql = "Delete from ZL_Node_ModelTemplate where NodeID=@NodeID and ModelID not in ("+ModelIDArr+")";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@NodeID",SqlDbType.Int)
            };
            sp[0].Value = NodeID;
            SqlHelper.ExecuteSql(strSql, sp);
        }
        bool ID_Node.IsExistTemplate(int NodeID, int ModelID)
        {
            string strSql = "select count(*) from ZL_Node_ModelTemplate where NodeID=@NodeID and ModelID=@ModelID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@NodeID",SqlDbType.Int),
                new SqlParameter("@ModelID",SqlDbType.Int)
            };
            sp[0].Value = NodeID;
            sp[1].Value = ModelID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp)) > 0;
        }
        void ID_Node.UpdateModelTemplate(int NodeID, int ModelID, string ModelTemplate)
        {
            string strSql = "Update ZL_Node_ModelTemplate set Template=@Template where NodeID=@NodeID and ModelID=@ModelID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@NodeID",SqlDbType.Int),
                new SqlParameter("@ModelID",SqlDbType.Int),
                new SqlParameter("@Template",SqlDbType.NVarChar,255)
            };
            sp[0].Value = NodeID;
            sp[1].Value = ModelID;
            sp[2].Value = ModelTemplate;
            SqlHelper.ExecuteSql(strSql, sp);
        }
        void ID_Node.DelTemplate(int NodeID)
        {
            string strSql = "Delete from ZL_Node_ModelTemplate where NodeID=@NodeID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@NodeID",SqlDbType.Int)
            };
            sp[0].Value = NodeID;
            SqlHelper.ExecuteSql(strSql, sp);
        }
        string ID_Node.GetModelTemplate(int NodeID, int ModelID)
        {
            string strSql = "select Template from ZL_Node_ModelTemplate where NodeID=@NodeID and ModelID=@ModelID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@NodeID",SqlDbType.Int),
                new SqlParameter("@ModelID",SqlDbType.Int)
            };
            sp[0].Value = NodeID;
            sp[1].Value = ModelID;
            using(SqlDataReader rdr=SqlHelper.ExecuteReader(CommandType.Text,strSql,sp))
            {
                if (rdr.Read())
                {
                    return rdr["Template"].ToString();
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        #endregion
        /// <summary>
        /// 将节点信息的各属性值传递到参数中
        /// </summary>
        /// <param name="administratorInfo"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_Node Nodeinfo)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@NodeID", SqlDbType.Int, 4),
                new SqlParameter("@NodeName", SqlDbType.NVarChar, 50),
                new SqlParameter("@NodeType", SqlDbType.Int, 4),
                new SqlParameter("@Tips", SqlDbType.NVarChar, 255),
                new SqlParameter("@NodeDir", SqlDbType.NVarChar, 50),
                new SqlParameter("@NodeUrl", SqlDbType.NVarChar, 255),
                new SqlParameter("@ParentID", SqlDbType.Int, 4),
                new SqlParameter("@Child", SqlDbType.Int, 4),
                new SqlParameter("@Depth", SqlDbType.Int, 4),
                new SqlParameter("@NodePicUrl", SqlDbType.NVarChar, 255),
                new SqlParameter("@Description", SqlDbType.NVarChar, 255),
                new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 255),
                new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 255),
                new SqlParameter("@OpenType", SqlDbType.Bit, 1),
                new SqlParameter("@PurviewType", SqlDbType.Bit, 1),
                new SqlParameter("@CommentType", SqlDbType.Bit, 1),
                new SqlParameter("@HitsOfHot", SqlDbType.Int, 4),
                new SqlParameter("@ListTemplateFile", SqlDbType.NVarChar, 255),
                new SqlParameter("@IndexTemplate", SqlDbType.NVarChar, 255),
                new SqlParameter("@ContentModel", SqlDbType.NVarChar, 255),
                new SqlParameter("@ItemOpenType", SqlDbType.Bit, 1),
                new SqlParameter("@ContentHtmlRule", SqlDbType.Int, 4),
                new SqlParameter("@ListPageHtmlEx", SqlDbType.Int, 4),
                new SqlParameter("@ContentFileEx", SqlDbType.Int, 4),
                new SqlParameter("@OrderID", SqlDbType.Int, 4)
            };
            parameter[0].Value = Nodeinfo.NodeID;
            parameter[1].Value = Nodeinfo.NodeName;
            parameter[2].Value = Nodeinfo.NodeType;
            parameter[3].Value = Nodeinfo.Tips;
            parameter[4].Value = Nodeinfo.NodeDir;
            parameter[5].Value = Nodeinfo.NodeUrl;
            parameter[6].Value = Nodeinfo.ParentID;
            parameter[7].Value = Nodeinfo.Child;
            parameter[8].Value = Nodeinfo.Depth;
            parameter[9].Value = Nodeinfo.NodePic;
            parameter[10].Value = Nodeinfo.Description;
            parameter[11].Value = Nodeinfo.Meta_Keywords;
            parameter[12].Value = Nodeinfo.Meta_Description;
            parameter[13].Value = Nodeinfo.OpenNew;
            parameter[14].Value = Nodeinfo.PurviewType;
            parameter[15].Value = Nodeinfo.CommentType;
            parameter[16].Value = Nodeinfo.HitsOfHot;
            parameter[17].Value = Nodeinfo.ListTemplateFile;
            parameter[18].Value = Nodeinfo.IndexTemplate;
            parameter[19].Value = Nodeinfo.ContentModel;
            parameter[20].Value = Nodeinfo.ItemOpenType;
            parameter[21].Value = Nodeinfo.ContentPageHtmlRule;
            parameter[22].Value = Nodeinfo.ListPageHtmlEx;
            parameter[23].Value = Nodeinfo.ContentFileEx;
            parameter[24].Value = Nodeinfo.OrderID;
            return parameter;
        }
        /// <summary>
        /// 从DataReader中读取管理员记录
        /// </summary>
        /// <param name="rdr">DataReader</param>
        /// <returns>M_Node 节点信息</returns>
        private static M_Node GetNodeFromReader(SqlDataReader rdr)
        {
            M_Node info = new M_Node();
            info.NodeID = DataConverter.CLng(rdr["NodeID"]);
            info.NodeName = rdr["NodeName"].ToString();
            info.NodeType = DataConverter.CLng(rdr["NodeType"]);
            info.Tips = rdr["Tips"].ToString();
            info.ParentID = DataConverter.CLng(rdr["ParentID"]);
            info.OrderID = DataConverter.CLng(rdr["OrderID"]);
            info.Child = DataConverter.CLng(rdr["Child"]);
            info.Depth = DataConverter.CLng(rdr["Depth"]);
            info.NodeDir = rdr["NodeDir"].ToString();
            info.NodeUrl = rdr["NodeUrl"].ToString();
            info.NodePic = rdr["NodePicUrl"].ToString();
            info.Description = rdr["Description"].ToString();
            info.Meta_Keywords = rdr["Meta_Keywords"].ToString();
            info.Meta_Description = rdr["Meta_Description"].ToString();
            info.OpenNew = DataConverter.CBool(rdr["OpenType"].ToString());
            info.PurviewType = DataConverter.CBool(rdr["PurviewType"].ToString());
            info.CommentType = DataConverter.CBool(rdr["CommentType"].ToString());
            info.HitsOfHot = DataConverter.CLng(rdr["HitsOfHot"]);
            info.ListTemplateFile = rdr["ListTemplateFile"].ToString();
            info.IndexTemplate = rdr["IndexTemplate"].ToString();
            info.ContentModel = rdr["ContentModel"].ToString();
            info.ItemOpenType = DataConverter.CBool(rdr["ItemOpenType"].ToString());
            info.ContentPageHtmlRule = DataConverter.CLng(rdr["ContentHtmlRule"]);
            info.ListPageHtmlEx = DataConverter.CLng(rdr["ListPageHtmlEx"]);
            info.ContentFileEx = DataConverter.CLng(rdr["ContentFileEx"]);
            return info;
        }
    }
}