using System;
using ZoomLa.Model;
using System.Data;
namespace ZoomLa.IDAL
{
    /// <summary>
    /// ID_Spec 的摘要说明
    /// </summary>
    public interface ID_Spec
    {
        M_Spec GetSpec(int SpecID);
        void AddSpec(M_Spec Spec);
        void UpdateSpec(M_Spec Spec);
        void DelSpec(int SpecID);
        int GetFirstID();
        DataTable GetSpecList(int SpecCateID);
        DataTable GetSpecAll();
    }
}