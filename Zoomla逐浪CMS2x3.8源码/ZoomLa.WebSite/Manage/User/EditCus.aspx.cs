using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Text;
using System.Data;

public partial class manage_User_EditCus : CustomerPageAction
{
    
    protected B_User Uinfo = new B_User();

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    //确定按钮
    protected void btnTrue_Click(object sender, EventArgs e)
    {
        int id = DataConverter.CLng(Request.QueryString["id"]);
        M_UserInfo userinfo = Uinfo.GetUserByUserID(id);
        M_Uinfo users = Uinfo.GetUserBaseByuserid(id);
        B_Client_Basic bsc = new B_Client_Basic();
        M_Client_Basic client = new M_Client_Basic();

        if (rbSelect.SelectedValue.Trim().Equals("3"))  //取消升级
        {
            Response.Redirect("Userinfo.aspx?id=" + id);
        }
        if (rbSelect.SelectedValue.Trim().Equals("2")) //覆盖客户
        {
            //通过客户姓名获取客户信息
            DataTable dt = bsc.SelByName(userinfo.UserName.Trim());
            if (dt != null) //如果找不到客户则升级
            {
                client.Flow = DataConverter.CLng(dt.Rows[0]["Flow"]);
                //client.Client_Source = "在线注册";
                //client.Client_Area = users.Province;
                client.P_name = userinfo.UserName;
                client.Add_Date = DateTime.Now;
                client.Code = dt.Rows[0]["Code"].ToString();
                //client.Add_Name = users.TrueName;
                client.Title = "在线注册";
                M_Client_Penson person = new M_Client_Penson();
                B_Client_Penson bperson = new B_Client_Penson();
                person = bperson.GetPenSonByCode(client.Code.Trim());
                if (person == null || person.Flow==0)
                {
                    person.Code = client.Code;
                }
                person.Birthday = DataConverter.CDate(users.BirthDay);
                person.city = users.County;
                person.country = users.Country;
                person.Fax_phone = users.Fax;
                person.Home_Phone = users.HomePhone;
                person.Homepage = users.HomePage;
                person.ICQ_Code = users.ICQ;
                person.Id_Code = users.IDCard;
                person.MSN_Code = users.MSN;
                person.Native = users.Province;
                person.province = users.County;
                person.QQ_Code = users.QQ;
                person.Telephone = users.Mobile;
                person.UC_Code = users.UC;
                person.Work_Phone = users.OfficePhone;
                person.YaoHu_Code = users.Yahoo;
                person.ZipCodeW = users.ZipCode;
                person.Code = client.Code.Trim();
                if (person.Flow == 0)
                {
                    if (bsc.GetUpdate(client) && bperson.GetInsert(person))
                    {
                        function.WriteSuccessMsg("覆盖升级成功，请进入客户管理系统管理客户信息！", "../user/Userinfo.aspx?id="+id);
                    }
                    else
                    {
                        function.WriteErrMsg("操作失败,请重试或检查操作错误!");
                    }
                }
                else
                {
                    if (bsc.GetUpdate(client) && bperson.GetUpdate(person))
                    {
                        function.WriteSuccessMsg("覆盖升级成功，请进入客户管理系统管理客户信息！", "../user/Userinfo.aspx?id=" + id);
                    }
                    else
                    {
                        function.WriteErrMsg("操作失败,请重试或检查操作错误!");
                    }
                }
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        if (rbSelect.SelectedValue.Trim().Equals("1"))//系统修改姓名,随机生成数字
        {
            //client.Client_Source = "在线注册";
            //client.Client_Area = users.Province;
            Random ro = new Random();
            int resu = ro.Next(100000);
            client.P_name = userinfo.UserName + resu.ToString();
            client.Add_Date = DateTime.Now;
            client.Code = DataSecurity.MakeFileRndName().ToString();
            //client.Add_Name = users.TrueName;
            client.Title = "在线注册";
            M_Client_Penson person = new M_Client_Penson();
            B_Client_Penson bperson = new B_Client_Penson();
            person.Code = client.Code.Trim();
            person.Birthday = DataConverter.CDate(users.BirthDay);
            person.city = users.County;
            person.country = users.Country;
            person.Fax_phone = users.Fax;
            person.Home_Phone = users.HomePhone;
            person.Homepage = users.HomePage;
            person.ICQ_Code = users.ICQ;
            person.Id_Code = users.IDCard;
            person.MSN_Code = users.MSN;
            person.Native = users.Province;
            person.province = users.County;
            person.QQ_Code = users.QQ;
            person.Telephone = users.Mobile;
            person.UC_Code = users.UC;
            person.Work_Phone = users.OfficePhone;
            person.YaoHu_Code = users.Yahoo;
            person.ZipCodeW = users.ZipCode;
            if (bsc.GetInsert(client) && bperson.GetInsert(person))
            {
                function.WriteSuccessMsg("升级成功,您的名称为:"+ client.P_name+"，请进入客户管理系统查看客户信息！", "../user/Userinfo.aspx?id=" + id);
            }
            else
            {
                function.WriteErrMsg("操作失败,请重试或检查操作错误!");
            }
        }
    }
    protected void btnCan_Click(object sender, EventArgs e)
    {
        int id = DataConverter.CLng(Request.QueryString["id"]);
        Response.Redirect("Userinfo.aspx?id=" + id);
    }
}
