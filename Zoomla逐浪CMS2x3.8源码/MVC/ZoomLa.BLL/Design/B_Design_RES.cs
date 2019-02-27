using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Design
{
    public class B_Design_RES : ZL_Bll_InterFace<M_Design_RES>
    {
        private M_Design_RES initMod = new M_Design_RES();
        private string TbName, PK;
        public B_Design_RES()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Design_RES model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public M_Design_RES SelReturnModel(int ID)
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
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="useage">使用场景</param>
        /// <param name="zType">类型</param>
        /// <returns></returns>
        public DataTable Search(string name, string useage, string ztype, string use = "", string fun = "", string style = "")
        {
            string where = "name LIKE @name";
            List<SqlParameter> sp = new List<SqlParameter> { new SqlParameter("name", "%" + name + "%") };
            if (!string.IsNullOrEmpty(useage))
            {
                where += " AND useage = @useage";
                sp.Add(new SqlParameter("useage", useage));
            }
            if (!string.IsNullOrEmpty(ztype))
            {
                where += " AND ztype = @ztype";
                sp.Add(new SqlParameter("ztype", ztype));
            }
            if (!string.IsNullOrEmpty(use))
            {
                where += " AND [use] LIKE @use";
                sp.Add(new SqlParameter("use", "%" + use + "%"));
            }
            if (!string.IsNullOrEmpty(fun))
            {
                where += " AND [fun] LIKE @fun";
                sp.Add(new SqlParameter("fun", "%" + fun+"%"));
            }
            if (!string.IsNullOrEmpty(style) && !style.Equals("全部"))
            {
                where += " AND [style] LIKE @style";
                sp.Add(new SqlParameter("style", "%" + style + "%"));
            }
            return DBCenter.Sel(TbName, where, PK + " DESC", sp);
        }
        /// <summary>
        /// 根据类型查找资源
        /// </summary>
        /// <param name="useage">应用场景</param>
        /// <param name="ztype">资源类型</param>
        /// <returns></returns>
        public DataTable SelByType(string useage, string ztype)
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(useage))
            {
                where += " AND useage = @useage";
                sp.Add(new SqlParameter("useage", useage));
            }
            if (!string.IsNullOrEmpty(ztype))
            {
                where += " AND ztype = @ztype";
                sp.Add(new SqlParameter("ztype", ztype));
            }
            return DBCenter.Sel(TbName, where, "", sp);
        }
        public bool UpdateByID(M_Design_RES model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        //--------------------------------------Tools
        public string GetZType(string ztype)
        {
            string result = "";
            switch (ztype)
            {
                case "img":
                    result = "图片";
                    break;
                case "music":
                    result = "音乐";
                    break;
            }
            return result;
        }
        public string GetUseage(string useage)
        {
            string result = "";
            switch (useage)
            {
                case "bk_pc":
                    result = "动力模块";
                    break;
                case "bk_h5":
                    result = "H5场景";
                    break;
            }
            return result;
        }
    }
}
