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
using ZoomLa.Model;

public partial class User_UserZone_School_ControlRoomInfo : System.Web.UI.UserControl
{
    private B_User bu=new B_User ();
    private B_ClassRoom bcr = new B_ClassRoom();
    private B_School bs = new B_School();
    private int roomid = 0;

    public int Roomid
    {
        get
        {
            if (roomid == 0)
                return 0;
            else
                return roomid;
        }
        set
        {
            roomid = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        M_ClassRoom cr = bcr.GetSelect(Roomid);
        tdCreateUser.InnerHtml = bu.GetUserByUserID(cr.CreateUser).UserName;
        tdCreateTime.InnerHtml = cr.Creation.Year.ToString() + "-" + cr.Creation.Month.ToString() + "-" + cr.Creation.Day.ToString() ;
        tdYear.InnerHtml = cr.Grade.ToString ();
        tdSchool.InnerHtml = bs.GetSelect(cr.SchoolID).SchoolName;
    }
}
