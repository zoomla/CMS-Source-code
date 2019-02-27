using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.Helper;

namespace ZoomLa.WebSite.User
{
    public partial class UserInfo : Page
    {
        B_User buser = new B_User();
        B_Group bgp = new B_Group();
        B_Model bmodel = new B_Model();
        B_ModelField Fll = new B_ModelField();
        B_PointGrounp pointBll=new B_PointGrounp();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin(false);
                this.LblUser.Text = mu.UserName;
                this.LblEmail.Text = mu.Email;
                this.LblGroup.Text = bgp.GetByID(mu.GroupID).GroupName;
                this.LblJoinTime.Text = mu.JoinTime.ToString();
                this.LblRegTime.Text = mu.RegTime.ToString();
                this.LblLoginTimes.Text = mu.LoginTimes.ToString();
                this.LblLastLogin.Text = mu.LastLoginTimes.ToString();
                this.LblLastIP.Text = IPScaner.IPLocation(mu.LastLoginIP);
                this.LblLastModify.Text = (mu.LastPwdChangeTime == mu.RegTime) ? "" : mu.LastPwdChangeTime.ToString();
                this.LblLastLock.Text = (mu.LastLockTime == mu.RegTime) ? "" : mu.LastLockTime.ToString();
                this.Purse_L.Text = mu.Purse.ToString();
                this.Point_L.Text = mu.UserExp.ToString();
                Sicon_L.Text = mu.SilverCoin.ToString();
                UserPoint_L.Text = mu.UserPoint.ToString();
                this.LblboffExp.Text = mu.boffExp.ToString();
                this.LblConsumeExp.Text = mu.ConsumeExp.ToString();
                this.DummyPurse_L.Text = mu.DummyPurse.ToString();
                M_PointGrounp pointmod = pointBll.SelectPintGroup(mu.UserExp);
                gradeTxt.Text = "";
                if (pointmod != null)
                {
                    gradeTxt.Text = pointmod.GroupName;
                    LvIcon_Span.Attributes["class"] = pointmod.ImgUrl;
                }
                if (DataConvert.CLng(mu.ParentUserID) > 0)
                {
                    M_UserInfo usinfo = buser.GetSelect(Convert.ToInt32(mu.ParentUserID));
                    LblParentUserID.Text = string.IsNullOrEmpty(mu.UserName) ? "" : mu.UserName;
                }
                int UserModelID = DataConverter.CLng(bgp.GetGroupModel(mu.GroupID));
                M_ModelInfo modelinfo = bmodel.GetModelById(UserModelID);
                if (modelinfo == null || modelinfo.IsNull || string.IsNullOrEmpty(modelinfo.TableName)) { }
                else
                {
                    GroupModelField_Li.Text = Fll.InputallHtml(UserModelID, 0, new ModelConfig()
                    {
                        ValueDT = bmodel.SelUserModelField(UserModelID, mu.UserID)
                    });
                }
            }
        }
    }
}