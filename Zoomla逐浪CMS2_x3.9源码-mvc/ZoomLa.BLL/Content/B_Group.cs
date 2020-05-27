namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Web.UI.WebControls;
    using System.Globalization;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Data.Common;
    using SQLDAL.SQL;
    public class B_Group : B_Base<M_Group>
    {
        private string strTableName, PK;
        private M_Group initMod = new M_Group();
        public B_Group()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(strTableName);
        }
        public DataTable Sel(int ID)
        {
            return DBCenter.Sel(strTableName, PK + "=" + ID);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable Select_All()
        {
            return DBCenter.JoinQuery("A.*,B.UserModel", "ZL_Group", "ZL_GroupModel", "A.GroupID=B.GroupID");
        }
        public DataTable GetGroupList()
        {
            return Sel();
        }
        /// <summary>
        /// 获取子会员组
        /// </summary>
        public DataTable GetChildGroup(int GroupID)
        {
            string fields = "*,";
            fields += "(SELECT COUNT(*) FROM ZL_Group WHERE ParentGroupID=A.GroupID) AS Child,";
            fields += "(SELECT COUNT(*) FROM ZL_User WHERE GroupID=A.GroupID) AS UserNum,";
            fields += "(SELECT TOP 1 UserModel FROM ZL_GroupModel WHERE GroupID=A.GroupID AND UserModel>0)AS UserModel";
            return DBCenter.SelWithField(strTableName, fields, "ParentGroupID=" + GroupID);
        }
        /// <summary>
        /// 获取由默认会员组和可选会员组组成的列表
        /// </summary>
        public DataTable GetSelGroup()
        {
            return DBCenter.Sel(strTableName, "IsDefault=1 or RegSelect=1", "IsDefault Desc,GroupID Desc");
        }
        /// <summary>
        /// 是否已有会员组，没有会员组时新增的第一个会员组自动成默认会员组
        /// </summary>
        public bool HasGroup()
        {
            return DBCenter.IsExist(strTableName, "");
        }
        /// <summary>
        /// 会员组的会员数
        /// </summary>
        public int GroupUserCount(int gid)
        {
            return DBCenter.Count("ZL_User", "GroupID=" + gid);
        }
        /// <summary>
        /// 默认会员组ID
        /// </summary>
        /// <returns></returns>
        public int DefaultGroupID()
        {
            return DataConvert.CLng(DBCenter.ExecuteScala(strTableName, "GroupID", "IsDefault=1"));
        }
        //--------------------------SELECT END;
        public bool Update(M_Group model)
        {
            return DBCenter.UpdateByID(model, model.GroupID);
        }
        /// <summary>
        /// 设定指定ID的会员组为默认会员组(并清除其他)
        /// </summary>
        public bool SetDefaultGroup(int GroupID)
        {
            DBCenter.UpdateSQL(strTableName, "IsDefault=0", "");
            return DBCenter.UpdateSQL(strTableName, "IsDefault=1", "GroupID=" + GroupID);
        }
        public int GetInsert(M_Group model)
        {
            return DBCenter.Insert(model);
        }
        public bool Add(M_Group model)
        {
            return GetInsert(model) > 0;
        }
        public bool Del(int GroupID)
        {
            return DBCenter.Del(strTableName, PK, GroupID);
        }
        #region 模型(是否将其改为存字符串)
        bool AddGroupModels(int GroupID, int ModelID)
        {
            if (ModelID < 1) { return false; }
            DBCenter.Insert("ZL_GroupModel", "GroupID,UserModel", GroupID + "," + ModelID);
            return true;
        }
        public bool IsExistModel(int GroupID, int ModelID)
        {
            return DBCenter.IsExist("ZL_GroupModel", " GroupID=" + GroupID + " and UserModel=" + ModelID);
        }
        /// <summary>
        /// 获取会员组的会员模型id(应该限定其只能绑定一个ID)
        /// </summary>
        public string GetGroupModel(int GroupID)
        {
            string result = "";
            DataTable dt = DBCenter.Sel("ZL_GroupModel", "GroupID=" + GroupID);
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["UserModel"] + ",";
            }
            return result.TrimEnd(',');
        }
        /// <summary>
        /// 删除不属于指定会员模型ID组合的会员模型记录
        /// </summary>
        public bool DelGroupModel(int GroupID, string modelids = "")
        {
            string where = "GroupID=" + GroupID;
            if (!string.IsNullOrEmpty(modelids)) { SafeSC.CheckIDSEx(modelids); where += " AND GroupModel IN (" + modelids + ")"; }
            return DBCenter.DelByWhere("ZL_GroupModel", where);
        }
        /// <summary>
        /// 为会员组指定模型(先删除绑定,再为其加入)
        /// </summary>
        public void SetGroupModel(int GroupID, string modelids)
        {
            SafeSC.CheckIDSEx(modelids);
            DelGroupModel(GroupID);
            foreach (string mid in modelids.Split(','))
            {
                AddGroupModels(GroupID, DataConvert.CLng(mid));
            }
        }
        #endregion
        /*---------------------------------------------------------------*/
        public M_Group GetByID(int ID)
        {
            return SelReturnModel(ID);
        }
        public M_Group SelReturnModel(int id)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(strTableName, PK, id))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return new M_Group(true);
            }
        }
        #region OA
        /// <summary>
        /// 通过组ID获取组名
        /// </summary>
        public string GetGroupNameByIDS(string ids)
        {
            ids = ids.Trim(',');
            if (string.IsNullOrEmpty(ids)) return "";
            string result = "";
            SafeSC.CheckIDSEx(ids);
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select GroupName From " + strTableName + " Where GroupID in(" + ids + ")");
            foreach (DataRow dr in dt.Rows)
            {
                result += "[" + dr["GroupName"] as string + "],";
            }
            result = result.TrimEnd(',');
            return result;
        }
        /// <summary>
        /// 通过用户组ID获取所有用户ID
        /// </summary>
        public string GetUserIDByGroupIDS(string ids)
        {
            string result = ""; ids = ids.Trim(',');
            if (string.IsNullOrEmpty(ids))
                return result;
            SafeSC.CheckIDSEx(ids);
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select UserID From ZL_User Where GroupID in(" + ids + ")");
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["UserID"].ToString() + ",";
            }
            result = result.TrimEnd(',');
            return result;
        }
        #endregion
    }
}