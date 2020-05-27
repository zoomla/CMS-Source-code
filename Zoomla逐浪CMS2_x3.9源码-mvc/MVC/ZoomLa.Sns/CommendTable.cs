using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    #region CommendTableö������

    public enum CommendTableType
    {
        /// <summary>
        /// ����
        /// </summary>
        ElseType,
        /// <summary>
        /// �鼮
        /// </summary>
        Book,
        /// <summary>
        /// ��Ӱ
        /// </summary>
        Cinema,
        /// <summary>
        /// ����
        /// </summary>
        Topic,
        /// <summary>
        /// ��Ⱥ
        /// </summary>
        Group,
        /// <summary>
        /// �
        /// </summary>
        Activity,
        /// <summary>
        /// ������
        /// </summary>
        NullType
    }
    #endregion
    

    /// <summary>
    ///CommendTableҵ��ʵ��
    /// </summary>
    [Serializable]
    public class CommendTable
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///�Ƽ�����
        ///</summary>
        private string commendTitle = String.Empty;

        ///<summary>
        ///�Ƽ�����
        ///</summary>
        private string commendContext = String.Empty;

        ///<summary>
        ///�Ƽ���ַ
        ///</summary>
        private string commendUrl = String.Empty;

        ///<summary>
        ///�Ƽ��û�ID
        ///</summary>
        private Guid commendUserID = Guid.Empty;

        ///<summary>
        ///�Ƽ�ͼƬ
        ///</summary>
        private string commendImage = String.Empty;

        ///<summary>
        ///�Ƽ�����
        ///</summary>
        private int commendType;

        ///<summary>
        ///�Ƽ�ʱ��
        ///</summary>
        private DateTime commendTime;

        ///<summary>
        ///�û���
        ///</summary>
        private string userName = String.Empty;


        #endregion

        #region ���캯��

        ///<summary>
        ///
        ///</summary>
        public CommendTable()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public CommendTable
        (
            Guid iD,
            string commendTitle,
            string commendContext,
            string commendUrl,
            Guid commendUserID,
            string commendImage,
            int commendType,
            DateTime commendTime,
            string userName
        )
        {
            this.iD = iD;
            this.commendTitle = commendTitle;
            this.commendContext = commendContext;
            this.commendUrl = commendUrl;
            this.commendUserID = commendUserID;
            this.commendImage = commendImage;
            this.commendType = commendType;
            this.commendTime = commendTime;
            this.userName = userName;

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
        ///�Ƽ�����
        ///</summary>
        public string CommendTitle
        {
            get { return commendTitle; }
            set { commendTitle = value; }
        }

        ///<summary>
        ///�Ƽ�����
        ///</summary>
        public string CommendContext
        {
            get { return commendContext; }
            set { commendContext = value; }
        }

        ///<summary>
        ///�Ƽ���ַ
        ///</summary>
        public string CommendUrl
        {
            get { return commendUrl; }
            set { commendUrl = value; }
        }

        ///<summary>
        ///�Ƽ��û�ID
        ///</summary>
        public Guid CommendUserID
        {
            get { return commendUserID; }
            set { commendUserID = value; }
        }

        ///<summary>
        ///�Ƽ�ͼƬ
        ///</summary>
        public string CommendImage
        {
            get { return commendImage == string.Empty ? "" : "<img src='" + commendImage + "' />"; }
            set { commendImage = value; }
        }

        ///<summary>
        ///�Ƽ�����
        ///</summary>
        public int CommendType
        {
            get { return commendType; }
            set { commendType = value; }
        }

        ///<summary>
        ///�Ƽ�ʱ��
        ///</summary>
        public DateTime CommendTime
        {
            get { return commendTime; }
            set { commendTime = value; }
        }

        ///<summary>
        ///�û���
        ///</summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        #endregion

    }
}
