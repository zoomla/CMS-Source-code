using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;

namespace ZoomLaCMS.MIS.OA
{
    public partial class Main : System.Web.UI.Page
    {
        B_User buser = new B_User();
        PagedDataSource[] pds = new PagedDataSource[2];
        B_OA_UserConfig ucBll = new B_OA_UserConfig();
        B_OA_Document oaBll = new B_OA_Document();
        B_Message msgBll = new B_Message();
        B_Guest_Bar barBll = new B_Guest_Bar();
        public M_OA_UserConfig ucMod = new M_OA_UserConfig();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                //用户配置块
                ucMod = ucBll.SelModelByUserID(buser.GetLogin().UserID);
                if (ucMod != null)//为空则不检测
                {
                    mainChk1.Visible = ucMod.HasAuth(ucMod.MainChk, "mainChk1");
                    mainChk2.Visible = ucMod.HasAuth(ucMod.MainChk, "mainChk2");
                    //mainChk3.Visible = ucMod.HasAuth(ucMod.MainChk, "mainChk3");
                    mainChk4.Visible = ucMod.HasAuth(ucMod.MainChk, "mainChk4");
                    mainChk5.Visible = ucMod.HasAuth(ucMod.MainChk, "mainChk5");
                    mainChk6.Visible = ucMod.HasAuth(ucMod.MainChk, "mainChk6");
                }
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = oaBll.SelDocByUid(mu.UserID);
            wait_sp.InnerText = dt.Rows.Count.ToString();//待办
            Span2.InnerText = msgBll.GetUnReadMail(mu.UserID).Rows.Count.ToString();
            //论坛交流
            RPT.DataSource = barBll.SelFocus();
            RPT.DataBind();
        }
        public DataTable DtSelectTop(int TopItem, DataTable oDT)
        {
            if (oDT.Rows.Count < TopItem)
                return oDT;

            DataTable NewTable = oDT.Clone();
            DataRow[] rows = oDT.Select("1=1");
            for (int i = 0; i < TopItem; i++)
            {
                NewTable.ImportRow((DataRow)rows[i]);
            }
            return NewTable;
        }
    }
}