using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZoomLaCMS.Manage.Exam
{
    public partial class QuestOption : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.Transfer("/User/Exam/QuestOption?" + Request.QueryString);
        }
    }
}