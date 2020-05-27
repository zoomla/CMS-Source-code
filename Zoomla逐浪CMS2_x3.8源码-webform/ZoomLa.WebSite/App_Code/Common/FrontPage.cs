using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public class FrontPage : System.Web.UI.Page
{
    public B_Node nodeBll = new B_Node();
    public int ItemID
    {
        get
        {
            return DataConverter.CLng(B_Route.GetParam("ID", Page));
        }
    }
    /// <summary>
    /// 允许值为0页面判断,为0显示全部或==1
    /// </summary>
    public int Cpage { get { int cpage = DataConverter.CLng(B_Route.GetParam("CPage", Page)); return cpage < 0 ? 0 : cpage; } }
    public void ErrToClient(string str)
    {
        string html = SafeSC.ReadFileStr("/Prompt/error.html");
        html = html.Replace("@msg", str);
        Response.Clear(); Response.Write(html); Response.Flush(); Response.End();
    }
}