using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///SystemPenҵ��ʵ��
    /// </summary>
    [Serializable]
    public class SystemPen
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///��������
        ///</summary>
        private string penName = String.Empty;

        ///<summary>
        ///����ͼƬ
        ///</summary>
        private string penImage = String.Empty;

        ///<summary>
        ///�����ʼֵ
        ///</summary>
        private int penInitialization;

        ///<summary>
        ///����۸�
        ///</summary>
        private decimal penPrice;

        ///<summary>
        ///������
        ///</summary>
        private string penContext = String.Empty;

        /// <summary>
        /// ����״̬
        /// </summary>
        private int penState;

        private string marker = string.Empty;

      
        #endregion

        #region ���캯��

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
        ///��������
        ///</summary>
        public string PenName
        {
            get { return penName; }
            set { penName = value; }
        }

        ///<summary>
        ///����ͼƬ
        ///</summary>
        public string PenImage
        {
            get { return penImage; }
            set { penImage = value; }
        }

        ///<summary>
        ///�����ʼֵ
        ///</summary>
        public int PenInitialization
        {
            get { return penInitialization; }
            set { penInitialization = value; }
        }

        ///<summary>
        ///����۸�
        ///</summary>
        public decimal PenPrice
        {
            get { return penPrice; }
            set { penPrice = value; }
        }

        ///<summary>
        ///������
        ///</summary>
        public string PenContext
        {
            get { return penContext; }
            set { penContext = value; }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public int PenState
        {
            get { return penState; }
            set { penState = value; }
        }

        /// <summary>
        /// ������־
        /// </summary>
        public string Marker
        {
            get { return marker; }
            set { marker = value; }
        }

        #endregion
    }
}
