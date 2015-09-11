namespace ZoomLa.IDAL
{
    using ZoomLa.Model;
    using System;
using System.Data;

    /// <summary>
    /// 模型字段表访问接口
    /// </summary>
    public interface ID_ModelField
    {
        bool Add(M_ModelField MField);
        bool Update(M_ModelField MField);
        bool AddFieldToTable(string TableName, string FieldName, string FieldType, string DefaultValue);
        bool Del(int FieldId);
        bool DelField(string TableName, string FieldName);
        DataSet GetModelFieldList(int ModelID);
        M_ModelField GetModelByID(int FieldID);
        int GetMaxOrder(int ModelID);
        int GetMinOrder(int ModelID);
        M_ModelField GetModelByOrder(int ModelID, int Order);
        M_ModelField GetModelByFieldName(int ModelID, string FieldName);
        int GetPreID(int ModelID, int CurrentID);
        int GetNextID(int ModelID, int CurrentID);
        bool IsExists(int ModelID, string fieldname);
        bool UpdateOrder(M_ModelField info);
    }
}