using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///I4GiveUForgive业务实体
    /// </summary>
    [Serializable]
    public class I4GiveUForgive
    {
        #region 字段定义

        ///<summary>
        ///忏悔ID
        ///</summary>
        private Guid confessID = Guid.Empty;

        ///<summary>
        ///原谅人ID
        ///</summary>
        private Guid forgiveID = Guid.Empty;

        ///<summary>
        ///原谅时间
        ///</summary>
        private DateTime forgiveTime;

        /// <summary>
        /// 原谅人名称
        /// </summary>
        private string userName;

        /// <summary>
        /// 头像图片
        /// </summary>
        private string image;

        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public I4GiveUForgive()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public I4GiveUForgive
        (
            Guid confessID,
            Guid forgiveID,
            DateTime forgiveTime,
            string userName,
            string image
        )
        {
            this.confessID = confessID;
            this.forgiveID = forgiveID;
            this.forgiveTime = forgiveTime;
            this.userName = userName;
            this.image = image;
        }

        #endregion

        #region 属性定义

        ///<summary>
        ///忏悔ID
        ///</summary>
        public Guid ConfessID
        {
            get { return confessID; }
            set { confessID = value; }
        }

        ///<summary>
        ///原谅人ID
        ///</summary>
        public Guid ForgiveID
        {
            get { return forgiveID; }
            set { forgiveID = value; }
        }

        ///<summary>
        ///原谅时间
        ///</summary>
        public DateTime ForgiveTime
        {
            get { return forgiveTime; }
            set { forgiveTime = value; }
        }

        /// <summary>
        /// 原谅人名称
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 头像图片
        /// </summary>
        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        #endregion

    }
}
