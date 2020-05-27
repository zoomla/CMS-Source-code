/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： Servant.cs
// 文件功能描述：定义数据表Servant的业务实体
//
// 创建标识：Owner(2008-10-14) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///Servant业务实体
    /// </summary>
    [Serializable]
    public class Servant
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///仆人ID
        ///</summary>
        private Guid servantID = Guid.Empty;

        ///<summary>
        ///主人ID
        ///</summary>
        private Guid masterID = Guid.Empty;

        ///<summary>
        ///购买价格
        ///</summary>
        private decimal buyMoney;

        ///<summary>
        ///购买时间
        ///</summary>
        private DateTime buyTime;

        ///<summary>
        ///卖出时间
        ///</summary>
        private DateTime averageTime;

        ///<summary>
        ///状态 1：被买，2：待出售，3：释放，4：赎身，5：禁止买卖
        ///</summary>
        private int stat;

        ///<summary>
        ///虐待次数
        ///</summary>
        private int abuseNum;

        ///<summary>
        ///虐待时间
        ///</summary>
        private DateTime abuseTime;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public Servant()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Servant
        (
            Guid iD,
            Guid servantID,
            Guid masterID,
            decimal buyMoney,
            DateTime buyTime,
            DateTime averageTime,
            int stat,
            int abuseNum,
            DateTime abuseTime
        )
        {
            this.iD = iD;
            this.servantID = servantID;
            this.masterID = masterID;
            this.buyMoney = buyMoney;
            this.buyTime = buyTime;
            this.averageTime = averageTime;
            this.stat = stat;
            this.abuseNum = abuseNum;
            this.abuseTime = abuseTime;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///仆人ID
        ///</summary>
        public Guid ServantID
        {
            get { return servantID; }
            set { servantID = value; }
        }

        ///<summary>
        ///主人ID
        ///</summary>
        public Guid MasterID
        {
            get { return masterID; }
            set { masterID = value; }
        }

        ///<summary>
        ///购买价格
        ///</summary>
        public decimal BuyMoney
        {
            get { return buyMoney; }
            set { buyMoney = value; }
        }

        ///<summary>
        ///购买时间
        ///</summary>
        public DateTime BuyTime
        {
            get { return buyTime; }
            set { buyTime = value; }
        }

        ///<summary>
        ///卖出时间
        ///</summary>
        public DateTime AverageTime
        {
            get { return averageTime; }
            set { averageTime = value; }
        }

        ///<summary>
        ///状态 1：被买，2：待出售，3：释放，4：赎身，5：禁止买卖
        ///</summary>
        public int Stat
        {
            get { return stat; }
            set { stat = value; }
        }

        ///<summary>
        ///虐待次数
        ///</summary>
        public int AbuseNum
        {
            get { return abuseNum; }
            set { abuseNum = value; }
        }

        ///<summary>
        ///虐待时间
        ///</summary>
        public DateTime AbuseTime
        {
            get { return abuseTime; }
            set { abuseTime = value; }
        }

        #endregion

    }
}
