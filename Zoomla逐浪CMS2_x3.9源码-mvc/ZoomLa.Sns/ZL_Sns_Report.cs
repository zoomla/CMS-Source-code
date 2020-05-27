using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_Reportҵ��ʵ��
    /// </summary>
    [Serializable]
    public class ZL_Sns_Report
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private int pRid;

        ///<summary>
        ///�û�������ϢID
        ///</summary>
        private int pmid;

        ///<summary>
        ///����ID
        ///</summary>
        private int pid;

        ///<summary>
        ///�����û�ID
        ///</summary>
        private int puid;

        ///<summary>
        ///��λ�û�ID
        ///</summary>
        private int r_to_uid;

        ///<summary>
        ///ͣ��ʱ��
        ///</summary>
        private DateTime r_to_time;

        ///<summary>
        ///ͣ������ 1��ͣ�ţ�2���Ѿ�Ų��
        ///</summary>
        private int r_type;


        #endregion

        #region ���캯��

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_Report()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_Report
        (
            int pRid,
            int pmid,
            int pid,
            int puid,
            int r_to_uid,
            DateTime r_to_time,
            int r_type
        )
        {
            this.pRid = pRid;
            this.pmid = pmid;
            this.pid = pid;
            this.puid = puid;
            this.r_to_uid = r_to_uid;
            this.r_to_time = r_to_time;
            this.r_type = r_type;

        }

        #endregion

        #region ���Զ���

        ///<summary>
        ///
        ///</summary>
        public int PRid
        {
            get { return pRid; }
            set { pRid = value; }
        }

        ///<summary>
        ///�û�������ϢID
        ///</summary>
        public int Pmid
        {
            get { return pmid; }
            set { pmid = value; }
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
        ///�����û�ID
        ///</summary>
        public int Puid
        {
            get { return puid; }
            set { puid = value; }
        }

        ///<summary>
        ///��λ�û�ID
        ///</summary>
        public int R_to_uid
        {
            get { return r_to_uid; }
            set { r_to_uid = value; }
        }

        ///<summary>
        ///ͣ��ʱ��
        ///</summary>
        public DateTime R_to_time
        {
            get { return r_to_time; }
            set { r_to_time = value; }
        }

        ///<summary>
        ///ͣ������ 1��ͣ�ţ�2���Ѿ�Ų��
        ///</summary>
        public int R_type
        {
            get { return r_type; }
            set { r_type = value; }
        }

        #endregion

    }
}
