/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： GSHuatee.cs
// 文件功能描述：定义数据表GSHuatee的业务实体
//
// 创建标识：Owner(2008-10-11) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///GSHuatee业务实体
    /// </summary>
    [Serializable]
    public class GSHuatee
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///群编号
        ///</summary>
        private Guid gSID = Guid.Empty;

        ///<summary>
        ///发表话题的人
        ///</summary>
        private int  userID ;

        ///<summary>
        ///阅读次数
        ///</summary>
        private int readCount;

        ///<summary>
        ///回复次数
        ///</summary>
        private int revertCount;

        ///<summary>
        ///内容
        ///</summary>
        private string huaTeeContent = String.Empty;

        ///<summary>
        ///话题标题
        ///</summary>
        private string huaTeeTitle = String.Empty;

        private string userName = string.Empty;

        ///<summary>
        ///用户头相
        ///</summary>
        private string userpic = String.Empty;

        private DateTime lastTime;

        private DateTime creatTime;


      

       
        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public GSHuatee()
        {
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
        ///群编号
        ///</summary>
        public Guid GSID
        {
            get { return gSID; }
            set { gSID = value; }
        }

        ///<summary>
        ///发表话题的人
        ///</summary>
        public int  UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///阅读次数
        ///</summary>
        public int ReadCount
        {
            get { return readCount; }
            set { readCount = value; }
        }

        ///<summary>
        ///回复次数
        ///</summary>
        public int RevertCount
        {
            get { return revertCount; }
            set { revertCount = value; }
        }

        ///<summary>
        ///内容
        ///</summary>
        public string HuaTeeContent
        {
            get { return huaTeeContent; }
            set { huaTeeContent = value; }
        }

        ///<summary>
        ///话题标题
        ///</summary>
        public string HuaTeeTitle
        {
            get { return huaTeeTitle; }
            set { huaTeeTitle = value; }
        }

        /// <summary>
        /// 发评论的用户名称
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


        ///<summary>
        ///用户头相
        ///</summary>
        public string Userpic
        {
            get { return userpic == string.Empty ? @"~\App_Themes\DefaultTheme\images\head.jpg" : userpic; }
            set { userpic = value; }
        }

        /// <summary>
        /// 最后回复时间
        /// </summary>
        public DateTime LastTime
        {
            get { return lastTime; }
            set { lastTime = value; }
        }

        /// <summary>
        /// 话题发表时间
        /// </summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

        #endregion

    }
}
