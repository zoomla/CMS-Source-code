using System;
namespace ZoomLa.Model
{
    /// <summary>
    /// 查看内容历史记录
    /// </summary>
    public class M_ContentHis
    {
        /// <summary>
        /// 查看内容历史记录ID
        /// </summary>
        public int HID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 内容ID
        /// </summary>
        public int ContentID { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime HisTime { get; set; }
        public M_ContentHis()
        {
            this.HisTime = DateTime.Now;
        }
    }
}