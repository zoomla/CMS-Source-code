/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： FileShare.cs
// 文件功能描述：定义数据表FileShare的业务实体
//
// 创建标识：Owner(2008-10-12) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///FileShare业务实体
    /// </summary>
    [Serializable]
    public class FileShare
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///发文件的人
        ///</summary>
        private int userID ;

        ///<summary>
        ///文件的实际名称
        ///</summary>
        private string factFileName = String.Empty;

        ///<summary>
        ///生成的文件名
        ///</summary>
        private string buildFileName = String.Empty;

        ///<summary>
        ///存储地址
        ///</summary>
        private string fileURL = String.Empty;

        ///<summary>
        ///下载次数
        ///</summary>
        private int downCount;

        ///<summary>
        ///创建时间
        ///</summary>
        private DateTime creatTime;

        ///<summary>
        ///文件大小
        ///</summary>
        private int fileSize;

        private string mono;


        ///<summary>
        ///群编号
        ///</summary>
        private Guid gSID = Guid.Empty;

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
        public FileShare()
        {
        }

        ///<summary>
        ///
        ///</summary>
       

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
        ///发文件的人
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///文件的实际名称
        ///</summary>
        public string FactFileName
        {
            get { return factFileName; }
            set { factFileName = value; }
        }

        ///<summary>
        ///生成的文件名
        ///</summary>
        public string BuildFileName
        {
            get { return buildFileName; }
            set { buildFileName = value; }
        }

        ///<summary>
        ///存储地址
        ///</summary>
        public string FileURL
        {
            get { return fileURL; }
            set { fileURL = value; }
        }

        ///<summary>
        ///下载次数
        ///</summary>
        public int DownCount
        {
            get { return downCount; }
            set { downCount = value; }
        }

        ///<summary>
        ///创建时间
        ///</summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

        ///<summary>
        ///文件大小
        ///</summary>
        public int FileSize
        {
            get { return fileSize; }
            set { fileSize = value; }
        }

        ///<summary>
        ///群编号
        ///</summary>
        public Guid GSID
        {
            get { return gSID; }
            set { gSID = value; }
        }


        /// <summary>
        /// 文件备注
        /// </summary>
        public string Mono
        {
            get { return mono; }
            set { mono = value; }
        }

        /// <summary>
        /// 上传文件的用户名称
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
