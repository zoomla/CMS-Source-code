using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;
using ZoomLa.Model;

/*
 * 创建人与主负责人,拥有修改删作权限
 * 项目成员拥有浏览权限
 * 其他人无权限访问该页
 */

namespace ZoomLaCMS.Plat.Blog
{
    public partial class ProjectDetail : System.Web.UI.Page
    {
        B_Plat_Pro proBll = new B_Plat_Pro();
        M_Plat_Pro proMod = new M_Plat_Pro();
        B_User_Plat upBll = new B_User_Plat();
        public int DetailID { get { return Convert.ToInt32(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                proMod = proBll.SelReturnModel(DetailID);
                AuthCheck(proMod);
                Name.Text = proMod.Name;
                IsStatues.SelectedValue = proMod.Status.ToString();
                StartDate.Text = proMod.StartDate.ToString("yyyy/MM/dd"); ;
                EndDate.Text = proMod.EndDate.ToString("yyyy/MM/dd");
                CUser_T.Text = upBll.SelInfoByIDS(proMod.UserID.ToString());//需替换为头像与真名可切换显示的
                LeaderIDS_L.Text = upBll.SelInfoByIDS(proMod.LeaderIDS.ToString());
                ParterIDS_L.Text = upBll.SelInfoByIDS(proMod.ParterIDS.ToString());
                LeaderIDS_Hid.Value = proMod.LeaderIDS;
                ParterIDS_Hid.Value = proMod.ParterIDS;
                Describe.Text = proMod.Desc;
                IsOpen_Rad.SelectedValue = proMod.IsOpen.ToString();
                ShowTaskFile();
            }
        }
        public void AuthCheck(M_Plat_Pro model)
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            if (model.UserID == upMod.UserID || model.LeaderIDS.Contains("," + upMod.UserID + ","))
            {

            }
            else if (model.ParterIDS.Contains("," + upMod.UserID + ","))
            {
                Edit_Btn.Visible = false;
                StartDate.Enabled = false;
                EndDate.Enabled = false;
                Describe.Enabled = false;
            }
            else
            {
                function.WriteErrMsg("你并非该组成员,无权访问该项目");
            }
        }
        //附件
        void ShowTaskFile()
        {
            if (proMod != null && !proMod.Attach.Trim().Equals(""))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ExtName"));
                dt.Columns.Add(new DataColumn("FileName"));
                dt.Columns.Add(new DataColumn("Path"));
                string[] fileurls = proMod.Attach.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < fileurls.Length; i++)
                {
                    if (!string.IsNullOrEmpty(fileurls[i]))
                    {
                        string[] datas = new string[3];
                        datas[0] = GroupPic.GetExtNameMini(Path.GetExtension(fileurls[i]).Replace(".", ""));
                        string fname = Path.GetFileName(fileurls[i]);
                        datas[1] = fname.Length > 6 ? fname.Substring(0, 5) + "..." : fname;
                        datas[2] = fileurls[i];
                        dt.Rows.Add(datas);
                    }
                }
                RShowFilelist.DataSource = dt;
                RShowFilelist.DataBind();
            }
        }
        protected void Edit_Btn_Click(object sender, EventArgs e)
        {
            proBll.UpdateByID(FillModel());
            Response.Redirect(Request.RawUrl);
        }
        protected void Del_Btn_Click(object sender, EventArgs e)
        {
            proBll.Del(DetailID);
        }
        private M_Plat_Pro FillModel()
        {
            M_Plat_Pro model = new M_Plat_Pro();
            model = proBll.SelReturnModel(DetailID);
            model.StartDate = Convert.ToDateTime(StartDate.Text);
            model.EndDate = Convert.ToDateTime(EndDate.Text);
            model.Name = Name.Text;
            model.LeaderIDS = LeaderIDS_Hid.Value;
            model.ParterIDS = ParterIDS_Hid.Value;
            model.IsOpen = Convert.ToInt32(IsOpen_Rad.SelectedValue);
            model.Desc = Describe.Text;
            model.Status = Convert.ToInt32(IsStatues.SelectedValue);
            if (!string.IsNullOrEmpty(Attach_Hid.Value))
            {
                model.Attach += Attach_Hid.Value + ",";
            }
            return model;
        }
    }
}