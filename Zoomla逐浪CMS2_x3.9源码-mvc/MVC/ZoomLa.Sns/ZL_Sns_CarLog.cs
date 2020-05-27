using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_CarLog业务实体
    /// </summary>
    [Serializable]
    public class ZL_Sns_CarLog
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private int plid;

        ///<summary>
        ///用户ID
        ///</summary>
        private int p_uid;

        ///<summary>
        ///日志类型
        ///</summary>
        private int p_type;

        ///<summary>
        ///日志内容
        ///</summary>
        private string p_content = String.Empty;

        ///<summary>
        ///添加日志时间
        ///</summary>
        private DateTime p_introtime;


        #endregion

        #region 构造函数

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

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public int Plid
        {
            get { return plid; }
            set { plid = value; }
        }

        ///<summary>
        ///用户ID
        ///</summary>
        public int P_uid
        {
            get { return p_uid; }
            set { p_uid = value; }
        }

        ///<summary>
        ///日志类型
        ///</summary>
        public int P_type
        {
            get { return p_type; }
            set { p_type = value; }
        }

        ///<summary>
        ///日志内容
        ///</summary>
        public string P_content
        {
            get { return p_content; }
            set { p_content = value; }
        }

        ///<summary>
        ///添加日志时间
        ///</summary>
        public DateTime P_introtime
        {
            get { return p_introtime; }
            set { p_introtime = value; }
        }

        #endregion

    }
}
