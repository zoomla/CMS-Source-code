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

public partial class manage_UserShopManage_AddShoplabels : CustomerPageAction
{
    //B_ShopLable slable = new B_ShopLable();

    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreProductManage"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        string strLi = "";
        if (!IsPostBack)
        {
            string menu = Request.QueryString["menu"];
            string getclass = Request.QueryString["LableClass"];
            //DataTable newtable = slable.GetLableClass();
            //this.LableClasslist.DataSource = newtable;
            //this.LableClasslist.DataTextField = "LableClass";
            //this.LableClasslist.DataValueField = "LableClass";
            //this.LableClasslist.DataBind();
            //this.LableClasslist.Items.Insert(0, new ListItem("选择标签", ""));
            //this.LableClasslist.Items.Add(new ListItem(">>输入标签", ""));
            if (!string.IsNullOrEmpty(getclass))
                strLi = "<li><a href='Shoplabelsclass.aspx?lablelname=" + getclass + "'>" + getclass + "</a></li>";
            this.Label2.Text = "<input id=\"Button2\" class='btn btn-primary' name=\"Button2\" type=\"button\" value=\"返回\" OnClick=\"location.href='Shoplabelsclass.aspx?lablelname=" + getclass + "'\">";
            if (Request.QueryString["LableClass"] != null)
            {
                if (getclass == "")
                {
                    this.LableClasslist.SelectedIndex = 0;
                }
                else
                {
                    this.LableClasslist.SelectedValue = getclass;
                }

            }
            else
            {
                this.LableClasslist.SelectedIndex = 0;
            }

            if (menu == "Derive")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                //M_ShopLable info = slable.GetShopLablebyid(id);
                //string ls = GetLableName(info.LableName);

                this.Button2.Visible = true;
                this.Button1.Visible = false;
                //Label1.Text = "派生" + info.LableName.ToString() + "标签";
                this.LableName.Width = 150;

                Derivetr.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;

                this.Label4.Text = "{$";
                this.Label5.Text = "$}";
                this.Lablevalue.Width = 100;
                //if (ls == "")
                //{
                //    this.Lablevalue.Visible = false;
                //}
                //else
                //{
                //    this.Lablevalue.Visible = true;
                //    this.Lablevalue.Text = ls;
                //}

                //this.Derive.Text = info.LableName.ToString();
                //this.LableType.Text = info.LableType.ToString();
                //this.LableInfo.Text = info.LableInfo.ToString();
                //this.LableContent.Text = info.LableContent.ToString();
                //this.IsTrue.Text = info.IsTrue.ToString();
                //this.sid.Value = info.ID.ToString();
                //this.LableClass.Text = info.LableClass.ToString();
                //this.LableClasslist.SelectedValue = info.LableClass.ToString();
                //this.Separator.Text = info.Separator.ToString();
                //this.Initial.Text = info.Initial.ToString();
                //this.Fildsinfo.Text = info.Fildsinfo.ToString();
                //string classname = info.LableClass;
                //if (!string.IsNullOrEmpty(classname))
                //    strLi = "<li><a href='Shoplabelsclass.aspx?lablelname=" + classname + "'>" + classname + "</a></li>";
                //this.Label2.Text = "<input id=\"Button2\" name=\"Button2\" class='btn btn-primary' type=\"button\" value=\"返回\" OnClick=\"location.href='Shoplabelsclass.aspx?lablelname=" + classname + "'\">";
            }
            else
            {
                this.Button2.Visible = false;
                this.Button1.Visible = true;
                this.LableName.Width = 414;
                this.Lablevalue.Visible = false;
                Label4.Visible = false;
                Label5.Visible = false;
                Derivetr.Visible = false;
            }
            if (menu == "edit")
            {
                Label1.Text = "修改标签";
                int id = DataConverter.CLng(Request.QueryString["id"]);
                //M_ShopLable info = slable.GetShopLablebyid(id);
                //if (info.Derive != "")
                //{
                //    this.Derivetr.Visible = true;
                //    this.Derive.Text = info.Derive.ToString();
                //}

                //this.LableName.Text = info.LableName.ToString();
                //this.LableType.Text = info.LableType.ToString();
                //this.LableInfo.Text = info.LableInfo.ToString();
                //this.LableContent.Text = info.LableContent.ToString();
                //this.IsTrue.Text = info.IsTrue.ToString();
                //this.sid.Value = info.ID.ToString();
                //this.LableClass.Text = info.LableClass.ToString();
                //this.LableClasslist.SelectedValue = info.LableClass.ToString();
                //this.Separator.Text = info.Separator.ToString();
                //this.Initial.Text = info.Initial.ToString();
                //this.Fildsinfo.Text = info.Fildsinfo.ToString();
                //string classname = info.LableClass;
                //if(!string.IsNullOrEmpty(classname))
                //    strLi = "<li><a href='Shoplabelsclass.aspx?lablelname=" + classname + "'>" + classname + "</a></li>";
                //this.Label2.Text = "<input id=\"Button2\" name=\"Button2\" type=\"button\" class='btn btn-primary' value=\"返回\" OnClick=\"location.href='Shoplabelsclass.aspx?lablelname=" + classname + "'\">";
                //if (info.LableType == 1)
                //{
                //    Label1.Text = "查看标签[只读]";
                //    //lbcnt.Visible = false;
                //    //Button1.Visible = false;

                //}
            }

