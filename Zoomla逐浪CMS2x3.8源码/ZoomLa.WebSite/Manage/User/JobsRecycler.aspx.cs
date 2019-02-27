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

public partial class manage_User_JobsRecycler : CustomerPageAction
{
    private B_User buser = new B_User();
    private B_ModelField bfield = new B_ModelField();
    private B_Model bmodel = new B_Model();
    private B_Group bgp = new B_Group();

    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li>后台管理</li><li>人才系统管理回收站</li><li><asp:Literal Text='添加房间'>" + Label1_Hid.Value+ "</asp:Literal></li>");
        B_Admin badmin = new B_Admin();

        string ModelID = string.IsNullOrEmpty(Request.QueryString["modeid"]) ? "none" : Request.QueryString["modeid"].ToString();
        this.HdnModelName.Value = ModelID;

        switch (ModelID)
        {
            case "qiye":
                ModelID = UserModuleConfig.JobsConfig.Company.ToString();

                break;
            case "zhappin":
                ModelID = UserModuleConfig.JobsConfig.CompanyJobs.ToString();

                break;
            case "geren":
                ModelID = UserModuleConfig.JobsConfig.Resume.ToString();

                break;
        }
        if (ModelID.ToString() == "0" || string.IsNullOrEmpty(ModelID.ToString()))
        {
            function.WriteErrMsg("没有设置模型");
        }
        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(ModelID));
        this.Label1_Hid.Value = model.ModelName;
        this.HdnModelID.Value = ModelID.ToString();
        ShowGrid();
        this.RepNodeBind();
        Call.SetBreadCrumb(Master, "<li>后台管理</li><li>人才系统管理回收站</li><li>[<a href='JobsRecycler.aspx?modeid=qiye'>招聘企业</a>][<a href='JobsRecycler.aspx?modeid=zhappin'>职位信息</a>][<a href='JobsRecycler.aspx?modeid=geren'>用户简历</a>]</li>");
    }

    private void ShowGrid()
    {
        DataTable dt = this.bfield.GetModelFieldListall(DataConverter.CLng(this.HdnModelID.Value));
        Egv.AutoGenerateColumns = false;
        DataControlFieldCollection dcfc = Egv.Columns;
        dcfc.Clear();


        TemplateField tf = new TemplateField();

        CheckBox ch = new CheckBox();

        tf.HeaderText = "选择";
        tf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        tf.ItemStyle.Width = Unit.Percentage(10);
        tf.ItemTemplate = new GenericItem();
        //Egv.Columns.Add(tf);
        dcfc.Add(tf);

        BoundField bf1 = new BoundField();
        bf1.HeaderText = "ID";
        bf1.DataField = "ID";
        bf1.HeaderStyle.Width = Unit.Percentage(5);
        bf1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        dcfc.Add(bf1);

        foreach (DataRow dr in dt.Rows)
        {
            if (DataConverter.CBool(dr["ShowList"].ToString()))
            {
                BoundField bf = new BoundField();
                bf.HeaderText = dr["FieldAlias"].ToString();
                bf.DataField = dr["FieldName"].ToString();
                bf.HeaderStyle.Width = Unit.Percentage(DataConverter.CDouble(dr["ShowWidth"]));
                bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                dcfc.Add(bf);
            }
        }



        ButtonField Link = new ButtonField();

        Link.ButtonType = ButtonType.Link;
        Link.HeaderText = "还原";
        Link.Text = "还原";
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

        if (dt != null)
        {
            dt.Dispose();
        }
        if (dt != null)
        {
            dt.Dispose();
        }

    }

    private void RepNodeBind()
    {
        M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(this.HdnModelID.Value));
        if (!string.IsNullOrEmpty(model.TableName))
        {
            if (model.TableName != "")
            {
                DataTable UserData = buser.GetUserModeInfo(model.TableName, 9, 10);
                this.Egv.DataSource = UserData;
                this.Egv.DataKeyNames = new string[] { "ID" };
                this.Egv.DataBind();
            }
        }
    }


    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.Egv.PageIndex = e.NewPageIndex;
        this.RepNodeBind();
    }
    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ModelID = this.HdnModelID.Value;


            LinkButton btn = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 2].Controls[0];
            //btn.PostBackUrl = "EditModelInfo.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[0].Text;
            btn.Attributes.Add("href", "DelJobsinfos.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[1].Text + "&ModelName=" + this.HdnModelName.Value + "&Modeltype=3");

            LinkButton btn1 = (LinkButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0];
            btn1.Attributes.Add("href", "DelJobsinfos.aspx?ModelID=" + ModelID + "&ID=" + e.Row.Cells[1].Text + "&ModelName=" + this.HdnModelName.Value + "&Modeltype=4");
            btn1.Attributes.Add("onclick", "javascript:return confirm('你确认要删除此数据吗？');");
        }
    }
    protected void btn_DeleteRecords_Click(object sender, EventArgs e)
    {

        int IDo;
        int ModelID = DataConverter.CLng(this.HdnModelID.Value);
        foreach (GridViewRow gvr in Egv.Rows)
        {
            if (((CheckBox)gvr.Cells[0].FindControl("pidCheckbox")).Checked) //此行cbSelect被勾选
            {
                IDo = DataConverter.CLng(gvr.Cells[1].Text);
                buser.DelModelInfo(bmodel.GetModelById(ModelID).TableName, IDo);
            
            }
        }

        ShowGrid();
        this.RepNodeBind();
    }
    protected void btn_ResumeRecords_Click(object sender, EventArgs e)
    {

        int IDo;
        int ModelID = DataConverter.CLng(this.HdnModelID.Value);
        foreach (GridViewRow gvr in Egv.Rows)
        {
            if (((CheckBox)gvr.Cells[0].FindControl("pidCheckbox")).Checked) //此行cbSelect被勾选
            {
                IDo = DataConverter.CLng(gvr.Cells[1].Text);
                buser.DelModelInfo2(bmodel.GetModelById(ModelID).TableName, IDo, 3);

            }
        }

        ShowGrid();
        this.RepNodeBind();
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {

        string IDo = "";
        string ModelID = this.HdnModelID.Value;
        foreach (GridViewRow gvr in Egv.Rows)
        {
            if (((CheckBox)gvr.Cells[0].FindControl("pidCheckbox")).Checked) //此行cbSelect被勾选
            {
                IDo = IDo + gvr.Cells[1].Text + "|";
                Response.Write("<script>alert('" + IDo + "');</script>");

            }
        }
        Response.Write("<script>alert('" + IDo + "');</script>");
        Response.Redirect("EditInfoList.aspx?ModelID=" + ModelID + "&ID=" + IDo + "&ModelName=" + this.HdnModelName.Value);

    }


    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow oo in Egv.Rows)
        {
            ((CheckBox)oo.Cells[0].FindControl("pidCheckbox")).Checked = ((CheckBox)sender).Checked;

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int ModelID = DataConverter.CLng(this.HdnModelID.Value);
        if (buser.DelModelInfoAll(bmodel.GetModelById(ModelID).TableName))
        {
            function.WriteSuccessMsg("删除成功");
        }
        else
        {
            function.WriteErrMsg("删除失败");
        }
        ShowGrid();
        this.RepNodeBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
    
        //int ModelID = DataConverter.CLng(this.HdnModelID.Value);
        //if( buser.DelModelInfo2(bmodel.GetModelById(ModelID).TableName,1,11))
        //{
        //    Response.Write("<script>alert('还原成功');</script>");
        //}
        //else
        //{
        //    Response.Write("<script>alert('还原失败');</script>");
        //}
        //ShowGrid();
        //this.RepNodeBind();
    }
}
public class GenericItem : ITemplate
{

    public GenericItem()
    {

    }

    public void InstantiateIn(Control container)
    {

        CheckBox txt = new CheckBox();

        txt.ID = "pidCheckbox";

        //txt.DataBinding += new EventHandler(this.BindData);

        container.Controls.Add(txt);

    }
}