namespace ZoomLa.IDAL
{
    using System;
    using ZoomLa.Model;
    using System.Collections.Generic;
    using System.Data;    

    /// <summary>
    /// 模型表访问接口
    /// </summary>
    public interface ID_Model
    {
        //添加模型到数据库
        bool Add(M_ModelInfo ModelInfo);
        /// <summary>
        /// 添加会员模型
        /// </summary>
        /// <param name="ModelInfo"></param>
        /// <returns></returns>
        bool AddUserModel(M_ModelInfo ModelInfo);
        /// <summary>
        /// //从数据库中读取指定ID的模型信息
        /// </summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        M_ModelInfo GetModelInfo(int ModelID);        
        /// <summary>
        /// 更新模型信息
        /// </summary>
        /// <param name="ModelInfo"></param>
        /// <returns></returns>
        bool Update(M_ModelInfo ModelInfo);        
        /// <summary>
        /// 读取内容模型列表
        /// </summary>
        /// <returns></returns>
        DataTable ListModel();
        /// <summary>
        /// 读取会员模型列表
        /// </summary>
        /// <returns></returns>
        DataTable ListUserModel();
        /// <summary>
        /// 读取商品模型列表
        /// </summary>
        /// <returns></returns>
        DataTable ListShopModel();        
        /// <summary>
        /// 删除指定id的模型 系统内置模型不能删除
        /// </summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        bool Delete(int ModelID);
        bool DeleteTable(string TableName);
        /// <summary>
        /// 更新输入界面代码
        /// </summary>
        /// <param name="InputCode"></param>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        bool UpdateInput(string InputCode, int ModelID);        
        /// <summary>
        /// 更新指定模板
        /// </summary>
        /// <param name="Template"></param>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        bool UpdateModule(string Template,int ModelID); 
    }
}