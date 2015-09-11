namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using ZoomLa.DALFactory;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    /// <summary>
    /// 收藏信息业务逻辑
    /// </summary>
    public class B_Favorite
    {
        private ID_Favorite dal = IDal.CreateFavorite();
        public B_Favorite()
        {            
        }
        /// <summary>
        /// 添加收藏信息到收藏夹
        /// </summary>
        /// <param name="favorite">收藏信息实例</param>
        public void AddFavorite(M_Favorite favorite)
        {
            dal.AddFavorite(favorite);
        }
        /// <summary>
        /// 根据收藏信息实例ID将信息从收藏夹删除
        /// </summary>
        /// <param name="favoriteid">收藏信息实例ID</param>
        public void DelFavorite(int favoriteid)
        {
            dal.DelFavorite(favoriteid);
        }
        /// <summary>
        /// 获取会员的收藏信息列表
        /// MyID - 会员ID
        /// NodeID - 栏目节点ID
        /// keyword - 标题搜索关键字
        /// </summary>
        /// <param name="MyID">会员ID</param>
        /// <param name="NodeID">栏目节点ID</param>
        /// <param name="keyword">标题搜索关键字</param>
        /// <returns>会员的收藏信息列表</returns>
        public DataTable GetMyFavorite(int MyID, int NodeID, string keyword)
        {
            return dal.GetFavoriteList(MyID, NodeID, keyword);
        }
    }
}