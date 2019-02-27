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
    public partial class ShopSearchKey :CustomerPageAction
    {
        //B_Shopsearch soull = new B_Shopsearch();
        B_Node nll = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreinfoManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                string menu = Request.QueryString["menu"];
                if (menu == "del")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    //soull.Delshopsearch(id);
                    Response.Write("<script language=javascript>alert('删除成功!');location.href='ShopSearchKey.aspx';</script>");
                }
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a><li class='active'>店铺关键字管理<a href='Addsearchkey.aspx'>[添加关键字]</a></li>");
        }
        protected void DataBind(string key = "")
        {
            //DataTable dt = soull.GetShopSearchall();
            //Egv.DataSource = dt;
            //Egv.DataBind();
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
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='Addsearchkey.aspx?menu=edit&id={0}'", this.Egv.DataKeys[e.Row.RowIndex].Value.ToString());
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //switch (e.CommandName.ToLower())
            //{

            //}
            DataBind();
        }
        protected string Getclass(string ccd)
        {
            string dd = "";
            string temps = "";
            if (ccd.IndexOf(",") > 0)
            {
                dd = "<select name=classname>";
                string[] csd = ccd.Split(new string[] { "," }, StringSplitOptions.None);

                for (int i = 0; i < csd.Length; i++)
                {
                    temps = nll.GetNodeXML(DataConverter.CLng(csd[i].ToString())).NodeName;
                    dd = dd + "<option>" + temps + "</option>";
                }

                dd = dd + "</select>";
            }
            else
            {
                M_Node nodeinfo = nll.GetNodeXML(DataConverter.CLng(ccd));
                if (!string.IsNullOrEmpty(nodeinfo.NodeName))
                {
                    dd = nodeinfo.NodeName.ToString();
                }
            }
            return dd;
        }
        protected string GetCommend(string dd)
        {
            int sd = DataConverter.CLng(dd);
            if (sd == 0)
            { return "<font color=red>不推荐</font>"; }
            else
            { return "<font color=green>推荐</font>"; }
        }
        protected string GetShowtop(string dd)
        {

            if (dd == "True")
            { return "<font color=green>是</font>"; }
            else
            { return "<font color=red>否</font>"; }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string item = Request.Form["item"];
            //if (soull.DelShopSearchlist(item))
            //{
            //    Response.Write("<script language=javascript>alert('删除成功!');location.href='ShopSearchKey.aspx';</script>");
            //}
            //else 
            //{
            //    Response.Write("<script language=javascript>alert('删除失败!');location.href='ShopSearchKey.aspx';</script>");
            //}
        }
    }
}