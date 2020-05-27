/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： Fortune.cs
// 文件功能描述：定义数据表Fortune的业务实体
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
    ///Fortune业务实体
    /// </summary>
    [Serializable]
    public class Fortune
    {
        #region 字段定义

        ///<summary>
        ///用户ID
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///兜币
        ///</summary>
        private decimal booDouMoney;

        ///<summary>
        ///现金
        ///</summary>
        private decimal cash;

        ///<summary>
        ///精神财富
        ///</summary>
        private decimal energyMoney;

        ///<summary>
        ///消耗精神
        ///</summary>
        private decimal consumeMoney;
        /// <summary>
        /// 用户名称
        /// </summary>
        private string userName;
        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public Fortune()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Fortune
        (
            Guid userID,
            int booDouMoney,
            int cash,
            int energyMoney,
            int consumeMoney,
            string userName
        )
        {
            this.userID = userID;
            this.booDouMoney = booDouMoney;
            this.cash = cash;
            this.energyMoney = energyMoney;
            this.consumeMoney = consumeMoney;
            this.userName = userName;
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
        ///兜币
        ///</summary>
        public decimal BooDouMoney
        {
            get { return booDouMoney; }
            set { booDouMoney = value; }
        }

        ///<summary>
        ///现金
        ///</summary>
        public decimal Cash
        {
            get { return cash; }
            set { cash = value; }
        }

        ///<summary>
        ///精神财富
        ///</summary>
        public decimal EnergyMoney
        {
            get { return energyMoney; }
            set { energyMoney = value; }
        }

        ///<summary>
        ///消耗精神
        ///</summary>
        public decimal ConsumeMoney
        {
            get { return consumeMoney; }
            set { consumeMoney = value; }
        }
        

        ///<summary>
        ///用户名
        ///</summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion

    }
}
