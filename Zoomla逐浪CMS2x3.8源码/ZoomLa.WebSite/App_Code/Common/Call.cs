using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public class Call
{
    public const string SplitStr = "------asdfasgasgwqtwgsadgavzvxcvadfvzx";
    public const string Boundary = "------asjdfohponzvnzcvapowtunzafadsfwt";
    //------3D平台,暂不用开
    //public static bool IsFirst=true;
    //public static List<Player> PlayerList = new List<Player>();
    /// <summary>
    /// 调用系统标签
    /// </summary>
    public static void Label(string ntext)
    {
        ZoomLa.BLL.B_CreateHtml dd = new ZoomLa.BLL.B_CreateHtml();
        System.Web.HttpContext.Current.Response.Write(dd.CreateHtml(ntext, 0, 0, 0));
    }
    public static string GetLabel(string ntext) 
    {
        return new B_CreateHtml().CreateHtml(ntext);
    }
    //--------------------站点属性获取
    public static string SiteName 
    {
        get 
        {
            return SiteConfig.SiteInfo.SiteName;
        }
    }
    /// <summary>
    /// 后台显示的Logo
    /// </summary>
    public static string Logo 
    {
        get 
        {
            return SiteConfig.SiteInfo.LogoAdmin;
        }
    }
    /// <summary>
    /// 前台等页面显示的Logo
    /// </summary>
    public static string LogoUrl
    {
        get 
        {
            return SiteConfig.SiteInfo.LogoUrl;
        }
    }
    //----缓存,避免对服务端频繁读取
    private static XmlDocument _bdXml = new XmlDocument();
    /// <summary>
    /// 背景XML
    /// </summary>
    public static XmlDocument BDXml
    {
        get
        {
            if (_bdXml == null || _bdXml.ChildNodes.Count < 1)
            {
                _bdXml.Load("http://code.z01.com/web/Images/BackGround.xml");
            }
            return _bdXml;
        }
    }
    /// <summary>
    /// 返回背景
    /// </summary>
    public static string GetRandomImg()
    {
        if (SiteConfig.SiteOption.SiteManageMode == 1) { return ""; }
        string result = ""; int index = 0;
        try
        {
            Random ra = new Random();
            index = ra.Next(BDXml.DocumentElement.ChildNodes.Count);
            result = BDXml.DocumentElement.ChildNodes[index].Attributes["Path"].Value;
        }
        catch{  }
        return result;
    }
    //------------------Appsetting
    /// <summary>
    /// 修改Appsetting中的配置项
    /// </summary>
    /// <param name="name">ShowedAD</param>
    /// <param name="value">true</param>
    public static void AppSettUpdate(string name, string value)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string ppath = function.VToP("/Config/AppSettings.config");
        xmlDoc.Load(ppath);
        XmlNode node = xmlDoc.SelectSingleNode("//appSettings/add[@key='" + name + "']");
        node.Attributes["value"].Value = value;
        xmlDoc.Save(ppath);
    }
    /// <summary>
    /// 获取指定项的值
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string AppSettValue(string name)
    {
        return System.Configuration.ConfigurationManager.AppSettings[name];
    }
    //-----------------实例化
    //分发给目标栏目
    protected B_Group groupBll = new B_Group();
    protected B_User buser = new B_User();
    protected B_Model bmode = new B_Model();
    protected B_Content conBll = new B_Content();
    public void AddContentToNode(M_OA_Document oaDoc, int NodeID, int ModelID)
    {
        DataTable table = new DataTable();
        table.Columns.Add(new DataColumn("FieldName", typeof(string)));
        table.Columns.Add(new DataColumn("FieldType", typeof(string)));
        table.Columns.Add(new DataColumn("FieldValue", typeof(string)));

        //手动完成赋值
        string[] fieldArr = { "Secret", "Urgency", "Importance", "Attach", "UserGroupT", "content" };
        for (int i = 0; i < fieldArr.Length; i++)
        {
            DataRow dr = table.NewRow();
            dr["FieldName"] = fieldArr[i];
            dr["FieldType"] = "TextType";
            table.Rows.Add(dr);
        }
        table.Rows[0]["FieldValue"] = OAConfig.StrToDic(OAConfig.Secret)[oaDoc.Secret.ToString()];
        table.Rows[1]["FieldValue"] = OAConfig.StrToDic(OAConfig.Urgency)[oaDoc.Urgency.ToString()];
        table.Rows[2]["FieldValue"] = OAConfig.StrToDic(OAConfig.Importance)[oaDoc.Importance.ToString()];
        table.Rows[3]["FieldValue"] = oaDoc.PublicAttach;
        table.Rows[4]["FieldValue"] = groupBll.GetByID(buser.GetLogin().GroupID).GroupName;
        table.Rows[5]["FieldType"] = "MultipleHtmlType";
        table.Rows[5]["FieldValue"] = oaDoc.Content;

        //将无法获取的值，手动写入table中

        M_CommonData CData = new M_CommonData();
        CData.NodeID = NodeID;
        CData.ModelID = ModelID;
        CData.TableName = bmode.GetModelById(ModelID).TableName;
        CData.Title =oaDoc.Title;
        //判断是否使用文件流程
        if (SiteConfig.SiteOption.ContentConfig == 1)
        {
            CData.Status = 0;
        }
        else
        {
            CData.Status = 99;
        }

        CData.Inputer = buser.GetLogin().UserName;
        CData.EliteLevel = 0;
        CData.InfoID = "";
        CData.SpecialID = "";
        CData.Hits = 0;
        CData.UpDateType = 2;
        CData.UpDateTime = DateTime.Now;
        //string Keyword = this.TxtTagKey.Text.Trim().Replace(" ", "|");
        CData.TagKey = oaDoc.Keywords;
        CData.Status = 99;//状态,如果需要审核后才能看到,这里做下逻辑
        CData.Titlecolor = "";
        CData.Template = "";
        CData.CreateTime = DateTime.Now;
        CData.ProWeek = 0;
        CData.Pronum = 0;
        CData.BidType = 0;
        CData.IsBid = (CData.BidType > 0) ? 1 : 0;
        CData.BidMoney = DataConverter.CDouble(0);
        CData.DefaultSkins = 0;
        CData.FirstNodeID = 0;//GetNo(NodeID)
        CData.TitleStyle = "" ;
        CData.ParentTree = conBll.GetParentTree(NodeID);//父级别树
        CData.TopImg = "";//首页图片
        CData.PdfLink = "";
        CData.Subtitle = "";
        CData.PYtitle = "";
        int newID = conBll.AddContent(table, CData);
    }
    public int AddContentToNode(M_OA_Document oaDoc,int NodeID,int ModelID,DataTable table) 
    {
        M_CommonData CData = new M_CommonData();
        CData.NodeID = NodeID;
        CData.ModelID = ModelID;
        CData.TableName = bmode.GetModelById(ModelID).TableName;
        CData.Title = oaDoc.Title;
        //判断是否使用文件流程
        CData.Status = 99;
        CData.Inputer = buser.GetLogin().HoneyName;
        CData.EliteLevel = 0;
        CData.InfoID = "";
        CData.SpecialID = "";
        CData.Hits = 0;
        CData.UpDateType = 2;
        CData.UpDateTime = DateTime.Now;
        //string Keyword = this.TxtTagKey.Text.Trim().Replace(" ", "|");
        CData.TagKey = oaDoc.Keywords;
        CData.Status = 99;//状态,如果需要审核后才能看到,这里做下逻辑
        CData.Titlecolor = "";
        CData.Template = "";
        CData.CreateTime = DateTime.Now;
        CData.ProWeek = 0;
        CData.Pronum = 0;
        CData.BidType = 0;
        CData.IsBid = 0;
        CData.BidMoney = DataConverter.CDouble(0);
        CData.DefaultSkins = 0;
        CData.FirstNodeID = 0;//GetNo(NodeID)
        CData.TitleStyle = "";
        CData.ParentTree = conBll.GetParentTree(NodeID);//父级别树
        CData.TopImg = "";//首页图片
        CData.PdfLink = "";
        CData.Subtitle = "";
        CData.PYtitle = "";
        int newid = conBll.AddContent(table, CData);
        oaDoc.ItemID = CData.ItemID;
        oaDoc.TableName = CData.TableName;
        oaDoc.UserName = CData.Inputer;
        return newid;
    }
    public static void SetBreadCrumb(MasterPage m,string html)
    {
        ((Literal)m.FindControl("navLabel")).Text = html;
    }
    public static void HideBread(MasterPage m) 
    {
        m.FindControl("BreadDiv").Visible = false;
    }
    /// <summary>
    /// 仅用于添加修改文章如AddContent等, DataTable dt = this.bfield.GetModelFieldList(this.ModelID).Tables[0];
    /// </summary>
    /// <param name="dt">模型字段列表</param>
    /// <param name="page">this.Page</param>
    /// <param name="viewState">ViewState</param>
    /// <param name="content">用于后台生成静态页</param>
    /// <param name="flag">是否需要检测空值</param>
    /// <param name="fields">如不为空,则非其中的字段不允许修改</param>
    public DataTable GetDTFromPage(DataTable dt,Page page, StateBag viewState,IList<string> content=null,bool flag=true,string fields="")
    {
        B_ModelField mfieldBll = new B_ModelField();
        dt.DefaultView.RowFilter = "IsCopy <>-1";
        dt = dt.DefaultView.ToTable();
        if (content == null) content = new List<string>();
        DataTable table = new DataTable();
        table.Columns.Add(new DataColumn("FieldName", typeof(string)));
        table.Columns.Add(new DataColumn("FieldType", typeof(string)));
        table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
        table.Columns.Add(new DataColumn("FieldAlias", typeof(string)));
        foreach (DataRow dr in dt.Rows)
        {
            DataRow fdr = table.NewRow();
            if (!string.IsNullOrEmpty(fields) && !("," + fields + ",").Contains("," + dr["FieldName"] + ","))
            {
                continue;
            }
            try
            {
                if (dr["FieldType"].ToString() == "DoubleDateType")
                {
                    fdr[0] = dr["FieldName"].ToString();
                    fdr[1] = "DoubleDateType";
                    DateTime ti1 = DataConverter.CDate(page.Request.Form["txt" + dr["FieldName"].ToString()]);
                    DateTime ti2 = DataConverter.CDate(page.Request.Form["txt_" + dr["FieldName"].ToString()]);
                    fdr[2] = ti1.ToShortDateString() + "|" + ti2.ToShortDateString();
                    fdr[3] = dr["FieldAlias"];
                    table.Rows.Add(fdr);
                }
                else
                {
                    if (DataConverter.CBool(dr["IsNotNull"].ToString()) && flag)
                    {
                        if (string.IsNullOrEmpty(page.Request.Form["txt_" + dr["FieldName"].ToString()]))
                        {
                            function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!", "javascript:history.go(-1);");
                        }
                    }
                    string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                    switch (dr["FieldType"].ToString())
                    {
                        case "FileType"://其与MultiPicType一样，都是读页面上的隐藏控件,而非<select>中的值,存在一个隐藏字段中
                            #region
                            //ChkFileSize=0,FileSizeField=,MaxFileSize=1024,UploadFileExt=rar|zip|docx|pdf:::是否保存文件大小,保存在大小的字段名,文件上传限制,上传后缀名限制
                            bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                            string sizefield = Sett[1].Split(new char[] { '=' })[1];
                            if (chksize && sizefield != "")
                            {
                                DataRow row1 = table.NewRow();
                                row1[0] = sizefield;
                                row1[1] = "FileSize";
                                row1[2] = page.Request.Form["txt_" + sizefield];
                                row1[3] = dr["FieldAlias"];
                                table.Rows.Add(row1);
                            }
                            //信息由JS控制放置入txt_中
                            DataRow rowr1 = table.NewRow();
                            rowr1[0] = dr["FieldName"];
                            rowr1[1] = "FileType";
                            rowr1[2] = page.Request.Form["txt_" + dr["FieldName"]];
                            rowr1[3] = dr["FieldAlias"];
                            table.Rows.Add(rowr1);
                            #endregion
                            break;
                        case "MultiPicType":
                            #region
                            //ChkThumb=0,ThumbField=,Warter=0,MaxPicSize=1024,PicFileExt=jpg|png|gif|bmp|jpeg:::是否保存缩略图地址,缩略点路径,水印,大小,后缀名限制
                            bool chkthumb = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                            string ThumbField = Sett[1].Split(new char[] { '=' })[1];
                            if (chkthumb && ThumbField != "")
                            {
                                DataRow row2 = table.NewRow();
                                if (DataConverter.CBool(dr["IsNotNull"].ToString()) && string.IsNullOrEmpty(page.Request.Form["txt_" + ThumbField]))
                                {
                                    function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                                }
                                row2[0] = ThumbField;
                                row2[1] = "ThumbField";
                                row2[2] = page.Request.Form["txt_" + ThumbField];
                                row2[3] = dr["FieldAlias"];
                                table.Rows.Add(row2);
                            }
                            {
                                DataRow rowr2 = table.NewRow();
                                rowr2[0] = dr["FieldName"];
                                rowr2[1] = "MultiPicType";
                                rowr2[2] = page.Request.Form["txt_" + dr["FieldName"]];
                                rowr2[3] = dr["FieldAlias"];
                                table.Rows.Add(rowr2);
                            }
                            #endregion
                            break;
                        case "TableField"://库选字段
                            fdr[0] = dr["FieldName"].ToString();
                            fdr[1] = "TableField";
                            string tableval = page.Request.Form["txt_" + dr["FieldName"].ToString()];
                            if (!string.IsNullOrEmpty(tableval))
                            {
                                string[] temparr = tableval.Split(',');
                                tableval = string.Join("$", temparr);
                            }
                            fdr[2] = tableval;
                            fdr[3] = dr["FieldAlias"];
                            table.Rows.Add(fdr);
                            break;
                        case "NumType":
                            #region
                            double value = DataConverter.CDouble(string.IsNullOrEmpty(page.Request.Form["txt_" + dr["FieldName"]]) ? "0" : page.Request.Form["txt_" + dr["FieldName"]]);//避免空字符串报错
                                                                                                                                                                                        //TitleSize=35,NumberType=1,DefaultValue=50,NumSearchType=0,NumRangType=1,NumSearchRang=0-100,NumLenght=0
                            string[] conArr = dr["Content"].ToString().Split(',');
                            string NumLenght = "f" + conArr[6].Split('=')[1];//小数点位数
                            if (conArr[4].Split('=')[1].Equals("1"))//指定了上限与下限检测,则检测
                            {
                                string range = conArr[5].Split('=')[1];
                                if (!string.IsNullOrEmpty(range) && range.Split('-').Length == 2)//范围不为空，并正确填了值，才做判断
                                {
                                    double minValue = Convert.ToDouble(range.Split('-')[0]);
                                    double maxValue = Convert.ToDouble(range.Split('-')[1]);
                                    if (value < minValue || value > maxValue) function.WriteErrMsg(dr["FieldAlias"] + "：的值不正确,必须大于" + minValue + ",并且小于" + maxValue);
                                }
                            }
                            fdr[0] = dr["FieldName"].ToString();
                            fdr[1] = "NumType";
                            fdr[2] = value.ToString(NumLenght);
                            fdr[3] = dr["FieldAlias"];
                            table.Rows.Add(fdr);
                            #endregion
                            break;
                        case "Images"://组图
                            fdr[0] = dr["FieldName"].ToString();
                            fdr[1] = "Images";
                            fdr[2] = (page.Request.Form["txt_" + dr["FieldName"].ToString()] ?? "").Trim(',');
                            table.Rows.Add(fdr);
                            break;
                        case "CameraType":
                            fdr[0] = dr["FieldName"].ToString();
                            fdr[1] = "CameraType";
                            fdr[2] = page.Request.Form[dr["FieldName"] + "_hid"].ToString();
                            table.Rows.Add(fdr);
                            break;
                        case "SqlType"://文件入库,图片入库,,,图片与文件base64存数据库，一个单独字段标识其
                            {
                                fdr[0] = dr["FieldName"];
                                fdr[1] = dr["FieldType"];
                                fdr[2] = page.Request.Form["FIT_" + dr["FieldName"]];
                                fdr[3] = dr["FieldAlias"];
                                table.Rows.Add(fdr);
                            }
                            break;
                        case "SqlFile":
                            {
                                fdr[0] = dr["FieldName"];
                                fdr[1] = dr["FieldType"];
                                fdr[2] = page.Request.Form["FIT_" + dr["FieldName"]];
                                fdr[3] = dr["FieldAlias"];
                                table.Rows.Add(fdr);
                            }
                            break;
                        case "MapType":
                            {
                                fdr[0] = dr["FieldName"].ToString();
                                fdr[1] = "MapType";
                                fdr[2] = page.Request.Form["txt_" + dr["FieldName"].ToString()];
                                fdr[3] = dr["FieldAlias"];
                                table.Rows.Add(fdr);
                            }
                            break;
                        case "autothumb":
                            {
                                FieldModel fieldMod = new FieldModel(dr["Content"].ToString());
                                string thumb = page.Request.Form["txt_" + dr["FieldName"].ToString()];
                                string topimg = page.Request.Form["ctl00$Content$ThumImg_Hid"];
                                thumb = ZoomLa.BLL.Helper.ThumbHelper.Thumb_Compress(topimg, thumb, fieldMod.GetValInt("width"), fieldMod.GetValInt("height"));
                                //如thumb为空,但topimg不为空,则生成缩图
                                fdr[0] = dr["FieldName"].ToString();
                                fdr[1] = "auththumb";
                                fdr[2] = thumb;
                                table.Rows.Add(fdr);
                            }
                            break;
                        case "api_qq_mvs":
                            {
                                fdr[0] = dr["FieldName"];
                                fdr[1] = dr["FieldType"];
                                fdr[2] = page.Request.Form["UpMvs_"+dr["FieldName"].ToString()];
                                fdr[3] = dr["FieldAlias"];
                                table.Rows.Add(fdr);
                            }
                            break;
                        default:
                            #region
                            string[] txtftype = "TextType,MultipleTextType,MultipleHtmlType,PullFileType".Split(',');//文本类型
                            DataRow row = table.NewRow();
                            row[0] = dr["FieldName"].ToString();
                            string ftype = dr["FieldType"].ToString();
                            row[1] = ftype;
                            string fvalue = page.Request.Form["txt_" + dr["FieldName"].ToString()];
                            if (!string.IsNullOrEmpty(fvalue) && (txtftype.Where(p => p.Equals(ftype)).ToArray().Length > 0))
                            {
                                if (ftype == "MultipleHtmlType")
                                {
                                    content.Add(fvalue);
                                }
                            }
                            switch (ftype)
                            {
                                case "Random":
                                    fvalue = page.Request.Form["txt_" + dr["FieldName"].ToString()];
                                    break;
                                case "MapType":
                                    //fvalue =;
                                    break;
                                case "Charts":
                                    if (page.Request.Form[dr["FieldName"].ToString()] == null || page.Request.Form[dr["FieldName"].ToString()] == "0")
                                    {
                                        fvalue = "0";
                                    }
                                    else
                                    {
                                        fvalue = page.Request.Form[dr["FieldName"].ToString()];
                                    }
                                    break;
                                case "SqlType":
                                    if (page.Request.Form["txt_" + dr["FieldName"].ToString()] == null || page.Request.Form["txt_" + dr["FieldName"].ToString()] == "")
                                    {
                                        fvalue = "";
                                    }
                                    else
                                    {
                                        fvalue = page.Request.Form["txt_" + dr["FieldName"].ToString()];
                                    }
                                    break;
                                case "SqlFile":
                                    if (page.Request.Form["txt_" + dr["FieldName"].ToString()] == null || page.Request.Form["txt_" + dr["FieldName"].ToString()] == "")
                                    {
                                        fvalue = "";
                                    }
                                    else
                                    {
                                        fvalue = page.Request.Form["txt_" + dr["FieldName"].ToString()];
                                    }
                                    break;

                            }
                            string strUrl = HttpContext.Current.Request.Url.Authority.ToString();
                            string serPath = page.Request.PhysicalApplicationPath;
                            if (SiteConfig.SiteOption.IsSaveRemoteImage)
                            {
                                //保存远程图片功能
                                if (dr["FieldName"].ToString() == "content")
                                {
                                    //获取img路径列表
                                    List<string> remoteImageList = mfieldBll.GetImgUrl(fvalue);
                                    fvalue = page.Request.Form["txt_content"];
                                    foreach (string str in remoteImageList)
                                    {
                                        string imgurl = page.Request.PhysicalApplicationPath + WaterModuleConfig.WaterConfig.imgLogo;
                                        //新图片名称
                                        string newImgname;
                                        string Patha;
                                        mfieldBll.SaveGz(str, out newImgname, out Patha, viewState["NodeDir"].ToString());
                                        string imgInfo = "";
                                        //保存远程图片,并获取图片详细名称
                                        if (str.Substring(0, 7) == "http://" && !str.Contains(strUrl) && !str.Contains(SiteConfig.SiteInfo.SiteUrl))
                                        {
                                            try
                                            {
                                                imgInfo = mfieldBll.downRemoteImg(page.Server.MapPath(Patha), str, newImgname, Patha, imgurl);
                                                fvalue = fvalue.Replace(str.ToString(), imgInfo);
                                            }
                                            catch (Exception)
                                            {
                                                imgInfo = str;
                                                fvalue = fvalue.Replace(str.ToString(), imgInfo);
                                            }
                                        }
                                    }
                                }
                            }
                            row[2] = fvalue;
                            row[3] = dr["FieldAlias"];
                            table.Rows.Add(row);
                            #endregion
                            break;
                    }
                }
            }
            catch (Exception ex) { function.WriteErrMsg("自定义字段[" + dr["FieldName"] + "(" + dr["FieldType"] + ")]出错,原因:" + ex.Message); }
        }
        return table;
    }

    /// <summary>
    /// 获取帮助信息
    /// </summary>
    public static string GetHelp(int id)
    {
        if (SiteConfig.SiteOption.IsOpenHelp == "1")
            return "<div id='help' class='pull-right'><a onclick=\"help_show('" + id + "')\" title='帮助'><i class='fa fa-question-circle'></i></a></div>";
        else
            return "";
    }

    //----------编辑器
    public static string GetUEditor(string txtid,int status=1) 
    {
        StringBuilder builder = new StringBuilder();
        switch (status)
        {
            case 1:
                builder.AppendLine("<script type=\"text/javascript\">");
                builder.AppendLine(@"setTimeout(function () {");
                builder.AppendLine(@"UE.getEditor('" + txtid + "', {" + BLLCommon.ueditorMin + "});");
                builder.AppendLine(@"}, 300);");
                builder.AppendLine(@"</script>");
                break;
            case 2:
                builder.AppendLine("<script type=\"text/javascript\">");
                builder.AppendLine(@"setTimeout(function () {UE.getEditor('" + txtid + "', {" + BLLCommon.ueditorMid + "});");
                builder.AppendLine(@"}, 300);");
                builder.AppendLine(@"</script>");
                break;
            case 3:
                builder.AppendLine(@"<script type=""text/javascript"">");
                builder.AppendLine(@"setTimeout(function () {UE.getEditor('" + txtid + "');");
                builder.AppendLine(@"}, 300);");
                builder.AppendLine(@"</script>");
                break;
            case 4://用于贴吧
                builder.AppendLine("<script type=\"text/javascript\">");
                builder.AppendLine(@"UE.getEditor('" + txtid + "', {" + BLLCommon.ueditorBar + "});");
                //builder.AppendLine(@"setTimeout(function () {UE.getEditor('" + txtid + "', {" + BLLCommon.ueditorBar + "});");
                //builder.AppendLine(@"}, 300);");
                builder.AppendLine(@"</script>");
                break;
            case 5://用于聊天等
                builder.AppendLine("<script type='text/javascript'>");
                builder.AppendLine(@"UE.getEditor('" + txtid + "', {" + BLLCommon.ueditorNom + "});");
                builder.AppendLine(@"</script>");
                break;
        }
        return builder.ToString();
    }
    /// <summary>
    /// 读取后台编译器配置，设置编译器(Disuse)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="toolbars">百度工具栏</param>
    /// <returns></returns>
    public static string GetEditor(string name, string toolbars = "")
    {
        if (!string.IsNullOrEmpty(toolbars))
            return "var editor; setTimeout(function () { editor = UE.getEditor('" + name + "') ,{ toolbars:[[" + toolbars + "]]} ;}, 300);";
        else
            return "var editor; setTimeout(function () { editor = UE.getEditor('" + name + "');}, 300);";
    }
}
