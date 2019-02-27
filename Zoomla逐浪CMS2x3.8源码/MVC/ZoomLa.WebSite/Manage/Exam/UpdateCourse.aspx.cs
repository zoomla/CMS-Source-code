using System;
namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using System.Data;
    public partial class UpdateCourse : System.Web.UI.Page
    {
        private B_Courseware bcourse = new B_Courseware();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = DataConverter.CLng(Request.QueryString["ID"]);
                coureId.Value = id.ToString();
                M_Courseware mcs = bcourse.GetSelect(id);
                if (mcs != null && mcs.ID > 0)
                {
                    this.txt_Courename.Text = mcs.Courseware;
                    this.TextBox1.Text = mcs.Speaker;
                    this.TextBox2.Text = mcs.SJName;
                    this.txt_Order.Text = (mcs.CoursNum).ToString();
                    this.RadioButtonList1.SelectedValue = mcs.CoursType.ToString();
                    this.RadioButtonList2.SelectedValue = mcs.Status.ToString();
                    this.rblHot.SelectedValue = mcs.Listen.ToString();
                    this.txtP_Content.Text = mcs.Description;
                    this.txtUrl.Text = mcs.FileUrl;
                }

            }
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>修改试听文件</li>");
        }
        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(coureId.Value);
            M_Courseware mc = bcourse.GetSelect(id);
            mc.Courseware = this.txt_Courename.Text;
            mc.Speaker = this.TextBox1.Text;
            mc.SJName = this.TextBox2.Text;
            mc.CoursNum = DataConverter.CLng(this.txt_Order.Text);
            mc.CoursType = DataConverter.CLng(this.RadioButtonList1.SelectedValue);
            mc.Status = DataConverter.CLng(this.RadioButtonList2.SelectedValue);
            mc.Listen = DataConverter.CLng(this.rblHot.SelectedValue);
            mc.Description = this.txtP_Content.Text;
            mc.FileUrl = this.txtUrl.Text;
            mc.UpdateTime = DateTime.Now;
            mc.CreationTime = DateTime.Now;
            if (mc != null && mc.ID > 0)
            {
                bool result = bcourse.GetUpdate(mc);
                if (result)
                {
                    function.WriteSuccessMsg("修改成功！");
                }
                else
                {
                    function.WriteErrMsg("修改失败！");
                }
            }
        }
        #endregion
    }
}