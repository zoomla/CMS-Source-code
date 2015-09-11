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
using ZoomLa.Model;
using ZoomLa.Common;
using System.IO;
using System.Text;

public partial class User_Project_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitPage();
        }
    }
    public void InitPage()
    {
        B_Project bpro = new B_Project();
        B_ProjectWork bwork=new B_ProjectWork();
        M_Project mpro = new M_Project();
        StringBuilder strbul = new StringBuilder();
        int rid = 0;
        if (Request.QueryString["pid"] != null)
        {
            rid = DataConverter.CLng(Request.QueryString["pid"].Trim());
            mpro = bpro.GetProjectByid(rid);
            LblProName.Text = mpro.ProjectName;
            LblProIntro.Text = mpro.ProjectIntro;
            LblStartDate.Text = mpro.StartDate.ToShortDateString();
            if (mpro.EndDate.ToShortDateString() == DateTime.MaxValue.ToShortDateString())
            {
                LblEndDate.Text = "";
            }
            else
            {
                LblEndDate.Text = mpro.EndDate.ToShortDateString();
            }
          
            DataView dv=bwork.SelectWorkByPID(rid);
            if (dv.Table.Rows.Count > 0)
            {
                foreach (DataRow dr in dv.Table.Rows)
                {
                    strbul.Append("<a href='DiscussList.aspx?wid=" + dr["WorkID"] + "&pid=" + Request.QueryString["pid"] + "'>" + dr["WorkName"] + "</a>&nbsp;&nbsp;<a href='DiscussList.aspx?wid=" + dr["WorkID"] + "&pid=" + Request.QueryString["pid"] + "'>查看讨论</a><br/>");

                }
            }
            else
            {
                strbul.Append("暂无工作内容!");
            }
               
            LblContent.Text = strbul.ToString();
        }
    }
}
