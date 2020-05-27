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

namespace ZoomLaCMS.Manage.Pay
{
    public partial class PayPlatManage :CustomerPageAction
    {
        private B_PayPlat bll = new B_PayPlat();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.other, "PayManage");
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href='PayPlatManage.aspx'>在线支付平台</a></li><li class='active'>支付平台管理<a href=\"AddPayPlat.aspx\">[添加支付平台]</a></li>" + Call.GetHelp(36));
            }
        }
        public string GetPayClass()
        {
            switch (Convert.ToInt32(Eval("PayClass")))
            {
                case 12:
                    return "支付宝[即时到帐]";
                case 1:
                    return "支付宝实物双工";
                case 100:
                    return "支付宝货到付款";
                case 2:
                    return "快钱支付";
                case 3:
                    return "网银在线";
                case 4:
                    return "财付通";
                case 5:
                    return "易宝支付";
                case 9:
                    return "中国银联";
                case 10:
                    return "易宝网";
                case 13:
                    return "汇付天下";
                case 16:
                    return "重庆摩宝";
                case 99:
                    return "线下支付";
                case 21:
                    return "微信支付";
                case 22:
                    return "宝付支付";
                case 23:
                    return "江西工行";
                case 24:
                    return "双乾支付";
                case 25:
                    return "贝付通";
                case 26:
                    return "江西建行";
                case 27:
                    return "汇潮支付";
            }
            return "未知类型";
        }
        public void MyBind()
        {
            DataTable dt = bll.GetPayPlatListAll();
            dt.DefaultView.RowFilter = "PayClass not in (15)";//去除支付宝网银
            dt = dt.DefaultView.ToTable();
            EGV.DataSource = dt;
            EGV.DataKeyNames = new string[] { "PayPlatID" };
            EGV.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Egv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int payplatid = DataConverter.CLng(e.Row.Cells[0].Text);
                M_PayPlat plat = bll.GetPayPlatByid(payplatid);
                if (plat.IsDefault)
                {
                    LinkButton btn1 = (LinkButton)e.Row.FindControl("LinkButton1");
                    LinkButton btn3 = (LinkButton)e.Row.FindControl("LinkButton3");
                    LinkButton btn4 = (LinkButton)e.Row.FindControl("LinkButton4");
                    LinkButton btn5 = (LinkButton)e.Row.FindControl("LinkButton5");
                    btn1.Enabled = false;
                    btn3.Enabled = false;
                }
                else
                {
                    LinkButton btn1 = (LinkButton)e.Row.FindControl("LinkButton1");
                    LinkButton btn3 = (LinkButton)e.Row.FindControl("LinkButton3");
                    LinkButton btn4 = (LinkButton)e.Row.FindControl("LinkButton4");
                    LinkButton btn5 = (LinkButton)e.Row.FindControl("LinkButton5");
                    btn1.Enabled = true;
                    btn3.Enabled = true;
                    btn4.Enabled = true;
                    btn5.Enabled = true;
                    if (plat.IsDisabled)
                        btn1.Text = "<span class='fa fa-check-circle' title='启用'></span>";
                    else
                        btn1.Text = "<span class='fa fa-ban' title='禁用'></span>";
                }
            }

        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            int pid = DataConverter.CLng(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "SetDef":
                    bll.SetDefault(pid);
                    break;
                case "Disabled":
                    M_PayPlat info = bll.GetPayPlatByid(pid);
                    if (info.IsDisabled)
                        info.IsDisabled = false;
                    else
                        info.IsDisabled = true;
                    bll.Update(info);
                    break;
                case "MovePre":
                    bll.MovePre(pid);
                    break;
                case "MoveNext":
                    bll.MoveNext(pid);
                    break;
                case "Delete":
                    bll.DeleteByID(pid);
                    Response.Redirect("PayPlatManage.aspx");
                    break;
            }
            MyBind();
        }
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in EGV.Rows)
            {
                if (row.RowState == DataControlRowState.Edit)
                {
                    row.Attributes.Remove("ondblclick");
                    row.Attributes.Remove("style");
                    continue;
                }
                if (row.RowType == DataControlRowType.DataRow)
                {

                    row.Attributes["ondblclick"] = String.Format("javascript:location.href='AddPayPlat.aspx?ID={0}'", EGV.DataKeys[row.RowIndex].Value.ToString());
                    row.Attributes["style"] = "cursor:pointer";
                }
            }
            base.Render(writer);
        }
        public string GetDefault(string IsDefault)
        {
            bool s = DataConverter.CBool(IsDefault);
            if (!s)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
        }
        public string GetDisabled(string IsDisabled)
        {
            bool s = DataConverter.CBool(IsDisabled);
            if (s)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
        }
    }
}