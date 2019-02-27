using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ZoomLa.Common;

public partial class MIS_Approval_EditProcedure : System.Web.UI.Page
{
    B_MisType Bt = new B_MisType();
    M_MisType Mt = new M_MisType();
    DataTable dt = new DataTable();
    B_User buser = new B_User();
    M_MisProLevel MpLevel = new M_MisProLevel();
    M_MisProcedure Mprocedure = new M_MisProcedure();
    B_MisProLevel BpLevel = new B_MisProLevel();
    B_MisProcedure Bprocedure = new B_MisProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            string result = "0";
            switch (action)
            {
                case "del":
                    if (BpLevel.Del(DataConverter.CLng(value)))
                        result = "1";
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }

        int id = Convert.ToInt32(Request.QueryString["ProID"]);
        if (!IsPostBack)
        {
            BindDpList();
            SqlParameter[] sp = new SqlParameter[]
            { new SqlParameter("ID",id)};
            DataTable dtProcedure = Bprocedure.Sel("ID=@ID", "ID", sp);
            if (dtProcedure.Rows.Count > 0)
            {
                this.TxtName.Text = dtProcedure.Rows[0]["ProcedureName"].ToString();
                BindTable();
                SqlParameter[] sp2 = new SqlParameter[] { new SqlParameter("ID", Convert.ToInt32(dtProcedure.Rows[0]["TypeID"])) };
                string Name = Bt.Sel("ID=@ID", "ID", sp2).Rows[0]["TypeName"].ToString();
                this.DrpType.SelectedValue = Name;
            }
            else
            {
                this.TxtName.Text = "";
            }

        }
    }

    protected void BindDpList()
    {
        this.DrpType.DataSource = Bt.Sels();
        this.DrpType.DataTextField = "TypeName";
        this.DrpType.DataValueField = "ID";
        this.DrpType.DataBind();
    }

    protected void BindTable()
    {
        int id = Convert.ToInt32(Request.QueryString["ProID"]);
        if (function.IsNumeric(Request.QueryString["ProID"]))
        {
            dt = BpLevel.SelByProID(id);
            if (dt.Rows.Count > 0)
            {
                this.txtTRLastIndex.Value = (dt.Rows.Count + 1).ToString();
            }
            else
            {
                this.txtTRLastIndex.Value = "1";
            }
            this.RepLevel.DataSource = dt;
            this.RepLevel.DataBind();
        }
    }

    protected string GetUserName(object ids)
    {
        string result = "";
        if (string.IsNullOrEmpty(ids.ToString()))
        {
        }
        else
        {
            dt = buser.SelectUserByIds(ids.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["UserName"].ToString() + ",";
            }
        }
        return result.TrimEnd(',');
    }
    protected void BtnComment_Click(object sender, EventArgs e)
    {
        Mt.TypeName = this.TxtComment.Text.ToString();
        if (Bt.insert(Mt) > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功');</script>");
            BindDpList();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改失败');</script>");
            BindDpList();
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["ProID"]);
        if (Bprocedure.Del(id))
        {
            function.WriteSuccessMsg("删除成功", "Default.aspx?type=7");
        }
        else
        {
            function.WriteErrMsg("删除失败", "Default.aspx?type=7");
        }
    }

    protected string add_fields(string count)
    {
        string str = "";
        if (!string.IsNullOrEmpty(Request["txtName" + count]))
        {
            str = "" + Request["txtNum" + count] + "|";
            str += "" + Request["txtName" + count] + "|";
            str += "" + Request["lblPeson" + count] + "|";
            function.WriteErrMsg(str);
            if (!string.IsNullOrEmpty(Request["nul" + count]) && Request["nul" + count].ToString() == "1")
            {
                str += "NULL";
            }
            else
            {
                str += "NOT NULL";
            }
        }
        return str;

    }

    protected void BtnSub_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["ProID"]);
        if (function.IsNumeric(Request.QueryString["ProID"]))
        {
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("ProcedureName",this.TxtName.Text.Trim()),
                new SqlParameter("ID",id)
            };
            if (Bprocedure.Update("ProcedureName=@ProcedureName", "ID=@ID", sp))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改失败');</script>");
            }
        }
        else
        {
            Mprocedure.ProcedureName = this.TxtName.Text.ToString();
            int tid = Convert.ToInt32(this.DrpType.SelectedValue);
            Mprocedure.TypeID = Convert.ToInt32(Bt.Sel("ID='" + tid + "'", "ID").Rows[0]["ID"]);
            if (Bprocedure.insert(Mprocedure) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加成功');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加失败');</script>");
            }
        }
        foreach (RepeaterItem item in RepLevel.Items)//原有的Repeater中的数据
        {
            dt = BpLevel.SelByProID(id);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MpLevel.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                MpLevel.stepNum = Convert.ToInt32(dt.Rows[i]["Level"]);
                MpLevel.stepName = ((TextBox)RepLevel.Items[i].FindControl("TxtLevelName") as TextBox).Text;
                MpLevel.ProID = Convert.ToInt32(dt.Rows[i]["ProID"]);
                if (BpLevel.UpdateByID(MpLevel))
                {
                    function.WriteSuccessMsg("修改成功", "EditProcedure.aspx?ProID=" + id);
                }
                else
                {
                    function.WriteErrMsg("修改失败", "EditProcedure.aspx?ProID=" + id);
                }
            }
        }

        HiddenField1.Value = HiddenField1.Value.TrimEnd(',');
        if (!string.IsNullOrEmpty(HiddenField1.Value))//新增的Table
        {
            int rows = Convert.ToInt32(this.txtTRLastIndex.Value);
            string[] idsArr = HiddenField1.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (rows > 0)
            {
                foreach (string s in idsArr)
                {
                    if (idsArr.Length > 0 && !string.IsNullOrEmpty(Request.QueryString["ProID"]))
                    {

                        //strs1 = add_fields(s);//1|第1级审批|| 
                        //strArrs = strs1.Split('|');
                        MpLevel.stepNum = Convert.ToInt32(Request["txtNum" + s]);
                        MpLevel.stepName = Request["txtName" + s];
                        //MpLevel.UserID = Request.Form["txtPeson" + s].TrimEnd(',');
                        //MpLevel.Send = Request.Form["txtSend" + s].TrimEnd(',');
                        MpLevel.ProID = id;
                        if (BpLevel.insert(MpLevel) > 0)
                        {
                            function.WriteSuccessMsg("修改成功", "EditProcedure.aspx?ProID=" + id);
                        }
                        else
                        {
                            function.WriteErrMsg("修改失败", "EditProcedure.aspx?ProID=" + id);
                        }
                    }
                }
            }
            else
            {

            }
        }
    }

    protected void RepLevel_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Delete":
                if (BpLevel.Del(id))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除失败');</script>");
                }
                break;
        }
    }
    //(Disuse)
    protected void SubBtns_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["ProID"]);
        if (this.HidPid.Value != null)
        {
            //int pid = Convert.ToInt32(this.HidPid.Value);
            int pid = id;
            string UserName = this.TxtApprover.Value.ToString();
            string CutName = "";
            if (UserName.IndexOf(",") > 0)
            {
                CutName = UserName.Substring(0, UserName.IndexOf(","));
            }
            else
            {
                CutName = UserName;
            }

            DataTable dt = buser.GetUserName(CutName);
            int uid = Convert.ToInt32(dt.Rows[0]["UserID"]);
            foreach (RepeaterItem item in this.RepLevel.Items)
            {
                dt = BpLevel.SelByProID(pid);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label tb = (Label)this.RepLevel.Items[i].FindControl("lblApprover");
                    tb.Text = CutName;
                }
            }
            if (BpLevel.UpdateUid(uid, pid))
            {
                BindTable();
            }
            else
            {
                BindTable();
            }
        }
    }
}