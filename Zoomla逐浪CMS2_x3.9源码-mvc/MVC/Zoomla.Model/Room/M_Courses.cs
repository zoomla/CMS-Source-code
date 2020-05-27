using System;
namespace ZoomLa.Model
{
    public class M_Courses
    {
        #region 构造函数
        public M_Courses()
        {
        }

        public M_Courses
        (
            int ID,
            string CoursesName,
            int RoomID
        )
        {
            this.ID = ID;
            this.CoursesName = CoursesName;
            this.RoomID = RoomID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] CoursesList()
        {
            string[] Tablelist = { "ID", "CoursesName", "RoomID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CoursesName { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        public int RoomID { get; set; }
        #endregion
    }
}


