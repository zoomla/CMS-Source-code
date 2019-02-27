namespace ZoomLa.IDAL
{
    using ZoomLa.Model;
    using System; 

    /// <summary>
    /// 管理员工作台配置
    /// </summary>
    public interface ID_AdminProfile
    {
        void Add(M_AdminProfile adminProileInfo);
        bool ExistsAdminName(string adminName);
        M_AdminProfile GetAdminProfile(string adminName);
        void Update(M_AdminProfile adminProileInfo);
    }
}