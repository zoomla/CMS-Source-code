namespace ZoomLa.Model
{
    using System;

    /// <summary>
    /// M_Favorite 的摘要说明
    /// </summary>
    public class M_Favorite
    {
        private int m_FavoriteID;
        private int m_Owner;
        private int m_InfoID;
        private DateTime m_AddDate;
        public M_Favorite()
        {
            
        }
        /// <summary>
        /// 收藏ID
        /// </summary>
        public int FavoriteID
        {
            get { return this.m_FavoriteID; }
            set { this.m_FavoriteID = value; }
        }
        /// <summary>
        /// 收藏人ID
        /// </summary>
        public int Owner
        {
            get { return this.m_Owner; }
            set { this.m_Owner = value; }
        }
        /// <summary>
        /// 被收藏内容的ID
        /// </summary>
        public int InfoID
        {
            get { return this.m_InfoID; }
            set { this.m_InfoID = value; }
        }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime AddDate
        {
            get { return this.m_AddDate; }
            set { this.m_AddDate = value; }
        }
    }
}