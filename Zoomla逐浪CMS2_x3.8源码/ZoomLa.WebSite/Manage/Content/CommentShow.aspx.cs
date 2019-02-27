using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;

using System.Collections.Generic;

public partial class Manage_I_Content_CommentShow : System.Web.UI.Page
{
    B_Comment commentBll = new B_Comment();
    B_Content contentBll = new B_Content();
    B_User userbll = new B_User();
    public int MainID 
    {
        get { return DataConverter.CLng(Request.QueryString["id"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CommentManage.aspx'>评论管理</a></li><li class='active'><a href='"+Request.RawUrl+"'>评论详情</a></li>");
            MyBind();
        }
    }
    void MyBind()
    {
        M_Comment model = commentBll.SelReturnModel(MainID);
        ID_L.Text = model.CommentID.ToString();
        M_CommonData coninfo = contentBll.GetCommonData(DataConverter.CLng(model.GeneralID));
        Title_L.Text = coninfo.Title;
        CUser_LB.Text = userbll.SeachByID(model.UserID).UserName;
        CID_L.Text = model.GeneralID.ToString();
        MsgContent_T.Text = model.Contents;
        CDate_T.Text = model.CommentTime.ToString();
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Comment model = commentBll.SelReturnModel(MainID);
        model.Contents = MsgContent_T.Text;
        model.CommentTime = DataConverter.CDate(CDate_T.Text);
        commentBll.UpdateByID(model);
        Response.Redirect("CommentManage.aspx");
    }
}