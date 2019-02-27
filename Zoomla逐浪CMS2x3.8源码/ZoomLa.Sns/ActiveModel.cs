/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： Active.cs
// 文件功能描述：定义数据表Active的业务实体
//
// 创建标识：Owner(2008-10-17) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///Active业务实体
    /// </summary>
    [Serializable]
    public class ActiveModel
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///创建者
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///管理者
        ///</summary>
        private Guid activeManage = Guid.Empty;

        ///<summary>
        ///标题
        ///</summary>
        private string activeTitle = String.Empty;

        ///<summary>
        ///类型
        ///</summary>
        private Guid typeID = Guid.Empty;

        ///<summary>
        ///开始时间
        ///</summary>
        private DateTime activeStarttime;

        ///<summary>
        ///结束时间
        ///</summary>
        private DateTime activeEndtime;

        ///<summary>
        ///地点
        ///</summary>
        private string activeAddress = String.Empty;

        ///<summary>
        ///内容描述
        ///</summary>
        private string activeContent = String.Empty;

        ///<summary>
        ///参加方式
        ///</summary>
        private int activeJoinType;

        ///<summary>
        ///其他
        ///</summary>
        private int activeOther;

        ///<summary>
        ///添加时间
        ///</summary>
        private DateTime activeAddtime;

        ///<summary>
        ///活动图片
        ///</summary>
        private string activePic = String.Empty;
        


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ActiveModel()
        {
        }

        ///<summary>
        ///
        ///</summary>

        #endregion

        #region 属性定义

        ///<summary>
        ///编号
        ///</summary>
        public Guid ID
        {
            get { return iD; }
            set { iD = value; }
        }

        ///<summary>
        ///创建者
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///管理者
        ///</summary>
        public Guid ActiveManage
        {
            get { return activeManage; }
            set { activeManage = value; }
        }

        ///<summary>
        ///标题
        ///</summary>
        public string ActiveTitle
        {
            get { return activeTitle; }
            set { activeTitle = value; }
        }

        ///<summary>
        ///类型
        ///</summary>
        public Guid TypeID
        {
            get { return typeID; }
            set { typeID = value; }
        }

        ///<summary>
        ///开始时间
        ///</summary>
        public DateTime ActiveStarttime
        {
            get { return activeStarttime; }
            set { activeStarttime = value; }
        }

        ///<summary>
        ///结束时间
        ///</summary>
        public DateTime ActiveEndtime
        {
            get { return activeEndtime; }
            set { activeEndtime = value; }
        }

        ///<summary>
        ///地点
        ///</summary>
        public string ActiveAddress
        {
            get { return activeAddress; }
            set { activeAddress = value; }
        }

        ///<summary>
        ///内容描述
        ///</summary>
        public string ActiveContent
        {
            get { return activeContent; }
            set { activeContent = value; }
        }

        ///<summary>
        ///参加方式
        ///</summary>
        public int ActiveJoinType
        {
            get { return activeJoinType; }
            set { activeJoinType = value; }
        }

        ///<summary>
        ///其他
        ///</summary>
        public int ActiveOther
        {
            get { return activeOther; }
            set { activeOther = value; }
        }

        ///<summary>
        ///添加时间
        ///</summary>
        public DateTime ActiveAddtime
        {
            get { return activeAddtime; }
            set { activeAddtime = value; }
        }

        ///<summary>
        ///活动图片
        ///</summary>
        public string ActivePic
        {
            get { return activePic; }
            set { activePic = value; }
        }

        #endregion

    }
}
