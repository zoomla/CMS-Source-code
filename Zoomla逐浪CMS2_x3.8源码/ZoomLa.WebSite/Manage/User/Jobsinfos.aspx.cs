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
using System.Xml;
using System.Text;
public partial class manage_User_Jobsinfos : CustomerPageAction
{
    private B_User buser = new B_User();
    private B_ModelField bfield = new B_ModelField();
    private B_Model bmodel = new B_Model();
    private B_Group bgp = new B_Group();
    string pagesizeo;
    //string pageneum2 = "";
    protected int ModelIDs = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        string ModelID = string.IsNullOrEmpty(Request.QueryString["modeid"]) ? "none" : Request.QueryString["modeid"].ToString();
        pagesizeo = string.IsNullOrEmpty(Request.QueryString["page"]) ? "15" : Request.QueryString["page"].ToString();
        if (!this.Page.IsPostBack)
        {
            function.WriteErrMsg("该页面无记录");
            if (Request.Form["DropDownList1"] != null)
            {
                this.Hiddenpagenum.Value = Request.Form["DropDownList1"];
            }
            else
            {
                this.Hiddenpagenum.Value = Request.QueryString["CurrentPage"];
            }
            this.HiddenPage.Value = pagesizeo;
        }


        this.HdnModelName.Value = ModelID;
        //switch (ModelID)
        //{
        //    case "qiye":
        //        ModelID = UserModuleConfig.JobsConfig.Company.ToString();

        //        break;
        //    case "zhappin":
        //        ModelID = UserModuleConfig.JobsConfig.CompanyJobs.ToString();
        //        break;
        //    case "geren":
        //        ModelID = UserModuleConfig.JobsConfig.Resume.ToString();

        //        break;
        //}
        this.ModelIDs = DataConverter.CLng(ModelID);
        //function.WriteErrMsg(ModelID);

