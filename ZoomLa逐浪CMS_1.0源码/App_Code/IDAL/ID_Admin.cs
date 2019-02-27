namespace ZoomLa.IDAL
{
    using ZoomLa.Model;
    using System;
    using System.Data;
    /// <summary>
    /// 管理员表访问接口
    /// </summary>
    public interface ID_Admin
    {
        //添加管理员到数据库
        bool Add(M_AdminInfo administratorInfo);
        //从数据库中删除指定AdminID的管理员
        bool Delete(int adminId);
        //通过AdminID查询管理员信息
        M_AdminInfo GetAdminByID(int adminId);
        //通过AdminName查询管理员信息
        M_AdminInfo GetAdminByName(string adminname);
        //从数据库中检索是否存在某管理员名的管理员
        bool IsExist(string adminName);
        bool IsExist(int adminID);
        //更新管理员信息
        bool Update(M_AdminInfo administratorInfo);
        //显示管理员信息
        DataSet SelectAdminInfo();
        bool Update2(M_AdminInfo adminInfo);
        M_AdminInfo GetLoginAdmin(string loginName, string password);
        M_AdminInfo GetModel(string loginName, string password);
    }
}