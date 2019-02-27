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
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL.User;
using ZoomLa.Model.User;

namespace ZoomLaCMS.Manage.Zone
{
    public partial class ZoneStyleAdd : CustomerPageAction
    {
        B_User_BlogStyle bsBll = new B_User_BlogStyle();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Mybind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li><a href='ZoneStyleManage.aspx'>空间模板管理</a></li><li class='active'>添加空间模板</li>");
            }
        }
        public void Mybind()
        {
            if (Mid > 0)
            {
                M_User_BlogStyle model = bsBll.SelReturnModel(Mid);
                StyleName_T.Text = model.StyleName;
                StylePic_T.Text = model.StylePic;
                UserIndexStyle_T.Text = model.UserIndexStyle.Trim(new char[] { '/' });
                EBtnSubmit.Text = "修改";
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_User_BlogStyle bsMod = Mid > 0 ? bsBll.SelReturnModel(Mid) : new M_User_BlogStyle();
            bsMod.StyleName = this.StyleName_T.Text;
            bsMod.StylePic = this.StylePic_T.Text;
            bsMod.UserIndexStyle = "/" + this.UserIndexStyle_T.Text;
            if (Mid > 0) { bsBll.UpdateByID(bsMod); }
            else { bsBll.Insert(bsMod); }
            function.WriteSuccessMsg("操作成功!", "ZoneStyleManage.aspx");
        }
    }
}