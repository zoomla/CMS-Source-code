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
using System.Data.SqlClient;

public partial class User_Pages_RegProUser : System.Web.UI.Page
{
    protected B_User b_User = new B_User();
    protected B_Model mll = new B_Model();
    protected B_ModelField Fll = new B_ModelField();
    protected B_Content bll = new B_Content();
    protected B_PageStyle sll = new B_PageStyle();
    protected M_UserInfo UserInfo;
    protected string uname = string.Empty;
    protected int ModelID;
    protected int NodeID;
    protected DataTable Userpageinfo = null;
    protected DataTable cmdinfo = null;
    protected B_Sensitivity sell = new B_Sensitivity();
    protected B_Page pagell = new B_Page();

    protected void Page_Load(object sender, EventArgs e)
    {
        int selfmodeid = 0;
        string tophtml = "";
        string endhtml = "";
        //this.uname = buser.GetLogin().UserName;
        //this.LblUserName.Text = uname;
        this.Username.Text = b_User.GetLogin().UserName;
        int SelectStyleid = 0;
        M_UserInfo info = b_User.GetLogin();
        string TableNames = "";

        this.cmdinfo = Fll.SelectTableName("ZL_CommonModel", "TableName like 'ZL/_Reg/_%' escape '/' and Inputer='" + info.UserName + "'");
        if (this.cmdinfo.Rows.Count > 0)
        {
            TableNames = this.cmdinfo.Rows[0]["TableName"].ToString();
            if (!mll.IsExistTemplate(TableNames)) { function.WriteErrMsg("找不到系统黄页信息!请到后台创建用户模型"); }

            DataTable modeinfo = Fll.SelectTableName("ZL_Model", "TableName = '" + TableNames + "'");
            this.Userpageinfo = Fll.SelectTableName(TableNames, "UserName='" + info.UserName + "'");
        }

        DataTable styletable = mll.GetListPage();
        Styleids.DataSource = styletable;
        Styleids.DataValueField = "ModelID";
        Styleids.DataTextField = "ModelName";
        Styleids.DataBind();

        SelectStyleid = DataConverter.CLng(Request.Form["Styleids"]);//用户注册样式

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
        Styleid.DataSource = sll.GetPagestylelist();
        Styleid.DataValueField = "PageNodeid";
        Styleid.DataTextField = "PageNodeName";
        Styleid.DataBind();
        this.Styleid.Items.Insert(0, new ListItem("请选择样式", "0"));
        M_CommonData Utabs = bll.GetCommonData(pagell.GetSelectByUserID(info.UserID).CommonModelID);
        string userns = Utabs.Inputer;
        DataTable Mtables = Fll.SelectTableName(Utabs.TableName,"UserName = @uname", new SqlParameter[] { new SqlParameter("uname", userns) });
        try
        {
            string styid = Mtables.Rows[0]["Styleid"].ToString();
            Styleid.SelectedValue = styid;
        }
        catch (Exception)
        {
            Styleid.SelectedValue = "0";
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
            //Response.Write(this.ModelID);
            //Response.End();
            ModelHtml.Text = Fll.InputallHtml(ModelID, 0, new ModelConfig()
            {
                Source = ModelConfig.SType.UserBase
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
                    regpage.Visible = true;
                    Auditing.Visible = false;
                    int Umodeid = DataConverter.CLng(SelectStyleid);
                    int infopageid = DataConverter.CLng(this.Userpageinfo.Rows[0]["ID"]);
                    DataTable dt1 = Fll.SelectTableName(this.cmdinfo.Rows[0]["TableName"].ToString(), "ID = " + this.cmdinfo.Rows[0]["GeneralID"] + "");
                    Label1.Text = "● 修改您已经提交了企业黄页信息!";
                    Label2.Text = "修改企业黄页";

                    this.ModelID = Umodeid;
                    this.HdnModel.Value = this.ModelID.ToString();
                    this.HdnNode.Value = "0";
                    this.HdnID.Value = this.cmdinfo.Rows[0]["GeneralID"].ToString();

                    tophtml = "<table width=\"100%\"><tr><td width=\"100px\"></td><td width = \"80%\"></td></tr>";
                    endhtml = "</table>";

                    if (DataConverter.CLng(this.Styleids.Text) == selfmodeid)
                    {
                        int modelid = Umodeid == 0 ? selfmodeid : Umodeid;
                        ModelHtml.Text = tophtml + Fll.InputallHtml(modelid, NodeID, new ModelConfig()
                        {
                            ValueDT = Userpageinfo
                        }) + endhtml;
                    }
                    else
                    {
                        ModelHtml.Text =tophtml+ Fll.InputallHtml(ModelID, 0, new ModelConfig()
                        {
                            Source = ModelConfig.SType.UserBase
                        }) + endhtml;
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

                    this.ModelID = Umodeid;

                    this.HdnModel.Value = this.ModelID.ToString();
                    this.HdnNode.Value = "0";

                    this.HdnID.Value = this.cmdinfo.Rows[0]["GeneralID"].ToString();

                    //显示域名绑定
                    if (!IsPostBack)
                    {
                        M_Page pageinfo = pagell.GetSelectByUserID(info.UserID);
                        this.Proname.Text = pageinfo.Proname;
                        this.txt_logos.Text = pageinfo.LOGO;
                        this.PageInfo.Text = pageinfo.PageInfo;
                        this.SeDomain.Text = pageinfo.Domain;

                    }

                    tophtml = "<table width=\"100%\"><tr><td width=\"100px\"></td><td width = \"80%\"></td></tr>";
                    endhtml = "</table>";

                    //this.ModelHtml.Text = tophtml + this.Fll.GetUpdateHtmlUser(Umodeid, 0, Userpageinfo) + endhtml;

                    if (DataConverter.CLng(this.Styleids.Text) == selfmodeid)
                    {
                        int modelid = Umodeid == 0 ? selfmodeid : Umodeid;
                        ModelHtml.Text = tophtml+Fll.InputallHtml(modelid, 0, new ModelConfig()
                        {
                            ValueDT = Userpageinfo
                        })+endhtml;
                    }
                    else
                    {
                        ModelHtml.Text = tophtml+Fll.InputallHtml(ModelID, 0, new ModelConfig()
                        {
                            Source = ModelConfig.SType.UserBase
                        }) + endhtml;
                    }

                }
                else
                {
                    //Label1.Text = "● 您已经提交了企业黄页信息!";
                    //regpage.Visible = false;
                    //Auditing.Visible = true; 
                    Response.Redirect("Default.aspx");
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

                    this.HdnID.Value = this.cmdinfo.Rows[0]["GeneralID"].ToString();

                    M_Page pageinfo = pagell.GetSelectByUserID(info.UserID);
                    this.Proname.Text = pageinfo.Proname;
                    this.txt_logos.Text = pageinfo.LOGO;
                    this.PageInfo.Text = pageinfo.PageInfo;
                    this.SeDomain.Text = pageinfo.Domain;

                    tophtml = "<table width=\"100%\"><tr><td width=\"100px\"></td><td width = \"80%\"></td></tr>";
                    endhtml = "</table>";
                    DataTable tbinfo = Fll.SelectTableName(TableNames, "UserName = @uname", new SqlParameter[] { new SqlParameter("uname", uname) });
                    if (DataConverter.CLng(this.Styleids.Text) == selfmodeid&&tbinfo.Rows.Count > 0)
                    {
                        ModelHtml.Text = tophtml + Fll.InputallHtml(Umodeid, 0, new ModelConfig()
                        {
                            ValueDT = Userpageinfo
                        }) + endhtml;
                    }
                    else
                    {
                        ModelHtml.Text = tophtml + Fll.InputallHtml(DataConverter.CLng(this.Styleids.Text), NodeID, new ModelConfig()
                        {
                            Source = ModelConfig.SType.UserBase
                        }) + endhtml;
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

    /// <summary>
    /// 提交黄页信息
    /// </summary>
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        //提交黄页信息
        if (this.Page.IsValid)
        {
            #region
            string tempurl = Request.Form["templateUrl"];
            bll.UpTemplata(DataConverter.CLng(this.HdnID.Value), tempurl);
            #endregion

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

            #region 扩展表
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));

            foreach (DataRow dr in dt.Rows)
            {
                if (DataConverter.CBool(dr["IsNotNull"].ToString()))
                {
                    if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
                    {
                        function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
                    }
                }
                if (dr["FieldType"].ToString() == "FileType")
                {
                    string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                    bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                    string sizefield = Sett[1].Split(new char[] { '=' })[1];
                    if (chksize && sizefield != "")
                    {
                        DataRow row2 = table.NewRow();
                        row2[0] = sizefield;
                        row2[1] = "FileSize";
                        row2[2] = this.Page.Request.Form["txt_" + sizefield];
                        table.Rows.Add(row2);
                    }
                }
                if (dr["FieldType"].ToString() == "MultiPicType")
                {
                    string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                    bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                    string sizefield = Sett[1].Split(new char[] { '=' })[1];
                    if (chksize && sizefield != "")
                    {
                        if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + sizefield]))
                        {
                            function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                        }
                        DataRow row1 = table.NewRow();
                        row1[0] = sizefield;
                        row1[1] = "ThumbField";
                        row1[2] = this.Page.Request.Form["txt_" + sizefield];
                        table.Rows.Add(row1);
                    }
                }
                DataRow row = table.NewRow();
                row[0] = dr["FieldName"].ToString();
                string ftype = dr["FieldType"].ToString();
                row[1] = ftype;
                string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
                if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
                {
                    fvalue = sell.ProcessSen(fvalue);
                }
                row[2] = fvalue;
                table.Rows.Add(row);
            }


            string uname = b_User.GetLogin().UserName;
            this.UserInfo = b_User.GetLogin();

            DataRow rs1 = table.NewRow();
            rs1[0] = "UserID";
            rs1[1] = "int";
            rs1[2] = this.UserInfo.UserID;
            table.Rows.Add(rs1);

            DataRow rs2 = table.NewRow();
            rs2[0] = "UserName";
            rs2[1] = "TextType";
            rs2[2] = UserInfo.UserName;
            table.Rows.Add(rs2);

            DataRow rs3 = table.NewRow();
            //Styleid|黄页样式ID|数字|0
            rs3[0] = "Styleid";
            rs3[1] = "int";
            rs3[2] = Convert.ToInt32(Styleid.SelectedValue); //sll.GetDefaultStyle().PageNodeid;
            table.Rows.Add(rs3);
            #endregion

            M_CommonData CData = new M_CommonData();
            if (HdnID > 0)
            {
                int Commandid = DataConverter.CLng(Request.Form["HdnID"]);
                CData = bll.GetCommonData(Commandid);
            }

            CData.NodeID = 0;
            // CData.ModelID = this.ModelID;
            if (this.cmdinfo.Rows.Count > 0)
            {
                CData.GeneralID = DataConverter.CLng(this.cmdinfo.Rows[0]["GeneralID"]);
            }
            else
            {
                CData.GeneralID = 0;
            }
            CData.ModelID = DataConverter.CLng(Request.Form["Styleids"].ToString());
            CData.TableName = this.mll.GetModelById(DataConverter.CLng(Request.Form["Styleids"])).TableName;

            CData.Title = this.UserInfo.UserName + "的黄页信息";
            CData.Inputer = this.UserInfo.UserName;
            CData.EliteLevel = 0;
            CData.TagKey = "";
            if (SiteConfig.SiteOption.RegPageStart)
            {
                CData.Status = 0;
            }
            else
            {
                CData.Status = 99;
            }
            //CData.Template = "";
            CData.SpecialID = "";
            CData.PdfLink = "";
            CData.Titlecolor = "";
            //CData.Template = pagetemplate;
            int styless = DataConverter.CLng(Request.Form["Styleids"]);
            M_Page pinfo = pagell.GetSelectByUserID(this.UserInfo.UserID);
            if (HdnID > 0 && pinfo != null && pinfo.ID > 0)
            {
                this.bll.UpdateContent(table, CData);
                DataTable pageinfoss = Fll.SelectTableName(CData.TableName, " Username='" + this.UserInfo.UserName + "'");
                int fileid = 0;
                if (pageinfoss != null && pageinfoss.Rows.Count > 0)
                {
                    fileid = DataConverter.CLng(pageinfoss.Rows[0]["id"]);
                }
                pinfo.Proname = this.Proname.Text;
                pinfo.UserName = this.UserInfo.UserName;
                pinfo.UserID = this.UserInfo.UserID;
                pinfo.TableName = this.mll.GetModelById(DataConverter.CLng(Request.Form["Styleids"])).TableName;
                if (SiteConfig.SiteOption.RegPageStart)
                {
                    pinfo.Status = 0;
                }
                else
                {
                    pinfo.Status = 99;
                }
                pinfo.PageInfo = this.PageInfo.Text;
                pinfo.Domain = SeDomain.Text;
                pinfo.LOGO = this.txt_logos.Text;
                if (pinfo.CreateTime.ToString().Trim() == "0001/1/1 0:00:00")
                {
                    pinfo.CreateTime = DateTime.Now;
                }
                //pinfo.InfoID = fileid;
                pinfo.CommonModelID = CData.GeneralID;
                pagell.GetUpdate(pinfo);
                Response.Write("<script language=javascript>alert('修改提交成功!');location.href='RegProUser.aspx';</script>");
            }
            else
            {
                int newID = this.bll.AddContent(table, CData);
                M_CommonData cdata = bll.GetCommonData(newID);

                pinfo = new M_Page();
                pinfo.ID = newID;
                pinfo.Proname = this.Proname.Text;
                pinfo.UserName = this.UserInfo.UserName;
                pinfo.UserID = this.UserInfo.UserID;
                pinfo.TopWords = "";
                pinfo.TableName = this.mll.GetModelById(DataConverter.CLng(Request.Form["Styleids"])).TableName;
                pinfo.Style = 0;
                if (SiteConfig.SiteOption.RegPageStart)
                {
                    pinfo.Status = 0;
                }
                else
                {
                    pinfo.Status = 99;
                }
                pinfo.ParentUserID = 0;
                pinfo.ParentPageID = 0;
                pinfo.PageTitle = "";
                pinfo.PageInfo = this.PageInfo.Text;
                pinfo.LOGO = this.txt_logos.Text;
                pinfo.KeyWords = "";
                pinfo.InfoID = cdata.ItemID;
                pinfo.NodeStyle = Convert.ToInt32(Styleid.SelectedValue);// sll.GetDefaultStyle().PageNodeid;
                pinfo.Hits = 0;
                pinfo.HeadHeight = 0;
                pinfo.HeadColor = "";
                pinfo.Domain = SeDomain.Text;
                pinfo.Description = "";
                pinfo.CreateTime = DateTime.Now;
                pinfo.CommonModelID = newID;
                pinfo.BottonWords = "";
                pinfo.Best = 0;
                pagell.GetInsert(pinfo);
                Response.Write("<script language=javascript>alert('申请提交成功!');location.href='RegProUser.aspx';</script>");
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegProUser.aspx?menu=modifile");
    }
}