using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Plat.EMail
{
    public partial class AddMailConfig : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Plat_MailConfig configBll = new B_Plat_MailConfig();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Mid > 0)
                {
                    M_Plat_MailConfig model = configBll.SelModelByUid(Mid, buser.GetLogin().UserID);
                    if (model == null) { function.WriteErrMsg("信息不存在,或你无权修改该信息"); }
                    Alias_T.Text = model.Alias;
                    POP_T.Text = model.POP;
                    SMTP_T.Text = model.SMTP;
                    Acount_T.Text = model.Acount;
                    Passwd_T.Attributes.Add("value", model.Passwd);
                    Days_T.Text = model.Days.ToString();
                }
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            Acount_T.Text = Acount_T.Text.Replace(" ", "");
            M_Plat_MailConfig configMod = null;
            if (Mid > 0) { configMod = configBll.SelReturnModel(Mid); }
            else
            {
                //检测邮箱是否已存在
                configMod = configBll.SelByMail(mu.UserID, Acount_T.Text);
                if (configMod != null) { function.WriteErrMsg("[" + Acount_T.Text + "]邮箱已经存在,不能重复添加"); }
                configMod = new M_Plat_MailConfig();
            }
            configMod.POP = POP_T.Text;
            configMod.SMTP = SMTP_T.Text;
            configMod.Alias = Alias_T.Text;
            configMod.Acount = Acount_T.Text;
            configMod.Passwd = Passwd_T.Text;
            configMod.Days = DataConverter.CLng(Days_T.Text);
            configMod.Days = configMod.Days == 0 ? 30 : configMod.Days;
            if (Mid > 0)
            {
                configBll.UpdateByID(configMod);
            }
            else
            {
                configMod.UserID = mu.UserID;
                configMod.CDate = DateTime.Now;
                configBll.Insert(configMod);
            }
            function.Script(this, "parent.location=parent.location;");
        }
    }
}