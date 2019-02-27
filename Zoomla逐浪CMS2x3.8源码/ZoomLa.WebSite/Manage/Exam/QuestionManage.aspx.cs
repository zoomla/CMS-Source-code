using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Xml;
using ZoomLa.Components;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class manage_Question_QuestionManage : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("QuestList.aspx?" + Request.QueryString);   
    }
}
