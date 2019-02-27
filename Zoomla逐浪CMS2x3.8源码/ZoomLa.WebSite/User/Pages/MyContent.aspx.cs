namespace ZoomLa.WebSite.User.Content
{
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Page;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    using ZoomLa.Model.Page;
    using ZoomLa.SQLDAL;
    public partial class MyContent : System.Web.UI.Page
    {
        B_Content conBll = new B_Content();
        B_Model bmode = new B_Model();
        B_User buser = new B_User();
        B_ModelField mll = new B_ModelField();
        B_Templata tll = new B_Templata();
        public string PageID
        {
            get
            {
                if (ViewState["PageID"] == null)
                {
                    B_PageReg regBll = new B_PageReg();
                    M_PageReg regMod = regBll.GetSelectByUserID(buser.GetLogin().UserID);
                    ViewState["PageID"] = regMod.ID;
                }
                return ViewState["PageID"].ToString();
            }
        }
        public int GetMaxOrderID()
        {
            string strSql3 = "select max(OrderID) from ZL_CommonModel";
            int OrderID = SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql3, null));
            return OrderID;
        }
        public int ModelID { get { return DataConvert.CLng(Request.QueryString["ModelID"]); } }
        public int NodeID { get { return DataConvert.CLng(Request.QueryString["NodeID"]); } }
        //筛选Audit,Elite
        public string Status { get { return Request.QueryString["type"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                DataTable cmdinfo = mll.SelectTableName("ZL_PageReg", "TableName like 'ZL_Reg_%' and  UserName='" + mu.UserName + "'");
                if (cmdinfo.Rows.Count == 0)
                {
                    Response.Redirect("PageInfo.aspx.aspx");
                }
                if (ModelID != 0)
                {
                    M_Templata tempinfos = tll.Getbyid(this.ModelID);
                    if (tempinfos.IsTrue != 1) { function.WriteMessage("找不到此栏目或此栏目未启用!", "MyContent.aspx", "栏目出错"); }
                    string modeinfo = tempinfos.Modelinfo.ToString();
                    string addhtml = "";
                    foreach (string modid in modeinfo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        M_ModelInfo modMod = bmode.GetModelById(DataConvert.CLng(modid));
                        if (modMod != null)
                        {
                            addhtml += "[<a href=\"AddContent.aspx?ModelID=" + modMod.ModelID + "&Nodeid=" + NodeID + "\">添加" + modMod.ItemName + "</a>]";
                        }
                    }
                    lblAddContent.Text = addhtml;
                }
                else
                {
                    lblAddContent.Text = "[全部内容]" + "&nbsp;&nbsp;<a href=\"Default.aspx?pageid=" + cmdinfo.Rows[0]["ID"].ToString() + "\"  target=\"_blank\">我的黄页</a>";
                }
                MyBind();
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = this.conBll.PageContentList("ZL_Page_", Status, mu.UserName, NodeID, TxtSearchTitle.Value);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected string GetCommandID()
        {
            M_UserInfo info = buser.GetLogin();
            DataTable cmdinfo = mll.SelectTableName("ZL_PageReg", "TableName like 'ZL_Reg_%' and UserName='" + info.UserName + "'");
            if (cmdinfo != null)
            {
                if (cmdinfo.Rows.Count > 0)
                {
                    return cmdinfo.Rows[0]["ID"].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        // 全部选择控件设置
        protected void btnDeleteAll_Click(object sender, EventArgs e)//批量删除
        {
            if (string.IsNullOrEmpty(Request.Form["idchk"])) return;
            string[] idArr = Request.Form["idchk"].Split(',');
            for (int i = 0; i < idArr.Length; i++)
            {
                this.conBll.SetDel(Convert.ToInt32(idArr[i]));
            }
            function.WriteSuccessMsg("删除成功");
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                this.conBll.SetDel(DataConverter.CLng(Id));
                function.WriteSuccessMsg("删除成功");
            }
        }
        public string GetStatus()
        {
            return B_Content.GetStatusStr(DataConvert.CLng(Eval("Status")));
        }
        public bool ChkStatus(string status)
        {
            int s = DataConverter.CLng(status);
            if (s == (int)ZLEnum.ConStatus.Audited)
                return false;
            else
                return true;
        }
        public string GetUrl(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.conBll.GetCommonData(p);
            if (cinfo.IsCreate == 1)
                return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
            else
                return "/Page/PageContent.aspx?ItemID=" + p + "&PageID=" + PageID;
        }
        public string GetModel(string infoid)
        {
            int p = DataConverter.CLng(infoid);
            M_CommonData cinfo = this.conBll.GetCommonData(p);

            if (cinfo.ModelID == 0)
            {
                return "";
            }
            else
            {
                return "[" + bmode.GetModelById(cinfo.ModelID).ItemName + "] ";
            }
        }
        public string GetCteate(string IsCreate)
        {
            int s = DataConverter.CLng(IsCreate);
            if (s != 1)
                return "<font color=red>×</font>";
            else
                return "<font color=green>√</font>";
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        //保存修改后的排序,仅交换OrderID,PageOrder等不动
        protected void saveOrder_Btn_Click(object sender, EventArgs e)
        {
            //mid,oid,nid,wid
            string[] wid = Request.Form["order_T"].Split(',');//需要更换成的ID
            string[] ids = Request.Form["order_Hid"].Split(',');//信息描述
            for (int i = 0; i < wid.Length; i++)
            {
                int wantPid = Convert.ToInt32(wid[i]);
                int mid = Convert.ToInt32(ids[i].Split(':')[0]);
                int oid = Convert.ToInt32(ids[i].Split(':')[1]);
                int nowPid = Convert.ToInt32(ids[i].Split(':')[2]);
                if (wantPid == nowPid) continue;//没有修改排序值
                else
                {
                    conBll.UpdateOrder(mid, wantPid);
                }// if end;
            }//for end;
            MyBind();
        }
    }
}