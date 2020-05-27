using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using ZoomLa.BLL.Helper;

namespace ZoomLaCMS.Manage.Content.Collect
{
    public partial class CollectionStep2 : CustomerPageAction
    {
        B_CollectionItem bc = new B_CollectionItem();
        HtmlHelper htmlHelper = new HtmlHelper();
        protected string title = "添加设置采集第二步";
        protected int PubCodinChoice = 0;

        private int ItemID
        {
            get { return DataConverter.CLng(Request.QueryString["ItemID"]); }
        }
        private string Url
        {
            get { return ViewState["url"].ToString(); }
            set { ViewState["url"] = value; }
        }
        private string Action
        {
            get
            {
                if (ViewState["action"] == null)
                { ViewState["action"] = Request.QueryString["action"]; }
                return ViewState["action"].ToString();
            }
            set { ViewState["action"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtListStart.Text = "<div class=\"blk13\">";
                //txtListEnd.Text = "</div>";
                M_CollectionItem mc = bc.GetSelect(ItemID);
                Url = mc.CollUrl;
                switch (Action)
                {
                    case "Insert":
                        lblLink.Text = "<a href='" + mc.CollUrl + "'  target=\"_blank\" class='btn btn-primary' style='color:white;'>查看原页面</a>";
                        this.PubCodinChoice = mc.CodinChoice;
                        if (!string.IsNullOrEmpty(mc.LinkList)) { txtHtml.Text = mc.LinkList.Replace(",", "\n"); }
                        else { txtHtml.Text = htmlHelper.GetHtmlFromSite(Url); }
                        break;
                    case "Modify":
                        #region Modify
                        txtHtml.Text = mc.LinkList.Replace(",", "");
                        DataSet ds = function.XmlToTable(mc.ListSettings);
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                txtListStart.Text = setText2(dr["ListStart"].ToString());
                                txtListEnd.Text = setText2(dr["ListEnd"].ToString());
                                Pre_T.Text = setText2(dr["LinkTop"].ToString());
                                End_T.Text = setText2(dr["LinkButton"].ToString());
                            }
                        }
                        ds = function.XmlToTable(mc.PageSettings);
                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                switch (dr["PageType"].ToString())
                                {
                                    case "1":
                                        Radio1.Checked = true;
                                        HiddenField1.Value = "0";
                                        break;
                                    case "2":
                                        Radio2.Checked = true;
                                        HiddenField1.Value = "1";
                                        txtNextPageBegin.Text = setText2(dr["PageNextBegin"].ToString());
                                        txtNextPageEnd.Text = setText2(dr["PageNextEnd"].ToString());
                                        break;
                                    case "3":
                                        Radio3.Checked = true;
                                        HiddenField1.Value = "2";
                                        txturl.Text = setText2(dr["PageUrl"].ToString());
                                        txtBeginNum.Text = setText2(dr["PageBeginNum"].ToString());
                                        txtEndNum.Text = setText2(dr["PageEndNum"].ToString());
                                        break;
                                    case "4":
                                        Radio4.Checked = true;
                                        HiddenField1.Value = "3";
                                        txtUrlList.Text = setText2(dr["PageInfo"].ToString());
                                        break;
                                    case "5":
                                        Radio5.Checked = true;
                                        HiddenField1.Value = "4";
                                        txtPageDivBegin.Text = setText2(dr["PageDivBegin"].ToString());
                                        txtPageDivEnd.Text = setText2(dr["PageDivEnd"].ToString());
                                        txtBegin.Text = setText2(dr["PageUrlBegin"].ToString());
                                        txtEnd.Text = setText2(dr["PageUrlEnd"].ToString());
                                        break;
                                }
                            }
                        }
                        title = "<a title=\"采集项目设置\" href=\"CollectionStep1.aspx?Action=Modify&ItemId=" + ItemID + " \">采集项目设置</a> >> <span style='color:red;'>列表页采集设置</span> >> <a title=\"内容页采集设置\" href=\"CollectionStep3.aspx?Action=Modify&amp;ItemID=" + ItemID + "\">内容页采集设置</a>";
                        #endregion
                        break;
                }//switch end;
                Call.SetBreadCrumb(Master, "<li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CollectionManage.aspx'>信息采集</a></li><li><a href='CollectionManage.aspx'>项目管理</a></li><li class='active'>当前:" + mc.ItemName + "</li>");
            }
        }
        //获取源代码
        protected void Button4_Click(object sender, EventArgs e)
        {
            txtHtml.Text = htmlHelper.GetHtmlFromSite(Url);
        }
        //筛选列表
        protected void FilterA_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtListStart.Text) && !string.IsNullOrEmpty(txtListEnd.Text))
            {
                txtHtml.Text = htmlHelper.GetHtmlFromSite(Url);
                txtHtml.Text = checkList(txtListStart.Text, txtListEnd.Text);
            }
        }
        private string checkList(string stext, string etext, string pre = "", string end = "")
        {
            string strhtml = txtHtml.Text;
            string strRef = stext + "[\\s\\S]*?" + etext;
            MatchCollection matches = new Regex(strRef).Matches(strhtml);
            Match TitleMatch = Regex.Match(strhtml, strRef, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            StringBuilder str = new StringBuilder();
            foreach (Match m in matches)
            {
                string url = (m.Value.Replace(stext, "").Replace(etext, "") + end);
                if (url.ToLower().Contains("http") || url.ToLower().Contains("https"))
                { }
                else { url = pre + url; }//自动用根路径,或用户预设好的值填充前面
                str.AppendLine(url);
            }
            if (string.IsNullOrWhiteSpace(str.ToString())) { function.WriteSuccessMsg("错误,未取到匹配的字符串,请核对开始与结束代码"); }
            return htmlHelper.GetAllLink("<html><head></head><body>" + str.ToString() + "</body></html>", Pre_T.Text, End_T.Text);
        }
        //上一步
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CollectionStep1.aspx?Action=" + Action + "&ItemId=" + ItemID);
        }
        private string setText(string str)
        {
            return str.Replace("<", "&lt;").Replace(">", "&gt;");
        }
        private string setText2(string str)
        {
            return str.Replace("&lt;", "<").Replace("&gt;", ">");//.Replace("<br />", "\n");
        }
        //下一步
        protected void Button1_Click(object sender, EventArgs e)
        {
            M_CollectionItem mc = bc.GetSelect(ItemID);
            mc.ListSettings = "<PageList><ListStart>" + setText(txtListStart.Text) + "</ListStart><ListEnd>" + setText(txtListEnd.Text) + "</ListEnd><LinkTop>" + setText(Pre_T.Text) + "</LinkTop><LinkButton>" + setText(End_T.Text) + "</LinkButton></PageList>";
            StringBuilder spage = new StringBuilder();
            mc.LinkList = "";
            foreach (string s in txtHtml.Text.Split('\n'))
            {
                if (string.IsNullOrEmpty(s.Replace(" ", ""))) continue;
                mc.LinkList += StrHelper.UrlDeal(s) + ",";
            }
            mc.LinkList = mc.LinkList.Replace(" ", "").Trim(',');
            switch (Request.Form["rdList"])
            {
                case "1":
                    spage = new StringBuilder("<PageSet><PageType>1</PageType></PageSet>");
                    break;
                case "2":
                    spage = new StringBuilder("<PageSet><PageType>2</PageType>");
                    spage.Append("<PageNextBegin>" + setText(txtNextPageBegin.Text) + "</PageNextBegin>");
                    spage.Append("<PageNextEnd>" + setText(txtNextPageEnd.Text) + "</PageNextEnd>");
                    spage.Append("</PageSet>");
                    break;
                case "3":
                    spage = new StringBuilder("<PageSet><PageType>3</PageType>");
                    spage.Append("<PageUrl>" + txturl.Text + "</PageUrl>");
                    spage.Append("<PageBeginNum>" + setText(txtBeginNum.Text) + "</PageBeginNum>");
                    spage.Append("<PageEndNum>" + setText(txtEndNum.Text) + "</PageEndNum>");
                    spage.Append("</PageSet>");
                    break;
                case "4":
                    spage = new StringBuilder("<PageSet><PageType>4</PageType>");
                    spage.Append("<PageInfo>" + setText(txtUrlList.Text) + "</PageInfo>");
                    spage.Append("</PageSet>");
                    break;
                case "5":
                    spage = new StringBuilder("<PageSet><PageType>5</PageType>");
                    spage.Append("<PageDivBegin>" + setText(txtPageDivBegin.Text) + "</PageDivBegin>");
                    spage.Append("<PageDivEnd>" + setText(txtPageDivEnd.Text) + "</PageDivEnd>");
                    spage.Append("<PageUrlBegin>" + setText(txtBegin.Text) + "</PageUrlBegin>");
                    spage.Append("<PageUrlEnd>" + setText(txtEnd.Text) + "</PageUrlEnd>");
                    spage.Append("</PageSet>");
                    break;
                default:
                    spage = new StringBuilder("<PageSet><PageType>1</PageType></PageSet>");
                    break;
            }
            mc.PageSettings = spage.ToString();
            bc.GetUpdate(mc);
            Response.Redirect("CollectionStep3.aspx?Action=" + Action + "&ItemId=" + ItemID);
        }
        //分页【测试下一页】
        protected void Button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNextPageBegin.Text) && !string.IsNullOrEmpty(txtNextPageEnd.Text))
            {
                string strurl = checkList(txtNextPageBegin.Text, txtNextPageEnd.Text);
                txtHtml.Text = orderUrl(strurl);
                SetUrl();
            }
        }
        //分页【测试从源代码中获取分页URL】
        protected void Button8_Click(object sender, EventArgs e)
        {
            string strurl = "";
            if (!string.IsNullOrEmpty(txtPageDivBegin.Text) && !string.IsNullOrEmpty(txtPageDivEnd.Text))
            {
                if (!string.IsNullOrEmpty(txtBegin.Text) && !string.IsNullOrEmpty(txtEnd.Text))
                {
                    txtHtml.Text = checkList(txtPageDivBegin.Text, txtPageDivEnd.Text);

                    strurl = checkList(txtBegin.Text, txtEnd.Text);

                    txtHtml.Text = orderUrl(strurl);
                    SetUrl();
                }
            }
        }
        private string orderUrl(string strurl)
        {
            string[] str = strurl.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] str2 = new string[str.Length];
            strurl = "";
            for (int i = 0; i < str.Length; i++)
            {
                bool b = true;
                for (int j = 0; j < str2.Length; j++)
                {

                    if (str[i] == str2[j])
                    {
                        b = false;
                    }
                }
                if (b)
                {
                    str2[i] = str[i];
                    strurl += str[i] + "\n";
                }
            }
            return strurl;
        }
        private void SetUrl()
        {
            string str = txtHtml.Text;
            txtHtml.Text = "";
            //切割字符串地址
            string[] UrlArr = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            //循环地址
            foreach (string ustr in UrlArr)
            {
                if (ustr.IndexOf("http://") < 0)
                {
                    //切割地址
                    string[] urlinfo = ustr.Split(new char[] { '/' });
                    int i = 0;
                    //循环切割后的地址字符
                    foreach (string s in urlinfo)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            i++;
                        }
                    }
                    if (i > 1)
                    {
                        i--;
                    }
                    //切割当前页面地址
                    string[] infoarr = Url.Split(new char[] { '/' });
                    for (int j = 0; j < infoarr.Length - i; j++)
                    {
                        txtHtml.Text += infoarr[j].ToString() + "/";
                    }
                    txtHtml.Text += urlinfo[urlinfo.Length - 1] + "\n";
                }
                else
                {
                    txtHtml.Text += ustr + "\n";
                }
            }
        }
    }
}