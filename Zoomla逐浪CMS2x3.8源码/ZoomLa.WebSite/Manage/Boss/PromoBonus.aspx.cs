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
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;

public partial class Manage_Boss_ProtoBonus : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_User_UnitWeek unitWeekBll = new B_User_UnitWeek();
    B_Deposit depBll = new B_Deposit();
    List<M_User_UnitWeek> unitList = new List<M_User_UnitWeek>();
    B_CodeModel modelBll = new B_CodeModel("ZL_C_fhmx");
    B_CodeModel promoBll = new B_CodeModel("ZL_User_PromoBonus");
    DataTable promoStruct = new DataTable();
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
    public DataTable depDT = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        promoStruct = promoBll.SelStruct();
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='AdminManage.aspx'>用户管理</a></li><li><a href='UserManage.aspx'>会员管理</a></li><li><a href='" + Request.RawUrl + "'>推广佣金</a></li>");
        }
    }
    //获取分红记录,生成树
    private void MyBind()
    {
        DataTable dt = promoBll.Sel();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    //计算佣金
    protected void Count_Btn_Click(object sender, EventArgs e)
    {
        depDT = depBll.SelSumByUid();
        M_User_UnitWeek unitMod = SelAMount(STime);
        MyBind();
    }
    //按用户分红记录,依据此来计算分红
    public M_User_UnitWeek SelAMount(DateTime time)
    {
        unitList.Clear();
        DataTable sumdt = SelForUnitByTime();
        M_User_UnitWeek model = new M_User_UnitWeek() { CDate = time, Level = 0 };
        CreateTree(sumdt, model);
        return model;
    }
    /// <summary>
    /// 1,根据提供的消费数据,递归按用户生成二叉树消费链表,并计算出提成
    /// 2,该方法用于提供后面所需要的逻辑判断的所有数据
    /// </summary>
    /// <param name="dt">需要统计的消费数据:ZL_User_Consume</param>
    /// <param name="pmodel">父模型,可使用一个空的UserID为零的模型,以此作为根</param>
    /// <return>pmodel中的是自己的层级链表,unitList是全数据链接,用于筛选等逻辑</return>
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
            model.Level = (pmodel.Level + 1);//注意对用户来说是相对,而不是绝对的
            model.PModel = pmodel;
            CreateTree(dt, model);//递归树顶,从树顶回朔运算
            //不需要统计业绩金额,每层都回朔
            //pmodel.ChildAMount += model.AMount + model.ChildAMount;//业绩金额汇总
            //-------本级可用于提成的金额汇总(需计算后才是真正的提成)
            //pmodel.ChildUnit0 += model.AMount + model.Unit0;
            //pmodel.ChildUnit10 += model.Unit10;
            //pmodel.ChildUnit20 += model.Unit20;
            //pmodel.ChildUnit30 += model.Unit30;
            //pmodel.ChildIDS += model.UserID + "," + model.ChildIDS; model.ChildIDS = "";//下级所有子用户IDS
            //-------下级子会员组别统计(存入GroupID)
            //pmodel.ChildGroupIDS = Arr_Merge(pmodel.ChildGroupIDS, model.ChildGroupIDS, model.GroupID);
            //-------其他
            pmodel.child.Add(model);
            unitList.Add(model);
        }//二叉树 end;
        #region 提成计算区
        if (pmodel.AMount > 0)
        {
            CountUnit(pmodel, pmodel.PModel);
        }
        #endregion
        if (pmodel != null)//插入提成记录,用户
        {
            //unitWeekBll.Insert(pmodel);
            //sumBll.UpdateUnit(pmodel, percent, pmodel.CDate);
        }
    }
    /// <summary>
    /// 递归最多20层分配用户的分红,每次分红都产生一条数据库记录
    /// </summary>
    /// <param name="startMod">起始模型,有分红数据,层次等信息</param>
    /// <param name="model">开始计算的模型,传值为 pmodel</param>
    /// <param name="dep">深度,从0开始</param>
    private void CountUnit(M_User_UnitWeek startMod, M_User_UnitWeek model)
    {
        int curLevel = (startMod.Level - model.Level);//起始层与现在层的层级差
        //根据层级,用户的投资额,计算出用户的分红比率,存入unit0中
        DataRow[] drs = depDT.Select("UserID=" + model.UserID);
        if (drs.Length > 1) { throw new Exception(model.UserID + "异常,同一用户出现多个数值"); }
        if (drs.Length > 0)//如果有存值存在(现记录),则记算分红
        {
            double bonus = 0, curper = 0;
            double per2=0.02,per5=0.05,per10=0.10;
            M_Deposit depMod = new M_Deposit().GetModelFromUnit(drs[0]);
            if (false)//预留给IB,最大20层
            {

            }
            else if (depMod.Money >= 50000)//15层
            {
                if (curLevel <= 5)
                {
                    curper = per10;
                }
                else if (curLevel <= 10)
                {
                    curper = per5;
                }
                else if (curLevel <= 15)
                {
                    curper = per2;
                }
            }
            else if (depMod.Money >= 10000)
            {
                if (curLevel <= 5)
                {
                    curper = per10;
                }
                else if (curLevel <= 10)
                {
                    curper = per5;
                }
            }
            else if (depMod.Money > 1000)
            {
                if (curLevel <= 5)
                {
                    curper = per10;
                }
            }
            else//不给予分红
            {

            }
            bonus = startMod.AMount * curper;
            //分红计算完毕,如有值,写入记录
            if (bonus > 0)
            {
                M_PromoBonus promoMod = new M_PromoBonus();
                promoMod.AMount = startMod.AMount;
                promoMod.MyLevel = curLevel;
                promoMod.Unit = bonus;
                promoMod.UserID = model.UserID;
                promoMod.UserName = model.UserName;
                promoMod.SUserID = startMod.UserID;
                promoMod.SUserName = startMod.UserName;
                //托管理财金额
                promoMod.Remark = "金额:" + depMod.Money + ",分红:" + startMod.AMount.ToString("f0") + ",层级:" + curLevel + ",佣金比率:" + curper.ToString("f2") + ",佣金:" + bonus;
                DataRow dr = promoMod.ModelToDR(promoStruct);
                promoBll.Insert(dr);
            }
        }
        //ZLLog.L(ZLEnum.Log.safe, model.PUserID + ":" + (model.PModel == null) + ":" + (startMod.Level - model.Level));
        //出口
        if (model.PUserID == 0 || model.PModel == null || (startMod.Level - model.Level) > 20) { return; }
        CountUnit(startMod, model.PModel);
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
    public string GetState()
    {
        switch (Eval("State").ToString())
        {
            case "0":
                return "<span style='color:green;'>未分成</span>";
            case "1":
                return "<span style='color:red;'>已分成入账</span>";
            default:
                return "数据异常,可能为NULL值";
        }
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
      
    }
    //仅用于分红提成
    private DataTable SelForUnitByTime(string time = "")
    {
        string stime, etime;
        B_User_UnitWeek.GetWeekSE(DataConvert.CDate(time), out stime, out etime);
        SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
        string fields = "A.UserID,A.UserName,A.ParentUserID,A.GroupID,A.HoneyName,B.AMount";
        string sql = "Select " + fields + " FROM ZL_User AS A LEFT JOIN"
                 + " (SELECT UserID,Sum(Unit)AS AMount FROM " + modelBll.TbName + " WHERE 1=1 ";
        if (!string.IsNullOrEmpty(time))
        {
            sql += " AND CDate BetWeen @stime AND @etime";
        }
        sql += " AND UserID>0 GROUP BY UserID)B ON A.UserID=B.UserID";
        return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
    }
    //-----------
    public string GetDate()
    {
        string stime, etime = "";
        B_User_UnitWeek.GetWeekSE(Convert.ToDateTime(Eval("CDate")), out stime, out etime);
        return Convert.ToDateTime(stime).ToString("yyyy/MM/dd--") + Convert.ToDateTime(etime).ToString("MM/dd");
    }
    public string GetBonusSource()
    {
        return "[" + Eval("SUserName") + "]的分红:" + Convert.ToDouble(Eval("AMount")).ToString("f0");
    }
    public class M_PromoBonus
    {
        public M_PromoBonus() { CDate = DateTime.Now; }
        public int ID;
        //分红时自己所处理的层级
        public int MyLevel = 0;
        //所获得的推广佣金
        public double Unit = 0;
        public int UserID = 0;
        public string UserName = "";
        //用于计算推广佣金的分红金额
        public double AMount = 0;
        //分红来源用户ID,应该是Sum过后的,所以无法获取到 BindID
        public int SUserID = 0;
        public string SUserName = "";
        public int MyStatus = 0;
        public DateTime CDate;
        //用于所看备注
        public string Remark = "";
        //详细备注,用于管理员
        public string Remind = "";
        public DataRow ModelToDR(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["AMount"] = AMount;
            dr["MyLevel"] = MyLevel;
            dr["Unit"] = Unit;
            dr["UserID"] = UserID;
            dr["UserName"] = UserName;
            dr["SUserID"] = SUserID;
            dr["SUserName"] = SUserName;
            dr["MyStatus"] = MyStatus;
            dr["CDate"] = CDate;
            dr["Remark"] = Remark;
            dr["Remind"] = Remind;
            return dr;
        }
    }
    public string GetStatus()
    {
        string tlp = "<a href='" + CustomerPageAction.customPath2 + "/User/Log4.aspx?ID=" + Eval("LogID") + "' title='查看处理日志' >{0}</a>";
        switch (Eval("MyStatus").ToString())
        {
            case "0":
                return "<span style='color:green;'>未处理</span>";
            case "1":
                return string.Format(tlp, "<span style='color:green;'>已入账</span>");
            case "-1":
                return string.Format(tlp, "<span style='color:red;'>入账失败(用户名或密码错误)</span>");
            default:
                return "<span>异常状态</span>";
        }
    }
}