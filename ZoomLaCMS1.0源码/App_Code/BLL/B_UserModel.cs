namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
using ZoomLa.Model;
    using ZoomLa.IDAL;
    using ZoomLa.DALFactory;

    /// <summary>
    /// 会员模型逻辑层
    /// </summary>
    public class B_UserModel
    {
        private static readonly ID_UserModel dal = IDal.CreateUserModel();
        public B_UserModel()
        {            
        }
        public void ADD(M_UserModel info)
        {
            dal.Add(info);
        }
        public void Update(M_UserModel info)
        {
            dal.Update(info);
        }
        public void Del(int ModelID)
        {
            dal.Delete(ModelID);
        }
        public M_UserModel GetInfoByID(int ModelID)
        {
            return dal.GetInfoByID(ModelID);
        }
        public DataTable UserModelList()
        {
            return dal.UserModelList();
        }
        public DataTable UserModelListByGroup(int GroupID)
        {
            return dal.UserModelListByGroup(GroupID);
        }
    }
}