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
using ZoomLa.SQLDAL.SQL;
using System.Data.Common;

namespace ZoomLa.BLL
{
    /// <summary>
    /// 手机短信验证类
    /// </summary>
    public class B_Safe_Mobile : ZL_Bll_InterFace<M_Safe_Mobile>
    {
        public string TbName, PK;
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
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CDate DESC");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "", "CDate DESC");
            DBCenter.SelPage(setting);
            return setting;
        }
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
        /// <summary>
        /// 根据手机号,查找最后一次发送的验证码记录
        /// </summary>
        public M_Safe_Mobile SelLastModel(string mobile)
        {
            if (string.IsNullOrEmpty(mobile)) { return null; }
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("mobile", mobile.Trim()) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "Phone=@mobile", "ID DESC", sp))
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
           return DBCenter.UpdateByID(model,model.ID);
        }
        //----------------------------检测块
          /// <summary>
        /// 发送手机短信检测(为0则不限定)
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="ip">用户ip</param>
        /// <param name="maxphonenum">同一个手机号最大访问次数</param>
        /// <param name="maxipnum">同一个ip最大访问次数</param>
        /// <returns></returns>
        public bool CheckMobile(string phone, string ip)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@phone", phone), new SqlParameter("@ip", ip) };
            string sql = "SELECT COUNT(*) AS NUM FROM " + TbName + " WHERE DATEDIFF(DAY,CDate,'" + DateTime.Now + "')=0 ";
            string wherestr = " AND Phone=@phone ";
            int phonenum = DataConvert.CLng(SqlHelper.ExecuteTable(sql + wherestr, sp).Rows[0]["NUM"]);
            if (SiteConfig.SiteOption.MaxMobileMsg != 0 && phonenum >= SiteConfig.SiteOption.MaxMobileMsg) { return false; }
            wherestr = " AND IP=@ip";
            int ipnum = DataConvert.CLng(SqlHelper.ExecuteTable(sql + wherestr, sp).Rows[0]["NUM"]);
            if (SiteConfig.SiteOption.MaxIpMsg != 0 && ipnum >= SiteConfig.SiteOption.MaxIpMsg) { return false; }
            return true;
        }
        public bool CheckMobile(string phone) { return CheckMobile(phone, IPScaner.GetUserIP()); }
        /// <summary>
        /// 检测手机验证码,10分钟内有效|存数据库|可换浏览器验证|未验证成功不会取消|必须手机与验证码同时匹配
        /// </summary>
        public bool CheckVaildCode(string mobile, string code, ref string err)
        {
            bool flag = false;
            M_Safe_Mobile model = SelLastModel(mobile);
            if (string.IsNullOrEmpty(mobile)) { err = "手机号码不能为空"; }
            else if (string.IsNullOrEmpty(code)) { err = "验证码不能为空"; }
            else if (model == null) { err = "验证码信息不存在"; }
            else if (model.ZStatus != 0) { err = "验证码无效"; }
            else if ((DateTime.Now - model.CDate > TimeSpan.FromMinutes(10))) { err = "验证码过期"; }
            else if (!model.VCode.Equals(code)) { err = "手机验证码不匹配"; }
            else
            {
                flag = true;
                model.ZStatus = 99;
                UpdateByID(model);
            }
            return flag;
        }
    }
}
