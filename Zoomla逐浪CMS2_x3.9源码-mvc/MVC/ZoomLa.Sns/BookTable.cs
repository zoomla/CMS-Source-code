/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： BookTable.cs
// 文件功能描述：定义数据表BookTable的业务实体
//
// 创建标识：Owner(2008-10-20) 
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/
using System;


namespace ZoomLa.Sns.Model
{
    /// <summary>
    ///BookTable业务实体
    /// </summary>
    [Serializable]
    public class BookTable
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///名称
        ///</summary>
        private string bookTitle = String.Empty;

        ///<summary>
        ///又名
        ///</summary>
        private string bookOtherTitle = String.Empty;

        ///<summary>
        ///作者
        ///</summary>
        private string bookAnthor = String.Empty;

        ///<summary>
        ///ISBN
        ///</summary>
        private string bookIsbn = String.Empty;

        ///<summary>
        ///书籍价格
        ///</summary>
        private decimal bookPrice;

        ///<summary>
        ///出版社
        ///</summary>
        private string bookConcerm = String.Empty;

        ///<summary>
        ///出版年
        ///</summary>
        private string bookYear = String.Empty;

        ///<summary>
        ///图片
        ///</summary>
        private string bookurl = String.Empty;

        ///<summary>
        ///简介
        ///</summary>
        private string bookContent = String.Empty;

        ///<summary>
        ///状态
        ///</summary>
        private int bookState;

        ///<summary>
        ///添加时间
        ///</summary>
        private DateTime bookAddtime;

        private int uid;
        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public BookTable()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public BookTable
        (
            Guid iD,
            string bookTitle,
            string bookOtherTitle,
            string bookAnthor,
            string bookIsbn,
            decimal bookPrice,
            string bookConcerm,
            string bookYear,
            string bookurl,
            string bookContent,
            int bookState,
            DateTime bookAddtime,
            int uid
        )
        {
            this.iD = iD;
            this.bookTitle = bookTitle;
            this.bookOtherTitle = bookOtherTitle;
            this.bookAnthor = bookAnthor;
            this.bookIsbn = bookIsbn;
            this.bookPrice = bookPrice;
            this.bookConcerm = bookConcerm;
            this.bookYear = bookYear;
            this.bookurl = bookurl;
            this.bookContent = bookContent;
            this.bookState = bookState;
            this.bookAddtime = bookAddtime;
            this.uid = uid;
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
        ///名称
        ///</summary>
        public string BookTitle
        {
            get { return bookTitle; }
            set { bookTitle = value; }
        }

        ///<summary>
        ///又名
        ///</summary>
        public string BookOtherTitle
        {
            get { return bookOtherTitle; }
            set { bookOtherTitle = value; }
        }

        ///<summary>
        ///作者
        ///</summary>
        public string BookAnthor
        {
            get { return bookAnthor; }
            set { bookAnthor = value; }
        }

        ///<summary>
        ///ISBN
        ///</summary>
        public string BookIsbn
        {
            get { return bookIsbn; }
            set { bookIsbn = value; }
        }

        ///<summary>
        ///书籍价格
        ///</summary>
        public decimal BookPrice
        {
            get { return bookPrice; }
            set { bookPrice = value; }
        }

        ///<summary>
        ///出版社
        ///</summary>
        public string BookConcerm
        {
            get { return bookConcerm; }
            set { bookConcerm = value; }
        }

        ///<summary>
        ///出版年
        ///</summary>
        public string BookYear
        {
            get { return bookYear; }
            set { bookYear = value; }
        }

        ///<summary>
        ///图片
        ///</summary>
        public string Bookurl
        {
            get { return bookurl; }
            set { bookurl = value; }
        }

        ///<summary>
        ///简介
        ///</summary>
        public string BookContent
        {
            get { return bookContent; }
            set { bookContent = value; }
        }

        ///<summary>
        ///状态
        ///</summary>
        public int BookState
        {
            get { return bookState; }
            set { bookState = value; }
        }

        ///<summary>
        ///添加时间
        ///</summary>
        public DateTime BookAddtime
        {
            get { return bookAddtime; }
            set { bookAddtime = value; }
        }

        ///<summary>
        ///用户名
        ///</summary>
        public int Uid
        {
            get { return uid ; }
            set { uid = value; }
        }
        #endregion

    }
}
