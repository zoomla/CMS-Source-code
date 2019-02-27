using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.BLL.User.Develop;
using ZoomLa.Model.User.Develop;
using ZoomLa.Model;
public partial class manage_Page_AddSheetStyle : CustomerPageAction
{
    //B_Zone_SheetStyle bzss = new B_Zone_SheetStyle();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "AddPageStyle"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!IsPostBack)
        {
            string menu = DataSecurity.FilterBadChar(Request.QueryString["menu"]);
            int sid = Convert.ToInt32(Request.QueryString["sid"]);
            if (menu == "edit")
            {
                //M_Zone_SheetStyle mzs = bzss.Select(sid);
                //this.Alias.Text = mzs.Alias;
                //this.Lname.Text = mzs.Lname;
                //this.ShowImg.Text = mzs.Img;
                //this.Price.Text = mzs.Price.ToString();
                //this.lblid.Value = mzs.ID.ToString();
                //this.Ltype.SelectedValue = mzs.Groups.ToString();
                //this.Button1.Text = "修改";
                //Label1_Hid.Value = "修改样式";
            }
            else
            {
                Label1_Hid.Value = "添加样式";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li> <li><a href='SheetStyleManage.aspx'>黄页标签管理</a></li><li>" + Label1_Hid.Value + "</li>");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //M_Zone_SheetStyle mzs = new M_Zone_SheetStyle();
        //mzs.Alias = Alias.Text.Trim();
        //mzs.Lname = Lname.Text.Trim();
        //mzs.Img = this.ShowImg.Text.Trim();
        //mzs.Price = Convert.ToDecimal(this.Price.Text.Trim());
        //mzs.Groups = Convert.ToInt32(Ltype.SelectedValue);
        //if (lblid.Value == "0")
        //{
        //    int a = bzss.Insert(mzs);
        //    if (a != 0)
        //    {
        //        function.WriteSuccessMsg("添加成功!", "SheetStyleManage.aspx");
        //    }
        //    else
        //    {
        //        function.WriteErrMsg("添加失败!");
        //    }
        //}
        //else
        //{
        //    //int sid = Convert.ToInt32(Request.QueryString["sid"]);
        //    M_Zone_SheetStyle mzss = new M_Zone_SheetStyle();
        //    mzss.Alias = Alias.Text;
        //    mzss.Lname = Lname.Text;
        //    mzss.Groups = Convert.ToInt32(Ltype.SelectedValue);
        //    mzss.Img = this.ShowImg.Text.Trim();
        //    mzss.Price = Convert.ToDecimal(Price.Text);
        //    mzss.ID = Convert.ToInt32(this.lblid.Value);
        //    bool b = bzss.Update(mzss);
        //    if (b == true)
        //    {
        //        function.WriteSuccessMsg("修改成功!", "SheetStyleManage.aspx");
        //    }
        //    else
        //    {
        //        function.WriteErrMsg("修改失败!");
        //    }
        //}
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("SheetStyleManage.aspx");
    }
}