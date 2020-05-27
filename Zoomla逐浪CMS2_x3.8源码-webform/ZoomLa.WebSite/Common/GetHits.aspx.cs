namespace ZoomLa.WebSite
{
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
    using ZoomLa.Model;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.BLL.Content;

    public partial class GetHits : System.Web.UI.Page
    {
        B_Content bll = new B_Content();
        B_Node nll = new B_Node();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int generalId = DataConverter.CLng(Request.QueryString["id"]);
                this.ShowHits(generalId);
            }
        }
        private void ShowHits(int id)
        {
            M_CommonData CData = this.bll.GetCommonData(id);
            M_Node ninfo = nll.GetNodeXML(CData.NodeID);
            int hits = CData.Hits;

            if (Session["Content" + id.ToString()] == null)
            {
                DateTime d1 = DataConverter.CDate(Session["Content" + id.ToString()]);
                TimeSpan d3 = DateTime.Now.Subtract(d1);
                hits = CData.Hits + 1;
                this.bll.UpHits(id);
                AddHits(id);//添加点击记录
                Session["Content" + id.ToString()] = DateTime.Now;

            }
            else
            {
                DateTime d1 = DataConverter.CDate(Session["Content" + id.ToString()]);
                TimeSpan d3 = DateTime.Now.Subtract(d1);
                if (d3.TotalSeconds >= ninfo.ClickTimeout)
                {
                    hits = CData.Hits + 1;
                    this.bll.UpHits(id);
                    AddHits(id);//添加点击记录
                    Session["Content" + id.ToString()] = DateTime.Now;
                }
            }
            base.Response.Write("document.write(" + hits + ");");
        }

        /// <summary>
        /// 添加点击记录
        /// </summary>
        private void AddHits(int Gid)
        {
            B_Hits bhits = new B_Hits();
            M_Hits mhits=new M_Hits();
            mhits.GID = Gid;
            mhits.IP = GetClientIP();
            mhits.UpdateTime = DateTime.Now;
            mhits.Status = 0;
            try
            {
                B_User buser = new B_User();
                mhits.UID = buser.GetLogin().UserID;
            }
            catch { }
            bhits.Add(mhits);
        }
        private string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

    }
}