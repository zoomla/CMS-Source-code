using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Text;

public partial class manage_User_InputUser : CustomerPageAction
{
    protected B_User buser = new B_User();
    protected B_Group gll = new B_Group();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
        }
        Label1.Text = "导入用户";
        Call.SetBreadCrumb(Master,"<li>后台管理</li><li>用户管理</li><li>会员管理</li><li><a href='InputUser.aspx'>导入用户</a></li>");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        Stream srdPreview = FileUpload1.FileContent;
        string filecontent = StreamtoString(srdPreview);
        string[] linetxt = filecontent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);


        for (int i = 1; i < linetxt.Length; i++)
        {
            string userinfo = linetxt[i];

            string[] userarr = linetxt[i].Split(new string[] { "\t" }, StringSplitOptions.None);
            for (int ii = 0; ii < userarr.Length; ii++)
            {

                if (buser.GetUserIDByUserName(userarr[0].ToString()).UserID == 0)
                {
                    M_UserInfo userinfos = new M_UserInfo();
                    userinfos.GroupID = gll.DefaultGroupID();
                    userinfos.UserName = userarr[0].ToString();
                    userinfos.Email = userarr[1].ToString();
                    //userinfos.UserExp = DataConverter.CLng(userarr[2].ToString());
                    if (userpwd.Text == "")
                    {
                        userinfos.UserPwd = StringHelper.MD5("admin888");
                      
                    }
                    else
                    {
                        userinfos.UserPwd = StringHelper.MD5(userpwd.Text);
                    }
                    userinfos.TrueName = userarr[3].ToString();
                    int userid = buser.AddModel(userinfos);
                    M_Uinfo uinfos = new M_Uinfo();
                    uinfos.UserId = DataConverter.CLng(userid);
                   
                    uinfos.BirthDay = userarr[4].ToString();
                    uinfos.Address = userarr[5].ToString();
                    uinfos.OfficePhone = userarr[6].ToString();
                    uinfos.HomePhone = userarr[7].ToString();
                    uinfos.Fax = userarr[8].ToString();
                    uinfos.ZipCode = userarr[9].ToString();
                    uinfos.Mobile = userarr[10].ToString();
                    uinfos.UserSex = DataConverter.CBool(userarr[11].ToString());
                    uinfos.QQ = userarr[12].ToString();
                    uinfos.MSN = userarr[13].ToString();
                    uinfos.IDCard = userarr[14].ToString();
                    uinfos.HomePage = userarr[15].ToString();
                    uinfos.Province = userarr[16].ToString();
                    uinfos.County = userarr[17].ToString();
                    uinfos.WorkProvince = userarr[18].ToString();
                    uinfos.WorkCounty = userarr[19].ToString();
                    buser.AddBase(uinfos);
                }
            }
        }
        Label1.Text = "<font color=green>导入成功!</font>";
    }

    protected string StreamtoString(Stream filestream)
    {
        StreamReader srdPreview = new StreamReader(filestream, System.Text.Encoding.Default, false);
        String temp = string.Empty;
        while (srdPreview.Peek() > -1)
        {
            String input = srdPreview.ReadLine();
            temp += input + "\n";
        }
        srdPreview.Close();
        return temp;
    }

    protected void DownFile_L_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string titles = "会员名,Email,会员积分,真实姓名,生日,联系地址,办公电话,家庭电话,传真号码,邮编,手机号,性别,QQ号码,MSN号码,身份证号码,个人主页,籍省,户籍县市,工作所在省,工作所在县市";
        sb.Append(titles.Replace(",", "\t"));
        sb.Append("\n");
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=UserInfo(" + DateTime.Now.ToString() + ").xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }
}