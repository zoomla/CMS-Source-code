using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.SQLDAL.SQL;
using ZoomLa.HtmlLabel;

namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class ContentManage : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        B_Content bll = new B_Content();
        B_Create CreateBll = new B_Create();
        B_ModelField bfield = new B_ModelField();
        B_Model bmodel = new B_Model();
        B_Node nodeBll = new B_Node();
        B_User buser = new B_User();
        ContentHelper conHelper = new ContentHelper();
        private string status;
        //与当前导入相关类
        ExcelImport import = new ExcelImport();//位于ZoomLa.Components
                                               //--------------
        private DataTable ModelDT { get { return ViewState["ModelDT"] as DataTable; } set { ViewState["ModelDT"] = value; } }
        private IList<M_AuditingState> StatusCodeList
        {
            get
            {
                if (ViewState["StatusCodeDT"] == null)
                {
                    B_AuditingState stateBll = new B_AuditingState();
                    ViewState["StatusCodeDT"] = stateBll.GetAuditingStateAll();
                }
                return ViewState["StatusCodeDT"] as IList<M_AuditingState>;
            }
            set
            {
                ViewState["StatusCodeDT"] = value;
            }
        }
        public int CNodeID//地址栏NodeID,默认为0
        {
            get
            {
                return DataConvert.CLng(ViewState["CNodeID"]);
            }
            set
            {
                ViewState["CNodeID"] = value;
            }
        }
        public int CModelID
        {
            get
            {
                return DataConvert.CLng(ViewState["ModelID"]);
            }
            set { ViewState["ModelID"] = value; }
        }
        private string KeyWord //关键词搜索
        {
            get
            {
                string result = "";
                if (ViewState["KeyWord"] != null)
                {
                    result = ViewState["KeyWord"].ToString();
                }
                return HttpUtility.HtmlDecode(result);
            }
            set
            {
                ViewState["KeyWord"] = value;
            }
        }
        //前端Type选择
        public int SelType
        {
            get
            {
                return DataConvert.CLng(ViewState["type"]);
            }
            set { ViewState["type"] = value; }
        }
        //0:按标题,Gid,1:按作者
        private int KeyType { get { return DataConvert.CLng(Request.QueryString["KeyType"]); } }
        //--------------
        //-----工作流
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                #region AJAX
                string result = "";
                switch (Request.Form["action"])
                {
                    case "move":
                        string direct = Request.Form["direct"];
                        int curid = DataConvert.CLng(Request.Form["curid"]), tarid = DataConvert.CLng(Request.Form["tarid"]);

                        M_CommonData curMod = bll.GetCommonData(curid);
                        M_CommonData tarMod = bll.GetCommonData(tarid);
                        if (curMod.OrderID == tarMod.OrderID)
                        {
                            switch (direct)
                            {
                                case "up":
                                    curMod.OrderID++;
                                    break;
                                case "down":
                                    curMod.OrderID--;
                                    break;
                            }
                        }
                        else
                        {
                            int temp = curMod.OrderID;
                            curMod.OrderID = tarMod.OrderID;
                            tarMod.OrderID = temp;
                        }
                        bll.UpdateByID(curMod); bll.UpdateByID(tarMod);
                        result = "true";
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                #endregion
            }
            RPT.SPage = SelPage;
            if (!IsPostBack)
            {
                SelType = DataConvert.CLng(Request.QueryString["type"]);
                if (SelType == 2) { btnAudit.CssClass = "btn btn-success"; }
                KeyWord = Request.QueryString["KeyWord"];
                CModelID = DataConvert.CLng(Request.QueryString["ModelID"]);
                CNodeID = DataConvert.CLng(Request.QueryString["NodeID"]);
                M_Node nodeMod = nodeBll.SelReturnModel(CNodeID);
                ModelDT = bmodel.GetAllMode();
                NodeName_L.Text = nodeMod.NodeName;
                //商城节点
                if (nodeMod.NodeListType == 2) { Response.Redirect("../Shop/ProductManage.aspx?NodeID=" + CNodeID); return; }
                if (CNodeID != 0 && !string.IsNullOrEmpty(nodeMod.ContentModel)) //生成添加文章链接
                {
                    string[] modelIDArr = nodeMod.ContentModel.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string temp = "<div class=\"btn-group\"><button type=\"button\" class=\"btn btn-default dropdown-toggle\" data-toggle=\"dropdown\">{0}管理<span class=\"caret\"></span></button><ul class=\"dropdown-menu\" role=\"menu\"><li><a href=\"AddContent.aspx?ModelID={1}&NodeID={2}\">添加{0}</a></li><li><a href=\"ContentManage.aspx?ModelID={1}&NodeID={2}&Import=1\">导入{0}</a></li><li><a href=\"ContentManage.aspx?ModelID={1}&NodeID={2}\">{0}列表</a></li></ul></div>";
                    for (int i = 0; i < modelIDArr.Length; i++)
                    {
                        DataRow modelDR = SelFromModelDT(DataConvert.CLng(modelIDArr[i]));
                        if (modelDR == null) { continue; }
                        if (i == 0)
                        {
                            ItemName_L.Text = modelDR["ItemName"].ToString();
                            RPT.ItemUnit = modelDR["ItemUnit"].ToString();
                            RPT.ItemName = modelDR["ItemName"].ToString();
                        }
                        lblAddContent.Text += String.Format(temp, modelDR["ItemName"], modelDR["ModelID"], CNodeID);
                    }
                }
                RPT.DataBind();
                string link = "<li><a href='" + customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='ContentManage.aspx'>" + Resources.L.内容管理 + "</a></li>";
                link += "<li><a href='" + Request.RawUrl + "'>" + Resources.L.按栏目管理 + "</a></li>";
                link += "<li class='active'><a href='" + Request.RawUrl + "' title='" + nodeMod.NodeName + "'>" + nodeMod.NodeName + "</a></li>";
                link += "<div class='pull-right hidden-xs'>";
                link += "<span><a href='" + customPath2 + "Content/SchedTask.aspx' title='" + Resources.L.查看计划任务 + "'><span class='fa fa-clock-o' style='color:#28b462;'></span></a>";
                link += GetOpenView() + "<span onclick=\"opentitle('EditNode.aspx?NodeID=" + CNodeID + "','" + Resources.L.配置本节点 + "');\" class='fa fa-cog' title='" + Resources.L.配置本节点 + "' style='cursor:pointer;margin-left:5px;'></span></span>";
                link += "</div>";
                Call.SetBreadCrumb(Master, link);
            }//IsPostBack End;
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            clearCache();
        }

        //超级管理员显示审核按钮,其他管理员走工作流审批流程
        public DataTable SelPage(int pageSize, int pageIndex)
        {
            string title = KeyWord;
            //string byfilde = ViewState["byfilde"] == null ? "" : ViewState["byfilde"].ToString();
            //string byOrder = ViewState["byOrder"] == null ? "" : ViewState["byOrder"].ToString();
            M_AdminInfo adminMod = B_Admin.GetLogin();
            PageSetting config = new PageSetting();
            DataTable dts = new DataTable();
            switch (SelType)//文章已审,未审等筛选
            {
                case 1:
                    status = "-3";
                    btnEsc.Enabled = false;
                    btnEsc.CssClass = "btn btn-primary";
                    break;
                case 2:
                    btnAudit.Visible = true;
                    status = ((int)ZLEnum.ConStatus.UnAudit).ToString();
                    break;
                case 3:
                    status = ((int)ZLEnum.ConStatus.Audited).ToString();
                    break;
                case 4:
                    status = ((int)ZLEnum.ConStatus.Reject).ToString();
                    btnEsc.Attributes.Add("disabled", "disabled");
                    break;
                default:
                    break;
            }
            //----------------工作流,其与角色绑定，不分是否超管(需将其改为视图)
            if (SelType == 5)//工作流审批
            {
                if (CNodeID > 0)
                {
                    dts = bll.GetDTByAuth(adminMod.RoleList, CNodeID);
                }
                else//获取全部
                {
                    dts = bll.GetDTByAuth(adminMod.RoleList);
                }
                btnUnAudit.Visible = false;
                audit_Div.Visible = true;
                return dts;
            }

            if (!adminMod.IsSuperAdmin())//非超级管理员(用视图,组合权限表)
            {
                //筛选数据,如何筛选
                DataTable authDT = badmin.GetNodeAuthList();
                if (authDT == null || authDT.Rows.Count < 1) return new DataTable();//没有分配角色,或权限为空
                string nodes = "";
                foreach (DataRow dr in authDT.Rows)
                {
                    nodes += dr["NID"].ToString() + ",";
                }
                if (CNodeID == 0 && !string.IsNullOrEmpty(nodes))//如果是全部文章,则筛选后输出
                {
                    config = bll.SelPage(pageIndex, pageSize, CNodeID, CModelID, status, KeyType, title, nodes);
                }
                else//如果是节点,判断用户权限,避免其直接通过URL地址进入
                {
                    if (("," + nodes).Contains("," + CNodeID + ","))
                    {
                        config = bll.SelPage(pageIndex, pageSize, CNodeID, CModelID, status, KeyType, title, nodes);
                    }
                    else
                    {
                        function.WriteErrMsg("你没有该节点的访问权限，请联系[系统管理员]!!!");
                    }
                }
            }
            else //超级管理员
            {
                config = bll.SelPage(pageIndex, pageSize, CNodeID, CModelID, status, KeyType, title);
            }
            if (!IsPostBack)
            {
                RPT.ItemCount = config.itemCount;
                WZS.Text = config.itemCount.ToString();
                DJS.Text = config.addon;
            }
            return config.dt;
        }
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idchk"])) return;
            string[] idArr = Request.Form["idchk"].Split(',');
            M_AdminInfo ad = badmin.GetAdminLogin();
            string unAud = "";
            for (int i = 0; i < idArr.Length; i++)
            {
                if (!ad.IsSuperAdmin() && !GetRole(bll.GetCommonData(Convert.ToInt32(idArr[i])).NodeID, "State"))
                {
                    unAud += idArr[i] + ",";
                }
                else
                {
                    Auditing();
                }
            }
            if (!string.IsNullOrEmpty(unAud)) Page.ClientScript.RegisterStartupScript(GetType(), "", "alert(" + unAud + "无权限审核);location=location;", true);
            else Page.ClientScript.RegisterStartupScript(GetType(), "", "location=location;", true);
        }
        //取消审核
        protected void btnUnAudit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idchk"])) return;
            string[] idArr = Request.Form["idchk"].Split(',');
            M_AdminInfo ad = badmin.GetAdminLogin();
            string unAud = "";
            bll.Reset(Request.Form["idchk"]);
            for (int i = 0; i < idArr.Length; i++)
            {
                M_CommonData mcom = bll.GetCommonData(Convert.ToInt32(idArr[i]));
                M_UserInfo userinfo = buser.GetUserIDByUserName(mcom.Inputer);
                if (!ad.IsSuperAdmin() && !GetRole(mcom.NodeID, "State"))
                {
                    unAud += idArr[i] + ",";
                }
                else
                {
                    if (mcom.Status != 0)
                    {
                        //判断是否会员添加、添加会员积分
                        if (SiteConfig.UserConfig.InfoRule > 0)
                        {
                            buser.ChangeVirtualMoney(userinfo.UserID, new M_UserExpHis()
                            {
                                score = SiteConfig.UserConfig.InfoRule,
                                detail = mcom.Title,
                                ScoreType = (int)M_UserExpHis.SType.Point
                            });
                        }
                    }

                }
            }
            DataBind();
        }
        //退稿
        protected void btnEsc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idchk"])) return;
            string[] idArr = Request.Form["idchk"].Split(',');
            M_AdminInfo ad = badmin.GetAdminLogin();
            //string unAud = "";
            for (int i = 0; i < idArr.Length; i++)
            {
                M_CommonData mcom = bll.GetCommonData(Convert.ToInt32(idArr[i]));
                string username = mcom.Inputer;
                M_UserInfo userinfo = buser.GetUserIDByUserName(username);
                if (!ad.IsSuperAdmin() && !GetRole(mcom.NodeID, "State"))
                {
                    //unAud += idArr[i] + ",";
                }
                else
                {
                    bll.SetAudit(Convert.ToInt32(idArr[i]), -1);
                }
            }
            DataBind();
        }
        //非前台使用
        public bool GetRole(int nid, string authType)
        {
            bool result = false;
            M_AdminInfo ad = badmin.GetAdminLogin();
            if (ad.IsSuperAdmin())
            {
                result = true;
            }
            else
            {
                DataTable dt = badmin.GetNodeAuthList();
                dt.DefaultView.RowFilter = "NID=" + nid + " And " + authType + " = 1";
                if (dt.DefaultView.ToTable().Rows.Count > 0) result = true;
            }
            return result;
        }
        //审核
        protected void Auditing()
        {
            //查询该用户所拥有的角色
            string roleList = badmin.GetAdminLogin().RoleList;
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    int status = 99;
                    if (!string.IsNullOrEmpty(roleList))
                    {
                        string[] strs = roleList.Split(new char[] { ',' });
                        foreach (string str in strs)
                        {
                            if (str == "0")
                            {
                                status = 99;
                                break;
                            }
                        }
                    }
                    #region  判断审核通过是否添加积分
                    M_CommonData mcom = bll.GetCommonData(itemID);
                    string username = mcom.Inputer;
                    M_UserInfo userinfo = buser.GetUserIDByUserName(username);
                    if (mcom != null && mcom.GeneralID > 0 && !userinfo.IsNull)
                    {
                        if (mcom.Status == 99)
                        {
                            continue;
                        }
                        M_Node node = nodeBll.GetNodeXML(mcom.NodeID);
                        //判断是否会员添加、添加会员积分
                        if (SiteConfig.UserConfig.InfoRule > 0)
                        {
                            buser.ChangeVirtualMoney(userinfo.UserID, new M_UserExpHis()
                            {
                                score = SiteConfig.UserConfig.InfoRule,
                                detail = mcom.Title,
                                ScoreType = (int)M_UserExpHis.SType.Point,
                                Operator = userinfo.UserID,
                                OperatorIP = Request.UserHostAddress
                            });
                        }
                    }
                    #endregion
                    bll.SetAudit(itemID, status);
                }
            }//For end;
            DataBind();
        }
        //批量删除
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            M_AdminInfo ad = badmin.GetAdminLogin();
            if (!ad.IsSuperAdmin() && !GetRole(CNodeID, "Modify"))
            {
                function.Script(this, "alert('你无权限删除信息');");
            }
            else
            {
                string title = "";
                string ids = "";
                string[] chkArr = GetChecked();
                if (chkArr != null)
                {
                    for (int i = 0; i < chkArr.Length; i++)
                    {
                        int itemID = Convert.ToInt32(chkArr[i]);
                        M_CommonData commoninfo = bll.GetCommonData(itemID);
                        if (commoninfo.HtmlLink != "")
                        {
                            SafeSC.DelFile(commoninfo.HtmlLink);
                        }
                        title = title + commoninfo.Title + "<br />";
                        if (string.IsNullOrEmpty(ids))
                            ids = itemID.ToString();
                        else
                            ids = ids + "," + itemID.ToString();
                        bll.SetDel(itemID);
                    }
                }
                Response.Redirect(Request.RawUrl);
            }
        }
        public string GetElite(string elite)
        {
            return conHelper.GetElite(elite);
        }
        //显示标题
        public string GetTitle()
        {
            int gid = DataConvert.CLng(Eval("GeneralID"));
            int nodeid = DataConvert.CLng(Eval("NodeID"));
            string title = StringHelper.SubStr(Eval("Title").ToString());
            string style = DataConvert.CStr(Eval("TitleStyle"));
            string n = "";
            if (nodeid == CNodeID)
            {
                n = "<a style=\"" + style + "\" href=\"ShowContent.aspx?GID=" + gid + "\">" + title + "</a>";
            }
            else
            {
                n = "<strong>[<a href=\"ContentManage.aspx?NodeID=" + nodeid + "\">" + Eval("NodeName") + "</a>]</strong><a style=\"" + style + "\" href=\"ShowContent.aspx?GID=" + gid + "\">" + title + "</a>";
            }
            return n;
        }
        //小屏幕上显示
        public string GetTitle(string title)
        {
            if (title.Length > 6)
                return title.Substring(0, 5) + "...";
            return title;
        }
        public string GetStatus(string status)
        {
            try
            {
                return StatusCodeList.First(p => p.StateCode == Convert.ToInt32(status)).StateName;
            }
            catch { return "未知"; }
        }
        //删除
        public bool getenabled(string gid)
        {
            return B_ARoleAuth.Check(ZLEnum.Auth.content, "ContentRecycle");
        }
        //显示模型标识图片
        public string GetPic(object modelid)
        {
            DataRow dr = SelFromModelDT(DataConvert.CLng(modelid));
            if (dr == null || string.IsNullOrEmpty(DataConvert.CStr(dr["ItemIcon"]))) { return ""; }
            return "<span class=\"" + dr["ItemIcon"] + "\" />";
        }
        //移动
        protected void btnMove_Click(object sender, EventArgs e)
        {
            M_AdminInfo ad = badmin.GetAdminLogin();
            if (!("," + ad.RoleList + ",").Contains(",1,") && !GetRole(CNodeID, "Modify"))
            {
                function.WriteErrMsg("你无权限移动信息", "ContentManage.aspx?NodeID=" + CNodeID);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                string[] chkArr = GetChecked();
                if (chkArr != null)
                {
                    for (int i = 0; i < chkArr.Length; i++)
                    {
                        if (sb.Length == 0)
                            sb.Append(chkArr[i]);
                        else
                            sb.Append("," + chkArr[i]);
                    }
                    Response.Redirect("ContentMove.aspx?id=" + sb.ToString());
                }
            }
        }
        //添加到专题
        protected void btnAddToSpecial_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    if (sb.Length == 0)
                        sb.Append(chkArr[i]);
                    else
                        sb.Append("," + chkArr[i]);
                }
                Response.Redirect("AddToSpecial.aspx?id=" + sb.ToString());
            }
        }
        //批量修改
        protected void Button1_Click(object sender, EventArgs e)
        {
            string itemID = "";
            M_Node nnn = nodeBll.GetNodeXML(CNodeID);
            IList<int> contentlist = new List<int>();
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int gid = DataConverter.CLng(chkArr[i]);
                    M_CommonData cmdinfo = bll.GetCommonData(gid);
                    if (contentlist.Count > 0)
                    {
                        if (contentlist.IndexOf(cmdinfo.ModelID) == -1)
                        {
                            function.WriteErrMsg("操作失败！只允许相同模型的批量修改");
                        }
                    }
                    contentlist.Add(cmdinfo.ModelID);
                    itemID += chkArr[i] + "|";
                }
            }
            if (contentlist.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "alert('请选择要修改的内容');", true);
            }
            else
            {
                Response.Redirect("editListContent.aspx?ModelID=" + contentlist[0] + "&NodeID=" + CNodeID + "&ID=" + itemID + "&ModelName=" + CNodeID);
            }
        }
        //推荐
        protected void Button2_Click(object sender, EventArgs e)
        {
            M_AdminInfo ad = badmin.GetAdminLogin();
            if (!("," + ad.RoleList + ",").Contains(",1,") && !GetRole(CNodeID, "State"))
            {
                function.WriteErrMsg("你无权推荐信息", "ContentManage.aspx?NodeID=" + CNodeID);
            }
            else
            {
                string[] chkArr = GetChecked();
                if (chkArr != null)
                {
                    for (int i = 0; i < chkArr.Length; i++)
                    {
                        int itemID = Convert.ToInt32(chkArr[i]);
                        M_CommonData mcom = bll.GetCommonData(itemID);
                        mcom.EliteLevel = 1;
                        bll.UpdateByID(mcom);
                        if (mcom.EliteLevel != 1)
                        {
                            if (string.IsNullOrEmpty(mcom.Inputer)) continue;
                            M_UserInfo mu = buser.GetUserIDByUserName(mcom.Inputer);
                            if (mu.IsNull || mu == null || mu.UserID < 1 || SiteConfig.UserConfig.InfoRule < 1) { continue; }
                            else
                            {
                                buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
                                {
                                    score = SiteConfig.UserConfig.RecommandRule,
                                    detail = mcom.Title,
                                    ScoreType = (int)M_UserExpHis.SType.Point,
                                    Operator = mu.UserID,
                                    OperatorIP = EnviorHelper.GetUserIP()
                                });
                            }
                        }
                    }
                    Response.Redirect("ContentManage.aspx");
                }
            }
        }
        //取消推荐
        protected void Button3_Click(object sender, EventArgs e)
        {
            M_AdminInfo ad = badmin.GetAdminLogin();
            if (!("," + ad.RoleList + ",").Contains(",1,") && !GetRole(CNodeID, "State"))
            {
                function.WriteErrMsg("你无权限取消推荐信息", "ContentManage.aspx?NodeID=" + CNodeID);
            }
            else
            {
                string[] chkArr = GetChecked();
                if (chkArr != null)
                {
                    for (int i = 0; i < chkArr.Length; i++)
                    {
                        int itemID = Convert.ToInt32(chkArr[i]);
                        M_CommonData contentinfo = bll.GetCommonData(itemID);
                        contentinfo.EliteLevel = 0;
                        bll.UpdateByID(contentinfo);
                    }
                    Response.Redirect("ContentManage.aspx");
                }
            }
        }
        //过滤重复行
        public DataTable SelectDistinctByField(DataTable dt, string FieldName)
        {
            DataTable returnDt = new DataTable();
            returnDt = dt.Copy();//将原DataTable复制一个新的
            DataRow[] drs = returnDt.Select("", FieldName);//将DataTable按指定的字段排序
            object LastValue = null;
            for (int i = 0; i < drs.Length; i++)
            {
                if ((LastValue == null) || (!(ColumnEqual(LastValue, drs[i][FieldName]))))
                {
                    LastValue = drs[i][FieldName];
                    continue;
                }

                drs[i].Delete();
            }

            return returnDt;
        }
        //整合两张表
        private bool ColumnEqual(object A, object B)
        {
            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is DBNull.Value
                return false;
            return (A.Equals(B));  // value type standard comparison
        }
        //获取选中的checkbox
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] chkArr = Request.Form["idchk"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
        //计划任务
        protected void timeReConfBtn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            DateTime executeTime = DataConvert.CDate(Request.Form["timeReBTime"]);
            if (string.IsNullOrEmpty(ids))
            {
                function.Script(this, "alert('未选择文章');");
            }
            else
            {
                B_Content_ScheTask scheBll = new B_Content_ScheTask();
                M_Content_ScheTask scheModel = new M_Content_ScheTask();
                scheModel.TaskName = "定时发布";
                scheModel.TaskContent = ids;//ID
                scheModel.TaskType = (int)M_Content_ScheTask.TaskTypeEnum.Content;//根据这个调用不同的方法处理TaskContent
                scheModel.ExecuteType = (int)M_Content_ScheTask.ExecuteTypeEnum.JustOnce;
                scheModel.Status = 0;
                scheModel.ExecuteTime = executeTime.ToString();
                M_AdminInfo adminMod = B_Admin.GetLogin();

                scheModel.CDate = DateTime.Now;
                scheModel.AdminID = adminMod.AdminId;
                scheModel.ID = scheBll.Add(scheModel);
                TaskCenter.AddTask(scheModel);
                function.Script(this, "alert('添加成功');");
            }
        }
        private void clearCache()
        {
            //ViewState["CommonDataDT"] = null;
            ViewState["NodeRoleDT0"] = null;
            ViewState["NodeRoleDT1"] = null;
            StatusCodeList = null;
            ModelDT = null;
        }
        public string GetOpenView()
        {
            string d = "";
            if (string.IsNullOrEmpty(Request.QueryString["NodeID"]))
                d = "1";
            else
                d = Convert.ToString(CNodeID);
            string IndexTemplate = "", outstr = "", strurl = string.Empty;
            strurl = "Class_" + d + "/Default.aspx";
            DataTable dt = Sql.Select("ZL_Node", "NodeID=" + CNodeID, "IndexTemplate");
            if (dt.Rows.Count > 0)
                IndexTemplate = dt.Rows[0][0].ToString();
            if (IndexTemplate != "")
            {
                outstr = " <a href='/" + strurl + "'  target='_blank' title='前台浏览'><span class='fa fa-eye'></span></a>";
            }
            else
            {
                outstr = " <a href='javascript:void(0)' onclick='editnode(" + d + ")' title='前台浏览'><span class='fa fa-eye'></span></a>";
            }
            return outstr;
        }

        //----------------工作流相关(需移入BLL)
        //工作流审批，将其改为下一状态码
        protected void NextStep_Btn_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] gidArr = Request.Form["idchk"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                // bll.UpdateStatus(gidArr,badmin.GetAdminLogin().RoleList,DataConvert.CLng(Request.QueryString["NodeID"]),"PPassCode");
            }
            Response.Redirect(Request.RawUrl);
        }
        //工作流拒绝审批，将其改为拒绝状态码
        protected void StepReject_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] gidArr = Request.Form["idchk"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                // bll.UpdateStatus(gidArr, badmin.GetAdminLogin().RoleList, DataConvert.CLng(Request.QueryString["NodeID"]), "PNoPassCode");
            }
            Response.Redirect(Request.RawUrl);
        }
        //----------------工作流相关(End)
        //int GeneralID = DataConverter.CLng(Request.QueryString["GeneralID"]);//传入GeneralID删除对应HTML?
        //删除文件
        //if (GeneralID > 0)
        //{
        //    M_CommonData cdatainfo = bll.GetCommonData(GeneralID);
        //    string htmllink = cdatainfo.HtmlLink;
        //    if (htmllink != "")
        //    {
        //        string dz = "";
        //        string bj = Server.MapPath("/");
        //        htmllink = htmllink.Substring(1);
        //        dz = bj + htmllink;
        //        File.Delete(dz);
        //        bll.UpdateCreate1(GeneralID);
        //        CreateBll.createann(cdatainfo.NodeID.ToString());//发布内容页
        //        CreateBll.CreateColumnByID(cdatainfo.NodeID.ToString());//发布栏目页
        //        CreateBll.CreatePageIndex(); //发布首页
        //        function.WriteErrMsg(cdatainfo.Title + "的静态文件删除成功！");
        //    }
        //}
        public string GetContent()
        {
            //是否优化为AJAX加载,其无法联接
            //或先显示,再一次性AJAX请求加载
            string result = "";
            DataTable contentDT = bll.GetContentByItems(Eval("TableName").ToString(), Convert.ToInt32(Eval("GeneralID")));
            if (contentDT.Rows.Count < 1) { return ""; }//避免删除模型后浏览报错
            DataTable dt = bfield.SelByModelID(Convert.ToInt32(Eval("ModelID")));
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["FieldName"].ToString() == "content")
                {
                    string fname = dr["FieldName"].ToString();
                    if (contentDT.Columns.Contains(fname))
                    {
                        result = contentDT.Rows[0][fname].ToString();
                    }
                    break;
                }
            }
            result = StringHelper.StripHtml(result, 200);
            return result;
        }
        protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item != null && e.Item.ItemType != ListItemType.Footer)
            {
                DataRowView dr = e.Item.DataItem as DataRowView;
                int generalId = DataConverter.CLng(dr["GeneralID"]);
                int isCreate, status;
                isCreate = DataConvert.CLng(dr["IsCreate"]);
                status = DataConvert.CLng(dr["Status"]);

                LinkButton lbHtml = e.Item.FindControl("lbHtml") as LinkButton;//删除||生成Html
                LinkButton lbCheck = e.Item.FindControl("lbCheck") as LinkButton;//浏览
                LinkButton lbDelete = e.Item.FindControl("lbDelete") as LinkButton;//删除内容
                if (isCreate == 1) //判断是否已生成,1.为已生成
                {
                    lbHtml.CommandName = "DelHtml";
                    lbHtml.CommandArgument = generalId.ToString();
                    lbHtml.Text = Resources.L.删除 + "HTML";
                }
                else if (isCreate == 0) //判断是否已生成,0.为未生成
                {
                    lbHtml.CommandName = "CreateHtml";
                    lbHtml.CommandArgument = generalId.ToString();
                    lbHtml.Text = "<i class='fa fa-play'></i>" + Resources.L.生成 + "HTML";
                }
            }
        }
        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Purview") { Page.Response.Redirect("/Item/" + e.CommandArgument + ".aspx"); return; }
            if (e.CommandName == "Edit") { Response.Redirect("EditContent.aspx?GeneralID=" + e.CommandArgument); return; }
            if (e.CommandName == "Del")
            {
                M_AdminInfo ad = badmin.GetAdminLogin();
                if (!ad.IsSuperAdmin() && !GetRole(CNodeID, "Modify"))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "alert('你无权限删除该内容!!');", true);
                }
                else
                {
                    string Id = e.CommandArgument.ToString();
                    M_CommonData cmdinfo = bll.GetCommonData(DataConverter.CLng(Id));
                    string title = cmdinfo.Title;
                    string htmllink = cmdinfo.HtmlLink;
                    if (htmllink != "")
                    {
                        SafeSC.DelFile(htmllink);
                    }
                    bll.SetDel(DataConverter.CLng(Id));
                    CreateBll.createann(cmdinfo.NodeID.ToString());//生成内容页静态文件
                    CreateBll.CreateColumnByID(cmdinfo.NodeID.ToString());//生成栏目页静态文件
                    CreateBll.CreatePageIndex(); //发布首页
                    DataBind();
                }
            }
            if (e.CommandName == "DelHtml")
            {
                int gid = DataConverter.CLng(e.CommandArgument.ToString());
                M_CommonData comdt = bll.GetCommonData(gid);
                if (string.IsNullOrEmpty(comdt.HtmlLink) && comdt.HtmlLink.Contains("."))
                {
                    string fleex = "." + comdt.HtmlLink.Split('.')[1];
                    FileSystemObject.Delete(Server.MapPath(comdt.HtmlLink), FsoMethod.File);
                    string HtmlLinkurl = comdt.HtmlLink.Replace(gid + fleex, "");
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath(HtmlLinkurl));
                    FileInfo[] ff = di.GetFiles(gid + "_" + "*");
                    if (ff.Length != 0)
                    {
                        foreach (FileInfo fi in ff)
                        {
                            fi.Delete();
                        }
                    }
                }
                comdt.HtmlLink = "";
                comdt.IsCreate = 0;
                bll.UpdateByID(comdt);
                CreateBll.CreatePageIndex(); //发布首页
                DataBind();
            }
            if (e.CommandName == "CreateHtml")//注：如节点设置为不生成,则不成内容页与首页,但状态会变更
            {
                int gid = DataConverter.CLng(e.CommandArgument.ToString());
                M_CommonData comdt = bll.GetCommonData(gid);
                comdt.IsCreate = 1;
                bll.UpdateByID(comdt);
                CreateBll.CreateInfo(DataConverter.CLng(gid), DataConverter.CLng(comdt.NodeID), DataConverter.CLng(comdt.ModelID));
                CreateBll.CreatePageIndex(); //发布首页
                DataBind();
            }
            if (e.CommandName == "bider")  //查看中标
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                Response.Redirect("BidManage.aspx?GeneralID=" + Id + "&NodeID=" + CNodeID);
            }
        }
        #region Excel相关
        string excel_headers = "标题,审核状态,添加时间,更新时间,点击数";
        //生成Excel模板
        protected void lbtnSaveTempter_Click(object sender, EventArgs e)
        {
            //根据模型ID查询字段
            DataTable dt = bfield.GetModelFieldList(CModelID);
            // 生成Excel模板表头,并回发
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                excel_headers += "," + dt.Rows[i]["FieldAlias"].ToString();
            }
            DataGrid dg = new DataGrid();
            dg.DataSource = dt.DefaultView;
            dg.DataBind();
            string fname = DateTime.Now.ToString("yyyyMMdd") + bmodel.GetModelById(CModelID).ItemName + "导入模板";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fname) + ".csv");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Clear(); Response.Write(excel_headers); Response.Flush(); Response.End();
        }
        //根据csv或xlsx,将数据导入数据库
        protected void btnCurrentImport_Click(object sender, EventArgs e)
        {
            string fileName = fileImp.FileName;
            string exName = Path.GetExtension(fileName).ToLower();
            if (!exName.Equals(".csv") && !exName.Equals(".xls") && !exName.Equals(".xlsx"))//判断扩展名
            {
                function.Script(this, "alert('上传的文件不是符合扩展名csv,请重新选择!');");
                return;
            }
            string path = Server.MapPath("~/xls/");
            if (!FileSystemObject.IsExist(path, FsoMethod.Folder))
            {
                FileSystemObject.CreateFileFolder(path);
            }
            //SafeSC.SaveFile("/xls/", fileImp, fileName);
            if (!fileImp.SaveAs("/xls/" + fileName)) { function.WriteErrMsg(fileImp.ErrorMsg); }
            //导入文件到数据集对象                   
            DataSet oldDs = new DataSet();
            if (exName.Equals(".csv"))
            {
                CsvToDataSet(oldDs, path + fileName);
            }
            else
            {
                ExcelToDataSet(oldDs, path + fileName);
            }
            File.Delete(path + fileName);
            //if (!CheckColCount(oldDs.Tables[0]))
            //    return;
            DataSet newDs = CreateTable(oldDs);
            //string s = "";
            //foreach (DataColumn dc in newDs.Tables[1].Columns)
            //{
            //    s += dc.ColumnName + ",";
            //}
            //function.WriteErrMsg(s);
            //保存到数据库                          
            SaveDb(newDs);
        }
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public void CsvToDataSet(DataSet ds, string csvPath)
        {
            string fileFullName = Path.GetFileName(csvPath);
            string folderPath = csvPath.Substring(0, csvPath.LastIndexOf('\\') + 1);

            string connStr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='text;HDR=Yes;IMEX=1'", folderPath);
            string sql = string.Format(@"SELECT * FROM [{0}]", fileFullName);
            OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr);

            da.Fill(ds);

            DataColumn dcTemp = new DataColumn("No", typeof(string));
            DataColumn dcTemp1 = new DataColumn("Up", typeof(string));

            ds.Tables[0].Columns.Add(dcTemp);
            ds.Tables[0].Columns["No"].SetOrdinal(0);
            ds.Tables[0].Columns.Add(dcTemp1);
            ds.Tables[0].Columns["Up"].SetOrdinal(2);

            int row = ds.Tables[0].Rows.Count;
            for (int j = 0; j < row; j++)
            {
                ds.Tables[0].Rows[j]["No"] = j + 1;
                ds.Tables[0].Rows[j]["Up"] = 0;
            }
        }
        /// <summary>
        /// 将Excel文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">Excel文件路径</param>
        /// <returns>返回读取了Excel数据的DataTable</returns>
        public void ExcelToDataSet(DataSet ds, string excelPath)
        {
            string fileFullName = Path.GetFileName(excelPath);
            string folderPath = excelPath.Substring(0, excelPath.LastIndexOf('\\') + 1);
            string connStr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", folderPath + fileFullName);

            ds.Tables.Add(OleDB.Select(excelPath, "select * from" + OleDB.SelectTables(excelPath).Rows[0]["Table_Name"]));//读取第一张表中的数据;
            DataColumn dcTemp = new DataColumn("No", typeof(string));
            DataColumn dcTemp1 = new DataColumn("Up", typeof(string));

            ds.Tables[0].Columns.Add(dcTemp);
            ds.Tables[0].Columns["No"].SetOrdinal(0);
            ds.Tables[0].Columns.Add(dcTemp1);
            ds.Tables[0].Columns["Up"].SetOrdinal(2);

            int row = ds.Tables[0].Rows.Count;
            for (int j = 0; j < row; j++)
            {
                ds.Tables[0].Rows[j]["No"] = j + 1;
                ds.Tables[0].Rows[j]["Up"] = 0;
            }
        }
        // 校验列数是否一致
        private bool CheckColCount(DataTable impxls)
        {
            //根据模型号查询字段 查询现有模型列
            DataTable dt = bfield.GetModelFieldList(CModelID);
            if ((impxls.Columns.Count - excel_headers.Split(',').Length) != dt.Rows.Count)
            {
                function.Script(this, "alert('上传的文件与现有模型不匹配，请重新选择！');");
                return false;
            }
            else { return true; }
        }
        // 生成新的主从表
        public DataSet CreateTable(DataSet oldDs)
        {
            int index = 3;
            int sp_no = 2;
            DataTable oldTable = oldDs.Tables[0];

            DataTable t1 = new DataTable();
            DataColumn c1 = oldTable.Columns[0];
            t1.Columns.Add(new DataColumn(c1.ColumnName, c1.DataType));
            for (int i = 1; i < index; i++)
            {
                DataColumn col = oldTable.Columns[i];
                t1.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
            }

            DataTable t2 = new DataTable();
            DataColumn c2 = oldTable.Columns[0];
            t2.Columns.Add(new DataColumn(c2.ColumnName, c2.DataType));
            for (int i = index; i < oldTable.Columns.Count; i++)
            {
                DataColumn col = oldTable.Columns[i];
                t2.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
            }

            for (int i = 0; i < oldTable.Rows.Count; i++)
            {
                DataRow r = t1.NewRow();
                r[0] = oldTable.Rows[i][0];
                for (int j = 1; j < index; j++)
                {
                    r[j] = oldTable.Rows[i][j];
                }
                //t1.ImportRow(r);
                t1.Rows.Add(r);
            }
            for (int i = 0; i < oldTable.Rows.Count; i++)
            {
                DataRow r = t2.NewRow();
                r[0] = oldTable.Rows[i][0];
                for (int j = index; j < oldTable.Columns.Count; j++)
                {
                    // r[j - 2] = oldTable.Rows[i][j];
                    r[j - sp_no] = oldTable.Rows[i][j];
                }
                //t2.ImportRow(r);
                t2.Rows.Add(r);
            }

            DataSet newDs = new DataSet();
            newDs.Tables.Add(t1);
            newDs.Tables.Add(t2);
            return newDs;
        }
        // 导入excel数据到数据库
        public void SaveDb(DataSet ds)
        {
            //根据模型号查询字段
            DataTable dt = bfield.GetModelFieldList(CModelID);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
            int colCount = ds.Tables[1].Columns.Count;
            foreach (DataRow childRow in ds.Tables[1].Rows)
            {
                table.Rows.Clear();
                #region 构建从表内容表
                int colNo = excel_headers.Split(',').Length;//从第几列开始
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow row = table.NewRow();
                    row["FieldName"] = dr["FieldName"].ToString();
                    row["FieldType"] = dr["FieldType"].ToString();
                    row["FieldValue"] = childRow[colNo].ToString();//excel或csv中的数据
                    table.Rows.Add(row);
                    colNo++;
                    if (colNo == colCount) { break; }
                }
                #endregion
                #region 构建主表
                ds.Tables[0].DefaultView.RowFilter = "No=" + childRow["No"].ToString();
                ds.Tables[1].DefaultView.RowFilter = "No=" + childRow["No"].ToString();
                DataRow mainRow = ds.Tables[0].DefaultView[0].Row;
                DataRow mainRow2 = ds.Tables[1].DefaultView[0].Row;
                M_CommonData CData = new M_CommonData();
                CData.NodeID = CNodeID;
                CData.ModelID = CModelID;
                CData.TableName = bmodel.GetModelById(CModelID).TableName;
                CData.Title = mainRow[1].ToString();
                CData.Inputer = badmin.GetAdminLogin().AdminName;
                CData.EliteLevel = 0;
                CData.InfoID = "";
                CData.SpecialID = "";
                CData.Hits = DataConvert.CLng(mainRow2["点击数"]);
                CData.IsCreate = 0;
                CData.UpDateType = 1;
                CData.CreateTime = DataConvert.CDate(mainRow2["添加时间"].ToString());
                CData.UpDateTime = DataConvert.CDate(mainRow2["更新时间"].ToString());
                CData.TagKey = "";
                CData.Status = DataConverter.CLng(mainRow2[1].ToString());
                CData.Template = "";
                #endregion
                bll.AddContent(table, CData);
            }
            function.WriteSuccessMsg("导入成功");
        }
        #endregion
        //工具
        private DataRow SelFromModelDT(int modelid)
        {
            if (ModelDT == null || ModelDT.Rows.Count < 1 || modelid < 1) { return null; }
            DataRow[] drs = ModelDT.Select("ModelID=" + modelid);
            if (drs.Length < 1) { return null; }
            else
            {
                drs[0]["ItemName"] = DataConvert.CStr(drs[0]["ItemName"]);
                return drs[0];
            }
        }
        //推送
        protected void PushCon_Btn_Click(object sender, EventArgs e)
        {
            string nodes = Request.Form["pushcon_hid"].Trim(',');
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(nodes) && !string.IsNullOrEmpty(ids))
            {
                string[] idsArr = ids.Split(',');
                for (int i = 0; i < idsArr.Length; i++)
                {
                    M_CommonData cdata = null;
                    DataTable dt = null;
                    bll.GetContent(Convert.ToInt32(idsArr[i]), ref cdata, ref dt);
                    if (cdata != null && dt != null)
                    {
                        PushConToNodes(nodes, cdata, dt);
                    }
                }
                function.WriteSuccessMsg("推送完成");
            }
            else { function.Script(this, "未选择需推送的文章或目标节点"); }
        }
        private void PushConToNodes(string nodes, M_CommonData cdata, DataTable dt)
        {
            nodes = nodes.Trim(',').Replace(" ", "");
            //if (string.IsNullOrEmpty(nodes)) { return; }
            string[] nodeArr = nodes.Split(',');
            for (int i = 0; i < nodeArr.Length; i++)
            {
                cdata.NodeID = Convert.ToInt32(nodeArr[i]);
                if (CNodeID == cdata.NodeID) { continue; }//不能将文章推送到自己节点
                cdata.FirstNodeID = 0;
                PushAddCon(dt, cdata);
            }
        }
        private void PushAddCon(DataTable dt, M_CommonData cdata)
        {
            //nsert into 库B.dbo.AA select * from 库A.dbo.AA where 库A.dbo.AA.C = ’‘
            DataRow dr = dt.Rows[0];
            string sql = "INSERT INTO {1} SELECT {0} FROM {1} WHERE [ID] = " + dr["ID"] + ";SELECT @@identity;";
            string columns = "";
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName.ToLower().Equals("id")) { continue; }
                columns += dc.ColumnName + ",";
            }
            columns = columns.Trim(',');
            cdata.ItemID = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, string.Format(sql, columns, cdata.TableName)));
            bll.insert(cdata);
        }
    }
}