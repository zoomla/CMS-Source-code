using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Plat;
using System.Data.SqlClient;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL.Helper;
namespace ZoomLa.BLL
{
    /// <summary>
    /// 手机短信验证类
    /// </summary>
    public class B_Safe_Mobile: ZL_Bll_InterFace<M_Safe_Mobile>
    {
        public string TbName,  PK;
        public M_Safe_Mobile initMod = new M_Safe_Mobile();
        public B_Safe_Mobile()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int Insert(M_Safe_Mobile model)
        {
            return Insert(model, "");
        }
        public int Insert(M_Safe_Mobile model,string code)
        {
            HttpContext.Current.Session.Add("Cur_MobileSafeCode", code);
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 检测手机验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CheckVaildCode(string mobile,string code)
        {
            if (!string.IsNullOrEmpty(code)&&HttpContext.Current.Session["Cur_MobileSafeCode"]!=null &&HttpContext.Current.Session["Cur_MobileSafeCode"].Equals(code))
            {
                string sql = "SELECT TOP 1 * FROM "+TbName+" WHERE Phone="+mobile+" ORDER BY CDate DESC";
                DataTable dt = SqlHelper.ExecuteTable(sql);
                DateTime vailtime = DataConvert.CDate(dt.Rows[0]["CDate"]);
                HttpContext.Current.Session["Cur_MobileSafeCode"] = null;
                if (DateTime.Now-vailtime>TimeSpan.FromMinutes(1))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CDate DESC");
        }
        /// <summary>
        /// 发送手机短信检测(为0则不限定)
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="ip">用户ip</param>
        /// <param name="maxphonenum">同一个手机号最大访问次数</param>
        /// <param name="maxipnum">同一个ip最大访问次数</param>
        /// <returns></returns>
        public bool CheckMobile(string phone,string ip)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@phone", phone), new SqlParameter("@ip", ip) };
            string sql = "SELECT COUNT(*) AS NUM FROM " + TbName + " WHERE DATEDIFF(DAY,CDate,'" + DateTime.Now + "')=0 ";
            string wherestr= " AND Phone=@phone ";
            int phonenum = DataConvert.CLng(SqlHelper.ExecuteTable(sql+ wherestr, sp).Rows[0]["NUM"]);
            if (SiteConfig.SiteOption.MaxMobileMsg != 0 && phonenum >= SiteConfig.SiteOption.MaxMobileMsg) { return false; }
            wherestr = " AND IP=@ip";
            int ipnum = DataConvert.CLng(SqlHelper.ExecuteTable(sql + wherestr, sp).Rows[0]["NUM"]);
            if (SiteConfig.SiteOption.MaxIpMsg != 0 && ipnum >= SiteConfig.SiteOption.MaxIpMsg) { return false; }
            return true;
        }
        public bool CheckMobile(string phone) { return CheckMobile(phone, IPScaner.GetUserIP()); }
        public M_Safe_Mobile SelReturnModel(int ID)
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

        public bool UpdateByID(M_Safe_Mobile model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
    }
}
