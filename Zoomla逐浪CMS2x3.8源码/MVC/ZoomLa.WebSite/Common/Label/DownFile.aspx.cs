namespace ZoomLaCMS.Common.Label
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.HtmlLabel;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    using ZoomLa.BLL.Content;
    using ZoomLa.Model.Content;
    using System.IO;
    public partial class LabelDownFile1 : System.Web.UI.Page
    {
        B_Content conBll = new B_Content();
        DownField downBll = new DownField();
        B_Content_FileBuy buyBll = new B_Content_FileBuy();
        B_User buser = new B_User();
        public int Gid { get { return DataConvert.CLng(Request.QueryString["Gid"]); } }
        //该字段不可开放
        public string Field { get { return Request.QueryString["Field"]; } }
        public string Ranstr { get { return Request.QueryString["Ranstr"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            //139  xzzd auGUaUnpbi
            if (!IsPostBack)
            {
                DownFile(Gid, Field);
            }
        }
        public void DownFile(int gid, string field)
        {
            M_UserInfo mu = buser.GetLogin(false);
            DataTable dt = new DataTable();
            M_CommonData conMod = conBll.SelReturnModel(gid);
            dt = conBll.GetContentByItems(conMod.TableName, conMod.GeneralID);
            if (dt.Columns.Contains(field))
            {
                string json = dt.Rows[0][field].ToString();
                M_Field_Down model = downBll.GetModel(json, Ranstr);
                M_Content_FileBuy buymod = buyBll.SelByGid(mu.UserID, gid, Ranstr);
                if (model == null) { function.WriteErrMsg("未找到匹配的下载信息"); }
                else if (model.price > 0)
                {
                    if (mu.IsNull || mu.UserID < 1) { function.WriteErrMsg("该文件必须登录后才可下载,<a href='/User/Login'>点击登录</a>"); return; }
                    if (buymod == null || buymod.EndDate < DateTime.Now)//没有文件记录或已过期
                    {
                        if (!HasEnoughMoney(mu, model.ptype, model.price))
                        { function.WriteErrMsg("金额不足,需要" + model.price + "" + GetMName(model.ptype)); }
                        else
                        {
                            buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis() { score = (int)-model.price, ScoreType = buser.GetTypeByStr(model.ptype), detail = "下载文件扣除" + model.price + GetMName(model.ptype) });
                            if (buymod == null)//插入
                            {
                                buyBll.InsertLog(gid, Ranstr, mu, model, field);
                            }
                            else//修改
                            {
                                buyBll.UpdateLog(mu.UserID, gid, Ranstr, model);
                            }

                        }
                    }
                }
                else
                {
                    if (buymod == null || buymod.EndDate < DateTime.Now)
                    {
                        if (buymod == null)//插入
                        {
                            buyBll.InsertLog(gid, Ranstr, mu, model, field);
                        }
                        else//修改
                        {
                            buyBll.UpdateLog(mu.UserID, gid, Ranstr, model);
                        }
                    }
                }
                model.count++;
                downBll.UpdateByList(conMod.TableName, Field, DataConvert.CLng(dt.Rows[0]["ID"].ToString()));
                if (model.url.ToLower().Contains("http://") || model.url.ToLower().Contains("https://")) { Response.Redirect(model.url); }
                else { SafeSC.DownFile(model.url, Path.GetFileName(model.url)); }
                Response.End();
            }
            else
            {
                function.WriteErrMsg("下载信息不存在");
            }
        }
        private bool HasEnoughMoney(M_UserInfo mu, string ptype, double price)
        {
            switch (ptype)
            {
                case "purse":
                    return mu.Purse >= price;
                case "point":
                    return mu.UserExp >= price;
                case "sicon":
                    return mu.SilverCoin >= price;
                default:
                    return false;
            }
        }
        private string GetMName(string ptype)
        {
            switch (ptype)
            {
                case "purse":
                    return "余额";
                case "point":
                    return "积分";
                case "sicon":
                    return "银币";
                default:
                    return "现金";
            }
        }
    }
}