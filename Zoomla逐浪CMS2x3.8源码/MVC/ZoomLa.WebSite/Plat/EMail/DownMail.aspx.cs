using Newtonsoft.Json;
using System;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Components.Mail;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;
//提供邮件的下载功能,模态框调用

namespace ZoomLaCMS.Plat.EMail
{
    public partial class DownMail : System.Web.UI.Page
    {
        //配置的邮箱ID,未传的话则按用户下载
        private int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
        public delegate void AsyncDownMail(M_UserInfo mu, System.Web.SessionState.HttpSessionState context);
        B_Plat_Mail mailBll = new B_Plat_Mail();
        B_Plat_MailConfig configBll = new B_Plat_MailConfig();
        B_User buser = new B_User();
        DownMailProg MyProgMod
        {
            get
            {
                if (Session["Mail_DownProg"] == null || string.IsNullOrEmpty(Session["Mail_DownProg"].ToString())) { return null; }
                return JsonConvert.DeserializeObject<DownMailProg>(Session["Mail_DownProg"].ToString());
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                #region 获取进度
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "getprogress":
                        if (MyProgMod == null) { result = "-1"; }
                        else { result = JsonConvert.SerializeObject(MyProgMod); }
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                #endregion
            }
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                if (configBll.SelByUid(mu.UserID).Rows.Count < 1)
                {
                    function.WriteErrMsg("你当前尚未配置邮箱,点击<a href='MailConfig.aspx'>前往配置</a>");
                }
                //M_Plat_MailConfig configMod = configBll.SelModelByUid(Mid, mu.UserID);
                //---------------------------------
                //如有任务存在,则未完成
                if (MyProgMod != null && !MyProgMod.iscomplete)
                {
                    function.Script(this, "BeginCheck();");
                }
                else
                {
                    //否则新建一个下载任务
                    AsyncDownMail downmail = DownMailFunc;
                    System.Web.SessionState.HttpSessionState session = Session;
                    System.IAsyncResult asynResult = downmail.BeginInvoke(mu, session, null, null);
                    function.Script(this, "BeginCheck();");
                }
            }
        }
        private void DownMailFunc(M_UserInfo mu, System.Web.SessionState.HttpSessionState session)
        {
            string ppath = function.VToP(SiteConfig.SiteOption.UploadDir + "\\" + mu.UserName + mu.UserID + @"\EMail\");
            SafeSC.CreateDir(ppath);
            DataTable mailDT = mailBll.SelMailIDByUid(mu.UserID);//避免重复下载邮件
            DataTable dt = configBll.SelByUid(mu.UserID);
            DownMailProg progMod = new DownMailProg();
            //-----------------
            FactoryPop3 popFactory = new FactoryPop3();
            //Pop3 pop= popFactory.CreatePop3();
            //OpenPopPop3 pop = new OpenPopPop3();
            OpenPopPop3 pop = new OpenPopPop3();
            pop.Pop3Port = 110;
            //附件的物理存储路径
            foreach (DataRow dr in dt.Rows)
            {
                pop.Pop3Address = dr["POP"].ToString();//pop.exmail.qq.com(QQ企业邮箱),pop3.163.com,pop.exmail.qq.com,pop.qq.com
                pop.EmailAddress = dr["ACount"].ToString();
                pop.EmailPassword = dr["Passwd"].ToString();
                try { if (!pop.Authenticate()) { continue; } }
                catch (Exception ex) { ZLLog.L("邮件接收异常,邮箱[" + pop.EmailAddress + "],原因:" + ex.Message); continue; }
                int count = pop.GetMailCount();
                progMod.email = pop.EmailAddress;
                progMod.total = count;
                progMod.index = 1;
                for (int i = count, index = 1; i >= 1; i--, index++)//从最后往前加,先更新日期最近的
                {
                    Session["Mail_DownProg"] = JsonConvert.SerializeObject(progMod);
                    try
                    {
                        progMod.index = index;
                        string mailid = pop.GetMailUID(i);
                        mailDT.DefaultView.RowFilter = "MailID='" + mailid + "'";
                        if (mailDT.DefaultView.ToTable().Rows.Count > 0) continue;//已存在
                        DateTime sendtime = pop.GetMailDate(i);
                        int days = DataConverter.CLng(dr["Days"].ToString()) == 0 ? 30 : DataConverter.CLng(dr["Days"].ToString());
                        //大于指定天数的邮件则不下载
                        if ((DateTime.Now - sendtime).TotalDays > days) { break; }
                        //--------------------模型
                        M_Plat_Mail mailMod = new M_Plat_Mail();
                        mailMod.UserID = mu.UserID;
                        mailMod.Sender = pop.GetSendMialAddress(i);
                        mailMod.Title = pop.GetMailSubject(i);
                        mailMod.Receiver = pop.EmailAddress;
                        mailMod.Content = pop.GetMailBodyAsText(i);
                        mailMod.CDate = DateTime.Now;
                        mailMod.MailDate = sendtime;
                        mailMod.MailID = mailid;
                        mailMod.Attach = pop.GetMailAttach(i, ppath);
                        mailMod.Status = 1;
                        mailBll.Insert(mailMod);
                    }
                    catch (Exception ex)
                    {
                        ZLLog.L("邮件接收异常,邮箱[" + pop.EmailAddress + "],原因:" + ex.Message);
                    }
                }
                pop.Pop3Close();
            }
            //------完成下载
            progMod.iscomplete = true;
            progMod.index = progMod.total;
            Session["Mail_DownProg"] = JsonConvert.SerializeObject(progMod);
        }
    }
    public class DownMailProg
    {
        //当前邮箱有多少待收邮件
        public int total = 0;
        //当前第多少封
        public int index = 0;
        //当前邮箱
        public string email = "";
        public bool iscomplete = false;
    }
}