using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Other;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Other;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.WeiXin
{
    public partial class RedPacketAdd : CustomerPageAction
    {
        B_WX_RedPacket redBll = new B_WX_RedPacket();
        B_WX_RedDetail detBll = new B_WX_RedDetail();
        B_WX_APPID appBll = new B_WX_APPID();
        private int AppID { get { return DataConvert.CLng(Request.QueryString["AppID"]); } }
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppID < 1) {function.WriteErrMsg("未指定APPID"); }
                MyBind();
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                M_WX_RedPacket redMod = redBll.SelReturnModel(Mid);
                Name_T.Text = redMod.Name;
                Flow_T.Text = redMod.Flow;
                AmountRange_T.Text = redMod.AmountRange;
                RedNum_T.Text = redMod.RedNum.ToString();
                CodeFormat_T.Text=redMod.CodeFormat;
                SDate_T.Text = redMod.SDate.ToString("yyyy-MM-dd");
                EDate_T.Text = redMod.EDate.ToString("yyyy-MM-dd");
                Wishing_T.Text = redMod.Wishing;
                Remind_T.Text = redMod.Remind;
                Save_Btn.Text="保存信息";

                CodeFormat_T.Attributes["disabled"]="disabled";
                RedNum_T.Attributes["disabled"]="disabled";
                AmountRange_T.Attributes["disabled"]="disabled";
            }
            else
            {
                Flow_T.Text=function.GetRandomString(10).ToUpper();
                SDate_T.Text = DateTime.Now.ToString("yyyy-MM-dd");
                EDate_T.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
            }
            M_WX_APPID appMod=appBll.SelReturnModel(AppID);
            Alias_L.Text=appMod.Alias;
            Call.SetBreadCrumb(Master, "<li><a href='Home.aspx'>移动微信</a></li><li class='active'><a href='RedPacket.aspx'>红包列表</a></li><li><a href='"+Request.RawUrl+"'>红包管理</a>[公众号:"+appMod.Alias+"]</li>");
        }

        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_AdminInfo adminMod = B_Admin.GetLogin();
            M_WX_RedPacket redMod = new M_WX_RedPacket();
            if (Mid > 0) { redMod = redBll.SelReturnModel(Mid); }
            redMod.AppID=AppID;
            redMod.Name = Name_T.Text;
            redMod.SDate = Convert.ToDateTime(SDate_T.Text);
            redMod.EDate = Convert.ToDateTime(EDate_T.Text);
            redMod.Wishing = Wishing_T.Text;
            redMod.Remind = Remind_T.Text;
            redMod.Flow=Flow_T.Text;
            if (redMod.EDate <= redMod.SDate) {function.WriteErrMsg("结束时间不能小于起始时间"); }
            if (string.IsNullOrEmpty(redMod.Wishing)) { redMod.Wishing = "祝你生活愉快"; }
            if (redMod.ID > 0)
            {
                redBll.UpdateByID(redMod);
            }
            else
            {
                redMod.RedNum = DataConvert.CLng(RedNum_T.Text);
                redMod.AmountRange = AmountRange_T.Text;
                redMod.CodeFormat = CodeFormat_T.Text;
                if (redMod.RedNum < 1) {function.WriteErrMsg("红包数量不能小于1"); }
                double minMoney = 0, maxMoney = 0;
                if (redMod.AmountRange.Contains("-"))
                {
                    minMoney = DataConvert.CDouble(redMod.AmountRange.Split('-')[0]);
                    maxMoney = DataConvert.CDouble(redMod.AmountRange.Split('-')[1]);
                }
                else
                {
                    minMoney = DataConvert.CDouble(redMod.AmountRange);
                    maxMoney = DataConvert.CDouble(redMod.AmountRange);
                }
                if (minMoney < 1 || maxMoney < 1) { function.WriteErrMsg("红包金额不能小于1"); }
                if (minMoney > 200 || maxMoney > 200) { function.WriteErrMsg("红包金额不能大于200"); }
                if (maxMoney < minMoney) { function.WriteErrMsg("红包的最大值不能小于最小值"); }

                redMod.AdminID = adminMod.AdminId;
                redMod.ID = redBll.Insert(redMod);
                //------------------------
                for (int i = 0; i < redMod.RedNum; i++)
                {
                    M_WX_RedDetail model = new M_WX_RedDetail();
                    model.MainID = redMod.ID;
                    model.ZStatus = 1;
                    model.Amount = new Random(Guid.NewGuid().GetHashCode()).NextDouble() * (maxMoney - minMoney) + minMoney;
                    model.RedCode = function.GetCodeByFormat(redMod.CodeFormat);
                    detBll.Insert(model);
                }
            }
             function.WriteSuccessMsg("修改成功", "RedPacket.aspx?AppID="+AppID);
        }
    }
}