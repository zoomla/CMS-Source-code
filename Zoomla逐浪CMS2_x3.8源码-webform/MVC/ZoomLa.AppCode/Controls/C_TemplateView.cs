using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Components;

namespace ZoomLa.AppCode.Controls
{
    public class C_TemplateView
    {
        public DataTable dt = null;
        public string name = "";
        public string selid = "";
        public string selurl = "";
        //是否默认选中第一个(后台不需要,前台需要)
        public bool IsFirstSelect = true;
        public C_TemplateView(DataTable _dt = null)
        {
            if (_dt == null) { this.dt = GetTemplateDT(); }
            else { this.dt = _dt; }
        }
        private DataTable GetTemplateDT()
        {
            string pathdir = function.VToP(SiteConfig.SiteOption.TemplateDir+ "/") ;
            if (!Directory.Exists(pathdir)) { return null; }
            DataTable tables = FileSystemObject.GetDirectoryAllInfos(pathdir + @"\内容页\", FsoMethod.File);
            tables.Columns.Add("TemplatePic");//添加模板图片url
            tables.Columns.Add("TemplateID");
            tables.Columns.Add("TemplateUrl");
            for (int i = 0; i < tables.Rows.Count; i++)
            {
                tables.Rows[i]["rname"] = Path.GetFileName(DataConverter.CStr(tables.Rows[i]["rname"]));
                string imgname = "内容页_" + Path.GetFileNameWithoutExtension(tables.Rows[i]["name"].ToString()) + ".jpg";
                tables.Rows[i]["TemplatePic"] = SiteConfig.SiteOption.TemplateDir + "/thumbnail/" + imgname;
                tables.Rows[i]["TemplateID"] = tables.Rows[i]["rname"];
                tables.Rows[i]["TemplateUrl"] = tables.Rows[i]["rname"];
                //if (!File.Exists(function.VToP(pathdir + "/thumbnail/" + imgname)))
                //{
                //    tables.Rows.Remove(tables.Rows[i]);
                //    i--;
                //}
            }
            tables.DefaultView.RowFilter = "type=1 OR name LIKE '%.html'";
            return tables.DefaultView.ToTable();
        }
    }
}
