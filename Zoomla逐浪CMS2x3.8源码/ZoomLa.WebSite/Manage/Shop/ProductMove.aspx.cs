namespace ZoomLa.WebSite.Manage.Product
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using System.Collections.Generic;
    public partial class ProductMove : CustomerPageAction
    {
        B_Content bll = new B_Content();
        B_Node bnode = new B_Node();
        B_Product pll = new B_Product();
        B_Node nll = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "ProductManage")) { function.WriteErrMsg("没有权限进行此项操作"); }
                string id = base.Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                    function.WriteErrMsg("没有指定要移动的商品ID", "ProductMove.aspx");
                else
                    this.TxtContentID.Text = id.Trim();
                //this.DDLNode.DataSource = this.bnode.CreateDP(0);
                //this.DDLNode.DataTextField = "NodeName";
                //this.DDLNode.DataValueField = "NodeID";
                //this.DDLNode.DataBind();
                Node_Lit.Text = nll.CreateDP(nll.GetAllShopNode());
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li class='active'>商品批量移动</li>");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int NodeID = DataConverter.CLng(Request.Form["node_dp"]);
            string Ids = this.TxtContentID.Text;
            string[] arrlist = null;
            string nodemodeid = nll.GetNodeXML(NodeID).ContentModel.ToString();
            string pslist = "";
            arrlist = Ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (arrlist != null && arrlist.Length > 0)
            {
                for (int i = 0; i < arrlist.Length; i++)
                {
                    int pid = DataConverter.CLng(arrlist[i]);
                    string modid = pll.GetproductByid(pid).ModelID.ToString();
                    if (("," + nodemodeid + ",").IndexOf("," + modid + ",") > -1)
                    {
                        M_Product pinfo = pll.GetproductByid(pid);
                        pinfo.Nodeid = DataConverter.CLng(NodeID);
                        pll.updateinfo(pinfo);
                        pslist = pslist + pinfo.ComModelID + ",";
                    }
                }
            }
            if (pslist.Length > 0)
            {
                string IIds = pslist.Substring(0, pslist.Length - 1);

                bool flag = this.bll.MoveContent(IIds, NodeID);
                if (flag)
                    function.WriteSuccessMsg("商品移动成功", "../shop/ProductManage.aspx");
            }
            else
            {
                function.WriteSuccessMsg("商品移动失败", "../shop/ProductManage.aspx");
            }
        }
}
}