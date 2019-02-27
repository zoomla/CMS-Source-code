namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Model;
    using ZoomLa.BLL;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Data;
    using System.Text;
    using System.IO;
    using System.Web.UI.HtmlControls;
    public partial class StudioInfoListByTech : System.Web.UI.Page
    {
        protected B_Recruitment rll = new B_Recruitment();
        public int Uid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                int eid = rll.GetSelect(id).EnrolllistID;

                rll.DeleteByGroupID(id);
                Response.Redirect("StudioInfoListByTech.aspx?id=" + eid.ToString());
            }
            MyBind();
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>培训资料库<a href='AddStudioInfo.aspx?id=" + Request.QueryString["id"] + "'>[添加学员信息]</a> <a href='javascript:void(0)' data-toggle=\"modal\" data-target=\"#InputUser_div\" onclick='open_window()'>[导入招生资料]</a></li>");
        }
        public void MyBind()
        {
            Rec_RPT.DataSource = rll.GetRecruintment(Uid);
            Rec_RPT.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string item = Request.Form["idchk"];
            if (item != null && item != "")
            {
                if (item.IndexOf(',') > -1)
                {
                    string[] itemarr = item.Split(',');
                    for (int i = 0; i < itemarr.Length; i++)
                    {
                        rll.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                    }
                }
                else
                {
                    rll.DeleteByGroupID(DataConverter.CLng(item));
                }
            }
            function.WriteSuccessMsg("操作成功!", "ApplicationManage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ViewState["tableinfo"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page page = new Page();
                HtmlForm form = new HtmlForm();
                string titles = "学员姓名,预设用户名,预设登录密码,联系电话,联系地址,身份证号码,登记时间,备注";
                string[] usertit = titles.Split(',');
                DataTable viewtable = (DataTable)ViewState["tableinfo"];
                sb.Append(titles.Replace(",", "\t"));
                sb.Append("\n");
                for (int ii = 0; ii < viewtable.Rows.Count; ii++)
                {
                    if (viewtable != null && viewtable.Rows.Count > 0)
                    {
                        int sssid = DataConverter.CLng(viewtable.Rows[0]["ssid"]);
                        M_Recruitment rinfo = rll.GetSelect(sssid);
                        sb.Append(rinfo.Studioname + "\t");
                        sb.Append(rinfo.PriorUserName + "\t");
                        sb.Append(rinfo.LogPassWord + "\t");
                        sb.Append(rinfo.Tel + "\t");
                        sb.Append(rinfo.Addinfo + "\t");
                        sb.Append(rinfo.CradNo + "\t");
                        sb.Append(rinfo.WriteTime + "\t");
                        sb.Append(rinfo.Remark + "\t");
                    }
                    sb.Append("\n");
                }
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=Recruitment(" + DateTime.Now.ToString() + ").xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }
        }
    }
}