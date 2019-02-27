using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;

public partial class Design_Diag_Label_InsertHtml : System.Web.UI.Page
{
    public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Save_Btn.Visible = string.IsNullOrEmpty(Mid);
            Edit_Btn.Visible = !Save_Btn.Visible;
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        //string label = textContent.Text;
        //string html = createBll.CreateHtml(label);
        //base64后传入页面,用于避免回车与换行符致Json解析失效
        string html =StringHelper.Base64StringEncode(Html_T.Text);
        function.Script(this, "LabelToDesign(\"" + html + "\",\"" + html + "\");");
    }
    protected void Edit_Btn_Click(object sender, EventArgs e)
    {
        //string label = textContent.Text;
        //string html = createBll.CreateHtml(label);
        string html = StringHelper.Base64StringEncode(Html_T.Text);
        function.Script(this, "SaveEdit(\"" + html + "\",\"" + html + "\");");
    }
}