using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;

public partial class manage_Exam_QuestionSelect : CustomerPageAction
{
    private B_Admin badmin = new B_Admin();
    private B_Exam_Sys_Questions bq = new B_Exam_Sys_Questions();
    private B_Exam_Type bqt = new B_Exam_Type();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindType();
            int id = DataConverter.CLng(Request.QueryString["id"]);
            M_Exam_Sys_Questions mq = bq.GetSelect(id);
            if (mq != null && mq.p_id > 0)
            {
                this.ddlType.SelectedValue = mq.p_Type.ToString();
                ddlType.Enabled = false;
                this.shuming.Text = mq.p_shuming.ToString();
                hfpid.Value = mq.p_id.ToString();
                txtP_Content.Text = mq.p_Content;
                hffilename.Value = mq.p_Shipin;
                txtCourse.Text = mq.p_defaultScores.ToString();
                hfParentId.Value = mq.parentId.ToString();
                ddlNumber1.SelectedValue = mq.p_ChoseNum.ToString();
                hfoption.Value = SafeSC.ReadFileStr(M_Exam_Sys_Questions.OptionDir+mq.p_id+".opt");
                hfanw.Value = mq.p_Answer;
            }
            else
            {
                ddlType.Enabled = true;
                hfParentId.Value = Request.QueryString["p_Id"];
            }
            option();
        }
        Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>课程管理</li>");
    }

    /// <summary>
    /// 绑定题型
    /// </summary>
    private void BindType()
    {
        B_Exam_Type bqt = new B_Exam_Type();
        List<M_Exam_Type> mqts = bqt.SelectAll();
        if (mqts != null && mqts.Count > 0)
        {
            this.ddlType.Items.Clear();
            for (int i = 0; i < mqts.Count; i++)
            {
                if (mqts[i].t_type == 1 || mqts[i].t_type == 2 || mqts[i].t_type == 3)
                {
                    ListItem li = new ListItem();
                    li.Text = mqts[i].t_name;
                    li.Value = mqts[i].t_id.ToString();
                    ddlType.Items.Add(li);
                }
            }
        }
    }

    /// <summary>
    /// 试题题型选择：页面显示
    /// </summary>
    private void option()
    {
        string option = hfoption.Value;
        string answer = hfanw.Value;

        int type = DataConverter.CLng(ddlType.SelectedValue);
        string[] str = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        M_Exam_Type mqt = bqt.GetSelectById(type);
        int num = DataConverter.CLng(ddlNumber1.SelectedValue);

        string[] options = option.Split(',');
        string[] answers = answer.Split(',');

        string opt = "";
        string anws = "";

        //单选,多选,判断，填空,问答，组合
        switch (mqt.t_type)
        {
            case 1:              //单选
                ddlNumber1.Visible = true;
                Tips.Visible = true;
                optionDiv.Visible = true;
                anwDiv.Visible = true;
                optionDiv.InnerHtml = "";
                anwDiv.InnerHtml = "";
                Tips.Text += "<font color='red'>&nbsp;注意：以下信息请不要输入','(逗号)</font>";
                for (int i = 0; i < num; i++)
                {
                    if (i < options.Length)
                    {
                        opt = options[i];
                    }
                    else
                    {
                        opt = "";
                    }
                    optionDiv.InnerHtml += str[i] + "： <textarea name='option' id='option" + i + "' style='width:400px;height:43px'>" + opt + "</textarea><br/>";
                    if (str[i] == answer)
                    {
                        anws = "checked='checked'";
                    }
                    else
                    {
                        anws = "";
                    }
                    anwDiv.InnerHtml += str[i] + "<input type='radio' id='anw" + i + "' name='anws' value='" + str[i] + "' " + anws + " />";
                }
                break;
            case 2:       //多选
                ddlNumber1.Visible = true;
                Tips.Visible = true;
                Tips.Text += "<font color='red'>&nbsp;注意：以下信息请不要输入','(逗号)</font>";
                optionDiv.Visible = true;
                anwDiv.Visible = true;
                optionDiv.InnerHtml = "";
                anwDiv.InnerHtml = "";
                for (int i = 0; i < num; i++)
                {
                    if (i < options.Length)
                    {
                        opt = options[i];
                    }
                    else
                    {
                        opt = "";
                    }
                    optionDiv.InnerHtml += str[i] + "： <textarea name='option' id='option" + i + "' style='width:400px;height:43px' >" + opt + "</textarea><br/>";
                    if (answer.IndexOf(str[i]) > -1)
                    {
                        anws = "checked='checked'";
                    }
                    else
                    {
                        anws = "";
                    }
                    anwDiv.InnerHtml += str[i] + "<input type='checkbox' id='anw" + i + "' name='anws'  value='" + str[i] + "' " + anws + "/>  ";
                }
                break;
            case 3:  //判段
                ddlNumber1.Visible = false;
                Tips.Visible = false;
                optionDiv.Visible = false;
                anwDiv.Visible = true;
                if (answer == "0")
                {
                    anwDiv.InnerHtml = "<input type='radio' id='anw1' name='anws' value='0' checked='checked'/>错误&nbsp;<input type='radio' id='anw2' name='anws' value='1'  />正确 ";
                }
                else
                {
                    anwDiv.InnerHtml = "<input type='radio' id='anw1' name='anws' value='0' />错误&nbsp;<input type='radio' id='anw2' name='anws' value='1' checked='checked' />正确 ";
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 保存事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        string[] cont = GetContent();
        bq = new B_Exam_Sys_Questions();
        int id = DataConverter.CLng(hfpid.Value);
        M_Exam_Sys_Questions questions = bq.GetSelect(id);
        questions.p_Shipin = hffilename.Value;
        questions.p_shuming = this.shuming.Text;
        questions.p_Content = this.txtP_Content.Text;
        questions.p_ChoseNum = DataConverter.CLng(ddlNumber1.SelectedValue);
        questions.p_defaultScores = DataConverter.CFloat(txtCourse.Text);
        if (cont != null && cont.Length > 0)
            questions.p_Answer = cont[1];
        if (cont != null && cont.Length > 0) { SafeSC.WriteFile(M_Exam_Sys_Questions.OptionDir + id + ".opt", cont[0]); }
            //questions.p_Optioninfo = cont[0];
        if (id > 0) //修改
        {
            bool result = bq.GetUpdate(questions);
            if (result)
            {
                function.WriteSuccessMsg("修改成功!");
                Response.Write("<script>window.close();</script>");
            }
            else
            {
                function.WriteErrMsg("修改失败!");
            }
        }
        else  //添加
        {
            questions.p_Type = DataConverter.CLng(ddlType.SelectedValue);
            questions.p_Inputer = badmin.GetAdminLogin().AdminName; //当前登录ID
            questions.p_CreateTime = DateTime.Now.Date;
            questions.p_Views = 1;
            questions.parentId = DataConverter.CLng(hfParentId.Value);
            int result = bq.GetInsert(questions);
            if (result > 0)
            {
                Response.Write("<script>alert('添加成功!');window.close();</script>");
            }
            else
            {
                function.WriteErrMsg("添加失败!");
            }
        }
    }

    /// <summary>
    /// 获取内容
    /// </summary>
    /// <returns></returns>
    private string[] GetContent()
    {
        string[] result = new string[2];
        int type = DataConverter.CLng(ddlType.SelectedValue);
        M_Exam_Type mqt = bqt.GetSelectById(type);
        int num = DataConverter.CLng(ddlNumber1.SelectedValue);
        //单选,多选,判断，填空,问答，组合
        switch (mqt.t_type)
        {
            case 1:
                result[0] = Request.Form["option"];
                result[1] = Request.Form["anws"];
                break;
            case 2:
                result[0] = Request.Form["option"];
                result[1] = Request.Form["anws"];
                break;
            case 3:
                result[0] = "";
                result[1] = Request.Form["anws"];
                break;
            case 4:
                result[0] = "";
                result[1] = Request.Form["amws"] + "&" + Request.Form["curse"];
                break;
            case 5:
                result[0] = "";
                result[1] = Request.Form["option"] + "&" + Request.Form["key"] + "&" + Request.Form["course"];
                break;
            case 6:
                break;
        }
        return result;
    }

    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEngLishQuestion.aspx?Id=" + hfParentId.Value);
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Tips.Text = "<font color='red'>请输入个数</font>";
        int type = DataConverter.CLng(ddlType.SelectedValue);
        tip.Visible = false;
        M_Exam_Type mqt = bqt.GetSelectById(type);
        //单选,多选,判断，填空,问答，组合
        switch (mqt.t_type)
        {
            case 1:
                ddlNumber1.SelectedValue = "4";
                break;
            case 2:
                ddlNumber1.SelectedValue = "4";
                break;
            case 4:
                ddlNumber1.SelectedValue = "1";
                tip.Visible = true;
                break;
            default:
                break;
        }
        option();
    }

    /// <summary>
    /// 选择项数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlNumber1_SelectedIndexChanged(object sender, EventArgs e)
    {
        option();
    }
}