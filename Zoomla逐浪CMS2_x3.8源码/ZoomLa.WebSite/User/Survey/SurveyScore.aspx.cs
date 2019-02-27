using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.IO;

public partial class User_SurveyScore : System.Web.UI.Page
{
    B_User buser = new B_User();
    M_Survey msurvey = new M_Survey();
    string pathStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
            //为上一条信息记录
            if (!string.IsNullOrEmpty(Request.Form["surveyID"]))
            {
                SetScore();
                this.ViewState["surveyID"] = Request.Form["surveyID"];
            }
            //问卷是否存在
            else {
                if (!string.IsNullOrEmpty(Request.QueryString["surveyID"]))
                {
                    this.ViewState["surveyID"] = Request.QueryString["surveyID"];
                }
                else
                {
                    Response.Write(@"<script language=javascript>alert(""问卷不存在或已经过期！"");location='../../'</script>");
                }
            }
            this.surveyID.Value = this.ViewState["surveyID"].ToString();
            //接受问卷是否需要登录
            msurvey = new B_Survey().GetSurveyBySid(Convert.ToInt32(this.ViewState["surveyID"]));
            if (msurvey.NeedLogin)
            {
                buser.CheckIsLogin();
                if (!string.IsNullOrEmpty(Request.Form["filename"]))
                {
                    this.ViewState["filename"] = Request.Form["filename"];
                }
                else {
                    this.ViewState["filename"] = buser.GetLogin().UserName;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.Form["filename"]))
                {
                    this.ViewState["filename"] = Request.Form["filename"];
                }
                else {
                    this.ViewState["filename"] = "SV" + DateTime.Now.Ticks;
                }                
            }
            this.filename.Value = this.ViewState["filename"].ToString();
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\SurveyXml");
            FileInfo fi;
            if (dir.GetFiles(this.ViewState["filename"] + ".xml").Length > 0)
                pathStr = Server.MapPath("~/SurveyXml/" + this.ViewState["filename"] + ".xml");
            else
            {
                fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\SurveyXml\" + this.ViewState["filename"] + ".xml");
                StreamWriter strw = fi.CreateText();
                strw.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?><SurveyScore></SurveyScore>");
                strw.Close();
                pathStr = Server.MapPath("~/SurveyXml/" + this.ViewState["filename"] + ".xml");
            }       

        DataTable dt = new DataTable();
        dt = B_Survey.GetQuestionList(Convert.ToInt32(this.ViewState["surveyID"]));
        DataRow dataRow = dt.NewRow();
        dataRow["Qtitle"] = @"回答完毕，去看看结果>>>>>><label style=""cursor:pointer"" id=""SeeResult"" runat=""server"" onclick=""SeeResult()""><img style=""cursor:pointer"" alt=""结果"" src=""../../Images/survey/result.jpg"" id=""rs"" /></label>";
        dt.Rows.Add(dataRow);
        Bind(dt); 
    }
    private void Bind(DataTable dd)
    {        
        int CPage, temppage;
        temppage = Convert.ToInt32(Request.Form["CurrentPage"]);
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }

        DataTable Cll = dd;
        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        cc.PageSize = 1;
        cc.CurrentPageIndex = CPage - 1;
        RepDiv.DataSource = cc;
        RepDiv.DataBind();

        int thispagenull = cc.PageCount;//总页数
        int CurrentPage = cc.CurrentPageIndex;
        int nextpagenum = CPage - 1;//上一页
        int downpagenum = CPage + 1;//下一页
        int Endpagenum = thispagenull;
        if (thispagenull <= CPage)
        {
            downpagenum = thispagenull;
            this.showScore.Visible = false;
        }
        this.CurrentPage.Value = downpagenum.ToString();//下一个问题的页码
        //是第一个题目
        if (nextpagenum <= 0)
        {
            this.Nextpage.Visible = false;
        }
        //不是第一个题目，提供向上翻功能
        else
        {
            this.Nextpage.Visible = true;         
        }
    }
    protected void SetScore()
    {
        //等于1时表示通过“上一个”到此，等于0时表示通过“下一个”到此 
        if (Request.Form["NextOrprior"]=="1")
        {
            return;
        }       
        string surveyID = Request.Form["surveyID"];
        string filename = Request.Form["filename"];
        int CurrentPage = Convert.ToInt32(Request.Form["CurrentPage"]);
        string SaveScore = Request.Form["SaveScore"];              

        pathStr = Server.MapPath("~/SurveyXml/" + filename + ".xml");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(pathStr);
        XmlNodeList list = xmldoc.SelectNodes("/SurveyScore/score" + surveyID);
        XmlElement xmle = null;
        int pagenum = 0;
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                xmle = (XmlElement)list[i];
                if (xmle.GetAttribute("pageNum") == (CurrentPage - 1).ToString())
                {
                    pagenum = CurrentPage - 1;
                    xmle.SetAttribute("scoreNum", SaveScore);
                    break;
                }
            }
        }
        if (pagenum == 0)
        {
            XmlElement xmlElement = xmldoc.DocumentElement;
            XmlElement xmlel = xmldoc.CreateElement("score" + surveyID);
            xmlel.SetAttribute("pageNum", (CurrentPage - 1).ToString());
            xmlel.SetAttribute("scoreNum", SaveScore);
            xmlElement.AppendChild(xmlel);
        }        
        xmldoc.Save(pathStr);
    }
}
