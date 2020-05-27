using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Plat
{
    public class B_Plat_UserRole : ZL_Bll_InterFace<M_Plat_UserRole>
    {
        string TbName, PK;
        M_Plat_UserRole initMod = new M_Plat_UserRole();
        public B_Plat_UserRole()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Plat_UserRole model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Plat_UserRole model)
        { 
            return DBCenter.UpdateByID(model,model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName,PK,ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName,PK,ids);
        }
        public M_Plat_UserRole SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public System.Data.DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public DataTable SelByCompID(int compID)
        {
            return DBCenter.Sel(TbName, "CompID=" + compID);
        }
        /// <summary>
        /// 根据公司ID，获取该公司的网络管理员角色ID
        /// </summary>
        public static int SelSuperByCid(int cid)
        {
            return DataConvert.CLng(DBCenter.ExecuteScala("ZL_Plat_UserRole","ID", "CompID=" + cid + " And IsSuper=1"));
        }
        public int GetAdminCount(M_User_Plat model) 
        {
            int id = SelSuperByCid(model.CompID);
            return DBCenter.Count(TbName, "Plat_Role Like '%" + id + "%' And UserID!=" + model.UserID);
        }
        /// <summary>
        /// 获取指定角色的所有权限,所回字符串
        /// </summary>
        public string GetAuthByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            if (string.IsNullOrEmpty(ids)) return "";
            string result = "";
            DataTable dt = DBCenter.SelWithField(TbName, "RoleAuth", "ID in(" + ids + ") And RoleAuth !='' And RoleAuth IS NOT NULL");
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["RoleAuth"].ToString();
            }
            result = result.Replace(",,", ",");
            return result;
        }
        /// <summary>
        /// 验证权限是存在,请先验证其是否为超级管理员
        /// </summary>
        public bool AuthCheck(string ids, string authname)
        {
            if (string.IsNullOrEmpty(ids)) return false;
            string auths = GetAuthByIDS(ids);
            return auths.Contains("," + authname + ",");
        }
    }
}
