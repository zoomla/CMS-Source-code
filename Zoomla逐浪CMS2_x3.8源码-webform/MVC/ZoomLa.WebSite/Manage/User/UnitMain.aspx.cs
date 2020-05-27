using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
/*
 * 会员提成统计页,流水见UnitDetail.aspx
 */
namespace ZoomLaCMS.Manage.User
{
    public partial class UnitMain : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_User_Consume sumBll = new B_User_Consume();
        B_User_UnitWeek unitWeekBll = new B_User_UnitWeek();
        List<M_User_UnitWeek> consumeList = new List<M_User_UnitWeek>();
        //1:普通,2:经理,3:总监,4:总裁,5:股东(分红机制同于总裁,但可额外拿分红)
        public enum Level { User = 1, Manager = 2, Supervisor = 3, ChairMan = 4, StockHolder = 5 };
        //需要搜索的时间
        public DateTime STime
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["stime"]) && string.IsNullOrEmpty(Time_T.Text))
                {
                    Time_T.Text = Request.QueryString["stime"];
                }
                else if (string.IsNullOrEmpty(Time_T.Text)) { Time_T.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd"); }
                return Convert.ToDateTime(Time_T.Text);
            }
            set { Time_T.Text = value.ToString("yyyy/MM/dd"); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            StructDP.MyBind = MyBind;
            if (!IsPostBack)
            {
                //自动为用户升级(winform中已改为其单独生成树)
                //UpdateUser();
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='AdminManage.aspx'>用户管理</a></li><li><a href='UserManage.aspx'>会员管理</a></li><li><a href='UnitMain.aspx'>会员提成</a></li>");
            }
        }
        public void MyBind()
        {
            DataTable dt = SelByTime(); //unitWeekBll.SelByTime();
            dt.DefaultView.RowFilter = "RealUnit>0";
            dt = dt.DefaultView.ToTable();
            EGV.DataSource = dt;
            EGV.DataBind();
        }

        // 升级用户,升级完成后,需要重新获取一次数据
        public void UpdateUser()
        {
            //string logTlp = "用户:{0}升级,升级前:{1},升级后:{2}";
            consumeList.Clear();
            DataTable sumdt = sumBll.SelByTime();//业绩记录表
            M_User_UnitWeek model = new M_User_UnitWeek() { CDate = DateTime.Now };
            CreateUpTree(sumdt, model);
            //-------------开始升级
            DataTable userdt = buser.Sel();
            DataRow[] userdrs = userdt.Select("GroupID=" + (int)Level.User);
            for (int i = 0; i < userdrs.Length; i++)
            {
                DataRow dr = userdrs[i];
                M_UserInfo mu = new M_UserInfo().GetModelFromReader(dr);
                List<M_User_UnitWeek> myList = consumeList.Where(p => p.PUserID == mu.UserID).ToList();
                bool upflag = false;
                #region 普通会员升经理
                int line = 100 * 10000;//必须到100W
                int bigManCount = myList.Where(p => (p.AMount + p.ChildAMount) >= line).ToArray().Length;
                //必须有最少一个,超过100W的大部门下级
                if (bigManCount < 1) { continue; }
                else if (bigManCount >= 2)//超过两个用户达到了100W,直接升级
                {
                    upflag = true;
                }
                else //拥有1个大部门情况下,判断小部门(未满100W的部门)合计是否达到了100W
                {
                    double allmoney = myList.Where(p => (p.AMount + p.ChildAMount) < line).ToList().Sum(p => (p.ChildAMount + p.AMount));
                    if (allmoney >= line)
                    {
                        upflag = true;
                    }
                }
                if (upflag)
                {
                    //检测用户是否普通用户,避免错升
                    if (mu.GroupID == (int)Level.User)
                    {
                        mu.GroupID = (int)Level.Manager;
                        buser.UpDateUser(mu);
                        dr["GroupID"] = mu.GroupID;//参加下一级运算
                        UpdateTemp(model.childUser, mu.UserID, Level.Manager);
                    }
                }
                #endregion
            }
            //-----------------------------
            userdrs = userdt.Select("GroupID=" + (int)Level.Manager);
            for (int i = 0; i < userdrs.Length; i++)
            {
                DataRow dr = userdrs[i];
                //相对于上方不同的在于,需要统计下线(所有级中是否有人达到需求)
                M_UserInfo mu = new M_UserInfo().GetModelFromReader(dr);
                M_User_UnitWeek selfMod = consumeList.Where(p => p.UserID == mu.UserID).ToList()[0];
                #region 经理升级为总监(两名经理)
                //int count = selfMod.ChildGroupIDS.Where(p => p >= (int)Level.Manager).ToArray().Length;
                int count = selfMod.childUser.Where(p => p.GroupID >= (int)Level.Manager).ToList().Count;
                if (count >= 2)
                {
                    if (mu.GroupID == (int)Level.Manager)
                    {
                        mu.GroupID = (int)Level.Supervisor;
                        UpdateTemp(model.childUser, mu.UserID, Level.Supervisor);
                        dr["GroupID"] = mu.GroupID;
                        buser.UpDateUser(mu);
                    }
                }
                #endregion
            }
            userdrs = userdt.Select("GroupID=" + (int)Level.Supervisor);
            for (int i = 0; i < userdrs.Length; i++)
            {
                DataRow dr = userdrs[i];
                M_UserInfo mu = new M_UserInfo().GetModelFromReader(dr);
                M_User_UnitWeek selfMod = consumeList.Where(p => p.UserID == mu.UserID).ToList()[0];
                #region 总监升级为总裁(两名总监,一名经理级别以上)
                int supCount = selfMod.childUser.Where(p => p.GroupID >= (int)Level.Supervisor).ToList().Count;
                int manageCount = selfMod.childUser.Where(p => p.GroupID == (int)Level.Manager).ToList().Count;
                //int supCount = selfMod.ChildGroupIDS.Where(p => p >= (int)Level.Supervisor).ToArray().Length;
                //int manageCount = selfMod.ChildGroupIDS.Where(p => p == (int)Level.Manager).ToArray().Length;
                if (supCount >= 3 || (supCount == 2 && manageCount > 1))
                {
                    if (mu.GroupID == (int)Level.Supervisor)
                    {
                        mu.GroupID = (int)Level.ChairMan;
                        buser.UpDateUser(mu);
                        UpdateTemp(model.childUser, mu.UserID, Level.ChairMan);
                        dr["GroupID"] = mu.GroupID;
                    }
                }
                #endregion
            }
            userdrs = userdt.Select("GroupID=" + (int)Level.Supervisor);
            for (int i = 0; i < userdrs.Length; i++)
            {
                DataRow dr = userdrs[i];
                M_UserInfo mu = new M_UserInfo().GetModelFromReader(dr);
                M_User_UnitWeek selfMod = consumeList.Where(p => p.UserID == mu.UserID).ToList()[0];
                #region 总裁升级为股东(仅需一名总裁)
                //int count = selfMod.ChildGroupIDS.Where(p => p >= (int)Level.ChairMan).ToArray().Length;
                int count = selfMod.childUser.Where(p => p.GroupID >= (int)Level.ChairMan).ToArray().Length;
                if (count >= 1)
                {
                    if (mu.GroupID == (int)Level.ChairMan)
                    {
                        mu.GroupID = (int)Level.StockHolder;
                        buser.UpDateUser(mu);
                        UpdateTemp(model.childUser, mu.UserID, Level.StockHolder);
                        dr["GroupID"] = mu.GroupID;
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 按用户计算出本周的计录,用于计算提成(是否不需要重复计算?)
        /// 并生成分红记录写入数据表中
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public M_User_UnitWeek SelAMount(DateTime time)
        {
            consumeList.Clear();
            DataTable sumdt = sumBll.SelByTime();
            M_User_UnitWeek model = new M_User_UnitWeek() { CDate = time, Level = 0 };
            CreateTree(sumdt, model);
            //抽出所有的股东,再对其进行一次总业绩的1%加权分红
            List<M_User_UnitWeek> holderList = consumeList.Where(p => p.GroupID == (int)Level.StockHolder).ToList();
            if (holderList.Count > 0)
            {
                double allmoney = consumeList.Sum(p => p.AMount);
                double bonus = (allmoney * 0.01) / holderList.Count;
                foreach (M_User_UnitWeek holder in holderList)
                {
                    holder.Bonus = bonus;
                }
            }
            return model;
        }
        /// <summary>
        /// 1,根据提供的消费数据,递归按用户生成二叉树消费链表,并计算出提成
        /// 2,该方法用于提供后面所需要的逻辑判断的所有数据
        /// </summary>
        /// <param name="dt">需要统计的消费数据:ZL_User_Consume</param>
        /// <param name="pmodel">父模型,可使用一个空的UserID为零的模型,以此作为根</param>
        /// <return>pmodel中的是自己的层级链表,consumeList是全数据链接,用于筛选等逻辑</return>
        public void CreateTree(DataTable dt, M_User_UnitWeek pmodel)
        {
            DataRow[] child = null;
            if (pmodel.UserID > 0)
            {
                child = dt.Select("ParentUserID='" + pmodel.UserID + "'");
            }
            else
            {
                //无推荐人
                child = dt.Select("ParentUserID='' OR ParentUserID IS Null OR ParentUserID='0'");
            }
            foreach (DataRow dr in child)
            {
                //返回如下的几个值,金额和分红,子用户详情
                M_User_UnitWeek model = new M_User_UnitWeek() { CDate = pmodel.CDate };
                model.UserID = Convert.ToInt32(dr["UserID"]);
                model.UserName = DataConvert.CStr(dr["UserName"]);
                model.PUserID = DataConvert.CLng(dr["ParentUserID"]);
                model.AMount = DataConvert.CDouble(dr["AMount"]);//不能用Clng否转换失败
                model.GroupID = Convert.ToInt32(dr["GroupID"]);
                model.PModel = pmodel;
                model.Level = (pmodel.Level + 1);//注意对用户来说是相对,而不是绝对的
                CreateTree(dt, model);//递归树顶,从树顶回朔运算
                pmodel.ChildAMount += model.AMount + model.ChildAMount;//业绩金额汇总
                                                                       //-------本级可用于提成的金额汇总(需计算后才是真正的提成)
                pmodel.ChildUnit0 += model.AMount + model.Unit0;
                pmodel.ChildUnit10 += model.Unit10;
                pmodel.ChildUnit20 += model.Unit20;
                pmodel.ChildUnit30 += model.Unit30;
                //pmodel.ChildIDS += model.UserID + "," + model.ChildIDS; model.ChildIDS = "";//下级所有子用户IDS,用于查看业绩流水记录
                //-------下级子会员组别统计(存入GroupID)
                pmodel.childUser.AddRange(model.childUser);
                pmodel.childUser.Add(new M_ConsumeUser(dr));
                //pmodel.ChildGroupIDS = Arr_Merge(pmodel.ChildGroupIDS, model.ChildGroupIDS, model.GroupID);
                //-------其他
                pmodel.child.Add(model);
                consumeList.Add(model);
            }
            #region 提成计算区,根据不同的逻辑此块更改
            //经理:1%,总监:最大2%,总裁:最大3%,股东:最大3%,另有业绩加权分红1%(平分给所有股东)
            switch ((Level)pmodel.GroupID)
            {
                case Level.User:
                    pmodel.RealUnit0 = pmodel.ChildUnit30 + pmodel.ChildUnit20 + pmodel.ChildUnit10 + pmodel.ChildUnit0;
                    pmodel.Unit0 = pmodel.ChildUnit0;
                    pmodel.Unit10 = pmodel.ChildUnit10;
                    pmodel.Unit20 = pmodel.ChildUnit20;
                    pmodel.Unit30 = pmodel.ChildUnit30;
                    break;
                case Level.Manager://提成1%
                    pmodel.RealUnit10 = pmodel.ChildUnit0;
                    pmodel.RealUnit0 = pmodel.ChildUnit30 + pmodel.ChildUnit20 + pmodel.ChildUnit10;

                    pmodel.Unit10 = pmodel.ChildUnit0 + pmodel.ChildUnit10;
                    pmodel.Unit20 = pmodel.ChildUnit20;
                    pmodel.Unit30 = pmodel.ChildUnit30;
                    break;
                case Level.Supervisor:
                    pmodel.RealUnit20 = pmodel.ChildUnit0;
                    pmodel.RealUnit10 = pmodel.ChildUnit10;
                    pmodel.RealUnit0 = pmodel.ChildUnit30 + pmodel.ChildUnit20;

                    pmodel.Unit20 = pmodel.ChildUnit0 + pmodel.ChildUnit10 + pmodel.ChildUnit20;
                    pmodel.Unit30 = pmodel.ChildUnit30;
                    break;
                case Level.ChairMan:
                case Level.StockHolder:
                    pmodel.RealUnit30 = pmodel.ChildUnit0;
                    pmodel.RealUnit20 = pmodel.ChildUnit10;
                    pmodel.RealUnit10 = pmodel.ChildUnit20;
                    pmodel.RealUnit0 = pmodel.ChildUnit30;
                    //提成后的值转换提成比较,这样更上一级便不会重复计算提成
                    pmodel.Unit30 = pmodel.ChildUnit0 + pmodel.ChildUnit10 + pmodel.ChildUnit20 + pmodel.ChildUnit30;
                    break;
            }
            #endregion
            if (pmodel != null)//插入提成记录,用户
            {
                unitWeekBll.Insert(pmodel);
                //sumBll.UpdateUnit(pmodel, percent, pmodel.CDate);
            }
        }
        //用于升级的树,仅用于统计业绩
        public void CreateUpTree(DataTable dt, M_User_UnitWeek pmodel)
        {
            DataRow[] child = null;
            if (pmodel.UserID > 0)
            {
                child = dt.Select("ParentUserID='" + pmodel.UserID + "'");
            }
            else
            {
                //无推荐人
                child = dt.Select("ParentUserID='' OR ParentUserID IS Null OR ParentUserID='0'");
            }
            foreach (DataRow dr in child)
            {
                //返回如下的几个值,金额和分红,子用户详情
                M_User_UnitWeek model = new M_User_UnitWeek() { CDate = pmodel.CDate };
                model.UserID = Convert.ToInt32(dr["UserID"]);
                model.UserName = DataConvert.CStr(dr["UserName"]);
                model.PUserID = DataConvert.CLng(dr["ParentUserID"]);
                model.AMount = DataConvert.CDouble(dr["AMount"]);//不能用Clng否转换失败
                model.GroupID = Convert.ToInt32(dr["GroupID"]);
                //model.childUser.Add(new M_TempUser(dr));
                model.PModel = pmodel;
                model.Level = (pmodel.Level + 1);//注意对用户来说是相对,而不是绝对的
                CreateUpTree(dt, model);//递归树顶,从树顶回朔运算
                pmodel.ChildAMount += model.AMount + model.ChildAMount;//业绩金额汇总
                                                                       //-------本级可用于提成的金额汇总(需计算后才是真正的提成)
                pmodel.ChildUnit0 += model.AMount + model.Unit0;
                pmodel.ChildUnit10 += model.Unit10;
                pmodel.ChildUnit20 += model.Unit20;
                pmodel.ChildUnit30 += model.Unit30;
                //pmodel.ChildIDS += model.UserID + "," + model.ChildIDS; model.ChildIDS = "";//下级所有子用户IDS
                //-------下级子会员组别统计(存入GroupID)
                //pmodel.ChildGroupIDS = Arr_Merge(pmodel.ChildGroupIDS, model.ChildGroupIDS, model.GroupID);
                pmodel.childUser.AddRange(model.childUser);
                pmodel.childUser.Add(new M_ConsumeUser(dr));
                //-------其他
                pmodel.child.Add(model);
                consumeList.Add(model);
            }
        }
        //----------------------Tools
        // int数组合并
        public int[] Arr_Merge(int[] arr, int[] arr2, int gid)
        {
            if (arr == null) arr = new int[] { };
            if (arr2 == null) arr2 = new int[] { };
            int[] x = new int[arr.Length + arr2.Length + 1];
            arr.CopyTo(x, 0);
            arr2.CopyTo(x, arr.Length);
            x[(x.Length - 1)] = gid;
            return x;
        }
        public string GetUnit(object o, double precent)
        {
            double unit = Convert.ToDouble(o);
            if (unit > 0) { return "<span class='unit'>(" + unit * precent + ")</span>"; }
            else { return ""; }
        }
        //输入一个日期,返回周开始和周结束时间
        public DateTime GetWeekStart(DateTime dt)
        {
            int today = (int)dt.DayOfWeek;
            return dt.AddDays(-today + 1);
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Begin_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='UnitMain.aspx?uid=" + dr["UserID"] + "';");
            }
        }
        //移除指定日期之内的信息,并重新生成日志写入
        protected void ReBegin_Btn_Click(object sender, EventArgs e)
        {
            DataTable dt = unitWeekBll.SelByTime();
            if (dt.Rows.Count > 0)
            {
                //if (dt.Select("State=1").Length > 0) { function.WriteErrMsg("拒绝重新生成,日志中包含已分成记录"); }
                unitWeekBll.DelByTime(STime);
            }
            MyBind();
        }
        //按照日志,自动将钱打入用户帐上,20%入理财(UserPoint),80%入手动账户(Sicon)  (disuse)
        protected void AutoUnit_Btn_Click(object sender, EventArgs e)
        {
            return;
            //DataTable unitDT = unitWeekBll.GetNeedUnitDT(STime);
            //string ids = "";
            //if (unitDT.Rows.Count < 1)
            //{
            //    function.WriteErrMsg("没有需要分成的用户");
            //}
            //else
            //{
            //    //是否已分成过判断,现暂时关闭逻辑
            //    //if (unitDT.Select("State=1").Length > 0)
            //    //{
            //    //    function.WriteErrMsg("该日期已分成过,停止分成");
            //    //    return;
            //    //}
            //    //自动将金额打入目标用户
            //    foreach (DataRow dr in unitDT.Rows)
            //    {
            //        ids += dr["ID"] + ",";
            //        if (Convert.ToInt32(dr["RealUnit"]) < 1) continue;//无分成则跳过
            //        int uid = Convert.ToInt32(dr["UserID"]);
            //        int bonus = Convert.ToInt32(dr["RealUnit"]);
            //        //buser.ChangeVirtualMoney(uid, new M_UserExpHis()
            //        //{
            //        //    score = (int)(bonus * 0.2),
            //        //    detail = "管理奖20%入理财账户",
            //        //    ScoreType = (int)M_UserExpHis.SType.UserPoint
            //        //});
            //        //buser.ChangeVirtualMoney(uid, new M_UserExpHis()
            //        //{
            //        //    score = (int)(bonus * 0.8),
            //        //    detail = "管理奖80%入手动账户",
            //        //    ScoreType = (int)M_UserExpHis.SType.SIcon
            //        //});
            //    }
            //    ids = ids.TrimEnd(',');
            //    //unitWeekBll.UpdateStateByIDS(ids);
            //    unitWeekBll.UpdateStateByTime(STime);
            //}
            //MyBind();
            //function.WriteSuccessMsg("分成完成",Request.RawUrl,0);
        }
        public string GetStatus()
        {
            string tlp = "<a href='" + CustomerPageAction.customPath2 + "/User/Log4.aspx?ID=" + Eval("LogID") + "' title='查看处理日志' >{0}</a>";
            switch (Eval("MyStatus").ToString())
            {
                case "0":
                    return "<span>未处理</span>";
                case "1":
                    return string.Format(tlp, "<span style='color:green;'>已入账</span>");
                case "-1":
                    return string.Format(tlp, "<span style='color:red;'>入账失败(用户名或密码错误)</span>");
                default:
                    return "<span>未处理</span>";
            }
        }
        protected void Chk_CheckedChanged(object sender, EventArgs e)
        {
            MyBind();
        }
        //-------------
        public string GetDate()
        {
            string stime, etime = "";
            B_User_UnitWeek.GetWeekSE(Convert.ToDateTime(Eval("CDate")), out stime, out etime);
            return Convert.ToDateTime(stime).ToString("yyyy/MM/dd--") + Convert.ToDateTime(etime).ToString("MM/dd");
        }
        private void UpdateTemp(List<M_ConsumeUser> list, int uid, Level ltype)
        {
            try
            {
                list.First(p => p.UserID == uid).GroupID = (int)ltype;
            }
            catch { }
        }
        //--------------
        public DataTable SelByTime()
        {
            string fields = "*,B.StructureID,B.UserName";
            string where = " A.UserID>0 ";
            if (StructDP.StructID > 0)
            {
                where += " AND B.StructureID=" + StructDP.StructID;
            }
            return SqlHelper.JoinQuery(fields, unitWeekBll.TbName, "ZL_User", "A.UserID=B.UserID", where, "A.UserID DESC");
        }
    }
}