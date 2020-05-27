using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_MyPoseҵ��ʵ��
    /// </summary>
    [Serializable]
    public class ZL_Sns_MyPose
    {
        #region �ֶζ���

        ///<summary>
        ///�û�ID
        ///</summary>
        private int p_uid;

        ///<summary>
        ///��һͣ��λ�û��ɣ�
        ///</summary>
        private int p_pose_uid_1;

        ///<summary>
        ///��һͣ��λ�û���
        ///</summary>
        private string p_pose_user_1 = String.Empty;

        ///<summary>
        ///��һͣ��λ������ϢID
        ///</summary>
        private int p_pose_Pmid_1;

        ///<summary>
        ///��һͣ��λ����ID
        ///</summary>
        private int p_pose_Pid_1;

        ///<summary>
        ///�ڶ�ͣ��λ�û��ɣ�
        ///</summary>
        private int p_pose_uid_2;

        ///<summary>
        ///�ڶ�ͣ��λ�û���
        ///</summary>
        private string p_pose_user_2 = String.Empty;

        ///<summary>
        ///�ڶ�ͣ��λ������ϢID
        ///</summary>
        private int p_pose_Pmid_2;

        ///<summary>
        ///�ڶ�ͣ��λ����ID
        ///</summary>
        private int p_pose_Pid_2;

        ///<summary>
        ///����ͣ��λ�û��ɣ�
        ///</summary>
        private int p_pose_uid_3;

        ///<summary>
        ///����ͣ��λ�û���
        ///</summary>
        private string p_pose_user_3 = String.Empty;

        ///<summary>
        ///����ͣ��λ������ϢID
        ///</summary>
        private int p_pose_Pmid_3;

        ///<summary>
        ///����ͣ��λ����ID
        ///</summary>
        private int p_pose_Pid_3;

        ///<summary>
        ///����ͣ��λ�û��ɣ�
        ///</summary>
        private int p_pose_uid_4;

        ///<summary>
        ///����ͣ��λ�û���
        ///</summary>
        private string p_pose_user_4 = String.Empty;

        ///<summary>
        ///����ͣ��λ������ϢID
        ///</summary>
        private int p_pose_Pmid_4;

        ///<summary>
        ///����ͣ��λ����ID
        ///</summary>
        private int p_pose_Pid_4;


        #endregion

        #region ���캯��

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_MyPose()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_MyPose
        (
            int p_uid,
            int p_pose_uid_1,
            string p_pose_user_1,
            int p_pose_Pmid_1,
            int p_pose_Pid_1,
            int p_pose_uid_2,
            string p_pose_user_2,
            int p_pose_Pmid_2,
            int p_pose_Pid_2,
            int p_pose_uid_3,
            string p_pose_user_3,
            int p_pose_Pmid_3,
            int p_pose_Pid_3,
            int p_pose_uid_4,
            string p_pose_user_4,
            int p_pose_Pmid_4,
            int p_pose_Pid_4
        )
        {
            this.p_uid = p_uid;
            this.p_pose_uid_1 = p_pose_uid_1;
            this.p_pose_user_1 = p_pose_user_1;
            this.p_pose_Pmid_1 = p_pose_Pmid_1;
            this.p_pose_Pid_1 = p_pose_Pid_1;
            this.p_pose_uid_2 = p_pose_uid_2;
            this.p_pose_user_2 = p_pose_user_2;
            this.p_pose_Pmid_2 = p_pose_Pmid_2;
            this.p_pose_Pid_2 = p_pose_Pid_2;
            this.p_pose_uid_3 = p_pose_uid_3;
            this.p_pose_user_3 = p_pose_user_3;
            this.p_pose_Pmid_3 = p_pose_Pmid_3;
            this.p_pose_Pid_3 = p_pose_Pid_3;
            this.p_pose_uid_4 = p_pose_uid_4;
            this.p_pose_user_4 = p_pose_user_4;
            this.p_pose_Pmid_4 = p_pose_Pmid_4;
            this.p_pose_Pid_4 = p_pose_Pid_4;

        }

        #endregion

        #region ���Զ���

        ///<summary>
        ///�û�ID
        ///</summary>
        public int P_uid
        {
            get { return p_uid; }
            set { p_uid = value; }
        }

        ///<summary>
        ///��һͣ��λ�û��ɣ�
        ///</summary>
        public int P_pose_uid_1
        {
            get { return p_pose_uid_1; }
            set { p_pose_uid_1 = value; }
        }

        ///<summary>
        ///��һͣ��λ�û���
        ///</summary>
        public string P_pose_user_1
        {
            get { return p_pose_user_1; }
            set { p_pose_user_1 = value; }
        }

        ///<summary>
        ///��һͣ��λ������ϢID
        ///</summary>
        public int P_pose_Pmid_1
        {
            get { return p_pose_Pmid_1; }
            set { p_pose_Pmid_1 = value; }
        }

        ///<summary>
        ///��һͣ��λ����ID
        ///</summary>
        public int P_pose_Pid_1
        {
            get { return p_pose_Pid_1; }
            set { p_pose_Pid_1 = value; }
        }

        ///<summary>
        ///�ڶ�ͣ��λ�û��ɣ�
        ///</summary>
        public int P_pose_uid_2
        {
            get { return p_pose_uid_2; }
            set { p_pose_uid_2 = value; }
        }

        ///<summary>
        ///�ڶ�ͣ��λ�û���
        ///</summary>
        public string P_pose_user_2
        {
            get { return p_pose_user_2; }
            set { p_pose_user_2 = value; }
        }

        ///<summary>
        ///�ڶ�ͣ��λ������ϢID
        ///</summary>
        public int P_pose_Pmid_2
        {
            get { return p_pose_Pmid_2; }
            set { p_pose_Pmid_2 = value; }
        }

        ///<summary>
        ///�ڶ�ͣ��λ����ID
        ///</summary>
        public int P_pose_Pid_2
        {
            get { return p_pose_Pid_2; }
            set { p_pose_Pid_2 = value; }
        }

        ///<summary>
        ///����ͣ��λ�û��ɣ�
        ///</summary>
        public int P_pose_uid_3
        {
            get { return p_pose_uid_3; }
            set { p_pose_uid_3 = value; }
        }

        ///<summary>
        ///����ͣ��λ�û���
        ///</summary>
        public string P_pose_user_3
        {
            get { return p_pose_user_3; }
            set { p_pose_user_3 = value; }
        }

        ///<summary>
        ///����ͣ��λ������ϢID
        ///</summary>
        public int P_pose_Pmid_3
        {
            get { return p_pose_Pmid_3; }
            set { p_pose_Pmid_3 = value; }
        }

        ///<summary>
        ///����ͣ��λ����ID
        ///</summary>
        public int P_pose_Pid_3
        {
            get { return p_pose_Pid_3; }
            set { p_pose_Pid_3 = value; }
        }

        ///<summary>
        ///����ͣ��λ�û��ɣ�
        ///</summary>
        public int P_pose_uid_4
        {
            get { return p_pose_uid_4; }
            set { p_pose_uid_4 = value; }
        }

        ///<summary>
        ///����ͣ��λ�û���
        ///</summary>
        public string P_pose_user_4
        {
            get { return p_pose_user_4; }
            set { p_pose_user_4 = value; }
        }

        ///<summary>
        ///����ͣ��λ������ϢID
        ///</summary>
        public int P_pose_Pmid_4
        {
            get { return p_pose_Pmid_4; }
            set { p_pose_Pmid_4 = value; }
        }

        ///<summary>
        ///����ͣ��λ����ID
        ///</summary>
        public int P_pose_Pid_4
        {
            get { return p_pose_Pid_4; }
            set { p_pose_Pid_4 = value; }
        }

        #endregion

    }
}
