using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.CreateJS
{
    public class M_Release
    {
        //取出值后直接删
        /// <summary>
        /// Index:首页,IDRegion:按ID范围发布(起始与结束ID),发布最新,按日期发布,发布所有,NodeIDS:发布栏目页,ALLNode:发布所有栏目页
        /// ByNodeIDS:按栏目发布内容,SPage:单页,ALLSPage:所有单页,Special:按专题发布内容页
        /// Schedule:定时任务
        /// </summary>
        public enum RType { Index, IDRegion, Gids, Newest, DateRegion, ALL, NodeIDS, ALLNode, ByNodeIDS, SPage, ALLSPage, Special, Schedule };
        public RType MyRType { get; set; }
        //需要发布的内容IDS
        public string Gids = "";
        //需要发布的节点IDS
        public string NodeIDS = "";
        //限定ID,用于限定生成某个栏目下最新的或某段时间内的文章
        public int NodeID = 0;
        //需要发布的内容IDS
        public string ContentIDS = "";
        public int Count = 0;
        //按ID发布,起始与结束ID
        public int SGid = 0;
        public int EGid = 0;
        public DateTime CDate = DateTime.Now;
        //起始与结束时间
        public DateTime STime = DateTime.Now;
        public DateTime ETime = DateTime.Now;
        /// <summary>
        /// 0:未处理,1:已处理
        /// </summary>
        public int MyStatus = 0;
    }
    public class M_ReleaseResult
    {
        /// <summary>
        /// 0:失败,1:成功(Disuse)
        /// </summary>
        public int Status = 0;
        public string ResultMsg = "";
        //虚拟路径
        public string VPath = "";
        public string Url = "";
        public int Count = 0;//第多少篇内容
        //是否终止,如结束则终止遍历
        public bool IsEnd = false;
    }
}
