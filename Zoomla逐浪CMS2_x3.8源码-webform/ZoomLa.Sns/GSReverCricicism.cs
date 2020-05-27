/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： GSReverCricicism.cs
// 文件功能描述：定义数据表GSReverCricicism的业务实体
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
    ///GSReverCricicism业务实体
    /// </summary>
    [Serializable]
    public class GSReverCricicism
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///回复评论话题相片编号
        ///</summary>
        private Guid huaTeePicID = Guid.Empty;

        ///<summary>
        ///回复的用户编号
        ///</summary>
        private int userID ;

        ///<summary>
        ///内容
        ///</summary>
        private string content = String.Empty;

        ///<summary>
        ///时间
        ///</summary>
        private DateTime creatTime;

        ///<summary>
        ///回复0，评论1
        ///</summary>
        private int flage;

        ///<summary>
        ///排序
        ///</summary>
        private int taxis;

        private string userName = string.Empty;

        ///<summary>
        ///用户头相
        ///</summary>
        private string userpic = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public GSReverCricicism()
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
        ///回复评论话题相片编号
        ///</summary>
        public Guid HuaTeePicID
        {
            get { return huaTeePicID; }
            set { huaTeePicID = value; }
        }

        ///<summary>
        ///回复的用户编号
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///内容
        ///</summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        ///<summary>
        ///时间
        ///</summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

        ///<summary>
        ///回复0，评论1
        ///</summary>
        public int Flage
        {
            get { return flage; }
            set { flage = value; }
        }

        ///<summary>
        ///排序
        ///</summary>
        public int Taxis
        {
            get { return taxis; }
            set { taxis = value; }
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

        #endregion

    }
}
