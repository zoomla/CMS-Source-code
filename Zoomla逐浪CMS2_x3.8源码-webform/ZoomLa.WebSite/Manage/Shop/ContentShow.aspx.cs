using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;


public partial class manage_Content_ContentShow : CustomerPageAction
{
    B_Product bll;
    B_Node node;
    public M_Product pinfo = new M_Product();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && !(Request.QueryString["id"].Equals("")))
        {
            int productId = DataConverter.CLng(Request.QueryString["id"]);
            if (productId == 0)
            {
                function.WriteErrMsg("参数错误");
            }
            else
            {
                bll = new B_Product();
                node = new B_Node();
                pinfo = bll.GetproductByid(productId);
                if (pinfo != null)
                {
                    int nodeId = pinfo.Nodeid;
                    M_Node m_node = node.GetNodeXML(nodeId);
                    NodeName.Text = m_node.NodeName;
                    AddUser_L.Text = pinfo.AddUser;
                    shopState.Text = pinfo.Sales == 1 ? "销售中" : "停售状态";
                    codes.Text = pinfo.ProClass == 1 ? "正常销售" : "特价处理";
                    codes.Text += pinfo.Isnew == 1 ? "|新品" : "";
                    codes.Text += pinfo.Ishot == 1 ? "|热销" : "";
                    codes.Text += pinfo.Isbest == 1 ? "|精品" : "";
                    ProID_L.Text = pinfo.ID.ToString();
                    title_T.Text = pinfo.Proname; 
                    lblCountHits.Text = pinfo.AllClickNum.ToString();
                    ckPrice.Text = pinfo.ShiPrice.ToString();
                    nowPrice.Text = pinfo.LinPrice.ToString();
                    B_Model m_model = new B_Model();
                    M_ModelInfo MM = m_model.GetModelById(pinfo.Nodeid);
                    string bar = " <span><a href=\"ProductManage.aspx?NodeID=" + nodeId + "\">" + node.GetNodeXML(nodeId).NodeName + "</a></span>";
                    Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='ProductManage.aspx'>商品管理</a></li><li>" + bar + "</li>"+
                        "<div class='pull-right hidden-xs'><span onclick=\"opentitle('../Content/EditNode.aspx?NodeID=" + nodeId + "','配置本节点');\" class='fa fa-cog' title='配置本节点' style='cursor:pointer;margin-left:5px;'></span></div>");

                }
            }
        }
    }
    
}
