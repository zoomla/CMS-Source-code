using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Content
{
    public partial class MoveSpec : System.Web.UI.Page
    {
        B_Spec specBll = new B_Spec();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href='" + CustomerPageAction.customPath2 + "Content/SpecialManage.aspx'>专题管理</a></li><li class=\"active\">专题迁移</li>" + Call.GetHelp(17));
        }
        public void MyBind()
        {
            ListItem parentItem = new ListItem();
            parentItem.Text = "根节点";
            parentItem.Value = "0";
            Specs_List.DataSource = specBll.SelAllByListBox();
            Specs_List.DataBind();
            TagetSpecs_List.DataSource = specBll.SelAllByListBox();
            TagetSpecs_List.DataBind();
            TagetSpecs_List.Items.Insert(0, parentItem);
        }

        protected void MoveSpecs_Btn_Click(object sender, EventArgs e)
        {
            string ids = "";
            if (string.IsNullOrEmpty(TagetSpecs_List.SelectedValue)) { function.WriteErrMsg("请选择需要迁移的专题!"); }
            int targetid = DataConverter.CLng(TagetSpecs_List.SelectedValue);
            M_Spec targetMod = specBll.SelReturnModel(targetid);
            foreach (ListItem SpecItem in Specs_List.Items)
            {
                if (SpecItem.Selected)
                {
                    if (targetid > 0 && SpecItem.Value.Equals(targetMod.SpecID.ToString())) { continue; }//判断是否选择了相同级别的专题
                    if (targetid > 0 && CheckExsitParent(DataConverter.CLng(SpecItem.Value), targetMod.Pid))//判断是否将父专题迁入其子专题
                    {
                        continue;
                        //M_Spec parentMod = specBll.SelReturnModel(DataConverter.CLng(SpecItem.Value));
                        //specBll.UpdatePidByIDS(targetMod.SpecID.ToString(), parentMod.Pid);//将目标专题从其父专题中迁移出来
                    }
                    ids += SpecItem.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(ids))
            {
                specBll.UpdatePidByIDS(ids.Trim(','), targetid);
            }
            function.WriteSuccessMsg("迁移成功!");
        }
        public bool CheckExsitParent(int value, int pid)
        {
            if (value == pid) { return true; }
            M_Spec parentMod = specBll.SelReturnModel(pid);
            if (parentMod != null && parentMod.Pid > 0)
            {
                return CheckExsitParent(value, parentMod.Pid);
            }
            return false;
        }
    }
}