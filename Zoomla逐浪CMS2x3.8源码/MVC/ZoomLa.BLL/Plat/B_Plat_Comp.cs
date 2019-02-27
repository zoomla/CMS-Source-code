using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Plat
{
    public class B_Plat_Comp : ZL_Bll_InterFace<M_Plat_Comp>
    {
        private string TbName, PK;
        private M_Plat_Comp initMod = new M_Plat_Comp();
        public B_Plat_Comp()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Plat_Comp model)
        {
            if (string.IsNullOrEmpty(model.CompShort)) { model.CompShort = StringHelper.SubStr(0, 4, ""); }
            return DBCenter.Insert(model);
        }
        /// <summary>
        /// 根据用户的企业邮箱,返回匹配的公司ID(注册时使用)
        /// </summary>
        /// <param name="email">test@z01.com</param>
        /// <returns>返回公司ID</returns>
        public int SelCompByMail(string email)
        {
            int compid = 0;
            if (!email.Contains("@") || !IsEnterMail(email)) return compid;//不是合规的邮件格式,或非企业邮箱,则不处理
            email = email.Split('@')[1];
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("email", email) };
            DataTable dt = DBCenter.Sel(TbName, "Mails=@email", "", sp);
            if (dt != null && dt.Rows.Count > 0)
            {
                compid = Convert.ToInt32(dt.Rows[0]["ID"]);
            }
            return compid;
        }
        /// <summary>
        /// 是否为企业邮箱
        /// </summary>
        /// <email>test@z01.com</email>
        /// <returns>True:是,False否</returns>
        public bool IsEnterMail(string email)
        {
            bool flag = false;
            if (email.Contains("@"))
            {
                email = email.Split('@')[1].ToLower();
                string[] entermails = BLLCommon.GetXmlByNode("EnterMail").Replace(" ", "").Split(',');
                if (entermails.Length < 1) throw new Exception("配置文件不存在,无法判断邮箱类型!!");
                flag = entermails.Where(p => p.Equals(email)).ToArray().Length == 0;
            }
            return flag;
        }
        public bool UpdateByID(M_Plat_Comp model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        public M_Plat_Comp SelModelByName(string name)
        {
            name = name.Trim();
            string where = "CompName=@name OR CompShort=@name";
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("@name", name) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, "", sp))
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
        public M_Plat_Comp SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    M_Plat_Comp model = new M_Plat_Comp();
                    model.CompShort = "";
                    return model;
                }
            }
        }
        public M_Plat_Comp SelModelByCUser(int uid) 
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "CreateUser", uid))
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
            return DBCenter.Sel(TbName, "", "CreateTime Desc");
        }
        public DataTable SelForList(string key = "")
        {
            string where = "";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(key))
            {
                where += "A.CompName LIKE @compname";
                sp.Add(new SqlParameter("compname", "%" + key + "%"));
            }
            return DBCenter.JoinQuery("A.*,B.UserName", TbName, "ZL_User", "A.CreateUser=B.UserID", where, "CreateTime DESC",sp.ToArray());
        }
        //--------------Tools
        public string CreateUPPath(M_Plat_Comp compMod)
        {
            compMod.UPPath = compMod.ID + ":" + compMod.CreateUser + ":zoomla";
            compMod.UPPath = EncryptHelper.AESEncrypt(compMod.UPPath);
            return compMod.UPPath;
        }
        /// <summary>
        /// 根据用户信息,创建公司与网络管理员信息,返回公司信息
        /// </summary>
        public M_Plat_Comp CreateByUser(M_User_Plat upMod)
        {
            M_Plat_Comp compMod = new M_Plat_Comp();
            compMod.CompName = B_User.GetUserName(upMod.TrueName, upMod.UserName) + "的";
            compMod.CreateUser = upMod.UserID;
            compMod.UPPath = CreateUPPath(compMod);
            compMod.ID = Insert(compMod);
            //if (compBll.IsEnterMail(mu.Email)) { compMod.Mails = mu.Email.Split('@')[1]; }
            //------创建公司网络管理员角色
            M_Plat_UserRole urMod = new M_Plat_UserRole();
            B_Plat_UserRole urBll = new B_Plat_UserRole();
            urMod.IsSuper = 1;
            urMod.RoleAuth = "";
            urMod.CompID = upMod.CompID;
            urMod.RoleName = "网络管理员";
            urMod.RoleDesc = "公司网络管理员,拥有全部权限,该角色只允许存在一个";
            urMod.UserID = upMod.UserID;
            urMod.CompID = compMod.ID;
            upMod.Plat_Role = "," + urBll.Insert(urMod) + ",";

            upMod.CompID = compMod.ID;
            upMod.CompName = compMod.CompName;

            return compMod;
        }
    }
}
