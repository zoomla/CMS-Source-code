using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public partial class api_SiteCount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["site"] == null)
        {
            Response.Write("请输入正确格式：SiteCount.aspx?site=(google或baidu)&type=(img或txt)&url=站点地址");
            Response.End();
        }
        else
        {
            string site = Request.QueryString["site"];
            #region Google统计
            if (site.ToLower() == "google")
            {
                if (Request.QueryString["url"] != null)
                {
                    string url = Request.QueryString["url"];
                    url = "http://www.google.com.hk/search?hl=zh-CN&newwindow=1&safe=strict&complete=1&q=site%3A" + url + "&aq=f&aqi=&aql=&oq=&gs_rfai=";
                    if (url != "")
                    {
                        string RemoteHtmlCode = GetRemoteHtmlCode(url).ToString();
                        string num = "";
                        if (RemoteHtmlCode.IndexOf("条结果<nobr> ") > -1)
                        {
                            string allstring = RemoteHtmlCode.Split(new string[] { "条结果<nobr> " }, StringSplitOptions.None)[0];
                            num = allstring.Split(new string[] { "获得约" }, StringSplitOptions.None)[1];
                            //Response.Write(num);
                        }
                        else
                        {
                            num = "0";
                        }


                        if (Request.QueryString["type"] != null)
                        {
                            string typest = Request.QueryString["type"];

                            if (typest == "txt")
                            {
                                Response.Write("document.write('" + num + "');");
                            }
                            else if (typest == "img")
                            {
                                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("/Images/google.jpg"));
                                Graphics g = Graphics.FromImage(image);
                                g.DrawImage(image, 0, 0, image.Width, image.Height);
                                Font f = new Font("Verdana", 10);
                                Brush b = new SolidBrush(Color.Black);
                                string addText = num.Trim();
                                g.DrawString(addText, f, b, 20, 1);
                                g.Dispose();

                                MemoryStream stream = new MemoryStream();
                                image.Save(stream, ImageFormat.Gif);

                                HttpContext.Current.Response.ClearContent();
                                HttpContext.Current.Response.ContentType = "image/gif";
                                HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                            }
                            else
                            {
                                Response.Write("document.write('" + num + "');");
                            }
                        }
                        else
                        {
                            Response.Write("document.write('" + num + "');");
                        }
                    }
                }
            }
            #endregion
            #region Baidu统计
            if (site.ToLower() == "baidu")
            {
                if (Request.QueryString["url"] != null)
                {
                    string url = Request.QueryString["url"];
                    url = "http://www.baidu.com/s?wd=site%3A" + url;
 
                    if (url != "")
                    {
                        string RemoteHtmlCode = GetRemoteHtmlCode2(url).ToString();
                        string num = "";

                        if (RemoteHtmlCode.IndexOf("篇，用时") > -1)
                        {
                            string allstring = RemoteHtmlCode.Split(new string[] { "篇，用时" }, StringSplitOptions.None)[0];
                            num = allstring.Split(new string[] { "百度一下，找到相关网页约" }, StringSplitOptions.None)[1];
                            //Response.Write(num);
                        }
                        else
                        {
                            num = "0";
                        }
                        num = num.Replace(",","");

                        if (Request.QueryString["type"] != null)
                        {
                            string typest = Request.QueryString["type"];

                            if (typest == "txt")
                            {
                                Response.Write("document.write('" + num + "');");
                            }
                            else if (typest == "img")
                            {
                                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("/Images/baidu.jpg"));
                                Graphics g = Graphics.FromImage(image);
                                g.DrawImage(image, 0, 0, image.Width, image.Height);
                                Font f = new Font("Verdana", 10);
                                Brush b = new SolidBrush(Color.Black);
                                string addText = num.Trim();
                                g.DrawString(addText, f, b, 20, 1);
                                g.Dispose();

                                MemoryStream stream = new MemoryStream();
                                image.Save(stream, ImageFormat.Gif);

                                HttpContext.Current.Response.ClearContent();
                                HttpContext.Current.Response.ContentType = "image/gif";
                                HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                            }
                            else
                            {
                                Response.Write("document.write('" + num + "');");
                            }
                        }
                        else
                        {
                            Response.Write("document.write('" + num + "');");
                        }
                    }
                }
            }
            #endregion
        }
    }

    public string GetRemoteHtmlCode(string Url)
    {
        string s = "";
        MSXML2.XMLHTTP _xmlhttp = new MSXML2.XMLHTTPClass();
        _xmlhttp.open("GET", Url, false, null, null);
        _xmlhttp.send("");
        if (_xmlhttp.readyState == 4)
        {
            s = System.Text.Encoding.UTF8.GetString((byte[])_xmlhttp.responseBody);
        }
        _xmlhttp.abort();
        return s;
    }

    public string GetRemoteHtmlCode2(string Url)
    {
        string s = "";
        MSXML2.XMLHTTP _xmlhttp = new MSXML2.XMLHTTPClass();
        _xmlhttp.open("GET", Url, false, null, null);
        _xmlhttp.send("");
        if (_xmlhttp.readyState == 4)
        {
            s = System.Text.Encoding.Default.GetString((byte[])_xmlhttp.responseBody);
        }
        _xmlhttp.abort();
        return s;
    }
}
