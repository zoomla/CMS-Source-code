using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Models.Cloud
{
    public class VM_Cloud
    {
        B_User_Cloud cloudBll = new B_User_Cloud();
        public M_UserInfo mu = null;
        private HttpRequestBase Request = null;
        public string FType { get { return string.IsNullOrEmpty(Request.QueryString["Type"]) ? "file" : Request.QueryString["Type"]; } }
        //当前地址栏指定的目录(不包含根类型与目录)
        public string VDir { get { return Request.QueryString["dir"] ?? ""; } }
        //当前完全虚拟目录(前后带/)
        public string CurrentDir
        {
            get
            {
                string _dir = (cloudBll.H_GetFolderByFType(FType, mu) + VDir).Replace("//", "/");
                _dir = _dir.TrimEnd('/') + "/";
                return _dir;
            }
        }
        //文件列表
        public PageSetting setting = null;
        //用于调用其中的方法
        public VM_Cloud(M_UserInfo mu, HttpRequestBase Request)
        {
            this.mu = mu;
            this.Request = Request;
        }
        public VM_Cloud(int cpage, int psize, M_UserInfo mu, HttpRequestBase Request)
        {
            this.mu = mu;
            this.Request = Request;
            setting = cloudBll.SelByPath(cpage, psize, CurrentDir, mu.UserID);
        }
        public MvcHtmlString GetDownUrl(DataRow dr)
        {
            string fileType = dr["FileType"].ToString();
            string result = "";
            if (fileType.Equals("1"))//普通文件
            {
                result = "<a href='/Plat/Doc/DownFile.aspx?CloudFile=" + dr["Guid"] + "' target='_down' class='btn btn-sm btn-info'><i class='fa fa-download' title='下载'></i></a>";
            }
            return MvcHtmlString.Create(result);
        }
        public MvcHtmlString GetSize(DataRow dr)
        {
            string result="";
            if (dr["FileType"].ToString() == "2") { }
            else result= FileSystemObject.ConvertSizeToShow(DataConverter.CLng(dr["FileSize"]));
            return MvcHtmlString.Create(result);
        }
        public MvcHtmlString GetUrl(DataRow dr)
        {
            string result = "";
            int filetype = Convert.ToInt32(dr["FileType"]);
            if (filetype == 2)
            { result = GroupPic.GetExtNameMini("filefolder"); }
            else if (SafeSC.IsImage(dr["FileName"].ToString()))
            {
                string imgurl = CurrentDir + dr["SFileName"];
                result = "<img src='" + imgurl + "' style='width:30px;height:30px;display:block;' />";
            }
            else
            {
                result= GroupPic.GetExtNameMini(Path.GetExtension(DataConverter.CStr(dr["FileName"])));
            }
            return MvcHtmlString.Create(result);
        }
        public MvcHtmlString GetLink(DataRow dr)
        {
            string result = "";
            if (DataConverter.CStr(dr["FileType"]).Equals("2")) 
            {
                result = "<a href='Index?Type=" + FType + "&Dir=" + HttpUtility.UrlEncode(VDir + "/" + dr["FileName"]) + "'>" + dr["FileName"] + "</a>";
            }
            else
            {
                result = "<a href='javascript:;' onclick='prefile(\"" + dr["Guid"] + "\")'>" + dr["FileName"] + "</a>";
            }
            return MvcHtmlString.Create(result);
        }
        //-----------------------------------
        /// <summary>
        /// 创建导航菜单,统一使用dir传参
        /// </summary>
        /// <param name="vdir">地址栏的dir传参</param>
        /// <param name="url">根目录地址</param>
        /// <returns></returns>
        public string GetBread(string vdir, string baseUrl)
        {
            string html = "";
            if (string.IsNullOrEmpty(vdir)) { html = "根目录"; }
            else
            {
                string url = baseUrl;
                string[] dirArr = vdir.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                html += "<a href='" + url + "'>全部文件</a>";
                for (int i = 0; i < dirArr.Length; i++)
                {
                    //上一级目录链接
                    url += HttpUtility.UrlEncode(dirArr[i] + "/");
                    html += "<i class='fa fa-angle-right spanr'></i>";
                    if (i == (dirArr.Length - 1)) { html += "<span>" + dirArr[i] + "</span>"; }
                    else { html += "<a href='" + url + "'>" + dirArr[i] + "</a>"; }
                    //设置返回上一级
                    if (dirArr.Length == 1) { html = "<a href='" + baseUrl + "'>返回上一级</a> | " + html; }
                    else if (i == (dirArr.Length - 2))
                    {
                        html = "<a href='" + url + "'>返回上一级</a> | " + html;
                    }
                }
            }
            return html;
        }
    }
}