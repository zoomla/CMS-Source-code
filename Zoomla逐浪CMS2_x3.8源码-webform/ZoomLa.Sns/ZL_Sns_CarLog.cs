using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_CarLogҵ��ʵ��
    /// </summary>
    [Serializable]
    public class ZL_Sns_CarLog
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private int plid;

        ///<summary>
        ///�û�ID
        ///</summary>
        private int p_uid;

        ///<summary>
        ///��־����
        ///</summary>
        private int p_type;

        ///<summary>
        ///��־����
        ///</summary>
        private string p_content = String.Empty;

        ///<summary>
        ///�����־ʱ��
        ///</summary>
        private DateTime p_introtime;


        #endregion

        #region ���캯��

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_CarLog()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_CarLog
        (
            int plid,
            int p_uid,
            int p_type,
            string p_content,
            DateTime p_introtime
        )
        {
            this.plid = plid;
            this.p_uid = p_uid;
            this.p_type = p_type;
            this.p_content = p_content;
            this.p_introtime = p_introtime;

        }

        #endregion

        #region ���Զ���

        ///<summary>
        ///
        ///</summary>
        public int Plid
        {
            get { return plid; }
            set { plid = value; }
        }

        ///<summary>
        ///�û�ID
        ///</summary>
        public int P_uid
        {
            get { return p_uid; }
            set { p_uid = value; }
        }

        ///<summary>
        ///��־����
        ///</summary>
        public int P_type
        {
            get { return p_type; }
            set { p_type = value; }
        }

        ///<summary>
        ///��־����
        ///</summary>
        public string P_content
        {
            get { return p_content; }
            set { p_content = value; }
        }

        ///<summary>
        ///�����־ʱ��
        ///</summary>
        public DateTime P_introtime
        {
            get { return p_introtime; }
            set { p_introtime = value; }
        }

        #endregion

    }
}
