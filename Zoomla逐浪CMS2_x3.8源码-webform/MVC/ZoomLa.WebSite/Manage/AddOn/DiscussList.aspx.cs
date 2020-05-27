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
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class DiscussList : System.Web.UI.Page
    {
        private B_Admin badmin = new B_Admin();
        private int m_uid = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href='WorkManage.aspx?Pid=1'>项目节点</a></li><li>节点讨论</li>");
            if (!Page.IsPostBack)
            {
                m_uid = badmin.GetAdminLogin().AdminId;
                CheckRight(this.m_uid);//检查权限
                if (B_Admin.GetAdminByAdminId(this.m_uid) != null)
                {
                    TxtUserName.Text = B_Admin.GetAdminByAdminId(this.m_uid).AdminName;
                }
                else
                {
                    TxtUserName.Text = "佚名";
                }
                MyBind();
            }
        }
        public void CheckRight(int uid)
        {
            //int workid=0;
            //bool flag=false;
            //M_AdminInfo admininfo = B_Admin.GetAdminByAdminId(uid);
            //string testrole = admininfo.RoleList;   
            //if (admininfo.IsSuperAdmin(admininfo.RoleList)  )
            //{
            //    flag = true;
            //} 
            //if (Request.QueryString["Wid"] != null)
            //{
            //    workid=DataConverter.CLng(Request.QueryString["Wid"].Trim());
            //}      
            //string roles = admininfo.RoleList;
            //string[] role = roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //ArrayList alistrole = bworkrole.GetWorkRole(workid);
            //for (int i = 0; i < alistrole.Count; i++)
            //{
            //    int p = DataConverter.CLng(alistrole[i]);
            //    for (int j = 0; j < role.Length; j++)
            //    {
            //        if (p == DataConverter.CLng(role[j].Trim()))
            //        {
            //            flag = true;
            //        }
            //    }
            //}
            //if (!flag)
            //{
            //    function.WriteErrMsg("没有权限进行此项操作");          
            //}
        }
        /// <summary>
        /// 管理所有的内容讨论
        /// </summary>
        public void Bind()
        {
            //DataView dv = this.bpro.GetDiscussAll().DefaultView;
            //Egv.DataSource = dv;
            //Egv.DataBind();
        }
        /// <summary>
        /// 管理指定的内容表讨论
        /// </summary>
        /// <param name="wid"></param>
        public void BindWorkDis(int wid)
        {
            //DataView dv = this.bpro.GetDiscussByWid(wid).DefaultView;
            //Egv.DataSource = dv;
            //Egv.DataBind();
            //pid=DataConverter.CLng(this.bwork.SelectWorkByWID(wid).ProjectID);
            //HLpro.NavigateUrl = "WorkManage.aspx?Pid=" + pid;
        }
        /// <summary>
        /// 两种情况的绑定
        /// </summary>
        private void MyBind()
        {
            if (Request.QueryString["Wid"] != null)
                BindWorkDis(DataConverter.CLng(Request.QueryString["Wid"].Trim()));
            else
                Bind();
            if (Egv.Rows.Count == 0)
            {
                cbAll.Visible = false;
                btnDel.Visible = false;
            }
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            if (Request.QueryString["Wid"] != null)
                BindWorkDis(DataConverter.CLng(Request.QueryString["Wid"].Trim()));
            else
                Bind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DeleteDiscuss":
                    string Id = e.CommandArgument.ToString();
                    //this.bpro.DelProjectDiscuss(DataConverter.CLng(Id));
                    BindWorkDis(DataConverter.CLng(Request.QueryString["Wid"].Trim()));
                    break;
            }
            MyBind();
        }
        protected void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbAll.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    btnDel.Attributes.Add("OnClientClick", "return confirm('你确定要删除吗？');");
                    //this.bpro.DelProjectDiscuss(DataConverter.CLng(Egv.DataKeys[i].Value));
                }
            }
            MyBind();
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (TxtDisContent.Text.Trim() != string.Empty)
            {
                //mpro.ProjectID = DataConverter.CLng(this.bwork.SelectWorkByWID(DataConverter.CLng(Request.QueryString["Wid"].Trim())).ProjectID);
                //mpro.WorkID = DataConverter.CLng(Request.QueryString["wid"].Trim());
                //mpro.UserID = this.m_uid;
                //mpro.Content = TxtDisContent.Text.ToString();
                //mpro.DiscussDate = DateTime.Now;
                //if (bpro.AddProjectDiscuss(mpro))
                //{
                //    Response.Write("<script language=javascript> alert('讨论发布成功！');window.document.location.href='DiscussList.aspx?Wid=" + Request.QueryString["Wid"].Trim() + "';</script>");
                //}
            }
            else
            {
                Response.Write("<script language=javascript> alert('讨论内容不能为空！');window.document.location.href='DiscussList.aspx?Wid=" + Request.QueryString["Wid"].Trim() + "';</script>");
            }

        }
    }
}