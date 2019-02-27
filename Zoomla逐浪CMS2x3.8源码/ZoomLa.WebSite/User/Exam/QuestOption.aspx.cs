using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class User_Exam_QuestOption : System.Web.UI.Page
{
    B_Exam_Sys_Questions examBll = new B_Exam_Sys_Questions();
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    public int QuestID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!buser.CheckLogin() && !badmin.CheckLogin()) { function.WriteErrMsg("您必须登录!"); }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        if (QuestID>0)
        {
            M_Exam_Sys_Questions questMod = examBll.GetSelect(QuestID);
            if (FileSystemObject.IsExist(Server.MapPath(M_Exam_Sys_Questions.OptionDir + QuestID + ".opt"), FsoMethod.File))
            { SaveData_Hid.Value = SafeSC.ReadFileStr(M_Exam_Sys_Questions.OptionDir + QuestID + ".opt"); }
        }
        //替换问题内容格式
        //string[] questarr= content.Split(new string[] { "（）" }, StringSplitOptions.RemoveEmptyEntries);
        //string tempstr = "";
        //int count = 0;//分隔数量
        //for (int i = 0; i < questarr.Length; i++)
        //{
        //     tempstr += questarr[i];
        //    if (i + 1 < questarr.Length)
        //    {
        //        tempstr += "（" + (i + 1) + "）";
        //        count++;
        //    }
        //}
        //QuestCon_L.Text = tempstr;
        //DataCount_Hid.Value = count.ToString();
    }

}