            if (this.LableClass.Text == "" && this.LableClasslist.SelectedValue != "")
            {
                this.LableClass.Text = this.LableClasslist.SelectedValue;
            }
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a></li><li><a href='Shoplabelsclass.aspx'>店铺标签管理</a></li>" + strLi + "<li class='active'>编辑标签</li>");
    }
    private static string GetLableName(string ls)
    {
        string labletext = "";
        ls = ls.Replace("{$", "");
        ls = ls.Replace("$}", "");
        if (ls.IndexOf("(") > -1)
        {
            string[] cccs = ls.Split(new string[] { "(" }, StringSplitOptions.None);
            labletext = "("+cccs[1];
        }
        return labletext;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //M_ShopLable lablemodel = new M_ShopLable();
        //if (this.sid.Value == "")
        //{
        //    if (slable.GetLableByname(this.LableName.Text).Rows.Count == 0)
        //    {
        //        lablemodel.ID = 0;
        //        lablemodel.LableName = this.LableName.Text;
        //        lablemodel.LableType = DataConverter.CLng(this.LableType.Text);
        //        lablemodel.LableInfo = this.LableInfo.Text;
        //        lablemodel.IsTrue = DataConverter.CBool(this.IsTrue.Text);
        //        lablemodel.LableContent = this.LableContent.Text;
        //        lablemodel.LableClass = this.LableClass.Text;
        //        lablemodel.Separator = this.Separator.Text;
        //        lablemodel.Initial = DataConverter.CLng(this.Separator.Text);
        //        lablemodel.Derive = this.Derive.Text;
        //        lablemodel.Fildsinfo = Fildsinfo.Text;
        //        slable.AddShopLable(lablemodel);
        //        Response.Write("<script language=javascript>alert('添加成功!请继续添加!');location.href='addshoplabels.aspx?LableClass=" + LableClass.Text.ToString() + "';</script>");
        //    }
        //    else {
        //        Response.Write("<script language=javascript>alert('添加失败!存在相同名的标签!!');</script>");
        //    }
        //}
        //else {
        //    if (slable.GetLableByname(LableName.Text).Rows.Count > 0)
        //    {
        //        if (slable.GetLableByname(LableName.Text).Rows[0]["ID"].ToString() == sid.Value.ToString())
        //        {
        //            lablemodel.ID = DataConverter.CLng(sid.Value.ToString());
        //            lablemodel.LableName = LableName.Text.ToString();
        //            lablemodel.LableType = DataConverter.CLng(LableType.SelectedValue.ToString());
        //            lablemodel.LableInfo = LableInfo.Text.ToString();
        //            lablemodel.IsTrue = DataConverter.CBool(IsTrue.SelectedValue.ToString());
        //            lablemodel.LableContent = LableContent.Text.ToString();
        //            lablemodel.LableClass = LableClass.Text.ToString();
        //            lablemodel.Separator = Separator.Text.ToString();
        //            lablemodel.Initial = DataConverter.CLng(Initial.Text.ToString());
        //            lablemodel.Derive = this.Derive.Text;
        //            lablemodel.Fildsinfo = Fildsinfo.Text;
        //            slable.UpdateShopLable(lablemodel);
        //            Response.Write("<script language=javascript>alert('修改成功!');location.href='Shoplabelsclass.aspx?lablelname=" + LableClass.Text.ToString() + "';</script>");
        //        }
        //        else
        //        {
        //            Response.Write("<script language=javascript>alert('修改失败!存在相同名的标签!!');</script>");
        //        }
        //    }
        //    else {
        //        lablemodel.ID = DataConverter.CLng(sid.Value.ToString());
        //        lablemodel.LableName = LableName.Text.ToString();
        //        lablemodel.LableType = DataConverter.CLng(LableType.SelectedValue.ToString());
        //        lablemodel.LableInfo = LableInfo.Text.ToString();
        //        lablemodel.IsTrue = DataConverter.CBool(IsTrue.SelectedValue.ToString());
        //        lablemodel.LableContent = LableContent.Text.ToString();
        //        lablemodel.LableClass = LableClass.Text.ToString();
        //        lablemodel.Separator = Separator.Text.ToString();
        //        lablemodel.Initial = DataConverter.CLng(Initial.Text.ToString());
        //        lablemodel.Derive = this.Derive.Text;
        //        lablemodel.Fildsinfo = Fildsinfo.Text;
        //        slable.UpdateShopLable(lablemodel);
                Response.Write("<script language=javascript>alert('修改成功!');location.href='Shoplabelsclass.aspx?lablelname=" + LableClass.Text.ToString() + "';</script>");
        //   }
        //}
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //string names = this.Label4.Text + this.LableName.Text + Lablevalue.Text + this.Label5.Text;
        //if (slable.GetLableByname(names).Rows.Count == 0)
        //{
        //    M_ShopLable lablemodel = new M_ShopLable();
        //    lablemodel.ID = 0;
        //    lablemodel.LableName = names;
        //    lablemodel.LableType = 0;
        //    lablemodel.LableInfo = this.LableInfo.Text;
        //    lablemodel.IsTrue = DataConverter.CBool(this.IsTrue.Text);
        //    lablemodel.LableContent = this.LableContent.Text;
        //    lablemodel.LableClass = this.LableClass.Text;
        //    lablemodel.Separator = this.Separator.Text;
        //    lablemodel.Initial = DataConverter.CLng(Initial.Text);
        //    lablemodel.Derive = this.Derive.Text;
        //    slable.AddShopLable(lablemodel);
        //    Response.Write("<script language=javascript>alert('添加派生标签成功!请继续添加!');location.href='Shoplabelsclass.aspx?lablelname=" + this.LableClass.Text + "';</script>");
        //}
        //else
        //{
        //    Response.Write("<script language=javascript>alert('添加失败!存在相同名的标签!!');</script>");
        //}
    }
}
