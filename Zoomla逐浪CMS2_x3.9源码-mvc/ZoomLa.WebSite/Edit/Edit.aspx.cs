using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.IO;
using ZoomLa.Common;
using System.Data;
using System.Xml;

namespace ZoomLaCMS.Edit
{
    public partial class Edit : System.Web.UI.Page
    {
        //B_EditWord b_EditWord=new B_EditWord();
        private B_User bll = new B_User();
        protected string id = string.Empty;
        protected string cont = string.Empty;
        private B_Content contentBll = new B_Content();
        public string DocType
        {
            get
            {
                if (ViewState["DocType"] == null)
                {
                    ViewState["DocType"] = Request.QueryString["DocType"];
                }
                return HttpUtility.HtmlEncode(string.IsNullOrEmpty(ViewState["DocType"] as string) ? "" : ViewState["DocType"].ToString());
            }
            set { ViewState["DocType"] = value; }
        }
        DataTable dt = new DataTable();//用于存标签集
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Charset = "utf-8";
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != "0")
                {
                    //content.InnerHtml = b_EditWord.SeleCon(Convert.ToInt32(Request.QueryString["ID"]));

                }
                if (Request.QueryString["Tg"] == "true")
                {
                    Tgid.Value = Server.HtmlEncode(Request.QueryString["ID"]);
                    if (Request.QueryString["Tgid"] == "0" || Request.QueryString["Tgid"] == "")
                    {
                        M_UserInfo info = bll.GetLogin();
                        //id = b_EditWord.AddTg(Request.QueryString["va"], info.UserID).ToString();
                        Response.Write(id);
                        Session["Tgid"] = id;
                        Response.End();
                    }
                    else
                    {
                        if (Request.QueryString["title"] == "" || Request.QueryString["title"] == null)
                        {
                            //Response.Write(b_EditWord.UpdateTg(Request.QueryString["va"], Convert.ToInt32(Request.QueryString["Tgid"]), false).ToString());
                            Session["Tgid"] = Server.HtmlEncode(Request.QueryString["Tg"]);
                        }
                        else {
                            //Response.Write(b_EditWord.UpdateTg(Request.QueryString["va"], Convert.ToInt32(Request.QueryString["Tgid"]), true).ToString());
                            Session["Tgid"] = Server.HtmlEncode(Request.QueryString["Tg"]);
                        }
                        Response.End();
                    }
                }

                string url = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + Request.ServerVariables["PATH_INFO"].ToString();  //获得URL的值
                int i = url.LastIndexOf("/");
                url = url.Substring(0, i);
                this.Session["URL"] = url;

                if (!string.IsNullOrEmpty(Request.Form["need"]))
                {
                    #region  获取标签集（disuse）
                    //string labelID=Request.Form["labelID"];
                    //if (string.IsNullOrEmpty(labelID)) { function.WriteErrMsg("标签集为空"); ; return; }
                    //labelID = labelID.Remove(labelID.LastIndexOf(","), 1);
                    //string[] idArrary ;
                    //if (labelID.IndexOf(",") < 0) 
                    //{idArrary = new string[]{labelID};}
                    //else
                    //{idArrary = labelID.Split(',');}
                    //string s = "[";
                    //for (int index = 0; index < idArrary.Length; index++)
                    //{
                    //    //dt = labelBll.SelAllChild(idArrary[index]);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        foreach (DataRow dr in dt.Rows)
                    //        {
                    //            s += "{\"Label\":\"" + dr["infoLabel"] + "\",\"LabelValue\":\"" + dr["infoValue"] + "\"},";
                    //        }
                    //    }
                    //}
                    //s += "]";
                    //if (s.LastIndexOf(",") > -1)
                    //    s = s.Remove(s.LastIndexOf(","), 1);
                    #endregion

                    //获取文件名-读取xml-再依据xml-获取表A-表B
                    XmlDocument doc = new XmlDocument();
                    string docName = TxtTagKey.Text.ToString().Split('/')[0];
                    //doc.Load(Server.MapPath("~/UploadFiles/DocTemp/" + docName + "/" + docName + ".xml"));
                    //int nodeID=DataConverter.CLng(doc.SelectSingleNode("DocInfo/generalID").InnerText);
                    //string generalID = doc.SelectSingleNode("DocInfo/generalID").InnerText;
                    //DataTable dt= contentBll.GetContentList(nodeID, "", "" + docName + "' and Title='" + docName + "' and Title like '" + docName + "", 0);
                    Response.Write(TxtTagKey.Text.ToString());
                    Response.Flush();
                    Response.End();
                }//if need end; 

            }
        }
    }
}