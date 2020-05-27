using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Xml;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class AddUpdateProject : System.Web.UI.Page
    {
        protected B_Sensitivity sll = new B_Sensitivity();
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>CRM应用</li><li><a href='Projects.aspx'>项目管理</a></li>");
            badmin.CheckIsLogin();
            //DataTable dt = bpj.GetBaseByID(DataConverter.CLng(GetID()));
            if (!IsPostBack)
            {
                BindType();
                BindProcess();
                ListItem lt = new ListItem();
                lt.Text = "--请选择--";
                lt.Value = "0";
                DDListProcess.Items.Insert(0, lt);
                BindProject();
                //lblHtml.Text = bpf.GetUpdateHtml(dt);
            }

            if (IsSupperAdmin())
            {
                TxtLeader.Enabled = true;
                WebCoding.Enabled = true;
                TxtBeginTime.Enabled = true;
                TxtLeader.Enabled = true;
                RBListStatus.Enabled = true;
                DDListProcess.Enabled = true;
                TxtCompleteTime.Enabled = true;
                TxtEvaluation.Enabled = true;
                TxtRating.Enabled = true;

                TxtBeginTime.Enabled = true;
                DDListProcess.Enabled = true;
                TxtCompleteTime.Enabled = true;
                TxtEvaluation.Enabled = true;
                TxtRating.Enabled = true;
                TxtCompleteTime.Enabled = true;
                TxtEvaluation.Enabled = true;
                TxtRating.Enabled = true;
                TxtBeginTime.Enabled = true;
                TxtLeader.Enabled = true;
                DDListProcess.Enabled = true;
                TxtCompleteTime.Enabled = true;
                TxtEvaluation.Enabled = true;
                TxtRating.Enabled = true;
                TxtProName.Enabled = true;
                DDListType.Enabled = true;
                TxtOrderID.Enabled = true;
                TxtUserID.Enabled = true;
                //TxtRequire.EnableTheming = true;
                TxtPrice.Enabled = true;
                TxtApplicationTime.Enabled = true;
                RBLAudit.Enabled = true;
                this.BtnCommit.Enabled = true;

            }
            else
            {
                TxtLeader.Enabled = false;
                WebCoding.Enabled = false;
                TxtBeginTime.Enabled = false;
                RBListStatus.Enabled = false;
                DDListProcess.Enabled = false;
                TxtCompleteTime.Enabled = false;
                TxtEvaluation.Enabled = false;
                TxtRating.Enabled = false;
                TxtBeginTime.Enabled = false;
                DDListProcess.Enabled = false;
                TxtCompleteTime.Enabled = false;
                TxtEvaluation.Enabled = false;
                TxtRating.Enabled = false;
                TxtCompleteTime.Enabled = false;
                TxtEvaluation.Enabled = false;
                TxtRating.Enabled = false;
                TxtBeginTime.Enabled = false;
                TxtLeader.Enabled = false;
                DDListProcess.Enabled = false;
                TxtCompleteTime.Enabled = false;
                TxtEvaluation.Enabled = false;
                TxtRating.Enabled = false;
                TxtProName.Enabled = false;
                DDListType.Enabled = false;
                TxtOrderID.Enabled = false;
                TxtUserID.Enabled = false;
                TxtRequire.Attributes.Add("readonly", "readonly");
                TxtPrice.Enabled = false;
                TxtApplicationTime.Enabled = false;
                RBLAudit.Enabled = false;
                this.BtnCommit.Enabled = false;
            }
        }
        protected bool IsSupperAdmin()
        {
            if (badmin.GetAdminLogin().IsSuperAdmin(badmin.GetAdminLogin().RoleList))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected string GetID()
        {
            return Request.QueryString["ID"];
        }
        //项目类型绑定
        protected void BindType()
        {
            //DataTable dt = bpt.Select_All();
            //DDListType.DataSource = dt;
            //DDListType.DataTextField = "ProjectTypeName";
            //DDListType.DataValueField = "ProjectTypeID";
            //DDListType.DataBind();
        }
        //读取项目信息
        protected void BindProject()
        {
            int id = DataConverter.CLng(Request.QueryString["ID"]);
            //mpj = bpj.GetSelect(id);
            //this.TxtProName.Text = mpj.Name;
            //TxtOrderID.Text = mpj.OrderID.ToString();
            //TxtUserID.Text = mpj.UserID.ToString();
            //WebCoding.Text = mpj.WebCoding.ToString();

            //读取XML用户信息
            XmlDocument xdm = new XmlDocument();
            //xdm.LoadXml(mpj.UserInfo);
            XmlNodeList nodelist = xdm.SelectSingleNode("Info").ChildNodes;
            lblName.Text = nodelist[0].InnerText;
            ViewState["txtCom"] = nodelist[1].InnerText;
            ViewState["txtTel"] = nodelist[2].InnerText;
            ViewState["txtMobile"] = nodelist[3].InnerText;
            ViewState["TxtQQ"] = nodelist[4].InnerText;
            ViewState["TxtMSN"] = nodelist[5].InnerText;
            ViewState["TxtAddress"] = nodelist[6].InnerText;
            ViewState["TxtEmail"] = nodelist[7].InnerText;

            //DDListType.SelectedValue = mpj.TypeID.ToString();
            //TxtRequire.Value = mpj.Requirements;

            if (GetManageGroup() == 1)
            {
                //TxtPrice.Text = mpj.Price.ToString();
            }
            else
            {
                TxtPrice.Text = "0";
            }


            //TxtBeginTime.Text = mpj.BeginTime.ToString();
            //TxtLeader.Text = mpj.Leader;
            //RBLAudit.SelectedValue = mpj.AuditStatus.ToString();
            ////Response.Write(mpj.AuditStatus.ToString());

            //RBListStatus.SelectedValue = mpj.ProStatus.ToString();
            //DDListProcess.SelectedValue = mpj.Progress.ToString();
            //TxtCompleteTime.Text = mpj.CompletionTime.ToString();
            //TxtRating.Text = mpj.Rating.ToString();
            //TxtEvaluation.Text = mpj.Evaluation;
            //TxtApplicationTime.Text = mpj.ApplicationTime.ToString();
        }
        //绑定进度
        protected void BindProcess()
        {
            //DDListProcess.DataSource = bpss.SelectByProID(DataConverter.CLng(GetID()));
            //DDListProcess.DataTextField = "Name";
            //DDListProcess.DataValueField = "ID";
            //DDListProcess.DataBind();
        }
        //返回项目列表页
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }
        /// <summary>
        /// 获得管理员等级
        /// </summary>
        /// <returns></returns>
        protected int GetManageGroup()
        {
            B_Admin badmin = new B_Admin();
            if (badmin.GetAdminLogin().IsSuperAdmin(badmin.GetAdminLogin().RoleList))
            {
                return 1;
            }
            else
            {
                //if (mpj.Leader == badmin.GetAdminLogin().AdminName)
                //{
                //    return 1;
                //}
                //else
                //{
                //    return 0;
                //}
                return 0;
            }
        }
        //修改项目信息
        protected void BtnCommit_Click(object sender, EventArgs e)
        {

            int id = DataConverter.CLng(Request.QueryString["ID"]);
            //mpj = bpj.GetSelect(id);
            //mpj.Name=this.TxtProName.Text.Trim();
            //mpj.OrderID = DataConverter.CLng(TxtOrderID.Text.Trim());
            //mpj.UserID = DataConverter.CLng(TxtUserID.Text.Trim());

            string str = "<Info>";
            str += "<item id=\"name\">" + this.lblName.Text.Trim() + "</item>";
            str += "<item id=\"company\">" + ViewState["txtCom"].ToString() + "</item>";
            str += "<item id=\"tel\">" + ViewState["txtTel"].ToString() + "</item>";
            str += "<item id=\"mobile\">" + ViewState["txtMobile"].ToString() + "</item>";
            str += "<item id=\"qq\">" + ViewState["TxtQQ"].ToString() + "</item>";
            str += "<item id=\"msn\">" + ViewState["TxtMSN"].ToString() + "</item>";
            str += "<item id=\"address\">" + ViewState["TxtAddress"].ToString() + "</item>";
            str += "<item id=\"email\">" + ViewState["TxtEmail"].ToString() + "</item>";
            str += "</Info>";

            //mpj.UserInfo = str;
            //mpj.TypeID = DataConverter.CLng(DDListType.SelectedValue);
            //mpj.Requirements=TxtRequire.Value.Trim();

            //if (GetManageGroup() == 1)
            //{
            //    mpj.Price = DataConverter.CLng(TxtPrice.Text.Trim());
            //}

            //mpj.BeginTime = DataConverter.CDate(TxtBeginTime.Text.Trim());
            //mpj.Leader=TxtLeader.Text.Trim();
            //mpj.WebCoding = WebCoding.Text;
            //mpj.AuditStatus=DataConverter.CLng( RBLAudit.SelectedValue);
            //mpj.ProStatus=DataConverter.CLng(RBListStatus.SelectedValue);
            //mpj.Progress = DataConverter.CLng(Request.Form["DDListProcess"]);
            //mpj.CompletionTime = DataConverter.CDate(TxtCompleteTime.Text);
            //mpj.Rating=DataConverter.CLng( TxtRating.Text.Trim());
            //mpj.Evaluation = TxtEvaluation.Text.Trim();
            //mpj.ApplicationTime = DataConverter.CDate(TxtApplicationTime.Text.Trim());

            //if (GetManageGroup(TxtLeader.Text.Trim()) == 1)
            //{
            //    bpj.GetUpdate(mpj);
            //}


            // DataTable dt = bpf.Select_All();
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        if (DataConverter.CBool(dr["IsNotNull"].ToString()))
            //        {
            //            if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
            //            {
            //                function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
            //            }
            //        }

            //        if (dr["FieldType"].ToString() == "FileType")
            //        {
            //            string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
            //            bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
            //            string sizefield = Sett[1].Split(new char[] { '=' })[1];
            //            if (chksize && sizefield != "")
            //            {
            //                DataRow row2 = table.NewRow();
            //                row2[0] = sizefield;
            //                row2[1] = "FileSize";
            //                row2[2] = this.Page.Request.Form["txt_" + sizefield];
            //                table.Rows.Add(row2);
            //            }
            //        }

            //        if (dr["FieldType"].ToString() == "MultiPicType")
            //        {
            //            string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
            //            bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
            //            string sizefield = Sett[1].Split(new char[] { '=' })[1];
            //            if (chksize && sizefield != "")
            //            {
            //                if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + sizefield]))
            //                {
            //                    function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
            //                }
            //                DataRow row1 = table.NewRow();
            //                row1[0] = sizefield;
            //                row1[1] = "ThumbField";
            //                row1[2] = this.Page.Request.Form["txt_" + sizefield];
            //                table.Rows.Add(row1);
            //            }
            //        }
            //        DataRow row = table.NewRow();
            //        row[0] = dr["FieldName"].ToString();
            //        string ftype = dr["FieldType"].ToString();
            //        row[1] = ftype;
            //        string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
            //        if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
            //        {
            //            fvalue = sll.ProcessSen(fvalue);
            //        }
            //        row[2] = fvalue;
            //        table.Rows.Add(row);
            //    }
            //}
            if (GetManageGroup(TxtLeader.Text.Trim()) == 1)
            {
                //bpj.UpdateProjectsFile(DataConverter.CLng(GetID()), table);
                function.WriteSuccessMsg("修改成功！", "AddUpdateProject.aspx?ID=" + GetID());
            }
            else
            {
                function.Script(this, "alert('对不起！您的权限不够！');history.back();");
            }
        }
        /// <summary>
        /// 获得管理员等级
        /// </summary>
        /// <returns></returns>
        protected int GetManageGroup(string Leader)
        {
            if (badmin.GetAdminLogin().IsSuperAdmin(badmin.GetAdminLogin().RoleList))
            {
                return 1;
            }
            else
            {
                if (Leader == badmin.GetAdminLogin().AdminName)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}