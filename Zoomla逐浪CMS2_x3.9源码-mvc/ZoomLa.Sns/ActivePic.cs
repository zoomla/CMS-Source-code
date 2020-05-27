/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： ActivePic.cs
// 文件功能描述：定义数据表ActivePic的业务实体
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
    ///ActivePic业务实体
    /// </summary>
    [Serializable]
    public class ActivePic
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///活动ID
        ///</summary>
        private Guid activeID = Guid.Empty;

        ///<summary>
        ///图片路径
        ///</summary>
        private string picUrl = String.Empty;

        ///<summary>
        ///添加时间
        ///</summary>
        private DateTime picAddtime;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public ActivePic()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ActivePic
        (
            Guid iD,
            Guid activeID,
            string picUrl,
            DateTime picAddtime
        )
        {
            this.iD = iD;
            this.activeID = activeID;
            this.picUrl = picUrl;
            this.picAddtime = picAddtime;

        }

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
        ///活动ID
        ///</summary>
        public Guid ActiveID
        {
            get { return activeID; }
            set { activeID = value; }
        }

        ///<summary>
        ///图片路径
        ///</summary>
        public string PicUrl
        {
            get { return picUrl; }
            set { picUrl = value; }
        }

        ///<summary>
        ///添加时间
        ///</summary>
        public DateTime PicAddtime
        {
            get { return picAddtime; }
            set { picAddtime = value; }
        }

        #endregion

    }
}
