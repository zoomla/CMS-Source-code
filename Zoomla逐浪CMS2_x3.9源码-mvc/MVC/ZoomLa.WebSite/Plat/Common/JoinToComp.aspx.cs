using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.BLL.Helper;

namespace ZoomLaCMS.Plat.Common
{
    public partial class JoinToComp : System.Web.UI.Page
    {
        B_Plat_Comp compBll = new B_Plat_Comp();
        M_Plat_Comp compMod = null;
        private int CompID { get { return DataConverter.CLng(ViewState["CompID"]); } set { ViewState["CompID"] = value; } }
        //用户必须未加入公司,才可进入此页,用户不注册则默认都进入
        //加入企业后,移除原本的企业信息
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_User_Plat upMod = B_User_Plat.GetLogin();
                compMod = compBll.SelReturnModel(upMod.CompID);
                if (compMod.Status == 1)
                {
                    noauth_div.Visible = true;
                    noauth_l.InnerHtml = "你已加入了" + compMod.CompName;
                }
                else
                {
                    join_div.Visible = true;
                }
            }
        }

        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            nocomp_div.Visible = false;
            hascomp_div.Visible = false;
            compMod = compBll.SelModelByName(Search_T.Text);
            if (compMod == null)
            {
                nocomp_l.InnerHtml = "[" + Search_T.Text + "]不存在,请核对公司名称后再搜索";
                nocomp_div.Visible = true;
            }
            else
            {
                hascomp_div.Visible = true;
                CompID = compMod.ID;
                Logo_Img.Src = compMod.CompLogo;
                CompName_L.Text = compMod.CompName;
                CompDesc_L.Text = compMod.CompDesc;
            }
        }
        //申请加入公司(一张表用于存其)//ZL_Plat_JoinCompApply
        protected void AddComp_Btn_Click(object sender, EventArgs e)
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            //记录用户信息并写入
            M_Common_UserApply ualyMod = new M_Common_UserApply();
            B_Common_UserApply ualyBll = new B_Common_UserApply();
            ualyMod.UserID = upMod.UserID;
            ualyMod.ZType = "plat_joincomp";
            ualyMod.UserName = upMod.UserName + "(" + upMod.TrueName + ")";
            ualyMod.Remind = CompID.ToString();
            ualyMod.IP = IPScaner.GetUserIP();
            ualyBll.Insert(ualyMod);
            function.WriteSuccessMsg("申请成功,请等待管理员审核", "/Plat/");
        }
    }
}