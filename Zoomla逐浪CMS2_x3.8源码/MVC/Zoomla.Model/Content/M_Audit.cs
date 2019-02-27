using System;
namespace ZoomLa.Model
{
    public class M_Audit
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}