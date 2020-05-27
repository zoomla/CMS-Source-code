/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： PenFoster.cs
// 文件功能描述：定义数据表PenFoster的业务实体
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
    ///PenFoster业务实体
    /// </summary>
    [Serializable]
    public class PenFoster
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///培养项目
        ///</summary>
        private string fosterItem = String.Empty;

        ///<summary> 
        ///培养介绍
        ///</summary>
        private string fosterSynopsis = String.Empty;

        ///<summary>
        ///培养价格
        ///</summary>
        private int fosterMoney;

        ///<summary>
        ///培养得到经验
        ///</summary>
        private int fosterExperience;

        ///<summary>
        ///培养图片
        ///</summary>
        private string fosterImage = String.Empty;

        ///<summary>
        ///培养时间
        ///</summary>
        private int fosterTime;

        /// <summary>
        /// 培养支付方式
        /// </summary>
        private int fostertPayment;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public PenFoster()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PenFoster
        (
            Guid iD,
            string fosterItem,
            string fosterSynopsis,
            int fosterMoney,
            int fosterExperience,
            string fosterImage,
            int fosterTime,
            int fostertPayment
        )
        {
            this.iD = iD;
            this.fosterItem = fosterItem;
            this.fosterSynopsis = fosterSynopsis;
            this.fosterMoney = fosterMoney;
            this.fosterExperience = fosterExperience;
            this.fosterImage = fosterImage;
            this.fosterTime = fosterTime;
            this.fostertPayment = fostertPayment;
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
        ///培养项目
        ///</summary>
        public string FosterItem
        {
            get { return fosterItem; }
            set { fosterItem = value; }
        }

        ///<summary>
        ///培养介绍
        ///</summary>
        public string FosterSynopsis
        {
            get { return fosterSynopsis; }
            set { fosterSynopsis = value; }
        }

        ///<summary>
        ///培养价格
        ///</summary>
        public int FosterMoney
        {
            get { return fosterMoney; }
            set { fosterMoney = value; }
        }

        ///<summary>
        ///培养得到经验
        ///</summary>
        public int FosterExperience
        {
            get { return fosterExperience; }
            set { fosterExperience = value; }
        }

        ///<summary>
        ///培养图片
        ///</summary>
        public string FosterImage
        {
            get { return fosterImage; }
            set { fosterImage = value; }
        }

        ///<summary>
        ///培养时间
        ///</summary>
        public int FosterTime
        {
            get { return fosterTime; }
            set { fosterTime = value; }
        }

        /// <summary>
        /// 培养支付方式
        /// </summary>
        public int FostertPayment
        {
            get { return fostertPayment; }
            set { fostertPayment = value; }
        }

        #endregion

    }
}
