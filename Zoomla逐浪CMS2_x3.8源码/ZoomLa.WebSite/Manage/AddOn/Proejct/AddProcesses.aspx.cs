using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;

public partial class manage_AddOn_AdvProcesses : System.Web.UI.Page
{

    //加载页面
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        int i = DataConverter.CLng(Request.QueryString["ID"]);
        //DataTable t = bps.SelectByProID(i);
        int line = 0;
        //foreach (DataRow r in t.Rows)
        //{
        //    line += Convert.ToInt32(r[4].ToString());
        //}
        line = 100 - line;
        RV.MaximumValue =line.ToString();
        Lbl.Text = line.ToString();
        lblText.Text = "查看/修改项目流程";
        int id = DataConverter.CLng(Request.QueryString["ID"]);
        //TxtProName.Text = bpj.GetSelect(id).Name;
        if (Request.QueryString["processesid"] != null)//读取数据
        {
            int processesid = DataConverter.CLng(Request.QueryString["processesid"]);
            //mps = bps.GetSelect(processesid);
            //TxtProcessesName.Text = mps.Name;
            //TxtProcessesInfo.Text = mps.Info;
            //TxtProgress.Text = mps.Progress.ToString();
            //TxtTime.Text = mps.CompleteTime.ToString();
            //TxtProName.Text = bpj.GetSelect(mps.OpjectID).Name;
        }
    }

    //修改，添加流程按钮事件
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["processesid"] != null)//修改
        {
            //mps.Name = Request.Form["TxtProcessesName"];
            //mps.Info = Request.Form["TxtProcessesInfo"];
            //mps.Progress = DataConverter.CLng(Request.Form["TxtProgress"]);
            //mps.CompleteTime = DataConverter.CDate(Request.Form["TxtTime"]);
            //bps.GetUpdate(mps);
            //function.WriteSuccessMsg("修改成功！", "ProjectsProcesses.aspx?ID="+mps.OpjectID);
        }
        else//添加
        {
            int id = DataConverter.CLng(Request.QueryString["ID"]);
            lblText.Text = "添加项目流程";
            //mps.Name = TxtProcessesName.Text.Trim();
            //mps.Info = TxtProcessesInfo.Text.Trim();
            //mps.Progress = DataConverter.CLng(TxtProgress.Text.Trim());
            //mps.CompleteTime = DataConverter.CDate(TxtTime.Text);
            //mps.OpjectID = id;
            //bps.GetInsert(mps);
            function.WriteSuccessMsg("添加成功！", "ProjectsProcesses.aspx?ID=" + id);
        }
    }

    //返回上一级
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["processesid"] != null)//通过编辑进
        {
            int processesid = DataConverter.CLng(Request.QueryString["processesid"]);
            //mps = bps.GetSelect(processesid);
            //Response.Redirect("ProjectsProcesses.aspx?ID=" + mps.OpjectID);
        }
        else//通过添加进
        {
            Response.Redirect("ProjectsProcesses.aspx?ID=" + Request.QueryString["ID"]);
        }
    }
}
