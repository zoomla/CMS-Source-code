using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_MyPose业务实体
    /// </summary>
    [Serializable]
    public class ZL_Sns_MyPose
    {
        #region 字段定义

        ///<summary>
        ///用户ID
        ///</summary>
        private int p_uid;

        ///<summary>
        ///第一停车位用户ＩＤ
        ///</summary>
        private int p_pose_uid_1;

        ///<summary>
        ///第一停车位用户名
        ///</summary>
        private string p_pose_user_1 = String.Empty;

        ///<summary>
        ///第一停车位车辆信息ID
        ///</summary>
        private int p_pose_Pmid_1;

        ///<summary>
        ///第一停车位车辆ID
        ///</summary>
        private int p_pose_Pid_1;

        ///<summary>
        ///第二停车位用户ＩＤ
        ///</summary>
        private int p_pose_uid_2;

        ///<summary>
        ///第二停车位用户名
        ///</summary>
        private string p_pose_user_2 = String.Empty;

        ///<summary>
        ///第二停车位车辆信息ID
        ///</summary>
        private int p_pose_Pmid_2;

        ///<summary>
        ///第二停车位车辆ID
        ///</summary>
        private int p_pose_Pid_2;

        ///<summary>
        ///第三停车位用户ＩＤ
        ///</summary>
        private int p_pose_uid_3;

        ///<summary>
        ///第三停车位用户名
        ///</summary>
        private string p_pose_user_3 = String.Empty;

        ///<summary>
        ///第三停车位车辆信息ID
        ///</summary>
        private int p_pose_Pmid_3;

        ///<summary>
        ///第三停车位车辆ID
        ///</summary>
        private int p_pose_Pid_3;

        ///<summary>
        ///第四停车位用户ＩＤ
        ///</summary>
        private int p_pose_uid_4;

        ///<summary>
        ///第四停车位用户名
        ///</summary>
        private string p_pose_user_4 = String.Empty;

        ///<summary>
        ///第四停车位车辆信息ID
        ///</summary>
        private int p_pose_Pmid_4;

        ///<summary>
        ///第四停车位车辆ID
        ///</summary>
        private int p_pose_Pid_4;


        #endregion

        #region 构造函数

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

        #region 属性定义

        ///<summary>
        ///用户ID
        ///</summary>
        public int P_uid
        {
            get { return p_uid; }
            set { p_uid = value; }
        }

        ///<summary>
        ///第一停车位用户ＩＤ
        ///</summary>
        public int P_pose_uid_1
        {
            get { return p_pose_uid_1; }
            set { p_pose_uid_1 = value; }
        }

        ///<summary>
        ///第一停车位用户名
        ///</summary>
        public string P_pose_user_1
        {
            get { return p_pose_user_1; }
            set { p_pose_user_1 = value; }
        }

        ///<summary>
        ///第一停车位车辆信息ID
        ///</summary>
        public int P_pose_Pmid_1
        {
            get { return p_pose_Pmid_1; }
            set { p_pose_Pmid_1 = value; }
        }

        ///<summary>
        ///第一停车位车辆ID
        ///</summary>
        public int P_pose_Pid_1
        {
            get { return p_pose_Pid_1; }
            set { p_pose_Pid_1 = value; }
        }

        ///<summary>
        ///第二停车位用户ＩＤ
        ///</summary>
        public int P_pose_uid_2
        {
            get { return p_pose_uid_2; }
            set { p_pose_uid_2 = value; }
        }

        ///<summary>
        ///第二停车位用户名
        ///</summary>
        public string P_pose_user_2
        {
            get { return p_pose_user_2; }
            set { p_pose_user_2 = value; }
        }

        ///<summary>
        ///第二停车位车辆信息ID
        ///</summary>
        public int P_pose_Pmid_2
        {
            get { return p_pose_Pmid_2; }
            set { p_pose_Pmid_2 = value; }
        }

        ///<summary>
        ///第二停车位车辆ID
        ///</summary>
        public int P_pose_Pid_2
        {
            get { return p_pose_Pid_2; }
            set { p_pose_Pid_2 = value; }
        }

        ///<summary>
        ///第三停车位用户ＩＤ
        ///</summary>
        public int P_pose_uid_3
        {
            get { return p_pose_uid_3; }
            set { p_pose_uid_3 = value; }
        }

        ///<summary>
        ///第三停车位用户名
        ///</summary>
        public string P_pose_user_3
        {
            get { return p_pose_user_3; }
            set { p_pose_user_3 = value; }
        }

        ///<summary>
        ///第三停车位车辆信息ID
        ///</summary>
        public int P_pose_Pmid_3
        {
            get { return p_pose_Pmid_3; }
            set { p_pose_Pmid_3 = value; }
        }

        ///<summary>
        ///第三停车位车辆ID
        ///</summary>
        public int P_pose_Pid_3
        {
            get { return p_pose_Pid_3; }
            set { p_pose_Pid_3 = value; }
        }

        ///<summary>
        ///第四停车位用户ＩＤ
        ///</summary>
        public int P_pose_uid_4
        {
            get { return p_pose_uid_4; }
            set { p_pose_uid_4 = value; }
        }

        ///<summary>
        ///第四停车位用户名
        ///</summary>
        public string P_pose_user_4
        {
            get { return p_pose_user_4; }
            set { p_pose_user_4 = value; }
        }

        ///<summary>
        ///第四停车位车辆信息ID
        ///</summary>
        public int P_pose_Pmid_4
        {
            get { return p_pose_Pmid_4; }
            set { p_pose_Pmid_4 = value; }
        }

        ///<summary>
        ///第四停车位车辆ID
        ///</summary>
        public int P_pose_Pid_4
        {
            get { return p_pose_Pid_4; }
            set { p_pose_Pid_4 = value; }
        }

        #endregion

    }
}
