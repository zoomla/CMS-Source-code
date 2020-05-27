using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using System.Text.RegularExpressions;
using ZoomLa.BLL.Other;

public partial class manage_WeiXin_QrCodeManage : CustomerPageAction
{
    DataTable dt = new DataTable();
    B_QrCode bll = new B_QrCode();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dt = bll.Sel();
            if (dt != null && dt.Rows.Count > 0)
                Bind(dt);
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/Default.aspx'>微信管理</a></li><li>二维码管理<a href='QrCode.aspx'>[生成二维码]</a></li>");
        }

    }

    protected void Bind(DataTable dt)
    {
        RPT.DataSource = dt;
        RPT.DataBind();
    }

    //文本
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        dt = bll.Sel();
        Bind(dt);
    }

    //分页
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        dt = bll.Sel();
        Bind(dt);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string items = Request.Form["Btchk"];

        if (items == null)
        {
            function.WriteErrMsg("未选中任何内容");
            return;
        }
        if (items.IndexOf(",") == -1)
        {
            int dsd = DataConverter.CLng(items);
            bll.Del(dsd);
        }

        else if (items.IndexOf(",") > -1)
        {
            string[] deeds = items.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int s = 0; s < deeds.Length; s++)
            {
                int dsd = DataConverter.CLng(deeds[s]);
                bll.Del(dsd);
            }
        }
        function.WriteSuccessMsg("批量删除成功!", "QrCodeManage.aspx");
    }
    protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int id = DataConverter.CLng(e.CommandArgument);
        if (e.CommandName == "Del")
        {
            bll.Del(id);
            function.WriteSuccessMsg("删除成功！", "QrCodeManage.aspx");
        }

    }
    protected string getType( int type)
    {
        switch (type)
        { 
            case 0:
                return "文字";
            case 1:
                return "名片";
            default:
                return "文字";
        }
    }
    protected string getTit(int type ,string con)
    {
        if (type == 1)
        {
            //MECARD: N:XX; TITLE:XXX; ORG:华夏互联; TEL:15857695211; EMAIL:46885524@163.com; X-MSN:; X-QQ:469582963; 主页:http://;
           // con = con.Split(new Char[] { ';' })[0].Split(new Char[] { ':' })[2] + " 的名片";

            try
            {
                Match m = Regex.Match(con, @"(N:).{1,5};");
                return m.ToString().Replace("N:", "").Replace(";", "") + "的名片";
            }
            catch
            {
                return con;
            }
            //return con;
        }
        else {
            return con;
        }
    }
}