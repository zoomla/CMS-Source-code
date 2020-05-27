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
using System.Xml;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.Page;
using ZoomLa.Model.Page;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Page
{
    public partial class PageContent : CustomerPageAction
    {
        private B_Content bll = new B_Content();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        protected B_ModelField mll = new B_ModelField();
        protected int ModelID;
        protected string flag;
        string keytxt = string.Empty;
        protected int Cpage, temppage;
        protected int nextpagenum;
        protected int downpagenum;
        protected bool flags = false;
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        B_PageReg regBll = new B_PageReg();
        protected PagedDataSource pds = new PagedDataSource();
        //与当前导入相关类

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int GeneralID = DataConverter.CLng(Request.QueryString["GeneralID"]);
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.page, "PageContent"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (string.IsNullOrEmpty(base.Request.QueryString["ModelID"]))
                {
                    this.ModelID = 0;
                    this.Label1.Text = "黄页内容管理";
                }
                else
                {
                    this.ModelID = DataConverter.CLng(base.Request.QueryString["ModelID"]);
                    this.Label1.Text = bmode.GetModelById(this.ModelID).ModelName.ToString();
                }
                this.flag = string.IsNullOrEmpty(base.Request.QueryString["flag"]) ? "" : base.Request.QueryString["flag"];
                this.ViewState["ModelID"] = this.ModelID.ToString();
                this.ViewState["flag"] = this.flag;
                ViewState["type"] = Request.QueryString["type"];
                if (Request.QueryString["title"] != null)
                {
                    this.TextBox1.Text = Request.QueryString["title"];
                }
                if (Request.QueryString["type"] != null)
                {
                    this.DropDownList1.SelectedValue = Request.QueryString["type"];
                }

                this.BindOrder();
                RepNodeBind();

            }
            else
            {
                ByFilde = this.txtbyfilde.SelectedValue.ToString();
                ByOrder = this.txtbyOrder.SelectedValue.ToString();
            }

            Call.HideBread(Master);
            //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li>" + Label1.Text + "</li><li>[<a href='PageRecyle.aspx'>回收站</a>]</li>" + Call.GetHelp(88));
        }
        public string ByFilde
        {
            get
            {
                return ViewState["byfilde"] == null ? "" : ViewState["byfilde"].ToString();
            }
            set
            {
                ViewState["byfilde"] = value;
            }
        }
        public string ByOrder
        {
            get
            {
                return ViewState["byOrder"] == null ? "" : ViewState["byOrder"].ToString();
            }
            set
            {
                ViewState["byOrder"] = value;
            }
        }
        public string ParentID
        {
            get
            {
                return Request.QueryString["TemplateID"];
            }
        }

        public string GetUrl(string infoid)
        {
            return "/Page/PageContent.aspx?ItemID=" + infoid + "&PageID=" + regBll.GetSelectByUName(Eval("Inputer").ToString()).ID;
        }
        //获取用户id(数据绑定)
        public string GetUserID()
        {
            M_UserInfo mu = buser.GetUserIDByUserName(Eval("Inputer").ToString());
            return mu.UserID.ToString();
        }
        public void RepNodeBind()
        {
            int type = DataConverter.CLng(this.DropDownList1.SelectedValue);
            string title = this.TextBox1.Text;
            this.flag = string.IsNullOrEmpty(base.Request.QueryString["flag"]) ? "" : base.Request.QueryString["flag"];
            string order = string.Empty;
            if (!string.IsNullOrEmpty(ByFilde))
            {
                order = ByFilde + " " + ByOrder;
            }
            else
            {
                order = "GeneralID desc";
            }
            DataTable dt = new DataTable();
            dt = mll.SelAllPage(title, type, flag, order, DataConvert.CLng(ParentID));
            string fiter = "1=1";
            if (!string.IsNullOrEmpty(Request.QueryString["ModelID"]))
                fiter += " AND ModelID=" + Request.QueryString["ModelID"];
            if (!string.IsNullOrEmpty(Request.QueryString["UserName"]))
                fiter += " AND Inputer='" + Request.QueryString["UserName"] + "'";
            dt.DefaultView.RowFilter = fiter;
            RPT.DataSource = dt;
            RPT.DataBind();
        }
        public string GetModel(string infoid)
        {
            return "[" + bmode.GetModelById(DataConvert.CLng(infoid)).ItemName + "] ";
        }
        // 绑定的行
        protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = e.Row.Cells[1].Text;
            }
        }
        // 审核通过
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    this.bll.SetAudit(itemID, 99);
                }
            }
            RepNodeBind();
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    this.bll.SetDel(itemID);
                }
            }
            RepNodeBind();
        }
        protected void Lnk_Click(object sender, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "Edit")
                Page.Response.Redirect("EditContent.aspx?GeneralID=" + e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                this.bll.SetDel(DataConverter.CLng(Id));
                RepNodeBind();
            }

        }
        public string GetElite(string Elite)
        {
            if (DataConverter.CLng(Elite) > 0)
                return "推荐";
            else
                return "未推荐";
        }
        public string GetTitle(string ItemID, string NID, string Title)
        {
            string n = "";
            n = "<a href=\"EditContent.aspx?GeneralID=" + ItemID + "\">" + Title + "</a>";
            if (string.IsNullOrEmpty(ParentID))
                n = "[<a href='PageContent.aspx?TemplateID=" + Eval("NodeID") + "'>" + Eval("TempName") + "</a>] " + n;
            return "<span class='fa fa-file-text-o'></span> " + n;
        }
        public string GetStatus(string status)
        {
            int s = DataConverter.CLng(status);
            if (s == 0)
                return "待审核";
            if (s == 99)
                return "已审核";
            if (s == -2)
                return "回收站";
            return "退档";
        }
        public string GetCteate(string IsCreate)
        {
            int s = DataConverter.CLng(IsCreate);
            if (s != 1)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
        }

        protected void txtbyfilde_SelectedIndexChanged(object sender, EventArgs e)
        {
            RepNodeBind();
        }

        protected void txtbyOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            RepNodeBind();
        }

        //分类查找
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (this.DropDownList1.SelectedValue.Equals("0"))
            {
                string urlstr = "PageContent.aspx?pageSize=" + Request.QueryString["pageSize"] + "&id=" + Server.UrlEncode(this.TextBox1.Text) + "&type=" + this.DropDownList1.SelectedValue + "&ModelID=" + Request.QueryString["ModelID"] + "&NodeID=" + Request.QueryString["NodeID"] + "&title=" + Server.UrlEncode(this.TextBox1.Text) + "&province=" + Request.QueryString["province"] + "&city=" + Request.QueryString["city"];
                Response.Redirect(urlstr);
            }
            else if (this.DropDownList1.SelectedValue.Equals("1"))
            {
                string urlstr = "PageContent.aspx?pageSize=" + Request.QueryString["pageSize"] + "&type=" + this.DropDownList1.SelectedValue + "&ModelID=" + Request.QueryString["ModelID"] + "&NodeID=" + Request.QueryString["NodeID"] + "&title=" + Server.UrlEncode(this.TextBox1.Text) + "&id=" + Server.UrlEncode(this.TextBox1.Text) + "&province=" + Request.QueryString["province"] + "&city=" + Request.QueryString["city"];
                Response.Redirect(urlstr);
            }
            else if (this.DropDownList1.SelectedValue.Equals("2"))
            {
                string urlstr = "PageContent.aspx?pageSize=" + Request.QueryString["pageSize"] + "&type=" + this.DropDownList1.SelectedValue + "&ModelID=" + Request.QueryString["ModelID"] + "&NodeID=" + Request.QueryString["NodeID"] + "&title=" + Server.UrlEncode(this.TextBox1.Text) + "&id=" + Server.UrlEncode(this.TextBox1.Text) + "&province=" + Request.QueryString["province"] + "&city=" + Request.QueryString["city"];
                Response.Redirect(urlstr);
            }
        }
        private void BindOrder()
        {
            this.txtbyfilde.Items.Clear();
            this.txtbyOrder.Items.Clear();
            this.txtbyfilde.Items.Add(new ListItem("选择字段", ""));
            this.txtbyfilde.Items.Add(new ListItem("内容ID", "GeneralID"));
            this.txtbyfilde.Items.Add(new ListItem("添加时间", "CreateTime"));
            this.txtbyfilde.Items.Add(new ListItem("更新时间", "UpDateTime"));
            this.txtbyfilde.Items.Add(new ListItem("点击数", "Hits"));
            this.txtbyfilde.Items.Add(new ListItem("推荐级别", "EliteLevel"));
            this.txtbyfilde.AutoPostBack = true;
            this.txtbyOrder.Items.Add(new ListItem("排列顺序", ""));
            this.txtbyOrder.Items.Add(new ListItem("升序", "asc"));
            this.txtbyOrder.Items.Add(new ListItem("降序", "desc"));
            this.txtbyOrder.AutoPostBack = true;
        }
        #region 地区选项
        protected void bind()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("/Common/Area.xml"));
            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                if (node.Name == "address")
                {
                    foreach (XmlNode province in node)
                    {
                        this.selprovince.Items.Add(province.Attributes["name"].Value);
                    }
                    foreach (XmlNode province in node)
                    {
                        if (province.Attributes["name"].Value == this.selprovince.Value)
                        {
                            foreach (XmlNode city in province.ChildNodes)
                            {
                                this.selcity.Items.Add(city.Attributes["name"].Value);

                            }
                        }
                    }

                }
            }
        }
        protected void bind1()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("/Common/Area.xml"));
            foreach (XmlNode node in xmlDoc.ChildNodes)
            {
                if (node.Name == "address")
                {
                    foreach (XmlNode province in node)
                    {
                        if (province.Attributes["name"].Value == Request["value"])
                        {
                            foreach (XmlNode city in province.ChildNodes)
                            {
                                Response.Write(city.Attributes["name"].Value + "|");
                            }
                            if (Request.QueryString["city"] != null)
                            {
                                this.selcity.SelectedValue = Request.QueryString["city"];
                            }
                        }
                    }
                }
            }
        }
        #endregion

        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            RepNodeBind();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RepNodeBind();
        }
        protected void selcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("PageContent.aspx?NodeID=" + Request.QueryString["NodeID"] + "&ModelID=" + Request.QueryString["ModelID"] + "&province=" + Request.Form["selprovince"] + "&city=" + Request.Form["selcity"]);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    this.bll.SetAudit(itemID, 0);
                }
            }
            RepNodeBind();
        }
        //获取选中的checkbox
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
            {
                string[] chkArr = Request.Form["chkSel"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}