using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_UserZone_AddStructure : System.Web.UI.Page
{ 
    B_Structure bll = new B_Structure();
    M_Structure model = new M_Structure();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            int ID = DataConverter.CLng(Request.QueryString["ID"]);
            model = bll.Select(ID);
            TxtProjectName.Text = model.Name;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("StructManage.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            int ID = DataConverter.CLng(Request.QueryString["ID"]);
            model = bll.Select(ID);
            model.Name = Request.Form["Name"]; 
            bll.Update(model);
            function.WriteSuccessMsg("修改成功！", "Structure.aspx");
        }
        else if (TxtProjectName.Text.Trim() != null && TxtProjectName.Text.Trim() != "")
        {
            DataTable dt = bll.SelByField("Name", TxtProjectName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                LblMessage.Text = "<font color=red>此结构名已存在，请重新输入！</font>";
            }
            else
            { 
                model.ParentID = 0;
                if (Request.QueryString["pid"] != null)
                {
                    model.ParentID = DataConverter.CLng(Request.QueryString["pid"]);
                }
                model.Opens = 1;
                model.Status = 99;
                if (!string.IsNullOrEmpty(Request["Group"]))
                    model.Group = DataConverter.CLng(Request.QueryString["Group"]);
                else model.Group = 1; 
                model.Name = TxtProjectName.Text.Trim();
                model.AddTime = DateTime.Now;
                model.UserID = buser.GetLogin().UserID;
                int id= bll.Insert(model);
                if (id > 0)
                {
                    M_UserInfo info = buser.SeachByID(buser.GetLogin().UserID);
                    info.StructureID += id;
                    buser.UpDateUser(info);
                    Response.Write("<script>alert('添加成功！！！');location.href='Structure.aspx';</script>");
                }
                LblMessage.Text = "";
            }
        }
        else
        {
            LblMessage.Text = "<font color=red>请输入项目类型名称</font>";
        }
    }
}