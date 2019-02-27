using System;
using System.Data;
using System.Configuration;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Web;
using System.Globalization;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_ShopGrade
    {
        private string TbName, PK;
        private M_ShopGrade initMod = new M_ShopGrade();
        public B_ShopGrade() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_ShopGrade SelReturnModel(int ID)
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
        private M_ShopGrade SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_ShopGrade model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_ShopGrade model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public DataTable GetShopGradeinfo()
        {
            return Sql.Sel(TbName, "", "");
        }
        public M_ShopGrade GetShopGradebyid(int ID)
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
        public bool UpdateShopGrade(M_ShopGrade model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool AddShopGrade(M_ShopGrade model)
        {
            Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            return true;
        }
        public bool DelShopGrade(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool ShopGradTrue(string strinfo, int type)
        {
            string[] strinfoArr = strinfo.Split(',');
            string sqlStr = "";
            SqlParameter[] sp = new SqlParameter[strinfoArr.Length];
            for (int i = 0; i < strinfoArr.Length; i++)
            {
                sp[i] = new SqlParameter("strinfo" + i, strinfoArr[i]);
                sqlStr += "@" + sp[i].ParameterName + ",";
            }
            sqlStr = sqlStr.TrimEnd(',');
            switch (type)
            {
                case 1:
                    return SqlHelper.ExecuteSql("update ZL_ShopGrade set Istrue=1 where id in (" + sqlStr + ")", sp);                  
                case 2:
                    return SqlHelper.ExecuteSql("update ZL_ShopGrade set Istrue=0 where id in (" + sqlStr + ")", sp);                
                case 3:
                    return SqlHelper.ExecuteSql("delete from ZL_ShopGrade where id in (" + sqlStr + ")", sp);
            }
            return true;
        }
        public DataTable GetShopGradeinfo(int type)
        {
            string sqlStr = "select * from Zl_ShopGrade where GradeType=@type";
            SqlParameter[] cc = new SqlParameter[1];
            cc[0] = new SqlParameter("@type", SqlDbType.Int);
            cc[0].Value = type;
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cc);
        }
        public M_ShopGrade Getbyrow(DataTable info)
        {
            M_ShopGrade cc = new M_ShopGrade();

            if (info.Rows.Count > 0)
            {
                cc.ID = DataConverter.CLng(info.Rows[0]["ID"].ToString());
                cc.GradeName = info.Rows[0]["GradeName"].ToString();
                cc.Gradeimg = info.Rows[0]["Gradeimg"].ToString();
                cc.CommentNum = DataConverter.CLng(info.Rows[0]["CommentNum"].ToString());
                cc.OtherName = info.Rows[0]["OtherName"].ToString();
                cc.Istrue = DataConverter.CBool(info.Rows[0]["Istrue"].ToString());
                cc.GradeType = DataConverter.CLng(info.Rows[0]["GradeType"].ToString());
                cc.Imgnum = DataConverter.CLng(info.Rows[0]["Imgnum"].ToString());
            }
            return cc;
        }
        public M_ShopGrade GetShopGradebynum(int num, int type)
        {
            string sqlStr = "select top 1 * from Zl_ShopGrade where CommentNum<=@num and GradeType=@type and Istrue=1 order by CommentNum desc";
            SqlParameter[] cc = new SqlParameter[2];
            cc[0] = new SqlParameter("@num", SqlDbType.Int);
            cc[0].Value = num;
            cc[1] = new SqlParameter("@type", SqlDbType.Int);
            cc[1].Value = type;
            DataTable dd = SqlHelper.ExecuteTable(CommandType.Text, sqlStr, cc);
            return Getbyrow(dd);
        }
    }
}