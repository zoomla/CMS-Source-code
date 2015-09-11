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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.IO;
using System.Text;
namespace ZoomLa.WebSite.Manage.Plus
{
    public partial class ADZoneManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.ViewState["AdName"] = "";
                RepNodeBind();
            }
        }

        private void RepNodeBind()
        {
            string adname = this.ViewState["AdName"].ToString();
            if (string.IsNullOrEmpty(adname))
            {
                DataTable da = B_ADZone.ADZone_GetAll();
                if (da.Rows.Count > 0)
                {
                    this.nocontent.Style["display"] = "none";
                    this.GridView1.DataSource = da;
                    GridView1.DataKeyNames = new string[] { "ZoneID" };
                    this.GridView1.DataBind();
                    this.GridView1.Visible = true;
                }
                else
                {
                    this.nocontent.Style["display"] = "";
                    this.GridView1.Visible = false;
                }
            }
            else
            {
                DataTable da = B_ADZone.ADZone_ByCondition(" Where ZoneName like '%" + adname + "%'");
                if (da.Rows.Count != 0)
                {
                    this.nocontent.Style["display"] = "none";
                    this.GridView1.DataSource = da;
                    GridView1.DataKeyNames = new string[] { "ZoneId" };
                    this.GridView1.DataBind();
                    this.GridView1.Visible = true;
                }
                else
                {
                    this.nocontent.Style["display"] = "";
                    this.GridView1.Visible = false;
                }
            }
        }

        protected void CheckSelectAll_CheckedChanged(object sender, EventArgs e)
        {            
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("chkSel");
                if (CheckSelectAll.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
                Page.Response.Redirect("ADZone.aspx?ZoneId=" + e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                if (B_ADZone.ADZone_Remove(Id))
                    Response.Write("<script>alert('删除成功！')</script>");
                RepNodeBind();
            }
            if (e.CommandName == "AddAdv")
                Page.Response.Redirect("Advertisement.aspx?ZoneId=" + e.CommandArgument.ToString());
            if (e.CommandName == "Copy")
            {
                string Id = e.CommandArgument.ToString();
                if (B_ADZone.ADZone_Copy(DataConverter.CLng(Id)))
                    Response.Write("<script>alert('复制成功！')</script>");
                RepNodeBind();
            }
            if (e.CommandName == "Clear")
            {
                string Id = e.CommandArgument.ToString();
                B_ADZone.ADZone_Clear(DataConverter.CLng(Id));
                Response.Write("<script>alert('清除成功！')</script>");
                RepNodeBind();
            }
            if (e.CommandName == "SetAct")
            {
                string Id = e.CommandArgument.ToString();
                if (!B_ADZone.getAdzoneByZoneId(DataConverter.CLng(Id)).Active)
                    B_ADZone.ADZone_Active(DataConverter.CLng(Id));
                else
                    B_ADZone.ADZone_Pause(Id);
                RepNodeBind();
            }
            if (e.CommandName == "Refresh")
            {
                B_ADZone.CreateJS(e.CommandArgument.ToString());
                Response.Write("<script>alert('刷新JS成功！')</script>");
                RepNodeBind();
            }
            if (e.CommandName == "PreView")
                Page.Response.Redirect("PreviewAD.aspx?ZoneID=" + e.CommandArgument.ToString() + "&Type=Zone");
            if(e.CommandName=="JS")
                Page.Response.Redirect("ShowJSCode.aspx?ZoneID=" + e.CommandArgument.ToString());
        }
        
        public static string getzonetypename(string  i)
        {
            int index = DataConverter.CLng(i);
            string zonetypename = "";
            switch (index)
            {
                case 0:
                    zonetypename = "矩形横幅";
                    break;
                case 1:
                    zonetypename = "弹出窗口";
                    break;
                case 2:
                    zonetypename = "随屏移动";
                    break;
                case 3:
                    zonetypename = "固定位置";
                    break;
                case 4:
                    zonetypename = "漂浮移动";
                    break;
                case 5:
                    zonetypename = "文字代码";
                    break;
                case 6:
                    zonetypename = "对联广告";
                    break;
            }
            return zonetypename;
        }
        public static string getzoneshowtypename(string  i)
        {
            int index = DataConverter.CLng(i);
            string zoneshowtypename = "";
            switch (index)
            {
                case 0:
                    zoneshowtypename = "权重随机显示";
                    break;
                case 1:
                    zoneshowtypename = "权重优先显示";
                    break;
                case 2:
                    zoneshowtypename = "顺序循环显示";
                    break;
            }
            return zoneshowtypename;

        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.RepNodeBind();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string Ids = "";
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    if (string.IsNullOrEmpty(Ids))
                        Ids = GridView1.DataKeys[i].Value.ToString();
                    else
                        Ids += "," + GridView1.DataKeys[i].Value.ToString();                    
                }                
            }
            if(!string.IsNullOrEmpty(Ids))
                B_ADZone.BatchRemove(Ids);
            this.RepNodeBind();
        }
        /// <summary>
        /// 批量激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnActive_Click(object sender, EventArgs e)
        {
            string Ids = "";
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    if (string.IsNullOrEmpty(Ids))
                        Ids = GridView1.DataKeys[i].Value.ToString();
                    else
                        Ids += "," + GridView1.DataKeys[i].Value.ToString();
                }
            }
            if (!string.IsNullOrEmpty(Ids))
                B_ADZone.BatchActive(Ids);
            this.RepNodeBind();
        }
        /// <summary>
        /// 批量暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnPause_Click(object sender, EventArgs e)
        {
            string Ids = "";
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    if (string.IsNullOrEmpty(Ids))
                        Ids = GridView1.DataKeys[i].Value.ToString();
                    else
                        Ids += "," + GridView1.DataKeys[i].Value.ToString();
                }
            }
            if (!string.IsNullOrEmpty(Ids))
                B_ADZone.BatchPause(Ids);
            this.RepNodeBind();
        }
        /// <summary>
        /// 批量刷新JS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnRefurbish_Click(object sender, EventArgs e)
        {            
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    B_ADZone.CreateJS(GridView1.DataKeys[i].Value.ToString());
                }
            }
            Response.Write("<script>alert('批量刷新JS成功！')</script>");
            this.RepNodeBind();
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BntSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TxtADName.Text.Trim()))
                this.ViewState["AdName"] = this.TxtADName.Text.Trim();
            this.RepNodeBind();
        }
}
}

