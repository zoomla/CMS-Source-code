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
    using System.Text;
    using ZoomLa.Components;

    public partial class Producer : CustomerPageAction
    {
        private B_Manufacturers bll = new B_Manufacturers();
        private B_Model bmode = new B_Model();
        B_Admin badmin = new B_Admin();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "DeliverType"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='Producer.aspx'>添加厂商</a></li>");
            }
        }
        protected void MyBind()
        {
            if (Mid > 0)
            {
                M_Manufacturers rs = bll.GetManufacturersByid(Mid);
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
                this.SFile_Up.FileUrl = rs.CoPhoto;
                this.Content.Value = rs.Content.ToString();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string adminname = badmin.GetAdminLogin().AdminName;
            M_Manufacturers CData = new M_Manufacturers();
            if (Mid > 0) { CData = bll.SelReturnModel(Mid); }
            CData.Producername = this.Producername.Text;
            CData.Smallname = this.Smallname.Text;
            CData.CreateTime = DataConverter.CDate(this.CreateTime.Text);
            CData.Coadd = Coadd.Text;
            CData.Telpho = Telpho.Text;
            CData.FaxCode = FaxCode.Text;
            CData.PostCode = this.PostCode.Text;
            CData.CoWebsite = this.CoWebsite.Text;
            CData.Email = this.Email.Text;
            CData.CoClass = this.CoClass.Text;

            string vpath = SiteConfig.SiteOption.UploadDir + "Image/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            SFile_Up.SaveUrl = vpath;
            string imgurl = 
            CData.CoPhoto = SFile_Up.SaveFile();
            CData.Content = Content.Value;
            if (Mid > 0)
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