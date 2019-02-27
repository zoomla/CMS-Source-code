using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using ZoomLa.Common;

namespace ZoomLa.BLL
{
    public class B_Arrive
    {
        private string TbName, PK;
        private M_Arrive initMod = new M_Arrive();
        public B_Arrive()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_Arrive SelReturnModel(int ID)
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
        private M_Arrive SelReturnModel(string strWhere, SqlParameter[] sp)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere, sp))
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
        public M_Arrive SelReturnModel(int ArriveNO, int ArrivePwd)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@yt", ArriveNO), new SqlParameter("yp", ArrivePwd) };
            return SelReturnModel("WHERE ArriveNO=@yt AND ArrivePwd=@yp", sp);
        }
        public M_Arrive SelReturnModel(string arriveNo, string arrivePwd)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("arriveNo", arriveNo), new SqlParameter("arrivePwd", arrivePwd) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " Where ArriveNo=@arriveNo And arrivePwd=@arrivePwd", sp))
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
        /// 根据流水号找到该用户未被使用的优惠券
        /// </summary>
        public M_Arrive SelModelByFlow(string flow, int uid)
        {
            if (string.IsNullOrEmpty(flow) || uid < 1) { return null; }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("flow", flow) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " Where flow=@flow AND UserID=" + uid + " AND State=1", sp))
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
        public DataTable Sel(int type, int state, string flow, string name, string stime, string etime)
        {
            string where = " 1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (type != -100) { where += " AND type=" + type; }
            if (state != -100) { where += " AND state=" + state; }
            if (!string.IsNullOrEmpty(flow)) { where += " AND flow=@flow"; sp.Add(new SqlParameter("flow", flow)); }
            if (!string.IsNullOrEmpty(name)) { where += " AND ArriveName LIKE @name"; sp.Add(new SqlParameter("name", "%" + name + "%")); }
            if (!string.IsNullOrEmpty(stime)) { where += " AND AgainTime>=@stime"; sp.Add(new SqlParameter("stime", stime)); }
            if (!string.IsNullOrEmpty(etime)) { where += " AND EndTime<=@etime"; sp.Add(new SqlParameter("etime", etime)); }
            return DBCenter.Sel(TbName, where, PK + " DESC",sp);
        }
        public bool GetUpdate(M_Arrive model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool GetDelete(int id)
        {
            return Sql.Del(TbName, id);
        }
        public bool DelByIDS(string ids) { SafeSC.CheckIDSEx(ids); return DBCenter.DelByIDS(TbName, PK, ids); }
        public bool GetActive(int id)
        {
            string sqlStr = "update ZL_Arrive set state=1 where id=" + id;
            return SqlHelper.ExecuteSql(sqlStr);
        }
        public int GetInsert(M_Arrive model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public M_Arrive GetArriveById(int id)
        {
            return SelReturnModel(id);
        }
        public bool UpdateState(string ArriveNo)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@ArriveNo", ArriveNo) };
            string sqlStr = "Update ZL_Arrive SET State =10 WHERE ArriveNO=@ArriveNo";
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        public decimal GetOtherArrive(int userId, string ArriveNo, string ArrivePwd)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@userId", userId), new SqlParameter("ArriveNo", ArriveNo), new SqlParameter("ArrivePwd", ArrivePwd) };
            decimal dm = 0;
            string sqlStr = "SELECT Amount FROM ZL_Arrive WHERE UserID not in (0,@userId) AND ArriveNO =@ArriveNo AND ArrivePwd = @ArrivePwd AND State=1 AND getdate()<=EndTime";
            object obj = SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, sp);
            if (obj != null)
            {
                dm = Convert.ToDecimal(obj);
            }
            return dm;
        }
        public bool UpdateUseTime(string ArriveNo)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@ArriveNo", ArriveNo) };
            string sqlStr = "update ZL_Arrive set UseTime=getdate() where ArriveNo=@ArriveNo";
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        public int GetUserid(string ArriveNo)
        {
            int dm = 0;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ArriveNo", ArriveNo) };
            string sqlStr = "SELECT UserID FROM ZL_Arrive WHERE ArriveNo =@ArriveNo";
            object obj = SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, sp);
            dm = Convert.ToInt32(obj);
            return dm;
        }
        /// <summary>
        /// 通过抵用劵ID修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetUpdateState(int id)
        {
            string sqlStr = "Update ZL_Arrive SET State =1 WHERE id=" + id;
            return SqlHelper.ExecuteSql(sqlStr);
        }
        /// <summary>
        /// 通过用户ID和ID修改所属用户
        /// </summary>
        public bool GetUpdateUserid(int id, int userId)
        {
            string sqlStr = "Update ZL_Arrive SET userId=" + userId + " WHERE Id=" + id;
            return SqlHelper.ExecuteSql(sqlStr);
        }
        /// <summary>
        /// 批量激活尚未激活的优惠券
        /// </summary>
        public void ActiveByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "State=1", "State=0 AND ID IN (" + ids + ")");
        }
        //-----新购物流程
        public double UserArrive(string arriveNo, string arrivePwd)
        {
            if (string.IsNullOrEmpty(arriveNo) || string.IsNullOrEmpty(arrivePwd)) return 0;
            M_Arrive model = SelReturnModel(arriveNo, arrivePwd);
            if (model == null || model.EndTime < DateTime.Now || model.State != 1)
            {
                return 0;
            }
            else
            {
                UpdateState(arriveNo);
                return model.Amount;
            }
        }
        public DataTable U_Sel(int uid, int type, int state)
        {
            if (uid < 1) { return null; }
            string where = "1=1";
            where += " AND UserID=" + uid;
            if (type != -100) { where += " AND type=" + type; }
            if (state != -100) { where += " AND state=" + state; }
            return DBCenter.Sel(TbName, where, PK + " DESC");
        }
        /// <summary>
        /// 筛选出,用户尚未领取过的优惠券
        /// </summary>
        public DataTable U_SelForGet(int uid)
        {
            string where = "";
            //1,用户未领取过的券
            where = "SELECT Flow,(SELECT COUNT(ID) FROM ZL_Arrive WHERE Flow=A.Flow AND UserID=0)LCount FROM ZL_Arrive A WHERE EndTime>'" + DateTime.Now + "' AND Flow NOT IN (SELECT Flow FROM ZL_Arrive WHERE UserID=" + uid + " GROUP BY Flow)GROUP BY Flow";
            //2,券还有剩余
            where = "SELECT Flow FROM (" + where + ")A WHERE LCount>0";
            where = "SELECT MIN(ID) FROM ZL_Arrive WHERE Flow IN (" + where + ") GROUP BY Flow";
            where = "ID IN (" + where + ")";
            DataTable dt= DBCenter.Sel(TbName, where, PK + " DESC");
            return dt;
        }
        /// <summary>
        /// 根据guid领取对应的优惠券
        /// (暂不限定用户领取数量,但领取页面会筛除掉)
        /// </summary>
        /// <returns>领取到的优惠券ID</returns>
        public int U_GetArrive(int uid,string guid)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(new SqlParameter("@flow", guid));
            DataTable dt = DBCenter.Sel(TbName,"flow=@flow AND UserID=0","",sp);
            if (dt.Rows.Count > 0)
            {
                DBCenter.UpdateSQL(TbName, "UserID="+uid, "ID=" + dt.Rows[0]["ID"]);
                return Convert.ToInt32(dt.Rows[0]["ID"]);
            }
            else { return 0; }
           
        }
        /// <summary>
        /// 测试优惠券,用于订单页
        /// </summary>
        /// <param name="flow"></param>
        /// <returns>优惠后的金额,</returns>
        public bool U_CheckArrive(M_Arrive model, int uid, ref double money, ref string err)
        {
            if (model == null) { err = "指定的优惠券不存在"; return false; }
            model.MaxAmount = model.MaxAmount == 0 ? double.MaxValue : model.MaxAmount;
            if (model.UserID != uid) { err = "优惠券与用户不匹配"; }
            else if (uid < 1) { err = "用户信息不正确"; }
            else if (model.State == 10) { err = "优惠券已被使用"; }
            else if (model.State == 0) { err = "优惠券尚未激活"; }
            else if (model.Amount < 1) { err = "优惠券金额异常["+model.Amount+"]"; }
            else if (model.MinAmount > money) { err = "未达到最小金额使用限制"; }
            else if (model.MaxAmount < money) { err = "超过最大金额使用限制"; }
            else if (model.EndTime < DateTime.Now) { err = "优惠券已过期"; }
            else if (model.AgainTime > DateTime.Now) { err = "优惠券尚未到可使用时段"; }
            else
            {
                money = money - model.Amount;
                money = money < 0 ? 0 : money;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 使用目标优惠券,并写入日志
        /// </summary>
        /// <param name="model">优惠券模型</param>
        /// <param name="uid">需要使用该优惠券的用户ID</param>
        /// <param name="money">订单的金额,优惠完成后该值会被修改</param>
        /// <param name="err">优惠券错误原因</param>
        /// <param name="remind">优惠券使用备注</param>
        /// <returns>true使用成功,false则查看err</returns>
        public bool U_UseArrive(M_Arrive model, int uid, ref double money, ref string err, string remind)
        {
            if (U_CheckArrive(model, uid, ref money, ref err))
            {
                List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("remind", remind), new SqlParameter("usetime", DateTime.Now) };
                DBCenter.UpdateSQL(TbName, "State=10,UseRemind=@remind,UseTime=@usetime", "ID=" + model.ID, sp);
                return true;
            }
            else { return false; }
        }
        //-----Logical
        public string GetMoneyRegion(double min,double max)
        {
            //double min = DataConvert.CDouble(Eval("MinAmount"));
            //double max = DataConvert.CDouble(Eval("MaxAmount"));
            if (min == 0 && max == 0) { return "无使用门槛"; }
            if (max == 0 && min > 0) { return "满" + min.ToString("f0") + "元使用"; }
            if (max > 0 && min == 0) { return max + "元以下使用"; }
            if (max > 0 && min > 0) { return min + "-" + max + "元使用"; }
            return "<span style='color:red;'>使用条件错误</span>";
        }
        public string GetTypeStr(int type) 
        {
            string result = "";
            switch (type)
            {
                case 0:
                    result = "现金卡";
                    break;
                case 1:
                    result = "银币卡";
                    break;
                case 2:
                    result = "优惠券";
                    break;
                default:
                    break;
            }
            return result;
        }
        public string GetStateStr(int state) 
        {
            string result = "";
            switch (state)
            {
                case 0:
                    result = "未激活";
                    break;
                case 1:
                    result = "已激活";
                    break;
                case 10:
                    result = "已使用";
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
