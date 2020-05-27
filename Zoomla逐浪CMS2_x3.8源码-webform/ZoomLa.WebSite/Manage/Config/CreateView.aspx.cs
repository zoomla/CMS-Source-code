using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Collections.Generic;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class manage_Config_CreateView : CustomerPageAction
{
    public DataTable dt = new DataTable();
    string str = "";
    string strView = "";
    //string strWhere = "";
    string strTName = "";
    bool status = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DataSearch.aspx'>扩展功能</a></li><li><a href='DatalistProfile.aspx'>开发者中心</a></li><li class=\"active\"><a href='CreateView.aspx'>创建视图</a></li>");
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select TABLE_NAME from information_schema.tables order by TABLE_NAME", null);
            ListBox1.DataSource = dt;
            ListBox1.DataValueField = "TABLE_NAME";
            ListBox1.DataBind();
            //BindR2();
            if (!string.IsNullOrEmpty(Request.QueryString["ViewName"]))
            {
                string ViewName = Request.QueryString["ViewName"];
                SqlParameter para = new SqlParameter("@ViewName", ViewName);
                DataTable dtView = SqlHelper.ExecuteTable(CommandType.Text, "select TABLE_NAME,VIEW_DEFINITION  from information_schema.views where TABLE_NAME=@ViewName", para);//获取视图的名字和定义语句
                if (dtView.Rows.Count == 1)
                {
                    //lbBan.InnerText = "修改视图";
                    lbTag.InnerText = "修改视图";
                    CheckName.Visible = false;
                    txtVName.Disabled = true;
                    txtVName.Value = ViewName.Substring(5);
                    DataTable dtVT = SqlHelper.ExecuteTable(CommandType.Text, "select object_name(depid) as tableName from sys.sysdepends where object_name(id) = @ViewName group by object_name(depid)", para);//获取视图所有的基表名
                    Repeater1.DataSource = dtVT;
                    Repeater1.DataBind();
                    taSQL.Value = dtView.Rows[0]["VIEW_DEFINITION"].ToString();
                    for (int i = 0; i < ListBox1.Items.Count; i++)
                    {
                        for (int j = 0; j < dtVT.Rows.Count; j++)
                        {
                            if (ListBox1.Items[i].Value == dtVT.Rows[j]["tableName"].ToString())
                            {
                                ListBox1.Items[i].Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('信息有误！');window.location('CreateView.aspx');</script>");
                }
            }
        }
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        BindTable();
    }
    protected void BindTable()
    {
        dt.Columns.Add("tableName");
        //ListBox2.Items.Clear();
        foreach (ListItem li in ListBox1.Items)
        {
            if (li.Selected == true)
            {
                //ListBox2.Items.Add(li);
                DataRow newRow = dt.NewRow();
                newRow["tableName"] = li.Text;
                dt.Rows.Add(newRow);
            }
        }
        Repeater1.DataSource = dt;
        Repeater1.DataBind();
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ListBox lb = (ListBox)e.Item.FindControl("ListBox3");
        HtmlInputHidden tN = (HtmlInputHidden)e.Item.FindControl("tN");
        DataTable dt1 = SqlHelper.ExecuteTable(CommandType.Text, "select a.name from syscolumns a inner join sysobjects b on a.id=b.id where b.name='" + tN.Value + "' order by a.name", null);
        lb.DataSource = dt1;
        lb.DataValueField = "name";
        lb.DataBind();
        if (!string.IsNullOrEmpty(Request.QueryString["ViewName"]))
        {
            string ViewName = Request.QueryString["ViewName"];
            SqlParameter para = new SqlParameter("@ViewName", ViewName);
            DataTable dt3 = SqlHelper.ExecuteTable(CommandType.Text, "select v.[name] as viewname, s.[name] as [schema] from sys.views as v,sys.schemas as s where v.schema_id = s.schema_id and s.[name] = 'dbo' and v.[name] = @ViewName ", para);
        }
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            HtmlInputHidden tN = (HtmlInputHidden)e.Item.FindControl("tN");
            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                if (ListBox1.Items[i].Text == tN.Value)
                {
                    ListBox1.Items[i].Selected = false;
                }
            }
            BindTable();
            e.Item.Visible = false;
        }
    }
    protected void BtnSQL_Click(object sender, EventArgs e)
    {
        CreateSql();
        taSQL.InnerText = str;
    }
    private int CheckView(string ViewName)
    {
        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select TABLE_NAME from information_schema.views where TABLE_NAME='" + ViewName + "'", null);

        return dt.Rows.Count;
    }
    protected void CheckName_Click(object sender, EventArgs e)
    {
        if (txtVName.Value == "")
        {
            lbCheck.InnerHtml = "<font style='color:red'>视图名不允许为空！</font>";
            lbCheck.Visible = true;
        }
        else if (CheckView("ZL_V_" + txtVName.Value) == 0)
        {
            lbCheck.InnerHtml = "<font style='color:green'>此视图名可以使用</font>";
            lbCheck.Visible = true;
        }
        else
        {
            lbCheck.InnerHtml = "<font style='color:red'>已存在此视图名，请重新命名！</font>";
            lbCheck.Visible = true;
        }
    }
    protected void BtnSub_Click(object sender, EventArgs e)
    {
        CreateSql();
        if (lbCheck.Visible == true || strView.Trim() == "" || str == "" || status == false)
        {
            return;
        }
        taSQL.InnerText = str;
        string connStr = SqlHelper.ConnectionString;
        SqlConnection myConn = new SqlConnection(connStr);
        System.Data.SqlClient.SqlCommand myCommand = new SqlCommand(str, myConn);
        myConn.Open();
        try
        {
            myCommand.ExecuteNonQuery();
        }
        catch
        {
            if (string.IsNullOrEmpty(Request.QueryString["ViewName"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('创建视图" + "ZL_V_" + txtVName.Value + "失败，请检查您输入的数据！');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改视图" + "ZL_V_" + txtVName.Value + "失败，请检查您输入的数据！');</script>");
            }
        }
        myConn.Close();
        if (CheckView("ZL_V_" + txtVName.Value.Trim()) == 1)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ViewName"]))
            {
            }
            string[] fields = strView.Split(',');
            //向ZL_View表插入数据
            foreach (string field in fields)
            {
                string s = field.Substring(0, field.Length).Split('.')[1];
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@ViewName","ZL_V_" + txtVName.Value),
                new SqlParameter("@FieldName",s),
                new SqlParameter("@TableName",field.Substring(0, field.Length).Split('.')[0])
                };
            }
            if (string.IsNullOrEmpty(Request.QueryString["ViewName"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('创建视图" + "ZL_V_" + txtVName.Value + "成功！');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改视图" + "ZL_V_" + txtVName.Value + "成功！');</script>");
            }

        }
        else
        {
            if (string.IsNullOrEmpty(Request.QueryString["ViewName"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('创建视图" + "ZL_V_" + txtVName.Value + "失败！');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改视图" + "ZL_V_" + txtVName.Value + "失败！');</script>");
            }
        }
    }
    protected void CreateSql()
    {
        if (txtVName.Value == "")
        {
            lbCheck.InnerHtml = "<font style='color:red'>视图名不允许为空！</font>";
            lbCheck.Visible = true;
            return;
        }
        if (string.IsNullOrEmpty(Request.QueryString["ViewName"]))
        {
            if (CheckView("ZL_V_" + txtVName.Value) != 0)
            {
                lbCheck.InnerHtml = "<font style='color:red'>已存在此视图名，请重新命名！</font>";
                lbCheck.Visible = true;
                return;
            }
        }
        lbCheck.Visible = false;
        if (string.IsNullOrEmpty(ListBox1.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请至少选择一个表！');</script>");
            return;
        }
        foreach (RepeaterItem RI in Repeater1.Items)
        {
            if (RI.Visible == true)
            {
                ListBox lb = (ListBox)RI.FindControl("ListBox3");
                HtmlInputHidden tN = (HtmlInputHidden)RI.FindControl("tN");
                strTName = strTName + tN.Value + ",";
                string[] checkStr = null;
                for (int i = 0; i < lb.Items.Count; i++)
                {
                    if (lb.Items[i].Selected == true)
                    {
                        if (!string.IsNullOrEmpty(strView))
                        {
                            checkStr = strView.Substring(0, strView.Length - 1).Split(',');
                            //Response.Write("<script>alert('"+checkStr.Length.ToString()+"');</script>");
                            foreach (string s in checkStr)
                            {
                                string s1 = s.Split('.')[1];
                                //Response.Write("<script>alert('" + s1.ToLower() + "');</script>");
                                if (lb.Items[i].Text.ToLower() == s1.ToLower())
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"视图或函数中的列名必须唯一，在视图或函数 'ZL_V_" + tN.Value + "' 中多次指定了列名 '" + s1 + "'\");</script>");
                                    return;
                                }
                            }
                        }
                        strView = strView + tN.Value + "." + lb.Items[i].Text + ",";
                    }
                }
            }
        }

        if (strView.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请至少选择一个要显示的字段！');</script>");
            return;
        }
        strTName = strTName.Substring(0, strTName.Length - 1);
        strView = strView.Substring(0, strView.Length - 1);
        if (string.IsNullOrEmpty(Request.QueryString["ViewName"]))
        {
            str = "create view " + "ZL_V_" + txtVName.Value.Trim() + " as select " + strView + " from " + strTName;
        }
        else
        {
            str = "alter view " + "ZL_V_" + txtVName.Value.Trim() + " as select " + strView + " from " + strTName;
        }
        if (taWhere.Value.Trim() != "")
        {
            if (!Regex.IsMatch(taWhere.Value.Trim(), @"^\w+\.\w+\s*=\s*\w+\.\w+(\s+[a,A] [n,N][d,D]\s+\w+\.\w+\s*=\s*\w+\.\w+)*$"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('您输入的连接字符串格式有误！');</script>");
                str = "";
                return;
            }
            str = str + " where " + taWhere.Value.Trim();
        }
        status = true;
    }
}