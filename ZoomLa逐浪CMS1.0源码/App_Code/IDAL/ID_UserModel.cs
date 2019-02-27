namespace ZoomLa.IDAL
{
    using System;
    using System.Data;
    using System.Configuration;
    using ZoomLa.Model;

    /// <summary>
    /// 会员模型接口
    /// </summary>
    public interface ID_UserModel
    {
        /// <summary>
        /// 添加会员模型
        /// </summary>
        /// <param name="UserInfo"></param>
        void Add(M_UserModel info);
        /// <summary>
        /// 删除指定会员模型
        /// </summary>
        /// <param name="ModelID"></param>
        void Delete(int ModelID);
        /// <summary>
        /// 读取指定会员模型
        /// </summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        M_UserModel GetInfoByID(int ModelID);
        /// <summary>
        /// 更新会员模型
        /// </summary>
        /// <param name="info"></param>
        void Update(M_UserModel info);
        /// <summary>
        /// 会员模型列表
        /// </summary>
        /// <returns></returns>
        DataTable UserModelList();
        /// <summary>
        /// 读取指定会员组启用的会员模型列表
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        DataTable UserModelListByGroup(int GroupID);
    }
}