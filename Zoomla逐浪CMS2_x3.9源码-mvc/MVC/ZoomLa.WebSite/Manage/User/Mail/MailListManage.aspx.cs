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
using System.Collections.Generic;
using ZoomLa.Model;
using ZoomLa.Common;
using System.IO;
using System.Text;

namespace ZoomLaCMS.Manage.User.Mail
{
    public partial class MailListManage : CustomerPageAction
    {
        B_MailManage mmbll = new B_MailManage();
        B_User bull = new B_User();
        B_Subscribe bs = new B_Subscribe();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            function.SetPageNoCache();//清空缓存

            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();

            if (Request.QueryString["menu"] == "download")
            {
                string filename = Request.QueryString["filename"];
                if (!string.IsNullOrEmpty(filename))
                {
                    Response.Write("<BR /><p><center><a href=\"Download.aspx?f=" + filename + "\">点击下载邮件列表</a></center></p>");
                    Response.Write("<script>location.href='Download.aspx?f=" + filename + "';</script>");
                    Response.End();
                }
                else {
                    Response.Write("<script>window.close();</script>");
                }
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["typeid"] != null)
                    {
                        DropDownList2.SelectedIndex = 7;
                    }
                    DropDownList2_SelectedIndexChanged(null, null);
                }
            }
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>附件管理</li><li>邮件订阅</li><li>邮箱列表</li>");
        }
        //分页绑定数据
        private void Bind(DataTable dt)
        {
            this.EGV.DataSource = dt;
            this.EGV.DataBind();
        }

        public string GetName(string id)
        {
            return bull.GetUserByUserID(int.Parse(id)).UserName;
        }

        public string GetState(string str)
        {
            string s = "";
            if (str == "True")
                s = "<font COLOR='#33cc00'>已验证</font>";
            return s;
        }

        public string GetState1(string str)
        {
            string s = "";
            if (str == "False")
                s = "<font COLOR='#cc0000'>待验证</font>";
            return s;
        }


        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DropDownList2.SelectedIndex)
            {
                case 0:
                    Bind(mmbll.Select_All());
                    DropDownList3.Visible = false;
                    Panel1.Visible = false;
                    txtType.Visible = false;
                    Button1.Visible = false;
                    break;
                case 1:
                    DropDownList3.Items.Clear();
                    DropDownList3.Items.Add(new ListItem("A", "A"));
                    DropDownList3.Items.Add(new ListItem("B", "B"));
                    DropDownList3.Items.Add(new ListItem("C", "C"));
                    DropDownList3.Items.Add(new ListItem("D", "D"));
                    DropDownList3.Items.Add(new ListItem("E", "E"));
                    DropDownList3.Items.Add(new ListItem("F", "F"));
                    DropDownList3.Items.Add(new ListItem("G", "G"));
                    DropDownList3.Items.Add(new ListItem("H", "H"));
                    DropDownList3.Items.Add(new ListItem("I", "I"));
                    DropDownList3.Items.Add(new ListItem("J", "J"));
                    DropDownList3.Items.Add(new ListItem("K", "K"));
                    DropDownList3.Items.Add(new ListItem("L", "L"));
                    DropDownList3.Items.Add(new ListItem("M", "M"));
                    DropDownList3.Items.Add(new ListItem("N", "N"));
                    DropDownList3.Items.Add(new ListItem("O", "O"));
                    DropDownList3.Items.Add(new ListItem("P", "P"));
                    DropDownList3.Items.Add(new ListItem("Q", "Q"));
                    DropDownList3.Items.Add(new ListItem("R", "R"));
                    DropDownList3.Items.Add(new ListItem("S", "S"));
                    DropDownList3.Items.Add(new ListItem("T", "T"));
                    DropDownList3.Items.Add(new ListItem("U", "U"));
                    DropDownList3.Items.Add(new ListItem("V", "V"));
                    DropDownList3.Items.Add(new ListItem("W", "W"));
                    DropDownList3.Items.Add(new ListItem("X", "X"));
                    DropDownList3.Items.Add(new ListItem("Y", "Y"));
                    DropDownList3.Items.Add(new ListItem("Z", "Z"));

                    DropDownList3.Visible = true;
                    Panel1.Visible = false;
                    txtType.Visible = false;
                    Button1.Visible = false;
                    DropDownList3_SelectedIndexChanged(null, null);
                    break;
                case 2:
                    DropDownList3.Items.Clear();
                    DropDownList3.Items.Add(new ListItem("1", "1"));
                    DropDownList3.Items.Add(new ListItem("2", "2"));
                    DropDownList3.Items.Add(new ListItem("3", "3"));
                    DropDownList3.Items.Add(new ListItem("4", "4"));
                    DropDownList3.Items.Add(new ListItem("5", "5"));
                    DropDownList3.Items.Add(new ListItem("6", "6"));
                    DropDownList3.Items.Add(new ListItem("7", "7"));
                    DropDownList3.Items.Add(new ListItem("8", "8"));
                    DropDownList3.Items.Add(new ListItem("9", "9"));
                    DropDownList3.Items.Add(new ListItem("0", "0"));
                    DropDownList3.Visible = true;
                    Panel1.Visible = false;
                    txtType.Visible = false;
                    Button1.Visible = false;
                    DropDownList3_SelectedIndexChanged(null, null);
                    break;
                case 5:
                    DropDownList3.Visible = false;
                    Panel1.Visible = false;
                    txtType.Visible = true;
                    Button1.Visible = true;
                    break;
                case 3:
                case 4:
                    DropDownList3.Visible = false;
                    Panel1.Visible = true;
                    txtType.Visible = false;
                    Button1.Visible = true;
                    break;
                case 6:
                    DropDownList3.Items.Clear();
                    DropDownList3.Items.Add(new ListItem("已验证", "True"));
                    DropDownList3.Items.Add(new ListItem("待验证", "False"));
                    DropDownList3.Visible = true;
                    Panel1.Visible = false;
                    txtType.Visible = false;
                    Button1.Visible = false;
                    DropDownList3_SelectedIndexChanged(null, null);
                    break;
                case 7:
                    DropDownList3.Items.Clear();
                    DropDownList3.DataSource = bs.Select_All();
                    DropDownList3.DataValueField = "ID";
                    DropDownList3.DataTextField = "SubscribeName";
                    DropDownList3.DataBind();
                    DropDownList3.Visible = true;
                    if (Request.QueryString["typeid"] != null)
                    {
                        DropDownList3.SelectedValue = Request.QueryString["typeid"].ToString();
                    }
                    Panel1.Visible = false;
                    txtType.Visible = false;
                    Button1.Visible = false;
                    DropDownList3_SelectedIndexChanged(null, null);
                    break;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            switch (DropDownList2.SelectedIndex)
            {
                case 1:
                case 2:
                    break;
                case 3:
                    Bind(mmbll.GetAddTime(txtStartTime.Text, txtEndTime.Text, ""));
                    break;
                case 4:
                    Bind(mmbll.GetBackMostTime(txtStartTime.Text, txtEndTime.Text, ""));
                    break;
                case 5:
                    Bind(mmbll.GetPostfix(txtType.Text, ""));
                    break;
                case 6:
                    break;
            }
        }
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DropDownList2.SelectedIndex)
            {
                case 1:
                case 2:
                    Bind(mmbll.GetABC(DropDownList3.SelectedValue, ""));
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    Bind(mmbll.GetState(DropDownList3.SelectedValue));
                    break;
                case 7:
                    Bind(mmbll.GetSubscribe(DropDownList3.SelectedValue, ""));
                    break;
            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            mmbll.GetDelete(int.Parse(EGV.DataKeys[e.RowIndex].Value.ToString()));
            Bind(mmbll.Select_All());
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow det = (GridViewRow)lb.NamingContainer;
            M_MailManage mm = mmbll.GetSelect(int.Parse(EGV.DataKeys[det.RowIndex].Value.ToString()));
            mm.State = true;
            mmbll.GetUpdate(mm);
            Bind(mmbll.Select_All());
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            M_MailManage mm = new M_MailManage();
            mm.AddTime = DateTime.Now;
            mm.BackMostTime = DateTime.Now;
            mm.Email = txtMail.Text.Trim().ToLower();
            mm.Postfix = txtMail.Text.Substring(txtMail.Text.Trim().LastIndexOf("@") + 1, txtMail.Text.Length - (txtMail.Text.Trim().LastIndexOf("@") + 1));
            mm.State = true;
            mm.UserID = new B_Admin().GetAdminLogin().AdminId;
            if (mm.Email != "")
            {
                if (mmbll.SelByEmail(mm.Email).Rows.Count == 0)
                {
                    mmbll.GetInsert(mm);
                }
            }
            Bind(mmbll.Select_All());
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mailpinput.aspx");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            switch (DropDownList2.SelectedIndex)
            {
                case 0:
                    Educe(mmbll.Select_All());
                    break;
                case 1:
                case 2:
                    Educe(mmbll.GetABC(DropDownList3.SelectedValue, ""));
                    break;
                case 3:
                    Educe(mmbll.GetAddTime(txtStartTime.Text, txtEndTime.Text, ""));
                    break;
                case 4:
                    Educe(mmbll.GetBackMostTime(txtStartTime.Text, txtEndTime.Text, ""));
                    break;
                case 5:
                    Educe(mmbll.GetPostfix(txtType.Text, ""));
                    break;
                case 6:
                    Educe(mmbll.GetState(DropDownList3.SelectedValue));
                    break;
                case 7:
                    Educe(mmbll.GetSubscribe(DropDownList3.SelectedValue, ""));
                    break;
            }
        }

        private void Educe(DataTable dt)
        {
            string str = "";
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i == 2)
                {
                    str += dr["Email"].ToString() + "\\n";
                    i = 0;
                }
                else
                {
                    str += dr["Email"].ToString();
                    i = i + 1;
                    if (i < dt.Rows.Count)
                    {
                        str += ",";
                    }
                }
            }
            string filesname = DateTime.Now.ToShortDateString() + "_Output";

            Response.Write("<script>var winname = window.open('', '_blank', 'top=10000');winname.document.open('text/html', 'replace');winname.document.write('" + str.ToString() + "');winname.document.execCommand('saveas','','" + filesname + ".csv');winname.close();</script>");


            str = "";


        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow det = (GridViewRow)lb.NamingContainer;
            EGV.EditIndex = det.RowIndex;
            DropDownList2_SelectedIndexChanged(null, null);
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Button lb = (Button)sender;
            GridViewRow det = (GridViewRow)lb.NamingContainer;
            mmbll.UpdateEmail(int.Parse(EGV.DataKeys[det.RowIndex].Value.ToString()), ((TextBox)EGV.Rows[det.RowIndex].FindControl("TextBox1")).Text);
            EGV.EditIndex = -1;
            DropDownList2_SelectedIndexChanged(null, null);
        }
        protected void btn_DeleteRecords_Click(object sender, EventArgs e)
        {
            string idst = Request.Form["pidCheckbox"];
            if (idst != "")
            {
                mmbll.DelByIDS(idst);
            }
            DropDownList2_SelectedIndexChanged(null, null);
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            this.Bind(mmbll.Select_All());
        }
    }
}