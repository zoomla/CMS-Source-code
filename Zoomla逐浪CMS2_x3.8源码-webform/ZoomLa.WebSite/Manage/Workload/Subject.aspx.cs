using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.SQLDAL;
using System.Data;
using System.Text;

public partial class manage_Workload_Subject : CustomerPageAction
{
    private B_Node b_Node = new B_Node();
    private B_Content b_Content = new B_Content();
    private B_Admin b_Admin = new B_Admin();
    private B_User b_User = new B_User();
    protected B_Model bll = new B_Model();
    B_Content_Count countBll = new B_Content_Count();
    string strHtml = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindListBox();
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li class='active'>工作统计</li>" + Call.GetHelp(52));
        }
    }
    protected void BindListBox()
    {
        DataTable dt = new DataTable();
        dt = b_Node.GetNodeListContainXML(0);
        NodeList.DataSource = dt;
        NodeList.DataTextField = "NodeName";
        NodeList.DataValueField = "NodeID";
        NodeList.DataBind();
        NodeList.Items.Insert(0, new ListItem("所有栏目", ""));
        NodeList.SelectedValue = "NodeID";

        DataTable Modeldt = bll.GetModel("'" + bll.GetModelType(Convert.ToInt32("1")) + "'", "");
        Modeldt.DefaultView.Sort = "ModelID ASC";
        ModelList.DataSource = Modeldt;
        ModelList.DataTextField = "ModelName";
        ModelList.DataValueField = "ModelID";
        ModelList.DataBind();
        ModelList.Items.Insert(0, new ListItem("所有模型", ""));

        DataView dv = new DataView();
        DataView roledt = B_Role.GetRoleInfo();     //管理员角色
        AdminRole.DataSource = roledt;
        AdminRole.DataBind();
    }
    public void MyBind()
    {
        DataTable dt = SelDT();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    private DataTable SelDT() 
    {
        string sdate = string.IsNullOrEmpty(start_T.Text) ? "" : DateTime.Parse(start_T.Text).ToString();
        string edate = string.IsNullOrEmpty(end_T.Text) ? "" : DateTime.Parse(end_T.Text).ToString();
        //只允许已有会员
        DataTable admindt = SqlHelper.ExecuteTable(CommandType.Text, "SELECT AdminName FROM ZL_Manager");
        string names = "";
        foreach (DataRow dr in admindt.Rows)
        {
            names += "'" + dr["AdminName"] + "',";
        }
        names = names.Trim(',');
        DataTable dt = countBll.SelGroupByDate(DataConvert.CLng(NodeList.SelectedValue), DataConvert.CLng(ModelList.SelectedValue), sdate, edate, names);
        return dt;
    }
    protected void count_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    public void RunSql(string con, string adminName)//输出详细,连接，用户名
    {
        StringBuilder sb = new StringBuilder();
        string sql = string.Empty;
        DataTable zhi = new DataTable();
        zhi = b_Content.CountData("manager", adminName);
        strHtml += "<table id='tb' class='table table-striped table-bordered table-hover'>";
        if (zhi != null && zhi.Rows.Count > 0)
        {
            strHtml += "<tr class='gridtitle' height='25'>";
            foreach (DataColumn dc in zhi.Columns)
            {
                strHtml += "<th>" + dc.ColumnName + "</th>";
            }
            strHtml += "</tr>";
            foreach (DataRow row in zhi.Select())
            {
                strHtml += "<tr class='tdbg' align='center' height='25' onmouseover=\"this.className='tdbgmouseover'\" onmouseout=\"this.className='tdbg'\">";
                try
                {
                    int i = 0;
                    if (row[i].ToString() == "")
                    {
                        strHtml += "<td width='10%'>&nbsp;</td>";
                    }
                    else
                    {
                        strHtml += "<td width='10%'>" + row[i++] + "</td>";
                        strHtml += "<td width='10%'>" + row[i++] + "</td>";
                        strHtml += "<td width='80%' align='left'>" + row[i] + "</td>";
                    }

                }
                catch
                {
                }
                strHtml += "</tr>";
            }
            if (Request["Type"] == "manager")
            {
                B_Content sd_CommonModel = new B_Content();
                switch (Request["Type"])
                {
                    case "user":
                        //zhi = sd_CommonModel.getNodeName(this.UserName.Text);
                        break;
                    case "manager":
                        //zhi = sd_CommonModel.getNodeName(adminName);
                        break;
                }
                foreach (DataRow row in zhi.Rows)
                {
                    strHtml += "<tr class='tdbg' align='center' onmouseover=\"this.className='tdbgmouseover'\" onmouseout=\"this.className='tdbg'\">";
                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (row[i].ToString() == "")
                            {
                                strHtml += "<td width='10%'>&nbsp;</td>";
                            }
                            else
                            {
                                if (i % 3 == 2)
                                    strHtml += "<td width='10%' align='left'> " + row[i] + " 篇" + "</td>";
                                else
                                    strHtml += "<td width='10%' align='center'>" + row[i] + "</td>";
                            }
                        }
                    }
                    catch
                    {
                    }
                    strHtml += "</tr>";
                }
            }
            strHtml += "</table>";
        }
    }
    public void RunSql(string con)
    {
        StringBuilder sb = new StringBuilder();
        string sql = string.Empty;
        DataTable zhi = new DataTable();
        switch (Request["Type"])
        {
            case "subject":

                zhi = b_Content.CountData("subject", NodeList.SelectedValue);
                break;
            //case "user":
            //    if (b_User.IsExit(this.UserName.Text))
            //        zhi = b_Content.CountData("user", this.UserName.Text);
            //    else
            //    {
            //        this.UserName.Text = "";
            //        function.WriteErrMsg("不存在该用户");
            //    }
            //    break;
            case "manager":
                zhi = b_Content.CountData("manager", Request["adminName"]);
                break;
           
        }
        ///查询
        strHtml += "<table id='tb' class='table table-striped table-bordered table-hover'>";
        if (zhi != null && zhi.Rows.Count > 0)
        {
            strHtml += "<tr class='gridtitle' height='25'>";
            foreach (DataColumn dc in zhi.Columns)
            {
                strHtml += "<th>" + dc.ColumnName + "</th>";
            }
            strHtml += "</tr>";
            foreach (DataRow row in zhi.Select())
            {
                strHtml += "<tr class='tdbg' align='center' onmouseover=\"this.className='tdbgmouseover'\" onmouseout=\"this.className='tdbg'\" height='25'>";
                try
                {
                    for (int i = 0; i < 100; i++)
                    {
                        if (row[i].ToString() == "")
                        {
                            strHtml += "<td width='15%'>&nbsp;</td>";
                        }
                        else if (i < 2)
                        {
                            strHtml += "<td width='15%'>" + row[i] + "</td>";
                        }
                        else
                        {
                            strHtml += "<td width='70%' align='left'>" + row[i] + "</td>";
                        }
                    }
                }
                catch
                {
                }
                strHtml += "</tr>";
            }
            if (Request["Type"] == "manager")
            {
                B_Content sd_CommonModel = new B_Content();
                switch (Request["Type"])
                {
                    case "user":
                        //zhi = sd_CommonModel.getNodeName(this.UserName.Text);
                        break;
                    case "manager":
                        //zhi = sd_CommonModel.getNodeName(Request["LstNodes"]);
                        break;
                }
                foreach (DataRow row in zhi.Rows)
                {
                    strHtml += "<tr class='tdbg' align='center' height='25'>";
                    try
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (row[i].ToString() == "")
                            {
                                strHtml += "<td width='10%'>&nbsp;</td>";
                            }
                            else
                            {
                                if (i % 3 == 2)
                                    strHtml += "<td width='10%' align='center'> " + row[i] + " 篇" + "</td>";
                                else
                                    strHtml += "<td width='10%' align='center'>" + row[i] + "</td>";
                            }
                        }
                    }
                    catch
                    {
                    }
                    strHtml += "</tr>";
                }
                //if (sd_CommonModel.CountDatas("Inputer", this.UserName.Text).Rows[0]["countNum"].ToString() != "0")
                //{
                //    strHtml += "<td class='tdbg'>最近录入时间</td>";
                //    switch (Request["Type"])
                //    {
                //        case "user":
                //            strHtml += "<td class='tdbg'>" + sd_CommonModel.getMaxTime(this.UserName.Text) + "</td>";
                //            break;
                //        case "manager":
                //            strHtml += "<td class='tdbg'>" + sd_CommonModel.getMaxTime(Request["LstNodes"]) + "</td>";
                //            break;
                //    }
                //    strHtml += "<td class='tdbg'></td>";
                //}
            }
            strHtml += "</table>";
            //this.t2.InnerHtml = strHtml;
        }
        else
        {
        }
    }
    protected void displayAdmin()//输出管理员信息
    {
        DataTable dt = new DataTable();
        DataTable zhi = new DataTable();
        zhi.Columns.Add("ID");
        zhi.Columns.Add("管理员");
        zhi.Columns.Add("合计");
        dt = b_Admin.Sel();
        //将dt中的有用的数据给zhi,ID，用户名，总篇数
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            zhi.Rows.Add(new object[] { Int16.Parse(dt.Rows[i]["AdminID"].ToString()), dt.Rows[i]["AdminName"].ToString(), b_Content.CountDatas("Inputer", dt.Rows[i]["AdminName"].ToString()) });
        }
        strHtml += "<table id='tb' class='table table-striped table-bordered table-hover'>";
        strHtml += "<tr class='gridtitle' height='25'>";
        foreach (DataColumn dc in zhi.Columns)
        {
            strHtml += "<th>" + dc.ColumnName + "</th>";
        }
        strHtml += "</tr>";
        foreach (DataRow row in zhi.Select())
        {
            strHtml += "<tr class='tdbg' align='left' height='25' onmouseover=\"this.className='tdbgmouseover'\" onmouseout=\"this.className='tdbg'\">";
            try
            {
                int i = 0;
                if (row[i].ToString() == "")
                {
                    strHtml += "<td width='10%'>&nbsp;</td>";
                }
                strHtml += "<td width='5%' align='center'><a href='Subject.aspx?Type=manager&adminName=" + row[1].ToString() + "'>" + row[i++] + "</a></td>";

                strHtml += "<td width='10%' align='center'><a href='Subject.aspx?Type=manager&adminName=" + row[1].ToString() + "'>" + row[i++] + "</a></td>";

                strHtml += "<td width='85%'><a href='Subject.aspx?Type=manager&adminName=" + row[1].ToString() + "'>" + row[i] + "</a></td>";
            }

            catch
            {

            }

            strHtml += "</tr>";
        }
        strHtml += "</table>";
        t1.InnerHtml = strHtml;
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void Export_Click(object sender, EventArgs e)
    {
        OfficeHelper office = new OfficeHelper();
        SafeSC.DownStr(office.ExportExcel(SelDT(), "发稿量,评论量,点击数,编辑"), "统计.xls");
    }
}