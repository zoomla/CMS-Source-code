using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

public class MVCHelper
{
    public static string GetAction(string action,HttpRequestBase Request)
    {
        return action + "?" + HttpUtility.UrlDecode(Request.QueryString.ToString());
    }
}