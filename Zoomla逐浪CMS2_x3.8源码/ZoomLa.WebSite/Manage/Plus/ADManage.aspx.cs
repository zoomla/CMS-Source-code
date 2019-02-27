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
using System.Collections.Generic;
using ZoomLa.Components;
namespace ZoomLa.WebSite.Manage.AddOn
{
    public partial class ADManage : CustomerPageAction
    {
        public string imageurl = "";
        protected B_Advertisement advBll = new B_Advertisement();
        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.other, "ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ADManage.aspx'>广告管理</a></li><li class='active'><a href='ADManage.aspx'>广告列表</a>  <a href='Advertisement.aspx?ZoneID=" + Request.QueryString["ZoneID"] + "'>[添加广告]</a></li>" + Call.GetHelp(29));
        }
        public void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(Request.QueryString["ZoneId"]) && DataConverter.CLng(Request.QueryString["ZoneId"])>0)
            {
                int type = DataConverter.CLng(Request.QueryString["type"]);
                int zoneid = DataConverter.CLng(Request.QueryString["ZoneId"]);
                if (!string.IsNullOrEmpty(Request.QueryString["type"])&&type>0)
                    dt = advBll.GetAdvByTypeAndZoneId(type, zoneid);
                else
                    dt = advBll.GetTableADList(zoneid);

            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                    dt = B_Advertisement.GetAdvByType(Convert.ToInt32(Request.QueryString["type"]));
                else
                    dt = B_Advertisement.GetAllAdvertisementList();
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
            if(e.Row!=null&&e.Row.RowType==DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = "javascript:location.href='Advertisement.aspx?ADId=" + Egv.DataKeys[e.Row.RowIndex].Value + "';";
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.Attributes["title"] = "双击修改";
            }
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Id = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "Del":
                    if (B_Advertisement.Advertisement_Delete(Convert.ToInt32(Id)))
                        function.WriteSuccessMsg("删除成功！", "ADManage.aspx");
                    break;
                case "Copy":
                    if (B_Advertisement.Advertisement_Copy(DataConverter.CLng(Id)))
                        function.WriteSuccessMsg("复制成功！", "ADManage.aspx");
                    break;
                case "Pass":
                    if (!B_Advertisement.Advertisement_GetAdvertisementByid(DataConverter.CLng(Id)).Passed)
                        B_Advertisement.Advertisement_SetPassed(Id);
                    else
                        B_Advertisement.Advertisement_CancelPassed(Id);
                    break;
            }
            DataBind();
        }
        // 批量删除
        protected void btndelete_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    B_Advertisement.Advertisement_Delete(Convert.ToInt32(chkArr[i]));
                }
            }
            this.DataBind();
        }
        // 批量取消审核
        protected void btncancelpassed_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    B_Advertisement.Advertisement_CancelPassed(Convert.ToInt32(chkArr[i]).ToString());
                }
            }
            this.DataBind();
        }

        // 批量审核
        protected void btnsetpassed_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    B_Advertisement.Advertisement_SetPassed(Convert.ToInt32(chkArr[i]).ToString());
                }
            }
            this.DataBind();
        }
        public string GetADType(string typeid)
        {
            switch (DataConverter.CLng(typeid))
            {
                case 1:
                    return "图片";
                case 2:
                    return "动画";
                case 3:
                    return "文本";
                case 4:
                    return "代码";
                case 5:
                    return "页面";
            }
            return "";
        }
        protected string getimg(string id)
        {
            int sid = DataConverter.CLng(id);
            string ddd = B_Advertisement.Advertisement_GetAdvertisementByid(sid).ADName;
            string tempstr = @"ShowADPreview('<a href=\&#39;/idc/ target=\&#39;_blank\&#39; title=\&#39;" + ddd + @"\&#39;><img src=\&#39;";
            string tempstr2 = @"\&#39; border=\&#39;0\&#39;></a>')";
            string imgurl = B_Advertisement.Advertisement_GetAdvertisementByid(sid).ImgUrl;
            imageurl = "<img src=\'/" + SiteConfig.SiteOption.UploadDir + "/" + imgurl + "\' width=\'200\' height=\'120\' border=\'0\' />";
            if (imgurl != "")
            {
                return tempstr + B_Advertisement.Advertisement_GetAdvertisementByid(sid).ImgUrl + tempstr2;
            }
            else
            {
                return "";
            }
        }
        public string getUrl(string url)
        {
            return "<img src=\'/" + SiteConfig.SiteOption.UploadDir + "/" + url + ".jpg\' width=\'200\' height=\'120\' border=\'0\' />";
        }
        //获取选中的checkbox
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] chkArr = Request.Form["idchk"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}
