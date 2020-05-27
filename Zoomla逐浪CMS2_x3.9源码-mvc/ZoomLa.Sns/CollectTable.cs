/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： CollectTable.cs
// 文件功能描述：定义数据表CollectTable的业务实体
//
// 创建标识：Owner(2008-10-29) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace ZoomLa.Sns.Model
{
    /// <summary>
    ///CollectTable业务实体
    /// </summary>
    [Serializable]
    public class CollectTable
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        private Guid iD;

        ///<summary>
        ///
        ///</summary>
        private int userID ;

        ///<summary>
        ///收藏ID
        ///</summary>
        private Guid cbyID = Guid.Empty;

        ///<summary>
        ///收藏类型(0书籍1电影2音乐)
        ///</summary>
        private int cbytype;

        ///<summary>
        ///state
        ///</summary>
        private int cstate;

        ///<summary>
        ///添加时间
        ///</summary>
        private DateTime cAddtime;

        ///<summary>
        ///标签
        ///</summary>
        private string labelName = String.Empty;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public CollectTable()
        {
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
        ///
        ///</summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        ///<summary>
        ///收藏ID
        ///</summary>
        public Guid CbyID
        {
            get { return cbyID; }
            set { cbyID = value; }
        }

        ///<summary>
        ///收藏类型(0书籍1电影2音乐)
        ///</summary>
        public int Cbytype
        {
            get { return cbytype; }
            set { cbytype = value; }
        }

        ///<summary>
        ///state
        ///</summary>
        public int Cstate
        {
            get { return cstate; }
            set { cstate = value; }
        }

        ///<summary>
        ///添加时间
        ///</summary>
        public DateTime CAddtime
        {
            get { return cAddtime; }
            set { cAddtime = value; }
        }

        ///<summary>
        ///标签
        ///</summary>
        public string LabelName
        {
            get { return labelName; }
            set { labelName = value; }
        }

        #endregion

    }
}
