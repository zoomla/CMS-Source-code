using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.IO;
using ZoomLa.BLL.Page;
using ZoomLa.Model.Page;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

public partial class User_Pages_PageInfo : System.Web.UI.Page
{
    protected B_User b_User = new B_User();
    protected B_Model mll = new B_Model();
    protected B_ModelField Fll = new B_ModelField();
    protected B_Content bll = new B_Content();
    protected B_PageStyle sll = new B_PageStyle();

    protected string uname = string.Empty;
    protected int ModelID;
    protected int NodeID;
    protected DataTable Userpageinfo = null;
    protected DataTable cmdinfo = null;
    protected B_Sensitivity sell = new B_Sensitivity();
    //protected B_Page pagell = new B_Page();
    public B_PageReg b_PageReg = new B_PageReg();
    public M_PageReg m_PageReg = new M_PageReg();

    public int intState = 0;    //1、无申请记录；2、申请未审核；3、申请审核通过
    protected void Page_Load(object sender, EventArgs e)
    {
        M_UserInfo info = b_User.GetLogin();
        int selfmodeid = 0, SelectStyleid = 0;
        this.Username.Text = info.UserName;
        m_PageReg = b_PageReg.GetSelectByUserID(info.UserID);
        string TableNames = "";
        this.cmdinfo = Fll.SelectTableName("ZL_Pagereg", "TableName like 'ZL/_Reg/_%' escape '/' and UserID='" + info.UserID + "'");
        if (this.cmdinfo.Rows.Count > 0)
        {
            TableNames = this.cmdinfo.Rows[0]["TableName"].ToString();
            SafeSC.CheckDataEx(TableNames);
            if (!mll.IsExistTemplate(TableNames))
            {
                function.WriteErrMsg("找不到系统黄页信息!请到后台创建用户模型");
            }
            DataTable modeinfo = Fll.SelectTableName("ZL_Model", "TableName = '" + TableNames + "'");
            this.Userpageinfo = Fll.SelectTableName(TableNames, "UserID='" + info.UserID + "'");
        }
        if (!IsPostBack)
        {
            DataTable styletable = mll.GetListPage();
            Styleids.DataSource = styletable;
            Styleids.DataValueField = "ModelID";
            Styleids.DataTextField = "ModelName";
            Styleids.DataBind();
            SelectStyleid = DataConverter.CLng(Styleids.SelectedValue);//用户注册样式
            if (TableNames != "")
            {
                DataTable modetb = Fll.SelectTableName("ZL_Model", "TableName ='" + TableNames + "'");
                if (modetb.Rows.Count > 0)
                {
                    selfmodeid = DataConverter.CLng(modetb.Rows[0]["ModelID"].ToString());
                }
            }
            if (SelectStyleid > 0)//选择用户注册样式
            {
                this.Styleids.Text = SelectStyleid.ToString();
            }
            else//默认用户注册样式
            {
                if (selfmodeid > 0)
                {
                    this.Styleids.Text = selfmodeid.ToString();
                }
                else
                {
                    this.Styleids.SelectedIndex = 0;
                }
            }

            int reg = 0;
            int cmds = 0;
            int PageStatus = 0;
            if (this.cmdinfo.Rows.Count > 0)
            {
                reg = this.Userpageinfo.Rows.Count;
                cmds = this.cmdinfo.Rows.Count;
                PageStatus = 0;
                if (cmds == 0)
                {
                    PageStatus = 0;
                }
                else
                {
                    PageStatus = DataConverter.CLng(this.cmdinfo.Rows[0]["Status"]);
                }
            }
            #region 样式
            DataTable styledt = sll.GetPagestylelist();
            TlpView_Tlp.DataSource = styledt;
            TlpView_Tlp.DataBind();
            DataTable Uptab = b_PageReg.Sel("UserID=" + info.UserID, "");
            if (Uptab.Rows.Count > 0)
            {
                string userns = b_PageReg.GetSelectByUserID(info.UserID).UserName;
                DataTable Mtables = null;
                if (Mtables!=null)
                {
                    string styid = Mtables.Rows[0]["Styleid"].ToString();
                    TlpView_Tlp.SelectedID = styid;
                }
            }
            #endregion
            if (reg == 0 && cmds == 0 && PageStatus != 99)//注册黄页
            {
                regpage.Visible = true;
                Auditing.Visible = false;
                this.ModelID = DataConverter.CLng(this.Styleids.Text);
                this.HdnModel.Value = this.ModelID.ToString();
                this.HdnNode.Value = "0";
                this.HdnID.Value = "0";
                ModelHtml.Text = Fll.InputallHtml(ModelID, NodeID, new ModelConfig()
                {
                    Source = ModelConfig.SType.Admin
                });
            }
            else
            {
                if (reg > 0 && cmds > 0 && PageStatus != 99)//审核黄页
                {
                    #region 正在审核黄页
                    string menu = DataSecurity.FilterBadChar(Request.QueryString["menu"]);
                    if (menu == "modifile")
                    {
                        Styleids.Enabled = false;
                        regpage.Visible = true;
                        Auditing.Visible = false;
                        int Umodeid = DataConverter.CLng(Styleids.SelectedValue);
                        int infopageid = DataConverter.CLng(this.Userpageinfo.Rows[0]["ID"]);
                        Label1.Text = "● 修改您已经提交了企业黄页信息!";
                        Label2.Text = "修改企业黄页";
                        M_PageReg m_PageRega = b_PageReg.GetSelectByUserID(info.UserID);
                        this.Proname.Text = m_PageRega.CompanyName;
                        this.txt_logos.Text = m_PageRega.LOGO;
                        this.PageInfo.Text = m_PageRega.PageInfo;
                        TlpView_Tlp.SelectedID = m_PageReg.NodeStyle.ToString();
                        this.ModelID = Umodeid;
                        this.HdnModel.Value = this.ModelID.ToString();
                        this.HdnNode.Value = "0";
                        this.HdnID.Value = this.cmdinfo.Rows[0]["InfoID"].ToString();
                        if (DataConverter.CLng(this.Styleids.Text) == selfmodeid)
                        {
                            int modelid = Umodeid == 0 ? selfmodeid : Umodeid;
                            ModelHtml.Text = Fll.InputallHtml(modelid, 0, new ModelConfig()
                            {
                                ValueDT = Userpageinfo
                            });
                        }
                        else
                        {
                            ModelHtml.Text = Fll.InputallHtml(ModelID, 0, new ModelConfig()
                            {
                                Source = ModelConfig.SType.Admin
                            });
                        }
                    }
                    else
                    {
                        regpage.Visible = false;
                        Auditing.Visible = true;
                        Label1.Text = "● 您已经提交了企业黄页信息!";
                    }
                    #endregion
                }
                else if (reg > 0 && cmds > 0 && PageStatus == 99)//审核通过和注册
                {
                    #region 审核通过的用户(修改资料)
                    string menu = DataSecurity.FilterBadChar(Request.QueryString["menu"]);
                    if (menu == "modifile")
                    {
                        regpage.Visible = true;
                        Auditing.Visible = false;
                        int Umodeid = DataConverter.CLng(SelectStyleid);
                        int infopageid = DataConverter.CLng(this.Userpageinfo.Rows[0]["ID"]);
                        DataTable dt1 = this.Userpageinfo;
                        Label1.Text = "● 修改您已经提交了企业黄页信息!";// 　<font color=red>注意: 修改已经审核的黄页资料需要管理员<b>再次审核通过</b>,以确保信息安全!</font>";
                        Label2.Text = "修改企业黄页";
                        this.ModelID = DataConvert.CLng(Styleids.SelectedValue);
                        this.HdnModel.Value = this.ModelID.ToString();
                        this.HdnNode.Value = "0";
                        this.HdnID.Value = this.cmdinfo.Rows[0]["InfoID"].ToString();
                        //显示域名绑定
                        if (!IsPostBack)
                        {
                            M_PageReg m_PageRega = b_PageReg.GetSelectByUserID(info.UserID);
                            this.Proname.Text = m_PageRega.CompanyName;
                            this.txt_logos.Text = m_PageRega.LOGO;
                            this.PageInfo.Text = m_PageRega.PageInfo;
                            TlpView_Tlp.SelectedID = m_PageReg.NodeStyle.ToString();
                        }
                        if (DataConverter.CLng(this.Styleids.Text) == selfmodeid)
                        {
                            int modelid = Umodeid == 0 ? selfmodeid : Umodeid;
                            ModelHtml.Text = Fll.InputallHtml(modelid, NodeID, new ModelConfig()
                            {
                                ValueDT = Userpageinfo
                            });
                        }
                        else
                        {
                            ModelHtml.Text = Fll.InputallHtml(ModelID, 0, new ModelConfig()
                            {
                                Source = ModelConfig.SType.Admin
                            });
                        }
                    }
                    else
                    {
                        Label1.Text = "● 您已经提交了企业黄页信息!";
                        regpage.Visible = false;
                        Response.Redirect("/User/Pages/PageInfo.aspx?menu=modifile");
                    }
                    #endregion
                }
                else
                {
                    string menu = DataSecurity.FilterBadChar(Request.QueryString["menu"]);
                    if (menu == "modifile")
                    {
                        regpage.Visible = true;
                        Auditing.Visible = false;
                        int Umodeid = DataConverter.CLng(SelectStyleid);
                        if (Userpageinfo.Rows.Count > 0)
                        {
                            int infopageid = DataConverter.CLng(this.Userpageinfo.Rows[0]["ID"]);
                        }
                        DataTable dt1 = Fll.SelectTableName(this.cmdinfo.Rows[0]["TableName"].ToString(), "ID = " + this.cmdinfo.Rows[0]["GeneralID"] + "");
                        Label1.Text = "修改您已经提交了企业黄页信息!";
                        Label2.Text = "修改企业黄页";
                        this.ModelID = Umodeid;
                        this.HdnModel.Value = this.ModelID.ToString();
                        this.HdnNode.Value = "0";
                        this.HdnID.Value = this.cmdinfo.Rows[0]["InfoID"].ToString();
                        M_PageReg m_PageRega = b_PageReg.GetSelectByUserID(info.UserID);
                        this.Proname.Text = m_PageRega.CompanyName;
                        this.txt_logos.Text = m_PageRega.LOGO;
                        this.PageInfo.Text = m_PageRega.PageInfo;
                        TlpView_Tlp.SelectedID = m_PageReg.NodeStyle.ToString();
                        DataTable tbinfo = Fll.SelectTableName(TableNames, "UserID = '" + b_User.GetLogin().UserID + "'");
                        if (DataConverter.CLng(this.Styleids.Text) == selfmodeid && tbinfo.Rows.Count > 0)
                        {
                            ModelHtml.Text = Fll.InputallHtml(Umodeid, 0, new ModelConfig()
                             {
                                 ValueDT = Userpageinfo
                             });
                            
                        }
                        else
                        {
                            ModelHtml.Text = Fll.InputallHtml(DataConverter.CLng(this.Styleids.Text), 0, new ModelConfig()
                            {
                                Source = ModelConfig.SType.Admin
                            });
                        }
                    }
                    else
                    {
                        regpage.Visible = false;
                        Auditing.Visible = true;
                        Label1.Text = "您已经提交了企业黄页信息!";
                    }
                }
            }
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string tempurl = Request.Form["templateUrl"];
        M_UserInfo mu = b_User.GetLogin();
        bll.UpTemplata(DataConverter.CLng(this.HdnID.Value), tempurl);
        int HdnID = DataConverter.CLng(this.HdnID.Value);
        this.ModelID = DataConverter.CLng(this.HdnModel.Value);
        this.NodeID = DataConverter.CLng(this.HdnNode.Value);
        DataTable dt = this.Fll.GetModelFieldList(this.ModelID);
        M_ModelInfo dts = this.mll.GetModelById(this.ModelID);
        string pagetemplate = "";
        if (dts.ModelID > 0)
        {
            pagetemplate = dts.ContentModule;
        }
        else
        {
            pagetemplate = "";
        }
        Call commonCall = new Call();
        DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState);
        AddValues(table, mu);
        M_PageReg CData = new M_PageReg();
        if (HdnID > 0)
        {
            int Commandid = DataConverter.CLng(this.HdnID.Value);
            CData = b_PageReg.GetSelectByUserID(b_User.GetLogin().UserID);
        }
        if (this.cmdinfo.Rows.Count > 0)
        {
            CData.InfoID = Convert.ToInt32(this.cmdinfo.Rows[0]["InfoID"].ToString());
        }
        else
        {
            CData.InfoID = 0;
        }
        CData.TableName = this.mll.GetModelById(DataConverter.CLng(Styleids.SelectedValue)).TableName;
        CData.UserName = mu.UserName;
        if (SiteConfig.SiteOption.RegPageStart)
        {
            CData.Status = 0;
        }
        else
        {
            CData.Status = 99;
        }
        CData.NavColor = "";
        int styless = DataConverter.CLng(Styleids.SelectedValue);
        if (m_PageReg != null && m_PageReg.ID > 0)
        {
            DataTable pageinfoss = Fll.SelectTableName(CData.TableName, " Username='" + mu.UserName + "'");
            int fileid = 0;
            if (pageinfoss != null && pageinfoss.Rows.Count > 0)
            {
                fileid = DataConverter.CLng(pageinfoss.Rows[0]["id"]);
            }
            m_PageReg.CompanyName = Proname.Text.Trim();
            m_PageReg.PageTitle = mu.UserName + "的黄页信息";
            m_PageReg.UserName = mu.UserName;
            m_PageReg.UserID = mu.UserID;
            m_PageReg.TableName = this.mll.GetModelById(DataConverter.CLng(Styleids.SelectedValue)).TableName;
            m_PageReg.ModelID = DataConverter.CLng(Styleids.SelectedValue);
            if (SiteConfig.SiteOption.RegPageStart)
            {
                m_PageReg.Status = 0;
            }
            else
            {
                m_PageReg.Status = 99;
            }
            m_PageReg.PageInfo = this.PageInfo.Text.Trim();
            m_PageReg.LOGO = txt_logos.Text.Trim();
            m_PageReg.NodeStyle = DataConvert.CLng(TlpView_Tlp.SelectedID);
            m_PageReg.Template = TlpView_Tlp.SelectedValue;
            m_PageReg.CreationTime = DateTime.Now;
            bll.Page_Update(table, m_PageReg);
            function.WriteSuccessMsg("修改提交成功", "PageInfo.aspx");
        }
        else
        {
            M_PageReg m_PageReg2 = new M_PageReg();
            m_PageReg2.CompanyName = Proname.Text.Trim();
            m_PageReg2.UserName = mu.UserName;
            m_PageReg2.UserID = mu.UserID;
            m_PageReg2.TopWords = "";
            m_PageReg2.TableName = this.mll.GetModelById(DataConverter.CLng(Styleids.SelectedValue)).TableName;
            m_PageReg2.ModelID = DataConverter.CLng(Styleids.SelectedValue);
            m_PageReg2.Style = 0;
            if (SiteConfig.SiteOption.RegPageStart)
            {
                m_PageReg2.Status = 0;
            }
            else
            {
                m_PageReg2.Status = 99;
            }
            M_PageReg cdata = new M_PageReg();
            m_PageReg2.ParentPageUserID = 0;
            m_PageReg2.ParentPageID = 0;
            m_PageReg2.PageTitle = mu.UserName + "的黄页信息";
            m_PageReg2.PageInfo = PageInfo.Text.Trim();
            m_PageReg2.LOGO = txt_logos.Text.Trim();
            m_PageReg2.Keyword = "";
            m_PageReg2.InfoID = 0;
            m_PageReg2.NodeStyle = DataConvert.CLng(TlpView_Tlp.SelectedID);
            m_PageReg2.Template = TlpView_Tlp.SelectedValue;
            m_PageReg2.Hits = 0;
            m_PageReg2.NavHeight = "0";
            m_PageReg2.NavColor = "";
            m_PageReg2.Description = "";
            m_PageReg2.CreationTime = DateTime.Now;
            m_PageReg2.BottonWords = "";
            int newID = this.bll.Page_Add(table, m_PageReg2);
            function.WriteSuccessMsg("申请提交成功", "PageInfo.aspx");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageInfo.aspx?menu=modifile");
    }
    protected void Styleids_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ModelID = Convert.ToInt32(Styleids.SelectedValue);
        this.HdnModel.Value = Styleids.SelectedValue;
        ModelHtml.Text = Fll.InputallHtml(ModelID, 0, new ModelConfig()
        {
            Source = ModelConfig.SType.Admin
        });
    }
    //附加上UserID,StyleID等的值
    private DataTable AddValues(DataTable table, M_UserInfo mu)
    {
        DataRow rs1 = table.NewRow();
        rs1[0] = "UserID";
        rs1[1] = "int";
        rs1[2] = mu.UserID;


        DataRow rs2 = table.NewRow();
        rs2[0] = "UserName";
        rs2[1] = "TextType";
        rs2[2] = mu.UserName;

        DataRow rs3 = table.NewRow();
        //Styleid|黄页样式ID|数字|0
        rs3[0] = "Styleid";
        rs3[1] = "int";
        int styleid = DataConvert.CLng(TlpView_Tlp.SelectedID);
        rs3[2] = styleid == 0 ? 1 : styleid; //未选择,则默认第一个
        table.Rows.Add(rs1);
        table.Rows.Add(rs2);
        table.Rows.Add(rs3);
        return table;
    }

}