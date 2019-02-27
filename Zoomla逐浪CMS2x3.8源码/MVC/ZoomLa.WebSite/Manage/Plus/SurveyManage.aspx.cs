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

namespace ZoomLaCMS.Manage.Plus
{
    public partial class SurveyManage : CustomerPageAction
    {
        B_Survey surBll = new B_Survey();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ADManage.aspx'>扩展功能</a></li><li class='active'><a href='SurveyManage.aspx'>问卷投票管理</a><a href='Survey.aspx'>[添加问卷投票]</a></li>");
            }
        }
        private void MyBind()
        {
            DataTable dtable = new DataTable();
            EGV.DataSource = B_Survey.GetSurveyList();
            EGV.DataBind();
        }

        // 获取问卷的类型
        protected string GetSurType(string type)
        {
            string value = "";
            switch (type)
            {
                case "1":
                    value = "投票";
                    break;
                case "2":
                    value = "问卷";
                    break;
                case "3":
                    value = "报名系统";
                    break;
                default:
                    break;
            }
            return value;
        }
        // 由 bool 值 true / false 转换为图像的名称： yes/no
        protected string BoolValueToImgName(string strflag)
        {
            if (DataConverter.CBool(strflag))
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }
        protected void gviewSurvey_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Del":
                    string Id = e.CommandArgument.ToString();
                    new B_Survey().Del(DataConverter.CLng(Id));
                    MyBind();
                    break;
                default:
                    break;
            }
        }
        protected void gviewSurvey_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='Survey.aspx?SID=" + dr["SurveyID"] + "'");
            }
        }
        // 搜索指定问卷
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.txtSurKey.Text.Trim()))
            //    this.HdnKey.Value = "";
            //else
            //    this.HdnKey.Value = this.txtSurKey.Text.Trim();
            MyBind();
        }
        //批量删除
        protected void btndelete_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                surBll.DelByIDS(ids);
                MyBind();
            }
        }
        // ajax 控制图像的变化
        protected void imgbtnCanNull_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            string imgurl = ibtn.ImageUrl;
            string imgName = imgurl.Substring(imgurl.LastIndexOf('/') + 1, imgurl.Length - imgurl.LastIndexOf('.') - 1);
            if (imgName == "yes")
            {
                ibtn.ImageUrl = "~/Images/no.gif";
            }
            else
            {
                ibtn.ImageUrl = "~/Images/yes.gif";
            }
            int sid = DataConverter.CLng(ibtn.CommandArgument);
            M_Survey survey = (new B_Survey()).SelReturnModel(sid);
            survey.IsOpen = !survey.IsOpen;
            surBll.UpdateSurvey(survey);
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
    }
}