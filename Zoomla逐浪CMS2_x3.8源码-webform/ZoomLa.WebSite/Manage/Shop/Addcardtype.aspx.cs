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
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
public partial class manage_Shop_Addcardtype : CustomerPageAction
{
    B_CardType bcard = new B_CardType();
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        

        string menu = Request.QueryString["menu"];
        int id = DataConverter.CLng(Request.QueryString["id"]);
        if (!IsPostBack)
        {
            if (menu == "edit")
            {

                M_CardType c = bcard.SelectType(id);
                this.tx_typename.Text = c.typename;
                this.tx_count.Text = c.iscount.ToString();
                this.ID_H.Value = c.c_id.ToString();
                this.HiddenField1.Value = "updata";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='CardManage.aspx'>VIP卡管理</a></li><li>卡类型</li>");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_CardType carotype = new M_CardType();
        carotype.typename = tx_typename.Text;
        carotype.iscount = DataConverter.CDecimal(tx_count.Text);
        carotype.c_id = DataConverter.CLng(this.ID_H.Value);


        if (this.HiddenField1.Value == "updata")
        {

            if (bcard.UpCardType(carotype))
            {
                Response.Write("<script language=javascript>alert('更新成功!');location.href='cardtypeManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('更新失败!');location.href='cardtypeManage.aspx';</script>");
            }

        }
        else
        {
            if (bcard.Add(carotype))
            {
                Response.Write("<script language=javascript>alert('添加成功!');location.href='cardtypeManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('添加失败!');location.href='cardtypeManage.aspx';</script>");
            }

        }

    }
}
