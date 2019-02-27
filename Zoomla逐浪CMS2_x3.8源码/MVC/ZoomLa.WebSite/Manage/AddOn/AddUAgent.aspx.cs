namespace ZoomLaCMS.Manage.AddOn
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using System.Data;
    using ZoomLa.Common;
    public partial class AddUAgent : System.Web.UI.Page
    {
        //B_UAgent bll = new B_UAgent();
        //M_UAgent model = new M_UAgent();
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href=\"UAgent/Blog\">自设配设置</a></li><li>添加自事故配置</li>");
            function.AccessRulo();
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    int ID = DataConverter.CLng(Request.QueryString["ID"]);
                    //model = bll.SelReturnModel(ID);
                    //TxtHeaders.Text = model.Headers;
                    //TxtUserAgent.Text = model.UserAgent;
                    //TxtUrl.Text = model.Url;
                    //Status.Checked = model.Status == 1 ? true : false;
                    lblText.Text = "查看/修改";
                }
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            //model.Headers = TxtHeaders.Text;
            //model.UserAgent = TxtUserAgent.Text;
            //model.Url = TxtUrl.Text.Trim();
            //if (Request.QueryString["ID"] != null)
            //{
            //    int ID = DataConverter.CLng(Request.QueryString["ID"]);
            //    //M_UAgent model2 = bll.SelReturnModel(ID);
            //    model.ID = ID;
            //    //model.Status = model2.Status;
            //    bll.UpdateByID(model);
            //    function.WriteSuccessMsg("修改成功！", "UAgent.aspx?type=" + Request["type"]);
            //}
            //else if (TxtUserAgent.Text.Trim() != null && TxtUserAgent.Text.Trim() != "")
            //{
            //    DataTable dt = bll.SelByField("UserAgent", TxtUserAgent.Text.Trim());
            //    if (dt.Rows.Count > 0)
            //    {
            //        LblMessage.Text = "<font color=red>此关键词已存在，请重新输入！</font>";
            //    }
            //    else
            //    {
            //        bll.insert(model);
            //        function.WriteSuccessMsg("添加成功!", "UAgent.aspx");
            //        LblMessage.Text = "";
            //    }
            //}
            //else
            //{
            //    LblMessage.Text = "<font color=red>请输入关键词</font>";
            //}
        }
    }
}