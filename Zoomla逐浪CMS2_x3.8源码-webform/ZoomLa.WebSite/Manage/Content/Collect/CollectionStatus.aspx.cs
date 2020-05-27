using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using ZoomLa.BLL;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL.Collect;

public partial class Manage_I_Content_CollectionStatus : CustomerPageAction
{
    private B_CollectionItem bll = new B_CollectionItem();
    //private B_MailManage mailinfo;
    private delegate void ASyncInvoke();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button1.Text = "正在采集...";
            ASyncInvoke func = ASyncFunc;
            IAsyncResult asyncResult = func.BeginInvoke(null, null);
            Call.SetBreadCrumb(Master, "<li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='CollectionManage.aspx'>信息采集</a></li><li class='active'><a href='" + Request.RawUrl + "'>采集状态</a></li>");
        }
    }
    public void ASyncFunc()
    {
        //if (Application["MailSends"] != null)
        //{
        //    mailinfo = (B_MailManage)Application["MailSends"];
        //}
        //if (mailinfo != null && !B_Coll_Worker.StopColl)
        //{
        //    //Application["allnum"] = B_Coll_Worker.all//数据总数
        //    //int cc = 0;
        //    //B_CollectionItem bci = new B_CollectionItem();
        //    //DataTable newc = bci.Select_Where("Switch=1", "*", "");
        //    //for (int i = 0; i < newc.Rows.Count; i++)
        //    //{
        //    //    M_CollectionItem mc = bll.GetSelect(DataConverter.CLng(newc.Rows[0]["CItem_ID"]));
        //    //    if (mc.Switch != 2)
        //    //    {
        //    //        cc = cc + 1;
        //    //    }
        //    //}
        //    //if (cc > 0)
        //    //{
        //    //    Application.Lock();
        //    //    mailinfo.url = null;
        //    //    mailinfo.isstop = false;
        //    //    System.Web.HttpContext.Current.Application["MailSends"] = mailinfo;
        //    //    Application.UnLock();
        //    //    this.Timer1.Enabled = true;
        //    //    function.Script(this, "alert('采集完成!');");
        //    //}
        //}
    }
    //后期需改成 开个异步开始执行采集
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(B_Coll_Worker.CollectLog))
        {
            string templl = B_Coll_Worker.CollectLog;
            B_Coll_Worker.CollectLog = "";
            foreach (string s in templl.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries))
            {
                TextBox1.Text = "正在采集：" + s.TrimEnd("\r\n".ToCharArray()) + "\r\n"+ TextBox1.Text;
            }
            if (templl.Contains("已完成采集."))
            {
                B_Coll_Worker.StopColl = true;
                Timer1.Enabled = false;
                Button1.Text = "采集完成...";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //B_MailManage mailinfo = new B_MailManage();
        //mailinfo = (B_MailManage)System.Web.HttpContext.Current.Application["MailSends"];

        //B_CollectionItem bci = new B_CollectionItem();
        //DataTable newc = bci.SelBySwitch(1);

        //for (int i = 0; i < newc.Rows.Count; i++)
        //{
        //    M_CollectionItem mc = bll.GetSelect(DataConverter.CLng(newc.Rows[0]["CItem_ID"]));
        //    mc.Switch = 2;
        //    mc.LastTime = DateTime.Now.ToString();
        //    bll.GetUpdate(mc);
        //}

        //mailinfo.CreateThread.Join(10);
        //if (mailinfo.progresstd.IsAlive)
        //{
        //    try
        //    {
        //        mailinfo.progresstd.Join();
        //        //mailinfo.progresstd.Interrupt();
        //        mailinfo.progresstd.Abort();
        //    }
        //    catch (Exception err)
        //    {
        //        //mailinfo.progresstd.Join();
        //        //mailinfo.progresstd.Interrupt();
        //        //mailinfo.progresstd.Abort();
        //    }
        //}

        Application.Lock();
        B_Coll_Worker.StopColl = true;
        //System.Web.HttpContext.Current.Application["MailSends"] = mailinfo;
        Application.UnLock();

        this.TextBox1.ForeColor = System.Drawing.Color.Red;
        this.TextBox1.Font.Size = 14;
        this.TextBox1.Text = "恭喜! 采集完成!\n\r" + this.TextBox1.Text;
        this.Timer1.Enabled = false;
    }
}