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

    public partial class ProducerManage : CustomerPageAction
    {
        private B_Manufacturers bll = new B_Manufacturers();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "DeliverType"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if(!IsPostBack)
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='ProducerManage.aspx'>厂商管理</a></li> <a href='Producer.aspx'>[添加厂商]</a>");
        }
        public void DataBind(string key="")
        {
            Egv.DataSource = bll.GetManufacturersAll(" order by id desc");
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
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row!=null&&e.Row.RowType==DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = "javascript:location.href='Producer.aspx?menu=edit&id=" + Egv.DataKeys[e.Row.RowIndex].Value + "';";
                e.Row.Attributes["title"] = "双击修改";
            }
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument.ToString());
            M_Manufacturers manMod = bll.SelReturnModel(id);
            switch(e.CommandName.ToLower())
            {
                case "stop":
                    if(manMod.Disable==1)
                        bll.Upotherdata("4", id.ToString());
                    else
                        bll.Upotherdata("1", id.ToString());
                    break;
                case "settop":
                    if(manMod.Istop==1)
                        bll.Upotherdata("5", id.ToString());
                    else
                        bll.Upotherdata("2", id.ToString());
                    break;
                case "tui":
                    if(manMod.Isbest==1)
                        bll.Upotherdata("6", id.ToString());
                    else
                        bll.Upotherdata("3", id.ToString());
                    break;
                case "del":
                    bll.DeleteByID(id);
                    break;
            }
            DataBind();
        }
        public string showstop(string Disable)
        {
            string returnstr = "";
            if (Disable == "1")
                 returnstr = "启用";
            else
                returnstr = "禁用";
            return returnstr;
        }
        public string showstop2(string Disable)
        {
            string returnstr = "";
            if (Disable =="1")
                returnstr = "<font color=red>×</font>"; 
            else
                returnstr = "<font color=blue>√</font>";
            return returnstr;
        }
        public string showtop(string istop)
        {
            string returnstr = "";
            if (istop == "1")
                 returnstr = "解固";
            else
                returnstr = "固顶";
            return returnstr;
        }
        public string showtop2(string istop)
        {
            string returnstr = "";
            if (istop == "1")
                returnstr = "<font color=blue>固</font>";
            else
                returnstr = "&nbsp;&nbsp;";
            return returnstr;
        }
        public string showjian(string isjian)
        {
            string returnstr = "";
            if (isjian == "1")
                returnstr = "解荐";
            else
                returnstr = "推荐";
            return returnstr;
        }
        public string showjian2(string Isbest)
        {
            string returnstr = "";
            if (Isbest == "1")
                returnstr = "<font color=blue>荐</font>";
            else
                returnstr = "&nbsp;&nbsp;";
            return returnstr;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string itemdata = Request.Form["chkSel"];
            if (!string.IsNullOrEmpty(itemdata) && bll.DeleteBylist(itemdata) == true)
                Response.Write("<script language=javascript>alert('批量删除成功!');location.href='producermanage.aspx';</script>");
            else 
                Response.Write("<script language=javascript>alert('批量删除失败!');location.href='producermanage.aspx';</script>");
        }
    }
}