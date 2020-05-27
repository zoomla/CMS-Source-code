namespace ZoomLaCMS.Manage.FtpFile
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using ZoomLa.Model.FTP;
    using ZoomLa.BLL;
    using ZoomLa.BLL.FTP;
    using ZoomLa.Common;
    using System.Diagnostics;
    public partial class FtpAll : System.Web.UI.Page
    {
        private B_FTP bf = new B_FTP();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            int SId = 0;
            if (!IsPostBack)
            {
                MyBind();
                if (Request.QueryString["SId"] != null)
                {
                    SId = Convert.ToInt32(Request.QueryString["SId"]);
                    bf.DeleteFtpFile(SId);
                    function.WriteSuccessMsg("删除成功!", "FtpAll.aspx");
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='UploadFile.aspx'>文件管理</a></li><li><a href='FtpAll.aspx'>云端存储</a>  <a href='FtpConfig.aspx'>[添加云端服务器]</a></li>");
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < EGV.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)EGV.Rows[i].FindControl("SelectCheckBox");
                cbox.Checked = this.CheckBox1.Checked;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            this.MyBind();
        }

        public void MyBind()
        {
            EGV.DataSource = bf.SelectFtp_All();// GetAuthorPage(0, 0, 10);
            EGV.DataBind();
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            int i = 0, flag = 0; string sid = "";
            //int f = EGV.Rows.Count;//测试用OnClientClick="return confirm('确定要删除选中的服务器吗？')"
            for (i = 0; i < EGV.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)EGV.Rows[i].Cells[0].FindControl("SelectCheckBox");
                bool check = cbox.Checked;
                if (((CheckBox)EGV.Rows[i].Cells[0].FindControl("SelectCheckBox")).Checked)//check
                {
                    sid = EGV.DataKeys[EGV.Rows[i].RowIndex].Value.ToString();
                    int Sid = Convert.ToInt32(sid);
                    if (bf.DeleteFtpFile(Sid))
                        flag++;
                }
            }
            if (flag > 0)
            {
                Response.Write("<script language=javascript> alert('批量删除成功！');window.document.location.href='FtpAll.aspx';</script>");
            }
        }

        /// 鼠标移动变色
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.className='tdbgmouseover'");
                e.Row.Attributes.Add("onmouseout", "this.className='tdbg'");
                e.Row.Attributes.Add("class", "tdbg");
            }
        }

        /// <summary>
        /// 单击选择行，双击打开编辑页面
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in this.EGV.Rows)
            {
                if (row.RowState == DataControlRowState.Edit)
                { // 编辑状态
                  //row.Attributes.Remove("onclick");
                    row.Attributes.Remove("ondblclick");
                    row.Attributes.Remove("style");
                    row.Attributes["title"] = "编辑行";
                    continue;
                }
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //// 单击事件，为了响应双击事件，需要延迟单击响应，根据需要可能需要增加延迟
                    //// 获取ASP.NET内置回发脚本函数，返回 __doPostBack(<<EventTarget>>, <<EventArgument>>)
                    //// 可直接硬编码写入脚本，不推荐                
                    //row.Attributes["onclick"] = String.Format("javascript:setTimeout(\"if(dbl_click){{dbl_click=false;}}else{{{0}}};\", 1000*0.3);", ClientScript.GetPostBackEventReference(GridView1, "Select$" + row.RowIndex.ToString(), true));
                    // 双击，设置 dbl_click=true，以取消单击响应
                    row.Attributes["ondblclick"] = String.Format("javascript:location.href='ShowFtpFile.aspx?FId={0}'", this.EGV.DataKeys[row.RowIndex].Value.ToString());
                    row.Attributes["style"] = "cursor:pointer";
                    row.Attributes["title"] = "双击查看文件信息";
                }
            }
            base.Render(writer);
        }
    }
}