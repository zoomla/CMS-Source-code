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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Components;
using System.Collections.Generic;
using ZoomLa.BLL.Page;
using ZoomLa.Model.Page;

public partial class manage_Page_AuditUser : CustomerPageAction
{
    public DataTable pageuserinfo;
    protected B_ModelField mll = new B_ModelField();
    protected B_Model moll = new B_Model();
    protected B_User ull = new B_User();
    protected M_UserInfo UserInfo;
    protected B_Content cll = new B_Content();
    protected B_PageStyle sll = new B_PageStyle();
    protected string username;
    public int ModelID, NodeID, styleid;
    protected B_Sensitivity sell = new B_Sensitivity();

    public B_PageReg b_PageReg = new B_PageReg();
    private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        string menu = DataSecurity.FilterBadChar(Request.QueryString["menu"]);
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li><a href='PageManage.aspx'>黄页管理</a>  <li>查看黄页申请</li>");
            B_Admin badmin = new B_Admin();
            B_Group gll = new B_Group();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "PageManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            M_PageReg regMod = b_PageReg.SelReturnModel(Mid);
            int modid = regMod.ModelID;//模型ID
            this.ModelID = modid;
            this.HdnID.Value = Mid.ToString();
            this.HdnModel.Value = this.ModelID.ToString();
            UName_T.Text = regMod.UserName;
            CompName_T.Text = regMod.CompanyName;
            Logo_T.Text = regMod.LOGO;
            PageInfo_T.Text = regMod.PageInfo;
            TxtTemplate_hid.Value = regMod.Template;
            if (menu == "view")//查看黄页资料
            {
                Label1.Text = "查看黄页资料";
            }
            else if (menu == "modify")
            {
                Label1.Text = "修改黄页资料";
            }
            int Umodeid = regMod.ModelID;
            DataTable tbinfo = mll.SelectTableName(regMod.TableName, "UserName = '" + regMod.UserName + "'");
            if (tbinfo.Rows.Count > 0)
            {
                ModelHtml.Text = mll.InputallHtml(Umodeid, 0, new ModelConfig()
                {
                    ValueDT = tbinfo
                });
            }
            else
            {
                this.ModelHtml.Text = this.mll.InputallHtml(DataConverter.CLng(Umodeid), 0,new ModelConfig());
            }
            Temp_RPT.DataSource = sll.Sel();
            Temp_RPT.DataBind();
            NodeStyle_Hid.Value = regMod.NodeStyle.ToString();
            if (string.IsNullOrEmpty(regMod.Template))//如果没有模板就默认为样式首页模板
            {
                M_PageStyle stylemod = sll.SelReturnModel(regMod.NodeStyle);
                TxtTemplate_hid.Value = stylemod.TemplateIndex;
            }
        }
       
    }

    protected string GetProName(int num)
    {
        B_Page pll = new B_Page();
        M_Page pageinfo = pll.GetSelect(Mid);
        string returntxt = "";
        switch (num)
        {
            case 1:
                returntxt = pageinfo.Proname;
                break;
            case 2:
                returntxt = pageinfo.Proname;
                break;
            case 3:
                returntxt = pageinfo.LOGO;
                break;
            case 4:
                returntxt = pageinfo.PageInfo;
                break;
        }
        return returntxt;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ull.Audit(Mid.ToString(), 1, 0);
        Response.Write("<script language=javascript>alert('设置成功!');location.href='AuditUser.aspx?menu=view&id=" + Mid + "';</script>");
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManage.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ull.Audit(Mid.ToString(), 0, 0);
        Response.Redirect("AuditUser.aspx?menu=view&id=" + Mid);
    }
    //修改
    protected void Button5_Click(object sender, EventArgs e)
    {
        int HdnID = DataConverter.CLng(this.HdnID.Value);
        this.ModelID = DataConverter.CLng(this.HdnModel.Value);
        DataTable dt = this.mll.GetModelFieldList(this.ModelID);
        M_ModelInfo dts = this.moll.GetModelById(this.ModelID);
        #region 扩展表
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
                    row1[2] = this.Page.Request.Form["txt_" + sizefield];
                    table.Rows.Add(row1);
                }
            }
            DataRow row = table.NewRow();
            row[0] = dr["FieldName"].ToString();
            string ftype = dr["FieldType"].ToString();
            row[1] = ftype;
            string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
            if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
            {
                fvalue = sell.ProcessSen(fvalue);
            }
            row[2] = fvalue;
            table.Rows.Add(row);
        }

        string uname = ull.GetLogin().UserName;
        this.UserInfo = ull.GetLogin();

        #endregion
        M_PageReg regMod = b_PageReg.SelReturnModel(Mid);
        regMod.CompanyName = CompName_T.Text;
        regMod.LOGO = Logo_T.Text;
        regMod.PageInfo = PageInfo_T.Text;
        regMod.Template = TxtTemplate_hid.Value;
        regMod.NodeStyle = Convert.ToInt32(NodeStyle_Hid.Value);
        cll.Page_Update(table, regMod);
        function.WriteSuccessMsg("修改成功", "PageManage.aspx");
    }

    public string getstylename(string styleid)
    {
        int styid = DataConverter.CLng(styleid);
        if (styid > 0)
        {
            return sll.Getpagestrylebyid(styid).PageNodeName.ToString();
        }
        else
        {
            return "无样式";
        }
    }
}