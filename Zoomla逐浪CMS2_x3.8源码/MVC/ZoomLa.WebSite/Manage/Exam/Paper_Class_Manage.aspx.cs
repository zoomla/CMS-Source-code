using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model.Exam;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using Newtonsoft.Json;

namespace ZoomLaCMS.Manage.Exam
{
    public partial class Paper_Class_Manage : System.Web.UI.Page
    {
        B_Exam_PaperNode nodeBll = new B_Exam_PaperNode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "getchild":
                        result = JsonConvert.SerializeObject(nodeBll.SelByPid(DataConverter.CLng(Request.Form["nid"])));
                        break;
                    case "del":
                        nodeBll.Del(DataConverter.CLng(Request.Form["value"]));
                        result = "1";
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                return;
            }
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='Papers_System_Manage.aspx'>试卷管理</a></li><li>试卷分类管理<a href='AddPaperClass.aspx'>[添加分类]</a></li>" + Call.GetHelp(76));
            }
        }
        public void MyBind()
        {
            RPT.DataSource = nodeBll.SelByPid(0);
            RPT.DataBind();
        }

        public string GetIcon()
        {
            return DataConverter.CLng(Eval("ChildCount")) > 0 ? "fa fa-folder" : "fa fa-file";
        }
    }
}