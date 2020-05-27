using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Common;

public partial class manage_AddOn_AddProjects : CustomerPageAction
{
    B_User ull = new B_User();
    protected B_Sensitivity sll = new B_Sensitivity();
    //页面加载
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        ull.CheckIsLogin();
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            //dt = bpt.Select_All();
            DDList.DataSource = dt;
            DDList.DataTextField = "ProjectTypeName";
            DDList.DataValueField = "ProjectTypeID";
            DDList.DataBind();
        }
    }

    //提交
    protected void Commit_Click(object sender, EventArgs e)
    {
        //mps.Name = Request.Form["TxtProjectName"];
        //mps.TypeID =Convert.ToInt32( Request.Form["DDList"]);
        //mps.Price = DataConverter.CDecimal(Request.Form["TxtProjectPrice"]);
        //mps.Requirements = Request.Form["TxtProjectRequire"];
        //mps.ApplicationTime =DataConverter.CDate( DateTime.Now.ToString());
        //mps.BeginTime = DateTime.MaxValue;
        //mps.CompletionTime = DateTime.MaxValue;
        //获取用户信息xml储存
        string str = "<Info>";
        str += "<item id=\"name\">" + this.TxtUserName.Text.Trim() + "</item>";
        str += "<item id=\"company\">" + this.TxtUserCompany.Text.Trim() + "</item>";
        str += "<item id=\"tel\">" + this.TxtUserTel.Text.Trim() + "</item>";
        str += "<item id=\"mobile\">" + this.TxtUserMobile.Text.Trim() + "</item>";
        str += "<item id=\"qq\">" + this.TxtUserQQ.Text.Trim() + "</item>";
        str += "<item id=\"msn\">" + this.TxtUserMSN.Text.Trim() + "</item>";
        str += "<item id=\"address\">" + this.TxtUserAddress.Text.Trim() + "</item>";
        str += "<item id=\"email\">" + this.TxtUserEmail.Text.Trim() + "</item>";
        str += "</Info>";
        //mps.AuditStatus = 1;
        //mps.UserInfo = str;
        //int id = bps.GetInsert(mps);

        if (this.TxtUserName.Text.Trim() != "")
        {
            B_Client_Basic bll = new B_Client_Basic();
            B_Client_Penson pll = new B_Client_Penson();
            DataTable tbles = bll.SelByName(this.TxtUserName.Text.Trim());
            if (tbles.Rows.Count == 0)
            {
                string Code = function.GetFileName();
                M_Client_Basic baseinfo = new M_Client_Basic();
                baseinfo.P_name = this.TxtUserName.Text.Trim();
                baseinfo.Client_Type = "0";
                baseinfo.Add_Date = DateTime.Now;
                baseinfo.Code = Code;
                //baseinfo.Unit_Address = this.TxtUserCompany.Text.Trim();
                bll.GetInsert(baseinfo);

                M_Client_Penson pinfo = new M_Client_Penson();
                pinfo.Fax_phone = this.TxtUserTel.Text.Trim();
                pinfo.Email_Address = this.TxtUserEmail.Text.Trim();
                pinfo.MSN_Code = this.TxtUserMSN.Text.Trim();
                pinfo.Message_Address = this.TxtUserAddress.Text.Trim();
                pinfo.QQ_Code = this.TxtUserQQ.Text.Trim();
                pinfo.Work_Phone = this.TxtUserMobile.Text.Trim();
                pinfo.Code = Code;
                pinfo.Birthday = DateTime.Now;
       
                
                pll.GetInsert(pinfo);
            }
        }

        //DataTable dt = bpf.Select_All();
        DataTable table = new DataTable();  
        table.Columns.Add(new DataColumn("FieldName", typeof(string)));
        table.Columns.Add(new DataColumn("FieldType", typeof(string)));
        table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
        //if (dt != null)
        //{
        //    //获取自定义字段的列表
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        if (DataConverter.CBool(dr["IsNotNull"].ToString()))
        //        {
        //            if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
        //            {
        //                function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
        //            }
        //        }

        //        if (dr["FieldType"].ToString() == "FileType")
        //        {
        //            string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
        //            bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
        //            string sizefield = Sett[1].Split(new char[] { '=' })[1];
        //            if (chksize && sizefield != "")
        //            {
        //                DataRow row2 = table.NewRow();
        //                row2[0] = sizefield;
        //                row2[1] = "FileSize";
        //                row2[2] = this.Page.Request.Form["txt_" + sizefield];
        //                table.Rows.Add(row2);
        //            }
        //        }

        //        if (dr["FieldType"].ToString() == "MultiPicType")
        //        {
        //            string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
        //            bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
        //            string sizefield = Sett[1].Split(new char[] { '=' })[1];
        //            if (chksize && sizefield != "")
        //            {
        //                if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + sizefield]))
        //                {
        //                    function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
        //                }
        //                DataRow row1 = table.NewRow();
        //                row1[0] = sizefield;
        //                row1[1] = "ThumbField";
        //                row1[2] = this.Page.Request.Form["txt_" + sizefield];
        //                table.Rows.Add(row1);
        //            }
        //        }
        //        DataRow row = table.NewRow();
        //        row[0] = dr["FieldName"].ToString();
        //        string ftype = dr["FieldType"].ToString();
        //        row[1] = ftype;
        //        string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
        //        if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
        //        {
        //            fvalue = sll.ProcessSen(fvalue);
        //        }
        //        row[2] = fvalue;
        //        table.Rows.Add(row);
        //    }
        //}
        //显示自定义字段
        if (table != null)
        {
            if (table.Rows.Count > 0)
            {
                //int id = bps.GetInsert(mps);
                //bps.InsertModel(table, "ZL_ProjectsBase",id);
            }
        }

        if (Request.Form["project"] != null)
        {
            string project = Request.Form["project"];
            string projectname = Request.Form["projectname"];

            if (project.IndexOf(",") > -1)
            {
                string[] arrlist = project.Split(new string[] { "," }, StringSplitOptions.None);//value
                string[] namelist = projectname.Split(new string[] { "," }, StringSplitOptions.None);//name

                for (int i = 0; i < arrlist.Length; i++)
                {
                     //arrlist[i];
                    //M_Processes processesinfo = new M_Processes();
                    //processesinfo.Info = "";
                    //processesinfo.IsComplete = 0;
                    //processesinfo.CompleteTime = DateTime.Now;
                    //processesinfo.Name = namelist[i].ToString();
                    ////processesinfo.OpjectID = id;
                    //processesinfo.Progress = DataConverter.CLng(arrlist[i]);
                    //pll.GetInsert(processesinfo);
                }
            }
        }
        function.WriteSuccessMsg("操作成功!", "Projects.aspx");
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        //foreach (Control ctr in this.form1.Controls)
        //{
        //    if (ctr is TextBox)
        //    {
        //        (ctr as TextBox).Text = "";
        //    }
        //}
    }
}
