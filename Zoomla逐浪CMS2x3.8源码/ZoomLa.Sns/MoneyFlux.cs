/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： MoneyFlux.cs
// 文件功能描述：定义数据表MoneyFlux的业务实体
//
// 创建标识：Owner(2008-10-14) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    public enum MoneyFluxType
    {
        /// <summary>
        /// 流入
        /// </summary>
        Afflux,
        /// <summary>
        /// 流出
        /// </summary>
        Pour,
        /// <summary>
        /// 兑换
        /// </summary>
        Change,
        /// <summary>
        /// 兑换兜币
        /// </summary>
        DBChange
    }
    /// <summary>
    ///MoneyFlux业务实体
    /// </summary>
    [Serializable]
    public class MoneyFlux
    {
        #region 字段定义

        ///<summary>
        ///用户ID
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///流向类型
        ///</summary>
        private int occurtype;

        ///<summary>
        ///现金流量
        ///</summary>
        private decimal  moneyQuantity;

        ///<summary>
        ///发生时间
        ///</summary>
        private DateTime occurTime;

        ///<summary>
        ///日志ID
        ///</summary>
        private Guid golid = Guid.Empty;

        /// <summary>
        /// 日志记录
        /// </summary>
        private string note = string.Empty;
        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public MoneyFlux()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public MoneyFlux
        (
            Guid userID,
            int occurtype,
            int moneyQuantity,
            DateTime occurTime,
            Guid golid,
            string note
        )
        {
            this.userID = userID;
            this.occurtype = occurtype;
            this.moneyQuantity = moneyQuantity;
            this.occurTime = occurTime;
            this.golid = golid;
            this.note = note;
        }

        #endregion

        #region 属性定义

        ///<summary>
        ///用户ID
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///流向类型
        ///</summary>
        public int Occurtype
        {
            get { return occurtype; }
            set { occurtype = value; }
        }

        ///<summary>
        ///现金流量
        ///</summary>
        public decimal MoneyQuantity
        {
            get { return moneyQuantity; }
            set { moneyQuantity = value; }
        }

        ///<summary>
        ///发生时间
        ///</summary>
        public DateTime OccurTime
        {
            get { return occurTime; }
            set { occurTime = value; }
        }

        ///<summary>
        ///日志ID
        ///</summary>
        public Guid GolID
        {
            get { return golid; }
            set { golid = value; }
        }

        ///<summary>
        ///记录
        ///</summary>
        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        #endregion

    }
}
