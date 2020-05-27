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

namespace ZoomLaCMS.MIS.OA.Doc
{
    public partial class FiledList : System.Web.UI.Page
    {
        M_OA_Document oaMod = new M_OA_Document();
        B_OA_Document oaBll = new B_OA_Document();
        B_Permission perBll = new B_Permission();
        B_User buser = new B_User();
        //文档ID需要直接与ItemID绑定,与GeneralID绑定用途不大
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!perBll.CheckAuth(buser.GetLogin().UserRole, "oa_pro_file")) { function.WriteErrMsg("你没有访问该页面的权限"); }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            DataTable dt = SelFiledDoc();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        //选取已归档的文件
        private DataTable SelFiledDoc()
        {
            string sql = "SELECT * FROM ZL_OA_Document WHERE [Status]=" + (int)ZLEnum.ConStatus.Filed;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
    }
}