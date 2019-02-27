using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

namespace sidlehelper
{
    /// <summary> 
    /// summary description for strhelper. 
    /// 命名缩写： 
    /// str: unicode string 
    /// arr: unicode array 
    /// hex: 二进制数据 
    /// hexbin: 二进制数据用ascii字符表示 例 字符1的hex是0x31表示为hexbin是 31 
    /// asc: ascii 
    /// uni: unicode 
    /// </summary> 
    public sealed class strhelper
    {
        #region hex与hexbin的转换
        public static void hexbin2hex(byte[] bhexbin, byte[] bhex, int nlen)
        {
            for (int i = 0; i < nlen / 2; i++)
            {
                if (bhexbin[2 * i] < 0x41)
                {
                    bhex[i] = Convert.ToByte(((bhexbin[2 * i] - 0x30) << 4) & 0xf0);
                }
                else
                {
                    bhex[i] = Convert.ToByte(((bhexbin[2 * i] - 0x37) << 4) & 0xf0);
                }

                if (bhexbin[2 * i + 1] < 0x41)
                {
                    bhex[i] |= Convert.ToByte((bhexbin[2 * i + 1] - 0x30) & 0x0f);
                }
                else
                {
                    bhex[i] |= Convert.ToByte((bhexbin[2 * i + 1] - 0x37) & 0x0f);
                }
            }
        }
        public static byte[] hexbin2hex(byte[] bhexbin, int nlen)
        {
            if (nlen % 2 != 0)
                return null;
            byte[] bhex = new byte[nlen / 2];
            hexbin2hex(bhexbin, bhex, nlen);
            return bhex;
        }
        public static void hex2hexbin(byte[] bhex, byte[] bhexbin, int nlen)
        {
            byte c;
            for (int i = 0; i < nlen; i++)
            {
                c = Convert.ToByte((bhex[i] >> 4) & 0x0f);
                if (c < 0x0a)
                {
                    bhexbin[2 * i] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bhexbin[2 * i] = Convert.ToByte(c + 0x37);
                }
                c = Convert.ToByte(bhex[i] & 0x0f);
                if (c < 0x0a)
                {
                    bhexbin[2 * i + 1] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bhexbin[2 * i + 1] = Convert.ToByte(c + 0x37);
                }
            }
        }
        public static byte[] hex2hexbin(byte[] bhex, int nlen)
        {
            byte[] bhexbin = new byte[nlen * 2];
            hex2hexbin(bhex, bhexbin, nlen);
            return bhexbin;
        }
        #endregion

        #region 数组和字符串之间的转化
        public static byte[] str2arr(string s)
        {
            return (new UnicodeEncoding()).GetBytes(s);
        }
        public static string arr2str(byte[] buffer)
        {
            return (new UnicodeEncoding()).GetString(buffer, 0, buffer.Length);
        }

        public static byte[] str2ascarr(string s)
        {
            return System.Text.UnicodeEncoding.Convert(System.Text.Encoding.Unicode,
            System.Text.Encoding.ASCII,
            str2arr(s));
        }

        public static byte[] str2hexascarr(string s)
        {
            byte[] hex = str2ascarr(s);
            byte[] hexbin = hex2hexbin(hex, hex.Length);
            return hexbin;
        }
        public static string ascarr2str(byte[] b)
        {
            return System.Text.UnicodeEncoding.Unicode.GetString(
            System.Text.ASCIIEncoding.Convert(System.Text.Encoding.Default,
            System.Text.Encoding.Unicode,
            b)
            );
        }

        public static string hexascarr2str(byte[] buffer)
        {
            byte[] b = hex2hexbin(buffer, buffer.Length);
            return ascarr2str(b);
        }
        #endregion
    }
}

namespace ZoomLaCMS.Manage.Config
{
    public partial class Sensitivity : CustomerPageAction
    {
        protected B_Sensitivity sll = new B_Sensitivity();
        private string Menu { get { return Request.QueryString["menu"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Menu == "delete")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    sll.DeleteByGroupID(id);
                }
                DataBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\">敏感词汇[<a href='AddSensitivity.aspx'>添加词汇</a>]</li>" + Call.GetHelp(9));
            }
        }
        public void DataBind(string key = "")
        {
            DataTable dt = sll.Select_All();
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            byte[] bSource = InputTxt.FileBytes;
            string stringlist = sidlehelper.strhelper.ascarr2str(bSource);
            string[] listarr = stringlist.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (!InputTxt.HasFile) { function.WriteErrMsg("没有选择上传文件!"); }
            foreach (string listitem in listarr)
            {
                M_Sensitivity sin = new M_Sensitivity();
                sin.istrue = 1;
                sin.keyname = listitem.Trim();
                sll.GetInsert(sin);
            }
            function.WriteSuccessMsg("导入成功!");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddSensitivity.aspx");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            //批量删除
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    sll.DeleteByGroupID(itemID);
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除成功！')", true);
                DataBind();
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            //批量启用
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    sll.UpdateStatus(itemID, 0);
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功！')", true);
                DataBind();
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            //批量停用
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    sll.UpdateStatus(itemID, 1);
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功！')", true);
                DataBind();
            }
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            DataTable tablelist = sll.Select_All();
            StringBuilder list = new StringBuilder();
            for (int i = 0; i < tablelist.Rows.Count; i++)
            {
                list.AppendLine(tablelist.Rows[i]["keyname"].ToString());
            }
            Response.AppendHeader("Content-Type", "application/ms-txt");
            Response.AppendHeader("Content-disposition", "attachment; filename=Sensiteivity.txt");
            Response.Write(list.ToString());
            Response.End();
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del1":
                    sll.DeleteByGroupID(Convert.ToInt32(e.CommandArgument.ToString()));
                    break;
                default:
                    break;
            }
            DataBind();
        }
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] chkArr = Request.Form["idchk"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
    }
}