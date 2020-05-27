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
using System.Data.SqlClient;
using System.Collections.Generic;
using ZoomLa.Sns;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class _Default : Page 
{
    B_User ubll = new B_User();
    Parking_BLL pbll = new Parking_BLL();
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = ubll.GetLogin().UserID;
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            BindUserInfo();
            if (CheckCar())
            {
                Label1.Text = Getstr(currentUser);
                Bind();

            }
            else
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
            }
        }

    }

    #region 绑定用户信息
    private void BindUserInfo()
    {
        M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
        this.lblPurse.Text = uinfo.DummyPurse.ToString ();
    }
    #endregion

    //绑定日志
    #region 绑定日志
    private void Bind()
    {
        List<ZL_Sns_CarLog> list = pbll.GetUserIDCarLog(currentUser);

        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = list;
        cc.AllowPaging = true;
        cc.PageSize = 4;
        cc.CurrentPageIndex = 0;
        Repeater1.DataSource = cc;
        Repeater1.DataBind();
    }
    #endregion

    //设置车位背景图片
    #region 设置车位背景图片
    private string Getstr(int userid)
    {
        try
        {
            ViewState["userid"] = userid;
            M_UserInfo uinfo = ubll.GetUserByUserID(userid);
            if (pbll.CheckUserPose(userid))
            {
                ZL_Sns_MyPose mp = pbll.GetMyPose(userid);
                string str = "<div style=\"background-image:url(CarImage/bg2.jpg); width:638px; height:200px;Z-INDEX:1\" ></div>";
                str += "<div style=\"background-image:url(CarImage/road.jpg); width:638px; height:120px;Z-INDEX:1\"></div>";
                str += "<SPAN  id=\"NameSpan\" style=\"Z-INDEX: 4; WIDTH: 638px; POSITION: absolute; TOP: 120px; HEIGHT: 100px;\">";
                str += "<div style=\" margin-left:20px; WIDTH: 200px; POSITION: absolute; margin-top:13px; HEIGHT: 36px; z-index:3\"><font color=\"white\"><h3>" + uinfo.UserName + "大街" + "</h3></font></div></SPAN>";
                str += "<SPAN  id=\"StopSpan\" style=\"Z-INDEX: 2; WIDTH: 638px; POSITION: absolute; TOP: 210px; HEIGHT: 300px;\">";
                str += "<div style=\"background-image:url(CarImage/stop.gif); margin-left:40px; WIDTH: 114px; POSITION: absolute; margin-top:13px; HEIGHT: 112px; z-index:3\"></div>";
                str += "<div style=\"background-image:url(CarImage/stop.gif); margin-left:190px; WIDTH: 114px; POSITION: absolute; margin-top:13px; HEIGHT: 112px; z-index:3\"></div>";
                str += "<div style=\"background-image:url(CarImage/stop.gif); margin-left:330px; WIDTH: 114px; POSITION: absolute; margin-top:13px; HEIGHT: 112px; z-index:3\"></div>";
                str += "<div style=\"background-image:url(CarImage/stop.gif); margin-left:470px; WIDTH: 114px; POSITION: absolute; margin-top:13px; HEIGHT: 112px; z-index:3\"></div>";
                if (mp.P_pose_uid_1 > 0 && mp.P_pose_Pmid_1 > 0 && mp.P_pose_Pid_1 > 0)
                {
                    str += GetCatImage(mp.P_pose_user_1, mp.P_pose_Pid_1, 40, mp.P_pose_uid_1, 1);
                }
                else
                {
                    str += SetButton(40, 1);
                }
                if (mp.P_pose_uid_2 > 0 && mp.P_pose_Pmid_2 > 0 && mp.P_pose_Pid_2 > 0)
                {
                    str += GetCatImage(mp.P_pose_user_2, mp.P_pose_Pid_2, 190, mp.P_pose_uid_2, 2);
                }
                else
                {
                    str += SetButton(190, 2);
                }
                if (mp.P_pose_uid_3 > 0 && mp.P_pose_Pmid_3 > 0 && mp.P_pose_Pid_3 > 0)
                {
                    str += GetCatImage(mp.P_pose_user_3, mp.P_pose_Pid_3, 340, mp.P_pose_uid_3, 3);
                }
                else
                {
                    str += SetButton(340, 3);
                }
                if (mp.P_pose_uid_4 > 0 && mp.P_pose_Pmid_4 > 0 && mp.P_pose_Pid_4 > 0)
                {
                    str += GetCatImage(mp.P_pose_user_4, mp.P_pose_Pid_4, 480, mp.P_pose_uid_4, 4);
                }
                else
                {
                    str += SetButton(480, 4);
                }
                str += "</SPAN>";
                return str;
            }
            else
            {
                return uinfo.UserName + "还未开通抢车位！快点通知他来参加吧！";
            }
        }
        catch
        {
           throw;
        }
    }
    #endregion

    //已经停放车辆的图片信息
    #region 已经停放车辆的图片信息
    private string GetCatImage(string username, int pid,int left,int userid,int poseid)
    {
        return "";
    }
    #endregion

    //设置停车的按钮
    #region 设置停车的按钮
    private string SetButton(int left,int index)
    {
        string str = "";
        str = "<div style=\"margin-left:" + (left + 5) + "px;  POSITION: absolute; margin-top:140px;  z-index:4\"><a herf=\"#\" onclick=\"javascript:showPopWin('我的车辆','MyCar.aspx?index=" + index +"&Math.random()',500,300, SetCar,true)\" ><img src=\"CarImage/stopcar.gif\"/></a></div>";
        return str;
    }
    #endregion

    //检查用户是否拥有车辆
    #region 检查用户是否拥有车辆
    public bool CheckCar()
    {
        try
        {
            List<ZL_Sns_MyCar> carlist = pbll.GetMyCarList(currentUser);
            //用户是否拥有车辆，没有就为用户添加一辆
            if (carlist.Count <= 0)
            {
                if (pbll.GetCarList().Count > 0)
                {
                    ZL_Sns_MyCar mycar = new ZL_Sns_MyCar();
                    mycar.P_uid = currentUser;
                    mycar.Pid = pbll.GetCarList()[0].Pid;
                    mycar.P_last_user = null;
                    mycar.P_last_uid = 0;
                    pbll.AddMyCar(mycar);
                }
                else
                {
                    return false;
                }
            }
            //检查用户是否初始化过车位
            if (!pbll.CheckUserPose(currentUser))
            {
                //为没有初始化车位的用户初始化车位
                pbll.AddMyPose(currentUser);
                List<ZL_Sns_CarConfig> cc = pbll.GetCarConfig();
                double Initialization = Convert.ToDouble(GetValue(cc, "Initialization "));


            }
            
            return true;
        }
        catch(Exception ee)
        {
            return false;
        }
    }
    #endregion

    //停放到好友车位
    #region 停放到好友车位
    protected void HiddenField1_ValueChanged(object sender, EventArgs e)
    {
        //this.Label1.Text = this.HiddenField1.Value;
        this.Label1.Text = Getstr(int.Parse(this.HiddenField1.Value));
    }
    #endregion

    //我的车辆
    #region 我的车辆
    protected void HiddenField2_ValueChanged(object sender, EventArgs e)
    {
        ZL_Sns_MyCar mycar = pbll.GetMyCar(currentUser, int.Parse(this.HiddenField2.Value));
        if (mycar.P_last_uid <= 0)
            Label1.Text = Getstr(currentUser);
        else
            Label1.Text = Getstr(mycar.P_last_uid);
    }
    #endregion

    //停车
    #region 停车
    protected void HiddenField3_ValueChanged(object sender, EventArgs e)
    {
        string[] str = this.HiddenField3.Value.Split(',');
        //判断当前车位是不是本用户的
        if (ViewState["userid"].ToString() != currentUser.ToString())
        {
            ZL_Sns_MyCar mc = pbll.GetMyCar(int.Parse(str[0]));
            //判断当前车位是不是用户当前车辆连续停放的用户车位
            if (ViewState["userid"].ToString() != mc.P_last_uid.ToString())
            {
                ZL_Sns_Report rp = pbll.GetCarReport(mc.P_action);
                if (rp.R_type == 1)
                {
                    List<ZL_Sns_CarConfig> cc = pbll.GetCarConfig();
                    string time = GetValue(cc, "Time");
                    string money = GetValue(cc, "Money");
                        
                    //计算停放车辆所得金钱
                    double reportmoney = Convert.ToDouble(time) * Convert.ToDouble(money) * DateDiff(rp.R_to_time);
                    //判断停放所得金钱是否大于限定最高限额
                    if (reportmoney > Convert.ToDouble(GetValue(cc, "Quota")))
                    {
                        reportmoney = Convert.ToDouble(GetValue(cc, "Quota"));
                    }
             
                    //修改停车表信息
                    pbll.UpdateReport(mc.P_action);
                }


                ZL_Sns_MyPose mp = pbll.GetMyPose(int.Parse(ViewState["userid"].ToString()));

                M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
                switch (str[1].ToString())
                {
                    case "1":
                        mp.P_pose_uid_1 = mc.P_uid;
                        mp.P_pose_Pid_1 = mc.Pid;
                        mp.P_pose_Pmid_1 = mc.Pmid;
                        mp.P_pose_user_1 = uinfo.UserName;
                        break;

                    case "2":
                        mp.P_pose_uid_2 = mc.P_uid;
                        mp.P_pose_Pid_2 = mc.Pid;
                        mp.P_pose_Pmid_2 = mc.Pmid;
                        mp.P_pose_user_2 = uinfo.UserName;
                        break;

                    case "3":
                        mp.P_pose_uid_3 = mc.P_uid;
                        mp.P_pose_Pid_3 = mc.Pid;
                        mp.P_pose_Pmid_3 = mc.Pmid;
                        mp.P_pose_user_3 = uinfo.UserName;
                        break;

                    case "4":
                        mp.P_pose_uid_4 = mc.P_uid;
                        mp.P_pose_Pid_4 = mc.Pid;
                        mp.P_pose_Pmid_4 = mc.Pmid;
                        mp.P_pose_user_4 = uinfo.UserName;
                        break;
                }
                uinfo = ubll.GetUserByUserID(int.Parse(ViewState["userid"].ToString()));
                    

                ZL_Sns_Report zsr = new ZL_Sns_Report();
                zsr.Pmid = mc.Pmid;
                zsr.Puid = mc.P_uid;
                zsr.R_to_uid = int.Parse(ViewState["userid"].ToString());
                zsr.R_type = 1;
                zsr.Pid = mc.Pid;

                mc.P_last_user = uinfo.UserName;
                mc.P_last_uid = uinfo.UserID;
                mc.P_last_time = DateTime.Now;
                mc.P_action = pbll.AddReport(zsr);
                pbll.UpdateMyPose(mp);
                pbll.UpdataMyCar(mc);
                Label1.Text = Getstr(uinfo.UserID);
            }
        }
    }
    #endregion

    //贴条
    #region 贴条
    protected void HiddenField4_ValueChanged(object sender, EventArgs e)
    {
       SetConfiscate(currentUser, this.HiddenField4.Value, 1);
    }
    #endregion

    #region 检测两个时间的差
    private double DateDiff(DateTime DateTime1)
    {
        TimeSpan ts = DateTime.Now - DateTime1;
        return ts.TotalMinutes;
    }
    #endregion

    #region 处理贴条和举报数据修改
    private void SetConfiscate(int Uid, string num,int type)
    {
        ZL_Sns_MyPose mp = pbll.GetMyPose(Uid);
        ZL_Sns_MyCar mc = new ZL_Sns_MyCar();
        string username = "";
        int prid = 0;
        int userid = 0;
        switch (num)
        {
            case "1":
                mc = pbll.GetMyCar(mp.P_pose_Pmid_1);
                prid = mc.P_action;
                username = mp.P_pose_user_1;
                userid = mc.P_uid;
                mp.P_pose_Pid_1 = 0;
                mp.P_pose_Pmid_1 = 0;
                mp.P_pose_uid_1 = 0;
                mp.P_pose_user_1 = "";

                break;

            case "2":
                mc = pbll.GetMyCar(mp.P_pose_Pmid_2);
                prid = mc.P_action;
                username = mp.P_pose_user_2;
                userid = mc.P_uid;
                mp.P_pose_Pid_2 = 0;
                mp.P_pose_Pmid_2 = 0;
                mp.P_pose_uid_2 = 0;
                mp.P_pose_user_2 = "";
                break;

            case "3":
                mc = pbll.GetMyCar(mp.P_pose_Pmid_3);
                prid = mc.P_action;
                username = mp.P_pose_user_3;
                userid = mc.P_uid;
                mp.P_pose_Pid_3 = 0;
                mp.P_pose_Pmid_3 = 0;
                mp.P_pose_uid_3 = 0;
                mp.P_pose_user_3 = "";
                break;

            case "4":
                mc = pbll.GetMyCar(mp.P_pose_Pmid_4);
                prid = mc.P_action;
                username = mp.P_pose_user_4;
                userid = mc.P_uid;
                mp.P_pose_Pid_4 = 0;
                mp.P_pose_Pmid_4 = 0;
                mp.P_pose_uid_4 = 0;
                mp.P_pose_user_4 = "";
                break;
        }
        List<ZL_Sns_CarConfig> cc = pbll.GetCarConfig();
        string time = GetValue(cc, "Time");
        string money = GetValue(cc, "Money");
        ZL_Sns_Report rp = pbll.GetCarReport(prid);
        //计算停放车辆所得金钱
        double reportmoney = Convert.ToDouble(time) * Convert.ToDouble(money) * DateDiff(rp.R_to_time);
        //判断停放所得金钱是否大于限定最高限额
        if (reportmoney > Convert.ToDouble(GetValue(cc, "Quota")))
        {
            reportmoney = Convert.ToDouble(GetValue(cc, "Quota"));
        }
        //修改停放信息，将车挪走
        pbll.UpdateReport(prid);
        //修改用户车位信息
        pbll.UpdateMyPose(mp);
        if (type == 1)
        {
            //修改用户钱包
        

            //添加被贴条用户信息日志
            ZL_Sns_CarLog clog = new ZL_Sns_CarLog();
            clog.P_uid = userid;
            clog.P_type = 1;
            clog.P_content = "被" + ubll.GetUserByUserID(currentUser).UserName + "成功贴条，没收停车所得" + reportmoney + "元";
            pbll.AddCarLog(clog);
            //添加用户的贴条日志
            clog.P_uid = currentUser;
            clog.P_type = 2;
            clog.P_content = "贴条成功！没收" + username + "的停车所得" + reportmoney + "元";
            pbll.AddCarLog(clog);
        }
        else
        {
            //修改用户钱包
            int bonus = Convert.ToInt32(GetValue(cc, "Bonus"));
   

            //添加被举报用户信息日志
            ZL_Sns_CarLog clog = new ZL_Sns_CarLog();
            clog.P_uid = userid;
            clog.P_type = 1;
            clog.P_content = "被" + ubll.GetUserByUserID(currentUser).UserName + "成功举报，没收停车所得" + reportmoney + "元";
            pbll.AddCarLog(clog);
            //添加用户的举报日志
            clog.P_uid = currentUser;
            clog.P_type = 2;
            clog.P_content = "举报成功！没收" + username + "的停车所得" + reportmoney + "元";
            pbll.AddCarLog(clog);
        }
        this.Label1.Text = Getstr(userid);
        Bind();
        BindUserInfo();
    }
    #endregion

    #region 获取游戏规则详细数据
    private string GetValue(List<ZL_Sns_CarConfig> list, string str)
    {
        string s="";
        foreach (ZL_Sns_CarConfig cc in list)
        {
            if (cc.CKey == str)
                s = cc.Cvalue;
        }
        return s;
    }
    #endregion

    //举报
    #region 举报
    protected void HiddenField5_ValueChanged(object sender, EventArgs e)
    {
        SetConfiscate(int.Parse(ViewState["userid"].ToString()), this.HiddenField5.Value, 2);
    }
    #endregion
    //上一个
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        this.Label1.Text = Getstr(currentUser);
    }
    //下一个
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    
}

