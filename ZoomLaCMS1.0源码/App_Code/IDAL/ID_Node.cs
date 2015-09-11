namespace ZoomLa.IDAL
{
    using System;
    using ZoomLa.Model;
    using System.Data;

    /// <summary>
    /// 节点访问接口
    /// </summary>
    public interface ID_Node
    {
        int AddNode(M_Node NodeInfo);
        bool DelNode(int NodeID);
        void DelChildNode(int ParentID);
        void SetChild(int ParentID);
        bool UpdateNode(M_Node NodeInfo);
        M_Node GetNode(int NodeID);
        int GetFirstNode(int ParentID);
        int GetMaxOrder(int ParentID);
        int GetMinOrder(int ParentID);
        int GetPreNode(int ParentID, int CurrentID);
        int GetNextNode(int ParentID, int CurrentID);
        int GetDepth(int ParentID);
        void SetChildAdd(int ParentID);
        void AddModelTemplate(int NodeID, int ModelID, string ModelTemplate);
        bool IsExistTemplate(int NodeID, int ModelID);
        void DelModelTemplate(int NodeID, string ModelIDArr);
        void UpdateModelTemplate(int NodeID, int ModelID, string ModelTemplate);
        void DelTemplate(int NodeID);
        string GetModelTemplate(int NodeID, int ModelID);
        DataSet GetNodeList(int ParentID);
        DataSet GetNodeListContain(int ParentID);
        DataTable GetSingleList();
        DataTable GetCreateAllList();
        DataTable GetCreateListByID(string IDArr);
        DataTable GetCreateSingleByID(string IDArr);
    }
}