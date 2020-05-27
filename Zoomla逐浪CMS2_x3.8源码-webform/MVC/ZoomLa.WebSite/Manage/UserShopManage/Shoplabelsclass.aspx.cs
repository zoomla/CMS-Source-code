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

namespace ZoomLaCMS.Manage.UserShopMannger
{
    public partial class Shoplabelsclass :CustomerPageAction
    {
        //B_ShopLable slable = new B_ShopLable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            B_Admin badmin = new B_Admin();
            string strAdd = "", strName = "无分类";
            if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreinfoManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                //DataTable lablec = slable.GetLableClass();

                //if (!string.IsNullOrEmpty(Request.QueryString["lablelname"]))
                //    strName = Request.QueryString["lablelname"];
                //if (!string.IsNullOrEmpty(Request.QueryString["lablelname"]))
                //{
                //    strAdd = "<a href=\"addshoplabels.aspx?LableClass=" + Request.QueryString["lablelname"] + "\">[添加标签]</a>";
                //}
                //else
                //{
                //    strAdd = "<a href=\"addshoplabels.aspx\">[添加标签]</a>";
                //}
                //int id = DataConverter.CLng(Request.QueryString["id"]);
                //if (Request.QueryString["menu"] == "del")
                //{
                //    string lableclass = slable.GetShopLablebyid(id).LableClass;
                //    if (slable.DelShopLable(id))
                //    {
                //        Response.Write("<script language=javascript>alert('删除成功');location.href='Shoplabelsclass.aspx?lablelname=" + lableclass + "';</script>");
                //    }
                //    else
                //    {
                //        Response.Write("<script language=javascript>alert('删除失败');';</script>");
                //    }
                //}
                //for (int i = 0; i < lablec.Rows.Count; i++)
                //{
                //    lablelistname.Text = lablelistname.Text + "<a href=?lablelname=" + lablec.Rows[i][0].ToString() + ">" + lablec.Rows[i][0].ToString() + "</a>　";
                //}
                lablelistname.Text = "　选择标签分类：<a href=Shoplabelsclass.aspx>所有标签</a>　<a href=Shoplabelsclass.aspx?Derive=1>派生标签</a>　<a href=Shoplabelsclass.aspx?lablelname=>无分类</a>　" + lablelistname.Text;
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a></li><li><a href='Shoplabelsclass.aspx'>店铺标签管理</a><li class='active'>" + strName + strAdd + "</li>");
        }
        public void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            //if (string.IsNullOrEmpty(Request.QueryString["lablelname"]))
            //{
            //    dt = slable.GetShopLableinfo();
            //}
            //else
            //{
            //    dt = slable.GetShopLableinfo(Request.QueryString["lablelname"].ToString());
            //}
            //if (Request.QueryString["Derive"] == "1")
            //{
            //    dt = slable.GetDeriveLable();
            //}
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = Egv.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = Egv.PageSize;
            }
            Egv.PageSize = pageSize;
            Egv.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected string Gettpye(string type)
        {
            if (type == "0")
            {
                return "用户标签";
            }
            else if (type == "1")
            {
                return "系统标签";
            }
            else
            {
                return "";
            }
        }
        protected string Getture(string str)
        {
            if (str == "True")
            {
                return "<font color=green>启用</font>";
            }
            else if (str == "False")
            {
                return "<font color=red>停用</font>";
            }
            else
            {
                return "";
            }
        }
        protected string Getinput(string id)
        {
            //int sid = DataConverter.CLng(id);
            //M_ShopLable cdd = slable.GetShopLablebyid(sid);
            //if (cdd.LableType == 0)
            //{
            //    return "<input name=\"Item\" type=\"checkbox\" value='" + id + "'/>";
            //}
            //else
            //{
            return "<input name=\"Item\" type=\"checkbox\" disabled=\"disabled\"/>";
            //}
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //string item = Request.Form["Item"];
            //slable.ShopLableTrue(item, 1);
            Response.Write("<script language=javascript>alert('批量启用成功');location.href='Shoplabelsclass.aspx';</script>");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            //string item = Request.Form["Item"];
            //slable.ShopLableTrue(item, 2);
            Response.Write("<script language=javascript>alert('批量停用成功');location.href='Shoplabelsclass.aspx';</script>");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string item = Request.Form["Item"];
            //slable.ShopLableTrue(item, 3);
            Response.Write("<script language=javascript>alert('批量删除成功');location.href='Shoplabelsclass.aspx';</script>");
        }
        protected string GetName(string id)
        {
            int sid = DataConverter.CLng(id);
            string returnname = "";
            //M_ShopLable lableinfo = new M_ShopLable();
            //lableinfo = slable.GetShopLablebyid(sid);
            //string Derivelable = lableinfo.Derive;
            //if (Derivelable != "")
            //{
            //    returnname = "<font color=blue>" + lableinfo.LableName + "</font>";
            //}
            //else
            //{
            //    returnname = lableinfo.LableName;
            //}
            return returnname;
        }
        protected string Getbottom(string id)
        {
            int sid = DataConverter.CLng(id);
            //int labletype = slable.GetShopLablebyid(sid).LableType;
            string cc = "";
            //if (labletype == 0)
            //{
            //    cc = "<a href=\"addshoplabels.aspx?menu=edit&id=" + id + "\" class='option_style'><i class='fa fa-pencil' title='修改'></i></a>　<a href=\"?menu=del&id=" + id + "\" OnClick=\"return confirm('不可恢复性删除数据,你确定将该数据删除吗？');\" class='option_style'><i class='fa fa-trash-o' title='删除'></i>删除</a>";
            //}
            //else
            //{
            //    cc = "<a href=\"addshoplabels.aspx?menu=Derive&id=" + id + "\">派生</a>";
            //}

            return cc;
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='addshoplabels.aspx?menu=edit&id={0}'", this.Egv.DataKeys[e.Row.RowIndex].Value.ToString());
        }
    }
}