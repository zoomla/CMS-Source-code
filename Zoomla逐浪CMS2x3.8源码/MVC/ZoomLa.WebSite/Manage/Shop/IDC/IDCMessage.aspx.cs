using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop.IDC
{
    public partial class IDCMessage : System.Web.UI.Page
    {
        B_OrderList orderBll=new B_OrderList();
        private int Mid {get{return DataConvert.CLng(Request.QueryString["ID"]);} }
        private string ZType {get{return DataConvert.CStr(Request.QueryString["ztype"]);} }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_OrderList orderMod = orderBll.SelReturnModel(Mid);
                switch (ZType)
                {
                    case "message":
                        Info_T.Text = orderMod.Ordermessage;
                        break;
                    case "record":
                        Info_T.Text = orderMod.Internalrecords;
                        break;
                }
                Call.HideBread(Master);
            }
        }

        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_OrderList orderMod = orderBll.SelReturnModel(Mid);
            switch (ZType)
            {
                case "message":
                    orderMod.Ordermessage = Info_T.Text;
                    break;
                case "record":
                    orderMod.Internalrecords = Info_T.Text;
                    break;
            }
            orderBll.UpdateByID(orderMod);
            function.Script(this,"parent.CloseDiag();");
        }
    }
}