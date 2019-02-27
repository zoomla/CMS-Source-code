using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Common.Addon;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Content
{
    public partial class EditContent : CustomerPageAction
    {
        B_Node bnode = new B_Node();
        B_Model bmode = new B_Model();
        B_ModelField fieldBll = new B_ModelField();
        B_Content contentBll = new B_Content();
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        B_Spec specbll = new B_Spec();
        B_Content_WordChain wordBll = new B_Content_WordChain();
        B_Content_ScheTask taskBll = new B_Content_ScheTask();
        B_KeyWord keyBll = new B_KeyWord();
        List<string> content = new List<string>();
        protected bool createnew = true;
        public string keys = "";
        //----工作流
        B_Process proBll = new B_Process();
        M_Process proMod = new M_Process();
        //-----------
        public int NodeID
        {
            get
            {
                return DataConvert.CLng(ViewState["NodeID"]);
            }
            set { ViewState["NodeID"] = value; }
        }
        public int ModelID
        {
            get
            {
                return DataConvert.CLng(ViewState["ModelID"]);
            }
            set { ViewState["ModelID"] = value; }
        }
        public int GeneralID { get { return Convert.ToInt32(Request.QueryString["GeneralID"]); } }
        public string NodeDir { get { return ViewState["NodeDir"] as string; } set { ViewState["NodeDir"] = value; } }
        private string pdfLink = SiteConfig.SiteOption.UploadDir + "PDF/";
        private string wordLink = SiteConfig.SiteOption.UploadDir + "Word/";
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
                M_CommonData Cdata = contentBll.GetCommonData(GeneralID);
                if (Cdata == null) { function.WriteErrMsg("请指定内容参数"); }
                //计划任务(审核时间)
                //M_Content_ScheTask taskmod = taskBll.SelByGid(GeneralID, M_Content_ScheTask.TaskTypeEnum.AuditArt);
                //if (taskmod != null)
                //{
                //    CheckDate_T.Text = taskmod.ExecuteTime2.ToString();
                //    CheckDate_Hid.Value = taskmod.ExecuteTime2.ToString();
                //}
                //taskmod = taskBll.SelByGid(GeneralID, M_Content_ScheTask.TaskTypeEnum.UnAuditArt);
                //if (taskmod!=null)
                //{
                //    TimeDate_T.Text = taskmod.ExecuteTime2.ToString();
                //    TimeDate_Hid.Value = taskmod.ExecuteTime2.ToString();
                //}
                M_ModelInfo model = bmode.GetModelById(Cdata.ModelID);
                ModelID = Cdata.ModelID;
                NodeID = Cdata.NodeID;
                DataTable fieldDT = fieldBll.SelByModelID(ModelID, true);
                bt_txt.Text = B_Content.GetFieldAlias("Title", fieldDT) + "：";
                gjz_txt.Text = B_Content.GetFieldAlias("Tagkey", fieldDT) + "：";
                SubTitle_L.Text = B_Content.GetFieldAlias("Subtitle", fieldDT) + "：";
                tj_txt.Text = B_Content.GetFieldAlias("EliteLevel", fieldDT);
                zht_txt.Text = B_Content.GetFieldAlias("Status", fieldDT);
                hits_txt.Text = B_Content.GetFieldAlias("Hits", fieldDT);
                RelatedIDS_Hid.Value = Cdata.RelatedIDS;
                AddToNew_Btn.Text = "添加为新" + model.ItemName;
                EBtnSubmit.Text = "保存" + model.ItemName;
                //-----工作流,检测该节点是否绑定工作流，如无绑定，则直接通过,未绑定,则以第一步为准
                ddlFlow.DataSource = proBll.SelByNodeID2(NodeID);
                ddlFlow.DataTextField = "PName";
                ddlFlow.DataValueField = "PPassCode";
                ddlFlow.DataBind();
                ddlFlow.SelectedValue = Cdata.Status.ToString().Equals("-3") || string.IsNullOrEmpty(Cdata.Status.ToString()) ? "0" : Cdata.Status.ToString();
                //-----工作流(End)
                //-----智能关键词
                keys = keyBll.SelToJson();
                ModelID = Cdata.ModelID;
                M_Node nnn = bnode.GetNodeXML(NodeID);
                if (nnn.Contribute != 1)
                {
                    function.Script(this, "ShowSys();");
                }
                NodeDir = nnn.NodeDir;
                CreateHTML.Visible = nnn.ListPageHtmlEx < 3;
                nodename.Value = nnn.NodeName;
                EBtnSubmit.Text = "修改" + bmode.GetModelById(ModelID).ItemName;
                txtTitle.Text = Cdata.Title;
                ThumImg_Hid.Value = Cdata.TopImg;
                txtAddTime.Text = Cdata.CreateTime == DateTime.MinValue ? "" : Cdata.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                txtInputer.Text = Cdata.Inputer;
                txtdate.Text = Cdata.UpDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                txtNum.Text = Cdata.Hits.ToString();
                ChkAudit.Checked = Cdata.EliteLevel > 0;
                TxtTemplate_hid.Value = Cdata.Template;
                TlpView_Tlp.SelectedID = Cdata.Template;
                TlpView_Tlp.SelectedValue = Cdata.Template;
                Keywords.Text = Cdata.TagKey;
                Subtitle.Text = Cdata.Subtitle;
                PYtitle.Text = Cdata.PYtitle;
                ThreadStyle.Value = Cdata.TitleStyle.ToString();
                txtTitle.Style.Value += Cdata.TitleStyle.ToString();
                IsComm_Radio.SelectedValue = Cdata.IsComm.ToString();
                #region 专题
                if (specbll.GetSpecList().Rows.Count > 0)
                { SpecInfo_Li.Text = "<button type='button' class='btn btn-info btn-sm' onclick='ShowSpDiag()'>添加至专题</button>"; }
                else
                { SpecInfo_Li.Text = "<div style='margin:5px;' class='btn btn-default disabled'><span class='fa fa-info-circle'></span> 尚未定义专题</div>"; }
                Spec_Hid.Value = Cdata.SpecialID;
                if (!string.IsNullOrEmpty(Cdata.SpecialID))
                {
                    string ids = Cdata.SpecialID.Trim(',');
                    DataTable dtspecs = specbll.SelByIDS(ids);
                    string names = "";
                    if (dtspecs != null)
                    {
                        foreach (DataRow item in dtspecs.Rows)
                        {
                            names += "{\"id\":\"" + item["SpecID"] + "\",\"name\":\"" + item["SpecName"] + "\"},";
                        }
                    }
                    names = names.TrimEnd(',');
                    function.Script(this, "DealResult('[" + names + "]');");
                }
                #endregion
                DataTable dtContent = contentBll.GetContent(GeneralID);
                ModelHtml.Text = fieldBll.InputallHtml(ModelID, NodeID, new ModelConfig()
                {
                    ValueDT = dtContent
                });//模型内容
                   //检测是否已生成PDF或Html
                if (File.Exists(Server.MapPath(Cdata.PdfLink)))
                {
                    makePDFBtn.Enabled = false;
                    downPdfHref.Text += Cdata.GeneralID + ".pdf";
                    delPdfHref.Visible = true;
                    downPdfHref.Visible = true;
                }
                if (File.Exists(Server.MapPath(wordLink + Cdata.GeneralID + ".doc")))
                {
                    makeWordBtn.Enabled = false;
                    downWordHref.Text += Cdata.GeneralID + ".doc";
                    downWordHref.Visible = true;
                    delWordHref.Visible = true;
                }
                string breadTlp = "<li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='ContentManage.aspx?NodeID=" + nnn.NodeID + "'>" + nnn.NodeName + "</a></li><li class='active'>";
                breadTlp += "[向本栏目修改" + model.ItemName + "]";
                if (!string.IsNullOrEmpty(Cdata.Template))//启用个性模板
                {
                    breadTlp += "<span class='margin_l5 rd_red'>(提示:该内容已启用个性模板)</span>";
                }
                breadTlp += "</li>";
                breadTlp += "<div class='pull-right hidden-xs'><span><a href='" + customPath2 + "Content/SchedTask.aspx' title='查看计划任务'><span class='fa fa-clock-o' style='color:#28b462;'></span></a>" + GetOpenView() + "<span onclick=\"ShowDiag('EditNode.aspx?NodeID=" + NodeID + "','配置本节点');\" class='fa fa-cog' title='配置本节点' style='cursor:pointer;margin-left:5px;'></span></span></div>";
                Call.SetBreadCrumb(Master, breadTlp);
                BindTempList();
            }
        }
        //专题id|名称，
        private string SetSpecial(string specialid)
        {
            string sps = "";
            B_Spec Bs = new B_Spec();
            if (!string.IsNullOrEmpty(specialid))
            {
                string[] arr = specialid.Split(new char[] { ',' });
                for (int i = 0; i < arr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arr[i]))
                    {
                        M_Spec dd = Bs.GetSpec(DataConverter.CLng(arr[i]));
                        sps += arr[i] + "|" + dd.SpecName + ",";
                    }
                }
            }
            return sps;
        }
        private string GetParent(int ParentID)
        {
            string str = "";
            M_Node mn = bnode.GetNodeXML(ParentID);
            if (mn.ParentID > 0)
            {
                str = GetParent(mn.ParentID) + "&gt;&gt;" + mn.NodeName;
            }
            else
            {
                str = mn.NodeName;
            }
            return str;
        }
        //保存项目
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            M_CommonData model = contentBll.SelReturnModel(GeneralID);
            FillModel(ref table, model);
            //推送
            if (!string.IsNullOrEmpty(pushcon_hid.Value))
            {
                string[] nodeArr = pushcon_hid.Value.Trim(',').Split(',');
                for (int i = 0; i < nodeArr.Length; i++)
                {
                    model.NodeID = Convert.ToInt32(nodeArr[i]);
                    contentBll.AddContent(table, model);
                }
            }
            contentBll.UpdateContent(table, model);
            #region disuse
            //ZLLog.ToDB(ZLEnum.Log.content, new M_Log()
            // {
            //     UName = adminMod.AdminName,
            //     Source = Request.RawUrl,
            //     Action = "修改内容",
            //     Message = "标题:" + CData.Title + ",Gid:" + CData.GeneralID,
            //     Level = "edit"
            // });
            //if (!string.IsNullOrEmpty(Request.Form["HdnSpec"]))//专题
            //{
            //    string SpecID = Request.Form["HdnSpec"];// HdnSpec.Value;
            //    if (SpecID.EndsWith(","))
            //    {
            //        SpecID = SpecID.Substring(0, SpecID.LastIndexOf(","));
            //    }
            //}
            #endregion
            CreateHtmlDel createHtml = CreateHtmlFunc;
            createHtml.BeginInvoke(HttpContext.Current.Request, createnew, model, table, null, null);
            //if (nodeMod.ListPageHtmlEx < 3 && quickmake.Checked == true)
            //    iscreate = "1";
            Response.Redirect("ContentShow.aspx?gid=" + GeneralID + "&type=edit");
        }
        protected void AddToNew_Btn_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            M_CommonData model = contentBll.SelReturnModel(GeneralID);
            FillModel(ref table, model);
            model.GeneralID = 0; model.ItemID = 0;
            model.GeneralID = contentBll.AddContent(table, model);
            Response.Redirect("ContentShow.aspx?gid=" + model.GeneralID + "&type=add");
        }
        private void FillModel(ref DataTable table, M_CommonData CData)
        {
            M_AdminInfo adminMod = badmin.GetAdminLogin();
            //CData = contentBll.GetCommonData(GeneralID);
            NodeID = CData.NodeID;
            ModelID = CData.ModelID;
            DataTable dt = fieldBll.SelByModelID(CData.ModelID);
            Call commonCall = new Call();
            table = commonCall.GetDTFromPage(dt, Page, ViewState, content);
            CData.Title = txtTitle.Text;
            /*-----------可用智能判断模型与节点绑定-------------------*/
            M_Node nodeinfo = bnode.GetNodeXML(CData.NodeID);
            if (nodeinfo.ContentModel != "")
            {
                string ContentModel = "," + nodeinfo.ContentModel + ",";
                if (ContentModel.IndexOf("," + ModelID.ToString() + ",") == -1)
                {
                    nodeinfo.ContentModel = nodeinfo.ContentModel + "," + ModelID.ToString();
                    bnode.UpdateNode(nodeinfo);
                }
            }
            else
            {
                nodeinfo.ContentModel = ModelID.ToString();
                bnode.UpdateNode(nodeinfo);
            }
            /*---------------------------------------------*/
            CData.EliteLevel = ChkAudit.Checked ? 1 : 0;
            CData.InfoID = "";
            CData.Template = TxtTemplate_hid.Value;
            CData.Hits = DataConverter.CLng(txtNum.Text);
            CData.UpDateType = 2;
            CData.UpDateTime = DataConverter.CDate(txtdate.Text);
            CData.Hits = string.IsNullOrEmpty(txtNum.Text) ? 0 : DataConverter.CLng(txtNum.Text);
            if (!string.IsNullOrEmpty(txtAddTime.Text))
            {
                CData.CreateTime = DataConverter.CDate(txtAddTime.Text);
            }
            if (!string.IsNullOrEmpty(txtInputer.Text))
            {
                CData.Inputer = txtInputer.Text;
            }
            string OldKey = CData.TagKey;
            CData.TagKey = Request.Form["tabinput"];
            CData.Status = Convert.ToInt32(string.IsNullOrEmpty(ddlFlow.SelectedValue) ? "-3" : ddlFlow.SelectedValue);//-3为草稿状态
            CData.Subtitle = Subtitle.Text;
            CData.PYtitle = PYtitle.Text;
            string tree = "";
            CData.FirstNodeID = bnode.SelFirstNodeID(CData.NodeID, ref tree);
            CData.TitleStyle = ThreadStyle.Value;
            CData.ParentTree = tree;
            CData.TopImg = ThumImg_Hid.Value;//首页图片
            CData.SpecialID = Spec_Hid.Value;
            CData.RelatedIDS = RelatedIDS_Hid.Value;
            CData.IsComm = Convert.ToInt32(IsComm_Radio.SelectedValue);
            // 关键词
            {
                if (string.IsNullOrEmpty(CData.TagKey)) { keys = ""; }
                else { keys = StrHelper.RemoveRepeat(CData.TagKey.Split(','), IgnoreKey_Hid.Value.Split(',')); }
                if (!string.IsNullOrEmpty(keys))
                {
                    keyBll.AddKeyWord(keys, 1);
                }
            }
            ////修改计划任务(审核时间)
            //if (!CheckDate_Hid.Value.Equals(CheckDate_T.Text))
            //    UpdateSched(GeneralID, M_Content_ScheTask.TaskTypeEnum.AuditArt, CheckDate_T.Text);
            ////修改计划任务(过期时间)
            //if (!TimeDate_Hid.Value.Equals(TimeDate_T.Text))
            //    UpdateSched(GeneralID, M_Content_ScheTask.TaskTypeEnum.UnAuditArt, TimeDate_T.Text);
        }
        //更新计划任务
        public void UpdateSched(int gid, M_Content_ScheTask.TaskTypeEnum type, string datetext)
        {
            M_Content_ScheTask taskmod = taskBll.SelByGid(GeneralID, type);
            if (!string.IsNullOrEmpty(datetext))
            {
                if (taskmod == null)
                    taskmod = new M_Content_ScheTask();
                taskmod.TaskType = (int)type;
                taskmod.TaskContent = GeneralID.ToString();
                taskmod.ExecuteTime = datetext;
                taskmod.Status = 0;
                if (taskmod.ID > 0)
                { taskBll.Update(taskmod); }
                else
                { taskBll.Add(taskmod); }
            }
            else if (taskmod != null)
            {
                taskBll.Delete(taskmod.ID);
            }
        }
        //生成Html
        public delegate void CreateHtmlDel(HttpRequest r, bool c, M_CommonData cdate, DataTable table);
        public void CreateHtmlFunc(HttpRequest r, bool c, M_CommonData cdate, DataTable table)
        {
            M_Node nnn = bnode.GetNodeXML(NodeID);
            if (nnn.ListPageHtmlEx < 3 && quickmake.Checked == true)
            {
                B_Create CreateBll = new B_Create(r);
                if (c)
                {
                    CreateBll.createann(cdate.GeneralID.ToString());//发布内容页
                    CreateBll.CreateColumnByID(NodeID.ToString());//发布栏目页
                }
                CreateBll.CreatePageIndex();//发布首页
            }
            string[] strArr = fieldBll.GetIsChain(ModelID, 1).Split(',');//需要替换的字段
            for (int i = 0; i < strArr.Length; i++)
            {
                DataRow[] dr = table.Select("FieldName = '" + strArr[i] + "' ");
                if (dr != null && dr.Length > 0)
                    dr[0]["FieldValue"] = wordBll.RePlaceKeyWord(dr[0]["FieldValue"].ToString());
            }
            contentBll.UpdateContent(table, cdate);
        }
        //-----用于生成Word与PDF
        HtmlHelper htmlHelp = new HtmlHelper();
        protected void makePDFBtn_Click(object sender, EventArgs e)
        {
            M_CommonData CData = contentBll.GetCommonData(GeneralID);
            if (!Directory.Exists(Server.MapPath(pdfLink))) { Directory.CreateDirectory(Server.MapPath(pdfLink)); }
            string vpath = pdfLink + GeneralID + ".pdf";
            string html = GetContentHtml();
            PdfHelper.HtmlToPdf(html, "", vpath);
            CData.PdfLink = vpath;
            contentBll.UpdateByID(CData);
            Response.Redirect("EditContent.aspx?GeneralID=" + GeneralID + "&tab=1");
        }
        protected void delPdfBtn_Click(object sender, EventArgs e)
        {
            M_CommonData CData = contentBll.GetCommonData(GeneralID);
            pdfLink += GeneralID + ".pdf";
            File.Delete(Server.MapPath(pdfLink));
            CData.PdfLink = "";
            contentBll.UpdateByID(CData);
            Response.Redirect("EditContent.aspx?GeneralID=" + GeneralID + "&tab=1");
        }
        protected void downPdfBtn_Click(object sender, EventArgs e)
        {
            pdfLink += GeneralID + ".pdf";
            if (File.Exists(Server.MapPath(pdfLink)))
            {
                DownFile(Server.MapPath(pdfLink), GeneralID + ".pdf");
            }
        }
        protected void makeWordBtn_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Server.MapPath(wordLink))) { Directory.CreateDirectory(Server.MapPath(wordLink)); }
            string vpath = wordLink + GeneralID + ".doc";
            string html = GetContentHtml();
            OfficeHelper.W_HtmlToWord(html, vpath);
            Response.Redirect("EditContent.aspx?GeneralID=" + GeneralID + "&tab=1");
        }
        protected void delWordBtn_Click(object sender, EventArgs e)
        {
            wordLink += GeneralID + ".doc";
            File.Delete(Server.MapPath(wordLink));
            Response.Redirect("EditContent.aspx?GeneralID=" + GeneralID + "&tab=1");
        }
        protected void downWordBtn_Click(object sender, EventArgs e)
        {
            wordLink += GeneralID + ".doc";
            if (File.Exists(Server.MapPath(wordLink)))
            {
                DownFile(Server.MapPath(wordLink), GeneralID + ".doc");
            }
            Response.Redirect("EditContent.aspx?GeneralID=" + GeneralID + "&tab=1");
        }
        private string GetText(DataTable table)
        {
            if (table == null || table.Rows.Count < 1) { return ""; }
            if (table.Columns.Contains("content")) { return DataConvert.CStr(table.Rows[0]["content"]); }
            //否则返回第一个找到的字段html
            foreach (DataRow dr in table.Rows)
            {
                string ftype = dr["FieldType"].ToString();
                string fvalue = Page.Request.Form["txt_" + dr["FieldName"].ToString()];
                if (ftype == "MultipleHtmlType")
                {
                    return fvalue;
                }
            }
            return "";
        }
        private string GetContentHtml()
        {
            string html = "", conhtml = "";
            M_CommonData conMod = contentBll.SelReturnModel(GeneralID);
            DataTable dt = fieldBll.SelByModelID(conMod.ModelID);
            Call commonCall = new Call();
            DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState, content);
            conhtml = GetText(table);
            html = "<div style='text-align:center;font-size:30px;font-weight:bolder;'><h1>" + conMod.Title + "</h1></div>";
            html += "<div style='text-align:center;'>作者：" + conMod.Inputer + "</div>";
            html = html + conhtml;
            html = htmlHelp.ConvertImgUrl(html, SiteConfig.SiteInfo.SiteUrl);
            html = "<html><body>" + html + "</body></html>";
            return html;
        }
        //物理路径,回传文件名
        public void DownFile(string path, string downName)
        {
            SafeSC.DownFile(function.PToV(path), downName);
        }
        //-----------------------------------------------------
        public string GetOpenView()
        {
            string outstr = " <a href='/Item/" + GeneralID + ".aspx' target='_blank'><span class='fa fa-eye'></span></a>";
            return outstr;
        }
        public void BindTempList()
        {
            string pathdir = (AppDomain.CurrentDomain.BaseDirectory + ZoomLa.Components.SiteConfig.SiteOption.TemplateDir.Replace("/", @"\")).Replace(@"\\", @"\");
            if (!File.Exists(pathdir)) { return; }
            DataTable tables = FileSystemObject.GetDirectoryAllInfos(pathdir + "/" + Resources.L.内容页 + "/", FsoMethod.File);
            tables.Columns.Add("TemplatePic");//添加模板图片url
            tables.Columns.Add("TemplateID");
            tables.Columns.Add("TemplateUrl");
            for (int i = 0; i < tables.Rows.Count; i++)
            {
                tables.Rows[i]["rname"] = tables.Rows[i]["rname"].ToString().Replace(pathdir, "").Replace(@"\", "/").Substring(1);
                string filename = "内容页_" + Path.GetFileNameWithoutExtension(tables.Rows[i]["name"].ToString()) + ".jpg";
                tables.Rows[i]["TemplatePic"] = ZoomLa.Components.SiteConfig.SiteOption.TemplateDir + "/thumbnail/" + filename;
                tables.Rows[i]["TemplateID"] = tables.Rows[i]["rname"];
                tables.Rows[i]["TemplateUrl"] = tables.Rows[i]["rname"];
                if (!File.Exists(function.VToP(pathdir + "/thumbnail/" + filename)))
                {
                    tables.Rows.Remove(tables.Rows[i]);
                    i--;
                }
            }
            TlpView_Tlp.DataSource = tables;
            tables.DefaultView.RowFilter = "type=1 OR name LIKE '%.html'";
            TlpView_Tlp.DataBind();
        }
    }
}