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
using FreeHome.common;
using System.Collections.Generic;
using FHModel;
using FHBLL;
using ZoomLa.Sns.BLL;
namespace FreeHome.FriendSearch
{
    public partial class WebUserControlSearchLeft : System.Web.UI.UserControl
    {
        #region 业务实体
        UserTableBLL utbll = new UserTableBLL();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GetPage();
            }
        }

        private string Xmlurl
        {
            get
            {
                return Server.MapPath(@"~/common/UserRegXml.xml");
            }
            set
            {
                Xmlurl = value;
            }
        }
        private void GetPage()
        {
            //绑定省
            //List<province> list2 = ct.readProvince(Server.MapPath(@"~/common/SystemData.xml"));
            //DropDownList3.DataSource = list2;
            //DropDownList3.DataTextField = "name";
            //DropDownList3.DataValueField = "code";
            //DropDownList3.DataBind();
            //ListItem li3 = new ListItem();
            //li3.Value = "";
            //li3.Text = "不限";
            //li3.Selected = true;
            //DropDownList3.Items.Add(li3);
        }
        //绑定城市
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pro = DropDownList3.SelectedValue;
            if (pro != "")
            {
                this.DropDownList4.Visible = true;
                //List<Pcity> listc = ct.ReadCity(Server.MapPath(@"~/common/SystemData.xml"), pro);
                //DropDownList4.DataSource = listc;
                //DropDownList4.DataTextField = "name";
                //DropDownList4.DataValueField = "code";
                //DropDownList4.DataBind();
            }
        }

        protected void quickbtn_Click(object sender, EventArgs e)
        {
            int age1 = this.TextBox1.Text == "" ? 0 : int.Parse(this.TextBox1.Text);
            int age2 = this.TextBox2.Text == "" ? 0 : int.Parse(this.TextBox2.Text);
            string sextext = this.RadioButtonList1.Text;
        }
    }
}