using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///I4GiveUForgiveҵ��ʵ��
    /// </summary>
    [Serializable]
    public class I4GiveUForgive
    {
        #region �ֶζ���

        ///<summary>
        ///���ID
        ///</summary>
        private Guid confessID = Guid.Empty;

        ///<summary>
        ///ԭ����ID
        ///</summary>
        private Guid forgiveID = Guid.Empty;

        ///<summary>
        ///ԭ��ʱ��
        ///</summary>
        private DateTime forgiveTime;

        /// <summary>
        /// ԭ��������
        /// </summary>
        private string userName;

        /// <summary>
        /// ͷ��ͼƬ
        /// </summary>
        private string image;

        #endregion

        #region ���캯��

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

        #region ���Զ���

        ///<summary>
        ///���ID
        ///</summary>
        public Guid ConfessID
        {
            get { return confessID; }
            set { confessID = value; }
        }

        ///<summary>
        ///ԭ����ID
        ///</summary>
        public Guid ForgiveID
        {
            get { return forgiveID; }
            set { forgiveID = value; }
        }

        ///<summary>
        ///ԭ��ʱ��
        ///</summary>
        public DateTime ForgiveTime
        {
            get { return forgiveTime; }
            set { forgiveTime = value; }
        }

        /// <summary>
        /// ԭ��������
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// ͷ��ͼƬ
        /// </summary>
        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        #endregion

    }
}
