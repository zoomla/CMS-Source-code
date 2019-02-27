namespace ZoomLa.IDAL
{
    using ZoomLa.Model;
    using System;
    using System.Data;

    /// <summary>
    /// 收藏信息数据层接口
    /// </summary>
    public interface ID_Favorite
    {
        void AddFavorite(M_Favorite favorite);
        void DelFavorite(int favoriteid);
        DataTable GetFavoriteList(int owner, int NodeID,string keyword);
    }
}