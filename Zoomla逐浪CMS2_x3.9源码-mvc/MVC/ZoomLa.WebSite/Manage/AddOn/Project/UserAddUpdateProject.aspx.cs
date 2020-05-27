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

namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class UserAddUpdateProject : System.Web.UI.Page
    {
        B_User ull = new B_User();
        protected B_Sensitivity sll = new B_Sensitivity();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li>修改项目</li>");
            ull.CheckIsLogin();
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
            if (RBLAudit.SelectedValue == "1")
            {
                TxtBeginTime.Enabled = false;
                TxtLeader.Enabled = false;
                RBListStatus.Enabled = false;
                DDListProcess.Enabled = false;
                TxtCompleteTime.Enabled = false;
                TxtEvaluation.Enabled = false;
                TxtRating.Enabled = false;
            }
            else
            {
                TxtBeginTime.Enabled = true;
                TxtLeader.Enabled = true;
                RBListStatus.Enabled = true;
                DDListProcess.Enabled = true;
                TxtCompleteTime.Enabled = true;
                TxtEvaluation.Enabled = true;
                TxtRating.Enabled = true;
                if (RBListStatus.SelectedValue == "0")
                {
                    TxtBeginTime.Enabled = false;
                    TxtLeader.Enabled = false;
                    DDListProcess.Enabled = false;
                    TxtCompleteTime.Enabled = false;
                    TxtEvaluation.Enabled = false;
                    TxtRating.Enabled = false;
                }
                else
                {
                    if (RBListStatus.SelectedValue == "1")
                    {
                        TxtCompleteTime.Enabled = false;
                        TxtEvaluation.Enabled = false;
                        TxtRating.Enabled = false;
                    }
                    if (RBListStatus.SelectedValue == "3")
                    {
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
                        TxtRequire.Enabled = false;
                        TxtPrice.Enabled = false;
                        TxtApplicationTime.Enabled = false;
                        RBLAudit.Enabled = false;
                    }
                }
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
            //if (dt != null)
            //{
            //    dt.Dispose();
            //}
        }
        //读取项目信息
        protected void BindProject()
        {
            int id = DataConverter.CLng(Request.QueryString["ID"]);
            //mpj = bpj.GetSelect(id);
            //this.TxtProName.Text = mpj.Name;
            //TxtOrderID.Text = mpj.OrderID.ToString();
            //TxtUserID.Text = mpj.UserID.ToString();

            //读取XML用户信息
            XmlDocument xdm = new XmlDocument();
            // xdm.LoadXml(mpj.UserInfo);
            XmlNodeList nodelist = xdm.SelectSingleNode("Info").ChildNodes;
            TxtName.Text = nodelist[0].InnerText;
            TxtCom.Text = nodelist[1].InnerText;
            TxtTel.Text = nodelist[2].InnerText;
            TxtMobile.Text = nodelist[3].InnerText;
            TxtQQ.Text = nodelist[4].InnerText;
            TxtMSN.Text = nodelist[5].InnerText;
            TxtAddress.Text = nodelist[6].InnerText;
            TxtEmail.Text = nodelist[7].InnerText;

            //DDListType.SelectedValue = mpj.TypeID.ToString();
            //TxtRequire.Text = mpj.Requirements;
            //TxtPrice.Text = mpj.Price.ToString();
            //TxtBeginTime.Text = mpj.BeginTime.ToString();
            //TxtLeader.Text = mpj.Leader;
            //RBLAudit.SelectedValue = mpj.AuditStatus.ToString();
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
        //修改项目信息
        protected void BtnCommit_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(Request.QueryString["ID"]);
            //mpj = bpj.GetSelect(id);
            //mpj.Name=this.TxtProName.Text.Trim();
            //mpj.OrderID = DataConverter.CLng(TxtOrderID.Text.Trim());
            //mpj.UserID = DataConverter.CLng(TxtUserID.Text.Trim());

            string str = "<Info>";
            str += "<item id=\"name\">" + this.TxtName.Text.Trim() + "</item>";
            str += "<item id=\"company\">" + this.TxtCom.Text.Trim() + "</item>";
            str += "<item id=\"tel\">" + this.TxtTel.Text.Trim() + "</item>";
            str += "<item id=\"mobile\">" + this.TxtMobile.Text.Trim() + "</item>";
            str += "<item id=\"qq\">" + this.TxtQQ.Text.Trim() + "</item>";
            str += "<item id=\"msn\">" + this.TxtMSN.Text.Trim() + "</item>";
            str += "<item id=\"address\">" + this.TxtAddress.Text.Trim() + "</item>";
            str += "<item id=\"email\">" + this.TxtEmail.Text.Trim() + "</item>";
            str += "</Info>";

            //mpj.UserInfo = str;
            //mpj.TypeID = DataConverter.CLng(DDListType.SelectedValue);
            //mpj.Requirements=TxtRequire.Text.Trim();
            //mpj.Price=DataConverter.CLng( TxtPrice.Text.Trim());
            //mpj.BeginTime = DataConverter.CDate(TxtBeginTime.Text.Trim());
            //mpj.Leader=TxtLeader.Text.Trim();
            //mpj.AuditStatus=DataConverter.CLng( RBLAudit.SelectedValue);
            //mpj.ProStatus=DataConverter.CLng(RBListStatus.SelectedValue);
            //mpj.Progress = DataConverter.CLng(Request.Form["DDListProcess"]);
            //mpj.CompletionTime = DataConverter.CDate(TxtCompleteTime.Text);
            //mpj.Rating=DataConverter.CLng( TxtRating.Text.Trim());
            //mpj.Evaluation = TxtEvaluation.Text.Trim();
            //mpj.ApplicationTime=DataConverter.CDate( TxtApplicationTime.Text.Trim());
            //bpj.GetUpdate(mpj);

            //DataTable dt = bpf.Select_All();
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (DataConverter.CBool(dr["IsNotNull"].ToString()))
            //    {
            //        if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
            //        {
            //            function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
            //        }
            //    }

            //    if (dr["FieldType"].ToString() == "FileType")
            //    {
            //        string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
            //        bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
            //        string sizefield = Sett[1].Split(new char[] { '=' })[1];
            //        if (chksize && sizefield != "")
            //        {
            //            DataRow row2 = table.NewRow();
            //            row2[0] = sizefield;
            //            row2[1] = "FileSize";
            //            row2[2] = this.Page.Request.Form["txt_" + sizefield];
            //            table.Rows.Add(row2);
            //        }
            //    }

            //    if (dr["FieldType"].ToString() == "MultiPicType")
            //    {
            //        string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
            //        bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
            //        string sizefield = Sett[1].Split(new char[] { '=' })[1];
            //        if (chksize && sizefield != "")
            //        {
            //            if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + sizefield]))
            //            {
            //                function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
            //            }
            //            DataRow row1 = table.NewRow();
            //            row1[0] = sizefield;
            //            row1[1] = "ThumbField";
            //            row1[2] = this.Page.Request.Form["txt_" + sizefield];
            //            table.Rows.Add(row1);
            //        }
            //    }
            //    DataRow row = table.NewRow();
            //    row[0] = dr["FieldName"].ToString();
            //    string ftype = dr["FieldType"].ToString();
            //    row[1] = ftype;
            //    string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
            //    if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
            //    {
            //        fvalue = sll.ProcessSen(fvalue);
            //    }
            //    row[2] = fvalue;
            //    table.Rows.Add(row);
            //}
            //bpj.UpdateProjectsFile(DataConverter.CLng( GetID()), table);
            //function.WriteSuccessMsg("修改成功！", "AddUpdateProject.aspx?ID=" + GetID());
            //if (dt != null)
            //{
            //    dt.Dispose();
            //}
        }
    }
}