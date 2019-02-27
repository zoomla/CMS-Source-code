using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Web.Security;
using ZoomLa.BLL.Design;
using ZoomLa.Model.Design;
using System.Text.RegularExpressions;
using ZoomLa.BLL.Helper;
/*
* 快速创建场景,上传图片后直接生成场景
*/
public partial class Design_mbh5_FastCreate : System.Web.UI.Page
{
    B_Design_Scence seBll = new B_Design_Scence();
    B_Design_Tlp tlpBll = new B_Design_Tlp();
    B_User buser = new B_User();
    public bool IsPC { get { return DeviceHelper.GetAgent() == DeviceHelper.Agent.PC; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void save_btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_Design_Tlp tlpMod = tlpBll.SelModelByDef("mbh5_fast");
        M_Design_Page seMod = null;
        if (tlpMod == null) { function.WriteErrMsg("模板不存在"); }
        seMod = seBll.SelModelByTlp(tlpMod.ID);
        if (seMod == null) { function.WriteErrMsg("场景模板不存在"); }
        string[] imgs = imgs_hid.Value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        if (imgs.Length < 1) { function.WriteErrMsg("未指定图片"); }
        //-----------开始替换
        string holder = "/images/nopic.gif";
        seMod.TlpID = 0;
        seMod.ID = 0;
        seMod.guid = "";
        seMod.CDate = DateTime.Now;
        seMod.UPDate = DateTime.Now;
        seMod.UserID = mu.UserID;
        seMod.UserName = mu.UserName;
        for (int i = 0; seMod.comp.Contains(holder); i++)
        {
            int start = seMod.comp.IndexOf(holder);
            seMod.comp = seMod.comp.Remove(start, holder.Length);
            seMod.comp = seMod.comp.Insert(start, imgs[i % imgs.Length]);
        }
        seMod.ID = seBll.Insert(seMod);
        if (IsPC)
        {
            function.Script(this, "top.CloseDiag();top.location.href='/Design/H5/Default.aspx?id=" + seMod.guid + "';");
        }
        else { Response.Redirect("default.aspx?id=" + seMod.guid); }
    }
}