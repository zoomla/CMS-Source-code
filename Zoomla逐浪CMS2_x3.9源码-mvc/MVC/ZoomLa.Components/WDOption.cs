using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Components
{
    [Serializable]
    public class WDOption
    {
        //会员问答参数设置

        //可查询的会员组(问答)
        public string selGroup { get; set; }
        //可回复的会员组(问答)
        public string ReplyGroup { get; set; }
        //可提问的会员组(问答)
        public string QuestGroup { get; set; }
        //普通答案获分
        public int WDPoint { get; set; }
        //推荐答案获分
        public int WDRecomPoint { get; set; }
        //提问获分
        public int QuestPoint { get; set; }
        //问答积分类型
        public string PointType { get; set; }
        //提问是否登录
        public bool IsLogin { get; set; }
        //回复是否登录
        public bool IsReply { get; set; }
    }
}
