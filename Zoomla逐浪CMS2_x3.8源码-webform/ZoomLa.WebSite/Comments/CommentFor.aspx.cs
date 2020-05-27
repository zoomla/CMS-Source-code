namespace ZoomLa.WebSite.Comment
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using System.Text;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using System.Xml;
    using ZoomLa.BLL.API;

    public partial class CommentFor : System.Web.UI.Page
    {
        B_Content bll = new B_Content();
        B_Comment cmtBll = new B_Comment();
        B_User buser = new B_User();
        B_Node nodeBll = new B_Node();
        public int ItemID { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        public int Cpage { get { int cpage = DataConverter.CLng(Request.QueryString["PageID"]); return cpage < 1 ? 1 : cpage; } }
        public string C_Title { get { return ViewState["C_Title"].ToString(); } set { ViewState["C_Title"] = value; } }
        public int ModelID { get { return Convert.ToInt32(ViewState["ModelID"]); } set { ViewState["ModelID"] = value; } }
        //当前文章下所有评论,回发前释放
        public DataTable CommentDT
        {
            get
            {
                if (ViewState["CommentList"] == null)
                {
                    ViewState["CommentList"] = cmtBll.SeachCommentByGid1(ItemID);
                }
                return (DataTable)ViewState["CommentList"];

            }
            set { ViewState["CommentList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request["action"];
                string value = "";
                switch (action)
                {
                    #region AJAX
                    case "report"://举报
                        M_UserInfo info = buser.GetLogin();
                        value = Request.Form["cid"];
                        cmtBll.ReportComment(Convert.ToInt32(value), info.UserID);
                        Response.Write("1");
                        break;
                    case "support"://支持反对操作
                        value = Request.Form["flag"];
                        bool result = true;
                        bool flag = DataConverter.CLng(value) > 0;
                        if (buser.GetLogin().IsNull)
                            result = cmtBll.Support(Convert.ToInt32(Request.Form["id"]), flag ? 1 : 0, EnviorHelper.GetUserIP());
                        else
                            result = cmtBll.Support(Convert.ToInt32(Request.Form["id"]), flag ? 1 : 0, EnviorHelper.GetUserIP(), buser.GetLogin().UserID, buser.GetLogin().UserName);
                        Response.Write(result ? "1" : "-1");
                        break;
                    case "assist"://顶与踩
                        bool bl = true;
                        if (buser.GetLogin().IsNull)
                            bl = cmtBll.Support(0, Convert.ToInt32(Request.Form["value"]), EnviorHelper.GetUserIP(), Convert.ToInt32(Request.Form["gid"]));
                        else
                            bl = cmtBll.Support(0, Convert.ToInt32(Request.Form["value"]), EnviorHelper.GetUserIP(), buser.GetLogin().UserID, buser.GetLogin().UserName, Convert.ToInt32(Request.Form["gid"]));
                        Response.Write(bl ? "1" : "0");
                        break;
                    case "reply"://回复
                        Response.Write(btnHuiFu());
                        break;
                    case "sender"://发送评论
                        Response.Write(SenderComm());
                        break;
                    default:
                        break;
                    #endregion
                }
                Response.Flush(); Response.End();
            }
            else if (!IsPostBack)
            {
                if (ItemID < 1) function.WriteErrMsg("内容ID错误");
                //获取节点配置
                M_CommonData cdata = bll.GetCommonData(ItemID);
                if (cdata == null) { function.WriteErrMsg("内容不存在"); }
                M_Node nodeMod = nodeBll.GetNodeXML(cdata.NodeID);
                if(nodeMod == null) { function.WriteErrMsg("节点不存在"); }
                C_Title = cdata.Title;
                ModelID = cdata.ModelID;
                switch (nodeMod.CommentType)
                {
                    case "0"://关闭
                        nocoment.Visible = true;
                        CommentInput.Visible = false;
                        return;
                    case "2"://注册用户
                        //islogin = buser.CheckLogin() ? 0 : 1;
                        break;
                    default://游客
                        break;
                }
                comentyes.Visible = true;
                RPT.ItemDataBound += RPT_ItemDataBound;
                MyBind();
            }

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            CommentDT = null;
        }
        private void MyBind()
        {
            int count = CommentDT.Rows.Count;
            CommCount_L.Text = cmtBll.GetUpCount(ItemID, -1).ToString();
            asscount.InnerText = cmtBll.GetUpCount(ItemID, 1).ToString();
            Label1.Text = count + "条";//评论总数
            //this.Label2.Text = pktrue.ToString();//支持
            //this.Label3.Text = pkfalse.ToString();//反对
            RPT.DataSource = CommentDT;
            RPT.DataBind();
            M_CommonData cdata = bll.GetCommonData(ItemID);
            M_Node nodeMod = nodeBll.GetNodeXML(cdata.NodeID);
            if (nodeMod == null)
            {
                nocoment.Visible = true;
                CommentInput.Visible = false;
                return;
            }
            if ((string.IsNullOrEmpty(nodeMod.CommentType) || nodeMod.CommentType.Equals("2")) && !buser.CheckLogin())
            {
                nologin.Visible = true;
                CommentInput.Visible = false;
                comentyes.Visible = false;
            }
                
        }
        //根据评论内容审核状态显示相应内容
        protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                string temphtml = "<div class='clearfix'></div><div class='replybody'>{5}<h5>{0}评论人：{1}<span class='comm-date'>@Level</span></h5>"
                                    + "<p class='list-group-item-text'><script>document.write(emotion.strToEmotion(\"{4}\"))</script></p><div class='text-right'>"
                                    + "<span class='comm_btns support' data-id='{6}' data-flag='1' onclick='Support(this)'>支持(<span class='count'>{7}</span>)</span>"
                                    + "<span class='comm_btns support' data-id='{6}' data-flag='0' onclick='Support(this)'>反对(<span class='count'>{8}</span>)</span>{3}"
                                    + "<span class='comm_btns' onclick='showHuiFu(this,{6})'>回复</span></div></div>";
                Literal lit = e.Item.FindControl("Commment_Lit") as Literal;
                int level = 0;
                lit.Text = SelChildComment(CommentDT, dr.Row, temphtml, ref level);
            }
        }
        //回复事件
        protected void Reply_B_Click(object sender, EventArgs e)
        {
            btnHuiFu();
        }
        //-------------Show
        public string GetPK(string pk)
        {
            if (DataConverter.CBool(pk))
            {
                return "我支持";
            }
            else
            {
                return "我反对";
            }
        }
        public string GetUserName(string uid)
        {
            try
            {
                string uname = buser.SeachByID(DataConverter.CLng(uid)).UserName;
                return string.IsNullOrEmpty(uname) ? "匿名" : uname;
            }
            catch
            {
                return "匿名";
            }
        }
        public string GetReport(string ids, string cid)
        {
            if (ids.IndexOf("," + buser.GetLogin().UserID + ",") > -1)
                return "<span class='comm_btns gray_9'>已举报</span>";
            else
                return "<span class='comm_btns' onclick='Report(this)' data-cid='" + cid + "'>举报</span>";
        }
        public string GetContent()
        {
            if (!DataConverter.CBool(Eval("Audited").ToString()))
            {
                return "已开启评论审核功能,待审核通过后即可显示";
            }
            else
            {
                string content = Eval("Contents", "").Replace("\"", "");
                return content;
            }
        }
        //-------------Tools
        private void AddComment(M_Comment cmtMod)
        {
            cmtBll.Add(cmtMod);
            if (SiteConfig.UserConfig.CommentRule > 0 && cmtMod.UserID>0)//增加积分
            {
                buser.ChangeVirtualMoney(cmtMod.UserID, new M_UserExpHis()
                {
                    score = SiteConfig.UserConfig.CommentRule,
                    detail = "发表评论增加积分",
                    ScoreType = (int)M_UserExpHis.SType.Point
                });
            }
        }
        private string SelChildComment(DataTable dt, DataRow dr, string temp, ref int level)
        {
            string str = "";
            dt.DefaultView.RowFilter = ""; dt = dt.DefaultView.ToTable();
            dt.DefaultView.RowFilter = "CommentID=" + dr["pid"];
            foreach (DataRow item in dt.DefaultView.ToTable().Rows)
            {
                string content = Convert.ToInt32(item["Audited"]) == 1 ? item["Contents"].ToString() : "<span class='comm_audited'><span class='fa fa-check-circle'></span>感谢回复,编辑正在审核中</span>";
                content = content.Replace("\"", "");
                str = string.Format(temp, "", GetUserName(item["UserID"].ToString()), GetPK(item["PKS"].ToString())
                                    , GetReport(item["ReprotIDS"].ToString(), item["CommentID"].ToString()), content, SelChildComment(dt, item, temp, ref level)
                                    , item["CommentID"], item["AgreeCount"], item["DontCount"]);
                str = str.Replace("@Level", (++level).ToString());
            }
            return str;
        }
        // 发表评论
        private string SenderComm()
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], Request.Form["VCode"])) { return "-1"; }
            //内容为空不允许发送
            if (string.IsNullOrEmpty(Request.Form["content"])) { return "-3"; }

            M_UserInfo mu = buser.GetLogin(false);
            M_Comment comment = new M_Comment();
            M_CommonData cdata = bll.GetCommonData(ItemID);
            comment.GeneralID = ItemID;
            //是否开放评论
            if (cdata.IsComm != 1) { return "-4"; }
            //节点是否开启评论权限
            M_Node nodeMod = nodeBll.SelReturnModel(cdata.NodeID);
            //需要登录,但用户未登录
            if (nodeMod.CommentType.Equals("2") && !buser.CheckLogin()) { return "-2"; }
            comment.UserID = mu.UserID;//支持一个支持匿名方法
            comment.Title = "";
            comment.Contents = BaseClass.CheckInjection(Request.Form["content"]);
            comment.Audited = true;
            DataTable dts = cmtBll.SeachComment_ByGeneralID2(ItemID);
            if (nodeMod.Purview != null && nodeMod.Purview != "")
            {
                string Purview = nodeMod.Purview;
                DataTable AuitDT = nodeBll.GetNodeAuitDT(nodeMod.Purview);
                if (AuitDT == null && AuitDT.Rows.Count <= 0) { return "-4"; }
                DataRow auitdr = AuitDT.Rows[0];
                string forum_v = auitdr["forum"].ToString();
                if (string.IsNullOrEmpty(forum_v)) { return "-4"; }
                string[] forumarr = forum_v.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //不允许评论
                if (!forumarr.Contains("1")) { return "-4"; }
                //不需要审核
                if (!forumarr.Contains("2")) { comment.Audited = true; }
                if (forumarr.Contains("3")) //一个文章只评论一次
                {
                    if (cmtBll.SearchByUser(mu.UserID, cdata.NodeID).Rows.Count > 0) { return "-5"; }
                }
            }
            comment.Status = 0;
            comment.CommentTime = DateTime.Now;
            AddComment(comment);
            return comment.Audited ? "2" : "1";
        }
        //回复
        private string btnHuiFu()
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], Request.Form["VCode"]))
                return "-1";
            M_UserInfo mu = buser.GetLogin();
            M_Comment comment = new M_Comment();
            comment.CommentID = 0;
            comment.GeneralID = ItemID;
            M_CommonData cdata = bll.GetCommonData(ItemID);
            M_Node mnode = nodeBll.GetNodeXML(cdata.NodeID);
            if (mnode.CommentType.Equals("2") && !buser.CheckLogin())
                return "-2";
            comment.UserID = mu.UserID;
            comment.Title = "";
            if (string.IsNullOrEmpty(Request.Form["content"]))
                return "-3";
            comment.Contents = BaseClass.CheckInjection(Request.Form["content"]);
            comment.Audited = true;
            comment.CommentTime = DateTime.Now;
            comment.Status = 0;
            comment.Pid = DataConverter.CLng(Request.Form["id"]);
            AddComment(comment);
            return "1";
        }
    }
}