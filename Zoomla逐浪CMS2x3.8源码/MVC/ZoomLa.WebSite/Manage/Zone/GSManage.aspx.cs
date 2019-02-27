using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.Zone
{
    public partial class GSManage : CustomerPageAction
    {
        #region 调用业务逻辑
        //GSManageBLL gsmbll = new GSManageBLL();
        B_Admin abll = new B_Admin();
        B_User ubll = new B_User();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            ZoomLa.Common.function.AccessRulo();
            if (!IsPostBack)
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li class='active'>空间族群列表</li>");
        }
        #region 页面调用方法
        public void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            Egv.DataSource = dt;// gsmbll.GetAllGatherStrain(Guid.Empty, string.Empty, null);
            Egv.DataBind();
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = Egv.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = Egv.PageSize;
            }
            Egv.PageSize = pageSize;
            Egv.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        private Dictionary<string, string> Order
        {
            get
            {
                Dictionary<string, string> dt = new Dictionary<string, string>();
                dt.Add("Addtime", "0");
                return dt;
            }
        }
        protected string GetState(string state)
        {
            switch (state)
            {
                case "0":
                    return "正常";
                case "1":
                    return "<strong><font color=\"#FF6600\">高亮</font></strong>";
                case "2":
                    return "<font color=\"#CCCCCC\">冻结</font>";
                default:
                    return "无";
            }
        }
        protected string GetUserName(string userid)
        {
            return ubll.GetUserByUserID(int.Parse(userid)).UserName;
        }


        protected void btn_DeleteRecords_Click(object sender, EventArgs e)
        {
            string idst = Request.Form["chkSel"];
            Button lb = sender as Button;
            if (idst != null && idst != "")
            {
                string[] list = idst.Split(new char[] { ',' });
                foreach (string st in list)
                {
                    //if (lb.CommandName == "5")
                    //{
                    //    gsmbll.DeleteGS(new Guid(st));
                    //}
                    //else
                    //{
                    //    gsmbll.UpdateState(int.Parse(lb.CommandName), new Guid(st));
                    //}
                }
            }
            DataBind();
        }
        #endregion
    }
}