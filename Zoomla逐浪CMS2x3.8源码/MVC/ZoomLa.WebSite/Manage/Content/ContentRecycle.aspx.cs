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
using ZoomLa.SQLDAL;
using System.Collections.Generic;


namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class ContentRecycle : CustomerPageAction
    {
        protected B_Admin adminBll = new B_Admin();
        private B_Content contentBll = new B_Content();
        private B_Model modelMod = new B_Model();
        protected B_Node nodeBll = new B_Node();
        protected B_ModelField fieldBll = new B_ModelField();
        private int NodeID { get { return DataConvert.CLng(ViewState["NodeID"]); } set { ViewState["NodeID"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentRecycle");
                if (!string.IsNullOrEmpty(Request.QueryString["NodeID"])) { NodeID = DataConvert.CLng(Request.QueryString["NodeID"]); }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>回收站</a></li>" + Call.GetHelp(93));
                MyBind();
            }
        }
        protected void MyBind(string key = "")
        {
            DataTable dt = contentBll.GetContentRecycle(NodeID);
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        public string GetNodeName(string nodeid)
        {
            return this.nodeBll.GetNodeXML(DataConverter.CLng(nodeid)).NodeName;
        }
        public string GetModelName(string modelid)
        {
            return this.modelMod.GetModelById(DataConverter.CLng(modelid)).ModelName;
        }
        // 还原
        protected void btnRevert_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                contentBll.Reset(ids);
            }
            MyBind();
        }
        // 清除所选内容
        protected void btnClear_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                foreach (string id in ids.Split(','))
                {
                    contentBll.DelContent(DataConvert.CLng(id));
                }
            }
            MyBind();
        }
        // 清空回收站
        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            contentBll.DelRecycle();
            MyBind();
        }
        // 还原所有内容
        protected void btnRevertAll_Click(object sender, EventArgs e)
        {
            contentBll.ResetAll();
            MyBind();
        }
        public string GetContent(string gid)
        {
            string result = "";
            M_CommonData comMod = contentBll.GetCommonData(Convert.ToInt32(gid));
            DataTable dt = this.fieldBll.GetModelFieldListAll(comMod.ModelID);
            DataTable contentDT = contentBll.GetContentByItems(comMod.TableName, comMod.GeneralID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["FieldName"].ToString() == "content")
                {
                    result = contentDT.Rows[0][dt.Rows[i]["FieldName"].ToString()].ToString();
                }
            }
            result = StringHelper.StripHtml(result);
            if (result.Length > 200)
                result = result.Substring(0, 200) + "......";
            return result;
        }
        protected void DBlclickDel_Click(object sender, EventArgs e)
        {
            string title = "";
            int id = 0;

            id = Convert.ToInt32(GeneralID_Hid.Value);
            title = contentBll.GetCommonData(id).Title;
            contentBll.DelContent(id);
            MyBind();
        }
        /// <summary>
        /// 显示标题
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            int nodeid = DataConvert.CLng(Eval("NodeID"));
            string title = StringHelper.SubStr(Eval("Title").ToString());
            if (nodeid == NodeID) { return title; }
            else { return "<strong>[<a href=\"ContentRecycle.aspx?NodeID=" + nodeid + "\">" + Eval("NodeName") + "</a>]</strong>" + title; }
        }
    }
}