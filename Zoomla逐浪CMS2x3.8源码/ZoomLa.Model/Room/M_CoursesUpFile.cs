using System;
namespace ZoomLa.Model
{
    public class M_CoursesUpFile
    {
        #region 构造函数
        public M_CoursesUpFile()
        {
        }

        public M_CoursesUpFile
        (
            int ID,
            string Filename,
            string Fileinfo,
            string Fileurl,
            int Coursesid
        )
        {
            this.ID = ID;
            this.Filename = Filename;
            this.Fileinfo = Fileinfo;
            this.Fileurl = Fileurl;
            this.Coursesid = Coursesid;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] CoursesUpFileList()
        {
            string[] Tablelist = { "ID", "Filename", "Fileinfo", "Fileurl", "Coursesid" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// 文件信息
        /// </summary>
        public string Fileinfo { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Fileurl { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public int Coursesid { get; set; }
        #endregion
    }
}


