using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using ZoomLa.Components;
using ZoomLa.BLL.Helper;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Filters;
using Newtonsoft.Json;
using System.Xml;

public partial class Manage_I_Content_CollectionInfoPage : System.Web.UI.Page
{
    B_CollectionItem bc = new B_CollectionItem();
    RegexHelper regHelper = new RegexHelper();
    HtmlHelper htmlHelper = new HtmlHelper();
    private string Url
    {
        get { return ViewState["url"] as string; }
        set { ViewState["url"] = value; }
    }
    private string SourceUrl
    {
        get {return Request.Form[Link_DP.UniqueID]; }
    }
    private int ItemID
    {
        get { return DataConverter.CLng(Request.QueryString["ItemID"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.HideBread(Master);
        }
    }
    public void MyBind() 
    {
        M_CollectionItem mc = bc.GetSelect(ItemID);
        Link_DP.DataSource = mc.LinkList.Split(',');
        Link_DP.DataBind();
        Url = Link_DP.Items[0].Value;
        txtHtml.Text = htmlHelper.GetHtmlFromSite(Url);
        CurLink_L.Text = "获取成功：" + Url;
        //----------------
        if (!string.IsNullOrWhiteSpace(mc.InfoPageSettings))
        {
            string value = regHelper.GetValueBySE(mc.InfoPageSettings, "<FieldStart>", "</FieldStart>",false);
            if (!string.IsNullOrWhiteSpace(value))
            {
                FilterModel model = JsonConvert.DeserializeObject<FilterModel>(value);
                EType_T.Text = model.EType;
                ID_T.Text = model.ID;
                Class_T.Text = model.CSS;
                Script_Chk.Checked = model.AllowScript;
            }
        }
    }
    private string GetStr(string LinkTop, string LinkButton, string Url, string ustr)
    {
        StringBuilder strurl = new StringBuilder();
        if ((LinkTop + ustr + LinkButton).IndexOf("http://") < 0)
        {
            //切割内容页面地址
            string[] urlinfo = ustr.Split(new char[] { '/' });
            //切割列表页地址
            string[] infoarr = Url.Split(new char[] { '/' });
            if (urlinfo[0] != "")
            {
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
                for (int j = 0; j < infoarr.Length - i; j++)
                {
                    strurl.Append(infoarr[j].ToString() + "/");
                }
                strurl.Append(LinkTop + urlinfo[urlinfo.Length - 1] + LinkButton + "\n\r");
            }
            else
            {
                strurl = new StringBuilder();
                strurl.Append("http://" + LinkTop + infoarr[2] + ustr + LinkButton);
            }
        }
        else
        {
            strurl.Append(LinkTop + ustr + LinkButton + "\n\r");
        }
        return strurl.ToString();
    }
    private string checkList(string stext, string etext)
    {
        string strhtml = txtHtml.Text;
        string strRef = stext + "[\\s\\S]*?" + etext;
        MatchCollection matches = Regex.Matches(strhtml,strRef,RegexOptions.IgnoreCase);
        Match TitleMatch = Regex.Match(strhtml, strRef, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string str = "";
        if (matches.Count > 0)
        {
            foreach (Match m in matches)
            {
                str += m.Value.Replace(stext, "").Replace(etext, "") + "\n\r";
            }
        }
        else
        {
            str = strhtml;
        }
        return Server.HtmlEncode(str);
    }
    private string checkList(string plist)
    {
        string strhtml = txtHtml.Text;

        string strs = "";

        string[] arr = GetS(plist, strhtml);

        if (arr.Length >= 1)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                string pras = arr[i];

                string prae = "</" + plist + ">";
                strs = pras + "[\\s\\S]*?" + prae;
                MatchCollection matches1 = new Regex(strs).Matches(strhtml);
                Match TitleMatch1 = Regex.Match(strhtml, strs, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                foreach (Match m in matches1)
                {
                    strhtml = strhtml.Replace(m.Value, "") + "\n\r";
                }
            }
        }
        return BaseClass.Htmldecode(strhtml);
    }
    protected string[] GetS(string s, string html)
    {
        string str = "<" + s + "[\\s\\S]*?>";
        MatchCollection matches1 = new Regex(str).Matches(html);
        Match TitleMatch1 = Regex.Match(html, str, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string[] arr = new string[matches1.Count];
        int index = 0;
        foreach (Match m in matches1)
        {
            arr[index] = m.Value;
            index++;
        }
        return arr;
    }
    protected void Link_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        Url = Request.Form[Link_DP.UniqueID];
        txtHtml.Text = htmlHelper.GetHtmlFromSite(Url);
        CurLink_L.Text = "获取成功：" + Url;
    }
    protected void Test_Btn_Click(object sender, EventArgs e)
    {
        string html = htmlHelper.GetHtmlFromSite(Url);
        FilterModel model = GetFModel();
        txtHtml.Text = htmlHelper.GetByFilter(html, model);
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        FilterModel model = GetFModel();
        Json_Hid.Value = JsonConvert.SerializeObject(model);
        function.Script(this, "SaveConfig();");
    }
    public FilterModel GetFModel()
    {
        FilterModel model = new FilterModel()
        {
            EType = EType_T.Text,
            ID = ID_T.Text,
            CSS = Class_T.Text,
            Start = Server.HtmlEncode(Start_T.Text),
            End = Server.HtmlEncode(End_T.Text),
            AllowScript = Script_Chk.Checked
        };
        return model;
    }
}
