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

namespace ZoomLaCMS.Manage.UserShopMannger
{
    public partial class AddShopBrand : CustomerPageAction
    {
        protected B_Trademark bll = new B_Trademark();
        protected B_Manufacturers mll = new B_Manufacturers();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "DeliverType"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!this.Page.IsPostBack)
            {
                string where = " order by id desc";
                DataTable rslist = mll.GetManufacturersAll(where);
                DataTable newprolist = rslist.DefaultView.ToTable(false, "id", "Producername");
                Hashtable ar = new Hashtable();
                foreach (DataRow dr in newprolist.Rows)
                {
                    ar.Add(dr[0].ToString(), dr[1].ToString());
                }
                this.Producer.DataSource = ar;
                this.Producer.DataValueField = "key";
                this.Producer.DataTextField = "value";
                this.Producer.DataBind();
                if (Request.QueryString["menu"] == "edit")
                {
                    this.uptype.Value = "update";
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    M_Trademark list = bll.GetTrademarkByid(id);
                    this.ID_H.Value = list.id.ToString();
                    this.Trname.Text = list.Trname.ToString();
                    this.Producer.Text = list.Producer.ToString();
                    this.TrClass.Text = list.TrClass.ToString();
                    this.txt_TrPhoto.Text = list.TrPhoto.ToString();
                    this.TrContent.Value = list.TrContent.ToString();
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a><li><a href='ShopScutcheon.aspx'>品牌管理</a></li><li class='active'>添加品牌</li>");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_Trademark CData = new M_Trademark();
                CData.id = DataConverter.CLng(this.ID_H.Value);
                CData.Trname = this.Trname.Text;
                CData.Producer = DataConverter.CLng(this.Producer.Text);
                CData.TrClass = this.TrClass.Text;
                CData.TrPhoto = this.txt_TrPhoto.Text;
                CData.TrContent = this.TrContent.Value;
                CData.Addtime = DateTime.Now;
                if (this.Isbest.Checked == true)
                {
                    CData.Isbest = 1;
                }
                else
                {
                    CData.Isbest = 0;
                }

                if (this.uptype.Value == "update")
                {
                    bll.Update(CData);
                    Response.Write("<script language=javascript>alert('修改成功!');location.href='ShopScutcheon.aspx';</script>");
                }
                else
                {
                    CData.Istop = 0;
                    CData.Isuse = 1;
                    bll.Add(CData);
                    Response.Write("<script language=javascript>alert('添加成功!');location.href='ShopScutcheon.aspx';</script>");
                }
            }
        }
    }
}