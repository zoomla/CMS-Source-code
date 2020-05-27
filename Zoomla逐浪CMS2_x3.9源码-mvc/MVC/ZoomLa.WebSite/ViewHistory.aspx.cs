using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model.User;
using ZoomLa.BLL.User;

namespace ZoomLaCMS
{
    public partial class ViewHistory : System.Web.UI.Page
    {
        private B_User b_U = new B_User();
        private B_ViewHistory b_VH = new B_ViewHistory();
        private M_ViewHistory m_VH = new M_ViewHistory();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["InfoId"] != null && Request["type"] != null)
            {
                m_VH.InfoId = Convert.ToInt32(Request["InfoId"]);
                m_VH.type = Request["type"];
                m_VH.UserID = b_U.GetLogin().UserID;
                m_VH.addtime = DateTime.Now;
                b_VH.Add(m_VH);
            }
        }
    }
}