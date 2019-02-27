using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///ZL_Sns_CarConfig业务实体
    /// </summary>
    [Serializable]
    public class ZL_Sns_CarConfig
    {
        #region 字段定义

        ///<summary>
        ///排序
        ///</summary>
        private int iD;

        ///<summary>
        ///关键字
        ///</summary>
        private string cKey = String.Empty;

        ///<summary>
        ///说明
        ///</summary>
        private string ctext = String.Empty;

        ///<summary>
        ///值
        ///</summary>
        private string cvalue = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_CarConfig()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ZL_Sns_CarConfig
        (
            int iD,
            string cKey,
            string ctext,
            string cvalue
        )
        {
            this.iD = iD;
            this.cKey = cKey;
            this.ctext = ctext;
            this.cvalue = cvalue;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///排序
        ///</summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///关键字
        ///</summary>
        public string CKey
        {
            get { return cKey; }
            set { cKey = value; }
        }

        ///<summary>
        ///说明
        ///</summary>
        public string Ctext
        {
            get { return ctext; }
            set { ctext = value; }
        }

        ///<summary>
        ///值
        ///</summary>
        public string Cvalue
        {
            get { return cvalue; }
            set { cvalue = value; }
        }

        #endregion

    }
}
