using System;

namespace ZoomLa.Model
{
    /// <summary>
    /// 采集项目模型
    /// </summary>
    public class M_CollectItem
    {
        public M_CollectItem(bool value)
        {
            this.IsNull = value;
            this.LastExecTime = DateTime.Now;
            this.Detection = false;
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ItemID
        {
            get;
            set;
        }
        /// <summary>
        /// 项目名
        /// </summary>
        public string ItemName
        {
            get;
            set;
        }
        /// <summary>
        /// 目标网站名
        /// </summary>
        public string TargetName
        {
            get;
            set;
        }
        /// <summary>
        /// 目标网址
        /// </summary>
        public string TargetUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 采集编码
        /// </summary>
        public string DocEncoder
        {
            get;
            set;
        }
        /// <summary>
        /// 归档节点ID
        /// </summary>
        public int NodeID
        {
            get;
            set;
        }
        /// <summary>
        /// 归档模型
        /// </summary>
        public int ModelID
        {
            get;
            set;
        }
        /// <summary>
        /// 项目简介
        /// </summary>
        public string Intro
        {
            get;
            set;
        }
        /// <summary>
        /// 目标网址是否内容页
        /// </summary>
        public bool IsContent
        {
            get;
            set;
        }
        /// <summary>
        /// 采集数量 0为不限制 其他正整数为采集循环未结束时达到成功采集数量就停止
        /// </summary>
        public int CollecteNum
        {
            get;
            set;
        }
        /// <summary>
        /// 重复内容采集模式 0忽略 1重新采集更新
        /// </summary>
        public int RptModel
        {
            get;
            set;
        }
        /// <summary>
        /// 采集间隔 缓解目标网站压力
        /// </summary>
        public int CInterval
        {
            get;
            set;
        }
        /// <summary>
        /// 上此采集执行时间
        /// </summary>
        public DateTime LastExecTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否检测通过
        /// </summary>
        public bool Detection
        {
            get;
            set;
        }
        /// <summary>
        /// 项目实例是否为空
        /// </summary>
        public bool IsNull
        {
            get;
            set;
        }
    }
}
