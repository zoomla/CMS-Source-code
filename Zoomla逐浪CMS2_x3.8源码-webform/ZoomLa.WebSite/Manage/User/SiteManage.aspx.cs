using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using ZoomLa.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZoomLa.BLL.ECharts;
using ZoomLa.BLL;

public partial class Manage_User_SiteManage : System.Web.UI.Page
{
    private string configUrl = function.VToP("/Config/ServerInfo.config");
    XmlDocument xdoc = new XmlDocument();
    public int Mod { get {return DataConverter.CLng(Request.QueryString["mod"]); } }
    public JArray jarray
    {
        get
        {
            XmlNode node = xdoc.SelectSingleNode("/Nodes/sites");
            if (!string.IsNullOrEmpty(node.InnerText))
            {
                return JsonConvert.DeserializeObject<JArray>(node.InnerText);
            }
            return null;
        }
        set
        {
            XmlNode node = xdoc.SelectSingleNode("/Nodes/sites");
            node.InnerText = JsonConvert.SerializeObject(value);
            xdoc.Save(configUrl);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.IsSuperManage();
        xdoc.Load(configUrl);
        if (!IsPostBack)
        {
            string modurl = Mod == 0 ? "<a href='?mod=1'>[查看拓扑图]</a>" : "<a href='?mod=0'>[查看列表]</a>";
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='AdminManage.aspx'>用户管理</a></li><li><a href='"+Request.RawUrl+"'>子站点管理</a> <a href='AddSite.aspx' class='reds'>[添加子站点]</a>"
                                        + modurl);
            MyBind();
        }
    }
    public void MyBind()
    {
        if (Mod==1)
        {
            selTable_Div.Visible = false;
            selImage_Div.Visible = true;
            JArray curjar = jarray == null ? new JArray() : jarray;
            code.Value = BindImgCode(Request.Url.Authority, curjar);
            function.Script(this, "ShowImage()");
        }
        else
        {
            XmlNode node = xdoc.SelectSingleNode("/Nodes/sites");
            if (!string.IsNullOrEmpty(node.InnerText))
            {
                Site_RPT.DataSource = JsonHelper.JsonToDT(node.InnerText);
                Site_RPT.DataBind();
            }
        }
    }
    //生成图片数据
    public string BindImgCode(string sitename, JArray jarray)
    {
        ChartOption option = new ChartOption();
        option.title.text = "站点关系:用户整合";
        option.title.subtext = "数据来自" + sitename;
        option.legend.data = new string[] { "主站", "子站", "二级子站" };
        option.series.Add(new ChartSeries()
        {
            name = "站点关系",
            categories = new Categories[] { new Categories("主站"), new Categories("子站"), new Categories("二级子站") },
            nodes = GetPicData(sitename, jarray),
            links = GetPicLinks(GetPicData(sitename, jarray), sitename),itemStyle=new SeriesItemStyle()
        });
        option.series[0].itemStyle.normal.label.textStyle.color = "#333";
        JsonSerializerSettings setting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        return "option=" + JsonConvert.SerializeObject(option, Newtonsoft.Json.Formatting.None, setting) + ";myChart.on(\"click\", function (param) {window.open(param.data.url); });";
    }
    public List<ChartNode> GetPicData(string sitename, JArray jarray)
    {
        List<ChartNode> nodeList = new List<ChartNode>();
        //SiteModel model = new SiteModel();
        ChartNode node = new ChartNode();
        node.category = 0;
        node.name = sitename;
        node.url = "http://" + sitename;
        node.value = "10";
        node.initial = new int[] { 500, 40 };
        node.fixX = true; node.fixY = true;
        node.draggable = true;
        nodeList.Add(node);

        //添加子站信息
        for (int i = 1; i <= jarray.Count; i++)
        {
            ChartNode childNode = new ChartNode();
            childNode.category = 1;
            childNode.name = jarray.ToArray()[i - 1]["domain"].ToString();
            childNode.value = "5";
            childNode.initial = new int[] { i * 150, 200 };
            childNode.fixX = true; childNode.fixY = true;
            childNode.url = "http://" + childNode.name;
            nodeList.Add(childNode);
        }
        return nodeList;
        //return JsonConvert.SerializeObject(nodeList);
    }
    public List<ChartLink> GetPicLinks(List<ChartNode> nodeList, string sitename)
    {
        List<ChartLink> linkList = new List<ChartLink>();
        for (int i = 0; i < nodeList.Count; i++)
        {
            ChartNode node = nodeList[i];
            ChartLink link = new ChartLink();
            link.name = "链接" + i;
            link.source = node.name;
            link.target = sitename;
            //link..linkStyle.type = "curve";
            linkList.Add(link);
        }
        return linkList;
        //return JsonConvert.SerializeObject(linkList);
    }
    protected void Site_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del":
                JArray jdatas = jarray;
                jdatas.Where(p => p["domain"].ToString().Equals(e.CommandArgument.ToString())).ToArray()[0].Remove();
                jarray = jdatas;
                Response.Redirect(Request.RawUrl);
                break;
            default:
                break;
        }
    }
}