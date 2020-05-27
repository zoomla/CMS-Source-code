namespace ZoomLa.BLL
{
    using System;
    using System.IO;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Collections.Generic;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Globalization;
    using System.Text;
    using System.Xml;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    using ZoomLa.BLL.Helper;
    using Newtonsoft.Json;
    using System.Data.Common;
    using SQLDAL.SQL;
    public class B_Node : ZoomLa.BLL.ZL_Bll_InterFace<M_Node>
    {
        private string TbName, PK;
        private M_Node initMod = new M_Node();
        private string defOrder = "OrderID ASC,NodeID ASC";
        public B_Node()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        #region 兼容区
        public DataTable GetNodeListContainXML(int pid)
        {
            return SelByPid(pid, true);
        }
        public void UpdateNodeXMLAll()
        {

        }
        public void UpNode()
        {

        }
        /// <summary>
        /// 检测是否存在该子节点
        /// </summary>
        /// <param name="main">父节点ID</param>
        /// <param name="child">子节点ID</param>
        /// <returns></returns>
        public bool checkChild(int main, int child)
        {
            if (main == 0) return false;
            if (main == child) return true;
            DataTable dt = SelByPid(main, true);
            return dt.Select("NodeID=" + child).Length > 0;
        }
        #endregion
        #region 整理区
        public int AddNode(M_Node model)
        {
            return Insert(model);
        }
        #endregion
        #region Tools
        /// <summary>
        /// 获取指定节点的起始父节点,并生成节点树,不包含0
        /// 注意节点不要存在循环(如NodeID=ParentID,或圆形)
        /// </summary>
        public int SelFirstNodeID(int nodeid, ref string nodeTree)
        {
            if (nodeid < 1) { return 0; }
            string sql = "with f as(SELECT * FROM ZL_Node WHERE NodeID=" + nodeid + " UNION ALL SELECT A.* FROM ZL_Node A, f WHERE a.NodeID=f.ParentID) SELECT * FROM ZL_Node WHERE NodeID IN(SELECT NodeID FROM f)";
            //string oracle = "SELECT * FROM ZL_Node START WITH NODEID =" + nodeid + " CONNECT BY PRIOR ParentID = NodeID";
            string oracle = "SELECT * FROM ZL_Node Where NodeID=" + nodeid;//[need deal]
            DataTable dt = DBCenter.ExecuteTable(DBCenter.GetSqlByDB(sql, oracle));
            if (dt.Rows.Count < 1) { return 0; }
            foreach (DataRow dr in dt.Rows)
            {
                nodeTree += dr["NodeID"] + ",";
            }
            nodeTree = nodeTree.Trim(',');
            return DataConvert.CLng(dt.Rows[0]["NodeID"]);
        }
        public int SelFirstNodeID(int nodeid)
        {
            string nodeTree = "";
            return SelFirstNodeID(nodeid, ref nodeTree);
        }
        /// <summary>
        /// 创建下拉选单,根据需要,在外层套上请选择或全部选择
        /// </summary>
        public string CreateDP(DataTable dt, int depth = 0, int pid = 0)
        {
            string hasChild = "<option value='{0}'>{2}|-{1}</option>";
            string noChild = "<option value='{0}'>{2}|-{1}</option>";
            string result = "", pre = "", span = "&nbsp&nbsp";
            DataRow[] dr = dt.Select("ParentID='" + pid + "'");
            depth++;
            for (int i = 1; i < depth; i++)
            {
                pre = span + pre;
            }
            for (int i = 0; i < dr.Length; i++)
            {
                if (dt.Select("ParentID='" + Convert.ToInt32(dr[i]["NodeID"]) + "'").Length > 0)
                {
                    result += string.Format(hasChild, dr[i]["NodeID"], dr[i]["NodeName"], pre);
                    result += CreateDP(dt, depth, Convert.ToInt32(dr[i]["NodeID"]));
                }
                else
                {
                    result += string.Format(noChild, dr[i]["NodeID"], dr[i]["NodeName"], pre);
                }
            }
            return result;
        }
        /// <summary>
        /// 批量修改,ListBox,需要特殊处理,按子父级关系输出table
        /// </summary>
        /// <returns></returns>
        public DataTable CreateForListBox(int pid = 0)
        {
            DataTable dt = SelByPid(pid, true);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int level = Convert.ToInt32(dr["Depth"]);
                dr["NodeName"] = GetLevelStr("　", "|-", level) + dr["NodeName"];
            }
            //重新整理一次,再输出,dt是已排序好的数据
            DataTable result = dt.Clone();
            AddToDT(dt, result, pid);
            return result;
        }
        /// <summary>
        /// 以层级关系,将其加入至需要返回的表
        /// </summary>
        private void AddToDT(DataTable dt, DataTable result, int pid)
        {
            DataRow[] drs = dt.Select("ParentID=" + pid);
            for (int i = 0; i < drs.Length; i++)
            {
                result.ImportRow(drs[i]);
                AddToDT(dt, result, Convert.ToInt32(drs[i]["NodeID"]));
            }
        }
        #endregion
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", defOrder);
        }
        public DataTable SelByNode(int node)
        {
            return DBCenter.Sel(TbName, "NodeID=" + node);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "", defOrder);
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable Sel(int pid = -1, string nodeName = "")
        {
            string field = " A.*,(SELECT COUNT(GeneralID) FROM ZL_CommonModel WHERE NodeID=A.NodeID AND Status=99) ItemCount,(SELECT Count(NodeID) From " + TbName + " WHERE A.NodeID=ParentID) ChildCount ";
            string where = "";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(nodeName)) { where += "NodeName LIKE @nodename"; sp.Add(new SqlParameter("nodename", "%" + nodeName + "%")); }
            DataTable dt = DBCenter.SelWithField(TbName, field, where, "CDate DESC", sp);
            dt.Columns["ItemCount"].ReadOnly = false;//使用了聚合,需要取消只读
            CountDT(dt, null);
            DataTable result = null;
            if (pid == -1)//非显示全部,不用排序
            {
                result = dt.Clone();
                AddToDT(dt, result, 0);
            }
            else
            {
                result = dt;
                result.DefaultView.RowFilter = "ParentID=" + pid;
                result = result.DefaultView.ToTable();
            }
            return result;
        }
        //根据节点父id得到子节点ids
        public string GetChilds(int pid)
        {
            DataTable dt = SelByPid(pid, true);
            string nodeids = pid.ToString();
            foreach (DataRow dr in dt.Rows)
            {
                nodeids += "," + dr["NodeID"];
            }
            return nodeids;
        }
        /// <summary>
        ///获取子节点列表,不包含父节点信息
        /// </summary>
        /// <param name="pid">父节点ID</param>
        /// <param name="isall">true所有子节点信息,false:仅下一级</param>
        public DataTable SelByPid(int pid, bool isall = false)
        {
            if (isall)
            {
                //string sql = "with Tree as(SELECT *,cast(1 as int) as [level] FROM ZL_Node WHERE ParentID=" + pid + " UNION ALL SELECT a.*,b.[level]+1 FROM ZL_Node a JOIN Tree b on a.ParentID=b.NodeID) SELECT *,(SELECT Count(NodeID) From ZL_Node WHERE A.NodeID=ParentID) ChildCount FROM Tree AS A ORDER BY OrderID,NodeID ASC";
                //DataTable dt = Sel();
                //string nids = "";
                //RecursionByPid(dt, pid, ref nids);
                //nids = StrHelper.PureIDSForDB(nids);
                //dt.DefaultView.RowFilter = "NodeID IN(" + nids + ")";
                //return dt.DefaultView.ToTable();
                //WITH Tree AS(
                //SELECT * FROM ZL_Node WHERE ParentID=0 UNION ALL SELECT A.* FROM ZL_Node A JOIN Tree B on A.ParentID=B.NodeID) 
                //SELECT * FROM Tree ORDER BY OrderID,NodeID ASC
                string sql = "with Tree as(SELECT * FROM ZL_Node WHERE ParentID=" + pid + " UNION ALL SELECT a.* FROM ZL_Node a JOIN Tree b on a.ParentID=b.NodeID) SELECT *,(SELECT Count(NodeID) From ZL_Node WHERE A.NodeID=ParentID) ChildCount FROM Tree AS A ORDER BY OrderID,NodeID ASC";
                //获取父节点下子级后,再去除父级
                string oracle = "SELECT * FROM(SELECT * FROM ZL_Node START WITH NODEID IN(" + pid + ") CONNECT BY nocycle prior NodeID=ParentID )WHERE NodeID!=" + pid;
                return DBCenter.ExecuteTable(DBCenter.GetSqlByDB(sql, oracle));
            }
            else
            {
                string fields = "A.*,(SELECT Count(NodeID) From ZL_Node WHERE A.NodeID=ParentID) ChildCount ";
                //string sql = "SELECT " + fields + " FROM " + TbName + " A WHERE ParentID=" + pid+" ORDER BY OrderID,NodeID ASC";
                return DBCenter.SelWithField(TbName, fields, "ParentID=" + pid, defOrder);
            }
        }
        public DataTable SelByIDS(string nodeids, string nodeType = "")
        {
            SafeSC.CheckIDSEx(nodeids);
            string where = "NodeID IN (" + nodeids + ")";
            if (!string.IsNullOrEmpty(nodeType)) { SafeSC.CheckIDSEx(nodeType); where += " AND NodeType IN (" + nodeType + ")"; }
            return DBCenter.Sel(TbName, where, defOrder);
        }
        public DataTable SelByType(string nodeType)
        {
            SafeSC.CheckIDSEx(nodeType);
            string where = "NodeID IN (" + nodeType + ")";
            return DBCenter.Sel(TbName, where, defOrder);
        }
        /// <summary>
        /// 用于节点管理--显示全部
        /// (仅统计内容方面的节点,所以数量会与ZL_CommonModel表中的总数有差异)
        /// </summary>
        /// <returns></returns>
        public DataTable SelForShowAll(int pid, bool isall = false)
        {
            string field = " A.*,(SELECT COUNT(GeneralID) FROM ZL_CommonModel WHERE NodeID=A.NodeID AND Status=99) ItemCount,(SELECT Count(NodeID) From " + TbName + " WHERE A.NodeID=ParentID) ChildCount ";
            //string sql = "SELECT " + field + " FROM " + TbName + " A ORDER BY A.OrderID,A.NodeID ASC";
            DataTable dt = DBCenter.SelWithField(TbName, field, "", defOrder);
            dt.Columns["ItemCount"].ReadOnly = false;//使用了聚合,需要取消只读
            CountDT(dt, null);
            DataTable result = null;
            if (isall)//非显示全部,不用排序
            {
                result = dt.Clone();
                AddToDT(dt, result, 0);
            }
            else
            {
                result = dt;
                result.DefaultView.RowFilter = "ParentID=" + pid;
                result = result.DefaultView.ToTable();
            }
            return result;
        }
        //public DataTable SelectNodeHtml()
        //{
        //    string strSql = "select * from ZL_Node where (NodeUrl<>' ' or NodeListUrl<>' ') and ListPageHtmlEx<3 order by nodeid desc";
        //}
        public DataTable SelectNodeHtmlXML()
        {
            return SelByPid(0, true);
        }
        public DataTable GetNodeListUserShop(int ParentID)
        {
            return DBCenter.Sel(TbName, "ParentID=" + ParentID + " AND NodeType=1 AND NodeListType=5", defOrder);
        }
        public void UpdateNode(M_Node model)
        {
            UpdateByID(model);
        }
        /// <summary>
        /// 删除所有节点及节点模板
        /// </summary>
        public void DeleteAll()
        {
            DBCenter.DB.Table_Clear("ZL_Node_ModelTemplate");
            DBCenter.DB.Table_Clear(TbName);
        }
        /// <summary>
        /// 更改节点数据
        /// </summary>
        public void InitNode(string tablename, string fieldname)
        {
            DBCenter.DB.Table_Clear(TbName);
        }
        /// <summary>
        /// 删除节点，并删除该节点的子节点   
        /// </summary>
        public void DelNode(int NodeID)
        {
            if (NodeID < 1) { throw new Exception("NodeID不能小于或等于0"); }
            DBCenter.DelByWhere(TbName, "NodeID=" + NodeID + " OR ParentID=" + NodeID);
        }
        private void SetChild(int ParentID)
        {
            DBCenter.UpdateSQL(TbName, "Child=Child-1", "NodeID=" + ParentID);
        }
        public DataTable CheckNode(int Nodeid)
        {
            return SelByNode(Nodeid);
        }
        public DataTable GetShopNode(string nodeIDS)
        {
            SafeSC.CheckIDSEx(nodeIDS);
            return DBCenter.Sel(TbName, "NodeType=1 AND (NodeListType=2 OR NodeListType=3) AND NodeID not in (" + nodeIDS + ")", "OrderID,NodeId");
        }
        public void SetChildDel(int ParentID)
        {
            this.SetChild(ParentID);
        }
        /// <summary>
        /// 获取父节点下的第一个节点的ID
        /// </summary>
        public int GetFirstNode(int ParentID)
        {
            return DataConvert.CLng(DBCenter.ExecuteScala(TbName, "NodeID", "ParentID=" + ParentID + " AND NodeType=1", defOrder));
        }
        public int GetMaxOrder(int ParentID)
        {
            return DataConvert.CLng(DBCenter.ExecuteScala(TbName, "MAX(OrderID)", "ParentID=" + ParentID));
        }
        public int GetMinOrder(int ParentID)
        {
            return DataConvert.CLng(DBCenter.ExecuteScala(TbName, "MIN(OrderID)", "ParentID=" + ParentID));
        }
        private int dal_GetPreNode(int ParentID, int CurrentID)
        {
            return DataConvert.CLng(DBCenter.ExecuteScala(TbName, "NodeID", "ParentID=" + ParentID + " and OrderID<" + CurrentID, defOrder));
        }
        public M_Node GetPreNode(int ParentID, int CurrentID)
        {
            int PID = this.dal_GetPreNode(ParentID, CurrentID);
            return this.dal_GetNode(PID);
        }
        /// <summary>
        /// 获取下一个节点模型,如果不存在,则返回自身
        /// </summary>
        public M_Node GetNextNode(int ParentID, int CurrentID)
        {
            int NextID = CurrentID;
            DataTable dt = DBCenter.SelTop(1, PK, "NodeID", TbName, "ParentID=" + ParentID + " AND OrderID>" + CurrentID, "OrderId");
            if (dt.Rows.Count > 0) { NextID = DataConvert.CLng(dt.Rows); }
            return this.dal_GetNode(NextID);
        }
        public M_Node SelReturnModel(int ID)
        {
            if (ID < 1)
            {
                M_Node nodeMod = new M_Node(true);
                nodeMod.NodeID = 0;
                nodeMod.NodeName = "根节点";
                return nodeMod;
            }
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Node(true);
                }
            }
        }
        public M_Node SelModelByWhere(string where, SqlParameter[] sp)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, sp))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Node(true);
                }
            }
        }
        public M_Node GetNode(int NodeID)
        {
            return this.dal_GetNode(NodeID);
        }
        public M_Node dal_GetNode(int NodeID)
        {
            return SelReturnModel(NodeID);
        }
        public M_Node GetNodeXML(int NodeID)
        {
            return SelReturnModel(NodeID);
        }
        public DataTable GetNodeChildList(int parentID)
        {
            //取消对child的代码维护
            string where = "";
            if (parentID >= 0)
            {
                where += " ParentID=" + parentID;
            }
            return DBCenter.SelWithField(TbName, "A.*,(SELECT Count(NodeID) From " + TbName + " WHERE A.NodeID=ParentID) ChildCount", where, defOrder);
        }
        /// <summary>
        /// 从1开始的深度
        /// </summary>
        public int GetDepth(int ParentID)
        {
            if (ParentID == 0) { return 0; }
            string tree = "";
            SelFirstNodeID(ParentID, ref tree);
            return tree.Split(',').Length;
        }
        public int GetNodeListCount(int ParentID)
        {
            return DBCenter.Count(TbName, "ParentID=" + ParentID);
        }
        #region datatable
        //返回父节点下的子节点
        private DataTable dal_GetNodeListContain(int ParentID)
        {
            return DBCenter.Sel(TbName, "ParentID=" + ParentID + " AND NodeListType<>6", "OrderID");
        }
        public void AddDataRowNode(DataTable dt, int ParentID)
        {
            DataTable ds = this.dal_GetNodeListContain(ParentID);
            DataRow myDataRow;

            foreach (DataRow dr in ds.Rows)
            {
                myDataRow = dt.NewRow();
                myDataRow["NodeID"] = DataConverter.CLng(dr["NodeID"]);
                int Dep = DataConverter.CLng(dr["Depth"].ToString());
                string str = "";
                if (Dep > 0)
                {
                    for (int i = 1; i <= Dep - 1; i++)
                    {
                        str = str + "　";
                    }
                    str = str + "|-";
                }
                myDataRow["NodeName"] = str + dr["NodeName"].ToString();
                dt.Rows.Add(myDataRow);
                if (DataConverter.CLng(dr["Child"]) > 0)
                {
                    AddDataRowNode(dt, DataConverter.CLng(dr["NodeID"]));
                }
            }
        }
        /// <summary>
        /// 读取节点下的子节点
        /// </summary>
        /// <param Name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetNodeList(int ParentID)
        {
            DataTable dt = new DataTable("NodeTree");
            DataColumn myDataColumn;
            DataRow myDataRow;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NodeID";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "NodeName";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NodeType";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Depth";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ParentID";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "OrderID";
            dt.Columns.Add(myDataColumn);

            myDataRow = dt.NewRow();
            myDataRow["NodeID"] = 0;
            myDataRow["NodeName"] = SiteConfig.SiteInfo.SiteName;
            myDataRow["NodeType"] = 0;
            myDataRow["Depth"] = 0;
            myDataRow["ParentID"] = 0;
            myDataRow["OrderID"] = 0;
            dt.Rows.Add(myDataRow);

            this.AddDataRow(dt, ParentID);
            return dt;
        }
        /// <summary>
        /// 读取节点下的子节点
        /// </summary>
        /// <param Name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetNodeList(int ParentID, string name)
        {
            DataTable dt = new DataTable("NodeTree");
            DataColumn myDataColumn;
            DataRow myDataRow;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NodeID";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "NodeName";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NodeType";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Depth";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ParentID";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "OrderID";
            dt.Columns.Add(myDataColumn);

            if (ParentID != 0)
            {
                M_Node mn = GetNodeXML(ParentID);

                myDataRow = dt.NewRow();
                myDataRow["NodeID"] = mn.NodeID;
                myDataRow["NodeName"] = name;
                myDataRow["NodeType"] = 0;
                myDataRow["Depth"] = mn.Depth;
                myDataRow["ParentID"] = mn.ParentID;
                myDataRow["OrderID"] = mn.OrderID;
                dt.Rows.Add(myDataRow);
            }
            else
            {
                myDataRow = dt.NewRow();
                myDataRow["NodeID"] = 0;
                myDataRow["NodeName"] = SiteConfig.SiteInfo.SiteName;
                myDataRow["NodeType"] = 0;
                myDataRow["Depth"] = 0;
                myDataRow["ParentID"] = 0;
                myDataRow["OrderID"] = 0;
                dt.Rows.Add(myDataRow);
            }
            this.AddDataRow(dt, ParentID);
            return dt;
        }
        public void AddDataRow(DataTable dt, int ParentID)
        {
            DataTable childdt = this.GetNodeChildList(ParentID);
            DataRow myDataRow;
            foreach (DataRow dr in childdt.Rows)
            {
                myDataRow = dt.NewRow();
                myDataRow["NodeID"] = DataConverter.CLng(dr["NodeID"]);
                myDataRow["NodeName"] = dr["NodeName"].ToString();
                myDataRow["NodeType"] = DataConverter.CLng(dr["NodeType"]);
                myDataRow["Depth"] = DataConverter.CLng(dr["Depth"]);
                myDataRow["ParentID"] = DataConverter.CLng(dr["ParentID"]);
                myDataRow["OrderID"] = DataConverter.CLng(dr["OrderID"]);
                dt.Rows.Add(myDataRow);
                if (DataConverter.CLng(dr["Child"]) > 0)
                {
                    AddDataRow(dt, DataConverter.CLng(dr["NodeID"]));
                }
            }
        }
        /// <summary>
        /// 读取节点下的栏目子节点
        /// </summary>
        /// <param Name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetNodeListContain(int ParentID)
        {
            DataTable dt = new DataTable("NodeList");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NodeID";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "NodeName";
            dt.Columns.Add(myDataColumn);

            this.AddDataRowNode(dt, ParentID);
            return dt;
        }
        public DataTable GetNodeList(int ParentID, int NodeID)
        {
            DataTable dt = new DataTable("NodeTree");
            DataColumn myDataColumn;
            DataRow myDataRow;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NodeID";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "NodeName";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NodeType";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Depth";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ParentID";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "OrderID";
            dt.Columns.Add(myDataColumn);

            myDataRow = dt.NewRow();
            myDataRow["NodeID"] = 0;
            myDataRow["NodeName"] = SiteConfig.SiteInfo.SiteName;
            myDataRow["NodeType"] = 0;
            myDataRow["Depth"] = 0;
            myDataRow["ParentID"] = 0;
            myDataRow["OrderID"] = 0;
            dt.Rows.Add(myDataRow);

            AddRow(dt, ParentID, NodeID);
            return dt;
        }
        public void AddRow(DataTable dt, int ParentID, int NodeID)
        {
            DataTable childDT = GetNodeChildList(ParentID);
            DataRow myDataRow;
            foreach (DataRow dr in childDT.Rows)
            {
                if (dr["NodeID"].ToString() != NodeID.ToString() && dr["NodeID"] != null)
                {
                    myDataRow = dt.NewRow();
                    myDataRow["NodeID"] = DataConverter.CLng(dr["NodeID"]);
                    myDataRow["NodeName"] = dr["NodeName"].ToString();
                    myDataRow["NodeType"] = DataConverter.CLng(dr["NodeType"]);
                    myDataRow["Depth"] = DataConverter.CLng(dr["Depth"]);
                    myDataRow["ParentID"] = DataConverter.CLng(dr["ParentID"]);
                    myDataRow["OrderID"] = DataConverter.CLng(dr["OrderID"]);
                    dt.Rows.Add(myDataRow);
                    if (DataConverter.CLng(dr["Child"]) > 0)
                    {
                        AddRow(dt, DataConverter.CLng(dr["NodeID"]), NodeID);
                    }
                }
            }
        }
        #endregion
        public DataTable GetCreateSingleByID(string nids)
        {
            if (string.IsNullOrEmpty(nids)) return null;
            SafeSC.CheckIDSEx(nids);
            return DBCenter.Sel(TbName, "NodeType=2 and NodeID in (" + nids + ")", "OrderID");
        }
        public void ExpandParent(TreeNode tn)
        {
            if (tn.Parent != null) { ExpandParent(tn.Parent); tn.Parent.Expand(); }
        }
        /// <summary>
        /// 投稿管理
        /// </summary>
        public void InitTreeNodeUser(TreeNodeCollection Nds, int ParentID, int t)
        {
            B_User bu = new B_User();

            string search = "";

            TreeNode tmpNd;
            //DataTable ds = GetNodeListContainXML2(ParentID);
            DataTable dt = SelByPid(ParentID);
            B_Permission pii = new B_Permission();
            foreach (DataRow dr in dt.Rows)
            {
                t = Convert.ToInt32(dr["NodeListType"]);
                switch (t)
                {
                    case 1:
                        search = "javascript:gotourl('User/Content/MyContent.aspx?";
                        break;
                    case 2:
                        search = "javascript:gotourl('User/Content/MyContent.aspx?type=UnAudit&";
                        break;
                    case 3:
                        search = "javascript:gotourl('User/Content/MyProduct.aspx?type=Audit&";
                        break;
                    case 4:
                        search = "javascript:gotourl('User/Content/MyFavori.aspx?";
                        break;
                    case 5:
                        search = "javascript:gotourl('User/Content/MyComment.aspx?";
                        break;
                    case 6:
                        search = "javascript:gotourl('User/Content/Pub.aspx?"; break;
                    case 7:
                        search = "javascript:gotourl('User/Content/ReleaseManager.aspx?";
                        break;
                }
                //if (pii.Get_Node(DataConverter.CLng(dr["NodeID"].ToString()), bu.GetLogin().GroupID).Add)
                //{
                tmpNd = new TreeNode();
                tmpNd.Value = dr["NodeID"].ToString();
                tmpNd.Text = dr["NodeName"].ToString();
                tmpNd.NavigateUrl = search + "NodeID=" + tmpNd.Value + "');";
                //tmpNd.Target = "main_right";
                //tmpNd.ImageUrl = "~/Images/TreeLineImages/plus.gif";
                tmpNd.ToolTip = dr["Tips"].ToString();
                Nds.Add(tmpNd);
                if (DataConverter.CLng(dr["Child"]) > 0)
                {
                    InitTreeNodeUser(tmpNd.ChildNodes, DataConverter.CLng(tmpNd.Value), t);
                }
                //}
            }
        }
        ///<summary>
        ///增加判断子节点显示
        ///</summary>
        public void InitTreeNodeUser(TreeNodeCollection Nds, int ParentID, int t, DataTable dts)
        {
            B_User bu = new B_User();

            string search = "";

            TreeNode tmpNd;
            DataTable dt = SelByPid(ParentID);
            B_Permission pii = new B_Permission();
            foreach (DataRow dr in dt.Rows)
            {
                t = Convert.ToInt32(dr["NodeListType"]);
                switch (t)
                {
                    case 1:
                        search = "javascript:gotourl('User/Content/MyContent.aspx?";
                        break;
                    case 2:
                        search = "javascript:gotourl('User/Content/MyContent.aspx?type=UnAudit&";
                        break;
                    case 3:
                        search = "javascript:gotourl('User/Content/MyProduct.aspx?type=Audit&";
                        break;
                    case 4:
                        search = "javascript:gotourl('User/Content/MyFavori.aspx?";
                        break;
                    case 5:
                        search = "javascript:gotourl('User/Content/MyComment.aspx?";
                        break;
                    case 6:
                        search = "javascript:gotourl('User/Content/Pub.aspx?";
                        break;
                    case 7:
                        search = "javascript:gotourl('User/Content/ReleaseManager.aspx?";
                        break;
                }
                //if (pii.Get_Node(DataConverter.CLng(dr["NodeID"].ToString()), bu.GetLogin().GroupID).Add)
                //{
                foreach (DataRow row in dts.Rows)//如果NodeID存在于表中
                {
                    if (row["NodeID"].ToString().Equals(dr["NodeID"].ToString()))
                    {
                        if (row["look"].ToString().Equals("0"))
                            return;
                        tmpNd = new TreeNode();
                        tmpNd.Value = dr["NodeID"].ToString();
                        tmpNd.Text = dr["NodeName"].ToString();
                        tmpNd.NavigateUrl = search + "NodeID=" + tmpNd.Value + "');";
                        //tmpNd.Target = "main_right";
                        //tmpNd.ImageUrl = "~/Images/TreeLineImages/plus.gif";
                        tmpNd.ToolTip = dr["Tips"].ToString();
                        Nds.Add(tmpNd);
                        if (DataConverter.CLng(dr["Child"]) > 0)
                        {
                            InitTreeNodeUser(tmpNd.ChildNodes, DataConverter.CLng(tmpNd.Value), t, dts);
                        }
                    }

                }

                //}
            }
        }
        public DataTable GetNodeListContainByParentID(int ParentID)
        {
            return dal_GetNodeListContain(ParentID);
        }
        /// <summary>
        /// 返回所有商城节点,用于生成树节点
        /// </summary>
        public DataTable GetAllShopNode(int parentid = 0)
        {
            string where = "NodeType=1 AND (NodeListType=2 OR NodeListType=3)";
            //默认取出全部给予左边栏使用
            if (parentid > 0) { where += " AND ParentID=" + parentid; }
            return DBCenter.Sel(TbName, where, "OrderID");
        }
        /// <summary>
        /// 返回所有店铺节点,用于生成树节点
        /// </summary>
        public DataTable GetAllUserShopNode()
        {
            return DBCenter.Sel(TbName, "NodeType=1 AND NodeListType=5", defOrder);
        }
        public DataTable SelNodeByModel(int modelID)
        {
            return SelNodeByModel(modelID.ToString());
        }
        public DataTable SelNodeByModel(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string where = DBCenter.GetSqlByDB("','+ContentModel+','", "','||ContentModel||','");
            string[] idarr = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < idarr.Length; i++)
            {
                where += " LIKE '%," + idarr[i] + ",%' OR";
            }
            where = where.TrimEnd("OR".ToCharArray());
            return DBCenter.Sel(TbName, where);
        }
        /// <summary>
        /// 移动节点
        /// </summary>
        public void UpDNode(int NodeID, int main)
        {
            DBCenter.UpdateSQL(TbName, "ParentID=" + NodeID, "ParentID=" + main);
        }
        /// <summary>
        /// 检测节点名称与栏目名是否存在,用于节点修改
        /// </summary>
        /// <returns>存为或不规范,则为false</returns>
        public bool CheckCanSave(M_Node nodeMod)
        {
            if (StrHelper.StrNullCheck(nodeMod.NodeName, nodeMod.NodeDir)) { return false; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("NodeName", nodeMod.NodeName), new SqlParameter("NodeDir", nodeMod.NodeDir) };
            string where = "NodeID!=" + nodeMod.NodeID + " AND ParentID=" + nodeMod.ParentID + " AND (NodeName=@NodeName OR NodeDir=@NodeDir)";
            return DBCenter.Count(TbName, where, sp) == 0;
        }
        public bool CheckNodeName(M_Node nodeMod)
        {
            if (StrHelper.StrNullCheck(nodeMod.NodeName)) { return false; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("NodeName", nodeMod.NodeName) };
            string where = "NodeID!=" + nodeMod.NodeID + " AND ParentID=" + nodeMod.ParentID + " AND NodeName=@NodeName";
            return DBCenter.Count(TbName, where, sp) == 0;
        }
        public bool CheckNodeDir(M_Node nodeMod)
        {
            if (StrHelper.StrNullCheck(nodeMod.NodeDir)) { return false; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("NodeName", nodeMod.NodeName), new SqlParameter("NodeDir", nodeMod.NodeDir) };
            string where = "NodeID!=" + nodeMod.NodeID + " AND ParentID=" + nodeMod.ParentID + " AND NodeDir=@NodeDir";
            return DBCenter.Count(TbName, where, sp) == 0;
        }
        public void InitTreeNodeSel(TreeNodeCollection Nds, int ParentID, string customPath)
        {
            string search = customPath + "user/SelectContent.aspx?NodeID=";
            TreeNode tmpNd;
            DataTable ds = this.dal_GetNodeListContain(ParentID);
            foreach (DataRow dr in ds.Rows)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr["NodeID"].ToString();
                tmpNd.Text = dr["NodeName"].ToString();

                tmpNd.NavigateUrl = search + tmpNd.Value;
                //tmpNd.Target = "main_right";
                tmpNd.ToolTip = dr["Tips"].ToString();
                Nds.Add(tmpNd);
                if (DataConverter.CLng(dr["Child"]) > 0)
                {
                    InitTreeNodeSel(tmpNd.ChildNodes, DataConverter.CLng(tmpNd.Value), customPath);
                }
            }
        }
        /// <summary>
        /// [main]添加
        /// </summary>
        public int Insert(M_Node model)
        {
            model.Depth = (GetDepth(model.ParentID) + 1);
            if (model.OrderID < 1) { model.OrderID = (GetMaxOrder(model.ParentID)) + 1; }
            model.NodeID = DBCenter.Insert(model);
            return model.NodeID;
        }
        public bool UpdateByID(M_Node model)
        {
            if (model.NodeID < 1) { throw new Exception("更新节点失败,NodeID不能为空"); }
            model.Depth = (GetDepth(model.ParentID) + 1);
            return DBCenter.UpdateByID(model, model.NodeID);
        }
        public bool Del(int ID)
        {
            DelNode(ID);
            return true;
        }
        // Common/NodeList
        public DataTable SelToNodeList()
        {
            return DBCenter.SelWithField(TbName, "A.*,NodeID AS ID,NodeName AS Name", "", defOrder);
        }
        /// <summary>
        /// 查询节点的最终父节点
        /// </summary>
        /// <param Name="NodeID"></param>
        /// <param Name="p">判断上级站点为5，判断最终父节点为0</param>
        /// <returns></returns>
        public int GetContrarily(int NodeID, int p)
        {
            M_Node mn = GetNodeXML(NodeID);
            int x = 0;

            if (p == 0)
            {
                if (mn.ParentID == 0)
                {
                    x = mn.NodeID;
                }
                else
                {
                    x = GetContrarily(mn.ParentID, p);
                }
            }
            if (p == 5)
            {
                if (mn.ParentID > 0)
                    x = GetContrarily(mn.ParentID, p);
                else
                    x = mn.NodeID;
            }
            return x;
        }
        /// <summary>
        /// 子站定义文件夹路径
        /// </summary>
        public string GetDir(int nodeid, string strDir)
        {
            M_Node mn = GetNodeXML(nodeid);
            if (nodeid != 0)
            {
                strDir = GetDir(mn.ParentID, "/" + mn.NodeDir) + strDir;
            }
            else
            {
                if (string.IsNullOrEmpty(strDir))
                {
                    strDir = "/";
                }
                else
                {
                    strDir = mn.NodeDir + strDir;
                }
            }
            return strDir.Replace("//", "/");
        }
        public void GetColumnList(int ParentID, DataTable newDT)
        {
            DataTable oldDT = GetNodeChildList(ParentID);

            for (int i = 0; i < oldDT.Rows.Count; i++)
            {
                string depth = "";
                if (oldDT.Rows[i]["NodeType"].ToString() != "5")
                {
                    if (DataConverter.CLng(oldDT.Rows[i]["Depth"].ToString()) > 1)
                    {
                        for (int x = 0; x < DataConverter.CLng(oldDT.Rows[i]["Depth"].ToString()); x++)
                        {
                            depth += "　";
                        }
                        depth += "├";
                    }
                    DataRow dr = newDT.NewRow();
                    dr[0] = oldDT.Rows[i]["NodeID"].ToString();

                    dr[1] = depth + oldDT.Rows[i]["NodeName"].ToString();
                    newDT.Rows.Add(dr);

                    //判断节点下是否还有节点
                    if (GetNodeListCount(DataConverter.CLng(oldDT.Rows[i]["NodeID"].ToString())) > 0)
                    {
                        GetColumnList(DataConverter.CLng(oldDT.Rows[i]["NodeID"].ToString()), newDT);
                    }
                }
            }
        }
        /// <summary>
        /// View:浏览/查看权限
        /// ViewGroup:允许浏览此栏目的会员组(会员组ids)
        /// ViewSunGroup:允许查看此栏目下信息的会员组(会员组ids)
        /// input:发表权限(会员组ids)
        /// forum:评论权限(1,允许在此栏目发表评论;2,评论需要审核;3,一篇文章只能发表一次评论)
        /// </summary>
        /// <param name="nodejson"></param>
        /// <returns></returns>
        public DataTable GetNodeAuitDT(string nodejson = "")
        {
            DataTable CheckDt = null;
            CheckDt = new DataTable();
            CheckDt.Columns.Add("View");
            CheckDt.Columns.Add("ViewGroup");
            CheckDt.Columns.Add("ViewSunGroup");
            CheckDt.Columns.Add("input");
            CheckDt.Columns.Add("forum");
            CheckDt.Rows.Add(CheckDt.NewRow());
            try
            {
                if (!string.IsNullOrEmpty(nodejson)) { return JsonConvert.DeserializeObject<DataTable>(nodejson); }
            }
            catch (Exception) { }
            return CheckDt;
        }
        // 商品
        public void UnionUp(int Nodeid, int Nodeid2)
        {
            string strSql = "Update ZL_Commodities set Nodeid=" + Nodeid + " where Nodeid=" + Nodeid2;
            SqlHelper.ExecuteNonQuery(CommandType.Text, strSql);
        }
        /// <summary>
        /// 内容转移
        /// </summary>
        /// <param name="tnid">目标节点</param>
        /// <param name="snid">来源节点</param>
        public void UnionUp2(int tnid, int snid)
        {
            DBCenter.UpdateSQL("ZL_CommonModel", "Nodeid=" + tnid, "NodeID=" + snid);
        }
        #region  模板操作
        public void AddModelTemplate(int NodeID, int ModelID, string ModelTemplate)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Template", ModelTemplate) };
            DBCenter.Insert("ZL_Node_ModelTemplate", "NodeID,ModelID,Template", NodeID + "," + ModelID + ",@Template", sp);
        }
        public bool IsExistTemplate(int NodeID, int ModelID)
        {
            return DBCenter.IsExist("ZL_Node_ModelTemplate", "NodeID=" + NodeID + " AND ModelID=" + ModelID);
        }
        public void DelModelTemplate(int NodeID, string ModelIDs)
        {
            ModelIDs = StrHelper.PureIDSForDB(ModelIDs);
            if (string.IsNullOrEmpty(ModelIDs)) { return; }
            SafeSC.CheckIDSEx(ModelIDs);
            DBCenter.DelByWhere("ZL_Node_ModelTemplate", "NodeID=" + NodeID + " AND ModelID Not IN (" + ModelIDs + ")");
        }
        public void UpdateModelTemplate(int NodeID, int ModelID, string ModelTemplate)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("Template", ModelTemplate) };
            DBCenter.UpdateSQL("ZL_Node_ModelTemplate", "Template=@Template", "NodeID=" + NodeID + " and ModelID=" + ModelID, sp);
        }
        public void DelTemplate(int NodeID)
        {
            DBCenter.Del("ZL_Node_ModelTemplate", "NodeID", NodeID);
        }
        public string GetModelTemplate(int NodeID, int ModelID)
        {
            return DBCenter.ExecuteScala("ZL_Node_ModelTemplate", "Template", "NodeID=" + NodeID + " and ModelID=" + ModelID).ToString();
        }
        #endregion
        //--------------------------------------Tools
        public static string GetNodeType(int type)
        {
            switch (type)
            {
                case 0:
                    return "根节点";
                case 1:
                    return "栏目节点";
                case 2:
                    return "单页节点";
                case 3:
                    return "外部链接";
                default:
                    return "未知栏目类型";
            }
        }
        private string GetLevelStr(string pre, string preChar, int level)
        {
            string str = "";
            for (int i = 1; i < level; i++)
            {
                str += pre;
            }
            return (str += preChar);
        }
        /// <summary>
        /// 统计数量
        /// </summary>
        private void CountDT(DataTable dt, DataRow parentDR)
        {
            int pid = (parentDR == null ? 0 : Convert.ToInt32(parentDR["NodeID"]));
            DataRow[] drs = dt.Select("ParentID=" + pid);
            for (int i = 0; i < drs.Length; i++)
            {
                CountDT(dt, drs[i]);
                if (parentDR != null)
                {
                    parentDR["ItemCount"] = Convert.ToInt32(parentDR["ItemCount"]) + Convert.ToInt32(drs[i]["ItemCount"]);
                }
            }
        }
        /// <summary>
        /// 用于节点层级展示
        /// </summary>
        /// <param name="dt">节点展示dt</param>
        /// <param name="depth">深度,自动计算,第一级传0</param>
        /// <param name="pid">第一级传0</param>
        /// <param name="hasChild">有子节点的菜单模板</param>
        /// <param name="noChild">无子节点的菜单模板</param>
        /// <returns>用于显示的Html</returns>
        public static string GetLI(DataTable dt, string hasChild, string noChild, int depth = 0, int pid = 0)
        {
            if (dt == null || dt.Rows.Count < 1) { return ""; }
            string result = "", pre = "<img src='/Images/TreeLineImages/t.gif' border='0'>", span = "<img src='/Images/TreeLineImages/tree_line4.gif' border='0' width='19' height='20'>";
            DataRow[] dr = dt.Select("ParentID='" + pid + "'");
            depth++;
            for (int i = 1; i < (depth - 1); i++)
            {
                pre = span + pre;
            }
            for (int i = 0; i < dr.Length; i++)
            {
                result += "<li>";
                if (dt.Select("ParentID='" + Convert.ToInt32(dr[i]["ID"]) + "'").Length > 0)
                {
                    result += string.Format(hasChild, dr[i]["ID"], dr[i]["Name"], pre);
                    result += "<ul class='tvNav tvNav_ul'>" + GetLI(dt, hasChild, noChild, depth, Convert.ToInt32(dr[i]["ID"])) + "</ul>";
                }
                else
                {
                    result += string.Format(noChild, dr[i]["ID"], dr[i]["Name"], pre);
                }
                result += "</li>";
            }
            return result;
        }
        //--------------------------------------
    }
}