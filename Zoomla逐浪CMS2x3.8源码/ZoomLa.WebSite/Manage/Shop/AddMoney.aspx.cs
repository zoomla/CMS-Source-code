namespace Zoomla.Website.manage.Shop
{
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
    using ZoomLa.Components;
    using ZoomLa.Model;
    using System.Text;

    public partial class AddMoney : CustomerPageAction
    {
        private B_MoneyManage bll = new B_MoneyManage();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string titles = "添加货币";
                if (!B_ARoleAuth.Check(ZLEnum.Auth.shop,"DeliverType"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (Mid > 0)
                {
                    M_MoneyManage minfos = bll.SelReturnModel(Mid);
                    function.Script(this, "document.getElementById('moneydescp').value='" + minfos.Money_code + "'");
                    moneycode.Text = minfos.Money_code;
                    moneysign.Text = minfos.Money_sign;
                    moneyrate.Text = minfos.Money_rate.ToString();
                    isflag_Chk.Checked = minfos.Is_flag == "1";
                    titles = minfos.Money_descp;
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='MoneyManage.aspx'>货币管理 </a></li><li class='active'>" + titles + "</li>");
            }
        }
        //保存
        protected void Button1_Click(object sender, EventArgs e)
        {
            M_MoneyManage moneyMod = new M_MoneyManage();
            if (Mid > 0)
            {
                moneyMod = bll.SelReturnModel(Mid);
            }
            moneyMod.Money_code = Request.Form[moneydescp.UniqueID];
            moneyMod.Money_descp = moneydescp.Items[moneydescp.SelectedIndex].Text;
            moneyMod.Money_rate = Decimal.Parse(moneyrate.Text.Trim());
            moneyMod.Money_sign = moneysign.Text;
            moneyMod.Is_flag = isflag_Chk.Checked ? "1" : "0";
            if (isflag_Chk.Checked)//取消其他默认项
            {
                bll.CancelDef();
            }
            if (Mid > 0)
            {
                bll.UpdateByID(moneyMod);
            }
            else
            {
                bll.insert(moneyMod);
            }
            function.WriteSuccessMsg("操作成功", "MoneyManage.aspx");
        }
    }
}