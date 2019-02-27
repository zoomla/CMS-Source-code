namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using System.Data;
    public partial class ApplicationManage : System.Web.UI.Page
    {
        protected B_User ull = new B_User();
        protected B_Recruitment rll = new B_Recruitment();
        protected B_EnrollList ell = new B_EnrollList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "delete")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    ell.DeleteByGroupID(id);
                }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li>培训资源库<a href='AddEnroll.aspx'>[添加招生信息]</a> <a href='javascript:void(0)' data-toggle=\"modal\" data-target=\"#TechUser_div\" onclick='open_window()'>[导入招生资料]</a></li>");
            }
        }
        public void MyBind()
        {
            Repeater1.DataSource = rll.GetRecruintmentall();
            Repeater1.DataBind();
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

        protected string GetUserName(string UserID)
        {
            M_UserInfo uinfo = ull.GetUserByUserID(DataConverter.CLng(UserID));
            M_Uinfo ubaseinfo = ull.GetUserBaseByuserid(DataConverter.CLng(UserID));
            return uinfo.UserName;
        }
        protected string getusercount(string userid)
        {
            return rll.GetRencount(DataConverter.CLng(userid)).Rows.Count.ToString();
        }
    }
}