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
    using ZoomLa.Model;
    using ZoomLa.Components;

    public partial class AddPresentProject : CustomerPageAction
    {
        protected B_Model bmode = new B_Model();
        protected B_ShowField bshow = new B_ShowField();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Promotions bll = new B_Promotions();
        B_Admin badmin = new B_Admin();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Product pll = new B_Product();
            int id = DataConverter.CLng(Request.QueryString["id"]);
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='PresentProject.aspx'>促销方案管理</a></li><li>添加促销方案</li>");
            if (!IsPostBack)
            {
                if (id > 0)
                {
                    M_Promotions pinfo = bll.GetPromotionsByid(id);
                    this.HiddenID.Value = id.ToString();
                    this.Promoname.Text = pinfo.Promoname.ToString();
                    this.Promostart.Text = pinfo.Promostart.ToString();
                    this.Promoend.Text = pinfo.Promoend.ToString();
                    this.Pricetop.Text = pinfo.Pricetop.ToString();
                    this.Priceend.Text = pinfo.Priceend.ToString();
                    this.Integral.Text = pinfo.Integral.ToString();
                    this.GetPresent.Checked = (pinfo.GetPresent == 1) ? true : false;
                    this.Presentmoney.Text = pinfo.Presentmoney.ToString();
                    this.IntegralTure.Checked = (pinfo.IntegralTure == 1) ? true : false;
                    if (!string.IsNullOrEmpty(pinfo.PromoProlist))
                    {
                        if (pinfo.PromoProlist.IndexOf(",") > -1)
                        {
                            string[] listarr = pinfo.PromoProlist.Split(new string[] { "," }, StringSplitOptions.None);
                            for (int i = 0; i < listarr.Length; i++)
                            {
                                string pronames = pll.GetproductByid(DataConverter.CLng(listarr[i])).Proname;
                                this.PromoProlist.Items.Add(new ListItem(pronames, listarr[i].ToString()));
                            }
                        }
                        else
                        {
                            string pronames = pll.GetproductByid(DataConverter.CLng(pinfo.PromoProlist)).Proname;
                            this.PromoProlist.Items.Add(new ListItem(pronames, pinfo.PromoProlist));
                        }
                    }

                    if (this.PromoProlist.Items.Count > 0)
                    {
                        for (int p = 0; p < this.PromoProlist.Items.Count; p++)
                        {
                            this.PromoProlist.Items[p].Selected = true;
                        }
                    }
                    //this.PromoProlist.Text = pinfo.PromoProlist.ToString();
                    this.Label1.Text = "修改促销方案";
                    this.Save_B.Text = "修改";
                }
            }
        }
        protected void Save_B_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string adminname = badmin.GetAdminLogin().AdminName;
                M_Promotions CData = new M_Promotions();

                CData.Id = DataConverter.CLng(this.HiddenID.Value);
                CData.Promoname = Promoname.Text.Trim();
                CData.Promostart = DataConverter.CDate(Promostart.Text.Trim());
                CData.Promoend = DataConverter.CDate(Promoend.Text.Trim());
                if(CData.Promostart > CData.Promoend) { function.WriteErrMsg("起始日期不能大于结束日期!"); }
                CData.Pricetop = DataConverter.CDouble(Pricetop.Text);
                CData.Priceend = DataConverter.CDouble(Priceend.Text);
                CData.AddUser = adminname;
                CData.Integral = DataConverter.CLng(Integral.Text);
                if (GetPresent.Checked)
                {
                    CData.GetPresent = 1;
                }
                else
                {
                    CData.GetPresent = 0;
                }
                CData.Presentmoney = DataConverter.CDouble(Presentmoney.Text);

                if (IntegralTure.Checked)
                {
                    CData.IntegralTure = 1;
                }
                else
                {
                    CData.IntegralTure = 0;
                }
                CData.PromoProlist = (string.IsNullOrEmpty(PromoProlist.SelectedValue)) ? "" : PromoProlist.SelectedValue;
                CData.Addtime = DateTime.Now;
                CData.TureUser = adminname;
                if (CData.Id > 0)
                {
                    this.bll.Update(CData);
                    function.WriteSuccessMsg("促销方案修改成功!", "PresentProject.aspx");
                }
                else
                {
                    this.bll.Add(CData);
                    function.WriteSuccessMsg("促销方案添加成功!请继续添加", "AddPresentProject.aspx");
                }
            }
        }
        
}
}
