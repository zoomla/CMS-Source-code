﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class Addproject :CustomerPageAction
    {

        B_Product pll = new B_Product();
        B_BindPro bll = new B_BindPro();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        

        public string getproimg(string type)
        {
            string restring;
            restring = "";

            if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
            if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
            {
                restring = "<img src=../../" + type + " border=0 width=60 height=45>";
            }
            else
            {
                restring = "<img src=../../UploadFiles/nopic.gif border=0 width=60 height=45>";
            }
            return restring;

        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>window.close();</script>");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
     
        }
    }
}