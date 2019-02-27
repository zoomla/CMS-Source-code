using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Xml;
using Microsoft.Web.Administration;
using System.Runtime.InteropServices;
using System.Security.Principal;
using ZoomLa.Components;
using System.IO;


/*
 * Does't allow duplicate name
 * Do not assign Application Pool will use the Default Pool
 * Note that index needs to be -1 before use
 * VD is VirtualDirectory
 */

namespace ZoomLaCMS.Manage.Site
{
    public partial class SitePool : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        protected B_Create b = new B_Create();
        protected B_Content bContent = new B_Content();
        protected int m_CreateCount;
        protected string siteName;

        protected ServerManager iis = new ServerManager();
        protected IISHelper iisHelper = new IISHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            IdentityAnalogue ia = new IdentityAnalogue();
            ia.CheckEnableSA();
            siteName = Server.UrlDecode(Request.Params["siteName"]);
            //ia.UndoImpersonation();
            if (function.isAjax(Request))
            {
                string action = Request.Form["action"];
                Response.End();
            }
            if (!IsPostBack)
            {
                int poolCount = iis.ApplicationPools.Count;
                int classicCount = iisHelper.GetPoolCountByMNS("Mode", ManagedPipelineMode.Classic.ToString());
                int integerCount = iisHelper.GetPoolCountByMNS("Mode", ManagedPipelineMode.Integrated.ToString());
                titleSpan.InnerText = string.Format(titleSpan.InnerText, poolCount, classicCount, integerCount);
            }
            Call.HideBread(Master);
        }
        //--------EGV4
        protected void EGV4_RowCommand(object sender, GridViewCommandEventArgs e)//ApplicationPool
        {
            switch (e.CommandName)
            {
                case "Edit2":
                    EGV4.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                    break;
                case "Save":
                    string[] s = e.CommandArgument.ToString().Split(':');
                    EGV4_Update(DataConvert.CLng(s[0]), s[1]);
                    EGV4.EditIndex = -1;
                    break;
                case "start":
                    iisHelper.StartAppPool(e.CommandArgument as string);
                    DataBind();
                    break;
                case "stop":
                    iisHelper.StopAppPool(e.CommandArgument as string);
                    DataBind();
                    break;
                case "Cancel":
                    EGV4.EditIndex = -1;
                    break;
                default: break;
            }
        }

        protected void EGV4_Update(int rowNum, string appName)
        {
            GridViewRow gr = EGV4.Rows[rowNum];
            string mode = ((DropDownList)gr.FindControl("EditMode")).SelectedValue;
            string version = ((DropDownList)gr.FindControl("EditNetVersion")).SelectedValue;

            iisHelper.ChangeMode(appName, mode);
            iisHelper.ChangeNetVersion(appName, version);
            DataBind();
        }

        protected void EGV4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState == (DataControlRowState.Alternate | DataControlRowState.Edit))
                {
                    DropDownList list = (DropDownList)e.Row.FindControl("EditNetVersion");
                    DropDownList list2 = (DropDownList)e.Row.FindControl("EditMode");
                    list.SelectedValue = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("NetVersion")).Value;
                    list2.SelectedValue = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("Mode")).Value;
                    list.DataBind(); list2.DataBind();
                }
            }

        }
    }
}