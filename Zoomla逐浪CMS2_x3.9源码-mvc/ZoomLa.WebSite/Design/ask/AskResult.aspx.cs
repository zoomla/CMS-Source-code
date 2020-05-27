using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Design
{
    public partial class AskResult : System.Web.UI.Page
    {
        B_Design_Ask askBll = new B_Design_Ask();
        B_Design_Question questBll = new B_Design_Question();
        B_Design_Answer ansBll = new B_Design_Answer();
        B_Design_AnsDetail ansdeBll = new B_Design_AnsDetail();
        B_User buser = new B_User();
        B_Admin badmin = new B_Admin();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Mid < 1) { function.WriteErrMsg("未指定需要问卷"); }
                if (buser.CheckLogin() || badmin.CheckLogin()) { }
                else { B_User.CheckIsLogged(Request.RawUrl); }
                M_Design_Ask askMod = askBll.SelReturnModel(Mid);
                DataTable questDT = questBll.Sel(askMod.ID, "qlist");
                RPT.DataSource = questDT;
                RPT.DataBind();
                title_sp.InnerHtml = askMod.Title + "的结果分析";
                if (DBCenter.Count(ansBll.TbName, "AskID=" + askMod.ID) < 1) { empty_div.Visible = true; RPT.Visible = false; }
            }
        }
        public string GetQType()
        {
            return questBll.GetQType(Eval("QType", ""));
        }
        public string GetPercent()
        {
            double percent = DataConvert.CDouble(Eval("percent"));
            return (percent * 100).ToString("f2") + "%";
        }
        public string ShowChartWrap()
        {
            string qtype = Eval("QType", "");
            if (qtype.Equals("radio") || qtype.Equals("checkbox")) { return ""; }
            else { return "style='display:none;'"; }
        }
        public string TbName = "ZL_Design_AnsDetail";
        //将选项的数据存入字段,以便前端绘图
        private void SaveChartHid(RepeaterItemEventArgs e, DataTable opdt)
        {
            //text:count-->name:value
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("name", typeof(string)));
            dt.Columns.Add(new DataColumn("value", typeof(string)));
            foreach (DataRow opdr in opdt.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = opdr["text"];
                dr["value"] = opdr["count"];
                dt.Rows.Add(dr);
            }
            (e.Item.FindControl("chart_hid") as HiddenField).Value = JsonConvert.SerializeObject(dt);
        }
        private DataTable GetAnsDT(int qid)
        {
            DataTable ansDT = SqlHelper.ExecuteTable("SELECT answer,COUNT(Answer)AS count FROM ZL_Design_AnsDetail WHERE Qid=" + qid + " AND Answer!='' AND Answer IS NOT NULL GROUP BY Answer");
            for (int i = 0; i < ansDT.Rows.Count; i++)
            {
                ansDT.Rows[i]["count"] = DataConvert.CLng(ansDT.Rows[i]["count"]);
            }
            return ansDT;
        }
        protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                //text:count
                DataRowView drv = e.Item.DataItem as DataRowView;
                int qid = Convert.ToInt32(drv["ID"]);
                M_Design_Question questMod = questBll.SelReturnModel(qid);
                switch (drv["QType"].ToString())
                {
                    case "radio":
                        {
                            Repeater rpt = e.Item.FindControl("Radio_RPT") as Repeater;
                            DataTable optionDT = JsonConvert.DeserializeObject<DataTable>(questMod.QOption);//四舍五入导至不匹配
                            optionDT.Columns.Add(new DataColumn("count", typeof(int)));
                            optionDT.Columns.Add(new DataColumn("percent", typeof(double)));
                            DataTable ansDT = GetAnsDT(qid);
                            int total = ansDT.Rows.Count < 1 ? 0 : Convert.ToInt32(ansDT.Compute("SUM(count)", ""));
                            for (int i = 0; i < optionDT.Rows.Count; i++)
                            {
                                DataRow opdr = optionDT.Rows[i];
                                foreach (DataRow ansdr in ansDT.Rows)
                                {
                                    string answer = ansdr["answer"].ToString();
                                    int count = Convert.ToInt32(ansdr["count"]);
                                    if (opdr["value"].ToString().Equals(answer)) { opdr["count"] = DataConvert.CLng(opdr) + count; }
                                }
                            }
                            for (int i = 0; i < optionDT.Rows.Count; i++)
                            {
                                double count = DataConvert.CDouble(optionDT.Rows[i]["count"]);
                                optionDT.Rows[i]["count"] = count;//未选择的为0
                                optionDT.Rows[i]["percent"] = count / total;
                            }
                            rpt.DataSource = optionDT;
                            rpt.DataBind();
                            SaveChartHid(e, optionDT);
                        }
                        break;
                    case "checkbox":
                        {
                            Repeater rpt = e.Item.FindControl("Checkbox_RPT") as Repeater;
                            DataTable optionDT = JsonConvert.DeserializeObject<DataTable>(questMod.QOption);
                            optionDT.Columns.Add(new DataColumn("count", typeof(int)));
                            optionDT.Columns.Add(new DataColumn("percent", typeof(double)));
                            //多选项一个回答,会有多个结果,所以需要再处理一次
                            DataTable ansDT = GetAnsDT(qid);
                            int total = ansDT.Rows.Count < 1 ? 0 : Convert.ToInt32(ansDT.Compute("SUM(count)", ""));
                            for (int i = 0; i < optionDT.Rows.Count; i++)
                            {
                                DataRow opdr = optionDT.Rows[i];
                                foreach (DataRow ansdr in ansDT.Rows)
                                {
                                    string[] ansArr = ansdr["answer"].ToString().Split(',');
                                    int count = Convert.ToInt32(ansdr["count"]);
                                    foreach (string ans in ansArr)
                                    {
                                        if (opdr["value"].ToString().Equals(ans)) { opdr["count"] = DataConvert.CLng(opdr) + count; }
                                    }
                                }
                            }
                            for (int i = 0; i < optionDT.Rows.Count; i++)
                            {
                                double count = DataConvert.CDouble(optionDT.Rows[i]["count"]);
                                optionDT.Rows[i]["count"] = count;
                                optionDT.Rows[i]["percent"] = count / total;
                            }
                            rpt.DataSource = optionDT;
                            rpt.DataBind();
                            SaveChartHid(e, optionDT);
                        }
                        break;
                    case "blank"://前台直接判断出链接即可
                        break;
                    case "score":
                        {
                            JObject jobj = JsonConvert.DeserializeObject<JObject>(questMod.QFlag);
                            string score = "0";
                            TextBox text = e.Item.FindControl("Score_T") as TextBox;
                            text.Attributes.Add("data-stars", DataConvert.CLng(jobj["maxscore"]).ToString());
                            DataTable dt = DBCenter.SelWithField(TbName, "AVG(CAST(Answer as int))", "Answer IN('1','2','3','4','5','6','7','8','9','10') AND Qid=" + qid);
                            if (dt.Rows.Count > 0) { score = DataConvert.CDouble(dt.Rows[0][0]).ToString("f1"); }
                            text.Text = score;
                        }
                        break;
                    case "sort"://未实现
                        {

                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}