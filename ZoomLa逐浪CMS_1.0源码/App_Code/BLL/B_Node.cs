namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.IDAL;
    using ZoomLa.DALFactory;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Web.UI.WebControls;
    using System.Globalization;

    /// <summary>
    /// 节点业务逻辑层操作
    /// </summary>
    public class B_Node
    {
        private ID_Node dal = IDal.CreateNode();
        public B_Node()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 添加节点到数据库
        /// </summary>
        /// <param name="node"></param>
        public int AddNode(M_Node node)
        {
            return this.dal.AddNode(node);
        }
        /// <summary>
        /// 更改节点数据
        /// </summary>
        /// <param name="node"></param>
        public void UpdateNode(M_Node node)
        {
            this.dal.UpdateNode(node);
        }
        /// <summary>
        /// 删除节点，并删除该节点的子节点
        /// </summary>
        /// <param name="NodeID"></param>
        public void DelNode(int NodeID)
        {
            M_Node node = this.dal.GetNode(NodeID);
            int parentid = node.ParentID;
            if (node.Child > 0)
            {
                DataSet ds = this.dal.GetNodeList(node.NodeID);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DelNode(DataConverter.CLng(dr["NodeID"]));
                }
            }
            this.dal.DelNode(NodeID);
            this.dal.SetChild(parentid);
        }
        public void SetChildDel(int ParentID)
        {
            this.dal.SetChild(ParentID);
        }
        public int GetFirstNode(int ParentID)
        {
            return this.dal.GetFirstNode(ParentID);
        }
        public int GetOrder(int ParentID)
        {
            return GetMaxOrder(ParentID)+1;
        }
        public int GetMaxOrder(int ParentID)
        {
            return this.dal.GetMaxOrder(ParentID);
        }
        public int GetMinOrder(int ParentID)
        {
            return this.dal.GetMinOrder(ParentID);
        }
        public M_Node GetPreNode(int ParentID, int CurrentID)
        {
            int PID = this.dal.GetPreNode(ParentID, CurrentID);
            return this.dal.GetNode(PID);
        }
        public M_Node GetNextNode(int ParentID, int CurrentID)
        {
            int NextID = this.dal.GetNextNode(ParentID, CurrentID);
            return this.dal.GetNode(NextID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public int GetDepth(int ParentID)
        {
            return this.dal.GetDepth(ParentID);
        }
        public void SetChildAdd(int ParentID)
        {
            this.dal.SetChildAdd(ParentID);
        }
        public M_Node GetNode(int NodeID)
        {
            return this.dal.GetNode(NodeID);
        }
        public DataTable GetNodeChildList(int ParentID)
        {
            return this.dal.GetNodeList(ParentID).Tables[0];
        }
        /// <summary>
        /// 读取节点下的子节点
        /// </summary>
        /// <param name="ParentID"></param>
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

            myDataRow = dt.NewRow();
            myDataRow["NodeID"] = 0;
            myDataRow["NodeName"] = SiteConfig.SiteInfo.SiteName;
            myDataRow["NodeType"] = 0;
            myDataRow["Depth"] = 0;
            dt.Rows.Add(myDataRow);

            this.AddDataRow(dt, ParentID);
            return dt;
        }
        public void AddDataRow(DataTable dt, int ParentID)
        {
            DataSet ds = this.dal.GetNodeList(ParentID);
            DataRow myDataRow;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                myDataRow = dt.NewRow();
                myDataRow["NodeID"] = DataConverter.CLng(dr["NodeID"]);
                myDataRow["NodeName"] = dr["NodeName"].ToString();
                myDataRow["NodeType"] = DataConverter.CLng(dr["NodeType"]);
                myDataRow["Depth"] = DataConverter.CLng(dr["Depth"]);
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
        /// <param name="ParentID"></param>
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
        public void AddDataRowNode(DataTable dt, int ParentID)
        {
            DataSet ds = this.dal.GetNodeListContain(ParentID);
            DataRow myDataRow;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                myDataRow = dt.NewRow();
                myDataRow["NodeID"] = DataConverter.CLng(dr["NodeID"]);
                int Dep = DataConverter.CLng(dr["Depth"].ToString());
                string str = "";
                if (Dep > 0)
                {
                    for (int i = 1; i <= Dep-1 ; i++)
                    {
                        str = str + "";
                    }
                    str = str + "|-";
                }
                myDataRow["NodeName"] = str+dr["NodeName"].ToString();
                dt.Rows.Add(myDataRow);
                if (DataConverter.CLng(dr["Child"]) > 0)
                {
                    AddDataRowNode(dt, DataConverter.CLng(dr["NodeID"]));
                }
            }
        }
        public DataTable GetSingleList()
        {
            return this.dal.GetSingleList();
        }
        public DataTable GetCreateAllList()
        {
            return this.dal.GetCreateAllList();
        }
        public DataTable GetCreateListByID(string IDArr)
        {
            return this.dal.GetCreateListByID(IDArr);
        }
        public DataTable GetCreateSingleByID(string IDArr)
        {
            return this.dal.GetCreateSingleByID(IDArr);
        }
        public void InitTreeNode(TreeNodeCollection Nds, int ParentID,int t)
        {
            string search = "";
            switch (t)
            {
                case 0:
                    search = "~/" + this.GetManagePath() + "/Content/ContentManage.aspx?NodeID=";
                    break;
                case 1:
                    search = "~/" + this.GetManagePath() + "/Content/CommentManage.aspx?NodeID=";
                    break;
                case 2:
                    search = "~/" + this.GetManagePath() + "/Content/ContentRecycle.aspx?NodeID=";
                    break;
                default:
                    search = "~/" + this.GetManagePath() + "/Content/ContentManage.aspx?NodeID=";
                    break;
            }
            TreeNode tmpNd;            
            DataSet ds = this.dal.GetNodeListContain(ParentID);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr["NodeID"].ToString();
                tmpNd.Text = dr["NodeName"].ToString();
                tmpNd.NavigateUrl = search + tmpNd.Value;
                
                tmpNd.Target = "main_right";
                if (DataConverter.CLng(dr["Child"]) <= 0)
                    tmpNd.ImageUrl = "~/TreeLineImages/plus.gif";
                else
                    tmpNd.ImageUrl = "";

                tmpNd.ToolTip = dr["Tips"].ToString();
                Nds.Add(tmpNd);
                if(DataConverter.CLng(dr["Child"])>0)
                {
                    InitTreeNode(tmpNd.ChildNodes, DataConverter.CLng(tmpNd.Value),t);
                }
            }
        }
        public void InitTreeNodeUser(TreeNodeCollection Nds, int ParentID,int t)
        {
            string search = "";
            switch(t)
            {
                case 1:
                    search ="~/User/Content/MyContent.aspx?";
                    break;
                case 2:
                    search = "~/User/Content/MyContent.aspx?type=UnAudit&";
                    break;
                case 3:
                    search = "~/User/Content/MyContent.aspx?type=Audit&";
                    break;
                case 4:
                    search = "~/User/Content/MyFavori.aspx?";
                    break;
                case 5:
                    search = "~/User/Content/MyComment.aspx?";
                    break;
            }            
            TreeNode tmpNd;
            DataSet ds = this.dal.GetNodeListContain(ParentID);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr["NodeID"].ToString();
                tmpNd.Text = dr["NodeName"].ToString();
                tmpNd.NavigateUrl = search + "NodeID=" + tmpNd.Value;
                tmpNd.Target = "main_right";
                tmpNd.ImageUrl = "~/TreeLineImages/plus.gif";
                tmpNd.ToolTip = dr["Tips"].ToString();
                Nds.Add(tmpNd);
                if (DataConverter.CLng(dr["Child"]) > 0)
                {
                    InitTreeNodeUser(tmpNd.ChildNodes, DataConverter.CLng(tmpNd.Value),t);
                }
            }
        }
        public string GetManagePath()
        {
            return SiteConfig.SiteOption.ManageDir.ToLower(CultureInfo.CurrentCulture);
        }
        public void AddModelTemplate(int NodeID, int ModelID, string ModelTemplate)
        {
            dal.AddModelTemplate(NodeID, ModelID, ModelTemplate);
        }
        public bool IsExistTemplate(int NodeID, int ModelID)
        {
            return dal.IsExistTemplate(NodeID, ModelID);
        }
        public void DelModelTemplate(int NodeID, string ModelIDArr)
        {
            dal.DelModelTemplate(NodeID, ModelIDArr);
        }
        public void UpdateModelTemplate(int NodeID, int ModelID, string ModelTemplate)
        {
            dal.UpdateModelTemplate(NodeID, ModelID, ModelTemplate);
        }
        public void DelTemplate(int NodeID)
        {
            dal.DelTemplate(NodeID);
        }
        public string GetModelTemplate(int NodeID, int ModelID)
        {
            return dal.GetModelTemplate(NodeID, ModelID);
        }
    }
}