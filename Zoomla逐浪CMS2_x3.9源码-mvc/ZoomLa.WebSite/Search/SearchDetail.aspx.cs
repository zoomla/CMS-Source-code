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
using System.Collections.Specialized;
using ZoomLa.Model;
using System.Collections.Generic;
using ZoomLa.Components;

namespace ZoomLaCMS.Search
{
    public partial class SearchDetail : System.Web.UI.Page
    {
        private B_Node bnod = new B_Node();
        private B_Model bmod = new B_Model();
        private B_ModelField bfield = new B_ModelField();
        private B_Content bll = new B_Content();

        protected void Page_Load(object sender, EventArgs e)
        {
            function.WriteErrMsg("页面关闭"); return;
            //form1.Target = "_blank";
            //if (!this.Page.IsPostBack)
            //{
            //    this.NodeID.DataSource = this.bnod.GetNodeListContainXML(0);
            //    this.NodeID.DataTextField = "NodeName";
            //    this.NodeID.DataValueField = "NodeID";
            //    this.NodeID.DataBind();
            //    ListItem item = new ListItem("所有栏目", "0");
            //    this.NodeID.Items.Insert(0, item);
            //    string titles = Request.QueryString["title"];
            //    string TxtTagKeys = Request.QueryString["TxtTagKey"];
            //    string TxtInputers = Request.QueryString["TxtInputer"];
            //    if (!string.IsNullOrEmpty(titles))
            //    {
            //        this.TxtTitle.Text = Server.HtmlEncode(titles);
            //    }

            //    if (!string.IsNullOrEmpty(TxtTagKeys))
            //    {
            //        this.TxtTagKey.Text = Server.HtmlEncode(TxtTagKeys);
            //    }
            //    if (!string.IsNullOrEmpty(TxtInputers))
            //    {
            //        this.TxtInputer.Text = Server.HtmlEncode(TxtInputers);
            //    }


            //    int ModelID = string.IsNullOrEmpty(Request.QueryString["ModelID"]) ? 0 : DataConverter.CLng(Request.QueryString["ModelID"]);
            //    if (ModelID <= 0)
            //    {
            //        Response.Write("<center>url提交方式:<font color=red>SearchDetail.aspx?ModelID=模型ID&title=标题&TxtTagKey=关键词&TxtInputer=录入人</font></center>");
            //        Response.Write("<br />");
            //        function.WriteErrMsg("没有指定要搜索的内容模型ID");
            //    }
            //    else
            //    {
            //        this.HdnModel.Value = ModelID.ToString();
            //        this.LblModelName.Text = "详细查询" + bmod.GetModelById(ModelID).ItemName + "信息";
            //        //this.ModelSearchHtml.Text = bfield.GetSearchModelHtml(ModelID);
            //    }
            //}
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            this.ViewState["QuerryList"] = GetNewQuerysString();
            string modeid = this.HdnModel.Value;
            this.Session["QuerryList"] = GetNewQuerysString();
            Response.Redirect("SearchDetailOut.aspx?modeid=" + modeid);
        }
        public string GetUrl(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.bll.GetCommonData(p);
            if (cinfo.IsCreate == 1)
                return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
            else
                return "/Content.aspx?ID=" + p;
        }
        public string GetNodeUrl(string nodeid)
        {
            M_Node nodeinfo = bnod.GetNodeXML(DataConverter.CLng(nodeid));
            if (!string.IsNullOrEmpty(nodeinfo.NodeUrl) && nodeinfo.ListPageHtmlEx < 3)
                return "<a href=\"" + SiteConfig.SiteInfo.SiteUrl + nodeinfo.NodeUrl + "\">" + nodeinfo.NodeName + "</a>";
            else
                return "<a href=\"/ColumnList.aspx?NodeID=" + nodeid + "\">" + nodeinfo.NodeName + "</a>";
        }
        private IList<M_SearchCondition> GetNewQuerysString()
        {
            IList<M_SearchCondition> para = new List<M_SearchCondition>();
            string TableName = bmod.GetModelById(DataConverter.CLng(this.HdnModel.Value)).TableName;
            //节点ID
            if (DataConverter.CLng(this.NodeID.SelectedValue) > 0)
                para.Add(new M_SearchCondition("NodeID", "int", this.NodeID.SelectedValue, 1, 1, "ZL_CommonModel"));
            //标题
            if (!string.IsNullOrEmpty(this.TxtTitle.Text.Trim()))
                para.Add(new M_SearchCondition("Title", "text", DataSecurity.FilterBadChar(this.TxtTitle.Text.Trim()), 0, 1, "ZL_CommonModel"));
            if (!string.IsNullOrEmpty(this.TxtTagKey.Text.Trim()))
            {
                string kw = DataSecurity.FilterBadChar(this.TxtTagKey.Text.Trim());
                para.Add(new M_SearchCondition("TagKey", "text", kw, 0, 1, "ZL_CommonModel"));
                B_KeyWord bk = new B_KeyWord();
                if (bk.GetIDByName(kw) > 0)
                {
                    M_KeyWord kinfo = bk.GetKeyByName(kw);
                    kinfo.Hits++;
                    bk.Update(kinfo);
                }
            }
            //录入人
            if (!string.IsNullOrEmpty(DataSecurity.FilterBadChar(this.TxtInputer.Text.Trim())))
                para.Add(new M_SearchCondition("Inputer", "text", DataSecurity.FilterBadChar(this.TxtInputer.Text.Trim()), 0, 1, "ZL_CommonModel"));
            //录入时间
            string str = this.BeginDate.Text;
            if (!string.IsNullOrEmpty(str))
            {
                str = DataConverter.CDate(str).ToString("yyyy-MM-dd");
            }
            string str2 = this.EndDate.Text;
            if (!string.IsNullOrEmpty(str2))
            {
                str2 = DataConverter.CDate(str2).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
                para.Add(new M_SearchCondition("CreateTime", "datetime", "BeginDate=" + str + ",EndDate=" + str2, 1, 0, "ZL_CommonModel"));

            //获取模型搜索字段的输入值
            IList<M_ModelField> list = new List<M_ModelField>();
            for (int i = 0; i < list.Count; i++)
            {
                M_SearchCondition con = new M_SearchCondition();
                con.FieldName = list[i].FieldName;
                con.TableName = TableName;
                switch (list[i].FieldType)
                {
                    case "MultipleTextType":
                    case "MultipleHtmlType":
                    case "PicType":
                    case "FileType":
                    case "MultiPicType":
                    case "SuperLinkType":
                    case "TextType": //文本
                        con.FieldType = "text";
                        con.SearchValue = DataSecurity.FilterBadChar(Request.Form["txt_" + list[i].FieldName]);
                        con.SearchType = 0;
                        con.RangeType = 1;
                        break;
                    case "NumType":
                        string[] strArray1 = list[i].Content.Split(new char[] { ',' });
                        string[] searchtype = strArray1[3].Split(new char[] { '=' });
                        string[] rangetype = strArray1[4].Split(new char[] { '=' });
                        string numtype = strArray1[1].Split(new char[] { '=' })[1];
                        int numstyle = DataConverter.CLng(strArray1[1].Split(new char[] { '=' })[1]);
                        string fieldType = "";
                        if (numstyle == 1)
                            fieldType = "int";
                        if (numstyle == 2)
                            fieldType = "float";
                        if (numstyle == 3)
                            fieldType = "money";
                        con.FieldType = fieldType; //数据类型
                        if (searchtype[1] == "1") //精确查询
                        {
                            con.SearchValue = DataSecurity.FilterBadChar(Request.Form["txt_" + list[i].FieldName]);
                            con.SearchType = 1;
                            con.RangeType = 1;
                        }
                        else
                        {
                            if (rangetype[1] == "1") //下限-上限
                            {
                                str = Request.Form["txt_" + list[i].FieldName + "_Begin"];
                                if (!string.IsNullOrEmpty(str))
                                {
                                    str = DataConverter.CLng(str).ToString();
                                }
                                str2 = Request.Form["txt_" + list[i].FieldName + "_end"];
                                if (!string.IsNullOrEmpty(str2))
                                {
                                    str2 = DataConverter.CLng(str2).ToString();
                                }
                                if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
                                    con.SearchValue = "BeginNum=" + str + ",EndNum=" + str2;
                                con.SearchType = 0;
                                con.RangeType = 1;
                            }
                            else  //自定范围
                            {
                                con.SearchValue = DataSecurity.FilterBadChar(Request.Form["txt_" + list[i].FieldName]);
                                con.SearchType = 0;
                                con.RangeType = 0;
                            }
                        }
                        break;
                    case "DateType":
                        con.FieldType = "datetime";
                        string[] strArray2 = list[i].Content.Split(new char[] { ',' });
                        string[] searchtype1 = strArray2[0].Split(new char[] { '=' });
                        string dateunit = strArray2[2].Split(new char[] { '=' })[1];
                        if (searchtype1[1] == "1")
                        {
                            str = Request.Form["txt_" + list[i].FieldName + "_Begin"];
                            if (!string.IsNullOrEmpty(str))
                            {
                                str = DataConverter.CDate(str).ToString("yyyy-MM-dd");
                            }
                            str2 = Request.Form["txt_" + list[i].FieldName + "_end"];
                            if (!string.IsNullOrEmpty(str2))
                            {
                                str2 = DataConverter.CDate(str2).ToString("yyyy-MM-dd");
                            }
                            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
                                con.SearchValue = "BeginDate=" + str + ",EndDate=" + str2;
                            con.SearchType = 1;
                            con.RangeType = 0;
                        }
                        else
                        {
                            con.SearchValue = Request.Form["txt_" + list[i].FieldName];
                            con.SearchType = 0;
                            con.RangeType = DataConverter.CLng(dateunit);
                        }
                        break;
                    default:
                        con.FieldType = "text";
                        con.SearchValue = DataSecurity.FilterBadChar(Request.Form["txt_" + list[i].FieldName]);
                        con.SearchType = 1;
                        con.RangeType = 0;
                        break;
                }
                if (!string.IsNullOrEmpty(con.SearchValue))
                    para.Add(con);
            }
            list.Clear();
            return para;
        }
    }
}