using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Auth;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_ARoleAuth : ZoomLa.BLL.ZL_Bll_InterFace<M_ARoleAuth>
    {
        private string PK, TbName;
        private M_ARoleAuth initMod = new M_ARoleAuth();
        public B_ARoleAuth() 
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        /// <summary>
        /// 添加或更新指定元素(如果角色ID已存在)
        /// </summary>
        public int Insert(M_ARoleAuth model)
        {
            if (model.Rid < 1) { throw new Exception("角色ID不正确"); }
            M_ARoleAuth ridMod = SelModelByRid(model.Rid);
            if (ridMod != null)
            {
                model.ID = ridMod.ID;
                UpdateByID(model);
                return model.ID;
            }
            else
            {
                return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            }
        }
        public bool UpdateByID(M_ARoleAuth model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_ARoleAuth SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
        public M_ARoleAuth SelModelByRid(int Rid)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE Rid=" + Rid))
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
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public static bool Check(Model.ZLEnum.Auth authfield, string code)
        {
            M_AdminInfo adminMod = B_Admin.GetLogin();
            return Check(adminMod.RoleList, authfield, code);
        }
        public static bool CheckEx(Model.ZLEnum.Auth authfield, string code)
        {
            M_AdminInfo adminMod = B_Admin.GetLogin();
            if (adminMod.IsSuperAdmin()) { return true; }
            bool result = Check(adminMod.RoleList, authfield, code);
           
            if (!result)
            {
                function.WriteErrMsg("你无权进行此项操作");
            }
            return result;
        }
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="rids">角色IDS</param>
        /// <param name="auth">需要验证的权限大类</param>
        /// <param name="code">具体权限码</param>
        /// <returns>True拥有权限</returns>
        public static bool Check(string rids, Model.ZLEnum.Auth authfield, string code)
        {
            rids = rids ?? ""; rids = rids.Trim(',');
            if (string.IsNullOrEmpty(rids)) return false;
            if (rids.Split(',').Where(p => p.Equals("1")).ToArray().Length > 0) { return true; }
            //if (rids.Split(',').Equals(1)) { return true; }//超级管理员
            SafeSC.CheckIDSEx(rids);//稍后必须设定好前后,避免无效验证
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("code", "%" + code + "%") };
            string field = authfield.ToString();
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text,"SELECT ID FROM ZL_ARoleAuth WHERE Rid IN(" + rids + ") AND [" + field + "] Like @code",sp);
            return (dt.Rows.Count > 0);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
