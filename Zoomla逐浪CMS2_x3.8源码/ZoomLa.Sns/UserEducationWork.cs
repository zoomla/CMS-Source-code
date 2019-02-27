/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： UserEducationWork.cs
// 文件功能描述：定义数据表UserEducationWork的业务实体
//
// 创建标识：Owner(2008-10-14) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace BDUModel
{
    /// <summary>
    ///UserEducationWork业务实体
    /// </summary>
    [Serializable]
    public class UserEducationWork
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///公司学校名称
        ///</summary>
        private string compSchoolName = String.Empty;

        ///<summary>
        ///部门班级院系
        ///</summary>
        private string branchYard = String.Empty;

        ///<summary>
        ///开始时间
        ///</summary>
        private DateTime startTime;

        ///<summary>
        ///结束时间
        ///</summary>
        private DateTime endTime;

        ///<summary>
        ///用户编号
        ///</summary>
        private Guid userID = Guid.Empty;

        ///<summary>
        ///是否公开(0不公开1公开)
        ///</summary>
        private bool isPublic;

        ///<summary>
        ///
        ///</summary>
        private int flage;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public UserEducationWork()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UserEducationWork
        (
            Guid iD,
            string compSchoolName,
            string branchYard,
            DateTime startTime,
            DateTime endTime,
            Guid userID,
            bool isPublic,
            int flage
        )
        {
            this.iD = iD;
            this.compSchoolName = compSchoolName;
            this.branchYard = branchYard;
            this.startTime = startTime;
            this.endTime = endTime;
            this.userID = userID;
            this.isPublic = isPublic;
            this.flage = flage;

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
        ///公司学校名称
        ///</summary>
        public string CompSchoolName
        {
            get { return compSchoolName; }
            set { compSchoolName = value; }
        }

        ///<summary>
        ///部门班级院系
        ///</summary>
        public string BranchYard
        {
            get { return branchYard; }
            set { branchYard = value; }
        }

        ///<summary>
        ///开始时间
        ///</summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        ///<summary>
        ///结束时间
        ///</summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        ///<summary>
        ///用户编号
        ///</summary>
        public Guid UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///是否公开(0不公开1公开)
        ///</summary>
        public bool IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }

        ///<summary>
        ///教育上班(0教育1上班)
        ///</summary>
        public int Flage
        {
            get { return flage; }
            set { flage = value; }
        }

        #endregion

    }
}
