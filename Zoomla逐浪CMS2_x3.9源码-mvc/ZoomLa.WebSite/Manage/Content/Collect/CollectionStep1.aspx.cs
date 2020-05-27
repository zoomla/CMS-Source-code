using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZoomLa.BLL.Helper;

namespace ZoomLaCMS.Manage.Content.Collect
{
    public partial class CollectionStep1 : CustomerPageAction
    {
        private B_Model bll = new B_Model();
        private B_Node bn = new B_Node();
        private B_CollectionItem bc = new B_CollectionItem();
        protected string title = "添加设置采集第一步";
        protected string type = "添加采集项目";
        private int ItemID
        {
            get { return DataConverter.CLng(Request.QueryString["ItemID"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlModel.DataSource = this.bll.GetList();
                ddlModel.DataBind();
                if (ItemID > 0)
                {
                    M_CollectionItem mci = bc.GetSelect(ItemID);
                    txtItemName.Text = mci.ItemName;
                    txtSiteName.Text = mci.SiteName;
                    Proto_DP.SelectedValue = mci.CollUrl.Contains("http:") ? "http://" : "https://";
                    txtUrl.Text = mci.PureUrl;
                    txtNum.Text = mci.CollNum.ToString();
                    ddlModel.SelectedValue = mci.ModeID.ToString();
                    rblCoding.SelectedValue = mci.CodinChoice.ToString();
                    txtContext.Text = mci.CollContext;
                    hfNode.Value = SetNode(mci.NodeID);
                    ddlModel.Enabled = false;
                    type = "修改采集项目";
                    title = "<span style='color:red;'>采集项目设置</span> >> <a title=\"列表页采集设置\" href=\"CollectionStep2.aspx?Action=Modify&amp;ItemId=" + ItemID + "\">列表页采集设置</a> >> <a title=\"内容页采集设置\" href=\"CollectionStep3.aspx?Action=Modify&amp;ItemId=" + ItemID + "\">内容页采集设置</a>";
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CollectionManage.aspx'>信息采集</a></li><li><a href='CollectionManage.aspx'>项目管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>添加采集项目</a></li>");
        }

        private string SetNode(string noid)
        {
            StringBuilder str = new StringBuilder();
            string[] nodes = noid.Split(new char[] { ',' });
            foreach (string node in nodes)
            {
                if (!string.IsNullOrEmpty(node))
                    str.Append(node + "|" + GetParent(DataConverter.CLng(node)) + ",");
            }
            return str.ToString();
        }
        private string GetParent(int ParentID)
        {
            string str = "";
            M_Node mn = bn.GetNodeXML(ParentID);
            if (mn.ParentID > 0)
            {
                str = GetParent(mn.ParentID) + "&gt;&gt;" + mn.NodeName;
            }
            else
            {
                str = mn.NodeName;
            }
            return str;
        }

        protected void Button1_Click(object sender, EventArgs e)//点击开始采集
        {
            M_CollectionItem mc = new M_CollectionItem();
            mc.CollContext = txtContext.Text;
            mc.CodinChoice = DataConverter.CLng(rblCoding.SelectedValue);//采集字符编码
            mc.CollNum = (txtNum.Text == "") ? 0 : DataConverter.CLng(txtNum.Text);
            mc.CollUrl = StrHelper.UrlDeal(txtUrl.Text, Proto_DP.SelectedValue);
            mc.ItemName = txtItemName.Text;
            mc.SiteName = txtSiteName.Text;
            mc.ModeID = DataConverter.CLng(ddlModel.SelectedValue);
            mc.NodeID = hfNode.Value;
            mc.CodinChoice = Convert.ToInt32(rblCoding.SelectedValue);
            if (ItemID > 0)
            {
                M_CollectionItem mci = bc.GetSelect(ItemID);
                mc.CItem_ID = ItemID;
                mc.AddTime = mci.AddTime;
                mc.InfoPageSettings = mci.InfoPageSettings;
                mc.ListSettings = mci.ListSettings;
                mc.PageSettings = mci.PageSettings;
                mc.LinkList = mci.LinkList;
                mc.LastTime = mci.LastTime;
                mc.Switch = mci.Switch;
                mc.State = mci.State;
                bc.GetUpdate(mc);
                Response.Redirect("CollectionStep2.aspx?Action=Modify&ItemId=" + ItemID);
            }
            else
            {
                mc.AddTime = DateTime.Now;
                int newid = bc.GetInsert(mc);
                Response.Redirect("CollectionStep2.aspx?Action=Insert&ItemId=" + newid);
            }
        }
    }
}