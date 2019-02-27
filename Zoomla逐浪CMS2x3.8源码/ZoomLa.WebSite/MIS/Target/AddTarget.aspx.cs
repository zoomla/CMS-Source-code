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
    DataTable dt = new DataTable();
     protected void Page_Load(object sender, EventArgs e)
     {
          
     }
    protected void Button_Click(object sender, EventArgs e)
     {
        if (Request.QueryString["ID"] != null)
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
        model.Inputer = TxtInputer.Value;
        model.Pic = TxtPic.Text;
        model.Content = TxtContent1.Text;
        model.Rate = DataConverter.CLng(TxtSchedule.Text);
         
        if (Request.QueryString["ID"] != null)
        {
            bll.UpdateByID(model);
            if (ParentID.Value != "0")
            {
                function.WriteSuccessMsg("修改成功！", "Default.aspx?ID=" + ParentID.Value);
            }
            else
            {
                function.WriteSuccessMsg("修改成功！", "Default.aspx?ID=" + Request["ID"]);
            }
        }
        else if (TextTitle.Text.Trim() != null && TextTitle.Text.Trim() != "")
        {
            DataTable dt = bll.SelByField("Title", TextTitle.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                LblMessage.Text = "<font color=red>此目标已存在，请重新输入！</font>";
            }
            else
            {
            }
        }
        else
        {
            LblMessage.Text = "<font color=red>请输入目标信息！</font>";
        }
    }


    protected void Bind1()
    {
    
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string drType = this.drType.SelectedValue;
        string TxtKey = this.TxtKey.Text;
        dt = bll.SelByUTT(buser.GetLogin().UserName, drType, TxtKey);
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
    }
    protected string GetLong(string id)
    {
        return "";
    }
}