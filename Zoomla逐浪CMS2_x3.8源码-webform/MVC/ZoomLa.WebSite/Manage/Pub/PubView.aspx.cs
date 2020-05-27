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
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Pub
{
    public partial class PubView : CustomerPageAction
    {
        private B_User buser = new B_User();
        private B_ModelField bfield = new B_ModelField();
        private B_Model bmodel = new B_Model();
        private B_Pub pubBll = new B_Pub();
        private M_Pub pubMod = new M_Pub();
        B_Admin admin = new B_Admin();
        private B_Pub bpub = new B_Pub();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            if (!this.Page.IsPostBack)
            {
                string Pubid = string.IsNullOrEmpty(Request.QueryString["Pubid"].ToString()) ? "0" : Request.QueryString["Pubid"].ToString();
                pubMod = pubBll.SelReturnModel(DataConverter.CLng(Pubid));
                string prowinfo = B_Role.GetPowerInfoByIDs(badmin.GetAdminLogin().RoleList);
                if (!badmin.GetAdminLogin().RoleList.Contains(",1,") && !prowinfo.Contains("," + pubMod.PubTableName + ","))
                {
                    function.WriteErrMsg("无权限管理该互动模型!!");
                }
                string ModelID = bpub.GetSelect(DataConverter.CLng(Pubid)).PubModelID.ToString();
                //  int ModelID = string.IsNullOrEmpty(Request.QueryString["ModelID"]) ? 0 : DataConverter.CLng(Request.QueryString["ModelID"]);
                this.HiddenPubid.Value = Pubid;
                if (DataConverter.CLng(ModelID) <= 0)
                    function.WriteErrMsg("缺少用户模型ID参数！");
                //jc:查找相应模版实体
                //    M_ModelInfo model = bmodel.GetModelById(ModelID);
                string small = string.IsNullOrEmpty(Request.QueryString["small"]) ? "0" : Request.QueryString["small"].ToString();
                this.HiddenSmall.Value = small;
                this.HdnModelID.Value = ModelID;
                M_ModelInfo model = bmodel.GetModelById(DataConverter.CLng(ModelID));
                int ID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : DataConverter.CLng(Request.QueryString["ID"]);
                this.HiddenID.Value = ID.ToString();
                ViewState["ID"] = ID.ToString();
                if (ID < 0)
                    function.WriteErrMsg("缺少ID参数！");


                DataTable UserData = new DataTable();
                DataTable aas = ShowGrid();
                int zong = aas.Rows.Count;


                UserData = buser.GetUserModeInfo(model.TableName, ID, 18);

                this.ptit.Text = UserData.Rows[0]["PubTitle"].ToString();

                DetailsView1.DataSource = UserData.DefaultView;
                DetailsView1.DataBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='pubmanage.aspx'>互动管理</a></li> <li><a href='Pubsinfo.aspx?Pubid=" + Pubid + "&type=0'>互动信息管理</a></li><li>互动信息</li>");
            }
        }


        private DataTable ShowGrid()
        {
            DataTable dt = this.bfield.GetModelFieldList(DataConverter.CLng(this.HdnModelID.Value));
            DetailsView1.AutoGenerateRows = false;
            DataControlFieldCollection dcfc = DetailsView1.Fields;


            dcfc.Clear();


            BoundField bf2 = new BoundField();
            bf2.HeaderText = "ID";
            bf2.DataField = "ID";
            bf2.HeaderStyle.Width = Unit.Percentage(15);
            bf2.HeaderStyle.CssClass = "tdbgleft";
            bf2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf2.ItemStyle.HorizontalAlign = HorizontalAlign.Left;

            dcfc.Add(bf2);

            BoundField bf5 = new BoundField();
            bf5.HeaderText = "用户名";
            bf5.DataField = "PubUserName";
            bf5.HeaderStyle.CssClass = "tdbgleft";
            bf5.HeaderStyle.Width = Unit.Percentage(15);
            bf5.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf5.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            dcfc.Add(bf5);

            BoundField bf1 = new BoundField();
            bf1.HeaderText = "标题";
            bf1.DataField = "PubTitle";
            bf1.HeaderStyle.CssClass = "tdbgleft";
            bf1.HeaderStyle.Width = Unit.Percentage(15);
            bf1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf1.HtmlEncode = false;
            dcfc.Add(bf1);

            BoundField bfa = new BoundField();
            bfa.HeaderText = "内容";
            bfa.DataField = "PubContent";
            bfa.HeaderStyle.CssClass = "tdbgleft";
            bfa.HeaderStyle.Width = Unit.Percentage(15);
            bfa.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bfa.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bfa.HtmlEncode = false;
            dcfc.Add(bfa);

            BoundField bf3 = new BoundField();
            bf3.HeaderText = "IP地址";
            bf3.DataField = "PubIP";
            bf3.HeaderStyle.CssClass = "tdbgleft";
            bf3.HeaderStyle.Width = Unit.Percentage(15);
            bf3.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf3.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf3.HtmlEncode = false;
            dcfc.Add(bf3);

            BoundField bf4 = new BoundField();
            bf4.HeaderText = "添加时间";
            bf4.DataField = "PubAddTime";
            bf4.HeaderStyle.CssClass = "tdbgleft";
            bf4.HeaderStyle.Width = Unit.Percentage(15);
            bf4.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bf4.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            bf4.HtmlEncode = false;
            dcfc.Add(bf4);


            foreach (DataRow dr in dt.Rows)
            {


                BoundField bf = new BoundField();
                bf.HeaderText = dr["FieldAlias"].ToString();
                bf.DataField = dr["FieldName"].ToString();
                bf.HeaderStyle.Width = Unit.Percentage(15);
                bf.HeaderStyle.CssClass = "tdbgleft";
                bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                bf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                bf.HtmlEncode = false;
                dcfc.Add(bf);

            }
            return dt;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.HiddenSmall.Value != "0")
                Response.Redirect("ViewSmallPub.aspx?pubid=" + this.HiddenPubid.Value + "&ID=" + this.HiddenSmall.Value);
            else
                Response.Redirect("Pubsinfo.aspx?pubid=" + this.HiddenPubid.Value + "&type=0");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPub.aspx?Pubid=" + this.HiddenPubid.Value + "&Parentid=" + ViewState["ID"].ToString());
        }
    }
}