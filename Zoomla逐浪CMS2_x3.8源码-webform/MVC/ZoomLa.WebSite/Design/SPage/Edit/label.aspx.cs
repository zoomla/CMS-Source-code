using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Design.SPage.Edit
{
    public partial class label : System.Web.UI.Page
    {
        B_Label labelBll = new B_Label();
        B_FunLabel bfun = new B_FunLabel();
        //通过其完成跳转(子动态标签)
        public string LName { get { return Request.QueryString["LName"]; } }
        public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CustomLabel_DP.DataSource = labelBll.GetLabelCateListXML();
                CustomLabel_DP.DataTextField = "name";
                CustomLabel_DP.DataValueField = "name";
                CustomLabel_DP.DataBind();
                CustomLabel_DP.Items.Insert(0, new ListItem("选择标签类型", ""));
                Field_DP.DataSource = labelBll.GetSourceLabelXML();//LabelType
                Field_DP.DataTextField = "LabelName";
                Field_DP.DataValueField = "LabelID";
                Field_DP.DataBind();
                Field_DP.Items.Insert(0, new ListItem("选择数据源标签", ""));
                lblSys.Text = bfun.GetSysLabel();
                lblFun.Text = bfun.GetFunLabel();
                //DealInvoke();
                Call.HideBread(Master);
            }
        }
        //private void DealInvoke()
        //{
        //    M_Label labelinfo = labelBll.GetLabelXML(LabelName);
        //    function.Script(this, "cit2('" + labelinfo.LableType + "','" + labelinfo.LableName + "');");
        //}
    }
}