using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.Components;

public partial class User_UserFunc_MoneyConver : System.Web.UI.Page
{
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        Array list = Enum.GetValues(typeof(M_UserExpHis.SType));
        foreach (int value in list)
        {
            ListItem item = new ListItem(buser.GetVirtualMoneyName(value), value.ToString());
            VirtualType_Drop.Items.Add(item);
        }
        VirtualType_Drop.SelectedIndex = 0;
        LoadTargetVirtual(DataConverter.CLng(VirtualType_Drop.SelectedValue));
        UpdateScore();
    }
    //更新用户相应币值
    public void UpdateScore()
    {
        M_UserInfo mu = buser.GetLogin();
        int vtype = DataConverter.CLng(VirtualType_Drop.SelectedValue);
        int ttype = DataConverter.CLng(TargetVirtual_Drop.SelectedValue);
        VirtualScore_L.Text ="当前"+buser.GetVirtualMoneyName(vtype)+":"+buser.GetVirtualMoney(mu, vtype).ToString();
        TargetScore_L.Text = "当前" + buser.GetVirtualMoneyName(ttype) + ":"+ buser.GetVirtualMoney(mu, ttype).ToString();
        UserScore_Hid.Value = buser.GetVirtualMoney(mu,vtype).ToString();
    }
    public void LoadTargetVirtual(int vtype)
    {
        M_UserExpHis.SType stype = (M_UserExpHis.SType)vtype;
        string types = "";//可兑换币种,格式为:"1,2"
        switch (stype)
        {
            case M_UserExpHis.SType.Purse:
                types = (int)M_UserExpHis.SType.DummyPoint + ","
                       + (int)M_UserExpHis.SType.UserPoint+","
                       +(int)M_UserExpHis.SType.Point;
                break;
            case M_UserExpHis.SType.SIcon:
                types = (int)M_UserExpHis.SType.Point+"";
                break;
            case M_UserExpHis.SType.Point:
                types = (int)M_UserExpHis.SType.UserPoint + ","
                        + (int)M_UserExpHis.SType.Purse + ","
                        + (int)M_UserExpHis.SType.SIcon;
                break;
            case M_UserExpHis.SType.UserPoint:
                types = (int)M_UserExpHis.SType.Purse + ","
                        +(int)M_UserExpHis.SType.Point;
                break;
            case M_UserExpHis.SType.DummyPoint:
                types = (int)M_UserExpHis.SType.Purse+"";
                break;
            case M_UserExpHis.SType.Credit:
                break;
            default:
                break;
        }
        TargetVirtual_Drop.Items.Clear();
        if (string.IsNullOrEmpty(types))
        {
            TargetVirtual_Drop.Items.Add(new ListItem("当前币种不支持兑换","-1"));
            Formula_L.Text = "";
            ConvertRate_Hid.Value = "";
            return;
        }
        foreach (string item in types.Split(','))
        {
            ListItem listitem = new ListItem(buser.GetVirtualMoneyName(int.Parse(item)),item);
            TargetVirtual_Drop.Items.Add(listitem);
        }
        Formula_L.Text=GetFormula(VirtualType_Drop.SelectedValue, TargetVirtual_Drop.SelectedValue);
    }
    //MoneyExchangePointByMoney,MoneyExchangePointByPoint 资金转点券
    //MoneyExchangeDummyPurseByMoney,MoneyExchangeDummyPurseByDummyPurse 资金转虚拟币
    //UserExpExchangePointByExp,UserExpExchangePointByPoint 积分转点券
    //PointExp,PointMoney 积分转资金
    //ChangeSilverCoinByExp,PointSilverCoin 积分转银币

    //获取虚拟币计算公式
    //当value>0时返回转换值，否则返回公式说明
    public string GetFormula(string vtype, string ttype,int value=0)
    {
        int vtype_num = int.Parse(vtype);//兑换币种(数字)
        int ttype_num = int.Parse(ttype);//目标币种(数字)
        double vtype_value = 0;//兑换值
        double ttype_value = 0;//目标兑换值
        vtype = "," + vtype + ",";
        ttype = "," + ttype + ",";
        //虚拟币转换组合(,1,2,)
        string types = ","+(int)M_UserExpHis.SType.Purse+","+(int)M_UserExpHis.SType.UserPoint+",";
        if (types.Contains(vtype)&&types.Contains(ttype))//资金与点券的兑换比率
        {
            if (vtype_num == (int)M_UserExpHis.SType.Purse)
            {
                vtype_value = SiteConfig.UserConfig.MoneyExchangePointByMoney;
                ttype_value = SiteConfig.UserConfig.MoneyExchangePointByPoint;
            }
            else//点券转资金
            {
                vtype_value = SiteConfig.UserConfig.MoneyExchangePointByPoint;
                ttype_value = SiteConfig.UserConfig.MoneyExchangePointByMoney;
            }
        }
        types = "," + (int)M_UserExpHis.SType.Purse + "," + (int)M_UserExpHis.SType.DummyPoint + ",";
        if (types.Contains(vtype)&&types.Contains(ttype))//资金与虚拟币的兑换比率
        {
            if (vtype_num == (int)M_UserExpHis.SType.Purse)
            {
                vtype_value = SiteConfig.UserConfig.MoneyExchangeDummyPurseByMoney;
                ttype_value = SiteConfig.UserConfig.MoneyExchangeDummyPurseByDummyPurse;
            }
            else//虚拟币转资金
            {
                vtype_value = SiteConfig.UserConfig.MoneyExchangeDummyPurseByDummyPurse;
                ttype_value = SiteConfig.UserConfig.MoneyExchangeDummyPurseByMoney;
            }
        }
        types = "," + (int)M_UserExpHis.SType.Point + "," + (int)M_UserExpHis.SType.UserPoint + ",";
        if(types.Contains(vtype)&&types.Contains(ttype))//积分与点券的兑换比率
        {
            if (vtype_num == (int)M_UserExpHis.SType.Point)
            {
                vtype_value = SiteConfig.UserConfig.UserExpExchangePointByExp;
                ttype_value = SiteConfig.UserConfig.UserExpExchangePointByPoint;
            }
            else//点券转积分
            {
                vtype_value = SiteConfig.UserConfig.UserExpExchangePointByPoint;
                ttype_value = SiteConfig.UserConfig.UserExpExchangePointByExp;
            }
        }
        types = "," + (int)M_UserExpHis.SType.Point + "," + (int)M_UserExpHis.SType.Purse + ",";
        if (types.Contains(vtype)&&types.Contains(ttype))//积分与资金的兑换比率
        {
            if (vtype_num == (int)M_UserExpHis.SType.Point)
            {
                vtype_value = SiteConfig.UserConfig.PointExp;
                ttype_value = SiteConfig.UserConfig.PointMoney;
            }
            else//资金转积分
            {
                vtype_value = SiteConfig.UserConfig.PointMoney;
                ttype_value = SiteConfig.UserConfig.PointExp;
            }
        }
        types = "," + (int)M_UserExpHis.SType.Point + "," + (int)M_UserExpHis.SType.SIcon + ",";
        if (types.Contains(vtype)&&types.Contains(ttype))//积分与银币的兑换比率
        {
            if (vtype_num == (int)M_UserExpHis.SType.Point)
            {
                vtype_value = SiteConfig.UserConfig.ChangeSilverCoinByExp;
                ttype_value = SiteConfig.UserConfig.PointSilverCoin;
            }
            else//银币转积分
            {
                vtype_value = SiteConfig.UserConfig.PointSilverCoin;
                ttype_value = SiteConfig.UserConfig.ChangeSilverCoinByExp;
            }
        }
        if (value > 0)
        {
            return ((int)(value * (ttype_value / vtype_value))).ToString("f2");
        }
        ConvertRate_Hid.Value = (ttype_value / vtype_value).ToString();//兑换比率
        return vtype_value + buser.GetVirtualMoneyName(vtype_num) + "=" + ttype_value + buser.GetVirtualMoneyName(ttype_num);
    }
    protected void VirtualType_Drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTargetVirtual(DataConverter.CLng(VirtualType_Drop.SelectedValue));
        UpdateScore();
    }
    protected void TargetVirtual_Drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        Formula_L.Text = GetFormula(VirtualType_Drop.SelectedValue, TargetVirtual_Drop.SelectedValue);
        UpdateScore();
    }
    protected void Convert_Btn_Click(object sender, EventArgs e)
    {
        int vtype = DataConverter.CLng(VirtualType_Drop.SelectedValue);
        int ttype = DataConverter.CLng(TargetVirtual_Drop.SelectedValue);
        M_UserInfo mu = buser.GetLogin();
        double score = buser.GetVirtualMoney(mu, vtype);
        int value = DataConverter.CLng(Score_T.Text);
        if (value < 1) { function.WriteErrMsg("兑换值不能小于1"); }
        if (value > score) { function.WriteErrMsg("您没有足够的" + buser.GetVirtualMoneyName(vtype)); }
        double targetValue =DataConverter.CDouble(GetFormula(vtype.ToString(), ttype.ToString(), value));
        buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis() {
            score = -value,
            ScoreType = vtype,
            detail = "兑换" + targetValue + buser.GetVirtualMoneyName(ttype) + ",扣除" + value + buser.GetVirtualMoneyName(vtype)
        });
        buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
        {

            score=targetValue,
            ScoreType=ttype,
            detail = "兑换" + targetValue + buser.GetVirtualMoneyName(ttype) + ",增加" + value + buser.GetVirtualMoneyName(ttype)
        });
        function.WriteSuccessMsg("兑换成功!");
    }
}