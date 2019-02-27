using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class User_survey_SurveyResult : System.Web.UI.Page
{
    B_User buser = new B_User();
    M_Survey msurvey = new M_Survey();
    XmlDocument xmldoc = new XmlDocument();
    XmlElement xmle = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["surveyID"].ToString()))
        {
            this.ViewState["surveyID"] = Request.Form["surveyID"];
        }
        else
        {
            Response.Write(@"<script language=javascript>alert(""问卷不存在或已经过期！"");location='../../'</script>");
        }        
        msurvey = new B_Survey().GetSurveyBySid(Convert.ToInt32(this.ViewState["surveyID"]));
        if (msurvey.NeedLogin)
        {
            buser.CheckIsLogin();
            this.ViewState["filename"] = buser.GetLogin().UserName;
        }
        else
        {
            this.ViewState["filename"] = Request.Form["filename"];
        }
        int scorenum = ScoreNum();
        if (scorenum < 60)
        {
            Response.Write("<script>location.href='../../SurveyXml/SurveyResult/1.htm';</script>");
        }
        else if (scorenum >= 60 && scorenum < 100)
        {
            Response.Write("<script>location.href='../../SurveyXml/SurveyResult/2.htm';</script>");
        }
        else if (scorenum >= 100)
        {
            Response.Write("<script>location.href='../../SurveyXml/SurveyResult/3.htm';</script>");      
        }
    }
    protected int ScoreNum()
    {
        string pathstr = Server.MapPath("~/SurveyXml/" + this.ViewState["filename"] + ".xml");        
        xmldoc.Load(pathstr);
        XmlNodeList list = xmldoc.SelectNodes("/SurveyScore/score" + this.ViewState["surveyID"]);
        int num = 0;
        for (int i = 0; i < list.Count; i++)
        {
            xmle = (XmlElement)list[i];
            num += Convert.ToInt32(xmle.Attributes["scoreNum"].InnerText);
        }
        return num;
    }
}