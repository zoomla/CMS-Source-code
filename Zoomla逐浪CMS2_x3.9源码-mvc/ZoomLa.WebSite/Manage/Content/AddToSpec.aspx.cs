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
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Content
{
    public partial class AddToSpec : CustomerPageAction
    {
        private B_Spec bspec = new B_Spec();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!this.Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(base.Request.QueryString["InfoIDs"]))
                {
                    function.WriteErrMsg("没有选定要添加到其他专题的内容ID!");
                }
                else
                {
                    //this.TxtGeneralID.Text = base.Request.QueryString["InfoIDs"].Trim();
                }
                //this.LstSpec.DataSource = this.bspec.GetSpecAll();
                //this.LstSpec.DataTextField = "SpecName";
                //this.LstSpec.DataValueField = "SpecID";
                //this.LstSpec.DataBind();
            }
        }

        protected void BtnBacthSet_Click(object sender, EventArgs e)
        {
            string InfoID = this.TxtGeneralID.Text;
            string[] Infos = InfoID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < this.LstSpec.Items.Count; i++)
            {
                if (this.LstSpec.Items[i].Selected)
                {
                    int SpecID = DataConverter.CLng(this.LstSpec.Items[i].Value);
                    for (int t = 0; t < Infos.Length; t++)
                    {
                        //if (!this.bll.IsExist(SpecID, DataConverter.CLng(Infos[t])))
                        //{
                        //    //M_SpecInfo specinfo = new M_SpecInfo();
                        //    //specinfo.SpecInfoID = 0;
                        //    //specinfo.SpecialID = SpecID;
                        //    //specinfo.InfoID = DataConverter.CLng(Infos[t]);
                        //    //this.bll.Add(specinfo);
                        //}
                    }
                }
            }
            Response.Write("<script language=\"javascript\">alert('批量添加操作执行完成!')</script>");
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SpecContent.aspx");
        }
    }
}