namespace ZoomLaCMS.Manage.Guest
{
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
    using System.IO;
    using ZoomLa.Components;

    public partial class GuestCateMana : System.Web.UI.Page
    {
        B_Guest_Bar barBll = new B_Guest_Bar();
        B_GuestBookCate cateBll = new B_GuestBookCate();
        public string GType { get { return string.IsNullOrEmpty(Request.QueryString["Type"]) ? "0" : "1"; } }
        private int DelCateID { get { return DataConverter.CLng(Request.QueryString["Del"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                #region AJAX请求
                string action = Request.Form["action"];
                string value = Request.Form["value"];
                string result = "";
                switch (action)
                {
                    case "GetChild":
                        int id = DataConverter.CLng(value);
                        DataTable dt = cateBll.Cate_SelByType(M_GuestBookCate.TypeEnum.PostBar, id);
                        dt.Columns.Add(new DataColumn("NLogStr", typeof(string)));
                        dt.Columns.Add(new DataColumn("CountStr", typeof(string)));
                        //countdt=barBll.SelYTCount(id.ToString());
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            dr["BarInfo"] = GetBarStatus(dr["BarInfo"].ToString());
                            dr["NLogStr"] = GetNeedLog(dr["NeedLog"].ToString());
                            dr["CountStr"] = GetCount(dr["CateID"].ToString());
                        }
                        dt.Columns.Remove("desc");
                        result = JsonHelper.JsonSerialDataTable(dt);
                        Response.Write(result);
                        break;
                }
                Response.Flush(); Response.End();
                #endregion
            }
            if (!IsPostBack)
            {
                B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "BarManage");
                if (DelCateID > 0)
                {
                    GuestConfigInfo guestinfo = GuestConfig.GuestOption;
                    GuestConfig config = new GuestConfig();
                    BarOption baroption = guestinfo.BarOption.Find(v => v.CateID == DelCateID);
                    if (baroption != null) { guestinfo.BarOption.Remove(baroption); }
                    cateBll.Del(DelCateID);
                    config.Update(guestinfo);
                }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx" + "'>工作台</a></li><li class='active'><a href='/admin/Guest/GuestCateMana.aspx'>栏目管理</a></li><li class='active'>[<a href='javascript:;' onclick='ShowCate(0,0);' id='showDiv'>添加栏目</a>] [<a href='javascript:ShowOrder(0);'>排序管理</a>] [<a href='TieList.aspx?status=" + (int)ZLEnum.ConStatus.Recycle + "'>回收站</a>]</li>" + Call.GetHelp(92));
            }
        }
        private void MyBind()
        {
            DataTable dt = cateBll.Cate_SelByType(M_GuestBookCate.TypeEnum.PostBar, 0);
            RPT.DataSource = dt;
            //string cateids = "";
            //foreach (DataRow dr in dt.Rows)
            //{
            //    cateids += dr["CateID"].ToString() + ",";
            //}
            //cateids = cateids.TrimEnd(',');
            RPT.DataBind();
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //LinkButton lb = (LinkButton)sender;
            //GridViewRow gvr = (GridViewRow)lb.NamingContainer;

            //string Id = EGV1.DataKeys[gvr.RowIndex].Value.ToString();
            //B_GuestBook.DelCate(DataConverter.CLng(Id));
            //MyBind();
        }
        public string GetBarStatus(string barInfo)
        {
            string strcolor = "black";
            string restr = "普通";
            if (!string.IsNullOrWhiteSpace(barInfo) && barInfo.Contains("Recommend"))
            {
                strcolor = "blue";
                restr = "推荐";
            }
            return "<span style='color:" + strcolor + "'>" + restr + "</span>";
        }
        protected void BtnSetRecomPosation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] ids = Request.Form["idchk"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in ids)
                {
                    M_GuestBookCate mc = cateBll.SelReturnModel(Convert.ToInt32(item));
                    mc.BarInfo = "Recommend";
                    cateBll.UpdateByID(mc);
                }
                function.WriteSuccessMsg("操作完成！");
            }
        }
        protected void BtnSetChanlnePosation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] ids = Request.Form["idchk"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in ids)
                {
                    M_GuestBookCate mc = cateBll.SelReturnModel(Convert.ToInt32(item));
                    mc.BarInfo = "";
                    cateBll.UpdateByID(mc);
                }
                function.WriteSuccessMsg("操作完成！");
            }
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        public string GetNeedLog(string needlog)
        {
            string result = "";
            switch (needlog)
            {
                case "0":
                    result = "允许匿名";
                    break;
                case "1":
                    result = "登录用户";
                    break;
                case "2":
                    result = "指定用户";
                    break;
                default:
                    result = "未知";
                    break;
            }
            return result;
        }
        public string GetCateName()
        {
            string url = "";
            url = string.Format("ShowCate({0},0)", Eval("CateID"));
            string linkName = string.Format("<a href=\"javascript:;\" onclick=\"{0}\">{1}</a>", url, Eval("CateName"));
            return linkName;
        }
        //总数/今日/昨日
        public string GetCount(string cateid)
        {
            string result = "";
            string[] countArr = barBll.SelYTCount(cateid).Split(':');
            result = "<span>" + countArr[0] + "</span>/<span>" + countArr[1] + "</span>/<span>" + countArr[2] + "</span>";
            return result;
        }

        protected void Dels_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                cateBll.DelByCateIDS(Request.Form["idchk"]);
            }
            function.WriteSuccessMsg("批量删除成功!");
        }
    }
}