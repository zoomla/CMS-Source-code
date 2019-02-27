namespace ZoomLaCMS.BU.Exam
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Exam;
    using ZoomLa.BLL.Helper;
    using ZoomLa.BLL.User;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    using ZoomLa.Model.User;
    using ZoomLa.SQLDAL;
    public partial class Paper : System.Web.UI.Page
    {
        B_Temp tempBll = new B_Temp();
        B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        B_Exam_Answer answerBll = new B_Exam_Answer();
        B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
        B_User buser = new B_User();
        B_TempUser userBll = new B_TempUser();
        HtmlHelper htmlHelp = new HtmlHelper();
        //试卷ID
        private int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
        DataTable QuestDT { get { return (DataTable)ViewState["QuestDT"]; } set { ViewState["QuestDT"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            QuestDT = null;
        }
        private void MyBind()
        {
            M_UserInfo mu = userBll.GetLogin();
            // if (mu.UserID < 1 || Mid < 1) { return; }
            M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(Mid);
            //M_Temp tempMod = tempBll.SelModelByUid(mu.UserID, 10);
            //ExTime = DataConverter.CLng(paperMod.p_UseTime);
            //if (tempMod == null || string.IsNullOrEmpty(tempMod.Str1)) { function.WriteErrMsg("试题篮为空,请先选择试题!"); }
            Title_L.Text = paperMod.p_name;
            //string ids = "";
            //DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "SELECT p_id FROM ZL_Exam_Sys_Questions");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    ids += dr["p_id"] + ",";
            //}
            //ids = ids.TrimEnd(',');
            QuestDT = questBll.SelByIDSForExam(paperMod.QIDS, paperMod.id);//获取问题,自动组卷则筛选合适的IDS
                                                                           //QuestDT.DefaultView.RowFilter = "";
                                                                           //QuestDT_Hid.Value = JsonConvert.SerializeObject(QuestDT.DefaultView.ToTable(false, "p_id,p_title,p_type,p_defaultScores,istoshare,pid".Split(',')));
            DataTable typeDT = answerBll.GetTypeDT(QuestDT);
            MainRPT.DataSource = typeDT;
            MainRPT.DataBind();
        }
        protected void MainRPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                Repeater RPT = e.Item.FindControl("RPT") as Repeater;
                DataTable dt = null;
                string normFilter = "p_type=" + drv["QType"] + " AND IsToShare=0 AND (pid=0 OR pid IS NULL)";
                string bigfilter = "pid=" + drv["QType"] + " AND IsToShare=0";
                if (drv["IsBig"].ToString().Equals("0")) { QuestDT.DefaultView.RowFilter = normFilter; }
                else { QuestDT.DefaultView.RowFilter = bigfilter; }
                dt = QuestDT.DefaultView.ToTable();
                RPT.DataSource = dt;
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
                        string tlp = "<td title='{0}' style='width:100px;'>{1}. {2}</td>";
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
                        string tlp = "<td title='{0}' style='width:100px;'>{1}. {2}</td>";
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
                        string tlp = "<td>解：</td>";
                        builder.Append(tlp);
                    }
                    break;
                case (int)M_Exam_Sys_Questions.QType.FillTextBlank:
                    {
                        string optionjson = SafeSC.ReadFileStr(M_Exam_Sys_Questions.OptionDir + id + ".opt");
                        JArray array = JsonConvert.DeserializeObject<JArray>(optionjson);
                        builder.Append(FillTextHtml(array));
                    }
                    break;
            }
            string html = builder.ToString().Trim();
            if (!string.IsNullOrEmpty(html))
            {

                html = "<table><tr>" + html + "</tr></table><br/>";
            }
            return html;
        }
        //完型填空html生成
        public string FillTextHtml(JArray array)
        {
            string tlp = "<div style='margin-top:10px'><div>{0},{1}</div> <div style='margin-left:20px;'>{2}</div></div>";
            string childtlp = " <div style='margin-top:5px;'>{0}.{1}</div>";
            string html = "<td>";
            foreach (JToken item in array)
            {
                JArray childarray = (JArray)item["opts"];
                string childhtml = "";
                foreach (JToken item_i in childarray)
                {
                    string tempstr = htmlHelp.ConvertImgUrl(item_i["val"].ToString().Replace("<p>", "").Replace("</p>", ""), "http://" + Request.Url.Authority);
                    childhtml += string.Format(childtlp, item_i["op"], tempstr);
                }
                html += string.Format(tlp, item["id"], item["title"], childhtml);
            }
            return html + "</td>";
        }

        public string GetContent()
        {
            string content = Eval("p_content").ToString();
            if (DataConverter.CLng(Eval("p_Type")) == 4)
            {
                string[] conArr = Regex.Split(content, Regex.Escape("（）"));
                content = "";
                for (int i = 0; i < conArr.Length; i++)
                {
                    if (i != (conArr.Length - 1))
                    { content += conArr[i] + string.Format("（<span style='color:green'>{0}</span>）", (i + 1)); }
                }
                return content;
            }
            return htmlHelp.ConvertImgUrl(Eval("p_content", ""), SiteConfig.SiteInfo.SiteUrl);
        }
    }
}