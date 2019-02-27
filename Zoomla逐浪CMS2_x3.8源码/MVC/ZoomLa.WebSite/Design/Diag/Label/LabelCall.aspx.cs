namespace ZoomLaCMS.Design.Diag.Label
{
    using Newtonsoft.Json;
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    public partial class LabelCall : System.Web.UI.Page
    {
        B_Admin badmin = new B_Admin();
        B_Label labelBll = new B_Label();
        B_FunLabel bfun = new B_FunLabel();
        B_CreateHtml createBll = new B_CreateHtml();
        public string Action { get { return Request["Action"] ?? ""; } }
        //标签名
        public string LabelName { get { return Request.QueryString["labelName"]; } }
        //通过其完成跳转(子动态标签)
        public string LName { get { return Request.QueryString["LName"]; } }
        public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                #region ajax
                string result = "";
                switch (Action)
                {
                    case "custom":
                        {
                            string cate = Request.Form["cate"];
                            DataTable dt = labelBll.SelAllLabel(cate);
                            result = JsonConvert.SerializeObject(dt);
                        }
                        break;
                    case "field":
                        {
                            int labelid = DataConverter.CLng(Request.Form["labelid"]);
                            if (labelid < 1) { break; }
                            M_Label labelMod = labelBll.GetLabelXML(labelid);
                            string html = "", connStr = SqlHelper.ConnectionString;
                            html = labelBll.SetLabelColumn(labelMod.LabelField, labelMod.LabelTable, labelMod.LableName, connStr);
                            result = StringHelper.Base64StringEncode(html);
                        }
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                #endregion
            }
            B_Permission.CheckAuthEx("design");
            if (!IsPostBack)
            {
                Design_Btn.Visible = string.IsNullOrEmpty(Mid);
                Save_Btn.Visible = !string.IsNullOrEmpty(Mid);
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
                DealInvoke();
                Alias_T.Text = LabelName;
                if (!string.IsNullOrEmpty(LName))
                {
                    if (badmin.CheckLogin()) { Response.Redirect(CustomerPageAction.customPath2 + "Template/LabelSQL.aspx?LabelName=" + HttpUtility.UrlEncode(LabelName)); }
                    else { function.WriteErrMsg("修改动态标签,必须以管理员身份登录"); }
                }
            }
        }
        private void DealInvoke()
        {
            M_Label labelinfo = labelBll.GetLabelXML(LabelName);
            function.Script(this, "cit2('" + labelinfo.LableType + "','" + labelinfo.LableName + "');");
        }
        protected void Design_Btn_Click(object sender, EventArgs e)
        {
            string label = textContent.Text;
            string html = createBll.CreateHtml(label);
            html = StringHelper.Base64StringEncode(html);
            function.Script(this, "LabelToDesign(\"" + StringHelper.Base64StringEncode(label) + "\",\"" + html + "\");");
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            string label = textContent.Text;
            string html = createBll.CreateHtml(label);
            html = StringHelper.Base64StringEncode(html);
            function.Script(this, "SaveEdit(\"" + StringHelper.Base64StringEncode(label) + "\",\"" + html + "\");");
        }
    }
}