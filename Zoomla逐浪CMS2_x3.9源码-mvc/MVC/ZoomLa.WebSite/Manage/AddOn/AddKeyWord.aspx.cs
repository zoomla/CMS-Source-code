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
using ZoomLa.Common;
using ZoomLa.Model;
using System.IO;
using System.Text;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class AddKeyWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["KWId"]))
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='KeyWordManage.aspx'>作者管理</a></li><li class='active'>修改关键字</li>");
            else
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='AuthorManage.aspx'>作者管理</a></li><li class='active'>添加关键字</li>");
            int DSId = 0;
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Action"] != null)
                {
                    if (Request.QueryString["Action"] == "Modify")
                    {
                        DSId = Convert.ToInt32(Request.QueryString["KWId"].Trim());
                        InItModify(DSId);
                    }

                }
            }

        }
        private void InItModify(int Kwid)
        {
            B_KeyWord bkeyword = new B_KeyWord();
            M_KeyWord mkeyword = bkeyword.GetKeyWordByid(Kwid);
            TxtKeywordText.Text = mkeyword.KeywordText;
            RadlKeywordType.SelectedValue = mkeyword.KeywordType.ToString();
            TxtPriority.Text = mkeyword.Priority.ToString();
            EBtnModify.Visible = true;
            EBtnSubmit.Visible = false;
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            B_KeyWord bkeyword = new B_KeyWord();
            M_KeyWord mkeyword = new M_KeyWord();
            mkeyword.KeywordText = TxtKeywordText.Text.ToString();
            mkeyword.KeywordType = DataConverter.CLng(RadlKeywordType.SelectedValue);
            mkeyword.Priority = DataConverter.CLng(TxtPriority.Text.ToString());
            mkeyword.LastUseTime = DateTime.Now;
            mkeyword.ArrGeneralID = "";

            if (bkeyword.Add(mkeyword))
            {
                Response.Write("<script language=javascript> alert('添加成功！');window.document.location.href='KeyWordManage.aspx';</script>");
                //Page.Response.Redirect("KeyWordManage.aspx");
            }
        }
        protected void EBtnModify_Click(object sender, EventArgs e)
        {
            B_KeyWord bkeyword = new B_KeyWord();
            M_KeyWord mkeyword = new M_KeyWord();
            mkeyword.KeyWordID = Convert.ToInt32(Request.QueryString["KWId"].Trim());
            mkeyword.KeywordText = TxtKeywordText.Text.ToString();
            mkeyword.KeywordType = DataConverter.CLng(RadlKeywordType.SelectedValue);
            mkeyword.Priority = DataConverter.CLng(TxtPriority.Text.ToString());
            mkeyword.LastUseTime = DateTime.Now;
            mkeyword.ArrGeneralID = "";

            if (bkeyword.Update(mkeyword))
            {
                Response.Write("<script language=javascript> alert('修改成功！');window.document.location.href='KeyWordManage.aspx';</script>");
            }
        }
    }
}