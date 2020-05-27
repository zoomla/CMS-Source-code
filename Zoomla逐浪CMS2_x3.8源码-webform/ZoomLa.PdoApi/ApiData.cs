namespace ZoomLa.API
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Xml;
    using System.IO;
    using System.Text;
    using System.Net;
    using System.Web;

    /// <summary>
    /// Api接口数据对象
    /// </summary>
    public class ApiData
    {
        public string AppID;
        public ApiConfig config = new ApiConfig("~/Config/pdo.config");
        public string Message = "";
        public string Status = "1";
        public ApiData()
        {
            
        }
        public string GetValue(string name)
        {
            return "";
        }

        public void PrintXmlData()
        {
           
        }
        public void SendHttpData()
        {
            
        }

        public string SetCookie(string c_username, string c_password, string c_type)
        {
            return "";
        }

        public void SetValue(string name, string value, int type)
        {
          
        }
    }
}