using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Design_User_Node_AddNode : System.Web.UI.Page
{
    B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
    B_Node nodeBll = new B_Node();
    B_User buser = new B_User();
    private int Pid { get { return DataConvert.CLng(ViewState["Pid"]); } set { ViewState["Pid"] = value; } }
    private int NodeID { get { return DataConvert.CLng(Request.QueryString["NodeID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Pid = DataConvert.CLng(Request.QueryString["Pid"]);
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        M_Design_SiteInfo sfMod = sfBll.SelReturnModel(mu.SiteID);
        PNodeName_L.Text = sfMod.SiteName;
        if (NodeID > 0)
        {
            M_Node nodeMod = nodeBll.SelReturnModel(NodeID);
            NodeName_T.Text = nodeMod.NodeName;
            NodePic_T.Text = nodeMod.NodePic;
            Description_T.Text = nodeMod.Description;
            Meta_T.Text = nodeMod.Meta_Keywords;
            Pid = nodeMod.ParentID;
        }
        if (Pid > 0)
        {
            M_Node pmod = nodeBll.SelReturnModel(Pid);
            if (pmod.NodeBySite != mu.SiteID) { function.WriteErrMsg("你没有权限在该栏目下添加子栏目"); }
            else 
            {
                PNodeName_L.Text = pmod.NodeName;
            }
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Node nodeMod = new M_Node();
        if (NodeID > 0)
        {
            nodeMod = nodeBll.SelReturnModel(NodeID);
        }
        nodeMod.NodeName = NodeName_T.Text;
        nodeMod.NodePic = NodePic_T.Text;
        nodeMod.Description = Description_T.Text;
        nodeMod.Meta_Keywords = Meta_T.Text;
        if (NodeID > 0) { nodeBll.UpdateByID(nodeMod); }
        else
        {
            M_UserInfo mu = buser.GetLogin();
            nodeMod.ParentID = Pid;
            nodeMod.CUName = mu.UserName;
            nodeMod.CUser = mu.UserID;
            nodeMod.NodeBySite = mu.SiteID;
            nodeMod.NodeType = 1;
            nodeBll.Insert(nodeMod);
        }
        function.WriteSuccessMsg("操作完成", "NodeList.aspx?SiteID=" + nodeMod.NodeBySite);
    }
}