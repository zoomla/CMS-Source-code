using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class MIS_AddMis : System.Web.UI.Page
{
    B_Mis bll = new B_Mis();
    M_Mis model = new M_Mis();
    B_User buser = new B_User();
    new  int ID = 0;
     int PID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin(Request.Url.LocalPath);
        if (!IsPostBack)
        {
            ID = DataConverter.CLng(Request.QueryString["ID"]);
            PID = DataConverter.CLng(Request.QueryString["ParentID"]);
            model = bll.SelReturnModel(ID);
            if (function.IsNumeric(Request.QueryString["ID"]))
            {
                TextTitle.Text = model.Title;
                TextStatus.Text = model.Status.ToString();
                TextJoiner.Text = model.Joiner;
                TextType.Text = model.Type.ToString();
                TextContent.Text = model.Content.ToString();
                StarDate.Text = model.ComTime.ToString();
                EndDate.Text = model.limitTime.ToString();
                ParentID.Value = model.ParentID.ToString();
                
            }
        }
    }
    protected void Button_Click(object sender, EventArgs e)
    {
        if (function.IsNumeric(Request.QueryString["ID"]))
        {
            int ID = DataConverter.CLng(Request.QueryString["ID"]);
            model = bll.SelReturnModel(ID);
        }
        model.Title = TextTitle.Text;
        model.Status = DataConverter.CLng(TextStatus.Text);
        model.Joiner = TextJoiner.Text;
        model.Type = DataConverter.CLng(TextType.Text);
        model.ComTime = DataConverter.CDate(StarDate.Text);
        model.limitTime = DataConverter.CDate(EndDate.Text);
        model.ParentID = DataConverter.CLng(ParentID.Value);
        if (function.IsNumeric(Request.QueryString["ID"]))
        {
            bll.UpdateByID(model);
            function.WriteSuccessMsg("修改成功！", "Mis.aspx?ID=" + Request["ID"]);
        }
        else if (TextTitle.Text.Trim() != null && TextTitle.Text.Trim() != "")
        {
           
        }
        else
        {
            LblMessage.Text = "<font color=red>请输入目标信息！</font>";
        }
       
    }
}