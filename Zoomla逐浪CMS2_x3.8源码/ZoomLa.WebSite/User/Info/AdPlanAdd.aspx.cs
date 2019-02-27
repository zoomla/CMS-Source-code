using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model.AdSystem;
using ZoomLa.BLL.AdSystem;
using System.IO;
using System.Text;
using System.Data;
public partial class User_Info_AdPlanAdd : System.Web.UI.Page
{
    public B_Adbuy B_A = new B_Adbuy();
    public B_User buser = new B_User();
    public M_Adbuy M_A = new M_Adbuy();
    DataTable dt = new DataTable();
    //private string m_filetyle;
    //private string m_showpath;
    protected void Page_Load(object sender, EventArgs e)
    {
        int AdId = Convert.ToInt32(Request.QueryString["id"]);
        if (!IsPostBack)
        {
            DataTable dts = B_ADZone.ADZone_ID();
            dt = B_A.SelectAdbuy();
            if (dts != null && dts.Rows.Count > 0)
            {
                this.nocontent.Visible = false;
                this.ADID.SelectedIndex = 0;
                if (Request["id"] == null)
                {
                    this.Button2.Visible = false; 
                }
                else
                {
                    this.Button2.Visible = true; 
                    this.Label1.Visible = false;
                    this.Label2.Visible = true;
                    M_A = B_A.SelectId(AdId);

                    this.AddMain.Visible = true;
                    this.nocontent.Visible = false;
                    this.ADID.SelectedValue = M_A.ADID.ToString();
                    this.ShowTime.Text = M_A.ShowTime.ToString();
                    this.Scale.SelectedValue = M_A.Scale.ToString();
                    this.Ptime.Text = M_A.Ptime.ToString("yyyy-MM-dd");
                    this.Content.Text = M_A.Content.ToString();
                    this.txt.Text = M_A.Files.ToString();
                    this.Price.Text = M_A.Price.ToString("0.00");
                }
            }
            else
            {
                Button2.Visible = false;
                this.nocontent.Visible = true;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_Adbuy AdPr = new M_Adbuy();
        //try
        //{
        if (this.ShowTime.Text == "广告投放时间数（单位：天）")
        {
            Response.Write("<script>alert('请输入购买天数');</script>");
        }
        else if (this.Ptime.Text == "")
        {
            Response.Write("<script>alert('请输入投放时间');</script>");
        }
        else if (this.Price.Text == "")
        {
            Response.Write("<script>alert('请输入计划费用');</script>");
        }
        else
        {
            AdPr.ADID = Convert.ToInt32(Request.Form["ADID"]);
            AdPr.UID = buser.GetLogin().UserID;
            AdPr.Time = DateTime.Now.Date;
            AdPr.ShowTime = Convert.ToInt32(this.ShowTime.Text);
            AdPr.Scale = Convert.ToInt32(this.Scale.SelectedValue);
            AdPr.Ptime = Convert.ToDateTime(this.Ptime.Text);
            AdPr.Content = Server.HtmlEncode(this.Content.Text.ToString());
            AdPr.Files = this.txt.Text;
            AdPr.Price = Convert.ToDecimal(this.Price.Text);
          //  AdPr.Audit = false;
            B_A.Add(AdPr);
            Response.Write("<script>alert('恭喜，您的广告计划提交成功，请尽快付款完成购买！');window.location.href='AdPlan.aspx';</script>");
        }
        //}
        //catch
        //{
        //    //Response.Write("<script>alert('请选择申请人ID');Location.href='';</script>");
        //    AdPr.ADID = 1;
        //}
        //finally
        //{
        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('您的内容已经成功的添加了');window.location.href='GuanGaoJiHua.aspx';</script>");
        //}
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        /* if (txtpic.HasFile)
         {
             string fname = ifram1
             int fi = fname.LastIndexOf("\\") + 1;
             string filename = fname.Substring(fi);
             //txtpic.PostedFile.SaveAs(Server.MapPath("upload\\" + filename));
             Response.Write("<script>alert('成功上传的文件！');</script>");
         }
         else
         {
             Response.Write("<script>alert('请选择您要上传的文件！');</script>");
         }
        if (txtpic.HasFile)
        {
            string iform = txtpic.FileName;
            int fi = iform.LastIndexOf("\\") + 1;
            string filename = iform.Substring(fi);
            //txtpic.PostedFile.SaveAs(Server.MapPath("upload\\" + filename));
            //txt.Text = filename;
            Response.Write("<script>alert('成功上传的文件！');</script>");
            //iform = "";
        }
        else
        {
            Response.Write("<script>alert('请选择您要上传的文件！');</script>");
        }*/
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            int AdId = Convert.ToInt32(Request.QueryString["id"]);
            M_A = B_A.SelectId(AdId);
            M_A.UID = buser.GetLogin().UserID;
            M_A.ADID = Convert.ToInt32(Request["ADID"]);
            M_A.ShowTime = Convert.ToInt32(Request["ShowTime"]);
            M_A.Scale = Convert.ToInt32(Request["Scale"]);
            M_A.Ptime = Convert.ToDateTime(Request["Ptime"]);
            M_A.Price = Convert.ToDecimal(Request["Price"]);
            M_A.Content = Request["Content"];
            //if (this.txt.ReadOnly == true)
            //{

            //}
            //else
            //{
            M_A.Files = Request["txt"];
            //}
            B_A.Update(M_A);
            Response.Write("<script>alert('修改成功！');window.location.href='AdPlan.aspx';</script>");
        }
        catch
        {
        }
    }
}