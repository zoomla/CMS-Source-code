using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
public partial class manage_AddOn_AddStruct : CustomerPageAction
{
    B_Structure bll = new B_Structure();
    M_Structure model = new M_Structure();
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pid = DataConverter.CLng(Request.QueryString["pid"]); 
                if (Request.QueryString["ID"] != null)
                {
                    int id = DataConverter.CLng(Request.QueryString["ID"]);
                    model = bll.Select(id);
                    TxtStructName.Text = model.Name;
                    Remind_T.Text = model.Remind;
                    rd1.Checked = model.Status == 1 ? true : false;
                    lblText.Text = "查看/修改";
                    pid = model.ParentID;
                }
                if (pid == 0)
                    parent_L.Text = "根结构";
                else
                    parent_L.Text = bll.SelReturnModel(pid).Name;
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='StructList.aspx'>组织结构</a></li><li class='active'>添加组织结构</li>");
            }
        }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("StructManage.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            int ID = DataConverter.CLng(Request.QueryString["ID"]);
            model = bll.Select(ID);
            model.Name = TxtStructName.Text;
            model.Remind = Remind_T.Text;
            model.Status = rd1.Checked ? 1 : 0;
            bll.Update(model);
            function.Script(this, "alert('修改成功！')");
        }
        else if (!string.IsNullOrEmpty(TxtStructName.Text.Trim()))
        {
            if (bll.IsExist(TxtStructName.Text))
            {
                LblMessage.Text = "<font color=red>此结构名已存在，请重新输入！</font>";
            }
            else
            {
                model.ParentID = DataConverter.CLng(Request.QueryString["pid"]);
                model.Opens = 1;
                model.Status = 1;
                model.Group = (int)M_Structure.GroupType.User;//扩展的话，根据传入的GroupType,分不同的组
                model.Name = TxtStructName.Text.Trim();
                model.AddTime = DateTime.Now;
                model.UserID = badmin.GetAdminLogin().AdminId;
                model.Status = rd1.Checked ? 1 : 0;
                bll.Insert(model);
                function.WriteSuccessMsg("添加成功!", "StructList.aspx?type=" + Request.QueryString["type"]);
                LblMessage.Text = "";
            }
        }
        else
        {
            LblMessage.Text = "<font color=red>请输入结构名称</font>";
        }
    }
}