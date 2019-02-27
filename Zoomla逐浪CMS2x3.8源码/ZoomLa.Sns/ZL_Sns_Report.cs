using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_Report业务实体
    /// </summary>
    [Serializable]
    public class ZL_Sns_Report
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private int pRid;

        ///<summary>
        ///用户车辆信息ID
        ///</summary>
        private int pmid;

        ///<summary>
        ///车辆ID
        ///</summary>
        private int pid;

        ///<summary>
        ///车辆用户ID
        ///</summary>
        private int puid;

        ///<summary>
        ///车位用户ID
        ///</summary>
        private int r_to_uid;

        ///<summary>
        ///停放时间
        ///</summary>
        private DateTime r_to_time;

        ///<summary>
        ///停放类型 1，停放；2，已经挪走
        ///</summary>
        private int r_type;


        #endregion

        #region 构造函数

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

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public int PRid
        {
            get { return pRid; }
            set { pRid = value; }
        }

        ///<summary>
        ///用户车辆信息ID
        ///</summary>
        public int Pmid
        {
            get { return pmid; }
            set { pmid = value; }
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
        ///车辆用户ID
        ///</summary>
        public int Puid
        {
            get { return puid; }
            set { puid = value; }
        }

        ///<summary>
        ///车位用户ID
        ///</summary>
        public int R_to_uid
        {
            get { return r_to_uid; }
            set { r_to_uid = value; }
        }

        ///<summary>
        ///停放时间
        ///</summary>
        public DateTime R_to_time
        {
            get { return r_to_time; }
            set { r_to_time = value; }
        }

        ///<summary>
        ///停放类型 1，停放；2，已经挪走
        ///</summary>
        public int R_type
        {
            get { return r_type; }
            set { r_type = value; }
        }

        #endregion

    }
}
