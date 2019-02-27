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

public partial class manage_UserShopManage_AddShopGrades : CustomerPageAction
{
    B_ShopGrade bshopgrade = new B_ShopGrade();
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = "添加等级";
        if (!this.Page.IsPostBack)
        {
            B_Admin badmin = new B_Admin();
            
            if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreProductManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!B_ARoleAuth.Check(ZLEnum.Auth.model, "ModelEdit"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            //foreach (DataRow row in FileSystemObject.GetDirectoryAllInfos(HttpContext.Current.Server.MapPath("/Images/levelIcon/"), FsoMethod.File).Rows)
            //{
            //    this.DrpGradeimg.Items.Add(new ListItem(row["name"].ToString(), row["name"].ToString()));
            //}
            this.DrpGradeimg.Attributes.Add("onchange", "ChangeImgItemIcon(this.value);ChangeTxtItemIcon(this.value);");
            this.TxtGradeimg.Attributes.Add("onchange", "ChangeImgItemIcon(this.value);");
            string id = base.Request.QueryString["ID"];
            if (!string.IsNullOrEmpty(id))
            {

                this.HdnModelId.Value = id;
                str = "修改等级";
                this.LTitle.Text = "修改等级";
                M_ShopGrade info = this.bshopgrade.GetShopGradebyid(int.Parse(id));
                this.GradeName.Text = info.GradeName.ToString();
                string selectValue = string.IsNullOrEmpty(info.Gradeimg) ? "m_1.gif" : info.Gradeimg;
                this.ImgGradeimg.ImageUrl = "/Images/levelIcon/" + selectValue;
                this.TxtGradeimg.Text = info.Gradeimg.ToString();
                this.TxtCommentNum.Text = info.CommentNum.ToString();
                this.TxtOtherName.Text = info.OtherName.ToString();
                this.IsTrue.Text = info.Istrue.ToString();
                this.GradeType.Text = info.GradeType.ToString();
                this.DrpGradeimg.SelectedValue = selectValue;
                this.Imgnum.Text = info.Imgnum.ToString();
            }
            else
            {
                this.HdnModelId.Value = "0";
            }
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a><li><a href='ShopGrade.aspx'>店铺等级管理</a></li><li class='active'>" + str + "</li>");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_ShopGrade mshopgrade = new M_ShopGrade();
        if (this.HdnModelId.Value == "0")
        {
            mshopgrade.ID = 0;
            mshopgrade.GradeName = this.GradeName.Text;
            mshopgrade.Gradeimg = this.TxtGradeimg.Text;
            mshopgrade.CommentNum = DataConverter.CLng(TxtCommentNum.Text);
            mshopgrade.OtherName = this.TxtOtherName.Text;
            mshopgrade.Istrue = DataConverter.CBool(this.IsTrue.Text);
            mshopgrade.GradeType = DataConverter.CLng(Request.Form["GradeType"]);
            mshopgrade.Imgnum = DataConverter.CLng(this.Imgnum.Text);
            bshopgrade.AddShopGrade(mshopgrade);
            Response.Write("<script language=javascript>alert('添加成功!请继续添加!');location.href='AddShopGrades.aspx';</script>");
        }
        else 
        {
            mshopgrade.ID =DataConverter.CLng(this.HdnModelId.Value);
            mshopgrade.GradeName = this.GradeName.Text;
            mshopgrade.Gradeimg = this.TxtGradeimg.Text;
            mshopgrade.CommentNum = DataConverter.CLng(TxtCommentNum.Text);
            mshopgrade.OtherName = this.TxtOtherName.Text;
            mshopgrade.Istrue = DataConverter.CBool(this.IsTrue.Text);
            mshopgrade.GradeType = DataConverter.CLng(Request.Form["GradeType"]);
            mshopgrade.Imgnum = DataConverter.CLng(this.Imgnum.Text);
            bshopgrade.UpdateShopGrade(mshopgrade);
            Response.Write("<script language=javascript>alert('修改成功!');location.href='ShopGrade.aspx';</script>");
        
        }
    }
}
