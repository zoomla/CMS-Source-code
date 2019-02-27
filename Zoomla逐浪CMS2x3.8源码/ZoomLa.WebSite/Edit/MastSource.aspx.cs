using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;


public partial class Edit_Contents : System.Web.UI.Page
{

    //protected B_EditWord bedit = new B_EditWord();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["type"] == "clear")
        {
            ClearClipboard();
        }
    }
    public string getContent()
    {
        //if (Request.QueryString["Itemid"] != null)
        //{
        //    int Itemid = Convert.ToInt16(Request.QueryString["Itemid"]);
        //    return bedit.Select_Content(Itemid);
        //}
        //else
        //{
        return "";
        //}
    }
    private void ClearClipboard()
    {
        //System.Windows.Forms.Clipboard.SetDataObject( " ") 
    }
}