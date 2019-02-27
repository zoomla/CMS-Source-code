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
using System.Text;
using ZoomLa.Common;
namespace ZoomLa.WebSite.Manage.Template
{
    public partial class InsertLabel : System.Web.UI.Page
    {
        protected B_Label bll = new B_Label();
        private int m_Labeltype;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            this.ShowLabelAdd();
        }

        private void ShowLabelAdd()
        {
            string str = base.Request.QueryString["n"];
            if (!string.IsNullOrEmpty(str))
            {
                this.LabelName.Text = str;
                M_Label lblinfo = this.bll.GetLabel(str);
                if (lblinfo.LableType == 3)
                    this.m_Labeltype = 1;
                else
                    this.m_Labeltype = 2;

                if (!lblinfo.IsNull)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<table width='100%' cellpadding=\"2\" cellspacing=\"1\" class=\"border\">");
                    int ptype = 0;
                    string aname = "";
                    string avalue = "";
                    string aintro = "";
                    string[] pa = lblinfo.Param.Split(new char[] { '|' });
                    for (int i = 0; i < pa.Length; i++)
                    {
                        ptype = DataConverter.CLng(pa[i].Split(new char[] { ',' })[2]);
                        if (ptype != 2)
                        {
                            aname = pa[i].Split(new char[] { ',' })[0];
                            avalue = pa[i].Split(new char[] { ',' })[1];
                            aintro = pa[i].Split(new char[] { ',' })[3];
                            builder.Append("<tr class=\"tdbg\"><td class=\"tdbgleft\" align=\"right\"><SPAN sid=\"" + aname + "\" stype=\"0\" title=\"" + aname + "\">" + aintro + "</SPAN>：</td><td align=\"left\">");
                            builder.Append("<input type=\"text\" id=\"" + aname + "\" value=\"" + avalue + "\"/></td></tr>");
                        }
                    }

                    builder.Append("</table>");
                    this.labelbody.Text = builder.ToString();
                    this.labelintro.Text = lblinfo.Desc;
                }
            }
        }
        public int LabelType
        {
            get { return this.m_Labeltype; }
        }
    }
}