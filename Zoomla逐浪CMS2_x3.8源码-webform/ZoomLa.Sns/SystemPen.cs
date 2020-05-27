using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///SystemPen业务实体
    /// </summary>
    [Serializable]
    public class SystemPen
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///宠物名称
        ///</summary>
        private string penName = String.Empty;

        ///<summary>
        ///宠物图片
        ///</summary>
        private string penImage = String.Empty;

        ///<summary>
        ///宠物初始值
        ///</summary>
        private int penInitialization;

        ///<summary>
        ///宠物价格
        ///</summary>
        private decimal penPrice;

        ///<summary>
        ///宠物简介
        ///</summary>
        private string penContext = String.Empty;

        /// <summary>
        /// 宠物状态
        /// </summary>
        private int penState;

        private string marker = string.Empty;

      
        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public SystemPen()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SystemPen
        (
            Guid iD,
            string penName,
            string penImage,
            int penInitialization,
            int penPrice,
            string penContext,
            int penState
        )
        {
            this.iD = iD;
            this.penName = penName;
            this.penImage = penImage;
            this.penInitialization = penInitialization;
            this.penPrice = penPrice;
            this.penContext = penContext;
            this.penState = penState;
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
        ///宠物名称
        ///</summary>
        public string PenName
        {
            get { return penName; }
            set { penName = value; }
        }

        ///<summary>
        ///宠物图片
        ///</summary>
        public string PenImage
        {
            get { return penImage; }
            set { penImage = value; }
        }

        ///<summary>
        ///宠物初始值
        ///</summary>
        public int PenInitialization
        {
            get { return penInitialization; }
            set { penInitialization = value; }
        }

        ///<summary>
        ///宠物价格
        ///</summary>
        public decimal PenPrice
        {
            get { return penPrice; }
            set { penPrice = value; }
        }

        ///<summary>
        ///宠物简介
        ///</summary>
        public string PenContext
        {
            get { return penContext; }
            set { penContext = value; }
        }

        /// <summary>
        /// 宠物状态
        /// </summary>
        public int PenState
        {
            get { return penState; }
            set { penState = value; }
        }

        /// <summary>
        /// 汽车标志
        /// </summary>
        public string Marker
        {
            get { return marker; }
            set { marker = value; }
        }

        #endregion
    }
}
