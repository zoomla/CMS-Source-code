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
using System.Text;

namespace ZoomLaCMS.Manage.UserShopMannger
{
    public partial class Addsearchkey : CustomerPageAction
    {
        B_Node nll = new B_Node();
        //B_Shopsearch sll = new B_Shopsearch();
        protected int cdd = 0;
        protected int ddd = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();

            if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreinfoManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["menu"] == "edit")
                {
                    Label1.Text = "修改关键字";
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    //M_Shopsearch llinfo = sll.GetShopsearchbyid(id);
                    //this.sid.Value = llinfo.ID.ToString();
                    //this.Searchkey.Text = llinfo.Searchkey.ToString();
                    //this.SearchNum.Text = llinfo.SearchNum.ToString();
                    //this.SearchTime.Text = llinfo.SearchTime.ToString();
                    //this.Showtop.Checked = llinfo.Showtop;
                    //if (llinfo.Commend == 1)
                    //{
                    //    this.Commend.Checked = true;
                    //}
                    //else
                    //{
                    //    this.Commend.Checked = false;
                    //}
                    //Getnodelist(0, llinfo.Class.ToString());
                }
                else
                {
                    this.SearchNum.Text = "0";
                    this.SearchTime.Text = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
                    if (!IsPostBack)
                    {
                        Getnodelist(0, "");
                    }
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a></li><li><a href='ShopSearchKey.aspx'>店铺关键字管理</a><li class='active'>添加关键字</li>");
        }
        private void Getnodelist(int Parentid, string classlist)
        {
            DataTable cc = nll.GetNodeChildList(Parentid);
            char ddc = char.Parse("　");
            string spancode = new string(ddc, cdd);
            cdd = cdd + 1;

            for (int i = 0; i < cc.Rows.Count; i++)
            {
                string nodelistname = cc.Rows[i]["NodeName"].ToString();
                this.Class.Items.Add(new ListItem(spancode + "├" + cc.Rows[i]["NodeName"].ToString(), cc.Rows[i]["NodeID"].ToString()));

                string cd1 = "," + classlist + ",";
                string cd2 = "," + cc.Rows[i]["NodeID"].ToString() + ",";

                if (cd1.IndexOf(cd2) > -1)
                {
                    this.Class.Items[ddd].Selected = true;
                }
                ddd = ddd + 1;
                Getnodelist(DataConverter.CLng(cc.Rows[i]["NodeID"].ToString()), classlist);
            }
            cdd = cdd - 1;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //M_Shopsearch smode = new M_Shopsearch();
            //if (this.sid.Value == "")
            //{
            //    smode.ID = 0;
            //    smode.Searchkey = this.Searchkey.Text.ToString();
            //    smode.SearchNum = DataConverter.CLng(this.SearchNum.Text.ToString());
            //    smode.SearchTime = DataConverter.CDate(this.SearchTime.Text.ToString());
            //    if (Showtop.Checked)
            //    {
            //        smode.Showtop = true;
            //    }
            //    else
            //    {
            //        smode.Showtop = false;
            //    }
            //    if (Showtop.Checked)
            //    {
            //        smode.Commend = 1;
            //    }
            //    else
            //    {
            //        smode.Commend = 0;
            //    }
            //    string scalas = Class.SelectedValue;
            //    if (string.IsNullOrEmpty(scalas))
            //    {
            //        scalas = "";
            //    }
            //    smode.Class = scalas;
            //    sll.Addshopsearch(smode);
            //    Response.Write("<script language=javascript>alert('添加成功!请继续添加!');location.href='ShopSearchKey.aspx';</script>");
            //}
            //else
            //{
            //    smode.ID = DataConverter.CLng(this.sid.Value);
            //    smode.Searchkey = this.Searchkey.Text.ToString();
            //    smode.SearchNum = DataConverter.CLng(this.SearchNum.Text.ToString());
            //    smode.SearchTime = DataConverter.CDate(this.SearchTime.Text.ToString());
            //    if (Showtop.Checked)
            //    {
            //        smode.Showtop = true;
            //    }
            //    else
            //    {
            //        smode.Showtop = false;
            //    }
            //    if (Commend.Checked)
            //    {
            //        smode.Commend = 1;
            //    }
            //    else
            //    {
            //        smode.Commend = 0;
            //    }

            //    string scalas = Class.SelectedValue;

            //    if (string.IsNullOrEmpty(scalas))
            //    {
            //        scalas = "";
            //    }
            //    smode.Class = scalas;
            //    sll.UpdateShopsearch(smode);
            //    Response.Write("<script language=javascript>alert('修改成功!');location.href='ShopSearchKey.aspx';</script>");
            //}
        }
    }
}