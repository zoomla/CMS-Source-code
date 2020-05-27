using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class IPManage : CustomerPageAction
{
    B_IPOperation b_ipOperation = new B_IPOperation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["delete"] == "yes")
        {
            b_ipOperation.deleteClass(Convert.ToInt32(Request.QueryString["class_ID"]));
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='IPManage.aspx'>其他功能</a></li><li><a href='IPManage.aspx'>IP分类</a>   <a href='InputClass.aspx'>[添加分类]</a></li>");
    }
    protected string table()
    {
        string table_html = "";
        DataTable pro_dt = b_ipOperation.searchIP_pro_Class();


        if (pro_dt != null)
        {
            for (int i = 0; i < pro_dt.Rows.Count; i++)
            {
                DataTable city_dt = b_ipOperation.searchIP_city_Class(Convert.ToInt32(pro_dt.Rows[i]["class_ID"].ToString()));
                table_html = table_html + "<tr align=\"center\"><td>" + pro_dt.Rows[i][0] + "</td><td>" + getclassname(DataConverter.CLng(pro_dt.Rows[i][1])) + "</td><td align=\"left\"><b>" + pro_dt.Rows[i][2] + "</b></td><td align=\"right\"><a href=\"LookIP.aspx?class_ID=" + pro_dt.Rows[i]["class_ID"].ToString() + "\">查看IP</a>&nbsp|&nbsp<a href=\"InputClass.aspx?leadto_IP_ID=" + pro_dt.Rows[i][0] + "\">添加分类</a>&nbsp|&nbsp<a href=\"AlterClass.aspx?class_ID=" + pro_dt.Rows[i][0] + "\">修改分类</a>&nbsp|&nbsp<a href=\"InputIp.aspx?class_ID=" + pro_dt.Rows[i][0] + "\">添加IP</a>&nbsp|&nbsp<a href=\"IPManage.aspx?delete=yes&class_ID=" + pro_dt.Rows[i][0] + "\">删除</a></td></tr>";
                for (int j = 0; j < city_dt.Rows.Count; j++)
                {
                    table_html += "<tr align=\"center\"><td>" + city_dt.Rows[j][0] + "</td><td>" + getclassname(DataConverter.CLng(city_dt.Rows[j][1])) + "</td><td align=\"left\">　　" + city_dt.Rows[j][2] + "</td><td align=\"right\"><a href=\"LookIP.aspx?class_ID=" + city_dt.Rows[j]["class_ID"].ToString() + "\">查看IP</a>&nbsp|&nbsp<a href=\"AlterClass.aspx?class_ID=" + city_dt.Rows[j][0] + "\">修改分类</a>&nbsp|&nbsp<a href=\"InputIp.aspx?class_ID=" + city_dt.Rows[j][0] + "\">添加IP</a>&nbsp|&nbsp<a href=\"IPManage.aspx?delete=yes&class_ID=" + city_dt.Rows[j][0] + "\">删除</a></td></tr>";
                }
            }
        }
        return table_html;
    }
    protected void addClass_Click(object sender, EventArgs e)
    {
        Response.Redirect("inputclass.aspx");
    }
    protected string getclassname(int classid)
    {
        if (classid == 0)
        {
            return "";
        }
        else
        {
            return b_ipOperation.searchClass(classid).class_name;
        }
    }
}