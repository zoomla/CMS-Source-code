using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class AuditManage : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li>内容预审核管理</li>");
            badmin.CheckIsLogin();
            if (!IsPostBack)
            {
                Bind();
            }
        }

        //批量删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            int Ids = 0;
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    Ids = DataConverter.CLng((Egv.Rows[i].FindControl("hfIds") as HiddenField).Value);
                    DeleteXmlNode(Ids);
                }
            }
            Bind();
        }

        ////全选
        //protected void cbAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    for (int i = 0; i <= Egv.Rows.Count - 1; i++)
        //    {
        //        CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
        //        if (cbAll.Checked == true)
        //        {
        //            cbox.Checked = true;
        //        }
        //        else
        //        {
        //            cbox.Checked = false;
        //        }
        //    }
        //}

        //分页文本
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            ViewState["page"] = "1";
            Bind();
        }

        //下拉
        protected void DropDownList3_TextChanged(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// 删除xml节点
        /// </summary>
        private void DeleteXmlNode(int id)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string path = "../../Config/AuditData.xml";
            xmlDoc.Load(Server.MapPath(path));
            XmlNodeList xnl = xmlDoc.SelectSingleNode("content").ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                if (xnl[i].Attributes["id"].Value == id.ToString())
                {
                    xnl[i].ParentNode.RemoveChild(xnl[i]);
                }
            }

            xmlDoc.Save(Server.MapPath(path));

        }

        /// <summary>
        /// 读取xml内容
        /// </summary>
        /// <returns></returns>
        private List<M_Audit> ReadXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../../Config/AuditData.xml"));
            XmlNodeList list = doc.SelectNodes("content/cont");
            List<M_Audit> audits = new List<M_Audit>();
            foreach (XmlNode node in list)
            {
                M_Audit audit = new M_Audit();
                audit.Id = DataConverter.CLng(node.Attributes["id"].Value);
                audit.NodeId = DataConverter.CLng(node.Attributes["nodeId"].Value);
                audit.BeginTime = DataConverter.CDate(node.Attributes["startTime"].Value);
                audit.EndTime = DataConverter.CDate(node.Attributes["endTime"].Value);
                audits.Add(audit);
            }
            if (audits != null && audits.Count > 0)
            {
                return audits;
            }
            else
            {
                return new List<M_Audit>();
            }
        }

        private void Bind()
        {
            List<M_Audit> audits = ReadXml();
            if (audits != null && audits.Count > 0)
            {
                if (audits != null && audits.Count > 0)
                {
                    this.nocontent.Style["display"] = "none";
                    this.Egv.Visible = true;
                }
                else
                {
                    this.nocontent.Style["display"] = "";
                    this.Egv.Visible = false;
                }
                PagedDataSource dd = new PagedDataSource();
                dd.DataSource = audits;
                this.Egv.DataSource = dd;
                this.Egv.DataBind();
            }
        }

        //行绑定
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfId = e.Row.FindControl("hfId") as HiddenField;
                int id = DataConverter.CLng(hfId.Value);
                Label lblContent = e.Row.FindControl("lblContent") as Label;
                B_Node node = new B_Node();
                M_Node no = node.GetNodeXML(id);
                if (no != null && no.NodeID > 0)
                {
                    lblContent.Text = no.NodeName;
                }
            }
        }

        //行命令
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Upd")  //修改
            {
                Response.Redirect("AddAudit.aspx?menu=update&id=" + e.CommandArgument);
            }
            if (e.CommandName == "Del")  //删除
            {
                int id = DataConverter.CLng(e.CommandArgument);
                DeleteXmlNode(id);
                Bind();
            }
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            this.Bind();
        }
    }
}