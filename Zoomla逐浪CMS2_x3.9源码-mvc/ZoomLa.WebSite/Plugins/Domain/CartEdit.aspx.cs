namespace ZoomLaCMS.Plugins.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Site;
    using ZoomLa.Common;

    //编辑域名的购买信息
    public partial class CartEdit : System.Web.UI.Page
    {
        protected B_User buser = new B_User();
        protected B_IDC_DomainTemp domTempBll = new B_IDC_DomainTemp();
        protected void Page_Load(object sender, EventArgs e)
        {
            buser.CheckIsLogin();
            //function.WriteErrMsg(DomCartDT.Rows[Index]["TempValue"].ToString());
            if (string.IsNullOrEmpty(Request.QueryString["Index"]))
            {
                return;
            }
            else if (DomCartDT == null || DomCartDT.Rows.Count < 1)
            {
                function.WriteErrMsg("购物车不存在!!");
            }
            if (!IsPostBack)
            {
                domNameT.Text = DomCartDT.Rows[Index]["DomName"] as string;
                DataBind();
            }
        }
        private void DataBind(string key = "")
        {
            //prvinceDP.DataSource = city.readProvince();//省份
            //prvinceDP.DataTextField = "Name";
            //prvinceDP.DataValueField = "Name";
            //prvinceDP.DataBind();

            tempListDP.DataSource = domTempBll.SelTempWithHistory(buser.GetLogin().UserID);
            tempListDP.DataTextField = "TempName";
            tempListDP.DataValueField = "Index";
            tempListDP.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setValue('" + DomCartDT.Rows[Index]["TempValue"] + "');", true);//key:value,key2:value2
        }
        //内存购物车
        public DataTable DomCartDT
        {
            get { return Session["DomCartDT"] as DataTable; }
            set { Session["DomCartDT"] = value; }
        }
        //数据库中的域名购物车
        public DataTable DBCartDT { get; set; }
        public int Index
        {
            get { return Convert.ToInt32(Request.QueryString["Index"]); }
        }
        public DataTable TempDT
        {
            get
            {
                if (ViewState["TempDT"] == null)
                    ViewState["TempDT"] = domTempBll.SelTempWithHistory(buser.GetLogin().UserID);
                return ViewState["TempDT"] as DataTable;
            }
            set { ViewState["TempDT"] = value; }
        }
        protected void tempListDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setValue('" + SelValueFromDT(tempListDP.SelectedValue) + "');", true);//key:value,key2:value2
        }
        //确认,将修改后的值写入Session
        protected void submitBtn_Click(object sender, EventArgs e)
        {
            DomCartDT.Rows[Index]["TempValue"] = GetValueFromPage();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "setValue('" + DomCartDT.Rows[Index]["TempValue"] + "');alert('修改成功');", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "b", "parent.checkFunc();", true);
        }
        //--------------Tool
        private string GetValueFromPage()
        {
            string value = "";
            List<QueryParam> param = new List<QueryParam>();
            param.Add(new QueryParam("tempName", Request.Form["tempName"]));
            param.Add(new QueryParam("uname1", Request.Form["uname1"]));//注册人|单位名称 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("uname2", Request.Form["uname2"]));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("rname1", Request.Form["rname1"]));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("rname2", Request.Form["rname2"]));
            param.Add(new QueryParam("aname1", Request.Form["rname1"]));//管理联系人 中|英名称   [国内域名必填]|[国际域名必须],与上方用同一信息
            param.Add(new QueryParam("aname2", Request.Form["rname2"]));
            param.Add(new QueryParam("aemail", Request.Form["aemail"]));//管理联系人电子邮件地址                [必须]
            param.Add(new QueryParam("ucity1", prvinceDP.SelectedValue + Request.Form["cityText"]));//注册人城市名称 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("ucity2", Request.Form["ucity2"]));
            param.Add(new QueryParam("uaddr1", Request.Form["uaddr1"]));//通讯地址,中|英 [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("uaddr2", Request.Form["uaddr2"]));
            param.Add(new QueryParam("uzip", Request.Form["uzip"]));//注册人邮政编码                    [必须]
            param.Add(new QueryParam("uteln", Request.Form["uteln"]));//注册人电话号码
            param.Add(new QueryParam("ateln", Request.Form["ateln"]));
            param.Add(new QueryParam("ufaxa", Request.Form["ufaxa"]));   //传真区号
            param.Add(new QueryParam("ufaxn", Request.Form["ufaxn"]));//不能超过8位,与API的不能超过12位不同
            for (int i = 0; i < param.Count; i++)
            {
                value += param[i].QueryName + ":" + param[i].QueryValue + ",";
            }
            value += "prvinceDP:" + prvinceDP.SelectedValue + ",";
            value += "cityText:" + Request.Form["cityText"];
            return value;
        }
        private string SelValueFromDT(string index)
        {
            string result = "";
            TempDT.DefaultView.RowFilter = "Index = " + index;
            if (TempDT.DefaultView.ToTable().Rows.Count > 0)
            {
                result = TempDT.DefaultView.ToTable().Rows[0]["TempValue"].ToString();
            }
            return result;
        }
    }
}