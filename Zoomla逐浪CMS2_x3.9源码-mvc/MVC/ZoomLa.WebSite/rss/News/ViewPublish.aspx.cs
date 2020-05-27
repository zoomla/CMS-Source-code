using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.rss.News
{
    public partial class ViewPublish : System.Web.UI.Page
    {
        public M_Content_Publish pubMod = new M_Content_Publish();
        B_Content_Publish pubBll = new B_Content_Publish();
        public int Pid { get { return DataConvert.CLng(Request.QueryString["Pid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = DataConvert.CLng(Request.QueryString["ID"]);
                MyBind(id);
            }
        }
        public void MyBind(int id = 0)
        {
            M_Content_Publish curMod = new M_Content_Publish();
            pubMod = pubBll.SelReturnModel(Pid);
            if (pubMod == null) function.WriteErrMsg("报纸不存在");
            if (id > 0)
                curMod = pubBll.SelReturnModel(id);
            else
                curMod = pubBll.SelModel(id, Pid, "n");
            if (curMod == null) function.WriteErrMsg("[" + pubMod.NewsName + "]下没有任何文章!!");
            Literal1.Text = pubMod.NewsName;
            newimg.Src = curMod.ImgPath;
            CurID_Hid.Value = curMod.ID.ToString();
            Title_Span.InnerText = curMod.Title;
            lbDown.Visible = !string.IsNullOrEmpty(curMod.AttachFile);
            function.Script(this, "AnalyJson('" + curMod.Json + "');");
        }
        protected void Pre_Btn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(CurID_Hid.Value);
            pubMod = pubBll.SelModel(id, Pid, "p");
            if (pubMod != null)
            { Response.Redirect(Request.Path + "?id=" + pubMod.ID + "&Pid=" + Pid); }
            else function.Script(this, "alert('当前已是最前一篇');");
        }
        protected void Next_Btn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(CurID_Hid.Value);
            pubMod = pubBll.SelModel(id, Pid, "n");
            if (pubMod != null)
            { Response.Redirect(Request.Path + "?id=" + pubMod.ID + "&Pid=" + Pid); }
            else function.Script(this, "alert('当前已是最后一篇');");
        }
        protected void NextPid_Btn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path + "?Pid=" + pubBll.GetPid(Pid, "n"));
        }
        protected void PrePid_Btn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path + "?Pid=" + pubBll.GetPid(Pid, "p"));
        }
        protected void lbDown_Click(object sender, EventArgs e)
        {
            int id = DataConvert.CLng(Request.QueryString["ID"]);
            pubMod = pubBll.SelReturnModel(id);
            SafeSC.DownFile(pubMod.AttachFile);
        }
    }
}