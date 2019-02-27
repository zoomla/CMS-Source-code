namespace ZoomLaCMS.Manage.Exam
{
    using Aspose.Words;
    using Aspose.Words.Saving;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Winista.Text.HtmlParser.Filters;
    using Winista.Text.HtmlParser.Visitors;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Exam;
    using ZoomLa.BLL.Helper;
    using ZoomLa.BLL.User;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.User;
    public partial class ViewPaperCenter : System.Web.UI.Page
    {
        B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        B_Exam_Class classBll = new B_Exam_Class();
        B_Exam_Answer answerBll = new B_Exam_Answer();
        B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
        DataTable QuestDT { get { return (DataTable)ViewState["QuestDT"]; } set { ViewState["QuestDT"] = value; } }
        public int PaperID { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PaperID < 1) { function.WriteErrMsg("请输入试卷ID"); }
                MyBind();
                Call.HideBread(Master);
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            QuestDT = null;
        }
        private void MyBind()
        {
            M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(PaperID);
            Title_T.Text = paperMod.p_name;
            QuestDT = questBll.SelByIDSForExam(paperMod.QIDS, PaperID);//获取问题,自动组卷则筛选合适的IDS
            if (QuestDT != null)
            {
                QuestDT.DefaultView.RowFilter = "";
                DataTable typeDT = answerBll.GetTypeDT(QuestDT);
                MainRPT.DataSource = typeDT;
                MainRPT.DataBind();
            }
        }
        protected void MainRPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                Repeater RPT = e.Item.FindControl("RPT") as Repeater;
                DataTable dt = null;
                string normFilter = "p_type=" + drv["QType"] + " AND IsToShare=0 AND (pid=0 OR pid IS NULL)";
                string bigfilter = "pid=" + drv["QType"] + " AND IsToShare=0";//big下,qtype为其id
                if (drv["IsBig"].ToString().Equals("0")) { QuestDT.DefaultView.RowFilter = normFilter; }
                else { QuestDT.DefaultView.RowFilter = bigfilter; }
                dt = QuestDT.DefaultView.ToTable();
                if (dt.Columns.Contains("order"))
                {
                    dt.DefaultView.Sort = "order asc";
                }
                RPT.DataSource = dt.DefaultView.ToTable();
                RPT.DataBind();
            }
        }
        public string GetSubmit()
        {
            string option = SafeSC.ReadFileStr(M_Exam_Sys_Questions.OptionDir + Eval("p_id") + ".opt");
            int id = Convert.ToInt32(Eval("p_id"));
            JArray arr = JsonConvert.DeserializeObject<JArray>(option);
            StringBuilder builder = new StringBuilder();
            //单,多,填,解
            switch (DataConverter.CLng(Eval("p_Type")))
            {
                case (int)M_Exam_Sys_Questions.QType.Radio:
                    {
                        if (arr == null || arr.Count < 1) { return "未定义选项"; }
                        string name = "srad_" + id;
                        string tlp = "<li class='opitem'><label><input type='radio' name='{0}' value='{1}'>{1}. {2}</label></li>";
                        foreach (JObject obj in arr)
                        {
                            builder.Append(string.Format(tlp, name, obj["op"], obj["val"]));
                        }
                    }
                    break;
                case (int)M_Exam_Sys_Questions.QType.Multi:
                    {
                        if (arr == null || arr.Count < 1) { return "未定义选项"; }
                        string name = "mchk_" + id;
                        string tlp = "<li class='opitem'><label><input class='opitem' type='checkbox' name='{0}' value='{1}'>{1}. {2}</label></li>";
                        foreach (JObject obj in arr)
                        {
                            builder.Append(string.Format(tlp, name, obj["op"], obj["val"]));
                        }
                    }
                    break;
                case (int)M_Exam_Sys_Questions.QType.FillBlank:
                    {
                        //string tlp = "<div contenteditable='true' class='answerdiv'>解：</div>";
                        //builder.Append(tlp);
                    }
                    break;
                case (int)M_Exam_Sys_Questions.QType.Answer://放置一个ueditor
                    {
                        string name = "answer_" + id;
                        string tlp = "<div id='" + name + "' contenteditable='true' class='answerdiv'>解：</div>";
                        builder.Append(tlp);
                    }
                    break;
            }
            return builder.ToString();
        }
        protected void Sure_Btn_Click(object sender, EventArgs e)
        {
            M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(PaperID);
            paperMod.QIDS = Qids_Hid.Value;
            paperMod.QuestList = QInfo_Hid.Value;
            paperBll.UpdateByID(paperMod);
            function.WriteSuccessMsg("保存成功");
        }
    }
}