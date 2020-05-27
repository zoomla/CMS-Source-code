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
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Site
{
    public partial class AddDomain : System.Web.UI.Page
    {
        protected B_IDC_DomainList domListBll = new B_IDC_DomainList();
        protected B_IDC_DomainTemp domTempBll = new B_IDC_DomainTemp();
        protected B_User buser = new B_User();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>站群管理</a></li><li class='active'>添加绑定</li>");
                DataBindDP();
                DataBindUser();
                if (Mid > 0)
                {
                    domListBtn.Text = "修改";
                    DataTable doml = domListBll.SelByID(Mid.ToString());
                    if (doml != null)
                    {
                        domListT1.Text = doml.Rows[0]["DomName"].ToString();
                        domListT1.Enabled = false;
                        M_UserInfo muser = new M_UserInfo();
                        muser = buser.GetUserByUserID(DataConverter.CLng(doml.Rows[0]["OwnUserID"].ToString()));
                        DomListDPBind(muser.UserID);
                        domListT2.Text = muser.UserName;
                        domListT2.Enabled = true;
                        domListT3.Text = DataConvert.CDate(doml.Rows[0]["CreatDate"].ToString()).ToString("yyyy/MM/dd");
                        yearDP.SelectedValue = doml.Rows[0]["Year"].ToString();
                        sitetab1.Visible = false;
                        sitetab2.Visible = true;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setValue('" + doml.Rows[0]["RegInfo"] + "');", true);
                    }
                }
            }
        }
        private void DataBindDP()
        {
            //prvinceDP.DataSource = city.readProvince();//省份
            //prvinceDP.DataTextField = "Name";
            //prvinceDP.DataValueField = "Name";
            //prvinceDP.DataBind();
        }
        private void DataBindUser()
        {
            EGV.DataSource = this.buser.Sel();
            EGV.DataBind();
        }
        public void DomListDPBind(int userID)
        {
            domListDP.DataSource = GetTempDT(userID);
            domListDP.DataTextField = "TempName";
            domListDP.DataValueField = "Index";
            domListDP.DataBind();
        }
        public void txtPageFunc(string size)
        {

            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = EGV.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = EGV.PageSize;
            }
            EGV.PageSize = pageSize;
            EGV.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBindUser();
        }
        protected void domListDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (domListDP.Items.Count < 1) { return; }
            DataTable dt = ViewState["TempDT"] as DataTable;
            dt.DefaultView.RowFilter = "Index=" + domListDP.SelectedValue;
            dt = dt.DefaultView.ToTable();
            if (dt.Rows.Count < 1)
                return;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setValue('" + dt.Rows[0]["TempValue"] + "');", true);//key:value,key2:value2
        }
        //为用户添加域名
        protected void domListBtn_Click(object sender, EventArgs e)
        {
            string datavalue = Request.Form["dataValue"] + ",";
            datavalue += "prvinceDP:" + prvinceDP.SelectedValue + ",";
            datavalue += "uteln:" + Request.Form["uteln"];
            string t1 = domListT1.Text.Trim();//域名
            string t2 = domListT2.Text.Trim();//用户名
            string t3 = domListT3.Text.Trim();//日期
            int year = Convert.ToInt32(yearDP.SelectedValue);
            DateTime endtime = DataConvert.CDate(t3).AddYears(year);
            M_UserInfo mu = buser.GetUserIDByUserName(t2);

            if (year < 1)
                function.WriteErrMsg("年限错误!");
            else if (string.IsNullOrEmpty(t1) || string.IsNullOrEmpty(t2))
            {
                function.WriteErrMsg("用户名与域名不能为空");
            }
            else if (mu.IsNull)
            {
                function.WriteErrMsg("用户不存在");
            }

            if (Request.QueryString["Edit"] == "1" && Request.QueryString["id"] != "")
            {
                domListBll.UpdateByID(t1, DataConvert.CDate(t3), endtime, mu.UserID, datavalue, year, Request.QueryString["id"]);
                function.WriteSuccessMsg("修改成功", "DomManage.aspx");
            }
            else
            {
                domListBll.Insert(t1, DataConvert.CDate(t3), endtime, mu.UserID, datavalue, year);
                function.WriteSuccessMsg("添加成功", "DomManage.aspx");
            }
        }
        //绑定用户模板
        protected void domListT2_TextChanged(object sender, EventArgs e)
        {
            int userID = buser.GetUserIDByUserName(domListT2.Text.Trim()).UserID;
            if (userID <= 0)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "会员不存在!!";
                domListBtn.Enabled = false;
                domListDP.Items.Clear();
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.Green;
                Label1.Text = "会员名验证成功";
                domListBtn.Enabled = true;
                DomListDPBind(userID);
            }

        }
        //添加模板
        protected void addTempBtn_Click(object sender, EventArgs e)
        {
            string tName = Request.Form["tempName"];
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
            param.Add(new QueryParam("ucity1", Request.Form["ucity1"]));//注册人城市名称 中|英名称    [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("ucity2", Request.Form["ucity2"]));
            param.Add(new QueryParam("uaddr1", Request.Form["uaddr1"]));//通讯地址,中|英 [国内域名必填]|[国际域名必须]
            param.Add(new QueryParam("uaddr2", Request.Form["uaddr2"]));
            param.Add(new QueryParam("uzip", Request.Form["uzip"]));//注册人邮政编码                    [必须]
            param.Add(new QueryParam("uteln", Request.Form["uteln"]));//注册人电话号码
            param.Add(new QueryParam("ateln", Request.Form["ateln"]));
            param.Add(new QueryParam("ufaxa", Request.Form["ufaxa"]));   //传真区号
            param.Add(new QueryParam("ufaxn", Request.Form["ufaxn"]));//不能超过8位,与API的不能超过12位不同

            ////自定义Dns
            //if (Request.Form["dnsOption"].Equals("1") && string.IsNullOrEmpty(dns1.Text.Trim() + dns2.Text.Trim()))
            //{
            //    param.Add(new QueryParam("dns1", dns1.Text.Trim()));//不能超过8位,与API的不能超过12位不同
            //    param.Add(new QueryParam("dns2", dns2.Text.Trim()));//不能超过8位,与API的不能超过12位不同
            //}
            for (int i = 0; i < param.Count; i++)
            {
                value += param[i].QueryName + ":" + param[i].QueryValue + ",";
            }
            value = value.TrimEnd(',');
            domTempBll.UpdateByID(tName, value, domListDP.SelectedValue);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');", true);
            DataBindDP();
        }
        //检测域名是否已被添加
        protected void domListT1_TextChanged(object sender, EventArgs e)
        {
            if (domListBll.isExist(domListT1.Text.Trim()) > 0)
            {
                remindL.Text = "该域名已存在，无法添加";
                domListBtn.Enabled = false;
            }
            else
            {
                remindL.Text = "该域名可注册！";
                domListBtn.Enabled = true;
            }
            domListDP_SelectedIndexChanged(null, null);
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBindUser();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    M_UserInfo muser = new M_UserInfo();
                    muser = buser.GetUserByUserID(DataConverter.CLng(e.CommandArgument.ToString()));
                    this.domListT2.Text = muser.UserName;
                    DataTable dt = GetTempDT(muser.UserID);
                    domListDP.DataSource = dt;
                    domListDP.DataValueField = "Index";
                    domListDP.DataTextField = "TempName";
                    domListDP.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        function.Script(this, "setValue('" + dt.Rows[0]["TempValue"] + "');");//key:value,key2:value2
                    }
                    sitetab1.Visible = false;
                    sitetab2.Visible = true;
                    break;
                default:
                    break;
            }
        }
        public string GetGroupName(string GroupID)
        {
            B_Group bgp = new B_Group();
            return bgp.GetByID(DataConverter.CLng(GroupID)).GroupName;
        }
        //会员搜索
        protected void Button1_Click(object sender, EventArgs e)
        {
            B_User bu = new B_User();
            DataTable dt = new DataTable();
            //if (TextBox1.Text != "")
            //{
            //    switch (DropDownList2.SelectedValue)
            //    {
            //        case "0":
            //            dt = bu.GetuserTb(TextBox1.Text);
            //            break;
            //        case "1":
            //            dt = bu.GetuserTb(DataConverter.CLng(TextBox1.Text));
            //            break;
            //    }
            //}
            //else
            //{
            dt = bu.Sel();
            //}
            this.EGV.DataSource = dt;
            this.EGV.DataBind();
            if (dt != null)
            {
                dt.Dispose();
            }
        }

        public DataTable GetTempDT(int userID)
        {
            if (ViewState["TempDT"] == null)
                ViewState["TempDT"] = domTempBll.SelTempWithHistory(userID);
            return ViewState["TempDT"] as DataTable;
        }
    }
}