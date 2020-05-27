using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Common;

public partial class manage_Shop_profile_LmUserListTree : CustomerPageAction
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "shop/ProductManage.aspx'>商城管理</a></li><li><a href='StatisticsBriefing.aspx'>推广返利</a></li><li>联盟会员树状图</li>");
        if (!IsPostBack)
        {
            GetUserTree(UserTree.Nodes,0);
        }
    }

    public void GetUserTree(TreeNodeCollection tv,int parentid)
    {
       
     
    }
}