using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

namespace ZoomLaCMS.Questions
{
    public partial class Default : System.Web.UI.Page
    {
        private B_Questions_Class cll = new B_Questions_Class();
        private B_Questions_Knowledge Kll = new B_Questions_Knowledge();
        protected B_User ull = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable tabs = cll.GetSelectByC_ClassId(0);
            this.Repeater1.DataSource = tabs;
            this.Repeater1.DataBind();
            if (Request.QueryString["id"] != null)
            {
                int classid = DataConverter.CLng(Request.QueryString["id"]);
                DataTable clastable = cll.GetSelectByC_ClassId(classid);
                M_Questions_Class ins = cll.GetSelect(classid);
                string search = "";
                TreeNode tmpNd;
                tmpNd = new TreeNode();
                tmpNd.Value = "0";
                tmpNd.Text = ins.C_ClassName;
                tmpNd.NavigateUrl = search;
                tmpNd.ToolTip = ins.C_ClassName;//根节点
                tmpNd.NavigateUrl = "javascript:main.location='Paper.aspx?cid=" + classid.ToString() + "';TreeView_ToggleNode(tvNav_Data,1,tvNavn1,'t',tvNavn1Nodes);";
                tvNav.Nodes.Add(tmpNd);
                InitTreeNode(tmpNd.ChildNodes, classid);
                tvNav.ExpandDepth = 1;
                tmpNd.SelectAction = TreeNodeSelectAction.Expand;
            }
        }
        public void InitTreeNode(TreeNodeCollection Nds, int ParentID)
        {
            TreeNode tmpNd;
            DataTable ds = this.cll.GetSelectByC_ClassId(ParentID);
            foreach (DataRow dr in ds.Rows)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr["C_Id"].ToString();
                tmpNd.Text = dr["C_ClassName"].ToString();

                if (this.Kll.GetSelectByCid(DataConverter.CLng(dr["C_Id"].ToString())).Count > 0)
                {
                    tmpNd.NavigateUrl = "javascript:main.location='Paper.aspx?cid=" + dr["C_Id"].ToString() + "';TreeView_ToggleNode(tvNav_Data,1,tvNavn1,'t',tvNavn1Nodes);";
                }
                else
                {
                    tmpNd.NavigateUrl = "Paper.aspx?cid=" + dr["C_Id"].ToString() + "";
                    tmpNd.Target = "main";
                }
                tmpNd.ToolTip = dr["C_ClassName"].ToString();
                Nds.Add(tmpNd);

                if (this.Kll.GetSelectByCid(DataConverter.CLng(dr["C_Id"].ToString())).Count > 0)
                {
                    InitKllTreeNode(tmpNd.ChildNodes, DataConverter.CLng(dr["C_Id"].ToString()));
                }
            }
            if (ds != null)
                ds.Dispose();
        }

        public void InitKllTreeNode(TreeNodeCollection Nds, int ParentID)
        {
            TreeNode tmpNd;
            List<M_Questions_Knowledge> ds = this.Kll.GetSelectByCid(ParentID);

            foreach (M_Questions_Knowledge dr in ds)
            {
                tmpNd = new TreeNode();
                tmpNd.Value = dr.K_id.ToString();
                tmpNd.Text = dr.K_name.ToString();
                tmpNd.NavigateUrl = "Paper.aspx?kid=" + dr.K_id.ToString() + "&cid=" + ParentID;
                tmpNd.Target = "main";
                tmpNd.ToolTip = dr.K_name.ToString();
                Nds.Add(tmpNd);
            }
            ds.Clear();
        }

        protected string GetClassName()
        {
            int id = DataConverter.CLng(Server.HtmlEncode(Request.QueryString["id"]));
            return cll.GetSelect(id).C_ClassName;
        }
    }
}