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
using ZoomLa.Common;
using ZoomLa.Web;
using ZoomLa.BLL;
using System.Collections.Generic;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Globalization;

namespace ZoomLaCMS.Manage.UserShopMannger
{
    public partial class StoreStyleManage :CustomerPageAction
    {
        protected B_StoreStyleTable sstbll = new B_StoreStyleTable();
        protected B_Model mbll = new B_Model();
        protected B_Admin badmin = new B_Admin();
        protected B_Content cbll = new B_Content();
        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreStyleManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Shop/ProductManage.aspx'>商城管理</a></li><li><a href='StoreManage.aspx'>店铺管理</a></li><li class='active'>商家店铺模板管理<a href='StoreStyleEdit.aspx'>[添加店铺模板]</a></li>" + Call.GetHelp(89));
        }
        #region 页面方法
        //初始化
        public void DataBind(string key = "")
        {
            DataTable dt = sstbll.GetAllStyle();
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
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del1":
                    sstbll.DelStoreStyle(e.CommandArgument.ToString());
                    break;
                default:
                    break;
            }
            DataBind();
        }
        protected string GetModel(string gid)
        {
            return mbll.GetModelById(int.Parse(gid)).ModelName;
        }
        protected string GetImg(string gid)
        {
            string stylepic = sstbll.GetStyleByID(int.Parse(gid)).StylePic;
            if (!string.IsNullOrEmpty(stylepic))
            {
                stylepic = stylepic.ToLower();
            }
            if (!string.IsNullOrEmpty(stylepic) && (stylepic.IndexOf(".gif") > -1 || stylepic.IndexOf(".jpg") > -1 || stylepic.IndexOf(".png") > -1))
            {
                string delpath = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";
                if (stylepic.StartsWith("http://", true, CultureInfo.CurrentCulture) || stylepic.StartsWith("/", true, CultureInfo.CurrentCulture) || stylepic.StartsWith(delpath, true, CultureInfo.CurrentCulture))
                {

                }
                else
                {
                    stylepic = "/" + delpath + "/" + stylepic;
                }
            }
            else
            {
                stylepic = "/UploadFiles/nopic.gif";
            }
            return stylepic;
        }
        protected string GetState(string gid)
        {
            switch (gid)
            {
                case "0":
                    return "<font color='red'>停用</font>";
                case "1":
                    return "启用";
                default:
                    return "";
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string list = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(list))
            {
                Button bt = sender as Button;
                if (bt.CommandName == "5")
                {
                    sstbll.DelStoreStyle(list);
                }
                else
                {
                    sstbll.UpdateStoreState(list, int.Parse(bt.CommandName));
                }
            }
            DataBind();
        }
        #endregion
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='StoreStyleEdit.aspx?id={0}'", this.Egv.DataKeys[e.Row.RowIndex].Value.ToString());
        }
    }
}