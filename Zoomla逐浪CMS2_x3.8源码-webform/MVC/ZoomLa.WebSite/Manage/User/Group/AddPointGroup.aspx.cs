using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.User
{
    public partial class AddPointGroup : CustomerPageAction
    {
        B_PointGrounp bpgr = new B_PointGrounp();

        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li>" + Resources.L.后台管理 + "</li><li><a href=\"UserManage.aspx\">" + Resources.L.会员管理 + "</a></li><li><a href=\"PointGroup.aspx\">" + Resources.L.积分等级管理 + "</a></li><li><asp:Literal Text='" + Resources.L.添加房间 + "'>" + LNav_Hid.Value + "</asp:Literal></li>");
            if (!IsPostBack)
            {
                int id = DataConverter.CLng(Request.QueryString["ID"]);
                M_PointGrounp mpoin = bpgr.GetSelect(id);
                txt_imgUrl.Text = mpoin.ImgUrl;
                if (mpoin != null && mpoin.ID > 0)
                {
                    LNav_Hid.Value = Resources.L.修改积分等级;
                    LTitle.Text = Resources.L.修改积分等级;
                    txtPoint.Text = mpoin.PointVal.ToString();
                    txtPointGroup.Text = mpoin.GroupName;
                    TxtDescription.Text = mpoin.Remark;
                    txt_imgUrl.Text = mpoin.ImgUrl;
                }
                else
                {
                    LNav_Hid.Value = Resources.L.添加积分等级;
                    LTitle.Text = Resources.L.添加积分等级;
                }
                HdnGroupID.Value = id.ToString();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='AdminManage.aspx'>" + Resources.L.用户管理 + "</a></li><li><a href='UserManage.aspx'>" + Resources.L.会员管理 + "</a></li><li><a href='PointGroup.aspx'>" + Resources.L.积分等级管理 + "</a></li><li class='active'>" + Resources.L.添加积分等级 + "</a></li>");
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(HdnGroupID.Value);
            M_PointGrounp mp = bpgr.GetSelect(id);
            mp.GroupName = BaseClass.Htmlcode(txtPointGroup.Text);
            mp.PointVal = DataConverter.CLng(txtPoint.Text);
            mp.Remark = BaseClass.Htmlcode(TxtDescription.Text);
            mp.ImgUrl = txt_imgUrl.Text;
            if (id > 0)
            {
                bool result = bpgr.GetUpdate(mp);
                if (result)
                {
                    function.WriteSuccessMsg(Resources.L.修改成功 + "!", "PointGroup.aspx");
                }
                else
                {
                    function.WriteErrMsg(Resources.L.修改失败 + "!");
                }
            }
            else
            {
                mp.AddTime = DateTime.Now;
                int ids = bpgr.GetInsert(mp);
                if (ids > 0)
                {
                    function.WriteSuccessMsg(Resources.L.添加成功 + "!", "PointGroup.aspx");
                }
                else
                {
                    function.WriteErrMsg(Resources.L.添加失败 + "!");
                }
            }
        }
    }
}