using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Models.Front
{
    public class VM_Register
    {
        B_ModelField fieldBll = new B_ModelField();
        B_Group gpBll = new B_Group();
        B_User buser = new B_User();
        HttpRequest Request = HttpContext.Current.Request;
        //-----------------------------------------
        private string _rurl = "";
        //不允许http跳转,不允许带空格,如未指定返回Url,则以后台为准
        public string ReturnUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_rurl))
                {
                    string url = HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"] ?? "").Split(' ')[0];
                    url = string.IsNullOrEmpty(url) ? SiteConfig.SiteOption.LoggedUrl : url;
                    _rurl = HttpUtility.HtmlEncode(url);
                }
                return _rurl;
            }
        }
        public string Mobile = "";
        //------------获取的值
        public M_UserInfo pmu = new M_UserInfo(true);
        public IPCity cityMod = null;
        public DataTable groupDT = null;
        //模型-必填|选填字段
        //public string html_must = "", html_select = "";
        public DataTable selectDT = new DataTable(), mustDT = new DataTable(), fieldDT = null;
        public VM_Register()
        {
            //根据IP,分析出地址,并填充
            cityMod = IPScaner.FindCity(IPScaner.GetUserIP());//"59.52.159.79"
            //
            //html_must = fieldBll.InputallHtml(0, 0, new ModelConfig()
            //{
            //    Source = ModelConfig.SType.UserRegister,
            //    Fields = SiteConfig.UserConfig.RegFieldsMustFill
            //});
            //html_select = fieldBll.InputallHtml(0, 0, new ModelConfig()
            //{
            //    Source = ModelConfig.SType.UserRegister,
            //    Fields = SiteConfig.UserConfig.RegFieldsSelectFill
            //});
            //1,添加字段2,用户参数--将其加入必填或选填字段3,输出给前端使用
            string selectFields = SiteConfig.UserConfig.RegFieldsSelectFill, mustFields = SiteConfig.UserConfig.RegFieldsMustFill;
            if (!string.IsNullOrEmpty(selectFields) || !string.IsNullOrEmpty(mustFields))
            {
                fieldDT = SqlHelper.ExecuteTable("SELECT *,ModelID=0,IsShow='True',IsSearchForm='False',IsView=1,IsDownField=0 FROM ZL_UserBaseField");
                if (!string.IsNullOrEmpty(selectFields))
                {
                    fieldDT.DefaultView.RowFilter = "FieldName IN (" + WrapFields(selectFields) + ")";
                    selectDT = fieldDT.DefaultView.ToTable();
                }
                if (!string.IsNullOrEmpty(mustFields))
                {
                    fieldDT.DefaultView.RowFilter = "FieldName IN (" + WrapFields(mustFields) + ")";
                    mustDT = fieldDT.DefaultView.ToTable();
                }
            }
            //----------------------------------------------
            groupDT = gpBll.GetSelGroup();
            int puid = DataConvert.CLng(Request.QueryString["ParentUserID"]);
            string puname = DataConvert.CStr(Request.QueryString["ParentUser"]);
            if (puid > 0) { pmu = buser.SelReturnModel(puid); }
            else if (!string.IsNullOrEmpty(puname)) { pmu = buser.GetUserIDByUserName(puname); }
        }
        private string WrapFields(string fields)
        {
            string result = "";
            foreach (string field in fields.Split(','))
            {
                result += "'" + field + "',";
            }
            return result.TrimEnd(',');
        }
    }
}