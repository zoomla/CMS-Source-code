using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model.Plat;

public partial class Plat_Blog_Declined : System.Web.UI.Page
{
    //只能删除和修改自己建的
    B_Blog_Sdl sdlBll = new B_Blog_Sdl();
    M_Blog_Sdl sdlMod = new M_Blog_Sdl();
    B_User buser = new B_User();
    public int DetailID { get { return Convert.ToInt32(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sdlMod = sdlBll.SelReturnModel(DetailID);
            Name.Text = sdlMod.Name;
            StartDate.Text = sdlMod.StartDate.ToString();
            EndDate.Text = sdlMod.EndDate.ToString();
            //LeaderIDS_L.Text = buser.GetUserNameByIDS(sdlMod.LeaderIDS);
            //ParterIDS_L.Text = buser.GetUserNameByIDS(sdlMod.ParterIDS);
            Describe.Text = sdlMod.Describe;
        }
    }
    protected void Edit_Btn_Click(object sender, EventArgs e)
    {
        M_Blog_Sdl sdlmod = FillModel();
        sdlBll.UpdateByID(sdlmod);
        function.Script(this, "UpdateData("+sdlmod.ID+",'"+sdlmod.Name+ "');HideMe()");
    }
    protected void Del_Btn_Click(object sender, EventArgs e)
    {
        sdlBll.Del(DetailID);
        function.Script(this, "DelData(" + DetailID + ");HideMe();");
    }
    private M_Blog_Sdl FillModel() 
    {
        M_Blog_Sdl model = new M_Blog_Sdl();
        model = sdlBll.SelReturnModel(DetailID);
        model.StartDate = Convert.ToDateTime(StartDate.Text);
        model.EndDate = Convert.ToDateTime(EndDate.Text);
        model.Name = Name.Text;
        model.Describe = Describe.Text;
        return model;
    }
}