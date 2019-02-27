using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_I_Content_CommentView : System.Web.UI.Page
{
    B_Comment comBll = new B_Comment();
    M_Comment comMod = new M_Comment();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["CommentID"]); } }
    public int Gid { get { return DataConvert.CLng(Request.QueryString["Gid"]); } }
    public int modeid { get { return DataConvert.CLng(Request.QueryString["Modeid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CommentManage.aspx'>评论管理</a></li><li class='active'>修改评论</li>");
        }
    }
    public void MyBind()
    {
        comMod=comBll.SelReturnModel(Mid);
        CommentID.Text = comMod.CommentID.ToString();
        Title.Text = comMod.Title;
        Contents.Text = comMod.Contents;
        CommentTime.Text = comMod.CommentTime.ToString();
        commScore.Text = comMod.Score.ToString();
        commStatus.Text = comMod.Audited ? "已审核" : "未审核";
    }
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        comMod = comBll.SelReturnModel(Mid);
        comMod.Contents = Contents.Text;
        comMod.Title = Title.Text;
        comMod.CommentTime =DataConvert.CDate(CommentTime.Text);
        comBll.UpdateByID(comMod);
        function.WriteSuccessMsg("修改成功","ShowContent.aspx?Gid="+Gid+"&Modeid="+modeid);
    }
}