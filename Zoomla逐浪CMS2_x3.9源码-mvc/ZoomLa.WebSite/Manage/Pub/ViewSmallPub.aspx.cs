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
using ZoomLa.Components;
using ZoomLa.Model.Content;
using ZoomLa.BLL.Content;

namespace ZoomLaCMS.Manage.Pub
{
    public partial class ViewSmallPub : CustomerPageAction
    {
        private B_User buser = new B_User();
        private B_ModelField bfield = new B_ModelField();
        private B_Model bmodel = new B_Model();
        private int m_type;
        private B_Pub pub = new B_Pub();
        public String ids { get { return Request.QueryString["ids"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();

            if (!this.IsPostBack)
            {
                int pubid = DataConverter.CLng(Request.QueryString["Pubid"]);
                ViewState["pubid"] = pubid.ToString();
                M_Pub pubinfo = pub.GetSelect(pubid);
                string prowinfo = B_Role.GetPowerInfoByIDs(badmin.GetAdminLogin().RoleList);
                if (!badmin.GetAdminLogin().RoleList.Contains(",1,") && !prowinfo.Contains("," + pubinfo.PubTableName + ","))
                {
                    function.WriteErrMsg("无权限管理该互动模型!!");
                }
                string ModelID = (pubinfo.PubModelID == 0) ? "0" : pubinfo.PubModelID.ToString();
                if (DataConverter.CLng(ModelID) <= 0)
                {
                    function.WriteErrMsg("无模块信息");
                }
                int ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"]);
                this.HdnID.Value = ID.ToString();
                string type = (Request.QueryString["type"] == null) ? "0" : Request.QueryString["type"].ToString();
                M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(ModelID));
                this.HdnModelID.Value = ModelID.ToString();
                this.HiddenType.Value = type;
                this.HiddenPubid.Value = pubid.ToString();
                this.ViewState["ModelID"] = ModelID.ToString();
                this.ViewState["cType"] = "1";
                RepNodeBind();
                int nodeid = (Request.QueryString["nodeid"] == null) ? 0 : DataConverter.CLng(Request.QueryString["nodeid"]);
                this.HiddenNode.Value = "";
                if (nodeid == 0)
                {
                    Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='pubmanage.aspx'>互动管理</a></li><li><a href='AddPub.aspx?Parentid=" + this.HdnID.Value + "&Pubid=" + this.HiddenPubid.Value + "' style='color:red;'>[添加回复]</a></li>");
                }
                else
                {
                    B_Node bbn = new B_Node();
                    M_Node nn = bbn.GetNodeXML(nodeid);
                    Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='pubmanage.aspx'>互动管理</a></li><li><a href='Pubsinfo.aspx?nodeid=" + nodeid + "&Pubid=" + this.HiddenPubid.Value + "' style='color:red;'>" + nn.NodeName + "</a></li>");
                    this.HiddenNode.Value = "&nodeid=" + nodeid;

                }
                //this.Label1.Text = "<a href='AddPub.aspx?Parentid=" + this.HdnID.Value + "&Pubid=" + this.HiddenPubid.Value + "'>[&nbsp;&nbsp;&nbsp;添加回复&nbsp;&nbsp;]</a>";
            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (CheckBox2.Checked == true)
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
        /// GridView 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            RepNodeBind();
        }
        protected DataTable GetDT()
        {
            this.ViewState["cType"] = Request.QueryString["type"];
            this.m_type = DataConverter.CLng(this.ViewState["cType"].ToString());
            DataTable UserData;
            M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(this.HdnModelID.Value));
            try
            {
                switch (m_type)
                {
                    case 1:
                        UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 13);
                        break;
                    case 2:
                        UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 19);
                        break;
                    case 3:
                        UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 20);
                        break;
                    default:
                        UserData = buser.GetUserModeInfo(model.TableName, DataConverter.CLng(this.HdnID.Value), 13);
                        break;
                }
                return UserData;
            }
            catch (Exception)
            {
                function.WriteErrMsg("模型表[" + model.TableName + "]不存在!您可以点"); return null;
            }
        }
        private void RepNodeBind()
        {

            this.Egv.DataSource = GetDT();
            this.Egv.DataKeyNames = new string[] { "ID" };
            this.Egv.DataBind();

            #region 动态添加绑定列
            //  //  动态添加绑定列很简单：例如：
            //    BoundField bf1 = new BoundField();
            //    BoundField bf2 = new BoundField();
            //    BoundField bf3 = new BoundField();

            //    bf1.HeaderText = "ID";
            //    bf1.DataField = "ID";
            //    bf1.ReadOnly = true;
            //    bf1.SortExpression = "ID";
            //    CommandField cf = new CommandField();
            //    cf.ButtonType = ButtonType.Button;
            //    cf.ShowCancelButton = true;
            //    cf.ShowEditButton = true;
            //    Egv.Columns.Insert(2, bf1);
            ////    Egv.Columns[Egv.Columns.Count - 2].Add(bf1);

            // //   Egv.Columns.Add(cf);
            //        TemplateField tf = new TemplateField();
            //        tf.HeaderText = "自定义模板列";
            //        MyTemplate mt = new MyTemplate();
            //        mt.ProName = "ID";//数据源字段
            //        tf.ItemTemplate = mt;
            //        this.Egv.Columns.Add(tf);

            #endregion
        }

        public string shenhe(string shen)
        {
            if (shen == "1")
                return "<span style='color:red'>已审核</span>";
            else
                return "未审核";
        }

        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "View")
            {
                Page.Response.Redirect("PubView.aspx?ID=" + e.CommandArgument.ToString() + "&Pubid=" + this.HiddenPubid.Value.ToString() + "&small=" + this.HdnID.Value);
            }
            if (e.CommandName == "Audit")
            {
                string Id = e.CommandArgument.ToString();
                int modeid = DataConverter.CLng(this.ViewState["ModelID"].ToString());
            }
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                buser.DelModelInfo(bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString())).TableName, DataConverter.CLng(Id));
            }
            if (e.CommandName == "Edit")
                Page.Response.Redirect("EditPub.aspx?Pubid=" + this.HiddenPubid.Value.ToString() + "&ID=" + e.CommandArgument.ToString() + "&small=" + this.HdnID.Value);
            RepNodeBind();
        }

        protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";
                //e.Row.Attributes.Add("style", "cursor:pointer");
                //e.Row.Attributes.Add("title", "双击修改");
                //e.Row.Attributes.Add("onmouseover ", "this.style.backgroundColor= '#C4E4FF ' ");
                //e.Row.Attributes.Add("onmouseout ", "this.style.backgroundColor= '#E8F5FF' ");
                //e.Row.Attributes.Add("ondblclick", "location.href('EditPub.aspx?Pubid=" + this.HiddenPubid.Value.ToString() + "&ID="+Egv.DataKeys[e.Row.DataItemIndex].Value.ToString()+"&small="+this.HdnID.Value+"');");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ids = "";
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    if (string.IsNullOrEmpty(ids))
                        ids = itemID.ToString();
                    else
                        ids = ids + "," + itemID.ToString();
                    buser.DelModelInfo(bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString())).TableName, itemID);
                }
            }
            RepNodeBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string ids = "";
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    if (string.IsNullOrEmpty(ids))
                        ids = itemID.ToString();
                    else
                        ids = ids + "," + itemID.ToString();
                    buser.DelModelInfo2(bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString())).TableName, itemID, 12);
                }
            }
            RepNodeBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string ids = "";
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int itemID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    if (string.IsNullOrEmpty(ids))
                        ids = itemID.ToString();
                    else
                        ids = ids + "," + itemID.ToString();
                    buser.DelModelInfo2(bmodel.GetModelById(DataConverter.CLng(this.ViewState["ModelID"].ToString())).TableName, itemID, 13);
                }
            }
            RepNodeBind();
        }

        //Excel下载
        protected void DownExcel_Btn_Click(object sender, EventArgs e)
        {
            M_Pub_Excel excelMod = new M_Pub_Excel();
            B_Pub_Excel excelBll = new B_Pub_Excel();
            int pubid = Convert.ToInt32(Request.QueryString["PubID"]);
            M_Pub pubinfo = pub.GetSelect(pubid);
            excelMod = excelBll.SelByTbName(pubinfo.PubTableName);
            if (excelMod == null) { function.WriteErrMsg("尚未为表：" + pubinfo.PubTableName + "指定导出规则,请先<a href='PubExcel.aspx'>点此设定导出规则</a>"); }
            OfficeHelper ofHelper = new OfficeHelper();
            SafeSC.DownStr(ofHelper.GetExcelByDT(GetDT(), excelMod.Fields, excelMod.CNames), "互动回复.xls");
        }
    }
}