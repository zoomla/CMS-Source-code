using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;

public partial class manage_Iplook_LookIp : CustomerPageAction
{
    B_IPOperation b_IPOperation = new B_IPOperation();
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='IPManage.aspx'>其他功能</a></li><li><a href='LookIp.aspx'>IP地址管理</a></li>");
        if(Request.QueryString["delete"]!=null)
        {
            deleIP(Convert.ToInt32(Request.QueryString["delete"]));
        }
    }

    public string page_table()
    {

        string table_html = "";
        
        DataTable dt = new DataTable();
        if (Request.QueryString["class_ID"] != null)
        {
            dt = b_IPOperation.searchIP(Convert.ToInt32(Request.QueryString["class_ID"]));
        }
        else {
            dt = b_IPOperation.searchAllIP();
        }
        
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string startIP = "";
                int ip1 = Convert.ToInt32(dt.Rows[i][4].ToString());
                int split1 = 3;
                int startIPnode = Convert.ToInt32(dt.Rows[i][4].ToString());
                for (int j = 3; j >= 0; j--)
                {
                    startIPnode = Convert.ToInt32(ip1 / Math.Pow(255, split1));
                    if (split1 == 0)
                    {
                        startIP = startIP + startIPnode;
                    }
                    else
                    {
                        startIP = startIP + startIPnode + ".";
                    }
                    
                    ip1 = Convert.ToInt32(ip1 % Math.Pow(255, split1));
                    split1--;
                }

                string endIP = "";
                int ip2 = Convert.ToInt32(dt.Rows[i][5].ToString());
                int split2 = 3;
                int endIPnode = Convert.ToInt32(dt.Rows[i][5].ToString());
                for (int j = 3; j >= 0; j--)
                {
                    endIPnode = Convert.ToInt32(ip2 / Math.Pow(255, split2));
                    if (split2 == 0)
                    {
                        endIP = endIP + endIPnode;
                    }
                    else {
                        endIP = endIP + endIPnode + ".";
                    }
                    
                    ip2 = Convert.ToInt32(ip2 % Math.Pow(255, split2));
                    split2--;
                }

                table_html = table_html + "<tr align=\"center\"><td>" + dt.Rows[i][0] + "</td><td>" + dt.Rows[i][1] + "</td><td align=\"left\"><center><b>" + dt.Rows[i][2] + "</b></center></td><td align=\"center\">" + dt.Rows[i][3] + "</td><td>" + startIP + "</td><td>" + endIP + "</td><td><a href=\"AlterIP.aspx?ID=" + dt.Rows[i][0] + "\">修改</a>|<a href=\"LookIp.aspx?delete=" + dt.Rows[i][0] + "\">删除</a></td></tr>";
            }
        }
        return table_html;
    }

    private void deleIP(int ID)
    {
        b_IPOperation.deleIP(ID);
        Response.Redirect("LookIp.aspx");
    }
}
