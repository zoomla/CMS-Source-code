using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Content;
using ZoomLa.Common;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;

public partial class Manage_Copyright_WorksShow : System.Web.UI.Page
{
    B_Content_CR crBll = new B_Content_CR();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Mid < 1) { function.WriteErrMsg("未指定模型ID!"); }
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Plus/ADManage.aspx'>扩展功能</a></li><li><a  href=\"Config.aspx\">版权中心</a></li><li class=\"active\">作品详情</li>");
        }
    }
    public void MyBind()
    {
        M_Content_CR copyMod = crBll.SelReturnModel(Mid);
        if (null == copyMod) { function.WriteErrMsg("指定模型不存在!"); }
        Title_L.Text = copyMod.Title;
        Author_L.Text = copyMod.Author;
        KeyWords_L.Text = copyMod.KeyWords;
        Type_L.Text = copyMod.Type;
        RepPrice_L.Text = string.Format("{0:C}", copyMod.RepPrice) ;
        MatPrice_L.Text = string.Format("{0:C}", copyMod.MatPrice);
        FromType_L.Text = copyMod.FromType;
        CreateDate_L.Text = copyMod.CreateDate.ToShortDateString();
        FromUrl_L.Text = copyMod.FromUrl;
        Content_T.Text = copyMod.Content;
    }
}