using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
public partial class manage_Exam_AddEnroll : CustomerPageAction
{
    protected B_EnrollList ell = new B_EnrollList();
    protected B_Recruitment rll = new B_Recruitment();
    protected void Page_Load(object sender, EventArgs e)
    {
       DataTable techinfo = rll.GetRecruintmentallTech();
       if (techinfo != null && techinfo.Rows.Count > 0)
       {
           for (int i = 0; i < techinfo.Rows.Count; i++)
           {
               EnrollUser.Items.Add(new ListItem(techinfo.Rows[i]["TrueName"].ToString() == "" ? techinfo.Rows[i]["UserName"].ToString() : techinfo.Rows[i]["TrueName"].ToString(), techinfo.Rows[i]["UserID"].ToString()));
           }
       }
       EnrollUser.Items.Insert(0, "选择招生人员");
       if (!IsPostBack)
       {
           Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='ApplicationManage.aspx'>培训资料库</a></li><li><a href='ApplicationManage.aspx'>招生信息</a></li><li>添加招生信息</li>");
       }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_EnrollList enlist = new M_EnrollList();
        enlist.UesrID = DataConverter.CLng(this.EnrollUser.SelectedValue);
        enlist.CreateTime = DateTime.Now;        
        enlist.infos = this.txt_infos.Text;
        ell.GetInsert(enlist);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('添加成功！');location.href='ApplicationManage.aspx';</script>");
    }
}