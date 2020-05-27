using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Chat
{
    public class M_OnlineUser
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserFace { get; set; }
        //是否游客true:是
        public bool IsVisitor { get; set; }
        //最后一次有请求的时间
        public DateTime LastTime { get; set; }
    }
}
