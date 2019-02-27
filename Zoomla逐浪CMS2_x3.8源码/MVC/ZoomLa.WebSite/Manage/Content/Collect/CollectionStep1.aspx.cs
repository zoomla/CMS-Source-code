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
        B_Model bll = new B_Model();
        B_Node nodeBll = new B_Node();
        B_CollectionItem bc = new B_CollectionItem();
        string title = "添加设置采集第一步";
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
                DataTable dt = nodeBll.GetNodeListContainXML(0);
                Node_L.Text = "<select id='node_dp' name='node_dp' class='form-control text_300'>" + nodeBll.CreateDP(dt) + "</select>";
                if (ItemID > 0)
                {
                    M_CollectionItem mc = bc.GetSelect(ItemID);
                    txtItemName.Text = mc.ItemName;
                    txtSiteName.Text = mc.SiteName;
                    Proto_DP.SelectedValue = mc.CollUrl.Contains("http:") ? "http://" : "https://";
                    txtUrl.Text = mc.PureUrl;
                    txtNum.Text = mc.CollNum.ToString();
                    ddlModel.SelectedValue = mc.ModeID.ToString();
                    rblCoding.SelectedValue = mc.CodinChoice.ToString();
                    txtContext.Text = mc.CollContext;
                    ddlModel.Enabled = false;
                    function.Script(this,"setNodeDP('"+mc.NodeID+"');");
                    title = "<span style='color:red;'>采集项目设置</span> >> <a title=\"列表页采集设置\" href=\"CollectionStep2.aspx?Action=Modify&amp;ItemId=" + ItemID + "\">列表页采集设置</a> >> <a title=\"内容页采集设置\" href=\"CollectionStep3.aspx?Action=Modify&amp;ItemId=" + ItemID + "\">内容页采集设置</a>";
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CollectionManage.aspx'>信息采集</a></li><li class='active'><a href='" + Request.RawUrl + "'>采集管理</a></li>");
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
            mc.NodeID = Request.Form["node_dp"];
            if (DataConverter.CLng(mc.NodeID) < 1 || mc.ModeID < 1) { function.WriteErrMsg("未指定节点或模型"); }
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