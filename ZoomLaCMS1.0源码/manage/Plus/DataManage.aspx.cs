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
using System.Data.SqlClient;
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
public partial class manage_Plus_DataManage : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        M_Cate temp = new M_Cate();
        int CateID = Convert.ToInt32(Request.QueryString["cateid"]);
        Session.Add("CateId",CateID);
        temp = B_Cate.GetCateByid(CateID);
        this.Label2.Text = temp.CateName;
        if (!Page.IsPostBack)
        {
            Bind();
        }
    }
    private void Bind()
    {
        this.GridView1.DataSource = B_CateDetail.GetcatedetailAll(Convert.ToInt32(Session["CateId"]));
        GridView1.DataKeyNames = new string[] { "CateDetailID" };
        this.GridView1.DataBind();
        if (GridView1.Rows.Count == 0)
        {
            Label3.Text = "没有此分类下的数据值";
            this.CheckBox1.Visible = false;
            this.Button7.Visible = false;
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Bind();
    }

    //获取选中记录，并绑定数据
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        //this.HdnRoleId.Value = (GridView1.DataKeys[e.NewEditIndex].Value).ToString();
        Bind();
    }
    //全选
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("detailCheckBox");
            if (CheckBox1.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }

    //批量删除
    protected void Button7_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("detailCheckBox");
            if (cbox.Checked == true)
            {
                int catedetailID = Convert.ToInt32(GridView1.DataKeys[i].Value);
                B_CateDetail.DeleteByID(catedetailID);
            }
        }
        Bind();
    }
    


    protected void Button8_Click(object sender, EventArgs e)
    {
        if (Button8.Text == "添加")
        {
            M_CateDetail temp = new M_CateDetail();
            temp.CateDetailName= TextBox1.Text;
            temp.CateID = Convert.ToInt32(Session["cateid"]);
            if (B_CateDetail.Add(temp))
            {
                
                this.Bind();
            
            }
            
        }
        if (Button8.Text == "更新")
        {
            Button8.Text = "添加";
            M_CateDetail temp = new M_CateDetail();
            temp.CateID =Convert.ToInt32( Session["CateID"]);
            temp.CateDetailName = TextBox1.Text;
            temp.CateDetailID = Convert.ToInt32(Session["catedetailid"]);
            B_CateDetail.Update(temp);
             this.Bind();
            
        }
    }
    protected void Button7_Click1(object sender, EventArgs e)
    {
        for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("detailCheckBox");
            if (cbox.Checked == true)
            {
                int CateDetailID = Convert.ToInt32(GridView1.DataKeys[i].Value);
               B_CateDetail.DeleteByID(CateDetailID);
            }
        }
        Bind();
    }
    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("detailCheckBox");
            if (CheckBox1.Checked == true)
                cbox.Checked = true;
            else
                cbox.Checked = false;


        }
    }
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mydelete")
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox IdCheck;
                IdCheck = (CheckBox)GridView1.Rows[i].FindControl("detailCheckBox");
                if (IdCheck.Checked)
                {
                    B_CateDetail.DeleteByID(Convert.ToInt32(e.CommandArgument.ToString()));
                }
            }
        }
        if (e.CommandName == "myupdate")
        {
            Session.Add("catedetailid", e.CommandArgument);
            M_CateDetail temp = B_CateDetail.GetcatedetailById(Convert.ToInt32(e.CommandArgument));
            
                TextBox1.Text = temp.CateDetailName;
                Button8.Text = "更新";
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("DataManage.aspx?cateid=21");
    }
}
