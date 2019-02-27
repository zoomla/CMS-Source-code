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
using System.Collections.Generic;
using System.Text;

public partial class Common_VoteResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ////判断是否被屏蔽
        //string ip = Request.UserHostAddress;
        //if (IsIPShielded(ip))
        //{
        //    function.WriteErrMsg("你的IP已被列入黑名单， 禁止访问此页面！", "../Plus/SurveyManage.aspx");
        //    return;
        //}
        if (!this.Page.IsPostBack)
        {
            //B_Admin badmin = new B_Admin();
            //
            int SID = string.IsNullOrEmpty(Request.QueryString["SID"]) ? 0 : DataConverter.CLng(Request.QueryString["SID"]);
            if (SID <= 0)
                function.WriteErrMsg("缺少问卷投票的ID参数！");
            else
            {
                M_Survey info =new B_Survey().GetSurveyBySid(SID);
                if (info.IsNull)
                {
                    function.WriteErrMsg("该问卷投票不存在！可能已被删除");
                }
                else
                {
                    if (info.IsShow == 0)
                        function.WriteErrMsg("该问卷结果前台显示未启用!!");
                    this.ltlSurveyName.Text = info.SurveyName;
                    this.ltlDate.Text = DateTime.Now.AddSeconds(-30).ToString("yyyy-MM-dd hh:mm:ss");
                    IList<M_Question> list = new List<M_Question>();
                    list = B_Survey.GetQueList(SID);
                    rptResult.DataSource = list;
                    rptResult.DataBind();
                }
            }
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        //返回 问卷首页， 须修改。。
        Response.Redirect(Page.ResolveClientUrl(CustomerPageAction.customPath+"Plus/SurveyManage.aspx"));
    }

    //题目类型
    protected string GetQuesType(object qid)
    {
        int id = Convert.ToInt32(qid);
        string type = "";
        switch (id)
        {
            case 1:
                type = "单选题";
                break;
            case 2:
                type = "多选题";
                break;
            case 3:
            case 4:
                type = "填空题";
                break;
            case 5:
                type = "图片";
                break;
            default:
                break;
        }
        return type;
    }

    // 判断用户IP是否被屏蔽   -- 测试
    protected bool IsIPShielded(string ip)
    {
        List<string> ipaddrs = new List<string>();
        for (int i = 15; i < 50; i++)
        {
            ipaddrs.Add("192.168.1." + i);
        }

        if (ipaddrs.Contains(ip))
            return true;
        return false;
    }

    // 长字符串截取
    protected string CutContent(string content, int len)
    {
        string str = content;
        if (content.Length > len)
            str = content.Substring(0, len)+"..";
        return str;
    }
    protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            return;
        M_Question rowv = (M_Question)e.Item.DataItem;
        if (rowv.TypeID >= 3)
        {
            ((HtmlGenericControl)e.Item.FindControl("divTable")).Style["display"] = "none";
            (e.Item.FindControl("ltlTitle") as Literal).Text = rowv.QuestionTitle;
            return;
        }
        GridView gview = e.Item.FindControl("gviewOption") as GridView;
        string options = rowv.QuestionContent;//内容
        List<string> lstopts = new List<string>();
        for (int i = 0; i < options.Split('|').Length; i++)
        {
            lstopts.Add(options.Split('|')[i].Split(':')[0]);
        }

        int sum = 0;
        string ratios = "";
        string names = "";
        gview.RowDataBound += delegate(object s, GridViewRowEventArgs ex)
        {
            int total = 0;
            string rate = "";
            int papers = B_Survey.GetUsersCnt(rowv.SurveyID);
            if (ex.Row.RowType == DataControlRowType.DataRow)
            {                
                total = B_Survey.GetAnsOptionCount(rowv.QuestionID, ex.Row.DataItem.ToString());
                (ex.Row.FindControl("ltlTotal") as Literal).Text = total.ToString();
                sum += total;
                if (papers == 0)
                    rate = "未填写";
                else
                    rate = ((float)total / papers).ToString("P");
                (ex.Row.FindControl("ltlRate") as Literal).Text = rate;
                names += CutContent(ex.Row.DataItem.ToString(), 4) + ",";
                ratios += total + ",";
            }
            if (ex.Row.RowType == DataControlRowType.Footer)
            {
                (ex.Row.FindControl("ltlSum") as Literal).Text = papers.ToString();
            }
        };
        gview.DataSource = lstopts;
        gview.DataBind();
    }

    // 导出结果为Word 文档
    public void ExportControl(Control source)
    {
        string OutPutName = "分析报告_" + DateTime.Now.ToString("yyMMddhhmmss");
        //设置Http的头信息,编码格式 

        //Word 
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(OutPutName, Encoding.UTF8) + ".doc");
        Response.ContentType = "application/ms-word";

        ////Excel 
        //Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(OutPutName, System.Text.Encoding.UTF8) + ".xls");
        //Response.ContentType = "application/ms-excel"; 

        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        //关闭控件的视图状态 
        source.Page.EnableViewState = false;
        //初始化HtmlWriter 
        System.IO.StringWriter writer = new System.IO.StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);
        source.RenderControl(htmlWriter);
        //输出 
        Response.Write(writer.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportControl(divContent);
    }
}

