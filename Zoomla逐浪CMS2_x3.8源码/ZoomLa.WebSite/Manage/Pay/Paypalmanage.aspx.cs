namespace ZoomLa.WebSite.Manage.I.Pay
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
    using ZoomLa.Common;
    using ZoomLa.BLL;
    using ZoomLa.Model;

    public partial class Paypalmanage : System.Web.UI.Page
    {
        private B_Defray bll = new B_Defray();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.other, "PayManage");
            B_Admin badmin = new B_Admin();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href='PayPlatManage.aspx'>在线支付平台</a></li><li class=\"active\">PayPal支付平台 </li>" + Call.GetHelp(57));
            if (!IsPostBack)
            {
                DataTable dt = this.bll.Select_All();
                this.clientid.Text = dt.Rows[0]["Client_id"].ToString();
                this.title_T.Text = dt.Rows[0]["title"].ToString();
                this.number.Text = dt.Rows[0]["number"].ToString();
                if (dt.Rows[0]["money_code"].ToString() != "") {
                    string[] strtemp = dt.Rows[0]["money_code"].ToString().Split(',');
                    foreach (string str in strtemp)
                    {
                        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                        {
                            if (this.CheckBoxList1.Items[i].Value == str)
                            {
                                this.CheckBoxList1.Items[i].Selected = true;
                            }
                        }
                    }
                    if (dt != null)
                    {
                        dt.Dispose();
                    }

                }
            
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_Defray md = new M_Defray();

            string chek = "";
            for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
            {
                if (this.CheckBoxList1.Items[i].Selected)
                {
                    chek = chek + this.CheckBoxList1.Items[i].Value + ",";
                }
            }
            md.Client_id = this.clientid.Text;
            md.money_code = chek;
            md.number = this.number.Text;
            md.Pay_intf = this.pay_intf.Text;
            md.Title = this.title_T.Text;
            md.Pay_name = this.payname.Text;
            bll.InsertUpdate(md);
            function.WriteSuccessMsg("保存成功!");
        }
}
}