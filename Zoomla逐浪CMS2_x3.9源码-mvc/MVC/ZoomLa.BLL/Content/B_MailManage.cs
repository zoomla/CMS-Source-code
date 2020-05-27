namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Threading;
    using ZoomLa.Components;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Text;
    using System.Xml;
    using System.Collections.Generic;
    using System.Net;
    using System.IO;
    using System.Web;
    using System.Data.SqlClient;
    using ZoomLa.BLL.Helper;
    using Newtonsoft.Json;
    using SQLDAL.SQL;
    public class B_MailManage
    {
        private M_MailManage initmod = new M_MailManage();
        private string strTableName, PK;
        public B_MailManage()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_MailManage SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_MailManage SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize, string email = "")
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(email)) { where += " AND Email=@email"; sp.Add(new SqlParameter("email", email)); }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelByEmail(string email)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE Email=@email";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@email", email) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public bool UpdateByID(M_MailManage model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + strTableName + " WHERE ID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public int insert(M_MailManage model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_MailManage model)
        {
            return insert(model);
        }
        public bool GetUpdate(M_MailManage model)
        {
            return UpdateByID(model);
        }
        public bool GetDelete(int ID)
        {
            return Del(ID);
        }
        public M_MailManage GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }
        /// <summary>
        /// 按后缀名查询
        /// </summary>
        public DataTable GetPostfix(string str, string state)
        {
            string sql = "Select * From " + strTableName + " Where Postfix Like @str";
            if (!string.IsNullOrEmpty(state))
            {
                sql = " And State=@state";
            }
            sql += " Order By AddTime desc";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("str", "%" + str + "%"), new SqlParameter("state", state) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        /// <summary>
        /// 按最后发送时间查询
        /// </summary>
        public DataTable GetBackMostTime(string starttime, string endtime, string state)
        {
            string sql = "Select * From " + strTableName + " Where 1=1 ";

            if (!string.IsNullOrEmpty(starttime))
            {
                sql += " And DATEDIFF(day,BackMostTime,@starttime)<0";
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                sql += " And DATEDIFF(day,BackMostTime,@endtime)>0";
            }
            if (!string.IsNullOrEmpty(state))
            {
                sql += " And State=@state";
            }
            sql += " Order By AddTime desc";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("starttime", starttime), new SqlParameter("endtime", endtime), new SqlParameter("state", state) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }

        public DataTable GetSend()
        {
            string sql = "Select * From " + strTableName + " Where State=1";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 按类型查询
        /// </summary>
        public DataTable GetSubscribe(string str, string state)
        {
            string sql = "Select * From " + strTableName + " Where SubscribeToType=@str";
            if (!string.IsNullOrEmpty(state))
            {
                sql += " And State=@state";
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("str", str), new SqlParameter("state", state) };
            sql += " Order By AddTime Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="MailManage"></param>
        /// <returns></returns>
        public M_MailManage GetSelects(int ID)
        {
            return SelReturnModel(ID);
        }
        /// <summary>
        /// 按验证状态查询
        /// </summary>
        public DataTable GetState(string str)
        {
            string sql = "Select * From " + strTableName + " Where State=@str Order By AddTime Desc";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("str", str) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable Select_All()
        {
            return Sel();
        }
        /// <summary>
        /// 确认邮箱
        /// </summary>
        /// <param name="id">邮箱ID</param>
        /// <param name="mail"></param>
        /// <returns></returns>
        public M_MailManage ConfirmEmail(string mail)
        {

            string sql = "Select * From " + strTableName + " Where Email=@mail And State=0";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("mail", mail) };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            M_MailManage returnm = new M_MailManage();
            if (dt.Rows.Count > 0)
            {
                returnm = GetSelects(int.Parse(dt.Rows[0]["ID"].ToString()));
            }
            else
            {
                returnm = null;
            }
            if (dt != null)
                dt.Dispose();
            return returnm;
        }
        /// <summary>
        /// 更新Email地址
        /// </summary>
        public bool UpdateEmail(int id, string mail)
        {
            M_MailManage mm = GetSelect(id);
            mm.Email = mail;
            return GetUpdate(mm);
        }
        /// <summary>
        /// 按申请时间查询
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public DataTable GetAddTime(string stime, string etime, string state)
        {
            string st = "", et = "", s = "";
            if (!string.IsNullOrEmpty(stime))
            {
                st = " and DATEDIFF(day,AddTime,@st)<0";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                et = " and DATEDIFF(day,AddTime,@et)>0";
            }
            if (!string.IsNullOrEmpty(state))
            {
                s = " and State=@s";
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("st", stime), new SqlParameter("st", etime), new SqlParameter("s", state) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where 1=1" + st + et + s + " Order By AddTime Desc", sp);
        }
        /// <summary>
        /// 按字母查询
        /// </summary>
        public DataTable GetABC(string str, string state)
        {
            string s = "";
            if (!string.IsNullOrEmpty(state))
            {
                s = " and State=@state";
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + str + "%"), new SqlParameter("state", state) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where Email Like @key " + s + " Order By AddTime", sp);
        }
        /// <summary>
        /// 查询Email是否存在
        /// </summary>
        /// <param name="mail">Email地址</param>
        /// <returns>返回true 为地址不存在，返回false 为地址已存在</returns>
        public bool SelectByEmail(string mail, string state, string type)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("email", mail), new SqlParameter("state", state), new SqlParameter("type", type) };
            if (!string.IsNullOrEmpty(state))
                state = " and state=@state";
            if (!string.IsNullOrEmpty(type))
                type = " and SubscribeToType=@type";
            SqlHelper.ExecuteTable(CommandType.Text, "Select Count(*) From " + strTableName + " Where Email=@email", sp);

            bool returnbool = false;
            if (dt.Rows[0][0].ToString() == "0")
            {
                returnbool = true;
            }
            else
            {
                returnbool = false;
            }
            return returnbool;
        }
        /*-------------------------------*/
        //邮件模板操作
        public string GetTypeName(MailType type)
        {
            switch (type)
            {
                case MailType.NewUserReg:
                    return "新会员注册";
                case MailType.UserRegSuccess:
                    return "注册成功";
                case MailType.RetrievePWD:
                    return "找回密码";
                case MailType.MobileVerification:
                    return "手机验证短信";
                case MailType.OrderPlace:
                    return "订单下单管理员";
                case MailType.OrderPay:
                    return "订单付款管理员";
                case MailType.PlatReg:
                    return "能力中心注册";
                case MailType.SubMailVerification:
                    return "订阅邮件验证";
                case MailType.SubMailContent:
                    return "订阅邮件内容";
                case MailType.MailReg:
                    return "邮箱注册";
                case MailType.IDCOrder:
                    return "IDC订单邮件提醒";
                default:
                    return "";
            }
        }
        /// <summary>
        /// 获取邮件模板内容
        /// </summary>
        /// <param name="typename"></param>
        /// <returns></returns>
        public string SelByType(MailType typename)
        {
            string type = GetTypeName(typename);
            return FileSystemObject.ReadFile(function.VToP("/Common/MailTlp/" + type + ".html"));
        }
        /// <summary>
        /// 邮件模板类型
        /// </summary>
        public enum MailType
        {
            /// <summary>
            /// 新用户注册
            /// </summary>
            NewUserReg,
            /// <summary>
            /// 用户注册成功
            /// </summary>
            UserRegSuccess,
            /// <summary>
            /// 找回密码
            /// </summary>
            RetrievePWD,
            /// <summary>
            /// 手机验证短信
            /// </summary>
            MobileVerification,
            /// <summary>
            /// 订单下单管理员
            /// </summary>
            OrderPlace,
            /// <summary>
            /// 订单付款管理员
            /// </summary>
            OrderPay,
            /// <summary>
            /// 能力中心注册
            /// </summary>
            PlatReg,
            /// <summary>
            /// 订阅邮件验证
            /// </summary>
            SubMailVerification,
            /// <summary>
            /// 订阅邮件内容
            /// </summary>
            SubMailContent,
            /// <summary>
            /// 邮件注册
            /// </summary>
            MailReg,
            /// <summary>
            /// IDC订单邮件提醒
            /// </summary>
            IDCOrder
        }
    }
}