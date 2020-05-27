/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： GatherStrain.cs
// 文件功能描述：定义数据表GatherStrain的业务实体
//
// 创建标识：Owner(2008-10-10) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///GatherStrain业务实体
    /// </summary>
    [Serializable]
    public class GatherStrain
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///所属用户
        ///</summary>
        private int userID ;

        ///<summary>
        ///群族名称
        ///</summary>
        private string gSName = String.Empty;

     
        ///<summary>
        ///群族介绍
        ///</summary>
        private string gSIntro = String.Empty;

        ///<summary>
        ///群族所属类别
        ///</summary>
        private Guid gSType = Guid.Empty;

        ///<summary>
        ///群图标
        ///</summary>
        private string gSICO = String.Empty;

        ///<summary>
        ///是否支持匿名
        ///</summary>
        private bool cryptonym;

        ///<summary>
        ///可访问程度
        ///</summary>
        private int isPublic;

        private string userName = string.Empty;

        private DateTime creatTime;

        private int gSstate;
      


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public GatherStrain()
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
        ///所属用户
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///群族名称
        ///</summary>
        public string GSName
        {
            get { return gSName; }
            set { gSName = value; }
        }
      

        ///<summary>
        ///群族介绍
        ///</summary>
        public string GSIntro
        {
            get { return gSIntro; }
            set { gSIntro = value; }
        }

        ///<summary>
        ///群族所属类别
        ///</summary>
        public Guid GSType
        {
            get { return gSType; }
            set { gSType = value; }
        }

        private string gSTypeName;
        public string GSTypeName
        {
            get { return gSTypeName; }
            set { gSTypeName = value; }
        }

        ///<summary>
        ///群图标
        ///</summary>
        public string GSICO
        {
            get { return gSICO; }
            set { gSICO = value; }
        }

        ///<summary>
        ///是否支持匿名
        ///</summary>
        public bool Cryptonym
        {
            get { return cryptonym; }
            set { cryptonym = value; }
        }

        ///<summary>
        ///可访问程度
        ///</summary>
        public int IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

        public int GSstate
        {
            get { return gSstate; }
            set { gSstate = value; }
        }
        #endregion

    }
}
