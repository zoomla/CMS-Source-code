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
namespace Zoomla.Website.manage.UserShopManage
{
    public partial class Producer : CustomerPageAction
    {
        private B_Manufacturers bll = new B_Manufacturers();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            
            if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreinfoManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                string menu = Request.QueryString["menu"];
                if (menu == "edit")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    M_Manufacturers rs = bll.GetManufacturersByid(id);
                    this.ID.Value = rs.id.ToString();
                    this.Producername.Text = rs.Producername.ToString();
                    this.Smallname.Text = rs.Smallname.ToString();
                    this.CreateTime.Text = rs.CreateTime.ToString();
                    this.Coadd.Text = rs.Coadd.ToString();
                    this.Telpho.Text = rs.Telpho.ToString();
                    this.FaxCode.Text = rs.FaxCode.ToString();
                    this.PostCode.Text = rs.PostCode.ToString();
                    this.CoWebsite.Text = rs.CoWebsite.ToString();
                    this.Email.Text = rs.Email.ToString();
                    this.CoClass.Text = rs.CoClass.ToString();
                    this.CoPhoto.Text = rs.CoPhoto.ToString();
                    this.Content.Value = rs.Content.ToString();
                    this.uptype.Value = "update";
                    this.Label1.Text = "修改厂商";
                }
            
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a><li><a href='ProducerManage.aspx'>厂商管理</a></li><li class='active'>添加厂商</li>");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string adminname = badmin.GetAdminLogin().AdminName; 
            M_Manufacturers CData = new M_Manufacturers();
            CData.id = DataConverter.CLng(this.ID.Value);
            CData.Producername = this.Producername.Text;
            CData.Smallname = this.Smallname.Text;
            CData.CreateTime = DataConverter.CDate(this.CreateTime.Text);
            CData.Coadd = this.Coadd.Text;
            CData.Telpho = this.Telpho.Text;
            CData.FaxCode = this.FaxCode.Text;
            CData.PostCode = this.PostCode.Text;
            CData.CoWebsite = this.CoWebsite.Text;
            CData.Email = this.Email.Text;
            CData.CoClass = this.CoClass.Text;
            CData.CoPhoto = CoPhoto.Text;
            CData.Content = Content.InnerText;
            if (this.uptype.Value == "update")
            {
                bll.Update(CData);
                Response.Write("<Script language=javascript>alert('修改成功!');location.href='ProducerManage.aspx';</Script>");
            }
            else
            {
                CData.Istop = 0;
                CData.Disable = 0;
                CData.Isbest = 0;
                bll.Add(CData);
                Response.Write("<Script language=javascript>alert('添加成功!');location.href='ProducerManage.aspx';</Script>");
            }
        }
    }
}