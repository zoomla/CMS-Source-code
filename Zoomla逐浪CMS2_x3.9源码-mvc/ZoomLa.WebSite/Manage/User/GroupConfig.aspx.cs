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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
namespace ZoomLaCMS.Manage.User
{
    public partial class GroupConfig : CustomerPageAction
    {
        private B_Group Gll = new B_Group();
        private B_Model Mll = new B_Model();
        //private int id;

        public int Mid { get { return DataConverter.CLng(Request.QueryString["GroupID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_Group Ginfo = Gll.GetByID(Mid);
                this.GroupName.Text = Ginfo.GroupName;
                this.GroupInfo.Text = Ginfo.Description.ToString();
                DataTable umodelDT = Mll.GetListUser();
                GroupModel.DataSource = umodelDT;
                GroupModel.DataTextField = "ModelName";
                GroupModel.DataValueField = "ModelID";
                GroupModel.DataBind();
                GroupModel.Items.Insert(0, new ListItem("不绑定模型", "0"));
                if (GroupModel.Items.Count > 0)
                {
                    for (int i = 0; i < GroupModel.Items.Count; i++)
                    {
                        if (Gll.IsExistModel(Ginfo.GroupID, DataConverter.CLng(GroupModel.Items[i].Value)))
                            GroupModel.Items[i].Selected = true;
                        else
                            GroupModel.Items[i].Selected = false;
                    }
                }
            }
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href='GroupManage.aspx'>会员组管理</a></li><li>会员组设置</li>");
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string ModeItem = GroupModel.SelectedValue;
            SafeSC.CheckIDSEx(ModeItem);
            Gll.SetGroupModel(Mid, ModeItem);
            function.WriteSuccessMsg("设置成功", "GroupManage.aspx");
        }
    }
}