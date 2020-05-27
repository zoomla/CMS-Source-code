using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class Search_SearchClear : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //清除
    protected void btnclear_Click(object sender, EventArgs e)
    {
        //int count = DataConverter.CLng(txtCount.Text.Trim());
        //string updatetime = txtUpdate.Text.Trim() == "" ? null :txtUpdate.Text.Trim();
        //string stoData = txtStorageData.Text.Trim() == "" ?null : txtStorageData.Text.Trim();

        //bool result = bkey.GetDelect(count,stoData, updatetime);
        //if (result)
        //{
        //    Response.Write("<script>alert('清除成功!')</script>");
        //}
        //else
        //{
        //    Response.Write("<script>alert('清除失败!')</script>");
        //}
    }
}
