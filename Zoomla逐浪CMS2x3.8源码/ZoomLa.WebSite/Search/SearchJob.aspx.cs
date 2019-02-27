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

public partial class Search_SearchJob : System.Web.UI.Page
{
    private B_Node bnod = new B_Node();
    private B_Model bmod = new B_Model();
    private B_ModelField bfield = new B_ModelField();
    private B_Content bll = new B_Content();
    private B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            int ModelID = string.IsNullOrEmpty(Request.QueryString["ModelID"]) ? 0 : DataConverter.CLng(Request.QueryString["ModelID"]);
            if (ModelID <= 0)
                function.WriteErrMsg("没有指定要搜索的内容模型ID");
            else
            {
                this.HdnModel.Value = ModelID.ToString();
                this.LblModelName.Text = "详细查询" + bmod.GetModelById(ModelID).ItemName + "信息";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       // ShowGrid();
       // this.ViewState["QuerryList"] = GetNewQuerysString();
        string modeid = this.HdnModel.Value;
     this.Session["QuerryList"] = GetNewQuerysString();
     //   Response.Redirect("SearchJobOut.aspx?modeid=" + modeid);
        Response.Write("<script>window.open('SearchJobOut.aspx?modeid=" + modeid + "')</script>");
        
      //  RepNodeBind();
    }
   
    private IList<M_SearchCondition> GetNewQuerysString()
    {
        string str = "";
        string str2 = "";
        IList<M_SearchCondition> para = new List<M_SearchCondition>();
        string TableName = bmod.GetModelById(DataConverter.CLng(this.HdnModel.Value)).TableName;
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
                    con.SearchValue = DataSecurity.FilterBadChar(BaseClass.Htmlcode(Request.Form["txt_" + list[i].FieldName]));
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
                        con.SearchValue = DataSecurity.FilterBadChar(BaseClass.Htmlcode(Request.Form["txt_" + list[i].FieldName]));
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
                            con.SearchValue = DataSecurity.FilterBadChar(BaseClass.Htmlcode(Request.Form["txt_" + list[i].FieldName]));
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
                        con.SearchValue =BaseClass.Htmlcode(Request.Form["txt_" + list[i].FieldName]);
                        con.SearchType = 0;
                        con.RangeType = DataConverter.CLng(dateunit);
                    }
                    break;
                default:
                    con.FieldType = "text";
                    con.SearchValue = DataSecurity.FilterBadChar(BaseClass.Htmlcode(Request.Form["txt_" + list[i].FieldName]));
                    con.SearchType = 1;
                    con.RangeType = 0;
                    break;
            }
            if (!string.IsNullOrEmpty(con.SearchValue))
                para.Add(con);
        }
        return para;
    }
    //private string GetJobUrl(int p, int modeid)
    //{
    //    M_ModelInfo model = bmod.GetModelById(DataConverter.CLng(modeid));

    //    DataTable dt = buser.GetUserModeInfo(model.TableName, p, 11);
    //    if (dt.Rows.Count > 0)
    //    {

    //        if (dt.Rows[0]["IsCreate"].ToString() == "True")
    //        {
    //            return SiteConfig.SiteInfo.SiteUrl + "/" + model.TableName + "/" + model.TableName + "_" + p + ".html";
    //        }
    //        else
    //        {

    //            return "../User/Info/ShowModel.aspx?ModelID=" + modeid + "&id=" + p;
    //        }
    //    }
    //    else
    //    {
    //        return "../User/Info/ShowModel.aspx?ModelID=" + modeid + "&id=" + p;
    //    }
    //}
}
