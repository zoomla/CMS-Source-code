using System;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Design_SPage_Default : System.Web.UI.Page
{
    B_SPage_Page pageBll = new B_SPage_Page();
    public M_SPage_Page pageMod = null;
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged(Request.RawUrl);
        if (!IsPostBack)
        {
            if (function.isAjax())
            {
                M_APIResult retMod = new M_APIResult(M_APIResult.Failed);
                string action = (Request["action"] ?? "").ToLower();
                switch (Request["action"])
                {
                    case "save":
                        {
                            string layouts = Request.Form["layouts"];
                            pageMod = pageBll.SelReturnModel(Mid);
                            pageMod.Layouts = layouts;
                            pageBll.UpdateByID(pageMod);
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    case "savecomp":
                        {
                            string comp = Request.Form["comp"];
                            pageMod = pageBll.SelReturnModel(Mid);
                            pageMod.Comps = comp;
                            pageBll.UpdateByID(pageMod);
                            retMod.retcode = M_APIResult.Success;
                        }
                        break;
                    default:
                        retMod.retmsg = "[" + action + "]不存在";
                        break;
                }
                Response.Clear(); Response.Write(retMod.ToString()); Response.Flush(); Response.End();
            }
            switch (Request["action"])
            {
                case "new":
                    {
                        pageMod = new M_SPage_Page();
                        pageMod.PageName = "新页面";
                        pageMod.ID = pageBll.Insert(pageMod);
                        Response.Redirect("default.aspx?ID=" + pageMod.ID);
                    }
                    break;
                default:
                    break;
            }
            pageMod = pageBll.SelReturnModel(Mid);
            if (pageMod == null) { function.WriteErrMsg("页面不存在"); }
        }
    }
}