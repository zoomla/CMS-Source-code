using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    public enum MyCarType
    {
        //购买
        Buy,
        //贴条
        Amerce

    }
    /// <summary>
    ///ZL_Sns_MyCar业务实体
    /// </summary>
    [Serializable]
    public class ZL_Sns_MyCar
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private int pmid;

        ///<summary>
        ///
        ///</summary>
        private int p_uid;

        ///<summary>
        ///车辆ID
        ///</summary>
        private int pid;

        ///<summary>
        ///车辆购买时间
        ///</summary>
        private DateTime p_buy_time;

        ///<summary>
        ///车辆最后停放时间
        ///</summary>
        private DateTime p_last_time;

        ///<summary>
        ///车辆最后停放的车位用户ID
        ///</summary>
        private int p_last_uid;

        ///<summary>
        ///车辆最后停放的用户
        ///</summary>
        private string p_last_user = String.Empty;

        ///<summary>
        ///停放信息ID
        ///</summary>
        private int p_action;

        private string carImage = String.Empty;


        private string carImageLog = String.Empty;

        private string carName = String.Empty;

        private string userName = String.Empty;


        #endregion

        #region 构造函数

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

        #region 属性定义

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
        ///车辆ID
        ///</summary>
        public int Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        ///<summary>
        ///车辆购买时间
        ///</summary>
        public DateTime P_buy_time
        {
            get { return p_buy_time; }
            set { p_buy_time = value; }
        }

        ///<summary>
        ///车辆最后停放时间
        ///</summary>
        public DateTime P_last_time
        {
            get { return p_last_time; }
            set { p_last_time = value; }
        }

        ///<summary>
        ///车辆最后停放的车位用户ID
        ///</summary>
        public int P_last_uid
        {
            get { return p_last_uid; }
            set { p_last_uid = value; }
        }

        ///<summary>
        ///车辆最后停放的用户
        ///</summary>
        public string P_last_user
        {
            get { return p_last_user; }
            set { p_last_user = value; }
        }

        ///<summary>
        ///停放信息ID
        ///</summary>
        public int P_action
        {
            get { return p_action; }
            set { p_action = value; }
        }

        /// <summary>
        /// 汽车图片
        /// </summary>
        public string CarImage
        {
            get { return carImage; }
            set { carImage = value; }
        }

        /// <summary>
        /// 汽车LOG
        /// </summary>
        public string CarImageLog
        {
            get { return carImageLog; }
            set { carImageLog = value; }
        }

        /// <summary>
        /// 汽车名称
        /// </summary>
        public string CarName
        {
            get { return carName; }
            set { carName = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion

    }
}
