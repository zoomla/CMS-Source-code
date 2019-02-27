namespace ZoomLa.WebSite.Manage.User
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.BLL;
    public partial class UserExp : CustomerPageAction
    {
        B_User buser = new B_User();
        B_Admin badmin = new B_Admin();
        B_History hisBll = new B_History();
        public int EditID { get { return DataConverter.CLng(Request.QueryString["editid"]); } }
        public int ExpType { get { return DataConverter.CLng(Request.QueryString["type"]); } }
        public int UserID { get { return DataConverter.CLng(Request.QueryString["UserID"]); } }
        private int OrderID { get { return DataConverter.CLng(Request.QueryString["orderid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserID < 1) { function.WriteErrMsg("未指定会员ID"); }
            if (ExpType < 1) { function.WriteErrMsg("未指定虚拟币类型"); }
            if (!IsPostBack)
            {
                //订单赠送积分,自动填写备注
                if (OrderID > 0) 
                {
                    B_OrderList orderBll = new B_OrderList();
                    M_OrderList orderMod = orderBll.SelReturnModel(OrderID);
                    if (orderMod == null) { function.WriteErrMsg("指定的订单不存在"); }
                    TxtDetail.Text = "订单:[" + orderMod.OrderNo + "]";
                }
                if (EditID > 0)
                {
                    UserPDiv.Visible = false;
                    EditMoney.Visible = true;
                    M_UserExpHis expMod = hisBll.SelReturnModel((M_UserExpHis.SType)ExpType, EditID);
                    UserExpDomPID.Text = expMod.ExpHisID.ToString();
                    HisTime.Text = expMod.HisTime.ToString();
                    Score.Text = expMod.score.ToString();
                    Detail.Text = expMod.detail;
                    UserPH.Value = expMod.ExpHisID.ToString();
                    return;
                }
                else
                {
                    MyBind();
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>会员管理</a></li><li><a href='UserInfo.aspx?ID=" + UserID + "'>会员信息</a></li><li class='active'>会员" + GetTypeName() + "操作</li>");
            }
        }
        public void MyBind()
        {
            M_UserInfo mu = buser.SelReturnModel(UserID);
            Lbl_t.Text = "会员" + GetTypeName() + "操作";
            lbUserName.Text = mu.UserName;
            lblExp.Text = GetExp(mu).ToString();
            DataTable dt = hisBll.SelByType_U(ExpType,UserID);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "edit1":
                    break;
                default:
                    break;
            }
        }
        public string GetUserName(string adminid)
        {
            M_AdminInfo info = B_Admin.GetAdminByAdminId(DataConverter.CLng(adminid));
            if (info != null)
            {
                return info.AdminName;
            }
            else
            {
                return "";
            }
        }
        //根据类型获取币种值
        public double GetExp(M_UserInfo mu)
        {
            switch (ExpType)
            {
                case 1:
                    return mu.Purse;
                case 2:
                    return mu.SilverCoin;
                case 3:
                    return mu.UserExp;
                case 4:
                    return mu.UserPoint;
                case 5:
                    return mu.DummyPurse;
                case 6:
                    return mu.UserCreit;
                default:
                    return 0;
            }
        }
        //获取币种类型名称
        public string GetTypeName()
        {
            switch (ExpType)
            {
                case 1:
                    return "金额";
                case 2:
                    return "银币";
                case 3:
                    return "积分";
                case 4:
                    return "点卷";
                case 5:
                    return "虚拟币";
                case 6:
                    return "信誉值";
                default:
                    return "";
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            //无同步扣减功能
            if (isOK())
            {
                double score = DataConverter.CDouble(TxtScore.Text.Trim());
                string option = "增加";
                if (rblExp.SelectedValue.Equals("2"))
                {
                    option = "扣减";
                    score = DataConverter.CDouble("-" + TxtScore.Text.Trim());
                }
                buser.ChangeVirtualMoney(UserID, new M_UserExpHis()
                {
                    score = score,
                    ScoreType = ExpType,
                    detail = "管理员" + option + "用户" + GetTypeName() + ":" + score + " 备注:" + TxtDetail.Text
                });
                function.WriteSuccessMsg(option + GetTypeName() + "成功!");
            }
        }
        public bool isOK()
        {
            bool flag = true;
            M_UserInfo him = buser.GetUserByName(SourceMem.Text.Trim());
            M_UserInfo me = buser.GetUserByName(lbUserName.Text);
            if (him.UserID == me.UserID)
                function.WriteErrMsg("账户名不能相同");
            double score = DataConverter.CDouble(TxtScore.Text.Trim());
            if (score < 0) { function.WriteErrMsg("分值必须为正整数"); }
            if (rblExp.SelectedValue.Equals("2"))
            {
                bool meflag = false;//该用户判断状态
                bool himflag = false;//陪同用户判断状态
                double me_exp = 0;//该用户分值
                double him_exp = 0;//陪同用户分值
                switch (ExpType)
                {
                    case 1://金额
                        meflag = me.Purse < score;
                        me_exp = me.Purse;
                        himflag = him.Purse < score;
                        him_exp = him.Purse;
                        break;
                    case 2://银币
                        meflag = me.SilverCoin < score;
                        me_exp = me.SilverCoin;
                        himflag = him.SilverCoin < score;
                        him_exp = him.SilverCoin;
                        break;
                    case 3://积分
                        meflag = me.UserExp < score;
                        me_exp = me.UserExp;
                        himflag = him.SilverCoin < score;
                        him_exp = him.UserExp;
                        break;
                    case 4://点卷
                        meflag = me.UserPoint < score;
                        me_exp = me.UserPoint;
                        himflag = him.UserPoint < score;
                        him_exp = him.UserPoint;
                        break;
                    case 5://虚拟币
                        meflag = me.DummyPurse < score;
                        me_exp = me.DummyPurse;
                        himflag = him.DummyPurse < score;
                        him_exp = him.DummyPurse;
                        break;
                    case 6://信誉值
                        meflag = me.UserCreit < score;
                        me_exp = me.UserCreit;
                        himflag = him.UserCreit < score;
                        him_exp = him.UserCreit;
                        break;
                }
                if (meflag)
                    function.WriteErrMsg("会员" + me.UserName + "仅有"+GetTypeName()+":" + me_exp + "不够扣减");
                if (himflag && !him.IsNull)
                    function.WriteErrMsg("会员" + him.UserName + "仅有" + GetTypeName() + ":" + him_exp + "不够扣减");
            }
            return flag;
        }
        protected void EditBtn_Click(object sender, EventArgs e)
        {
            M_UserExpHis expMod = hisBll.SelReturnModel((M_UserExpHis.SType)ExpType,EditID);
            expMod.Operator = badmin.GetAdminLogin().AdminId;
            expMod.score = Convert.ToDouble(Score.Text);
            expMod.detail = Detail.Text;
            expMod.ExpHisID = EditID;
            expMod.ScoreType = ExpType;
            expMod.UserID = UserID;
            hisBll.UpdateByID(expMod);
            Response.Redirect("UserExp.aspx?UserID="+UserID+"&type="+ExpType);
        }
    }
}