namespace ZoomLa.IDAL
{
    using ZoomLa.Model;
    using System;
    using System.Data;

    /// <summary>
    /// I_SpecInfo 的摘要说明
    /// </summary>
    public interface ID_SpecInfo
    {
        /// <summary>
        /// 将专题信息添加到数据库中
        /// </summary>
        /// <param name="info">专题信息实例对象</param>
        void Add(M_SpecInfo info);
        /// <summary>
        /// 将专题信息从数据库中删除
        /// </summary>
        /// <param name="SpecInfoID"></param>
        void Del(int SpecInfoID);
        /// <summary>
        /// 获取某内容所属的专题ID字符串，可用','拆分成数组
        /// </summary>
        /// <param name="ItemID">内容ID</param>
        /// <returns></returns>
        string GetSpecial(int ItemID);
        /// <summary>
        /// 将指定内容不属于指定专题ID组的专题信息删除
        /// </summary>
        /// <param name="SpecID">专题ID组</param>
        /// <param name="ItemID">内容ID</param>
        void DelItemSpec(string SpecID, int ItemID);
        /// <summary>
        /// 检测指定专题ID和内容ID的专题信息是否存在
        /// </summary>
        /// <param name="SpecID">专题ID</param>
        /// <param name="ItemID">内容ID</param>
        /// <returns></returns>
        bool IsExist(int SpecID, int ItemID);
        /// <summary>
        /// 读取属于指定专题ID的内容列表
        /// </summary>
        /// <param name="SpecID">专题ID</param>
        /// <returns></returns>
        DataTable GetSpecContent(int SpecID);
        /// <summary>
        /// 将指定专题信息ID的专题信息改变成另一个专题所属，即移动到另一专题
        /// </summary>
        /// <param name="SpecInfoID">专题信息ID</param>
        /// <param name="SpecID">另一专题ID</param>
        void ChgSpecID(int SpecInfoID, int SpecID);
        /// <summary>
        /// 获取指定专题信息ID的专题信息实例对象
        /// </summary>
        /// <param name="SpecInfoID">专题信息ID</param>
        /// <returns></returns>
        M_SpecInfo GetSpecInfo(int SpecInfoID);
    }
}