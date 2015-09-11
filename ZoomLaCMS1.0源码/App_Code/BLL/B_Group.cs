namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.IDAL;
    using ZoomLa.DALFactory;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Web.UI.WebControls;
    using System.Globalization;

    /// <summary>
    /// B_Group 的摘要说明
    /// </summary>
    public class B_Group
    {
        private ID_Group dal = IDal.CreateGroup();
        public B_Group()
        {
            
        }
        public bool Add(M_Group info)
        {
            return dal.Add(info);
        }
        public bool Update(M_Group info)
        {
            return dal.Update(info);
        }
        public M_Group GetByID(int GroupID)
        {
            return dal.GetGroupByID(GroupID);
        }
        public bool Del(int GroupID)
        {
            return dal.Del(GroupID);
        }
        public DataTable GetGroupList()
        {
            return dal.GetGeoupList();
        }
        public string GetGroupModel(int GroupID)
        {
            return dal.GetGroupModel(GroupID);
        }
        public bool DelGroupModel(int GroupID, string GroupModel)
        {
            return dal.DelGroupModel(GroupID, GroupModel);
        }
        public void SetGroupModel(int GroupID, string GroupModel)
        {
            dal.DelGroupModel(GroupID, GroupModel);
            string[] ModelArr = GroupModel.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ModelArr.Length; i++)
            {
                if (!dal.IsExistModel(GroupID, DataConverter.CLng(ModelArr[i])))
                {
                    dal.AddGroupModel(GroupID, DataConverter.CLng(ModelArr[i]));
                }
            }
        }

    }
}