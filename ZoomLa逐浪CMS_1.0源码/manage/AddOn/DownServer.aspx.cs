using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
public partial class manage_Plus_DownServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int DSId=0;
        if(!Page.IsPostBack)
        {
            if (Request.QueryString["Action"]!=null)
                    {
                        if(Request.QueryString["Action"]=="Modify")
                        {
                             DSId = Convert.ToInt32(Request.QueryString["DSId"].Trim());
                             InItModify(DSId);
                        }
                       
                    }
        }
        
    }
    private void InItModify(int Dsid)
    {
        B_DownServer bdownerver=new B_DownServer();
        M_DownServer mdownserver =bdownerver.GetDownServerByid(Dsid);
        TxtServerName.Text = mdownserver.ServerName;
        TxtServerLogo.Text = mdownserver.ServerLogo;
        TxtServerUrl.Text = mdownserver.ServerUrl;
        DropShowType.SelectedValue = mdownserver.ShowType.ToString();
        EBtnModify.Visible = true;
        EBtnSubmit.Visible = false;
    }
    protected void EBtnModify_Click(object sender, EventArgs e)
    {
        B_DownServer bdownserver = new B_DownServer();
        M_DownServer mdownserver = new M_DownServer();
        mdownserver.ServerID = Convert.ToInt32(Request.QueryString["DSId"].Trim());//bdownserver.Max() + 1;
        mdownserver.ServerName = TxtServerName.Text.ToString();
        mdownserver.ServerLogo = TxtServerLogo.Text.ToString();
        mdownserver.ServerUrl = TxtServerUrl.Text.ToString();
        mdownserver.ShowType = Convert.ToInt32(DropShowType.SelectedValue);
        if (bdownserver.Update(mdownserver))
        {
            Response.Write("<script language=javascript> alert('修改成功！');window.document.location.href='DownServerManage.aspx';</script>");
            //Page.Response.Redirect("DownServerManage.aspx");
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        B_DownServer bdownserver = new B_DownServer();
        M_DownServer mdownserver = new M_DownServer();
        mdownserver.ServerID = bdownserver.Max() + 1;
        mdownserver.ServerName = TxtServerName.Text.ToString();
        mdownserver.ServerLogo = TxtServerLogo.Text.ToString();
        mdownserver.ServerUrl = TxtServerUrl.Text.ToString();
        mdownserver.ShowType = Convert.ToInt32(DropShowType.SelectedValue);
        if(bdownserver.Add(mdownserver))
        {
            Response.Write("<script language=javascript> alert('添加成功！');window.document.location.href='DownServerManage.aspx';</script>");
            //Page.Response.Redirect("DownServerManage.aspx");
        }

    }
}
