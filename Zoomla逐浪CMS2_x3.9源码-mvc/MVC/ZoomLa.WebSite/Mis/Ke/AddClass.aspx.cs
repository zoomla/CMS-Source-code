using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;


namespace ZoomLaCMS.MIS.Ke
{
    public partial class AddClass : CustomerPageAction
    {
        protected B_ExClassgroup ell = new B_ExClassgroup();
        private B_Course bcourse = new B_Course();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["cid"] != null)
                {
                    string cid = Request.QueryString["cid"].ToString();
                    M_ExClassgroup cinfo = ell.GetSelect(DataConverter.CLng(cid));
                    this.txt_Regulationame.Text = cinfo.Regulationame.ToString();
                    txt_Endtime.Text = cinfo.Endtime.ToString();
                    txt_Ratednumber.Text = cinfo.Ratednumber.ToString();
                    txt_Regulation.Text = cinfo.Regulation.ToString();
                    txt_Regulation.Text = cinfo.Regulation.ToString();
                    txt_Setuptime.Text = cinfo.Setuptime.ToString();
                    txtShiPrice.Text = cinfo.ShiPrice.ToString();
                    txtLinkPrice.Text = cinfo.LinPrice.ToString();
                    txtCoureTime.Text = cinfo.CourseHour.ToString();
                    rbl.Checked = cinfo.Presented == 1 ? true : false;
                    classtype.SelectedValue = cinfo.ClassID.ToString();
                    classid_Hid.Value = cid;
                    this.EBtnSubmit.Text = "修改班级";
                }
            }
        }

        private string GetCourese(int courseid)
        {
            M_Course mcourse = bcourse.GetSelect(courseid);
            if (mcourse != null && mcourse.id > 0)
            {
                return mcourse.CourseName;
            }
            else
            {
                return "";
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string strDay = calculateTime(txt_Setuptime.Text, txt_Endtime.Text);
            //if (strDay != null)
            //{
            //    if (Convert.ToInt32(strDay) <= 0)
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('结束时间不能小于或等于建立时间');</script>");
            //        return;
            //    }
            //    else
            //    {
            //        lbDay.Text = strDay + "天";
            //    }
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('建立与结束时间错误！');</script>");
            //    return;
            //}
            M_ExClassgroup exinfo = new M_ExClassgroup();
            //exinfo.Actualnumber =DataConverter.CLng(this.txt_Actualnumber.Text);
            exinfo.Endtime = DataConverter.CDate(txt_Endtime.Text);
            exinfo.Ratednumber = DataConverter.CLng(txt_Ratednumber.Text);
            exinfo.Regulation = txt_Regulation.Text;
            exinfo.Regulationame = txt_Regulationame.Text;
            exinfo.Setuptime = DataConverter.CDate(txt_Setuptime.Text);
            exinfo.ShiPrice = DataConverter.CDouble(txtShiPrice.Text);
            exinfo.LinPrice = DataConverter.CDouble(txtLinkPrice.Text);
            exinfo.CourseHour = DataConverter.CLng(txtCoureTime.Text);
            exinfo.Presented = rbl.Checked ? 1 : 0;
            exinfo.ClassID = DataConverter.CLng(classtype.SelectedValue);
            if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
            {
                int txt_Groupid = DataConverter.CLng(Request.QueryString["cid"].ToString());
                exinfo.GroupID = txt_Groupid;
                ell.GetUpdate(exinfo);
                function.WriteSuccessMsg("修改成功!", "ClassManage.aspx");
            }
            else
            {
                ell.GetInsert(exinfo);
                function.WriteSuccessMsg("添加成功!", "ClassManage.aspx");
            }
        }
        public string calculateTime(string strdt1, string strdt2)
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            try
            {
                dt1 = DateTime.Parse(strdt1.Trim());
                dt2 = DateTime.Parse(strdt2.Trim());
            }
            catch
            {
                return null;
            }
            TimeSpan ts1 = new TimeSpan(dt1.Ticks);
            TimeSpan ts2 = new TimeSpan(dt2.Ticks);

            return (ts2 - ts1).Days.ToString();
        }
    }
}