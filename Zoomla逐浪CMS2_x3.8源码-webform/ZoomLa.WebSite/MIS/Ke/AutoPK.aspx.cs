using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.BLL.Exam;
using ZoomLa.BLL.Room;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.Model.Exam;

public partial class test : System.Web.UI.Page
{
    B_ExTeacher teachBll = new B_ExTeacher();
    B_EDU_School schBll = new B_EDU_School();
    B_ExClassgroup classBll = new B_ExClassgroup();
    B_EDU_AutoPK pkBll = new B_EDU_AutoPK();
    B_User buser = new B_User();
    public int ClassID { get { return DataConverter.CLng(Request.QueryString["ClassID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //1,选择需要排课的教师,并设定好排课条件
        //2,读取所设定的条件,排好课

        /*
         * 1,学校一个学期是多久
         * 答:一般是一般来说有20或21个,或也可自己设定好时间,然后除以周期得出课数,暂时未默认值
         */
        buser.CheckIsLogin();
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        
        SchList_Radio.DataSource = schBll.SelByUid(buser.GetLogin().UserID);
        SchList_Radio.DataBind();
        SchList_Radio.Items.Insert(0,new ListItem("请选择配置"));
        DataTable teachDt = teachBll.Select_All();
        Teacher_RPT.DataSource = teachDt;
        Teacher_RPT.DataBind();
        ClassRPT.DataSource = classBll.Select_All();
        ClassRPT.DataBind();
        DataTable dt = pkBll.SelByClassID(ClassID,buser.GetLogin().UserID);
        foreach (DataRow item in dt.Rows)
        {
            DateList_R.Items.Add(new ListItem(Convert.ToDateTime(item["SDate"]).ToString("yyyy年MM月dd日") + "-" + Convert.ToDateTime(item["EDate"]).ToString("yyyy年MM月dd日"), item["Sdate"] + "," + item["EDate"]));
        }
        if(DateList_R.Items.Count>0)
          DateList_R.Items[0].Selected = true;
        if (ClassID > 0)
        {
            Set_Div.Visible = false;
            Sel_Div.Visible = true;
            string[] dates = DateList_R.SelectedValue.Split(',');
            SetConfig(dates[0], dates[1]);
        }
        TeacList_Dr.DataSource = teachDt;
        TeacList_Dr.DataBind();
    }
    void SetConfig(string sdate, string edate)//根据时期查询排课的内容
    {
        DateTime Sdate = string.IsNullOrEmpty(sdate) ? DateTime.Now : DataConverter.CDate(sdate);
        DateTime Edate = string.IsNullOrEmpty(edate) ? DateTime.Now.AddYears(1) : DataConverter.CDate(edate);
        DataTable pkMod = pkBll.SelByDate(ClassID,buser.GetLogin().UserID, Sdate, Edate);
        Json_Hid.Value = pkMod.Rows.Count <= 0 ? JsonConvert.SerializeObject(schBll.SelMyConfig()) : pkMod.Rows[0]["Config"].ToString();
        if (pkMod.Rows.Count > 0)
        {
            //function.Script(this, "SelRad(" + pkMod.Rows[0]["Ownclass"] + ");");
            function.Script(this, "SelChk('" + pkMod.Rows[0]["TechIDS"] + "');");
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_EDU_AutoPK pkMod = new M_EDU_AutoPK();
        if (ClassID>0)//根据班级id判断是添加还是修改排课内容
        {
            DateTime sdate=Convert.ToDateTime(DateList_R.SelectedValue.Split(',')[0]);
            DateTime edate=Convert.ToDateTime(DateList_R.SelectedValue.Split(',')[1]);
            pkMod = pkBll.SelReturnModel(Convert.ToInt32(pkBll.SelByDate(ClassID, buser.GetLogin().UserID, sdate, edate).Rows[0]["ID"]));
        }else{
            DateTime sdate = string.IsNullOrEmpty(SDate_T.Text) ? DateTime.Now : DataConverter.CDate(SDate_T.Text);
            DateTime edate = string.IsNullOrEmpty(EDate_T.Text) ? DateTime.Now.AddYears(1) : DataConverter.CDate(EDate_T.Text);
            DataTable checkdt = pkBll.SelByDate(Convert.ToInt32(Request.Form["class_rad"]), buser.GetLogin().UserID, sdate, edate);
            if (checkdt.Rows.Count > 0)
                function.WriteErrMsg("该班级已包含所选时期的课程表,请重新选择时期");
            pkMod.SDate = DataConverter.CDate(sdate);
            pkMod.EDate = DataConverter.CDate(edate);
        }
        pkMod.Config = Json_Hid.Value;
        pkMod.UserID = buser.GetLogin().UserID;
        pkMod.CDate = DateTime.Now;
        pkMod.Ownclass = ClassID>0?ClassID:DataConverter.CLng(Request.Form["class_rad"]);
        pkMod.TechIDS = Request.Form["idchk"];
        if (ClassID > 0)
            pkBll.UpdateByID(pkMod);
        else
            pkBll.Insert(pkMod);
        function.WriteSuccessMsg("保存成功");
    }
    protected void SeachDate_B_Click(object sender, EventArgs e)
    {
        string[] dates = DateList_R.SelectedValue.Split(',');
        SetConfig(dates[0], dates[1]);
    }
    protected void SchList_Radio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Json_Hid.Value = JsonConvert.SerializeObject(schBll.SelReturnModel(Convert.ToInt32(SchList_Radio.SelectedValue)));
    }
    protected void SaveD_T_Click(object sender, EventArgs e)
    {
        DateTime sdate = Convert.ToDateTime(DateList_R.SelectedValue.Split(',')[0]);
        DateTime edate = Convert.ToDateTime(DateList_R.SelectedValue.Split(',')[1]);
        M_EDU_AutoPK pkMod = pkBll.SelReturnModel(Convert.ToInt32(pkBll.SelByDate(ClassID, buser.GetLogin().UserID, sdate, edate).Rows[0]["ID"]));
        pkMod.Config = Json_Hid.Value;
        pkBll.UpdateByID(pkMod);
        pkBll.InsertTeach(Teach_Hid.Value, TeacList_Dr.SelectedItem.Text, pkMod.Ownclass, Time_Hid.Value, DataConverter.CDate(Date_T.Text));
    }
}