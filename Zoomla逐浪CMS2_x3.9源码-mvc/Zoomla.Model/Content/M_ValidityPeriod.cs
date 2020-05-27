using System;

namespace ZoomLa.Model
{
    /// <summary>
    /// 有效期明细
    /// </summary>
    public class M_ValidityPeriod
    {
        public M_ValidityPeriod()
        {
            this.PID = 0;
            this.UserID = 0;
            this.DayCount = 0;
            this.PDetail = "";
            this.OperDate = DateTime.Now;
            this.Operator = 0;
        }
        /// <summary>
        /// 有效期明细ID
        /// </summary>
        public int PID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 有效期操作天数
        /// </summary>
        public int DayCount { get; set; }
        /// <summary>
        /// 有效期事务事由
        /// </summary>
        public string PDetail { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperDate { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public int Operator { get; set; }
        /// <summary>
        /// 操作IP
        /// </summary>
        public string OperatorIP { get; set; }
    }
}
