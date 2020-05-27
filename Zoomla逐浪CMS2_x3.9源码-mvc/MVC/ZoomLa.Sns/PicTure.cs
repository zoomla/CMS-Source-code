using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///PicTure业务实体
    /// </summary>
    [Serializable]
    public class PicTure
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///相片名称
        ///</summary>
        private string picName = String.Empty;

        ///<summary>
        ///存储路径
        ///</summary>
        private string picUrl = String.Empty;

        ///<summary>
        ///注释
        ///</summary>
        private string remark = String.Empty;

        ///<summary>
        ///相册ID
        ///</summary>
        private Guid picCategID = Guid.Empty;

        ///<summary>
        ///相片大小
        ///</summary>
        private int picSize;

        ///<summary>
        ///相片上传时间
        ///</summary>
        private DateTime picUpTime;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public PicTure()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PicTure
        (
            Guid iD,
            string picName,
            string picUrl,
            string remark,
            Guid picCategID,
            int picSize,
            DateTime picUpTime
        )
        {
            this.iD = iD;
            this.picName = picName;
            this.picUrl = picUrl;
            this.remark = remark;
            this.picCategID = picCategID;
            this.picSize = picSize;
            this.picUpTime = picUpTime;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///相片名称
        ///</summary>
        public string PicName
        {
            get { return picName; }
            set { picName = value; }
        }

        ///<summary>
        ///存储路径
        ///</summary>
        public string PicUrl
        {
            get { return picUrl; }
            set { picUrl = value; }
        }

        ///<summary>
        ///注释
        ///</summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        ///<summary>
        ///相册ID
        ///</summary>
        public Guid PicCategID
        {
            get { return picCategID; }
            set { picCategID = value; }
        }

        ///<summary>
        ///相片大小
        ///</summary>
        public int PicSize
        {
            get { return picSize; }
            set { picSize = value; }
        }

        ///<summary>
        ///相片上传时间
        ///</summary>
        public DateTime PicUpTime
        {
            get { return picUpTime; }
            set { picUpTime = value; }
        }

        #endregion

    }
}
