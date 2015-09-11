namespace ZoomLa.WebSite.Manage.Content
{
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
    using ZoomLa.Common;
    using ZoomLa.BLL;
    using ZoomLa.Model;

    public partial class MoveToSpec : System.Web.UI.Page
    {
        private B_SpecInfo bll = new B_SpecInfo();
        private B_Spec bspec = new B_Spec();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ContentSpec"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (string.IsNullOrEmpty(base.Request.QueryString["specinfo"]))
                {
                    function.WriteErrMsg("没有选定要移动到其他专题的专题信息ID!");
                }
                else
                {
                    this.TxtGeneralID.Text = base.Request.QueryString["specinfo"].Trim();
                }
                this.LstSpec.DataSource = this.bspec.GetSpecAll();
                this.LstSpec.DataTextField = "SpecName";
                this.LstSpec.DataValueField = "SpecID";
                this.LstSpec.DataBind();
            }
        }

        protected void BtnBacthSet_Click(object sender, EventArgs e)
        {
            string SpecInfoID = this.TxtGeneralID.Text;
            string[] Infos = SpecInfoID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < this.LstSpec.Items.Count; i++)
            {
                if (this.LstSpec.Items[i].Selected)
                {
                    int SpecID = DataConverter.CLng(this.LstSpec.Items[i].Value);
                    for (int t = 0; t < Infos.Length; t++)
                    {
                        //内容ID
                        M_SpecInfo info = this.bll.GetSpecInfo(DataConverter.CLng(Infos[t]));
                        int ItemID = info.InfoID;
                        int sid=info.SpecialID;
                        if(SpecID!=sid)
                        {
                            if (!this.bll.IsExist(SpecID, ItemID))
                            {
                                this.bll.UpdateSpecID(DataConverter.CLng(Infos[t]), SpecID);
                            }
                            else
                            {
                                this.bll.Del(DataConverter.CLng(Infos[t]));
                            }
                        }
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