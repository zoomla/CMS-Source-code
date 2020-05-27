using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
    /// <summary>
    ///PicCateg
    /// 相册业务实体
    /// </summary>
    [Serializable]
    public class PicCateg
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///相册标题
        ///</summary>
        private string picCategTitle = String.Empty;

        ///<summary>
        ///所属用户
        ///</summary>
        private int picCategUserID ;

        ///<summary>
        ///标题图片
        ///</summary>
        private Guid titlePIc = Guid.Empty;

        ///<summary>
        ///相册状态
        ///</summary>
        private int state;

        ///<summary>
        ///相册创建时间
        ///</summary>
        private DateTime categTime;
        /// <summary>
        /// 相册密码
        /// </summary>
        private string picCategPws;
        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public PicCateg()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PicCateg
        (
            Guid iD,
            string picCategTitle,
            int picCategUserID,
            Guid titlePIc,
            int state,
            DateTime categTime,
            string picCategPws
        )
        {
            this.iD = iD;
            this.picCategTitle = picCategTitle;
            this.picCategUserID = picCategUserID;
            this.titlePIc = titlePIc;
            this.state = state;
            this.categTime = categTime;
            this.picCategPws = picCategPws;
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
        ///相册标题
        ///</summary>
        public string PicCategTitle
        {
            get { return picCategTitle; }
            set { picCategTitle = value; }
        }

        ///<summary>
        ///所属用户
        ///</summary>
        public int PicCategUserID
        {
            get { return picCategUserID; }
            set { picCategUserID = value; }
        }

        ///<summary>
        ///标题图片
        ///</summary>
        public Guid TitlePIc
        {
            get {
                return titlePIc;
            }
            set { titlePIc = value; }
        }

        ///<summary>
        ///相册状态
        ///</summary>
        public int State
        {
            get { return state; }
            set { state = value; }
        }

        ///<summary>
        ///相册创建时间
        ///</summary>
        public DateTime CategTime
        {
            get { return categTime; }
            set { categTime = value; }
        }
        private string titlePIcUrl;


        /// <summary>
        /// 相册图标
        /// </summary>
        public string TitlePIcUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(titlePIcUrl))
                    return titlePIcUrl;
                else
                    return "~/UploadFiles/nopic.gif";
            }
            set { titlePIcUrl = value; }
        }

        public string PicCategPws
        {
            get { return picCategPws; }
            set { picCategPws = value; }
        }
        #endregion

    }
}
