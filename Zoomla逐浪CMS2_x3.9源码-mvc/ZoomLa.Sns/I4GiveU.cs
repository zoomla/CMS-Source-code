using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///I4GiveU业务实体
    /// </summary>
    [Serializable]
    public class I4GiveU
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///忏悔人ID
        ///</summary>
        private Guid confessID = Guid.Empty;

        ///<summary>
        ///被忏悔人
        ///</summary>
        private string byConfessID = String.Empty;

        ///<summary>
        ///忏悔类别
        ///</summary>
        private int confessType ;

        ///<summary>
        ///忏悔事情
        ///</summary>
        private string confessContext = String.Empty;

        ///<summary>
        ///忏悔时间
        ///</summary>
        private DateTime confessTime;

        /// <summary>
        /// 忏悔标题
        /// </summary>
        private string confessTitle = String.Empty;

        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public I4GiveU()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public I4GiveU
        (
            Guid iD,
            Guid confessID,
            string byConfessID,
            int confessType,
            string confessContext,
            DateTime confessTime,
            string confessTitle
        )
        {
            this.iD = iD;
            this.confessID = confessID;
            this.byConfessID = byConfessID;
            this.confessType = confessType;
            this.confessContext = confessContext;
            this.confessTime = confessTime;
            this.confessTitle = confessTitle;
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
        ///忏悔人ID
        ///</summary>
        public Guid ConfessID
        {
            get { return confessID; }
            set { confessID = value; }
        }

        ///<summary>
        ///被忏悔人
        ///</summary>
        public string ByConfessID
        {
            get { return byConfessID; }
            set { byConfessID = value; }
        }

        ///<summary>
        ///忏悔类别
        ///</summary>
        public int ConfessType
        {
            get { return confessType; }
            set { confessType = value; }
        }

        ///<summary>
        ///忏悔事情
        ///</summary>
        public string ConfessContext
        {
            get { return confessContext; }
            set { confessContext = value; }
        }

        ///<summary>
        ///忏悔时间
        ///</summary>
        public DateTime ConfessTime
        {
            get { return confessTime; }
            set { confessTime = value; }
        }

        ///<summary>
        ///忏悔标题
        ///</summary>
        public string  ConfessTitle
        {
            get { return confessTitle; }
            set { confessTitle = value; }
        }
        #endregion

    }
}
