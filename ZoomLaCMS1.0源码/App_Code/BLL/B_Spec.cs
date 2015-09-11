namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.IDAL;
    using ZoomLa.DALFactory;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;

    /// <summary>
    /// B_Spec 的摘要说明
    /// </summary>
    public class B_Spec
    {
        private ID_Spec dal = IDal.CreateSpec();
        public B_Spec()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public void AddSpec(M_Spec Spec)
        {
            dal.AddSpec(Spec);
        }
        public void UpdateSpec(M_Spec Spec)
        {
            dal.UpdateSpec(Spec);
        }
        public void DelSpec(int SpecID)
        {
            dal.DelSpec(SpecID);
        }
        public int GetFirstID()
        {
            return dal.GetFirstID();
        }
        public M_Spec GetSpec(int SpecID)
        {
            return dal.GetSpec(SpecID);
        }
        public DataTable GetSpecList(int SpecCate)
        {
            return dal.GetSpecList(SpecCate);
        }
        public DataTable GetSpecAll()
        {
            return dal.GetSpecAll();
        }
    }
}