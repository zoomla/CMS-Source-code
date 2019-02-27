using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using BDUBLL;
using BDUModel;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.BLL;

namespace ZoomLa.GatherStrainManage
{
    public partial class CreatGS : Page
    {
        #region 业务对象
        //GSManageBLL gsBll = new GSManageBLL();
        B_User ubll = new B_User();
        /// <summary>
        /// 群族可访问性
        /// </summary>
        private int IsPublic
        {
            get
            {
                if (ViewState["IsPublic"] != null)
                {
                    return int.Parse(ViewState["IsPublic"].ToString());
                }
                else return 0;
            }
            set
            {
                ViewState["IsPublic"] = value;
            }
        }
        #endregion

        #region 业务对象初始化
        protected void Page_Load(object sender, EventArgs e)
        { 
            ubll.CheckIsLogin();
            if (!IsPostBack)
            {
                M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
                if (Page.Request.QueryString["IsPublic"] != null)
                {
                    GetGSType();
                    ViewState["IsPublic"] = Page.Request.QueryString["IsPublic"];
                }
                
            }
        }
        #endregion

        #region 辅助方法
        //获取群族类型
        private void GetGSType()
        {
            //List<GSType> list = gsBll.GetGSTypeList();
            //this.dropGSType.DataSource = list;
             
            //dropGSType.DataBind();
        }

        private void PageToGS(GatherStrain gs)
        {
            gs.Cryptonym = this.checkboxCryptonym.Checked;
            gs.GSIntro = this.txtGSInfo.Text;
            gs.GSName = this.txtGSName.Text;
            gs.GSType = new Guid(this.dropGSType.SelectedValue);
            gs.IsPublic = IsPublic;
            gs.UserID = ubll.GetLogin().UserID;
            gs.GSICO = txtpic.Text ;
        }
        #endregion

        #region 页面功能
        protected void btnOK_Click(object sender, EventArgs e)
        {
           
        }
        #endregion 
    }
}
