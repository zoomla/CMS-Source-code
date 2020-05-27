using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
namespace ZoomLaCMS.Manage.Page
{
    public partial class SetPageOrder : System.Web.UI.Page
    {
        B_Templata TempBll = new B_Templata();
        public int Pid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li>系统设置</li><li>栏目管理</li>");
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            RPT.DataSource = TempBll.SelByPid(Pid);
            RPT.DataBind();
        }

        public string gettemptype()
        {
            string restr = string.Empty;
            string type = Eval("templateType").ToString();
            switch (type)
            {
                case "1":
                    restr = "文本型栏目";
                    break;
                case "2":
                    restr = "栏目型栏目";
                    break;
                case "3":
                    restr = "Url转发型栏目";
                    break;
                case "4":
                    restr = "功能型栏目";
                    break;
                default:
                    restr = "未知栏目";
                    break;
            }
            return restr;
        }


        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["PageValue"]))
            {
                string[] idsarr = Request.Form["PageValue"].Split(',');
                for (int i = 0; i < idsarr.Length; i++)
                {
                    int orderid = DataConverter.CLng(Request.Form["OrderField" + idsarr[i]]);
                    M_Templata tempmod = TempBll.SelReturnModel(DataConverter.CLng(idsarr[i]));
                    tempmod.OrderID = orderid;
                    TempBll.UpdateByID(tempmod);
                }
            }
            MyBind();
            function.Script(this, "parent.window.location=parent.location;");
        }

        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName)
            {
                case "UpMove":
                    MovePage(id, true);
                    break;
                case "DownMove":
                    MovePage(id, false);
                    break;
                default:
                    break;
            }
        }
        public void MovePage(int id, bool isup)
        {
            string[] SpecValues = Request.Form["PageValue"].Split(',');
            M_Templata tempmod = TempBll.SelReturnModel(id);
            for (int i = 0; i < SpecValues.Length; i++)
            {
                if (SpecValues[i].Equals(id.ToString()))
                {
                    if ((isup && i - 1 < 0) || (!isup && i + 1 >= SpecValues.Length)) { break; }//上移下移判断
                    int index = isup ? i - 1 : i + 1;
                    M_Templata targetmod = TempBll.SelReturnModel(DataConverter.CLng(SpecValues[index]));
                    int nodeorder = DataConverter.CLng(Request.Form["OrderField" + SpecValues[index]]);
                    targetmod.OrderID = tempmod.OrderID;
                    tempmod.OrderID = nodeorder;
                    TempBll.UpdateByID(tempmod);
                    TempBll.UpdateByID(targetmod);
                    break;
                }
            }
            MyBind();
        }
    }
}