using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    public enum MyCarType
    {
        //����
        Buy,
        //����
        Amerce

    }
    /// <summary>
    ///ZL_Sns_MyCarҵ��ʵ��
    /// </summary>
    [Serializable]
    public class ZL_Sns_MyCar
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private int pmid;

        ///<summary>
        ///
        ///</summary>
        private int p_uid;

        ///<summary>
        ///����ID
        ///</summary>
        private int pid;

        ///<summary>
        ///��������ʱ��
        ///</summary>
        private DateTime p_buy_time;

        ///<summary>
        ///�������ͣ��ʱ��
        ///</summary>
        private DateTime p_last_time;

        ///<summary>
        ///�������ͣ�ŵĳ�λ�û�ID
        ///</summary>
        private int p_last_uid;

        ///<summary>
        ///�������ͣ�ŵ��û�
        ///</summary>
        private string p_last_user = String.Empty;

        ///<summary>
        ///ͣ����ϢID
        ///</summary>
        private int p_action;

        private string carImage = String.Empty;


        private string carImageLog = String.Empty;

        private string carName = String.Empty;

        private string userName = String.Empty;


        #endregion

        #region ���캯��

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_MyCar()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_MyCar
        (
            int pmid,
            int p_uid,
            int pid,
            DateTime p_buy_time,
            DateTime p_last_time,
            int p_last_uid,
            string p_last_user,
            int p_action,
            string carImage,
            string carImageLog,
            string carName,
            string userName
        )
        {
            this.pmid = pmid;
            this.p_uid = p_uid;
            this.pid = pid;
            this.p_buy_time = p_buy_time;
            this.p_last_time = p_last_time;
            this.p_last_uid = p_last_uid;
            this.p_last_user = p_last_user;
            this.p_action = p_action;
            this.carImage = carImage;
            this.carImageLog = carImageLog;
            this.carName = carName;
            this.userName = userName;
        }

        #endregion

        #region ���Զ���

        ///<summary>
        ///
        ///</summary>
        public int Pmid
        {
            get { return pmid; }
            set { pmid = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public int P_uid
        {
            get { return p_uid; }
            set { p_uid = value; }
        }

        ///<summary>
        ///����ID
        ///</summary>
        public int Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        ///<summary>
        ///��������ʱ��
        ///</summary>
        public DateTime P_buy_time
        {
            get { return p_buy_time; }
            set { p_buy_time = value; }
        }

        ///<summary>
        ///�������ͣ��ʱ��
        ///</summary>
        public DateTime P_last_time
        {
            get { return p_last_time; }
            set { p_last_time = value; }
        }

        ///<summary>
        ///�������ͣ�ŵĳ�λ�û�ID
        ///</summary>
        public int P_last_uid
        {
            get { return p_last_uid; }
            set { p_last_uid = value; }
        }

        ///<summary>
        ///�������ͣ�ŵ��û�
        ///</summary>
        public string P_last_user
        {
            get { return p_last_user; }
            set { p_last_user = value; }
        }

        ///<summary>
        ///ͣ����ϢID
        ///</summary>
        public int P_action
        {
            get { return p_action; }
            set { p_action = value; }
        }

        /// <summary>
        /// ����ͼƬ
        /// </summary>
        public string CarImage
        {
            get { return carImage; }
            set { carImage = value; }
        }

        /// <summary>
        /// ����LOG
        /// </summary>
        public string CarImageLog
        {
            get { return carImageLog; }
            set { carImageLog = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string CarName
        {
            get { return carName; }
            set { carName = value; }
        }

        /// <summary>
        /// �û���
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion

    }
}
