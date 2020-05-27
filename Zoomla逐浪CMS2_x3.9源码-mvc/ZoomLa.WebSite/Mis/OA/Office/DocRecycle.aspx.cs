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


/*
 * 文档等发文回收站
 */

namespace ZoomLaCMS.MIS.OA.Office
{
    public partial class DocRecycle : System.Web.UI.Page
    {

        protected B_OA_Document oaBll = new B_OA_Document();
        protected B_User buser = new B_User();
        protected B_Content bll = new B_Content();
        //用户组权限
        protected B_UserPromotions promBll = new B_UserPromotions();
        protected M_UserPromotions promMod = new M_UserPromotions();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            if (!IsPostBack)
            {
                DataBind();
            }
        }
        private void DataBind(string key = "")
        {
            DataTable dt = new DataTable();
            //string nodeIDS = bll.GetNodeIDS(OAConfig.ModelID);
            //dt = bll.GetContentByNodeS(nodeIDS, buser.GetLogin().UserName,-2);
            //if (!string.IsNullOrEmpty(key.Trim()))
            //{
            //    dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
            //    dt = dt.DefaultView.ToTable();
            //}
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        //处理页码
        public void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = EGV.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = EGV.PageSize;
            }
            EGV.PageSize = pageSize;
            EGV.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "read":
                    Response.Redirect("../ViewContent.aspx?Gid=" + e.CommandArgument.ToString());
                    break;
                case "recover":
                    bll.UpdateStatus(Convert.ToInt32(e.CommandArgument), 99);
                    break;
                case "del2":
                    bll.DelContent(Convert.ToInt32(e.CommandArgument));
                    break;
            }
            DataBind();
        }
        public string GetStatus(string status)
        {
            return ZLEnum.GetConStatus(DataConverter.CLng(status));
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["title"] = "双击查看文章";
                e.Row.Attributes["style"] = "cursor:pointer";
                e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='ViewContent.aspx?Gid={0}'", this.EGV.DataKeys[e.Row.RowIndex].Value.ToString());
            }
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            DataBind(searchText.Text);
        }

        //---------Tool
        public string GetNodeName(object nid)
        {
            return OACommon.GetNodeID(nid.ToString(), 1);
        }
        protected void batDelBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idChk"]))
            {

            }
            else
            {
                string[] chkArr = Request.Form["idChk"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < chkArr.Length; i++)
                    this.bll.SetDel(Convert.ToInt32(chkArr[i]));
                DataBind();
            }
        }
    }
}