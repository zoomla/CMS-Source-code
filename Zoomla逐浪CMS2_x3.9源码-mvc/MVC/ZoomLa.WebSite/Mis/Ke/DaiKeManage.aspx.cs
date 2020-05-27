using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.BLL.Exam;
using ZoomLa.BLL.Room;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.Model.Exam;
using System.Data;

namespace ZoomLaCMS.MIS.Ke
{
    public partial class DaiKeManage : System.Web.UI.Page
    {
        B_EDU_AutoPK pkBll = new B_EDU_AutoPK();
        B_User buser = new B_User();
        B_ExClassgroup classBll = new B_ExClassgroup();
        B_ExTeacher teachBll = new B_ExTeacher();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                MyBind();
            }
        }
        void MyBind()
        {
            DataTable dt = pkBll.SelTeachConfig();
            RPT.DataSource = dt;
            RPT.DataBind();
        }

        public string GetNumInfo()
        {
            string difftime = "";
            string[] datas = Eval("NumInfo").ToString().Split(',');
            switch (datas[0])
            {
                case "moring":
                    difftime = "上午";
                    break;
                case "afternoon":
                    difftime = "下午";
                    break;
                case "evening":
                    difftime = "晚上";
                    break;
            }
            return difftime + ":" + "第" + datas[2] + "课";
        }
    }
}