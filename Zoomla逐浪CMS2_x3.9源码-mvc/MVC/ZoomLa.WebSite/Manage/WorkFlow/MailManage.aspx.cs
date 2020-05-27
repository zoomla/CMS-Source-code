using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.WorkFlow
{
    public partial class MailManage : CustomerPageAction
    {
        private B_User buser = new B_User();
        private B_Message msgBll = new B_Message();
        private B_Structure strBll = new B_Structure();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li class='active'>邮箱管理</li>");
        }
        private void MyBind()
        {
            DataTable dt = new DataTable();
            if (!SizeStatus.Equals("All"))//仅显示无容量用户
            {
                if (TempDT != null)
                {
                    function.WriteErrMsg("123");
                    dt = TempDT;
                }
                else
                {
                    dt = buser.SelAll();
                    dt.Columns.Add(new DataColumn("flag", typeof(int)));
                    dt = GetNoSizeDT(dt);
                    TempDT = dt;
                }
            }
            else//全部用户
            {
                if (string.IsNullOrEmpty(SearchKey))
                {
                    dt = buser.SelAll();
                }
                else
                {
                    dt = buser.SearchByInfo(SearchKey);
                }
            }

            EGV.DataSource = dt;
            EGV.DataBind();
        }
        public string GetGroupName(string GroupID)
        {
            return "";
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del":
                    break;
            }
            MyBind();
        }
        public string SearchKey
        {
            get
            {
                if (ViewState["SearchKey"] == null)
                {
                    ViewState["SearchKey"] = "";
                }
                return ViewState["SearchKey"].ToString();
            }
            set { ViewState["SearchKey"] = value; }
        }
        public string SizeStatus
        {
            get
            {
                if (ViewState["SizeStatus"] == null)
                    ViewState["SizeStatus"] = "All";
                return ViewState["SizeStatus"].ToString();
            }
            set { ViewState["SizeStatus"] = value; }
        }
        public DataTable TempDT { get { return Session["MailManage_TempDT"] == null ? null : (DataTable)Session["MailManage_TempDT"]; } set { Session["MailManage_TempDT"] = value; } }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchKey = searchText.Text;
            MyBind();
        }
        #region 计算容量
        public string MailRemind(M_UserInfo mu, out int flag)
        {
            string maxSize = "", usedSize = "", surSize = "";
            float percent = 0;
            DataTable dt = msgBll.SelMyMail(mu.UserID, 4);
            usedSize = GetFileSize(dt).ToString("0.0");
            if (mu.MailSize == -1)
            {
                maxSize = "无限制";
                surSize = "无限制";
                percent = 0;
                flag = 1;
            }
            else if (mu.MailSize == 0)
            {
                maxSize = OAConfig.MailSize.ToString();
                surSize = CheckMailSize(mu).ToString();
                percent = (float.Parse(usedSize) / float.Parse(maxSize)) * 100;
                flag = Convert.ToDouble(surSize) < 1 ? 0 : 1;
            }
            else
            {
                maxSize = mu.MailSize.ToString();
                surSize = CheckMailSize(mu).ToString();
                percent = (float.Parse(usedSize) / float.Parse(maxSize)) * 100;
                flag = Convert.ToDouble(surSize) < 1 ? 0 : 1;
            }
            return string.Format("你有{0}M空间,已用{1}M,尚余{2}M", maxSize, usedSize, surSize);
        }
        //返回剩余容理,以M为单位,为-1不限制,不进此
        public float CheckMailSize(M_UserInfo mu)
        {
            //我的发件+草稿+回收站
            DataTable dt = msgBll.SelMyMail(mu.UserID, 4);
            //float usedSize = GetFileSize(oacom.OADir + @"\Mail\" + mu.UserName + "\\");
            float usedSize = GetFileSize(dt);
            float maxSize = 0;
            if (mu.MailSize == -1)
            {
                maxSize = 1000;
            }
            else if (mu.MailSize == 0)
            {
                maxSize = OAConfig.MailSize;
            }
            else
            {
                maxSize = mu.MailSize;
            }
            float surSize = maxSize - usedSize < 0 ? 0 : maxSize - usedSize;
            return float.Parse(surSize.ToString("0.0"));
        }
        public float GetFileSize(DataTable dt)
        {
            //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            //long size = FileSystemObject.getDirectorySize(path);
            //return (size / 1024 / 1024f);
            long size = 0;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    foreach (string vpath in dr["AttachMent"].ToString().Split(','))
                    {
                        size += new FileInfo(Server.MapPath(vpath)).Length;
                    }
                }
                catch { }//function.WriteErrMsg(dr["AttachMent"].ToString());
            }
            return (size / 1024 / 1024f);
        }
        public string GetMailRemind()
        {
            M_UserInfo mu = buser.SeachByID(Convert.ToInt32(Eval("UserID")));
            int flag = 1;
            string result = MailRemind(mu, out flag);
            if (flag == 0)
                result = "<span style='color:red;'>" + result + "</span>";
            return result;
        }
        public DataTable GetNoSizeDT(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                M_UserInfo mu = buser.SeachByID(Convert.ToInt32(dr["UserID"]));
                int flag = 1;
                MailRemind(mu, out flag);
                dr["flag"] = flag;
            }
            dt.DefaultView.RowFilter = "flag=0";
            dt = dt.DefaultView.ToTable();
            return dt;
        }
        #endregion
        protected void SizeStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SizeStatus"] = SizeStatus_Dp.SelectedValue;
            MyBind();
        }
    }
}