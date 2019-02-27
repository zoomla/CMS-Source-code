using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class MIS_Approval_Default : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_MisApproval bll = new B_MisApproval();
    DataTable dt = new DataTable();
    M_MisApproval model = new M_MisApproval();
    M_MisType mt = new M_MisType();
    B_MisType Bt = new B_MisType();
    B_MisProcedure Bprocedure = new B_MisProcedure();
    B_MisProLevel BProLevel = new B_MisProLevel();
    string str = "";
    string dts = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin();
        if (!IsPostBack)
        {
            BindTypeList();
            BindDrpType();
            MyApplication.Visible = true;
            MyApps.Visible = false;
            SaveOn.Visible = false;
            SendTome.Visible = false;
            this.SetUpType.Visible = false;
            this.DpList.AutoPostBack = true;
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                str = Request.QueryString["type"].ToString();//Results
                //我的全部申请
                if (str == "1")
                {
                    MyApplication.Visible = true;
                    dts = "Inputer='" + buser.GetLogin().UserName + "'";
                }
                //我的全部审批
                else if (str == "2")
                {
                    this.tit.Text = "我的审批";
                    MyApplication.Visible = false;
                    MyApps.Visible = true;
                    dts = "Approver like '%" + buser.GetLogin().UserName + "%'";
                }
                //已归档
                else if (str == "3")
                {
                    this.tit.Text = "已归档";
                    MyApplication.Visible = false;
                    SaveOn.Visible = true;
                    dts = "Approver = '" + buser.GetLogin().UserName + "' and Results=99 Or Results=-1";
                    this.SaveOn.Visible = true;
                }
                else if (str == "4")
                {
                    this.tit.Text = "我的申请";
                    MyApplication.Visible = true;
                    dts = "Inputer='" + buser.GetLogin().UserName + "' and Results=1";
                }
                else if (str == "5")
                {
                    this.tit.Text = "我的申请";
                    MyApplication.Visible = true;
                    dts = "Inputer='" + buser.GetLogin().UserName + "'";
                }
                else if (str == "6")
                {
                    this.tit.Text = "我的审批";
                    MyApplication.Visible = false;
                    MyApps.Visible = true;
                    dts = "Approver Like '%" + buser.GetLogin().UserName + "%' and Results!=99 And Results!=-1";
                }
                else if (str == "7")
                {
                    this.pro_right.Visible = false;
                    this.SetUpType.Visible = true;
                    DataTable data = Bt.Sels();
                }
                else if (str == "8")
                {
                    this.tit.Text = "抄送给我";
                    MyApplication.Visible = false;
                    this.SendTome.Visible = true;
                    dt = bll.Sel(Send: buser.GetLogin().UserName);
                }
                else if (str == "9")
                {
                    this.tit.Text = "抄送给我";
                    MyApplication.Visible = false;
                    this.SendTome.Visible = true;
                    dt = bll.Sel(Send: buser.GetLogin().UserName, Results: 3);
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                Page_list(dt);
            }
            DataTable dt1 = bll.Sel(Inputer: buser.GetLogin().UserName);
            DataTable dt2 = bll.Sel(Inputer: buser.GetLogin().UserName, Results: 1);
            DataTable dt3 = bll.Sel(Inputer: buser.GetLogin().UserName, Results: 3);
            DataTable dt5 = bll.Sel(Approver: buser.GetLogin().UserName);
            DataTable dt6 = bll.Sel(Approver: buser.GetLogin().UserName, Results: 1);
            DataTable dt7 = bll.Sel(Approver: buser.GetLogin().UserName, Results: 2);
            DataTable dt8 = bll.Sel(Send: buser.GetLogin().UserName);
            DataTable dt9 = bll.Sel(Send: buser.GetLogin().UserName, Results: 3);
            if (dt1 != null)
            {
                this.lblAllOf.Text = dt1.Rows.Count.ToString();
            }
            else
            {
                this.lblAllOf.Text = "0";
            }
            if (dt2 != null)
            {
                this.lblInApps.Text = dt2.Rows.Count.ToString();
            }
            else
            {
                this.lblInApps.Text = "0";
            }
            if (dt3 != null)
            {
                this.lblonApps.Text = dt3.Rows.Count.ToString();
            }
            else
            {
                this.lblonApps.Text = "0";
            }
            if (dt5 != null)
            {
                this.lblAlls.Text = dt5.Rows.Count.ToString();
            }
            else
            {
                this.lblAlls.Text = "0";
            }
            if (dt6 != null)
            {
                this.lblNosave.Text = dt6.Rows.Count.ToString();
            }
            else
            {
                this.lblNosave.Text = "0";
            }
            if (dt7 != null)
            {
                this.lalAllss.Text = dt7.Rows.Count.ToString();
            }
            else
            {
                this.lalAllss.Text = "0";
            }
            if (dt8 != null)
            {
                this.lblAllMe.Text = dt8.Rows.Count.ToString();
            }
            else
            {
                this.lblAllMe.Text = "0";
            }
            if (dt9 != null)
            {
                this.lblNoPass.Text = dt9.Rows.Count.ToString();
            }
            else
            {
                this.lblNoPass.Text = "0";
            }
        }
    }

    private void BindTypeList()
    {
        this.RepType.DataSource = Bt.Sels();
        this.RepType.DataBind();
        ReturnNum();
        this.DpList.DataSource = Bt.Sels();
        this.DpList.DataTextField = "TypeName";
        this.DpList.DataValueField = "ID";
        this.DpList.DataBind();
        this.DpList.Items.Insert(0, new ListItem("全部", "0"));
        this.RepProcedure.DataSource = Bprocedure.Sel();
        this.RepProcedure.DataBind();
    }

    private void BindDrpType()
    {
        this.DrpType.DataSource = Bt.Sels();
        this.DrpType.DataTextField = "TypeName";
        this.DrpType.DataValueField = "ID";
        this.DrpType.DataBind();
        this.DrpType.Items.Insert(0, new ListItem("全部", "0"));
        this.DrpType.SelectedIndex = 0;
    }

    protected string GetResults(int Rid)
    {
        if (Rid == 1)
        {
            return "<span style=\"color:#009c0f;\">审核中</span>";
        }
        else if (Rid == 2)
        {
            return "<span style=\"color:#FA0000;\">已同意</span>";
        }
        else
        {
            return "<span style=\"color:#FA0000;\">未通过</span>";
        }
    }

    protected void ReturnNum()
    {
        int j = -41;
        dt = Bt.Sels();
        foreach (RepeaterItem item in this.RepType.Items)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label lblnum = (Label)this.RepType.Items[i].FindControl("lblNum");
                lblnum.Text = j.ToString();
                j++;
            }
        }
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        RPT.DataSource = Cll;
        RPT.DataBind();
    }
    #endregion

    protected void RepType_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Delete":
                if (Bt.Del(id))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功');</script>");
                    BindTypeList();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除失败');</script>");
                }
                Response.Redirect(Request.Url.AbsolutePath);
                break;
            default:
                break;
        }
    }


    protected void BtnComment_Click(object sender, EventArgs e)
    {
        int id = 0;
        if (string.IsNullOrEmpty(this.HidCommTxt.Value))
        {
            id = 0;
        }
        else
        {
            id = Convert.ToInt32(this.HidCommTxt.Value);
        }
        if (id == 0)
        {
            if (!string.IsNullOrEmpty(this.TxtComment.Text.ToString()))
            {
                mt.TypeName = this.TxtComment.Text;
                if (Bt.insert(mt) > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加成功');</script>");
                    BindTypeList();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加失败');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入类型名');</script>");
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(this.TxtComment.Text.ToString()))
            {
                int ID = Convert.ToInt32(this.HidCommTxt.Value);
                string Name = this.TxtComment.Text;
            }
        }
    }
    protected void DpList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DpList.SelectedValue == "0")
        {
            this.RepProcedure.DataSource = Bprocedure.Sel();
            this.RepProcedure.DataBind();
        }
        else
        {
            this.RepProcedure.DataSource = Bprocedure.Sel("TypeID=" + Convert.ToInt32(this.DpList.SelectedValue), "ID");
            this.RepProcedure.DataBind();
        }
    }
    protected void DrpType_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}