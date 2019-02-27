using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Content.Flow
{
    public partial class FlowManager : CustomerPageAction
    {
        B_Flow bf = new B_Flow();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteConfig.SiteOption.ContentConfig == 1)
            {
                if (!IsPostBack)
                {
                    string action = Request.QueryString["Action"];
                    string id = Request.QueryString["id"];
                    if (action == "copy")
                    {
                        M_Flow mf = bf.GetFlowById(Convert.ToInt32(id));
                        if (mf != null)
                        {
                            bf.AddFlow(mf.FlowName + "复制", mf.FlowDepict);
                        }
                    }
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li class='active'>流程管理<a href='AddFlow.aspx'>[添加流程]</a></li>");

            //Call.SetBreadCrumb(Master, "<li>系统设置</li><li><a href='FlowManager.aspx'>流程管理</a></li>");
        }
        protected void gwFlow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "javascript:location.href='ModifyFlow.aspx?id=" + EGV.DataKeys[e.Row.RowIndex].Value + "'");//双击事件
            }
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}