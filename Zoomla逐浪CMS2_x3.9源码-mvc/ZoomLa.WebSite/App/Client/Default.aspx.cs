using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.App.Client
{
    public partial class Default : System.Web.UI.Page
    {
        public new int Site { get { return DataConvert.CLng(Request.QueryString["s"]); } }
        public string Url
        {
            get
            {
                string baseurl = "http://app.z01.com/APP/";
                if (Site == 1) { baseurl = "http://www.z01.com/APP/"; }
                return baseurl + Request.QueryString["U"];
            }
        }
        public string STitle { get { return Request.QueryString["T"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.HideBread(Master);
            }
        }
    }
}