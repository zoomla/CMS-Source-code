using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class EditShare : CustomerPageAction
    {
        B_Order_Share shareBll = new B_Order_Share();
        public int Pid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.HideBread(Master);
            }
        }
        public void MyBind()
        {
            M_Order_Share shareMod = shareBll.SelReturnModel(Pid);
            Title_T.Text = shareMod.Title;
            star_hid.Value = shareMod.Score.ToString();
            MsgContent_T.Text = shareMod.MsgContent;
            Imgs_Hid.Value = shareMod.Imgs;
            back_a.HRef = "/user/order/ShareList.aspx?ProID=" + shareMod.ProID + "&mode=admin";
            if (shareMod.Pid > 0) { function.Script(this, "HideShareTr();"); }
        }

        protected void Edit_Btn_Click(object sender, EventArgs e)
        {
            M_Order_Share shareMod = shareBll.SelReturnModel(Pid);
            shareMod.Title = Title_T.Text;
            shareMod.Score = DataConverter.CLng(star_hid.Value);
            shareMod.MsgContent = MsgContent_T.Text;
            shareMod.Imgs = Attach_Hid.Value;
            shareBll.UpdateByID(shareMod);
            function.WriteSuccessMsg("修改成功!", back_a.HRef);
        }
    }
}