/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： CommentAll.cs
// 文件功能描述：定义数据表CommentAll的业务实体
//
// 创建标识：Owner(2008-10-21) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace ZoomLa.Sns.Model
{
    /// <summary>
    ///CommentAll业务实体
    /// </summary>
    [Serializable]
    public class CommentAll
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        private Guid iD;

        ///<summary>
        ///标题
        ///</summary>
        private string ctitle = String.Empty;

        ///<summary>
        ///
        ///</summary>
        private int userID ;

        ///<summary>
        ///内容
        ///</summary>
        private string ccontent = String.Empty;

        ///<summary>
        ///被评论的ID
        ///</summary>
        private Guid cbyID = Guid.Empty;

        ///<summary>
        ///添加时间
        ///</summary>
        private DateTime caddtime;

        ///<summary>
        ///被评论的类型(0书籍1电影2音乐)
        ///</summary>
        private int cbyType;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public CommentAll()
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
        ///标题
        ///</summary>
        public string Ctitle
        {
            get { return ctitle; }
            set { ctitle = value; }
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
        ///内容
        ///</summary>
        public string Ccontent
        {
            get { return ccontent; }
            set { ccontent = value; }
        }

        ///<summary>
        ///被评论的ID
        ///</summary>
        public Guid CbyID
        {
            get { return cbyID; }
            set { cbyID = value; }
        }

        ///<summary>
        ///添加时间
        ///</summary>
        public DateTime Caddtime
        {
            get { return caddtime; }
            set { caddtime = value; }
        }

        ///<summary>
        ///被评论的类型(0书籍1电影2音乐)
        ///</summary>
        public int CbyType
        {
            get { return cbyType; }
            set { cbyType = value; }
        }

        #endregion

    }
}
