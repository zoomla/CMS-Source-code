using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.SQLDAL;

public partial class manage_Question_Question_Class_Manage : CustomerPageAction
{
    B_Exam_Class bqc=new B_Exam_Class();
    B_User buser = new B_User();
    public DataTable QuestDT
    {
        get
        {
            if (ViewState["QuestDT"] == null)
            {
                ViewState["QuestDT"] = bqc.Select_All();
            }
            return ViewState["QuestDT"] as DataTable;
        }
        set
        {
            ViewState["QuestDT"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!buser.IsTeach()) { function.WriteErrMsg("当前页面只有教师才可访问"); }
        if (function.isAjax() && Request.Form["want"] != null)
        {
            int groupID = DataConvert.CLng(Request.Form["gid"]);
            if (groupID == 0)
            {
                Response.Clear();
                Response.End();
            }
            string json = "";
            DataTable dt = bqc.GetSelectByC_ClassId(groupID);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("icon", typeof(string)));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dt.Rows[i]["icon"] = GetIcon(dr["C_ClassName"].ToString(), dr["C_id"].ToString(), dr["C_Classid"].ToString());
                }
                json = JsonHelper.JsonSerialDataTable(dt);
            }
            Response.Clear();
            Response.Write(json);
            Response.End();
            return;
        }
        if (Request.QueryString["menu"] != null && Request.QueryString["menu"] != "")
        {
            string menu = Request.QueryString["menu"].ToString();
            if (menu.Trim().Equals("delete"))
            {
                if (Request.QueryString["C_id"] != null && Request.QueryString["C_id"] !="")
                {
                    int c_id = DataConverter.CLng(Request.QueryString["C_id"]);
                    bqc = new B_Exam_Class();
                    bool result = bqc.DeleteByGroupID(c_id);
                    if (result)
                    {
                        Response.Redirect("Question_Class_Manage.aspx");
                    }
                    else
                    {
                        function.WriteErrMsg("删除失败!");
                    }
                }
            }
        }
        if (!IsPostBack)
        {
            DataBind();
        }
    }

    /// <summary>
    /// 树形模式数据邦定:repeater
    /// </summary>
    private new void DataBind()
    {

       DataTable dt = QuestDT;
       dt.DefaultView.RowFilter = "C_Classid=0";
        EGV.DataSource = dt;
        this.EGV.DataBind();
    }

    public string GetIcon()
    {
        int count = Convert.ToInt32(Eval("ChildCount"));
        return count > 0 ? "<span class='quest_icon fa fa-plus-square'></span>" : "";
    }
    public string GetIcon(string GroupName, string GroupID, string parentid)
    {
        string result = "";
        int depth = GetDepth(parentid);
        int pid = DataConvert.CLng(parentid);
        DataTable dt = QuestDT;
        DataView dv = dt.DefaultView;
        for (int i = 0; i < depth; i++)
        {
            result += "<img src='/Images/TreeLineImages/tree_line4.gif' border='0' width='19' height='20' />";
        }
        result += "<img src='/Images/TreeLineImages/t.gif' border='0' />";
        dv.RowFilter = "C_Classid=" + GroupID;
        if (dv.Count > 0)
        {
            result += "<span class='fa fa-folder' data-type='icon'></span> " + GroupName;
        }
        else
        {
            result += GroupName;
        }
        return result;
    }
    public int GetDepth(string ParentID)
    {
        int pid = DataConvert.CLng(ParentID);
        int depth = 0;
        DataTable dt = QuestDT;
        DataView dv = dt.DefaultView;
        for (int i = 0; pid > 0; i++)
        {
            dv.RowFilter = "C_id=" + pid;
            pid = DataConvert.CLng(dv.ToTable().Rows[0]["C_Classid"]);
            depth++;
        }
        return depth;
    }

    protected string ShoworHidden(string groupID, string parentID)
    {
        return "ondblclick=\"showlist(" + groupID + ");\" state='1'";
    }
    //数据行绑定事件
    protected void gvCard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //所属ID
            int c_Id = DataConverter.CLng((e.Row.FindControl("hfC_ClassId") as HiddenField).Value);
            bqc = new B_Exam_Class();
            Label lblC_ClassId = e.Row.FindControl("lblC_ClassId") as Label;
            M_Exam_Class mqc = new M_Exam_Class();
            mqc = bqc.GetSelect(c_Id);
            if (mqc != null)
            {
                lblC_ClassId.Text = mqc.C_ClassName;
            }
            if(c_Id == 0)
            {
                lblC_ClassId.Text = "无所属类别";
            }
        }
    }

    //命令行绑定事件
    protected void gvCard_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Trim().Equals("Del"))  //删除操作
        {
            int c_Id = DataConverter.CLng(e.CommandArgument.ToString());
            bqc = new B_Exam_Class();
            bool resu = bqc.DeleteByGroupID(c_Id);
            if (resu)
            {
                function.WriteSuccessMsg("删除成功!");
            }
            else
            {
                function.WriteErrMsg("删除失败!");
            }
        }
        if (e.CommandName.Trim().Equals("Upda")) //修改操作
        {
            int c_Id = DataConverter.CLng(e.CommandArgument.ToString());
            Response.Redirect("AddQuestion_Class.aspx?menu=Edit&C_id=" + c_Id);
        }
    }



    //项目绑定
    protected void GroupList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        
    }
    protected void GroupList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Item)
        {
            Label lblClassId = e.Item.FindControl("lblClassId") as Label;
            HiddenField hfClassId = e.Item.FindControl("hfClassId") as HiddenField;
            int class_Id =DataConverter.CLng(hfClassId.Value);
            bqc = new B_Exam_Class();
            M_Exam_Class mqc = bqc.GetSelect(class_Id);
            if (mqc != null)
            {
                lblClassId.Text = mqc.C_ClassName;
            }
            if (class_Id == 0)
            {
                //lblClassId.Text = "无所属类别";
            }
        }
    }


    /// <summary>
    /// 定义计数器
    /// </summary>
    protected int c = 0;
    /// <summary>
    /// 读取一级分类
    /// </summary>
    /// <param name="GroupID"></param>
    /// <returns></returns>
    protected string loadtree(string ClassId)
    {
        bqc = new B_Exam_Class();
        M_Exam_Class mqc = bqc.GetSelect(DataConverter.CLng(ClassId));
        if (mqc != null && mqc.C_id > 0)
        {
            string c_ClassName = mqc.C_ClassName;
            StringBuilder content = new StringBuilder();
            int classId = DataConverter.CLng(ClassId);
            DataTable bflist = bqc.GetSelectByC_ClassId(classId);
            if (bflist != null && bflist.Rows.Count > 0)
            {
                for (int i = 0; i < bflist.Rows.Count; i++)
                {
                    content.AppendLine("<tr class=\"tdbg\" style=\"width:100%\" >");
                    content.AppendLine("<td colspan=\"2\" align=\"left\" style=\"width:30%; height:26px\">");
                    content.AppendLine(bflist.Rows[i]["C_ClassName"].ToString());
                    content.AppendLine("</td>");
                    content.AppendLine("<td align=\"right\" style=\"width:70%\">");
                    content.AppendLine("<a href=\"AddQuestion_Class.aspx?menu=Add&C_id=" 
                        + bflist.Rows[i]["C_id"].ToString() + "\">新建分类</a>&nbsp;|&nbsp;<a href=\"AddQuestion_Class.aspx?menu=Edit&C_id=" + bflist.Rows[i]["C_id"].ToString() + "\">编辑分类</a>&nbsp;|&nbsp;<a href=\"?menu=delete&C_id="
                        + bflist.Rows[i]["C_id"].ToString() + "\" OnClick=\"return confirm('确认删除吗?');\">删除分类</a>");
                    content.AppendLine("</td>");
                    content.AppendLine("</tr>");
                    content = LoadClassID(bflist.Rows[i]["C_id"].ToString(), content);
                }
                return content.ToString();
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 遍历递归读取下级分类
    /// </summary>
    /// <param name="ForumID"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    protected StringBuilder LoadClassID(string ClassId, StringBuilder content)
    {
        bqc = new B_Exam_Class();
        c = c + 1;
        DataTable ClassList = bqc.GetSelectByC_ClassId(DataConverter.CLng(ClassId));
        if (ClassList != null && ClassList.Rows.Count>0)
        {
            foreach (DataRow dr in ClassList.Rows)
            {
                content.AppendLine("<tr class=\"tdbg\">");
                content.AppendLine("<td align=\"left\" colspan=\"2\" style=\"width:70%\">");
                content.AppendLine(new string('　', c * 2) + dr["C_ClassName"]);
                content.AppendLine("</td>");
                content.AppendLine("<td align=\"right\">");
                content.AppendLine("<a href=\"AddQuestion_Class.aspx?menu=Add&C_id=" + dr["C_id"].ToString() + 
                    "\">新建分类</a>&nbsp;|&nbsp;<a href=\"AddQuestion_Class.aspx?menu=Edit&C_id=" + dr["C_id"].ToString() +
                    "\">编辑分类</a>&nbsp;|&nbsp;<a href=\"?menu=delete&C_id=" + dr["C_id"].ToString() + 
                    "\" OnClick=\"return confirm('确认删除吗?');\">删除分类</a>");
                content.AppendLine("</td>");
                content.AppendLine("</tr>");
                content = LoadClassID(dr["C_id"].ToString(), content);//实现递归
            }
        }
        c = c - 1;
        return content;
    }

}
