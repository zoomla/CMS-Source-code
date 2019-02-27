using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;

public partial class User_Exam_ClassView : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_ClassRoom ClassBll = new B_ClassRoom();
    B_Group groupBll = new B_Group();
    B_Student stuBll = new B_Student();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind(string skey="")
    {
        DataTable dt = ClassBll.Select_U_All(buser.GetLogin().UserID, skey);
        RPT.DataSource = dt;
        RPT.DataBind();
        emptydata_div.Visible = dt.Rows.Count <= 0;
    }
    //根据学生申请状态显示对应按钮
    public string GetButton()
    {
        if (DataConverter.CLng(Eval("StuCount"))>0)
        {
            return "<button type='button' class='btn btn-primary' disabled='disabled'>已申请该班级</button>";
        }
        string tearcher = string.IsNullOrEmpty(Eval("UserName").ToString()) ? "管理员" : Eval("UserName").ToString();
        return "<button class='btn btn-primary' data-tearch='"+tearcher + "' data-info='"+Eval("ClassInfo")+"' type='button' data-id='" + Eval("RoomID") + "' onclick='ShowRemark(this)'>申请加入班级</button>";
    }
   

    protected void Audit_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_Group groupMod = groupBll.GetByID(mu.GroupID);
        int roomid= Convert.ToInt32(RoomID_Hid.Value);
        DataTable dt = ClassBll.SelByUid(mu.UserID, -1, roomid);
        if (dt.Rows.Count > 0) { function.WriteErrMsg("你已经申请过班级了!"); }
        M_Student stuMod = new M_Student();
        stuMod.Addtime = DateTime.Now;
        stuMod.UserID = mu.UserID;
        stuMod.UserName = mu.UserName;
        stuMod.StudentType = GetGroup(groupMod.Enroll);
        stuMod.AuditingContext = Remind_T.Text;
        stuMod.RoomID = roomid;
        stuBll.insert(stuMod);
        function.WriteSuccessMsg("申请班级成功!", "/User/Exam/ClassManage.aspx");
    }
    //返回用户角色(1:学生、2:教师、3:家长)
    public int GetGroup(string enroll)
    {
        enroll = enroll ?? "";
        if (enroll.Contains("isteach")) { return 2; }
        if (enroll.Contains("isfamily")) { return 3; }
        return 1;
    }

    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        MyBind(Search_T.Text.Trim());
    }
}