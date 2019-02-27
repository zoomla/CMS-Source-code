using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;

namespace ZoomLaCMS.Manage.Boss
{
    public partial class BossInfo : CustomerPageAction
    {
        B_BossInfo boll = new B_BossInfo();
        private B_User bll = new B_User();
        private B_Node bnll = new B_Node();
        private B_Card bc = new B_Card();
        private B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li>系统设置</li><li>加盟商管理</li>");
                M_UserInfo uinfo = buser.GetLogin();
                int nodeid = string.IsNullOrEmpty(Request.QueryString["nodeid"]) ? -1 : DataConverter.CLng(Request.QueryString["nodeid"]);
                int pardentid = string.IsNullOrEmpty(Request.QueryString["pardentid"]) ? -1 : DataConverter.CLng(Request.QueryString["pardentid"]);
                if (nodeid == -1)
                    function.WriteErrMsg("参数错误");
                this.HiddenNode.Value = nodeid.ToString();
                this.HiddenPnode.Value = pardentid.ToString();
                M_BossInfo MBoss = boll.GetSelect(DataConverter.CLng(nodeid));
                this.tx_cname.Text = MBoss.CName;
                M_BossInfo Mb = boll.GetSelect(nodeid);
                DataTable ddc = bc.SelCarByUserID(Mb.userid);
                gvCard.DataSource = ddc;
                gvCard.DataBind();
                this.fhwunum.Text = "";
                this.Enum.Text = "";
            }
        }
        protected string GetUserName(string uid)
        {
            if (DataConverter.CLng(uid) == 0)
            {
                return "暂无用户";
            }
            else
            {
                return buser.GetUserByUserID(int.Parse(uid)).UserName;
            }
        }
        protected string GetState(string str)
        {
            string state = "";
            switch (str)
            {
                case "1":
                    state = "未启用";
                    break;
                case "2":
                    state = "启用";
                    break;
                case "3":
                    state = "停用";
                    break;
            }
            return state;
        }
    }
}