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
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace ZoomLaCMS.Manage.Template
{
    public partial class LabelManage : CustomerPageAction
    {
        B_Label bll = new B_Label();
        B_Admin badmin = new B_Admin();
        public string LabelCate { get { string _cate = Request.QueryString["Cate"] ?? ""; return HttpUtility.UrlDecode(_cate); } }
        public string KeyWord { get { return TxtLableName.Text; } set { TxtLableName.Text = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.label, "LabelManage"))
            {
                function.WriteErrMsg(Resources.L.没有权限进行此项操作);
            }
            if (!IsPostBack)
            {
                if (bll.SelAllLabel().Rows.Count <= 1 && Directory.Exists(Server.MapPath(SiteConfig.SiteOption.TemplateDir + "/配置库/标签/")))
                {
                    Response.Redirect("LabelExport.aspx"); return;
                }
                Call.HideBread(Master);
                DataTable CateTable = bll.GetLabelCateListXML();//标签类别
                CateTable.Columns.Add("label");
                DataRow allrow = CateTable.NewRow();
                allrow["name"] = "";
                allrow["label"] = Resources.L.全部标签;
                CateTable.Rows.InsertAt(allrow, 0);
                LabelTypeData_Hid.Value = JsonConvert.SerializeObject(CateTable);
                //-----------------
                KeyWord = Request.QueryString["KeyWord"] ?? "";
                DataTable dt = bll.SelAllLabel(LabelCate, KeyWord);
                RPT.DataSource = dt;
                RPT.DataBind();
            }
        }
        protected void repFileReName_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string LabelName = e.CommandArgument.ToString();
                bll.DelLabelXML(LabelName);
                Response.Redirect("LabelManage.aspx");
            }
            if (e.CommandName == "Copy")
            {
                string LabelName = e.CommandArgument.ToString();
                M_Label newlbl = bll.GetLabelXML(LabelName);
                newlbl.LableName = newlbl.LableName + DataSecurity.RandomNum(4);
                newlbl.LabelID = 0;
                bll.AddLabelXML(newlbl);
                Response.Redirect("LabelManage.aspx");
            }
            if (e.CommandName == "Download")
            {
                string LabelName = e.CommandArgument.ToString();
                M_Label newlbl = bll.GetLabelXML(LabelName);
                SafeSC.DownFile(B_Label.GetLabelVPath(newlbl), newlbl.LableName + ".lable");
            }
        }
        public string GetLabelLink(string id, string labelname, string LableType, string text)
        {
            string href = "<a href=\"{0}?LabelName=" + Server.UrlEncode(labelname) + "\" title=\"" + labelname + "\">" + text + "</a>";
            string re = "";
            if (DataConverter.CLng(LableType) == 1)
                re = string.Format(href, "LabelHtml.aspx");
            else if (DataConverter.CLng(LableType) < 5)
                re = string.Format(href, "LabelSql.aspx");
            else
                re = string.Format(href, "PageLabel.aspx");
            return re;
        }
        public string GetLabelType(string type)
        {
            return bll.GetLabelType(Convert.ToInt32(type));
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("LabelManage.aspx?Cate=&KeyWord=" + HttpUtility.UrlEncode(KeyWord));
        }
        // 批量导出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //string lblid = "";
            //foreach (RepeaterItem item in repFile.Items)
            //{
            //    Control ctrl = item.FindControl("chkSel");
            //    Control ctrlHide = item.FindControl("LabelHiddenID");
            //    CheckBox ck = ctrl as CheckBox;
            //    Label lb = ctrlHide as Label;
            //    if (ck != null && lb != null)
            //    {
            //        if (ck.Checked)
            //        {
            //            if (string.IsNullOrEmpty(lblid))
            //                lblid = lb.Text;
            //            else
            //                lblid = lblid + "," + lb.Text;
            //        }
            //    }
            //}
            //DataSet ds = bll.GetLabelSelect(lblid);
            //string filename = base.Request.PhysicalApplicationPath + @"\" + "App_Data" + @"\" + "LabelExport.xml";
            //if (!FileSystemObject.IsExist(filename, FsoMethod.File))
            //    FileSystemObject.Create(filename, FsoMethod.File);
            //ds.WriteXml(filename);
            //function.WriteSuccessMsg("导出成功!");
        }
        // 批量删除
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"] ?? "";
            foreach (string id in ids.Split(','))
            {
                bll.DelLabelXML(Convert.ToInt32(id));
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}