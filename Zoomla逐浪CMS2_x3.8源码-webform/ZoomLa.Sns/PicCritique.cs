using System;
using System.Collections.Generic;
using System.Text;

namespace BDUModel
{
   	/// <summary>
	///PicCritique业务实体
	/// </summary>
    [Serializable]
    public class PicCritique
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///评论内容
        ///</summary>
        private string critiqueContent = String.Empty;

        ///<summary>
        ///相片ＩＤ
        ///</summary>
        private Guid picID = Guid.Empty;

        ///<summary>
        ///评论人ＩＤ
        ///</summary>
        private int userID ;

        ///<summary>
        ///发表评论的时间
        ///</summary>
        private DateTime critiqueTime;

        /// <summary>
        /// 评论人昵称
        /// </summary>
        private string userName = String.Empty;

        ///<summary>
        ///评论人头相
        ///</summary>
        private string userPic = String.Empty;

        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public PicCritique()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PicCritique
        (
            Guid iD,
            string critiqueContent,
            Guid picID,
            int userID,
            DateTime critiqueTime,
            string userName,
            string userPic
        )
        {
            this.iD = iD;
            this.critiqueContent = critiqueContent;
            this.picID = picID;
            this.userID = userID;
            this.critiqueTime = critiqueTime;
            this.userName = userName;
            this.userPic = userPic;
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
        ///评论内容
        ///</summary>
        public string CritiqueContent
        {
            get { return critiqueContent; }
            set { critiqueContent = value; }
        }

        ///<summary>
        ///相片ＩＤ
        ///</summary>
        public Guid PicID
        {
            get { return picID; }
            set { picID = value; }
        }

        ///<summary>
        ///评论人ＩＤ
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///发表评论的时间
        ///</summary>
        public DateTime CritiqueTime
        {
            get { return critiqueTime; }
            set { critiqueTime = value; }
        }

        ///<summary>
        ///用户名称
        ///</summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        ///<summary>
        ///用户头相
        ///</summary>
        public string UserPic
        {
            get { return userPic; }
            set { userPic = value; }
        }
        #endregion
    }
}
