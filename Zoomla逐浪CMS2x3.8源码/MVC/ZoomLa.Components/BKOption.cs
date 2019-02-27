using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Components
{
    /// <summary>
    /// 百科配置
    /// </summary>
    [Serializable]
    public class BKOption
    {
        //可查看百科词条的分组
        public string selGroup { get; set; }
        //可创建百科词条的分组
        public string CreateBKGroup { get; set; }
        //可编辑百科词条的分组
        public string EditGroup { get; set; }
        //创建百科词条所获分数
        public int CreatePoint { get; set; }
        //编辑百科词条所获分数
        public int EditPoint { get; set; }
        //推荐所获积分
        public int RemmPoint { get; set; }
        //分数类型
        public string PointType { get; set; }
        
    }
}
