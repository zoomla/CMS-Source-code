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

namespace ZoomLaCMS.Manage.Shop
{
    public partial class Trademark :CustomerPageAction
    {
        protected B_Trademark bll = new B_Trademark();
        protected B_Manufacturers mll = new B_Manufacturers();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!this.Page.IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='Trademark.aspx'>添加品牌</a></li>");
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
                    this.TrPhoto.Text = list.TrPhoto.ToString();
                    this.TrContent.Value = list.TrContent.ToString();

                }

            }



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
                CData.TrPhoto = this.TrPhoto.Text;
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
                    Response.Write("<script language=javascript>alert('修改成功!');location.href='TrademarkManage.aspx';</script>");
                }
                else
                {
                    CData.Istop = 0;
                    CData.Isuse = 1;
                    bll.Add(CData);
                    Response.Write("<script language=javascript>alert('添加成功!');location.href='TrademarkManage.aspx';</script>");
                }
            }
        }
    }
}