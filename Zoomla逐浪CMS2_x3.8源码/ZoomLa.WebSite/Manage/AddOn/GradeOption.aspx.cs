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

public partial class Manage_I_Guest_GradeOption : CustomerPageAction
{
    B_GradeOption gradeBll = new B_GradeOption();
    public int CateID { get { return DataConverter.CLng(Request.QueryString["CateID"]); } }
    public int ParentID { get { return DataConverter.CLng(Request.QueryString["ParentID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "AskManage");
            if (CateID <= 0)
                function.WriteErrMsg("没有指定多级数据字典分类ID", "GradeCateManage.aspx");       
            //this.lblCateName.Text = Cate.CateName;
            M_GradeCate Cate = gradeBll.GetCate(CateID);
            this.LblCate.Text = Cate.CateName;
            //分级选项别名
            string[] GradeAlias = Cate.GradeAlias.Split(new char[] { '|' });
            //当前选项级别
            int level = 0;
            if (ParentID == 0)
            {
                level = 1;
                this.LblPreGrade.Text = "";
            }
            else
            {
                M_Grade GradeOption = B_GradeOption.GetGradeOption(ParentID);
                level = GradeOption.Grade + 1;
                this.LblPreGrade.Text = GradeOption.GradeName;
            }
            if (level <= GradeAlias.Length)
            {
                if (level == GradeAlias.Length)
                    this.HdnLastLevel.Value = "1";
                else
                    this.HdnLastLevel.Value = "0";
            }
            else
            {
                function.WriteErrMsg("当前选项已无下级选项！");
            }
            this.LblLevel.Text = level.ToString();
            this.HdnCateID.Value = CateID.ToString();
            this.HdnParentID.Value = ParentID.ToString();
            dBind();
            string bread = "<li><a href='DictionaryManage.aspx'>数据字典</a></li><li><a href='GradeCateManage.aspx'>多级数据字典管理</a></li>";
            if (ParentID > 0)
            {
                M_Grade parentMod = B_GradeOption.GetGradeOption(ParentID);
                bread += "<li><a href='GradeOption.aspx?CateID=" + CateID + "'>" + parentMod.GradeName + "</a></li>";
            }
            else
            {
                bread += "<li><a href='GradeCateManage.aspx'>" + Cate.CateName + "</a></li>";
            }
            bread += "<li class='active'>添加选项</li>";
            Call.SetBreadCrumb(Master, bread);        
        }
    }
    private void dBind()
    {
        int ParentID = DataConverter.CLng(this.HdnParentID.Value);
        this.Gdv.DataSource = B_GradeOption.GetGradeList(CateID, ParentID);
        if (CateID == 3) { Gdv.Columns[2].HeaderText = "分类"; }//百科分类
        this.Gdv.DataKeyNames = new string[] { "GradeID" };
        this.Gdv.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.Gdv.PageIndex = e.NewPageIndex;
        dBind();
    }
    protected void Gdv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtn = (LinkButton)e.Row.FindControl("LinkButton3");
            if (this.HdnLastLevel.Value == "1")
                lbtn.Visible = false;
            string js = string.Format("window.location.href='GradeOption.aspx?CateID=" + HdnCateID.Value + "&ParentID=" + Gdv.DataKeys[e.Row.RowIndex].Value + "';");
            e.Row.Attributes.Add("ondblclick", js); 
        }
    }
    protected void Gdv_Editing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            string Id = e.CommandArgument.ToString();
            B_GradeOption.DelGradeOption(DataConverter.CLng(Id));
            dBind();
        }
        if (e.CommandName == "Edit1")
        {
            string Id = e.CommandArgument.ToString();
            M_Grade info = B_GradeOption.GetGradeOption(DataConverter.CLng(Id));
            this.txtGradeName.Text = info.GradeName;
            this.HdnGradeID.Value = Id;
            this.btnSave.Text = "修改";
        }
        if (e.CommandName == "DicList")
        {
            string Id = e.CommandArgument.ToString();
            Response.Redirect("GradeOption.aspx?CateID=" + this.HdnCateID.Value + "&ParentID=" + Id);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string GradeName = this.txtGradeName.Text.Trim();

        if (string.IsNullOrEmpty(GradeName))
        {
            function.WriteErrMsg("选项值不能为空！");
        }
        else
        {

            int GradeID = DataConverter.CLng(this.HdnGradeID.Value);
            if (GradeID > 0)
            {
                M_Grade info = B_GradeOption.GetGradeOption(GradeID);
                info.GradeName = GradeName;                    
                B_GradeOption.UpdateDic(info);
            }
            else
            {
                M_Grade info = new M_Grade();
                info.GradeID = 0;
                info.GradeName = GradeName;
                info.ParentID = DataConverter.CLng(this.HdnParentID.Value);
                info.Cate = CateID;
                info.Grade = DataConverter.CLng(this.LblLevel.Text);
                B_GradeOption.AddGradeOption(info);
            }
            this.txtGradeName.Text = "";
            this.HdnGradeID.Value = "0";
            this.btnSave.Text = "添加";
            dBind();
                    
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        int ParentID = DataConverter.CLng(this.HdnParentID.Value);
        if (ParentID == 0)
            Response.Redirect("GradeCateManage.aspx");
        else
            Response.Redirect("GradeOption.aspx?CateID=" + this.HdnCateID.Value + "&ParentID=" + B_GradeOption.GetGradeOption(ParentID).ToString());            
    }
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            gradeBll.DelOptioinsByIDS(Request.Form["idchk"]);
            Response.Redirect(Request.RawUrl); 
        }
    }
}