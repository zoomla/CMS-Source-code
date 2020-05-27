using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.MIS;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.MIS.OA.Tlp
{
    public partial class defTlp : ZoomLa.BLL.MIS.B_OAFormUI
    {
        B_OA_Document oaBll = new B_OA_Document();
        B_Content conBll = new B_Content();
        B_ModelField fieldBll = new B_ModelField();
        B_OA_ShowField showBll = new B_OA_ShowField();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["appID"]); } }
        private int ProID { get { return DataConverter.CLng(Request.QueryString["ProID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = fieldBll.GetModelFieldListall(ModelID);
                if (Mid > 0)
                {
                    M_OA_Document oaMod = oaBll.SelReturnModel(Mid);
                    DataTable dtContent = conBll.GetContent(Convert.ToInt32(oaMod.Content));
                    Html_Lit.Text = fieldBll.InputallHtml(ModelID, 9999, new ModelConfig()
                    {
                        ValueDT = dtContent
                    });
                }
                else
                {
                    Html_Lit.Text = showBll.ShowStyleField(dt, 9999);
                }
            }
        }
        public override DataTable CreateTable(string[] fields)
        {
            Call commonCall = new Call();
            DataTable dt = fieldBll.SelByModelID(ModelID);
            DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState);
            return table;
        }
    }
}