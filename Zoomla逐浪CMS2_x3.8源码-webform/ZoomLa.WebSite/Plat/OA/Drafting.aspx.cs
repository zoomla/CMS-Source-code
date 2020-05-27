using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.Components;
using System.IO;

/*
 * 公文发送等的界面,增加发起人权限限制
 * 
 * 签章:PicUrl写在radio上,前台完成初始,绑定等方法,拖动end时位置存在 curPosD,
 */
public partial class MIS_ZLOA_Drafting : System.Web.UI.Page
{
    protected B_Group groupBll = new B_Group();
    protected B_Mis_Model bmis = new B_Mis_Model();
    protected B_MisProcedure proBll = new B_MisProcedure();
    protected B_OA_Document boa = new B_OA_Document();
    protected B_User buser = new B_User();
    protected B_UserPurview purBll = new B_UserPurview();
    protected M_MisProcedure proMod = new M_MisProcedure();
    protected M_UserInfo minfo = new M_UserInfo();
    protected M_OA_Document moa = new M_OA_Document();
    protected M_Mis_Model mmis = new M_Mis_Model();
    protected B_OA_FreePro freeBll = new B_OA_FreePro();
    protected OACommon oaCom = new OACommon();
    //签章
    protected B_OA_Sign signBll = new B_OA_Sign();
    public string key = "";//关键词
    private string oaVPath = SiteConfig.SiteOption.UploadDir+"OA/";
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (function.isAjax())
        {
            string Action = Request.Form["Action"];
            string result = "";
            if(Action=="Print")
            {
                string content = Request.Form["Content"];
                string img = Request.Form["Image"];
                Session["PrintCon"] = content;
                Session["PrintImg"] = img;
                result = "1";
            }
            else
            {
                string value = Request.Form["value"];
                mmis = bmis.SelReturnModel(Convert.ToInt32(value));
                result = oaCom.GetHolder(mmis, buser.GetLogin().GroupID.ToString());
            }
            Response.Clear(); Response.Write(result); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            if (!B_User_Plat.AuthCheck("P_OA_Send")) { function.WriteErrMsg("你当前没有起草公文的权限,请联系网络管理员!"); }
            TypeDataBind();
            DPDataBind();
            #region 签章初始化
            DataTable signDT = signBll.SelByUserID(buser.GetLogin().UserID);
            if (signDT != null && signDT.Rows.Count > 0)
            {
                SignRadioBind(signDT);
            }
            else
            {
                signTrRemind.Visible = true;
            }
            #endregion
            if (Request.QueryString["Edit"] == "1" && !string.IsNullOrEmpty(Request.QueryString["AppID"]))
            {
                saveBtn.Text = "修改";
                AddNewBtn.Visible = true;
                int id = DataConvert.CLng(Request.QueryString["AppID"]);
                M_MisProLevel freeMod = freeBll.SelByDocID(id);
                moa = boa.SelReturnModel(id);
                minfo = buser.SeachByID(moa.UserID);
                Title.Text = moa.Title;
                key = moa.Keywords;
                Keywords.Text = moa.Keywords;
                Secret.SelectedValue = moa.Secret.ToString();
                Urgency.SelectedValue = moa.Urgency.ToString();
                Importance.SelectedValue = moa.Importance.ToString();
                Type.SelectedValue = moa.Type.ToString();
                proDP.SelectedValue = moa.ProID.ToString();
                Content.Text = moa.Content;
                CreateTime.Text = moa.CreateTime.ToString("yyyy/MM/dd HH:mm:ss");
                Label1.Text = minfo.UserName;
                Label2.Text = groupBll.GetByID(minfo.GroupID).GroupName;
                if (freeMod != null)
                {
                    RUserID_Hid.Value = freeMod.ReferUser;
                    RUserName_Lab.Text = buser.GetUserNameByIDS(freeMod.ReferUser);
                    CUserID_Hid.Value = freeMod.CCUser;
                    CUserName_Lab.Text = buser.GetUserNameByIDS(freeMod.CCUser);
                }
                //附件相关,移除,不做检测
                //proMod = proBll.SelReturnModel(moa.ProID);
                //if (proMod.AllowAttach == 1)
                //{
                    upFileTR.Visible = true;
                //}

                if (!string.IsNullOrEmpty(moa.PublicAttach))
                {
                    hasFileData.Value = moa.PublicAttach;
                    string[] af = moa.PublicAttach.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string h = "";
                    for (int i = 0; i < af.Length; i++)
                    {
                        h += "<span class='disupFile'>";
                        h += GroupPic.GetShowExtension(GroupPic.GetExtName(af[i]));
                        h += "<a target='_blank' href=" + af[i] +">"+ af[i].Split('/')[(af[i].Split('/').Length - 1)] + "</a><a href='javascript:;' title='删除' onclick='delHasFile(\"" + af[i] + "\",this);' ><img src='/App_Themes/AdminDefaultTheme/images/del.png'/></a></span>";
                    }
                    hasFileTD.InnerHtml = h;
                }
                //签章,用于修改
                if (!string.IsNullOrEmpty(moa.SignID)&&signRadio.Items.Count>0)
                {
                    signRadio.SelectedValue = moa.SignID.Split(':')[0];
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "signInit", "InitPos('"+moa.SignID+"');", true);
                }
                DraftBtn.Visible = false;
                //-----检测是否已开始流程
                if (boa.IsApproving(id))//已开始,不允许修改 
                {
                    saveBtn.Visible = false;
                    DraftBtn.Visible = false;
                    clearBtn.Attributes.Add("disabled","disabled");
                    clearBtn.Text = "流程已开始,禁止修改";
                }
            }
            else
            {
                AddNewBtn.Visible = false;
                Label1.Text = buser.GetLogin().UserName;
                Label2.Text = groupBll.GetByID(buser.GetLogin().GroupID).GroupName;
                CreateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
    }
    //绑定所有下拉选框
    private void DPDataBind() 
    {
        DataTable dt=new DataTable();
        dt = proBll.SelByUser(buser.GetLogin().UserID,buser.GetLogin().GroupID);
        proDP.DataSource = dt;
        proDP.DataTextField = "ProcedureName";
        proDP.DataValueField = "ID";
        proDP.DataBind();
        proDP.Items.Insert(0,new ListItem("自由流程","0"));

        //机密等下拉选框绑定
        Secret.DataSource = OAConfig.StrToDic(OAConfig.Secret);
        Secret.DataValueField = "Key";
        Secret.DataTextField = "Value";
        Secret.DataBind();

        Urgency.DataSource = OAConfig.StrToDic(OAConfig.Urgency);
        Urgency.DataValueField = "Key";
        Urgency.DataTextField = "Value";
        Urgency.DataBind();

        Importance.DataSource = OAConfig.StrToDic(OAConfig.Importance);
        Importance.DataValueField = "Key";
        Importance.DataTextField = "Value";
        Importance.DataBind();
    }
    private void SignRadioBind(DataTable dt) 
    {
        signRadio.DataSource = dt;
        signRadio.DataValueField = "ID";
        signRadio.DataTextField = "SignName";
        signRadio.DataBind();
        for (int i = 0; i < signRadio.Items.Count; i++)
        {
            signRadio.Items[i].Attributes["picUrl"] = dt.Rows[i]["VPath"].ToString();
        }
        signRadio.Items.Insert(0, new ListItem("不使用签章", "-1"));
        signRadio.SelectedValue = "-1";
    }
    private void TypeDataBind()
    {
        DataTable dt = new DataTable();
        dt = bmis.SelByDocType(0);
        Type.DataSource = dt;
        Type.DataTextField = "ModelName";
        Type.DataValueField = "ID";
        Type.DataBind();
        Type.Items.Insert(0, new ListItem("请选择", "0"));
    }
    protected void clearBtn_Click(object sender, EventArgs e)
    {
        Secret.SelectedValue = "1";
        Urgency.SelectedValue = "1";
        Importance.SelectedValue = "1";
        Type.SelectedValue = "0";
        Title.Text = "";
        Keywords.Text = "";
        CreateTime.Text = "";
        Content.Text = "";
    }
    #region 已改为AJAX
    //protected void Type_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int modelID = DataConvert.CLng(Type.SelectedValue.ToString());
    //    if (modelID > 0)
    //    {
    //        mmis = bmis.SelReturnModel(modelID);
    //        Content.Text = mmis.ModelContent;
    //    }
    //    else
    //    {
    //        Content.Text = "";
    //    }
    //}
    //protected void proDP_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (proDP.SelectedIndex == 0) return;
    //    int proID = DataConvert.CLng(proDP.SelectedValue);
    //    proMod = proBll.SelReturnModel(proID);
    //    if (proMod.AllowAttach == 1)
    //    {
    //        upFileTR.Visible = true;
    //    }
    //}
    //保存
    #endregion
    protected void saveBtn_Click(object sender, EventArgs e)
    {
        int proID = DataConverter.CLng(proDP.SelectedValue);
        int id = 0;
        if (Request.QueryString["Edit"] == "1" && Request.QueryString["appID"] != "")
        {
            moa = boa.SelReturnModel(DataConvert.CLng(Request.QueryString["appID"]));
            id = moa.ID;
            FillMod(0,moa);
            boa.UpdateByID(moa);
        }
        else
        {
            moa = FillMod(0);
            id = boa.insert(moa);
        }
        CreateStep(id);
        Response.Redirect("ViewDrafting.aspx?ID=" + id);
    }
    
