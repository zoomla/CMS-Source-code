using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using ZoomLa.Components;

public partial class User_Content_Reply : System.Web.UI.Page
{
    B_Pub bp = new B_Pub();
    B_ModelField bmf = new B_ModelField();
    B_User bu = new B_User();
    protected B_Sensitivity sll = new B_Sensitivity();
    public int ModelID
    {
        get { return DataConverter.CLng(ViewState["model"]); }
        set { ViewState["model"] = value; }
    }
    public int PubID
    {
        get { return DataConverter.CLng(ViewState["pubid"]); }
        set { ViewState["pubid"] = value; }
    }
    public string PubTable
    {
        get { return ViewState["table"] + ""; }
        set { ViewState["table"] = value; }
    }
    public int PubCID
    {
        get { return DataConverter.CLng(ViewState["cid"]); }
        set { ViewState["cid"] = value; }
    }
    public string PubInputer
    {
        get { return ViewState["pubinputer"]+""; }
        set { ViewState["pubinputer"]=value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string pid = base.Request.QueryString["pubid"];
        if (!string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            M_Pub mp = bp.GetSelect(DataConverter.CLng(pid));
            ModelID = mp.PubModelID;
            PubID = mp.Pubid;
            PubTable = mp.PubTableName;
            PubCID = DataConverter.CLng(Request.QueryString["ID"]);
            DataTable dtpub = bp.GetPubModeById(PubCID, PubTable);
            if (dtpub != null && dtpub.Rows.Count > 0)
            {
                txtTitle.Text = "回复 [" + dtpub.Rows[0]["PubUserName"] + "    " + dtpub.Rows[0]["PubTitle"] + "]";
                PubInputer = dtpub.Rows[0]["PubInputer"].ToString();
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = this.bmf.GetModelFieldList(ModelID);
        M_UserInfo UserInfo = bu.GetLogin();
        DataTable table = new DataTable();
        table.Columns.Add(new DataColumn("FieldName", typeof(string)));
        table.Columns.Add(new DataColumn("FieldType", typeof(string)));
        table.Columns.Add(new DataColumn("FieldValue", typeof(string)));

        foreach (DataRow dr in dt.Rows)
        {
            if (DataConverter.CBool(dr["IsNotNull"].ToString()))
            {
                if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
                {
                    function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
                }
            }
            if (dr["FieldType"].ToString() == "FileType")
            {
                string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                string sizefield = Sett[1].Split(new char[] { '=' })[1];
                if (chksize && sizefield != "")
                {
                    DataRow row2 = table.NewRow();
                    row2[0] = sizefield;
                    row2[1] = "FileSize";
                    row2[2] = this.Page.Request.Form["txt_" + sizefield];
                    table.Rows.Add(row2);
                }
            }
            if (dr["FieldType"].ToString() == "MultiPicType")
            {
                string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                string sizefield = Sett[1].Split(new char[] { '=' })[1];
                if (chksize && sizefield != "")
                {
                    if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + sizefield]))
                    {
                        function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                    }
                    DataRow row1 = table.NewRow();
                    row1[0] = sizefield;
                    row1[1] = "ThumbField";
                    row1[2] = BaseClass.CheckInjection(this.Page.Request.Form["txt_" + sizefield]);
                    table.Rows.Add(row1);
                }
            }
            DataRow row = table.NewRow();
            row[0] = dr["FieldName"].ToString();
            string ftype = dr["FieldType"].ToString();
            row[1] = ftype;
            string fvalue = BaseClass.CheckInjection(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]);
            if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
            {
                fvalue = BaseClass.CheckInjection(sll.ProcessSen(fvalue));
            }
            row[2] = fvalue;
            table.Rows.Add(row);


        }
        DataTable dt1 = bp.GetPubModeById(PubCID, PubTable);
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            bp.AddPubModelCustom(table, PubTable, PubID, UserInfo.UserName, UserInfo.UserID, DataConverter.CLng(dt1.Rows[0]["PubContentid"]), DataConverter.CLng(PubCID), Request.UserHostAddress, (DataConverter.CLng(dt1.Rows[0]["Pubnum"]) + 1), 0, Server.HtmlEncode(txtTitle.Text), Server.HtmlEncode(txtContent.Text), DateTime.Now, 0, PubInputer);
            Response.Write("<script>self.opener.location.reload();window.close();</script>");
        }
        Response.Write("<script>alert('回复成功');location='ManagePub.aspx';</script>");
        Response.End();
    }
}
