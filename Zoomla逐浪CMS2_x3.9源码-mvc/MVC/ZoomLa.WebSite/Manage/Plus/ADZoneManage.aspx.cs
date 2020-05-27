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
using System.IO;
using System.Text;
using ZoomLa.Components;
using System.Data.SqlClient;

namespace ZoomLaCMS.Manage.Plus
{
    public partial class ADZoneManage : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ADZoneManage.aspx'>扩展功能</a></li><li><a href='ADZoneManage.aspx'>广告管理</a></li><li class='active'><a href='ADManage.aspx'>版位管理</a><a href='ADZone.aspx'>[添加版位]</a></li>");
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.other, "ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.ViewState["AdName"] = "";
                DataBind();
                if (!string.IsNullOrEmpty(Request.QueryString["lbl"]))
                {
                    SearchAndBind(Request.QueryString["lbl"]);
                }
            }
        }
        public void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                int type = DataConverter.CLng(Request.QueryString["type"]);
                dt = B_ADZone.ADZone_ByCondition(" Where ZoneType=" + type + " order by ZoneID desc");
            }
            else
            {
                string adname = this.ViewState["AdName"].ToString();
                if (string.IsNullOrEmpty(adname))
                    dt = B_ADZone.ADZone_GetAll();
                else
                    dt = B_ADZone.ADZone_ByCondition(" Where ZoneName like @adname order by ZoneID desc", new SqlParameter[] { new SqlParameter("adname", "%" + adname + "%") });
            }
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
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='ADManage.aspx?ZoneId={0}';", this.Egv.DataKeys[e.Row.RowIndex].Value.ToString());
                e.Row.Attributes["style"] = "cursor:pointer";
                e.Row.Attributes["title"] = "双击查看版位之广告";
            }
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Id = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Del":
                    M_Adzone Old = B_ADZone.getAdzoneByZoneId(DataConverter.CLng(Id));
                    string jssource = Old.ZoneJSName;
                    jssource = VirtualPathUtility.AppendTrailingSlash(Request.PhysicalApplicationPath + "/" + SiteConfig.SiteOption.AdvertisementDir) + jssource;
                    if (B_ADZone.ADZone_Remove(Id))
                    {
                        FileSystemObject.Delete(jssource, FsoMethod.File);
                        function.Script(Page, "alert('删除成功！');");
                    }
                    break;
                case "Copy":
                    int NewID = B_ADZone.ADZone_Copy(DataConverter.CLng(Id));
                    if (NewID > 0)
                    {
                        M_Adzone mzone = B_ADZone.getAdzoneByZoneId(NewID);
                        string ZoneJSName = mzone.ZoneJSName;
                        ZoneJSName = ZoneJSName.Split(new string[] { "/" }, StringSplitOptions.None)[0].ToString();
                        if (ZoneJSName.Length == 5)
                            mzone.ZoneJSName = mzone.ZoneJSName.Insert(4, "0");
                        B_ADZone.ADZone_Update(mzone);
                        B_ADZone.CreateJS(NewID.ToString());
                        function.Script(Page, "alert('复制成功！" + NewID.ToString() + "');");
                    }
                    break;
                case "Clear":
                    if (B_ADZone.ADZone_Clear(DataConverter.CLng(Id)))
                        function.Script(Page, "alert('清除成功！');");
                    break;
                case "SetAct":
                    if (!B_ADZone.getAdzoneByZoneId(DataConverter.CLng(Id)).Active)
                        B_ADZone.ADZone_Active(DataConverter.CLng(Id));
                    else
                        B_ADZone.ADZone_Pause(Id);
                    B_ADZone.CreateJS(Id);
                    break;
                case "Refresh":
                    B_ADZone.CreateJS(e.CommandArgument.ToString());
                    function.WriteSuccessMsg("刷新版位成功");
                    break;
                case "PreView":
                    Page.Response.Redirect("PreviewAD.aspx?ZoneID=" + e.CommandArgument.ToString() + "&Type=Zone");
                    break;
                case "JS":
                    Page.Response.Redirect("ShowJSCode.aspx?ZoneID=" + e.CommandArgument.ToString());
                    break;
            }
            DataBind();
        }

        public static string getzonetypename(string i)
        {
            int index = DataConverter.CLng(i);
            string zonetypename = "";
            switch (index)
            {
                case 0:
                    zonetypename = "矩形横幅";
                    break;
                case 1:
                    zonetypename = "弹出窗口";
                    break;
                case 2:
                    zonetypename = "随屏移动";
                    break;
                case 3:
                    zonetypename = "固定位置";
                    break;
                case 4:
                    zonetypename = "漂浮移动";
                    break;
                case 5:
                    zonetypename = "文字代码";
                    break;
                case 6:
                    zonetypename = "对联广告";
                    break;
            }
            return zonetypename;
        }
        public string GetActive(string i)
        {
            string re = DataConverter.CBool(i) ? "活动" : "暂停";
            return re;
        }
        public string GetUnActive(string i)
        {
            string re = !DataConverter.CBool(i) ? "启动" : "暂停";
            return re;
        }
        public static string getzoneshowtypename(string i)
        {
            int index = DataConverter.CLng(i);
            string zoneshowtypename = "";
            switch (index)
            {
                case 1:
                    zoneshowtypename = "权重随机显示";
                    break;
                case 2:
                    zoneshowtypename = "权重优先显示";
                    break;
                case 3:
                    zoneshowtypename = "顺序循环显示";
                    break;
            }
            return zoneshowtypename;

        }
        // 批量删除
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string Ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(Ids))
                B_ADZone.BatchRemove(Ids);
            DataBind();
        }
        // 批量激活
        protected void BtnActive_Click(object sender, EventArgs e)
        {
            string Ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(Ids))
                B_ADZone.BatchActive(Ids);
            DataBind();
        }
        // 批量暂停
        protected void BtnPause_Click(object sender, EventArgs e)
        {
            string Ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(Ids))
                B_ADZone.BatchPause(Ids);
            DataBind();
        }
        // 批量刷新JS
        protected void BtnRefurbish_Click(object sender, EventArgs e)
        {
            string Ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(Ids))
            {
                string[] chkArr = Ids.Split(',');
                for (int i = 0; i < chkArr.Length; i++)
                {
                    B_ADZone.CreateJS(chkArr[i]);
                }
                //JS
                function.WriteSuccessMsg("批量刷新版位成功!");
            }
            else
                function.WriteErrMsg("批量刷新版位失败!");
            DataBind();
        }
        // 搜索
        protected void BntSearch_Click(object sender, EventArgs e)
        {
            SearchAndBind(this.TxtADName.Text.Trim());
        }
        private void SearchAndBind(string lblKey)
        {
            DataTable dt = B_ADZone.GetZoneName(lblKey);
            this.Egv.DataSource = dt;
            this.Egv.DataKeyNames = new string[] { "ZoneId" };
            this.Egv.DataKeyNames = new string[] { "ZoneName" };
            this.Egv.DataBind();
        }
    }
}