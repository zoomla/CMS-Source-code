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

public partial class User_Info_AddUserAddress : System.Web.UI.Page
{
    B_UserRecei receBll = new B_UserRecei();
    B_User buser = new B_User();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindDP();
            if (Mid > 0)
            {
                M_UserInfo mu = buser.GetLogin();
                M_UserRecei model = receBll.GetSelect(Mid, mu.UserID);
                if (model == null) function.WriteErrMsg("修改的地址不存在");
                ZipCode_T.Text = model.Zipcode;
                //省市县
                Street_T.Text = model.Street;
                ReceName_T.Text = model.ReceivName;
                MobileNum_T.Text = model.MobileNum;
                pro_hid.Value = model.Provinces;
                Def_chk.Checked = model.isDefault == 1 ? true : false;
                if (model.phone.Split('-').Length > 1)
                {
                    Area_T.Text = model.phone.Split('-')[0];
                    Phone_T.Text = model.phone.Split('-')[1];
                }
            }
        }
    }
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_UserRecei model = new M_UserRecei();
        bool isupdate = false;
        if (Mid > 0)
        {
            model = receBll.GetSelect(Mid, mu.UserID);
            isupdate = true;
        }
        model.UserID = mu.UserID;
        model.Email = mu.Email;
        //model.Provinces = Request.Form["province_dp"] + "|" + Request.Form["city_dp"] + "|" + Request.Form["county_dp"];
        model.CityCode = Request.Form["province_dp"] + "|" + Request.Form["city_dp"] + "|" + Request.Form["county_dp"];
        //model.CityCode = province_dp.SelectedValue + " " + city_dp.ValueSelectedValue + " " + county_dp.SelectedValue;
        model.Provinces = pro_hid.Value;
        model.Street = Street_T.Text;
        model.Zipcode = ZipCode_T.Text;
        model.ReceivName = ReceName_T.Text;
        model.MobileNum = MobileNum_T.Text;
        model.phone = Area_T.Text + "-" + Phone_T.Text;
        model.isDefault = Def_chk.Checked ? 1 : 0;
        if (isupdate)
        {receBll.GetUpdate(model); }
        else
        { model.ID = receBll.GetInsert(model); }
        if (Def_chk.Checked) { receBll.SetDef(model.ID); }
        function.WriteSuccessMsg("保存地址成功!","UserRecei.aspx");
    }
}