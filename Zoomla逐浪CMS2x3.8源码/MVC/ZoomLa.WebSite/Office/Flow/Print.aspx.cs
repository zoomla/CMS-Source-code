using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.MIS;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.MIS.OA.Flow
{
    public partial class Print : System.Web.UI.Page
    {
        B_OA_Document oaBll = new B_OA_Document();
        B_MisProcedure proceBll = new B_MisProcedure();
        B_Content conBll = new B_Content();
        private int AppID { get { return DataConverter.CLng(Request.QueryString["AppID"]); } }
        private string ascx { get { return ViewState["ascx"] as string; } set { ViewState["ascx"] = value; } }
        private B_OAFormUI OAFormUI
        {
            get
            {
                var control = OAForm_Div.FindControl("ascx_" + ascx);
                if (control == null)//加载默认
                {
                    control = OAForm_Div.FindControl("ascx_def");
                    control.Visible = true;
                    return (B_OAFormUI)control;
                }
                else { control.Visible = true; return (B_OAFormUI)control; }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppID < 1) { function.WriteErrMsg("未指定需要打印的文档"); }
                M_OA_Document oaMod = oaBll.SelReturnModel(AppID);
                M_MisProcedure proceMod = proceBll.SelReturnModel(oaMod.ProID);
                ascx = proceMod.PrintTlp;
                OAFormUI.InitControl(ViewState, Convert.ToInt32(proceMod.FormInfo));
                OAFormUI.Title_ASCX = oaMod.Title;
                OAFormUI.SendDate_ASCX = oaMod.SendDate.ToString("yyyy年MM月dd日");
                OAFormUI.NO_ASCX = oaMod.No;
                DataTable dtContent = conBll.GetContent(Convert.ToInt32(oaMod.Content));
                OAFormUI.dataRow = dtContent.Rows[0];
                OAFormUI.MyBind();
            }
        }
    }
}