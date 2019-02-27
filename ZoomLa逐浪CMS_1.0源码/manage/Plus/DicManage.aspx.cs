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
public partial class manage_Plus_DicManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
            {
                Bind();
            }
    }
        private void Bind()
        {
            DataView dv = B_Cate.GetCateInfo();
            this.GridView1.DataSource = dv;
            GridView1.DataKeyNames = new string[] { "CateID" };
            this.GridView1.DataBind();
        }
        //绑定分页
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
   
 
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mydelete")
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox IdCheck;
                IdCheck = (CheckBox)GridView1.Rows[i].FindControl("nameCheckBox");
                if (IdCheck.Checked)
                {
                    B_Cate.DeleteByID(Convert.ToInt32(e.CommandArgument.ToString()));
                    Bind();
                }
            }
        }
        if (e.CommandName == "myupdate")
        {
            Session.Add("cateid", e.CommandArgument);
            M_Cate tempcate=new M_Cate();
            tempcate=B_Cate.GetCateByid(Convert.ToInt32( e.CommandArgument.ToString()));
            TextBox1.Text =tempcate.CateName;
            Button1.Text = "更新";
        }
        if (e.CommandName == "mylink")
        {
            Response.Redirect("datamanage.aspx?cateid="+e.CommandArgument);
            Response.End();
            
        }
    }
    //添加
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "添加分类")
        {
           
            Button1.Text = "更新";
            M_Cate temp=new M_Cate();
            if (TextBox1.Text == "")
                return;
            temp.CateName = TextBox1.Text;
            if (B_Cate.Add(temp))
            {
                string str = " <script>alert('Add success'); </script>";
                Response.Write(str);
            }
            Bind();
        }
        if (Button1.Text == "更新")
        {
            Button1.Text = "添加分类";
            M_Cate temp = new M_Cate();
            temp.CateName = TextBox1.Text;
           
            B_Cate.Update(temp);
            Bind();
          
           
        }
    } 
   
    
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("nameCheckBox");
            if (CheckBox1.Checked == true)
                cbox.Checked = true;
            else
                cbox.Checked = false;
            
            
        }

    } 
    //批量删除
    protected void Button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("nameCheckBox");
            if (cbox.Checked == true)
            {
                int CateID = Convert.ToInt32(GridView1.DataKeys[i].Value);
                B_Cate.DeleteByID(CateID);
            }
        }
        Bind();

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        
    }
}
