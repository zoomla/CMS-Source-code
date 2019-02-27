namespace ZoomLaCMS.Plugins
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    public partial class Vote : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Survey surBll = new B_Survey();
        public int Sid
        {
            get { return DataConverter.CLng(Request.QueryString["Sid"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                M_Survey info = surBll.GetSurveyBySid(Sid);
                if (Sid <= 0) { function.WriteErrMsg("缺少问卷投票的ID参数！", Request.UrlReferrer.ToString()); }
                if (info == null || info.IsNull) { function.WriteErrMsg("缺少问卷!!"); }
                if (!B_Survey.HasQuestion(Sid)) { function.WriteErrMsg("该投票没有设定投票问题！"); }
                if (info.NeedLogin) { B_User.CheckIsLogged(Request.RawUrl); }
                if (info.IsCheck) { regVcodeRegister.Visible = true; }
                if (!info.IsOpen) { function.WriteErrMsg("对不起，该问卷尚未启用！！", Request.UrlReferrer.ToString()); }
                if (info.StartTime > DateTime.Now || info.EndTime < DateTime.Now) { function.WriteErrMsg("对不起，每年填写或报名时间是" + info.StartTime.ToLongDateString() + "到" + info.EndTime.ToLongDateString()); }
                //判断是否已参与了该问卷
                if (B_Survey.HasAnswerBySID(Sid, mu.UserID)) { function.WriteErrMsg("您已提交过该问卷！"); }
                CheckIP(info);
                Random_Hid.Value = GetRandomID().ToString();
                DataTable tblAnswers = new DataTable();
                if (Request.QueryString["UID"] != null && Request.QueryString["PTime"] != null)
                {
                    int uid = DataConverter.CLng(Request.QueryString["UID"]);
                    string time = Server.UrlDecode(Request.QueryString["PTime"]);
                    tblAnswers = B_Answer.GetUserAnswers(Sid, uid, time);
                }
                if (tblAnswers.Rows.Count > 0)
                {
                    btnSubmit.Visible = false;
                    btnExport.Visible = true;
                }
                else
                {
                    btnExport.Visible = false;
                    btnSubmit.Visible = true;
                }
                this.SurveyName_L.Text = info.SurveyName;
                this.Description_L.Text = info.Description;
                this.CreateDate_L.Text = info.CreateDate.ToString("yyyy-MM-dd");
                qtitle.Visible = !string.IsNullOrEmpty(info.Description);
                StringBuilder sb = new StringBuilder();
                IList<M_Question> list = new List<M_Question>();
                list = B_Survey.GetQueList(Sid);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].IsNull)
                    {
                        sb.AppendLine("<li id='mao_" + i + "' name='mao_" + i + " '>");
                        IsNull_H.Value += "vote_" + i + ",";
                    }
                    else { sb.AppendLine("<li id='mao_" + i + "' name='mao_" + i + " '>"); }
                    sb.AppendLine("<table id='tbl_" + i + "' style='width:100%;'>");
                    sb.AppendLine("<tr style='border-bottom:1px solid #ddd;padding-bottom:5px;'><th>" + (i + 1) + ". " + list[i].QuestionTitle + (list[i].IsNull ? "<span style='color:#f00;margin-left:10px;'>*</span>" : "") + "</th></tr>");
                    sb.AppendLine("<tr><td>" + list[i].QuestionContent + "</td></tr>");
                    List<string> optionlist = new List<string>();
                    JObject jobj = JsonConvert.DeserializeObject<JObject>(list[i].Qoption);
                    string[] OptionValue = list[i].TypeID == 0 ? jobj["sel_op_body"].ToString().Split(',') : jobj["text_str_dp"].ToString().Split(',');
                    string optiontype = list[i].TypeID == 0 ? jobj["sel_type_rad"].ToString() : jobj["text_type_rad"].ToString();
                    if (tblAnswers.Rows.Count > 0)
                    {
                        sb.AppendLine(SetAnswers(i, list[i].TypeID, list[i].QuestionID, tblAnswers, OptionValue));
                    }
                    else
                    {
                        sb.AppendLine(SetOptions(i, optiontype, OptionValue));
                    }
                    sb.AppendLine("</table></li>");
                }
                IsNull_H.Value = IsNull_H.Value.Trim(',');
                ltlResultHtml.Text = sb.ToString();
            }
        }
        private void CheckIP(M_Survey info)
        {
            string ip = IPScaner.GetUserIP();
            if (info.IPRepeat < 1) { return; }
            if (ip.StartsWith("192.168") || ip.Equals("::1")) { return; }
            List<SqlParameter> sp = new List<SqlParameter>() {
                new SqlParameter("ip", ip),
                new SqlParameter("sdate",DateTime.Now.ToString("yyyy/MM/dd 00:00:00")),
                new SqlParameter("edate",DateTime.Now.ToString("yyyy/MM/dd 23:59:59"))
            };
            int count = DBCenter.Count("ZL_Answer_Recode", "SubmitIP=@ip AND Sid=" + info.SurveyID + " AND (SubmitDate>@sdate AND SubmitDate<@edate)", sp);
            if (count > info.IPRepeat) { function.WriteErrMsg("对不起,系统限定：一个IP一天只能提交" + info.IPRepeat + "份,请明天再来。。"); }
        }
        //----如该问卷是不用登录的，则以随机码作为用户名
        public int GetUserID(M_Survey info)
        {
            int UserID = 0;
            if (info.NeedLogin)
            {
                UserID = buser.GetLogin().UserID;
            }
            else
            {
                UserID = DataConverter.CLng(Random_Hid.Value);
            }
            return UserID;
        }
        public int GetRandomID()
        {
            Random random = new Random();
            return random.Next(1000000, 9999999);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            M_Survey info = surBll.GetSurveyBySid(Sid);
            if (info.IsNull) { function.WriteErrMsg("该问卷不存在！可能已被删除"); }
            DateTime SubmitDate = DateTime.Now;
            string SIP = IPScaner.GetUserIP();
            //----如该问卷是不用登录的，则以随机码作为用户名
            int UserID = GetUserID(info);
            if (info.IsCheck && !CheckVCode(SendVcode.Text.Trim()))
            {
                function.Script(this, "alert('验证码不正确');");
                return;
            }
            IList<M_Question> list = B_Survey.GetQueList(Sid);
            for (int i = 0; i < list.Count; i++)
            {
                M_Answer ans = new M_Answer();
                string re = Request.Form["vote_" + i];
                string[] OptionValue = list[i].QuestionContent.Split(new char[] { '|' });
                if (list[i].TypeID == 2)
                {
                    string[] ReArr = re.Split(new char[] { ',' });
                    for (int s = 0; s < ReArr.Length; s++)
                    {
                        ans.AnswerID = 0;
                        ans.AnswerContent = ReArr[s];
                        ans.QuestionID = list[i].QuestionID;
                        ans.SurveyID = Sid;
                        ans.SubmitIP = SIP;
                        ans.SubmitDate = SubmitDate;
                        ans.UserID = UserID;
                        B_Survey.AddAnswer(ans);
                    }
                }
                else
                {
                    ans.AnswerID = 0;
                    ans.AnswerContent = re;
                    ans.QuestionID = list[i].QuestionID;
                    ans.SurveyID = Sid;
                    ans.SubmitIP = SIP;
                    ans.SubmitDate = SubmitDate;
                    ans.UserID = UserID;
                    B_Survey.AddAnswer(ans);
                }
            }
            B_Survey.AddAnswerRecord(Sid, UserID, SIP, SubmitDate, 1);
            string url = "VoteResult.aspx?SID=" + Sid;
            function.WriteSuccessMsg("提交成功！感谢您的参与！", Page.ResolveClientUrl(url));
        }

        protected object GetAnswer(int id, DataTable dtable, int type, string[] values)
        {
            string ans = "";
            DataView dv = new DataView(dtable);
            dv.RowFilter = "Qid=" + id;
            ans = dv[0][1].ToString();
            if (type > 2)
                return ans;
            int index = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == ans)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        // 设置每个选项的格式
        protected string SetOptions(int pid, string qtype, params string[] values)
        {
            StringBuilder sbuilder = new StringBuilder();
            switch (qtype)
            {
                case "radio":
                    for (int i = 0; i < values.Length; i++)
                    {
                        sbuilder.AppendFormat("<tr><td><label style='font-weight:normal;'><input type='radio' id='rdo_{0}' name='vote_{0}' value='{1}' /> {1} </label></td></tr>", pid, values[i]);
                    }
                    break;
                case "checkbox":
                    for (int i = 0; i < values.Length; i++)
                    {
                        sbuilder.AppendFormat("<tr><td><label style='font-weight:normal;'><input type='checkbox' id='chk_{0}' name='vote_{0}' value='{1}' /> {1} </label></td></tr>", pid, values[i]);
                    }
                    break;
                case "select":
                    {
                        string html = "<tr><td style='padding-top:5px;'><select id='sel_" + pid + "' name='sel_" + pid + "' class='form-control text_md'>";
                        for (int i = 0; i < values.Length; i++)
                        {
                            html += "<option value='" + values[i] + "'>" + values[i] + "</option>";
                            // sbuilder.AppendFormat("<tr><td><label style='font-weight:normal;'><input type='checkbox' id='chk_{0}' name='vote_{0}' value='{1}' /> {1} </label></td></tr>", pid, values[i]);
                        }
                        html += "</td></tr>";
                        sbuilder.Append(html);
                    }

                    break;
                case "text":
                    string tip = "";
                    string msg = "";
                    SetMessage(values[0], out tip, out msg);
                    sbuilder.AppendFormat("<tr><td style='padding-top:5px;'><input type='text' class='form-control' id='txt_{0}' name='vote_{0}' size='{1}' title='{2}' onblur='IsLegalValue({0}, \"{3}\")' />&nbsp; ", pid, values[0], tip, values[0]);
                    sbuilder.AppendFormat(" <span id='span_{0}' name='vote_{0}' style='display:none;float:right;color:red'>{1}</span></td></tr>", pid, msg);
                    break;
                case "textarea":
                    //     sbuilder.AppendFormat("<tr><td><input type='hidden' id='hdPath_{0}' name='vote_{0}' /> ", pid);
                    //     sbuilder.AppendFormat("<iframe id='UpImage_{0}', src='ImageUpload.aspx?SID={0}&QID={1}&Ext={2}', marginheight='0' marginwidth='0' frameborder='0' width='85%' height='35' scrolling='no'></iframe></td></tr>", Sid, pid, Server.UrlEncode(values[0]));
                    //     sbuilder.AppendFormat(" </tr></table><div id='viewid_{0}' runat='server' class='flaUpload' style='display:none;'><span class='closefls'><a href='javascript:void(0);'  onclick='ShowflaUp({0})'>×</a></span><object id='paizhao' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0'  height='225' width='405' classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000'  > <param name='_cx' value='11642'/> <param name='_cy' value='9737'/> <param name='FlashVars' value=''/> <param name='Movie' value='/Plugins/CutPic/CutPic2.swf'/> <param name='Src' value='/Plugins/CutPic/CutPic2.swf'/>"
                    //+ "<param name='WMode' value='transparent'/>"
                    //+ " <param name='Play' value='-1'/>"
                    //+ " <param name='Loop' value='-1'/>"
                    //+ " <script type='text/javascript '>  if (navegiator . mimeTypes && navigator . mimeTypes['application/x-shockwave-flash']) document . write('<embed src='/Plugins/CutPic/CutPic2.swf' quality='high'     type='application/x-shockwave-flash' width='405' height='225' name='paizhao'></embed>')"
                    // + "</script></object></div> ", pid);
                    sbuilder.Append("<tr><td style='padding-top:5px;'><textarea class='form-control' id='txt_{0}' name='vote_{0}'></textarea></td></tr>");
                    break;
                default:
                    break;
            }
            return sbuilder.ToString();
        }
        protected string SetAnswers(int pid, int qtype, int qid, DataTable tblans, params string[] values)
        {
            StringBuilder sbuilder = new StringBuilder();
            int index = DataConverter.CLng(GetAnswer(qid, tblans, qtype, values));
            string ans = GetAnswer(qid, tblans, qtype, values).ToString();
            switch (qtype)
            {
                case 1:
                    for (int i = 0; i < values.Length; i++)
                    {
                        sbuilder.AppendFormat("<tr><td><input type='radio' id='rdo_{0}' name='vote_{0}' value='{1}' disabled {2}/> {1} </td></tr>", pid, values[i], index == i ? "checked" : "");
                    }
                    break;
                case 2:
                    for (int i = 0; i < values.Length; i++)
                    {
                        sbuilder.AppendFormat("<tr><td><input type='checkbox' id='chk_{0}' name='vote_{0}' value='{1}' disabled {2}/> {1} </td></tr>", pid, values[i], index == i ? "checked" : "");
                    }
                    break;
                case 3:
                    sbuilder.AppendFormat("<tr><td><input type='text' id='txt_{0}' name='vote_{0}' size='{1}' disabled value='{2}' /></td></tr>", pid, values[0], ans);
                    break;
                case 4:
                    string tip = "";
                    string msg = "";
                    SetMessage(values[1], out tip, out msg);
                    sbuilder.AppendFormat("<tr><td><input type='text' id='txt_{0}' name='vote_{0}' size='{1}' title='{2}' onblur='IsLegalValue({0}, {3})' disabled value='{4}'/>&nbsp; ", pid, values[0], tip, values[1], ans);
                    break;
                case 5:
                    sbuilder.AppendFormat("<tr><td><img id='img_{0}' name='vote_{0}'  src='{1}' title='上传的图片' onload='AutoSetSize(this,280, 120)' /></td></tr>", pid, ans);
                    break;
                default:
                    break;
            }
            return sbuilder.ToString();
        }

        //设置验证错误信息
        protected void SetMessage(string vtype, out string name, out string errmsg)
        {
            errmsg = "填写错误， 格式形如：";
            switch (vtype)
            {
                case "date":
                    errmsg += "2015-01-01 08:00:00";
                    name = "时间日期";
                    break;
                case "num":
                    errmsg += "123456";
                    name = "数字";
                    break;
                case "email":
                    errmsg += "web@hx008.cn";
                    name = "邮箱地址";
                    break;
                default:
                    break;
            }
            name = "";
        }

        // 判断用户IP是否被屏蔽  -- 测试
        protected bool IsIPShielded(string ip)
        {
            List<string> ipaddrs = new List<string>();
            for (int i = 15; i < 50; i++)
            {
                ipaddrs.Add("192.168.1." + i);
            }
            if (ipaddrs.Contains(ip))
                return true;
            return false;
        }

        // 导出结果为Word 文档
        public void ExportControl(Control source)
        {
            string OutPutName = "分析报告_" + DateTime.Now.ToString("yyMMddhhmmss");
            //设置Http的头信息,编码格式 

            //Word 
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(OutPutName, Encoding.UTF8) + ".doc");
            Response.ContentType = "application/ms-word";

            ////Excel 
            //Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(OutPutName, System.Text.Encoding.UTF8) + ".xls");
            //Response.ContentType = "application/ms-excel"; 

            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //关闭控件的视图状态 
            source.Page.EnableViewState = false;
            //初始化HtmlWriter 
            System.IO.StringWriter writer = new System.IO.StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);
            source.RenderControl(htmlWriter);
            //输出 
            Response.Write(writer.ToString());
            Response.End();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportControl(divContent);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            M_Survey info = surBll.GetSurveyBySid(Sid);
            if (info.IsNull)
            {
                function.WriteErrMsg("该问卷不存在！可能已被删除");
            }
            else
            {
                DateTime SubmitDate = DateTime.Now;
                string SIP = Request.UserHostAddress;
                int UserID = GetUserID(info);
                if (info.IsCheck && !CheckVCode(SendVcode.Text.Trim()))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('验证码不正确!!');", true);
                    return;
                }
                CheckInsert();
                if (B_Survey.AddAnswerRecord(Sid, UserID, SIP, SubmitDate, -1))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "script", "<script> alert('保存成功！');</script>");
                }

            }
        }

        protected void CheckInsert()
        {
            M_Survey info = surBll.GetSurveyBySid(Sid);
            //判断是否登录
            DateTime SubmitDate = DateTime.Now;
            string SIP = Request.UserHostAddress;
            int UserID = GetUserID(info);

            //判断是否已参与了该问卷
            if (B_Survey.HasAnswerBySID(Sid, UserID)) { function.WriteErrMsg("您已提交过该问卷！"); }
            if (info.IPRepeat > 0)
            {
                if (B_Survey.HasAnswerCountIP(Sid, SIP) >= info.IPRepeat)
                    function.WriteErrMsg("于IP：" + SIP + " 提交的问卷次数已达到限定次数：" + info.IPRepeat.ToString() + "次！");
            }
            IList<M_Question> list = new List<M_Question>();
            list = B_Survey.GetQueList(Sid);
            for (int i = 0; i < list.Count; i++)
            {
                M_Answer ans = new M_Answer();
                string re = Request.Form["vote_" + i];
                string[] OptionValue = list[i].QuestionContent.Split(new char[] { '|' });
                if (string.IsNullOrEmpty(re))
                {
                    re = "";
                }
                if (list[i].TypeID == 1)
                {
                    if (list[i].IsNull && string.IsNullOrEmpty(Request.Form["vote_" + i]))
                    {
                        function.WriteErrMsg(list[i].QuestionTitle + ":为必填选项");
                    }
                    //ans.AnswerID = 0;
                    ans.AnswerContent = DataConvert.CStr(re);
                    ans.AnswerScore = surBll.GetScoreByContent(list[i].QuestionContent, re);
                    ans.QuestionID = list[i].QuestionID;
                    ans.SurveyID = Sid;
                    ans.SubmitIP = SIP;
                    ans.SubmitDate = SubmitDate;
                    ans.UserID = UserID;
                    B_Survey.AddAnswer(ans);
                }
                else
                {
                    string[] ReArr = re.Split(new char[] { ',' });
                    for (int s = 0; s < ReArr.Length; s++)
                    {
                        //ans.AnswerID = 0;
                        ans.AnswerContent = DataConvert.CStr(ReArr[s]);
                        ans.QuestionID = list[i].QuestionID;
                        ans.SurveyID = Sid;
                        ans.SubmitIP = SIP;
                        ans.SubmitDate = SubmitDate;
                        ans.UserID = UserID;
                        B_Survey.AddAnswer(ans);
                    }

                }
            }
        }
        //-----------
        /// <summary>
        /// 核对验证码
        /// </summary>
        /// <param name="code">验证码字符串</param>
        /// <returns></returns>
        public bool CheckVCode(string code)
        {
            //return ZoomlaSecurityCenter.VCodeCheck(code);
            return true;
        }
    }
}