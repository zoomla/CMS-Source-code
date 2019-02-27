using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZoomLa.Model.Edit
{
    //客户端提交的数据包
    public class M_Doc_DataPack
    {
        public string docID = "";
        public long version = 0;//客户端文档版本
        public int time = 10;
        public M_Doc_DataPack() { }
        public M_Doc_DataPack(HttpRequest Request)
        {
            docID = Request["docID"];
            time = 10;
            version = long.Parse(Request["version"]);
            if (time < 10) { time = 10; }
        }
    }
    public class M_Doc_Vesion
    {
        public int roomID = 0;
        public long version = 0;
        public string html = "";
        public string sessionID = "";
    }
    public static class DocCache
    {
        public static M_Doc_Vesion verMod = new M_Doc_Vesion();
    }
}
