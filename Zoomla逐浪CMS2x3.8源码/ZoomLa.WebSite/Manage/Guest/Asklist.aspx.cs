using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
public partial class Manage_I_Guest_Asklist : CustomerPageAction
{
    private B_Content bll = new B_Content();
    private B_User buser = new B_User();
    //private int m_type;
    private B_Role RLL = new B_Role();
    B_NodeRole bNr = new B_NodeRole();
    B_Admin badmin = new B_Admin();
    private B_Node bNode = new B_Node();
    protected int NodeID;
    B_Ask b_ask = new B_Ask();
    M_Ask m_ask = new M_Ask();
    M_GuestAnswer m_guestanswer = new M_GuestAnswer();
    B_GuestAnswer b_guestanswer = new B_GuestAnswer();
    protected PagedDataSource pds = new PagedDataSource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_ARoleAuth.Check(ZLEnum.Auth.content, "ComentManage")) { function.WriteErrMsg("没有权限进行此项操作"); }
        if (function.isAjax(Request))
        {
            int flag = 0;
            if (Request.Form["action"] == "add")
            {
                flag = replyBut_Click() ? 1 : 2;
            }
            else if (Request.Form["action"] == "update")
            {
                flag = replyEdit_Click() ? 1 : 2;
            }
            Response.Write(flag);Response.Flush();Response.End();
        }
        if (!this.IsPostBack && !string.IsNullOrEmpty(Request["ID"]))
        {
            int id = Int32.Parse(Request["ID"]);
            m_ask = b_ask.SelReturnModel(id);
            askName.Text += m_ask.UserName;
            askTime.Text += m_ask.AddTime.ToString("yyyy年MM月dd日 HH:mm");
            askContent.Text = m_ask.Qcontent;
            this.ViewState["cType"] = "";//this.m_type.ToString();
            DataBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='WdCheck.aspx'>百科问答</a></li><li class='active'>问题回复</li>");
        }
    }
    public void DataBind(string key = "")
    {
        DataTable dt = b_guestanswer.GetAnswersByQid(Convert.ToInt32(Request.QueryString["ID"]));
        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            switch (Request.QueryString["type"])
            {
                case "1":
                    dt.DefaultView.RowFilter = "Audit=0";
                    break;
                case "2":
                    dt.DefaultView.RowFilter = "Audit=1";
                    break;
                default:
                    break;
            }
            dt = dt.DefaultView.ToTable();
        }
        Egv.DataSource = dt;
        Egv.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
    public string getcommend(object aa)
    {
        string aaa = aa.ToString();
        switch (aaa)
        {
            case "1":
                return "已审核";

            case "0":
                return "待审核";
            //case "2":
            //    return "审核不合格";
            //case "3":
            //    return "新版本待审";
            default:
                return "待审核";
        }
    }
    /// <summary>
    /// 绑定的行
    /// </summary>
    protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";
        //}
    }
    protected void DelBtn_Click(object sender, EventArgs e)
    {
        string[] id = Request.Form["idchk"].Split(',');
        foreach (string itemID in id)
        {
          b_guestanswer.Del(" ID=" + itemID);
        }
        DataBind();
    }
    protected void BtnSubmit2_Click(object sender, EventArgs e)
    {
        // 审核通过选定的问题
        string[] id = Request.Form["idchk"].Split(',');
        foreach (string itemID in id)
        {
            this.b_guestanswer.Update("status=1", "ID=" + itemID);
        }
        DataBind();
    }
    protected void BtnSubmit3_Click(object sender, EventArgs e)
    {
        //取消审核选定的词条
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                b_guestanswer.UpdateStatus(0, itemID);
            }
        }
        DataBind();
    }
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {
        int Id = DataConvert.CLng(e.CommandArgument.ToString());
        if (e.CommandName == "Audit")
        {
            m_guestanswer = b_guestanswer.SelReturnModel(Id);
            if (m_guestanswer.Status==0)
            {
                b_guestanswer.Update(" Status = 1", " ID=" + Id);
            }
            else
            {
                b_guestanswer.Update(" Status = 0", " ID=" + Id);
            }
        }
        else if (e.CommandName == "Del")
        {
            b_guestanswer.Del(" ID=" + Id);
            DataTable dt = b_guestanswer.Sel(" QueId=" + Id, "",null);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    b_guestanswer.Del(" ID=" + ID);
                }

            }
        }
        DataBind();
    }

    protected string gettypes(string id)
    {
        SqlParameter[] sp = new SqlParameter[] {new SqlParameter("id",id) };
        DataTable dt = b_ask.Sel(" ID=@id", "",sp);
        if (dt.Rows.Count > 0)
        {
            string typeid = (dt.Rows[0]["QueType"]).ToString();

            DataTable dts = Sql.Sel("zl_grade", " GradeID=" + typeid, "");
            if (dts.Rows.Count > 0)
            {
                string name;
                name = (dts.Rows[0]["GradeName"]).ToString();
                return name;
            }
            else return "Empty";
        }
        else return "Empty";
    }
    //添加
    protected bool replyBut_Click()
    {
        if (string.IsNullOrEmpty(Request.Form["id"]) || string.IsNullOrEmpty(Request.Form["content"])) return false;
        int id = DataConvert.CLng(Request.Form["id"]);//问题ID
        string content = Request.Form["content"];
        //回复并添加留言，默认为已审核 
        m_guestanswer.UserId = buser.GetLogin().UserID;
        m_guestanswer.Content = content;
        m_guestanswer.QueId = id;
        m_guestanswer.AddTime = DateTime.Now;
        m_guestanswer.Status = 0;
        m_guestanswer.UserName = badmin.GetAdminLogin().AdminName;
        m_guestanswer.supplymentid = 0;
        m_guestanswer.audit = 1;//审核否
        b_guestanswer.insert(m_guestanswer);
        return true;
    }
    //更新
    protected bool replyEdit_Click()
    {
        if (string.IsNullOrEmpty(Request.Form["id"]) || string.IsNullOrEmpty(Request.Form["content"])) return false;
        int id = DataConvert.CLng(Request.Form["id"]);//回复ID
        string content = Request.Form["content"];

        m_guestanswer = b_guestanswer.SelReturnModel(id);
        m_guestanswer.Content = content;
        b_guestanswer.UpdateByID(m_guestanswer);
        return true;
    }

    #region(服务端添加更新(Disuse))
    //服务端添加
    protected void replyBut_Click(object sender, EventArgs e)
    {
        //回复并添加留言，默认为已审核 
        m_guestanswer.UserId = buser.GetLogin().UserID;
        m_guestanswer.Content = txtContent.Value;
        m_guestanswer.QueId = DataConverter.CLng(Request["ID"]);
        m_guestanswer.AddTime = DateTime.Now;
        m_guestanswer.Status = 0;
        m_guestanswer.UserName = badmin.GetAdminLogin().AdminName;
        m_guestanswer.supplymentid = 0;
        m_guestanswer.audit = 1;//审核否
        b_guestanswer.insert(m_guestanswer);
        this.Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('回复成功!!');location=location;", true);
    }
    //服务端更新(Diuse)
    protected void replyEdit_Click(object sender, EventArgs e)
    {
        m_guestanswer = ViewState["reply"] as M_GuestAnswer;
        m_guestanswer.Content = txtContent.Value;
        b_guestanswer.UpdateByID(m_guestanswer);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');location=location;", true);
    }
    #endregion
    private string[] GetChecked()
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            string[] chkArr = Request.Form["idchk"].Split(',');
            return chkArr;
        }
        else
            return null;
    }
}