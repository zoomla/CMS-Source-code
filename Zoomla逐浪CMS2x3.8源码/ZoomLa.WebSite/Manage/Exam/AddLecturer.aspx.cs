using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class manage_Question_AddLecturer : CustomerPageAction
{
    private B_Exam_Class bqc = new B_Exam_Class ();
    private B_ExLecturer bel = new B_ExLecturer();
    private B_Admin badmin = new B_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            hftid.Value = id.ToString();
            if (id > 0)
            {
                liCoures.Text = "修改讲师";
                M_ExLecturer mel = bel.GetSelect(id);
                if (mel != null && mel.ID > 0)
                {
                    txt_name.Text = mel.TechName;
                    txt_Phone.Text = mel.TechPhone;
                    txt_Awardsinfo.InnerHtml = mel.Awardsinfo;
                    txt_Popularity.Text = mel.Popularity.ToString();
                    txt_TechHobby.InnerHtml = mel.TechHobby;
                    txt_TechIntrodu.InnerHtml = mel.TechIntrodu;
                    txt_TechSpecialty.InnerHtml = mel.TechSpecialty;
                    txtClassname.Text = GetClassname(mel.TechDepart);
                    hfid.Value = mel.TechDepart.ToString();
                    SFile_Up.FileUrl = mel.FileUpload;
                    ddLevel.SelectedValue = mel.TechLevel.ToString();
                    ddSex.SelectedValue = mel.TechSex.ToString();
                    ddTechRecom.SelectedValue = mel.TechRecom.ToString();
                    ddTitle.SelectedValue = mel.TechTitle;
                    ddType.SelectedValue = mel.TechType;
                    ddProfessional.SelectedValue = mel.Professional.ToString();
                }
            }
            else
            {
                liCoures.Text = "添加讲师";
            }
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>讲师管理</li><li>" + liCoures.Text + "</li>");
        }
    }

    private string GetClassname(int classid)
    {
        M_Exam_Class mqc = bqc.GetSelect(classid);
        if (mqc != null && mqc.C_id > 0)
        {
            return mqc.C_ClassName;
        }
        else
        {
            return "";
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        int id = DataConverter.CLng(hftid.Value);
        M_ExLecturer exl = bel.GetSelect(id);
        exl.TechName = BaseClass.Htmlcode(txt_name.Text);
        exl.AddUser = badmin.GetAdminLogin().AdminId;
        exl.Awardsinfo = txt_Awardsinfo.InnerHtml;
         string fileul =getFilePath();
        if (!string.IsNullOrEmpty(fileul))
        {
            exl.FileUpload = fileul;
        }
        exl.Popularity = DataConverter.CLng(txt_Popularity.Text);
        exl.Professional = DataConverter.CLng(ddProfessional.SelectedValue);
        exl.TechDepart = DataConverter.CLng(hfid.Value);
        exl.TechHobby = BaseClass.Htmlcode(txt_TechHobby.InnerHtml);
        exl.TechIntrodu = BaseClass.Htmlcode(txt_TechIntrodu.InnerHtml);
        exl.TechLevel = ddLevel.SelectedValue;
        exl.TechPhone = BaseClass.Htmlcode(txt_Phone.Text);
        exl.TechRecom = DataConverter.CLng(ddTechRecom.SelectedValue);
        exl.TechSex = DataConverter.CLng(ddSex.SelectedValue);
        exl.TechSpecialty = BaseClass.Htmlcode(txt_TechSpecialty.InnerHtml);
        exl.TechTitle = ddTitle.SelectedValue;
        exl.TechType = ddType.SelectedValue;
        if (id > 0)
        {
            bool result = bel.GetUpdate(exl);
            if (result)
            {
                function.WriteSuccessMsg("修改成功！", "LecturerManage.aspx");
            }
            else
            {
                function.WriteErrMsg("修改失败！");
            }
        }
        else
        {
            exl.CreateTime = DateTime.Now;
            int ids = bel.GetInsert(exl);
            if (ids > 0)
            {
                function.WriteSuccessMsg("添加成功！", "LecturerManage.aspx");
            }
            else
            {
                function.WriteErrMsg("添加失败！");
            }
        }
    }

    public string getFilePath()
    {
        string upload;
        upload = SiteConfig.SiteOption.UploadDir;
        string filepath = Server.MapPath("/" + upload + "/Teacher/");
        FileSystemObject.CreateFileFolder(filepath, HttpContext.Current);
        SFile_Up.SaveUrl = function.PToV(filepath);
        return SFile_Up.SaveFile();
    }
}