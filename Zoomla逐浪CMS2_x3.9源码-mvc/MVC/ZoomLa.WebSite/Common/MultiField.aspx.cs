using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

namespace ZoomLaCMS.Common
{
    public partial class MultiField : System.Web.UI.Page
    {
        protected string strHtml = string.Empty;
        protected string fieldname = string.Empty;
        protected B_ModelField bfield = new B_ModelField();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 初始化
            string[] strArray = (Request["content"]).Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            strHtml += "属性：<select class='form-control text_x'>";
            foreach (string str in strArray)
            {
                string[] strArray1 = str.Split('|');
                try { strHtml += "<option value='" + strArray1[1].Split('$')[0] + "'>" + strArray1[0] + "</option>"; }
                catch { strHtml += "<option value='" + strArray1[1] + "'>" + strArray1[0] + "</option>"; }
            }
            strHtml += "</select>&nbsp;值：<input class='form-control text_x' type='text'/>";
            fieldname = Request["fieldname"];
            #endregion

            if (Request["fieldcontent"] != null && Request["fieldcontent"] != "")
            {
                string strTemp = string.Empty;
                string[] values = Request["fieldcontent"].Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    if (i == 0)
                    {
                        strTemp += "<tr  id='t" + i + "' onmouseover='setContent(this)' onmousemove='setContent(this)' onkeyup='setContent(this)'><td style='width: 20px; text-align: right;'><a class='btn btn-primary' href='javascript:addrow()'><span class='fa fa-plus'></span></a></td><td>属性：<select class=\"form-control text_x\">";
                        strTemp += SetField(values[i].Split('|')[0]);
                        strTemp += "</select>&nbsp;值：<input class='form-control text_x' type='text' value=" + values[i].Split('|')[1] + "></td></tr>";
                    }
                    else
                    {
                        strTemp += "<tr id='t" + i + "' onmouseover='setContent(this)' onmousemove='setContent(this)' onkeyup='setContent(this)'><td style='width: 20px; text-align: right;'><a class='btn btn-primary' href='javascript:deleterow(\"t" + i + "\")'><span class='fa fa-minus-circle'></span></a></td><td>属性：<select class=\"form-control text_x\">";
                        strTemp += SetField(values[i].Split('|')[0]);
                        strTemp += "</select>&nbsp;值：<input class='form-control text_x' type='text' value=" + values[i].Split('|')[1] + "></td></tr>";
                    }
                    this.content.Value += "t" + i + "|" + values[i] + ",";
                }
                sortList.InnerHtml = strTemp;
            }
            else
            {
                sortList.InnerHtml = "<tr id='t0' onmouseover='setContent(this)' onmousemove='setContent(this)' onkeyup='setContent(this)'><td style='text-align:right;width:20px'><a class='btn btn-primary' href='javascript:addrow()'><span class='fa fa-plus'></span></a></td><td>" + strHtml + "</td></tr>";
            }
        }
        //0|:100ml|100ml$0||200ml|200ml$0||300ml|300ml$0:vvvvvvvvvvvvv 
        private string SetField(string field)
        {
            string strTemp = string.Empty;
            string[] strArray = (Request["content"]).Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray)
            {
                string[] strArray1 = str.Split('|');
                if (field == strArray1[0])
                {
                    //throw new Exception(field+"============="+Request["content"]);
                    strTemp += "<option value='" + strArray1[1].Split('$')[0] + "' selected>" + strArray1[0] + "</option>";
                }
                else
                {
                    strTemp += "<option value='" + strArray1[1].Split('$')[0] + "' >" + strArray1[0] + "</option>";
                }
            }
            return strTemp;
        }
    }
}