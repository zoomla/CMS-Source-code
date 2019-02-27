using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.Config
{
    public partial class EditDataList : CustomerPageAction
    {
        B_DataList dtlist = new B_DataList();
        M_DataList mlist = new M_DataList();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!this.Page.IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>系统全库概况</a></li><li>修改说明</li>");
                if (!string.IsNullOrEmpty(Request["ID"]))
                {
                    mlist = dtlist.SelReturnModel(Convert.ToInt32(Request["ID"]));
                    this.TxtTableName.Text = mlist.TableName;
                    this.TxtTypeID.Value = mlist.Type.ToString();
                    switch (mlist.Type.ToString())
                    {
                        case "0":
                            this.TxtType.Text = "系统表";
                            break;
                        case "1":
                            this.TxtType.Text = "自定义表";
                            break;
                        case "2":
                            this.TxtType.Text = "临时表";
                            break;
                        default:
                            this.TxtType.Text = "系统表";
                            break;
                    }
                    this.TxtExplain.Text = mlist.Explain;
                }
            }
        }
        protected void EBtnTable_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["ID"]))
            {
                dtlist.UpdateByField("Explain", TxtExplain.Text, Convert.ToInt32(Request["ID"]));
                function.WriteSuccessMsg("修改成功", "DatalistProfile.aspx");
            }
        }
    }
}