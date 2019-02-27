using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Third;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Third
{
    public class B_Third_PlatInfo : ZL_Bll_InterFace<M_Third_PlatInfo>
    {
        string TbName, PK;
        M_Third_PlatInfo initMod = new M_Third_PlatInfo();
        private static List<M_Third_PlatInfo> list = new List<M_Third_PlatInfo>();
        public B_Third_PlatInfo()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Third_PlatInfo model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Third_PlatInfo model)
        {
            //写入时更新缓存
            M_Third_PlatInfo cacheMod = list.FirstOrDefault(p => p != null && p.ID == model.ID);
            if (cacheMod != null) { list.Remove(cacheMod); list.Add(model); }
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Third_PlatInfo SelReturnModel(int ID)
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
        public M_Third_PlatInfo SelModelByFlag(string flag)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("flag", flag.Replace(" ", "")) };
            string where = "flag=@flag";
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
        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据标识获取信息,如果未命中,则从数据库获取
        /// </summary>
        /// <param name="fromCach">如果为false则从数据库中获取信息,且更新缓存</param>
        public static M_Third_PlatInfo SelByFlag(string flag, bool fromCach = true)
        {
            M_Third_PlatInfo model = null;
            if (fromCach)
            {
                model = list.FirstOrDefault(p => p != null && p.Flag.Equals(flag));
                if (model == null)
                {
                    model = new B_Third_PlatInfo().SelModelByFlag(flag);
                    list.Add(model);
                }
            }
            else
            {
                model = new B_Third_PlatInfo().SelModelByFlag(flag);
                if (model != null)
                {
                    M_Third_PlatInfo cacheMod = list.FirstOrDefault(p => p != null && p.Flag.Equals(flag));
                    if (cacheMod == null) { list.Add(cacheMod); }
                    else { cacheMod = model; }
                }
            }
            return model;
        }
    }
}
