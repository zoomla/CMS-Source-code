using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///I4GiveUҵ��ʵ��
    /// </summary>
    [Serializable]
    public class I4GiveU
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///�����ID
        ///</summary>
        private Guid confessID = Guid.Empty;

        ///<summary>
        ///�������
        ///</summary>
        private string byConfessID = String.Empty;

        ///<summary>
        ///������
        ///</summary>
        private int confessType ;

        ///<summary>
        ///�������
        ///</summary>
        private string confessContext = String.Empty;

        ///<summary>
        ///���ʱ��
        ///</summary>
        private DateTime confessTime;

        /// <summary>
        /// ��ڱ���
        /// </summary>
        private string confessTitle = String.Empty;

        #endregion

        #region ���캯��

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

        #region ���Զ���

        ///<summary>
        ///
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///�����ID
        ///</summary>
        public Guid ConfessID
        {
            get { return confessID; }
            set { confessID = value; }
        }

        ///<summary>
        ///�������
        ///</summary>
        public string ByConfessID
        {
            get { return byConfessID; }
            set { byConfessID = value; }
        }

        ///<summary>
        ///������
        ///</summary>
        public int ConfessType
        {
            get { return confessType; }
            set { confessType = value; }
        }

        ///<summary>
        ///�������
        ///</summary>
        public string ConfessContext
        {
            get { return confessContext; }
            set { confessContext = value; }
        }

        ///<summary>
        ///���ʱ��
        ///</summary>
        public DateTime ConfessTime
        {
            get { return confessTime; }
            set { confessTime = value; }
        }

        ///<summary>
        ///��ڱ���
        ///</summary>
        public string  ConfessTitle
        {
            get { return confessTitle; }
            set { confessTitle = value; }
        }
        #endregion

    }
}
