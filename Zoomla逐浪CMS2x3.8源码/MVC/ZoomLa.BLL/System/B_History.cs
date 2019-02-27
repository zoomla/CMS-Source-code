using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using System.Web;
using ZoomLa.BLL.Helper;
using SType = ZoomLa.Model.M_UserExpHis.SType;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_History
    {
        private static string PK = "ExpHisID";
        public static void Insert(M_UserExpHis model)
        {
            if (model.UserID == 0)
            {
                M_UserInfo mu = new B_User().GetLogin();
                if (mu != null) { model.UserID = mu.UserID; }
            }
            if (model.Operator == 0)
            {
                M_AdminInfo adminMod = B_Admin.GetLogin();
                if (adminMod != null) { model.Operator = adminMod.AdminId; }
            }
            if (string.IsNullOrEmpty(model.OperatorIP)) model.OperatorIP = IPScaner.GetUserIP();
            if (string.IsNullOrEmpty(model.Remark) && HttpContext.Current != null) model.Remark = HttpContext.Current.Request.RawUrl;
            if (model.HisTime <= DateTime.MinValue) { model.HisTime = DateTime.Now; }
            model.TbName = GetTbName((SType)model.ScoreType);
            DBCenter.Insert(model);
        }
        public M_UserExpHis SelReturnModel(SType type, int id)
        {
            M_UserExpHis model = new M_UserExpHis();
            using (SqlDataReader reader = Sql.SelReturnReader(GetTbName(type), PK, id))
            {
                if (reader.Read())
                {
                    return new M_UserExpHis().GetModelFromReader(reader);
                }
                else { return null; }
            }
        }
        public void UpdateByID(M_UserExpHis model)
        {
            model.TbName = GetTbName((SType)model.ScoreType);
            DBCenter.UpdateByID(model, model.ExpHisID);
        }
        public DataTable SelByType_U(int stype, int uid)
        {
            string tbname = GetTbName((SType)stype);
            return DBCenter.Sel(tbname, "UserID=" + uid, "HisTime DESC");
        }
        /// <summary>
        /// 用于财务流水
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="searchtype">搜索类型 1:按userid检索,2:按UserName检索</param>
        /// <param name="search"></param>
        /// <returns></returns>
        public DataTable SelByType(SType stype, int searchtype = 1, string search = "")
        {
            string sql = "";
            string strwhere = "";
            string joinstr = "LEFT JOIN ZL_User B ON A.UserID=B.UserID";
            if (!string.IsNullOrEmpty(search))
            {
                if (searchtype == 1)
                {
                    strwhere = " AND A.UserID=@search";
                    search = DataConverter.CLng(search).ToString();
                }
                else
                {
                    strwhere = " AND UserName LIKE @search";
                    search = "%" + search + "%";
                }
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@search", search) };
            switch (stype)
            {
                case M_UserExpHis.SType.Purse:
                    sql = "SELECT A.*,B.UserName FROM ZL_UserExpDomP A " + joinstr + " WHERE 1=1 " + strwhere;//ZL_UserExpDomP
                    break;
                case M_UserExpHis.SType.SIcon:
                    sql = "SELECT A.*,B.UserName FROM ZL_UserCoinHis A " + joinstr + " WHERE 1=1 " + strwhere;//ZL_UserCoinHis
                    break;
                case M_UserExpHis.SType.Point:
                case M_UserExpHis.SType.Credit:
                    sql = "SELECT A.*,B.UserName FROM ZL_UserExpHis A " + joinstr + " Where ScoreType=" + (int)stype + strwhere;
                    break;
                case M_UserExpHis.SType.UserPoint://点券
                case M_UserExpHis.SType.DummyPoint://虚拟币
                    sql = "SELECT A.*,B.UserName FROM ZL_User_DummyPoint A " + joinstr + " Where ScoreType=" + (int)stype + strwhere;
                    break;
                default:
                    break;
            }
            sql += " ORDER BY HisTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public bool DelByIDS(SType stype, string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(GetTbName(stype),PK, ids);
        }
        public PageSetting SelPage(int cpage, int psize, int stype, int uid, string skey, string stime, string etime)
        {
            string tbname = GetTbName((SType)stype);
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "1=1 ";
            if (uid > 0) { where += " AND UserID=" + uid; }
            if (!string.IsNullOrEmpty(stime)) { where += " AND HisTime >=@stime"; sp.Add(new SqlParameter("stime", stime)); }
            if (!string.IsNullOrEmpty(etime)) { where += " AND HisTime <=@etime"; sp.Add(new SqlParameter("etime", etime)); }
            if (!string.IsNullOrEmpty(skey)) { where += " AND Detail LIKE @skey";sp.Add(new SqlParameter("skey","%"+skey+"%")); }
            PageSetting setting = PageSetting.Single(cpage, psize, tbname, PK, where, "HisTime DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        //------------------------------Tools
        public static string GetTbName(SType type)
        {
            string tbname = "";
            switch (type)//ExpHisID
            {
                case SType.Purse:
                    tbname = "ZL_UserExpDomP";
                    break;
                case SType.SIcon:
                    tbname = "ZL_UserCoinHis";
                    break;
                case SType.UserPoint:
                case SType.DummyPoint:
                    tbname = "ZL_User_DummyPoint";
                    break;
                case SType.Point:
                case SType.Credit:
                    tbname = "ZL_UserExpHis";
                    break;
            }
            return tbname;
        }
        //private  string GetPK(SType type)
        //{
        //    string pk = "";
        //    switch (type)
        //    {
        //        case SType.Purse:
        //            pk = "ID";
        //            break;
        //        case SType.SIcon:
        //            pk = "CoinHisID";
        //            break;
        //        case SType.UserPoint:
        //        case SType.DummyPoint:
        //        case SType.Point:
        //        case SType.Credit:
        //            pk = "ExpHisID";
        //            break;
        //    }
        //    return pk;
        //}
    }
}