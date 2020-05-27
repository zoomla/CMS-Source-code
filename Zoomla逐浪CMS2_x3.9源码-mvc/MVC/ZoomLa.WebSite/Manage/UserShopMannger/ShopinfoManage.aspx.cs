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
using System.Text;

namespace ZoomLaCMS.Manage.UserShopMannger
{
    public partial class ShopinfoManage : CustomerPageAction
    {
        protected B_Shopconfig sll = new B_Shopconfig();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreinfoManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                M_Shopconfig sinfos = sll.GetShopconfig();
                this.Anonymity.Checked = sinfos.Anonymity;
                this.Comment.Checked = sinfos.Comment;
                this.Dummymoney.Checked = sinfos.Dummymoney;
                this.IsOpen.Checked = sinfos.IsOpen;
                this.Pointcard.Checked = sinfos.Pointcard;
                this.goodpl.Text = sinfos.Goodpl.ToString();
                this.centerpl.Text = sinfos.Centerpl.ToString();
                this.badpl.Text = sinfos.Badpl.ToString();
                this.Auditing.Checked = sinfos.Auditing == 1 ? true : false;
                this.ScorePoint.Text = sinfos.ScorePoint.ToString();
                this.ChangOrder.Checked = sinfos.ChangeOrder;
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a></li><li class='active'>店铺信息配置</li>" + Call.GetHelp(91));
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            M_Shopconfig uinfo = sll.GetShopconfig();
            uinfo.Anonymity = Anonymity.Checked;
            uinfo.Comment = Comment.Checked;
            uinfo.Dummymoney = Dummymoney.Checked;
            uinfo.IsOpen = IsOpen.Checked;
            uinfo.Pointcard = Pointcard.Checked;
            uinfo.Goodpl = DataConverter.CLng(goodpl.Text);
            uinfo.Centerpl = DataConverter.CLng(centerpl.Text);
            uinfo.Badpl = DataConverter.CLng(badpl.Text);
            uinfo.ScorePoint = DataConverter.CLng(ScorePoint.Text);
            uinfo.BankInfo = "";
            uinfo.Auditing = Auditing.Checked ? 1 : 0;
            uinfo.ChangeOrder = ChangOrder.Checked;

            if (!sll.UpdateShopconfig(uinfo))
            {
                sll.AdddateShopconfig(uinfo);
            }
            Response.Write("<script language=javascript>alert('设置成功');location.href='ShopinfoManage.aspx';</script>");

        }
    }
}