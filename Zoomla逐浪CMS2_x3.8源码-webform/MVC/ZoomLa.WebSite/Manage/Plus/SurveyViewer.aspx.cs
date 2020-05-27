using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Plus
{
    public partial class SurveyViewer : CustomerPageAction
    {
        int surveyID = 0;
        protected B_Survey surBll = new B_Survey();
        protected B_Answer ansBll = new B_Answer();
        protected M_Survey surMod = new M_Survey();
        B_Answer_Recode bans = new B_Answer_Recode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Request.QueryString["SID"]))
            {
                function.WriteErrMsg("缺少问卷投票的ID参数！", Request.UrlReferrer.ToString());
                return;
            }
            surveyID = Convert.ToInt32(Request.QueryString["SID"]);
            function.AccessRulo();
            if (ViewState["PageSize"] == null)
            {
                ViewState["PageSize"] = 15;
            }
            if (ViewState["CurntNum"] == null)
            {
                ViewState["CurntNum"] = 1;
            }
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                surMod = surBll.SelReturnModel(surveyID);
                SurveySorceDBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SurveyManage.aspx'>问卷投票管理</a></li><li><a href='Survey.aspx?SID=" + surMod.SurveyID + "'>" + surMod.SurveyName + "</a></li><li class='active'>问卷来源分析</li>");
            }
        }
        //问卷来源信息， 绑定数据
        protected void SurveySorceDBind()
        {
            int pageSize = Convert.ToInt32(ViewState["PageSize"]);
            int curntNum = Convert.ToInt32(ViewState["CurntNum"]);

            //List<M_Answer_Recode> lstRecords = B_Answer_Recode.GetRecordsBySid(surveyID);
            DataTable lstRecords = B_Answer_Recode.GetRecordsBySid(surveyID);
            EGV.PageSize = pageSize;
            EGV.PageIndex = curntNum - 1;
            EGV.DataSource = lstRecords;
            EGV.DataBind();
        }

        //用户名称
        protected string GetUserName(string id)
        {
            B_User buser = new B_User();
            if (!buser.IsExit(Convert.ToInt32(id)))
            {
                return "佚名";
            }
            else
                return buser.SeachByID(Convert.ToInt32(id)).UserName;
        }
        //ip 所在地
        protected string GetCitybyIP(string ip)
        {
            return "";

        }
        // 下拉页码的绑定
        public void ListDataBind()
        {
            int curntNum = Convert.ToInt32(ViewState["CurntNum"]);
            int pages = Convert.ToInt32(ViewState["Pages"]);
            List<int> lstNums = new List<int>();
            for (int i = 1; i <= pages; i++)
            {
                lstNums.Add(i);
            }
        }
        //底部分页导航
        protected void LbtnAlterPage_Click(object sender, EventArgs e)
        {
            int curntNum = Convert.ToInt32(ViewState["CurntNum"]);
            int pages = Convert.ToInt32(ViewState["Pages"]);
            LinkButton lbtn = sender as LinkButton;
            switch (lbtn.CommandName)
            {
                case "First":
                    ViewState["CurntNum"] = 1;
                    break;
                case "Previous":
                    curntNum--;
                    if (curntNum < 1)
                        curntNum = 1;
                    ViewState["CurntNum"] = curntNum;
                    break;
                case "Next":
                    curntNum++;
                    if (curntNum > pages)
                        curntNum = pages;
                    ViewState["CurntNum"] = curntNum;
                    break;
                case "Last":
                    ViewState["CurntNum"] = pages;
                    break;
            }
            SurveySorceDBind();
        }
        protected void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            ViewState["PageSize"] = (sender as TextBox).Text;
            ViewState["CurntNum"] = 1;
            SurveySorceDBind();
        }
        protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["CurntNum"] = (sender as DropDownList).SelectedValue;
            SurveySorceDBind();
        }
        protected void gviewSurSorcer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                int rid = Convert.ToInt32(this.EGV.DataKeys[e.Row.RowIndex].Value.ToString());
                M_Answer_Recode redMod = bans.SelReturnModel(rid);
                e.Row.Attributes.Add("onmouseover", "this.className='tdbgmouseover'");
                e.Row.Attributes.Add("onmouseout", "this.className='tdbg'");
                e.Row.Attributes.Add("onclick", "javascript:location.href='AnswerView.aspx?SID=" + redMod.Sid + "&UID=" + redMod.Userid + "'");
                e.Row.Attributes.Add("style", "cursor:pointer");
                e.Row.Attributes.Add("title", "双击查看");
            }
        }
        protected void gviewSurSorcer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            SurveySorceDBind();
        }
        protected void gviewSurSorcer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Export":
                    ExportControl(EGV);
                    break;
                case "Status":
                    bans.Update("Status=1", "Rid=" + id);
                    sendmsg(1, id);
                    break;
                case "Concle":
                    bans.Update("Status=0", "Rid=" + id);
                    sendmsg(0, id);
                    break;
                case "Selected":
                    bans.Update("Status=2", "Rid=" + id);
                    sendmsg(2, id);
                    break;
                case "NLock":
                    bans.Update("Status=-2", "Rid=" + id);
                    sendmsg(2, id);
                    break;
                case "Del1":
                    M_Answer_Recode redMod = bans.SelReturnModel(Convert.ToInt32(id));
                    ansBll.DelByUid(redMod.Userid, redMod.Sid);
                    bans.Del(id);
                    break;
                default:
                    break;
            }
            SurveySorceDBind();
        }
        protected string GetStatus(string status)
        {
            switch (status)
            {
                case "0":
                    return "未审核";
                case "1":
                    return "已审核";
                case "2":
                    return "已录用";
                case "-1":
                    return "未提交";
                case "-2":
                    return "已解锁";

                default:
                    return "未审核";
            }

        }
        // 导出结果为Word 文档
        public void ExportControl(Control source)
        {
            string OutPutName = "用户信息列表_" + DateTime.Now.ToString("yyMMddhhmmss");
            //设置Http的头信息,编码格式 
            //Word 
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(OutPutName, Encoding.UTF8) + ".doc");
            Response.ContentType = "application/ms-word";
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //关闭控件的视图状态 
            source.Page.EnableViewState = false;
            (source as GridView).AllowPaging = false;
            //初始化HtmlWriter 
            System.IO.StringWriter writer = new System.IO.StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);
            source.RenderControl(htmlWriter);
            //输出 
            Response.Write(writer.ToString());
            Response.End();
            (source as GridView).AllowPaging = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void sendmsg(int type, int id)
        {
            //int SID = string.IsNullOrEmpty(Request.QueryString["SID"]) ? 0 : DataConverter.CLng(Request.QueryString["SID"]);
            //B_Answer ban = new B_Answer();
            //DataTable dt = B_Survey.GetQuestion("Qid", "surveyid=" + SID + " and typeid=4 and  Qcontent like '%|1'", "");
            //DataTable dts = surBll.Sel(SID);
            //string str = "";
            //switch (type)
            //{
            //    case 0:
            //        str = "已取消审核";
            //        break;
            //    case 1:
            //        str = "已审核";
            //        break;
            //    case 2:
            //        str = "已录取";
            //        break;
            //    case -2:
            //        str = "已解锁";
            //        break;
            //    default:
            //        break;
            //}
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    DataTable tblAnswers = ban.Sel(" Qid=" + dt.Rows[0]["Qid"], "");
            //    sendMsg.Visible = true;
            //    sendMsg.InnerHtml = " <span class='closex'><a href='javascript:void(0)' onclick='hid(\"sendMsg\")'>×</a></span> <strong>发送手机短信</strong><br /><iframe src='/manage/user/MobileMsg.aspx?MB=" + tblAnswers.Rows[0]["AnswerContent"] + "&txt=您好，您提交的[" + dts.Rows[0]["SurveyName"] + "]信息" + str + "。详情请进入" + SiteConfig.SiteInfo.SiteUrl + "[" + SiteConfig.SiteInfo.SiteName + "]' width='600' height='380' formborder='0' scrolling='no'></iframe>";
            //}
        }
        public string GetScoreAll(string uid)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["SID"]) && !string.IsNullOrEmpty(uid))
                return surBll.GetScoreByUID(Convert.ToInt32(Request.QueryString["SID"]), Convert.ToInt32(uid));
            else
                return "";
        }
        protected void DelAll_Click(object sender, EventArgs e)
        {
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    M_Answer_Recode redMod = bans.SelReturnModel(Convert.ToInt32(chkArr[i]));
                    ansBll.DelByUid(redMod.Userid, redMod.Sid);
                    bans.Del(Convert.ToInt32(chkArr[i]));
                }
            }
            SurveySorceDBind();
        }
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["chkSel"]))
            {
                string[] chkArr = Request.Form["chkSel"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}