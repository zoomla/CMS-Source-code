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
using ZoomLa.Web;

namespace ZoomLaCMS.Manage.UserShopMannger
{
    public partial class ShopGrade : System.Web.UI.Page
    {

        #region 业务对象
        B_ShopGrade shopGrade = new B_ShopGrade();
        B_Model mbll = new B_Model();
        B_Admin badmin = new B_Admin();
        B_Content cbll = new B_Content();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();

            if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreinfoManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a><li class='active'>店铺等级管理<a href='AddShopGrades.aspx'>[添加等级]</a></li>");
        }
        public void MyBind()
        {
            RPT.DataSource =shopGrade.GetShopGradeinfo();
            RPT.DataBind();
        }
        #region 页面方法
        protected string Gettpye(string type)
        {
            if (type == "0")
            {
                return "购物等级";
            }
            else if (type == "1")
            {
                return "卖家等级";
            }
            else if (type == "2")
            {
                return "商户等级";
            }
            else
            {
                return "";
            }
        }

        protected string Getture(string str)
        {
            if (str == "True")
            { return "<font color=green>启用</font>"; }
            else if (str == "False")
            { return "<font color=red>停用</font>"; }
            else { return ""; }
        }
        public string GetIcon(string IconPath, string picnum)
        {
            int pcnum = DataConverter.CLng(picnum);
            string cc = "/Images/levelIcon/" + (string.IsNullOrEmpty(IconPath) ? "m_1.gif" : IconPath);
            string ee = "<img src=" + cc + " style=\"border-width:0px;\" />";
            string dd = "";
            for (int i = 0; i < pcnum; i++)
            {
                dd = dd + ee;
            }
            return dd;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string item = Request.Form["idchk"];
            shopGrade.ShopGradTrue(item, 1);
            Response.Write("<script language=javascript>alert('批量启用成功');location.href='ShopGrade.aspx'</script>");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string item = Request.Form["idchk"];
            shopGrade.ShopGradTrue(item, 2);
            Response.Write("<script language=javascript>alert('批量停用成功');location.href='ShopGrade.aspx'</script>");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string item = Request.Form["idchk"];
            shopGrade.ShopGradTrue(item, 3);
            Response.Write("<script language=javascript>alert('批量删除成功');location.href='ShopGrade.aspx'</script>");
        }
    }
    #endregion

}
