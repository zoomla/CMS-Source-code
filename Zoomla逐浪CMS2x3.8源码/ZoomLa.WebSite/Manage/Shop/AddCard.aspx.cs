using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class manage_Shop_AddCard : CustomerPageAction
{

    private B_User buser = new B_User();
    private B_Card bcard = new B_Card();
    private B_CardType btype = new B_CardType();
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();


        if (!this.Page.IsPostBack)
        {
            string city = "辽宁|LN、吉林|JL、黑龙江|HLJ、河北|HB、山西|SX、陕西|SX2、甘肃|GS、青海|QH、山东|SD、安徽|NH、江苏|JS、浙江|ZJ、河南|HN、湖北|HB、湖南|HN、江西|JX、台湾|TW、福建|FJ、云南|YN、海南|HN、四川|SC、贵州|GZ、广东|GD、内蒙古|NMG、新疆|XJ、广西|GX、西藏|XZ、宁夏|LX、北京|BJ、上海|SH、天津|TJ、重庆|CQ、香港|XG、澳门|AM";
            string[] arrcity = city.Split(new char[] { '、' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arrcity.Length; i++)
            {
                string[] arrcitys = arrcity[i].Trim().ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                ListItem Lists = new ListItem(arrcitys[0].Trim().ToString(), arrcitys[1].Trim().ToString());
            }
            DataTable dt = btype.SelectAll();
            this.DropDownList2.DataSource = dt;
            this.DropDownList2.DataValueField = "c_id";
            this.DropDownList2.DataTextField = "typename";
            this.DropDownList2.DataBind();

            if (dt != null)
            {
                dt.Dispose();
            }
            tx_user.Text = buser.GetLogin().UserName;
            createtime_T.Text = DateTime.Now.ToString();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='CardManage.aspx'>VIP卡管理</a></li><li>生成VIP卡</li>");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DataConverter.CDate(endtime.Text) < DateTime.Now.Date)
        {
            function.WriteErrMsg("到期时间不能早于当前时间!");
            return;
        }
        if (DataConverter.CDate(createtime_T.Text) >= DataConverter.CDate(endtime.Text))
        {
            function.WriteErrMsg("到期时间不能晚于发布时间!");
            return;
        }
        int numo = DataConverter.CLng(this.num.Text);
        for (int i = 0; i < numo; i++)
        {
            M_Card caro = new M_Card();
            M_UserInfo muser = buser.GetUserByName(this.tx_user.Text.Trim());
            caro.PutUserID = muser.UserID;
            //caro.CardNum = cardnumo(caro.PutUserID, num);
            string len = "asdfghjklmnbvcxzpoiuytrewqASDFGHJKLPOIUYTREWQZXCVBNM";
            string n = "1234567890";
            string m = "mnbvcxzqwertyuioplkjhgfdsaLPOKMIJNUHBYGVTFCRDXESZWAQ0192837465";
            int length = DataConverter.CLng(Len_T.Text.Trim());
            if (length < 4) { function.WriteErrMsg("卡号长度不能少于4位!"); }
            switch (EcodeType.SelectedItem.Value)
            {
                case "0":
                    caro.CardNum = DataSecurity.MakeRandomString(n, length);
                    break;
                case "1":
                    caro.CardNum = "ZL" + DataSecurity.MakeRandomString(len, length - 2);
                    break;
                case "2":
                    caro.CardNum = "ZL" + DataSecurity.MakeRandomString(m, length - 2);
                    break;
                default:
                    break;
            }
            caro.AddTime = DataConverter.CDate(createtime_T.Text);
            caro.CircumscribeTime = DataConverter.CDate(endtime.Text);
            caro.CardPwd = "ZL" + DataSecurity.MakeRandomString(n, 3) + DataSecurity.MakeRandomString(len, 1) + DataSecurity.MakeRandomString(n, 1) + DataSecurity.MakeRandomString(m, 1);
            caro.VIPType = DataConverter.CLng(DropDownList2.SelectedValue.ToString());
            caro.CircumscribeTime = DataConverter.CDate(endtime.Text);
            caro.CardState = DataConverter.CLng(Request["isopen"]);
            caro.ActivateState = 0;
            bcard.GetInsert(caro);
            System.Threading.Thread.Sleep(50); //停0.05秒
        }
        function.WriteSuccessMsg("添加成功!","CardManage.aspx");
    }

    //public string cardnumo(int userid, int num)
    //{
    //    Random shu = new Random();
    //    string aa = num.ToString();

    //    DataTable dd = bcard.SelByNum(num);
    //    if (dd.Rows.Count > 0)
    //    {            
    //        aa = cardnumo(userid, num+1);
    //    }
    //    return aa;
    //}   
}