    //添加为新公文
    protected void AddNewBtn_Click(object sender, EventArgs e)
    {
        moa = FillMod(0);
        int id = boa.insert(moa);
        Response.Redirect("ViewDrafting.aspx?ID=" + id);
    }
    //草稿
    protected void DraftBtn_Click(object sender, EventArgs e)
    {
        moa = FillMod(-80);
        int id = boa.insert(moa);
        Response.Redirect("ViewDrafting.aspx?ID=" + id);
    }
    //-----Tools
    public string GetFileName(string fileName)
    {
        return function.GetFileName() + "." + fileName.Split('.')[(fileName.Split('.').Length - 1)] + "$" + fileName;
    }
    //保存上传的文件
    public string SaveFile()
    {
        oaVPath += DateTime.Now.ToString("yyyyMMddHHmmss") + "/";
        string oaPPath = Server.MapPath(oaVPath);
        string result = "";
        if (Request.Files.Count > 0)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files[i].ContentLength < 1) continue;//为空则不处理
                if (!Directory.Exists(oaPPath))
                {
                    Directory.CreateDirectory(oaPPath);
                }
                SafeSC.SaveFile(oaVPath, Request.Files[i], Path.GetFileName(Request.Files[i].FileName));
                result += oaVPath + Path.GetFileName(Request.Files[i].FileName) + ",";
            }
        }
        return result;
    }
    //填充模型
    public M_OA_Document FillMod(int status,M_OA_Document oaMod=null) 
    {
        if (oaMod == null) oaMod = new M_OA_Document();
        oaMod.UserID = buser.GetLogin().UserID;
        oaMod.Secret = (Secret.SelectedValue);
        oaMod.Urgency = (Urgency.SelectedValue);
        oaMod.Importance = (Importance.SelectedValue);
        oaMod.Urgency = (Urgency.SelectedValue);
        oaMod.Type = DataConverter.CLng(this.Type.SelectedValue);
        oaMod.Title = Title.Text;
        oaMod.Keywords = Keywords.Text;
        oaMod.CreateTime = DataConverter.CDate(CreateTime.Text);
        oaMod.Content = Content.Text;
        oaMod.Status = status;
        oaMod.ProID = DataConverter.CLng(proDP.SelectedValue);
        oaMod.Branch = groupBll.GetByID(buser.GetLogin().GroupID).GroupName;
        oaMod.CurStepNum = 0;
        oaMod.PublicAttach = SaveFile();
        
        if (signRadio.Items.Count > 0 && signRadio.SelectedIndex > 0)//0是不使用签章
        {
            oaMod.SignID = signRadio.SelectedValue+":"+curPosD.Value;
        }
        return oaMod;
    }
    public void CreateStep(int id)
    {
        M_MisProLevel freeMod = freeBll.SelByDocID(id);
        moa = boa.SelReturnModel(id);
        bool isUpdate = true;
        if (freeMod == null)
        {
            isUpdate = false;
            freeMod = new M_MisProLevel();
        }
        freeMod.ProID = 0;
        freeMod.stepNum = 1;
        freeMod.stepName = "自由流程第1步";
        freeMod.SendMan = buser.GetLogin().UserID.ToString();
        freeMod.ReferUser = RUserID_Hid.Value.Trim(',');
        freeMod.ReferGroup = "";
        freeMod.CCUser = CUserID_Hid.Value.Trim(',');
        freeMod.CCGroup = "";
        freeMod.HQoption = 1;
        freeMod.Qzzjoption = 0;
        freeMod.HToption = 2;
        freeMod.EmailAlert = "";
        freeMod.EmailGroup = "";
        freeMod.SmsAlert = "";
        freeMod.SmsGroup = "";
        freeMod.BackOption = id;
        freeMod.PublicAttachOption = 1;
        freeMod.PrivateAttachOption = 1;
        freeMod.Status = 1;
        freeMod.CreateTime = DateTime.Now;
        freeMod.Remind = moa.Title + "的自由流程";
        if (isUpdate)
        {
            freeBll.UpdateByID(freeMod);
        }
        else
        {
            freeBll.Insert(freeMod);
        }
    }
    public string GetEditor(string name)
    {
        if (SiteConfig.SiteOption.EditVer == "1")
            return "";
        else 
            return "var editor; setTimeout(function () { editor = UE.getEditor('" + name + "', { toolbars: [ [ '|', 'undo', 'redo', '|','bold', 'italic', 'underline', 'fontborder', 'strikethrough','superscript', 'subscript', 'removeformat', 'formatmatch', 'autotypeset', 'blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist','insertunorderedlist', '|','rowspacingtop', 'rowspacingbottom', 'lineheight', 'horizontal', 'spechars', 'snapscreen', 'touppercase', 'tolowercase', 'emotion', '|','fontfamily', 'fontsize', '|','directionalityltr', 'directionalityrtl', 'indent', '|','justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'imagenone', 'imageleft', 'imageright', 'imagecenter', '|','insertimage', 'insertvideo', 'music', 'attachment', 'map', 'gmap', '|','inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', '|']]});}, 300);";
    }
}