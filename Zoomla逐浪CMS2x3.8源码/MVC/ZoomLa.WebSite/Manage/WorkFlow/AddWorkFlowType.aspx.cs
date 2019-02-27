using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.WorkFlow
{
    public partial class AddWorkFlowType : System.Web.UI.Page
    {
        protected B_MisType typeBll = new B_MisType();
        protected M_MisType typeMod = new M_MisType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    typeMod = typeBll.SelReturnModel(DataConvert.CLng(Request.QueryString["ID"]));
                    TypeName.Text = typeMod.TypeName;
                    TypeDesc.Text = typeMod.TypeDescribe;
                    SavBtn.Text = "修改";
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='FlowTypeList.aspx'>类型管理</a></li><li class='active'>添加步骤</a></li>");
        }
        protected void SavBtn_Click(object sender, EventArgs e)
        {
            typeMod.TypeName = TypeName.Text;
            typeMod.CreateTime = DateTime.Now;
            typeMod.TypeDescribe = TypeDesc.Text;
            if (Request.QueryString["ID"] != null)
            {
                typeMod.ID = DataConvert.CLng(Request.QueryString["ID"]);
                typeBll.UpdateByID(typeMod);
            }
            else
                typeBll.insert(typeMod);
            Response.Redirect("FlowTypeList.aspx");
        }
    }
}