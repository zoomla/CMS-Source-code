namespace ZoomLa.IDAL
{
    using System;
    using ZoomLa.Model;
    using System.Collections.Generic;
    using System.Data;
    /// <summary>
    /// 会员组数据层接口
    /// </summary>
    public interface ID_Group
    {
        /// <summary>
        /// 添加会员组
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool Add(M_Group info);
        /// <summary>
        /// 更新会员组
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool Update(M_Group info);
        M_Group GetGroupByID(int GroupID);
        /// <summary>
        /// 删除会员组
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        bool Del(int GroupID);
        /// <summary>
        /// 获取会员组列表
        /// </summary>
        /// <returns></returns>
        DataTable GetGeoupList();
        /// <summary>
        /// 获取会员组的会员模型id字符串
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        string GetGroupModel(int GroupID);
        /// <summary>
        /// 删除不属于指定会员模型ID组合的会员模型记录
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="GroupModel"></param>
        /// <returns></returns>
        bool DelGroupModel(int GroupID, string GroupModel);
        /// <summary>
        /// 添加会员模型到会员组包含的会员模型组合中
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        bool AddGroupModel(int GroupID, int ModelID);
        /// <summary>
        /// 判断会员组是否包含某会员模型
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        bool IsExistModel(int GroupID, int ModelID);
        /// <summary>
        /// 设定会员组的一项会员功能
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="funname"></param>
        /// <returns></returns>
        bool SetGroupFun(int GroupID, string funname);
        /// <summary>
        /// 删除会员组的一项会员功能设置
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="funname"></param>
        /// <returns></returns>
        bool DelGroupFun(int GroupID, string funname);
    }
}