        if (ModelID.ToString() == "none" || string.IsNullOrEmpty(ModelID.ToString()))
        {
            function.WriteErrMsg("没有设置模型参数");
        }
        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(ModelID));
        //this.Label1.Text = "<a href=Jobsinfos.aspx?modeid=" + this.HdnModelName.Value + " >" + model.ModelName +"</a>";
        this.HdnModelID.Value = ModelID.ToString();

        ShowGrid();
        this.RepNodeBind(DataConverter.CLng(pagesizeo));

        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='Jobsconfig.aspx'>人才招聘</a></li><li>" + model.ModelName + "</li>");
    }
    private void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
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
    private void ShowGrid()
    {
        
        EGV.AutoGenerateColumns = false;
        DataControlFieldCollection dcfc = EGV.Columns;
        dcfc.Clear();
        TemplateField tf = new TemplateField();
        CheckBox ch = new CheckBox();
        tf.HeaderText = "选择";



        BoundField bf1 = new BoundField();
        bf1.HeaderText = "ID";
        bf1.DataField = "ID";
        bf1.HeaderStyle.Width = Unit.Percentage(5);
        bf1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        dcfc.Add(bf1);


        DataTable dt = this.bfield.GetModelFieldListall(DataConverter.CLng(this.HdnModelID.Value));
        foreach (DataRow dr in dt.Rows)
        {
            if (DataConverter.CBool(dr["ShowList"].ToString()))
            {
                ButtonField Link3 = new ButtonField();
                Link3.ButtonType = ButtonType.Link;
                Link3.HeaderText = dr["FieldAlias"].ToString();
                Link3.DataTextField = dr["FieldName"].ToString();
                Link3.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                Link3.ItemStyle.Width = Unit.Percentage(10);
                Link3.ItemStyle.CssClass = "overflow:hidden;text-overflow:ellipsis";

                dcfc.Add(Link3);
            }
        }
        ButtonField Link = new ButtonField();

        Link.ButtonType = ButtonType.Link;
        Link.HeaderText = "修改";
        Link.Text = "修改";
        Link.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        Link.ItemStyle.Width = Unit.Percentage(10);
        dcfc.Add(Link);


        ButtonField Link1 = new ButtonField();
        Link1.ButtonType = ButtonType.Link;
        Link1.HeaderText = "删除";
        Link1.Text = "删除";
        Link1.ItemStyle.Width = Unit.Percentage(10);
        Link1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        Link1.CausesValidation = true;
        dcfc.Add(Link1);

        
    }

    private void RepNodeBind(int page)
    {
        if (DataConverter.CLng(this.HdnModelID.Value) > 0)
        {
            M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(this.HdnModelID.Value));

            if (model.ItemName != null)
            {

                if (!string.IsNullOrEmpty(model.TableName))
                {
                    if (model.TableName != "")
                    {
                        DataTable UserData = buser.GetUserModeInfo(model.TableName, 9, 9);
                        this.EGV.DataSource = UserData;
                        if (UserData != null)
                        {
                            if (UserData.Rows.Count > 0)
                            {
                            }
                        }

                    }
                }
            }

        }
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
            case "del2":
                //删除记录，同时删除目标数据库
                break;
        }
    }
    public string Strsplit(string str, int maxlength)
    {
        M_ModelField mideld = bfield.GetModelByIDXML(maxlength);
       // M_ModelField mideld = bfield.GetModelByID(this.ModelIDs.ToString(), maxlength);
        string strg = string.Empty;
        if (str.Length > maxlength)
            strg = str.Substring(0, maxlength) + "...";
        else
            strg = str;
        return str;
    }

    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string ModelID = this.HdnModelID.Value;
            LinkButton btn = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            //btn.PostBackUrl = "EditModelInfo.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[0].Text;
            btn.Attributes.Add("href", "EditJobsinfos.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[1].Text + "&ModelName=" + this.HdnModelName.Value);
            LinkButton btn1 = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            btn1.Attributes.Add("href", "DelJobsinfos.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[1].Text + "&ModelName=" + this.HdnModelName.Value + "&Modeltype=1");
            btn1.Attributes.Add("onclick", "javascript:return confirm('你确认要删除此数据吗？');");
            LinkButton btn3 = (LinkButton)e.Row.Cells[2].Controls[0];
            //btn.PostBackUrl = "EditModelInfo.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[0].Text;
            btn3.Attributes.Add("href", "EditJobsinfos.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[1].Text + "&ModelName=" + this.HdnModelName.Value);
            if (this.EGV.HeaderRow.Cells.Count > 0)
            {
                this.EGV.HeaderRow.Cells[0].Width = 30;
            }
            if (this.EGV.HeaderRow.Cells.Count > 1)
            {
                this.EGV.HeaderRow.Cells[1].Width = 50;
            }
            if (this.EGV.HeaderRow.Cells.Count > 2)
            {
                this.EGV.HeaderRow.Cells[2].Width = 600;
            }
            if (this.EGV.HeaderRow.Cells.Count > 3)
            {
                this.EGV.HeaderRow.Cells[3].Width = 50;
            }
            if (this.EGV.HeaderRow.Cells.Count > 4)
            {
                this.EGV.HeaderRow.Cells[4].Width = 50;
            }

        }

    }
    protected void btn_DeleteRecords_Click(object sender, EventArgs e)
    {
        int IDo;
        int ModelID = DataConverter.CLng(this.HdnModelID.Value);
        foreach (GridViewRow gvr in EGV.Rows)
        {
            if (((CheckBox)gvr.Cells[0].FindControl("pidCheckbox")).Checked) //此行cbSelect被勾选
            {
                IDo = DataConverter.CLng(gvr.Cells[1].Text);

                buser.DelModelInfo2(bmodel.GetModelById(ModelID).TableName, IDo, 1);
            }
        }
        ShowGrid();
        this.RepNodeBind(DataConverter.CLng(this.HiddenPage.Value));
    }

    protected void btn_upIsCreate_Click(object sender, EventArgs e)
    {

        int IDo;
        int ModelID = DataConverter.CLng(this.HdnModelID.Value);
        foreach (GridViewRow gvr in EGV.Rows)
        {
            if (((CheckBox)gvr.Cells[0].FindControl("pidCheckbox")).Checked) //此行cbSelect被勾选
            {
                IDo = DataConverter.CLng(gvr.Cells[1].Text);

                buser.DelModelInfo2(bmodel.GetModelById(ModelID).TableName, IDo, 89);
            }
        }
        ShowGrid();
        this.RepNodeBind(DataConverter.CLng(this.HiddenPage.Value));
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        ShowGrid();
        this.RepNodeBind(DataConverter.CLng(this.HiddenPage.Value));
        foreach (GridViewRow oo in EGV.Rows)
        {
            ((CheckBox)oo.Cells[0].FindControl("pidCheckbox")).Checked = ((CheckBox)sender).Checked;

        }
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        string IDo = "";
        string ModelID = this.HdnModelID.Value;
        foreach (GridViewRow gvr in EGV.Rows)
        {
            if (((CheckBox)gvr.Cells[0].FindControl("pidCheckbox")).Checked) //此行cbSelect被勾选
            {
                IDo = IDo + gvr.Cells[1].Text + "|";

            }
        }
        Response.Redirect("EditInfoList.aspx?ModelID=" + ModelID + "&ID=" + IDo + "&ModelName=" + this.HdnModelName.Value);
    }
}




