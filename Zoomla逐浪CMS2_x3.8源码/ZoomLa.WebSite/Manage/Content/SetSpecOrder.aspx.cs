using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class Manage_Content_SetSpecOrder : System.Web.UI.Page
{
    B_Spec specBll = new B_Spec();
    public int Pid { get { return DataConverter.CLng(Request.QueryString["pid"]); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li>系统设置</li><li>专题管理</li>");
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        RPT.DataSource = specBll.GetSpecList(Pid);
        RPT.DataBind();
    }

    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["SpecIDValue"]))
        {
            string[] SpecValues = Request.Form["SpecIDValue"].Split(',');
            for (int i = 0; i < SpecValues.Length; i++)
            {
                string nodeorder = Request.Form["OrderField" + SpecValues[i]];
                M_Spec specmod = specBll.SelReturnModel(DataConverter.CLng(SpecValues[i]));
                specmod.OrderID = DataConverter.CLng(nodeorder);
                specBll.UpdateByID(specmod);
            }
        }
        MyBind();
        function.Script(this,"parent.window.location=parent.location;");
    }

    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = DataConverter.CLng(e.CommandArgument);
        switch (e.CommandName)
        {
            case "UpMove":
                MoveSpec(id, true);
                break;
            case "DownMove":
                MoveSpec(id, false);
                break;
            default:
                break;
        }
    }
    public void MoveSpec(int id,bool isup)
    {
        string[] SpecValues = Request.Form["SpecIDValue"].Split(',');
        M_Spec specmod = specBll.SelReturnModel(id);
        for (int i = 0; i < SpecValues.Length; i++)
        {
            if (SpecValues[i].Equals(id.ToString()))
            {
                if ((isup && i - 1 < 0)||(!isup && i + 1 >= SpecValues.Length)) { break; }//上移下移判断
                int index = isup ? i - 1 : i + 1;
                M_Spec targetmod = specBll.SelReturnModel(DataConverter.CLng(SpecValues[index]));
                int nodeorder =DataConverter.CLng(Request.Form["OrderField" + SpecValues[index]]);
                targetmod.OrderID = specmod.OrderID;
                specmod.OrderID = nodeorder;
                specBll.UpdateByID(specmod);
                specBll.UpdateByID(targetmod);
                break;
            }
        }
        MyBind();
    }
}