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
namespace ZoomLaCMS.Manage.Template
{
    public partial class InsertLabel : CustomerPageAction
    {
        protected B_Label bll = new B_Label();
        public string LName { get { return HttpUtility.UrlDecode((Request.QueryString["n"] ?? "")); } }
        public int LabelType { get { return DataConverter.CLng(ViewState["LabelType"]); } set { ViewState["LabelType"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(LName)) { function.WriteErrMsg("未指定标签名"); }
                M_Label labelMod = this.bll.GetLabelXML(LName);
                if (labelMod.IsNull) { function.WriteErrMsg("[" + LName + "]不存在"); }
                if (labelMod.LableType == 3) { LabelType = 1; }
                else { LabelType = labelMod.LableType; }
                if (string.IsNullOrEmpty(labelMod.Param)) { function.Script(this, "submitdate();"); return; }
                //创建table
                StringBuilder builder = new StringBuilder();
                builder.Append("<table class='table table-bordered table-striped'>");
                int ptype = 0;
                string aname = "";
                string avalue = "";
                string aintro = "";
                //分割参数
                string[] pa = labelMod.Param.Split(new char[] { '|' });
                for (int i = 0; i < pa.Length; i++)
                {
                    //pageid,默认值,2,参数说明
                    ptype = DataConverter.CLng(pa[i].Split(',')[2]);
                    if (ptype == 1)
                    {
                        aname = pa[i].Split(new char[] { ',' })[0];
                        avalue = pa[i].Split(new char[] { ',' })[1];
                        aintro = pa[i].Split(new char[] { ',' })[3];
                        builder.Append("<tr><td class='text-right td_l'><SPAN sid=\"" + aname + "\" stype=\"0\" title=\"" + aname + "\">" + aintro + "</SPAN>：</td><td class='text-left'>");
                        builder.Append("<input type=\"text\" id=\"" + aname + "\" value=\"" + avalue + "\"/></td></tr>");
                    }
                    else if (ptype == 2) { }//页面参数不需要处理
                    else if (ptype == 3)//单选
                    {
                        aname = pa[i].Split(new char[] { ',' })[0];
                        avalue = pa[i].Split(new char[] { ',' })[1];
                        aintro = pa[i].Split(new char[] { ',' })[3];
                        builder.Append("<tr><td class='text-right td_l'><SPAN sid=\"" + aname + "\" stype=\"0\" title=\"" + aname + "\">" + aintro + "</SPAN>：</td><td align=\"left\">");
                        builder.Append("<select id=\"" + aname + "\" style=\"width:156px;\">");
                        string[] item = avalue.Split('$');
                        foreach (string iten in item)
                        {
                            builder.Append("<option value=\"" + iten + "\">" + iten + "</option>");
                        }
                        builder.Append("</select></td></tr>");

                    }
                    else if (ptype == 4)//多选
                    {
                        aname = pa[i].Split(new char[] { ',' })[0];
                        avalue = pa[i].Split(new char[] { ',' })[1];
                        aintro = pa[i].Split(new char[] { ',' })[3];
                        builder.Append("<tr><td class='text-right td_l'><SPAN sid=\"" + aname + "\" stype=\"1\" title=\"" + aname + "\">" + aintro + "</SPAN>：</td><td align=\"left\">");
                        builder.Append("<input id=\"h" + aname + "\" type=\"hidden\" />");
                        builder.Append("<div id=\"d" + aname + "\" style=\"display:block;\">");
                        string[] items = avalue.Split('$');
                        foreach (string itens in items)
                        {
                            builder.Append("<input type=\"checkbox\" name=\"c" + aname + "\" onclick=\"selectchecked(this)\" value=\"" + itens + "\" />" + itens + "</br>");
                        }
                        builder.Append("</div></td></tr>");
                    }
                }
                if (!builder.ToString().Contains("<tr>")) { function.Script(this, "submitdate();"); return; }
                builder.Append("</table>");
                this.labelbody.Text = builder.ToString();
                this.labelintro.Text = labelMod.Desc;
            }
        }
    }
}