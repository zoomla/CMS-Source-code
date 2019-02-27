using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class User_Info_WebUserControlTop : System.Web.UI.UserControl
{
    private B_Group bgp = new B_Group();
    public int GroupID 
    { 
        get {return DataConverter.CLng(ViewState["groupid"]);}
        set { ViewState["groupid"] = value ; } 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string modellist = bgp.GetGroupModel(GroupID);
        string model = "";
        if (!string.IsNullOrEmpty(modellist))
        {
            B_Model mbll = new B_Model();
            string[] modarr = modellist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < modarr.Length; i++)
            {
                M_ModelInfo mod = mbll.GetModelById(DataConverter.CLng(modarr[i]));
                if (mod.MultiFlag)
                    model = model + "<a href=\"ModelInfoList.aspx?ModelID=" + mod.ModelID.ToString() + "\">" + mod.ModelName + "</a>&nbsp;&nbsp;";
                else
                    model = model + "<a href=\"Jobview.aspx?ModelID=" + mod.ModelID.ToString() + "\">" + mod.ModelName + "</a>&nbsp;&nbsp;";
            }
        }
        this.Label4.Text = model;
    }


}
