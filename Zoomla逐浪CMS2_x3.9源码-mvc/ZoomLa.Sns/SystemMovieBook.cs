/*---------------------------------------------------------------
// Copyright (C) 2008  
// 版权所有
//
// 文件名： SystemMovieBook.cs
// 文件功能描述：定义数据表SystemMovieBook的业务实体
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
    ///SystemMovieBook业务实体
    /// </summary>
    [Serializable]
    public class SystemMovieBook
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private Guid iD;

        ///<summary>
        ///书电影名称
        ///</summary>
        private string bookMovieName = String.Empty;

        ///<summary>
        ///导演作者
        ///</summary>
        private string bookMovieDirect = String.Empty;

        ///<summary>
        ///演员
        ///</summary>
        private string bookMovieStager = String.Empty;

        ///<summary>
        ///描述
        ///</summary>
        private string describe = String.Empty;

        ///<summary>
        ///电影书籍(0书籍1电影)
        ///</summary>
        private int falg;

        ///<summary>
        ///图片
        ///</summary>
        private string mBURL = String.Empty;

        ///<summary>
        ///作者简介
        ///</summary>
        private string authorSynopsis = String.Empty;

        ///<summary>
        ///出版社或影片公司简介
        ///</summary>
        private string publisherFilmCompany = String.Empty;

        ///<summary>
        ///书电影的标示
        ///</summary>
        private string nameplate = String.Empty;

        private string lookUrl = string.Empty;

        private DateTime creatTime;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public SystemMovieBook()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SystemMovieBook
        (
            Guid iD,
            string bookMovieName,
            string bookMovieDirect,
            string bookMovieStager,
            string describe,
            int falg,
            string mBURL,
            string authorSynopsis,
            string publisherFilmCompany,
            string nameplate
        )
        {
            this.iD = iD;
            this.bookMovieName = bookMovieName;
            this.bookMovieDirect = bookMovieDirect;
            this.bookMovieStager = bookMovieStager;
            this.describe = describe;
            this.falg = falg;
            this.mBURL = mBURL;
            this.authorSynopsis = authorSynopsis;
            this.publisherFilmCompany = publisherFilmCompany;
            this.nameplate = nameplate;

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
        ///书电影名称
        ///</summary>
        public string BookMovieName
        {
            get { return bookMovieName; }
            set { bookMovieName = value; }
        }

        ///<summary>
        ///导演作者
        ///</summary>
        public string BookMovieDirect
        {
            get { return bookMovieDirect; }
            set { bookMovieDirect = value; }
        }

        ///<summary>
        ///演员
        ///</summary>
        public string BookMovieStager
        {
            get { return bookMovieStager; }
            set { bookMovieStager = value; }
        }

        ///<summary>
        ///描述
        ///</summary>
        public string Describe
        {
            get { return describe; }
            set { describe = value; }
        }

        ///<summary>
        ///电影书籍(0书籍1电影)
        ///</summary>
        public int Falg
        {
            get { return falg; }
            set { falg = value; }
        }

        ///<summary>
        ///图片
        ///</summary>
        public string MBURL
        {
            get { return mBURL; }
            set { mBURL = value; }
        }

        ///<summary>
        ///作者简介
        ///</summary>
        public string AuthorSynopsis
        {
            get { return authorSynopsis; }
            set { authorSynopsis = value; }
        }

        ///<summary>
        ///出版社或影片公司简介
        ///</summary>
        public string PublisherFilmCompany
        {
            get { return publisherFilmCompany; }
            set { publisherFilmCompany = value; }
        }

        ///<summary>
        ///书电影的标示
        ///</summary>
        public string Nameplate
        {
            get { return nameplate; }
            set { nameplate = value; }
        }


        /// <summary>
        /// 观看地址
        /// </summary>
        public string LookUrl
        {
            get { return lookUrl; }
            set { lookUrl = value; }
        }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }

       


        #endregion

    }
}
