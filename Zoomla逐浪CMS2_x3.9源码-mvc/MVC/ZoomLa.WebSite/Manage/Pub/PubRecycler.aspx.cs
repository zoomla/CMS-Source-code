using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using System.Data;

namespace ZoomLaCMS.Manage.Pub
{
    public partial class PubRecycler : CustomerPageAction
    {
        private B_Pub pubBll = new B_Pub();
        private B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='ContentManage.aspx'>" + Resources.L.内容管理 + "</a></li><li><a href='PubManage.aspx'>" + Resources.L.互动模块管理 + "</a></li><li class='active'><a href='" + Request.RawUrl + "'>" + Resources.L.存档管理 + "</a></li>");
        }
        public void DataBind(string key = "")
        {
            DataTable dt = pubBll.SelByType(2);
            if (!badmin.GetAdminLogin().RoleList.Contains(",1,"))
                GetTable(dt, B_Role.GetPowerInfoByIDs(badmin.GetAdminLogin().RoleList));
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = "javascript:getinfo('" + Egv.DataKeys[e.Row.RowIndex].Value + "');";
            }
        }
        public string PubtypeName(string type)
        {
            string typename = "";
            switch (type)
            {
                case "0":
                    typename = Resources.L.评论;
                    break;
                case "1":
                    typename = Resources.L.投票;
                    break;
                case "2":
                    typename = Resources.L.活动;
                    break;
                case "3":
                    typename = Resources.L.留言;
                    break;
                case "4":
                    typename = Resources.L.问券调查;
                    break;
                case "5":
                    typename = Resources.L.通用统计;
                    break;
            }
            return typename;
        }
        public string GetClassName(string Classs)
        {
            string classname = "";
            switch (Classs)
            {
                case "0":
                    classname = Resources.L.内容;
                    break;
                case "1":
                    classname = Resources.L.商城;
                    break;
                case "2":
                    classname = Resources.L.黄页;
                    break;
                case "3":
                    classname = Resources.L.店铺;
                    break;
            }
            return classname;
        }
        public DataTable GetTable(DataTable dt, string PowerInfo)
        {
            string names = "";
            string[] PowerInfoArr = PowerInfo.Split(',');
            for (int i = 0; i < PowerInfoArr.Length; i++)
            {
                names += "'" + PowerInfoArr[i] + "',";
            }
            names = names.Trim(',');
            dt.DefaultView.RowFilter = "PubTableName in (" + names + ")";
            dt = dt.DefaultView.ToTable();
            return dt;
        }
        //批量删除
        protected void Clear_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                if (pubBll.DelByIDS(Request.Form["idchk"]))
                {
                    Response.Write("<script language=javascript>alert('" + Resources.L.删除成功 + "!');location.href='PubRecycler.aspx'</script>");
                }
                else
                    Response.Write("<script language=javascript>alert(" + Resources.L.删除失败 + "'!');location.href='PubRecycler.aspx'</script>");
            }
            else
                function.Script(this, "alert('" + Resources.L.批量删除失败 + "');");
            DataBind();
        }
        //清空文档
        protected void DelAll_Btn_Click(object sender, EventArgs e)
        {
            if (pubBll.DelAll())
            {
                Response.Write("<script language=javascript>alert('" + Resources.L.清空成功 + "!');location.href='PubRecycler.aspx'</script>");
            }
            else
                Response.Write("<script language=javascript>alert('" + Resources.L.清空失败 + "!');location.href='PubRecycler.aspx'</script>");
            DataBind();
        }
        //批量还原
        protected void Recyle_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                if (pubBll.RecyleByIDS(Request.Form["icdhk"]))
                {

                    Response.Write("<script language=javascript>alert('" + Resources.L.还原成功 + "!');location.href='PubRecycler.aspx'</script>");
                }
                else
                    Response.Write("<script language=javascript>alert('" + Resources.L.还原失败 + "!');location.href='PubRecycler.aspx'</script>");
            }
            else
                function.Script(this, "alert('" + Resources.L.还原失败 + "！')");

            DataBind();
        }
    }
}