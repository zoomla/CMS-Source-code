/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： GSRoom.cs
// 文件功能描述：定义数据表GSRoom的业务实体
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
    ///GSRoom业务实体
    /// </summary>
    [Serializable]
    public class GSRoom
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD = Guid.Empty;

        ///<summary>
        ///群族编号
        ///</summary>
        private Guid gSID = Guid.Empty;

        ///<summary>
        ///空间大小
        ///</summary>
        private double roomSize;

        ///<summary>
        ///已经用了多少
        ///</summary>
        private double useSize;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public GSRoom()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public GSRoom
        (
            Guid iD,
            Guid gSID,
            int roomSize,
            double useSize
        )
        {
            this.iD = iD;
            this.gSID = gSID;
            this.roomSize = roomSize;
            this.useSize = useSize;

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
        ///群族编号
        ///</summary>
        public Guid GSID
        {
            get { return gSID; }
            set { gSID = value; }
        }

        ///<summary>
        ///空间大小
        ///</summary>
        public double RoomSize
        {
            get { return roomSize; }
            set { roomSize = value; }
        }

        ///<summary>
        ///已经用了多少
        ///</summary>
        public double UseSize
        {
            get { return useSize; }
            set { useSize = value; }
        }

        #endregion

    }
}
