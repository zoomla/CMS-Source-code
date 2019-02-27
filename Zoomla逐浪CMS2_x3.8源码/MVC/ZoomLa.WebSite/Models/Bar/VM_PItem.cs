using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Message;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Message;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Models.Bar
{
    public class VM_PItem
    {
        B_Guest_Bar barBll = new B_Guest_Bar();
        B_Guest_BarAuth authBll = new B_Guest_BarAuth();
        B_GuestBookCate cateBll = new B_GuestBookCate();
        B_Plat_Like likeBll = new B_Plat_Like();
        B_TempUser tuBll = new B_TempUser();
        B_Guest_Medals medalBll=new B_Guest_Medals();
        public M_Guest_Bar barMod = null;
        public M_GuestBookCate cateMod = null;
        public M_UserInfo mu = null;
        //贴子列表
        public PageSetting setting = null;
        //是否为贴吧管理员
        public bool auth_barowner = false;
        //发贴是否需登录发布
        public bool auth_send = true;
        public bool auth_edit = false;//是否拥有修改权限(管理员|发贴人)
        public bool auth_like = false;
        public DataTable likeDT = null;
        public DataTable mediaDT = null;//该贴所获取的奖章
        //贴子数,回复数
        public int tiecount = 0, recount = 0;
        //违规或开启审核,需要隐藏贴子
        public bool HidePost = false;
        //-----------------
        private string AlertTlp = "<div class='alert alert-danger' role='alert'><span class='fa fa-exclamation-circle' aria-hidden='true'></span><span class='sr-only'>提示:</span>该贴子内容已被屏蔽！您拥有管理权限，以下是贴子内容</div>";
        private string UserAlertTlp = "<div class='alert alert-danger' role='alert'><span class='fa fa-exclamation-circle' aria-hidden='true'></span><span class='sr-only'>提示:</span>该贴子内容已被屏蔽！</div>";
        public VM_PItem(HttpRequestBase Request, int cpage, int psize) 
        {
            barMod = barBll.SelReturnModel(DataConvert.CLng(Request.QueryString["id"]));
            if (barMod == null) { function.WriteErrMsg("该贴子不存在!!"); }
            cateMod = cateBll.SelReturnModel(barMod.CateID);
            mu = tuBll.GetLogin();
            if (barMod.Status != (int)ZLEnum.ConStatus.Audited && cateMod.Status != 1)
            {
                function.WriteErrMsg("该贴子需要审核通过才能浏览！");
            }
            if (cateMod.IsBarOwner(mu.UserID))//吧主
            {
                auth_barowner = true;
                auth_edit = true;
            }
            else
            {
                if (!authBll.AuthCheck(cateMod, mu))
                {
                    function.WriteErrMsg("你没有访问权限或未登录,请<a href='/User/Login?Returnurl=/PItem?id=" + barMod.ID + "&cpage=" + cpage + "'>登录</a>后查看");
                }
                if ((!authBll.AuthCheck(cateMod, mu, "send")))
                {
                    auth_send = false;
                }
            }
            if (barMod.CUser == mu.UserID) { auth_edit = true; }
            setting = barBll.SelByID(cpage, psize, barMod, Request.QueryString["Filter"]);
            barBll.AddHitCount(barMod.ID);
            barMod.HitCount++;
            string msgids = "";
            for (int i = 0; i < setting.dt.Rows.Count; i++)
            {
                msgids += setting.dt.Rows[i]["ID"].ToString() + ",";
            }
            likeDT = likeBll.SelByMsgIDS(msgids.Trim(','), "bar");
            mediaDT = medalBll.SelByBarIDS(msgids.Trim(','));
            if (barMod.Status < 0 && barMod.Status == (int)ZLEnum.ConStatus.Recycle) function.WriteErrMsg("该帖子已删除!!", "/PClass?id=" + barMod.CateID);
            auth_like = !barMod.ColledIDS.Contains("," + mu.UserID + ",");
        }
        //游客不显示删除与编辑操作
        public MvcHtmlString GetDel(DataRow dr, int type = 0)
        {
            int uid = DataConvert.CLng(dr["CUser"]);
            int isfirst = Convert.ToInt32(dr["Pid"]) == 0 ? 1 : 2;
            string result = "";
            if (((mu.UserID == uid && uid > 0) || auth_barowner) && type == 0)
            {
                result = "<a href='/EditContent?ID=" + dr["ID"] + "' title='编辑' style='margin-right:5px;'><span class='fa fa-pencil'></span></a> <a href='javascript:;' onclick='PostDelMsg(" + dr["ID"] + "," + isfirst + ")' title='删除'><span class='fa fa-trash-o'></span> </a>" + (auth_barowner ? "<input type='checkbox' name='idchk' value='" + dr["ID"] + "'/>" : "");
            }
            else if (((mu.UserID == uid && uid > 0) || auth_barowner) && type == 1)
            {
                result = "<a href='/EditContent?ID=" + dr["ID"] + "'>编辑</a><a href='javascript:;' onclick='PostDelMsg(" + dr["ID"] + "," + isfirst + ")'>删除</a>";
            }
            return MvcHtmlString.Create(result);
        }
        public MvcHtmlString GetMsg(DataRow dr)
        {
            string result = "";
            //if (HidePost)
            //{
            //    result = HideTlp;
            //}
            if (DataConvert.CLng(dr["Status"]) == (int)ZLEnum.ConStatus.Recycle)
            {
                if (auth_barowner)
                {
                    result = AlertTlp + StrHelper.DecompressString(dr["MsgContent"].ToString());
                }
                else
                {
                    result = UserAlertTlp;
                }
            }
            else
            {
                result = StrHelper.DecompressString(dr["MsgContent"].ToString());
            }
            return MvcHtmlString.Create(result);
        }
        public string GetUserInfo(string str)
        {
            return "";
            //int id = DataConvert.CLng(Eval("CUser"));
            //if (str.Equals("groupName"))
            //{
            //    userinfo = buser.GetUserByUserID(id);
            //    return bgp.GetByID(DataConverter.CLng(userinfo.GroupID)).GroupName;
            //}
            //else if (str.Equals("count"))
            //{
            //    DataTable dt = new DataTable();
            //    dt = barBll.SelByCateID(id.ToString(), 2);
            //    return dt.Rows.Count + "";
            //}
            //else if (str.Equals("userBirth"))
            //{
            //    users = buser.GetUserBaseByuserid(id);
            //    return users.BirthDay;
            //}
            //else if (str.Equals("userExp"))
            //{
            //    userinfo = buser.GetUserByUserID(id);
            //    return userinfo.UserExp + "";
            //}
            //else if (str.Equals("userSex"))
            //{
            //    users = buser.GetUserBaseByuserid(id);
            //    if ((users.UserSex + "") == "False")
            //    {
            //        return "女";
            //    }
            //    else
            //    {
            //        return "男";
            //    }

            //}
            //else if (str.Equals("regTime"))
            //{
            //    DateTime datetime = tuBll.GetLogin().RegTime;
            //    return datetime.Year + "-" + datetime.Month + "-" + datetime.Day;
            //}
            //else
            //{
            //    return "";
            //}
        }
        public string GetHref(DataRow dr)
        {
            int uid = DataConvert.CLng(dr["CUser"]);
            string result = "javascript:;";
            if (uid > 0)
                result = "PostSearch?uid=" + uid;
            return result;
        }
        //--------------------Like
        public int GetLikeNum(DataRow dr)
        {
            if (likeDT == null) { return 0; }
            return likeDT.Select("MsgID=" + dr["ID"] + " AND Source='bar'").Length;
        }
        public MvcHtmlString ShowLikeUser(DataRow item)
        {
            DataRow[] drs = likeDT.Select("MsgID=" + item["ID"] + " AND Source='bar'");
            string result = "";
            foreach (DataRow dr in drs)
            {
                string uname = string.IsNullOrEmpty(DataConvert.CStr(dr["UserName"])) ? dr["UserName"].ToString() : dr["UserName"].ToString();
                result += "<li title='" + uname + "' data-uid='" + dr["CUser"] + "' class='likeids_li'><a href='javascript:;'><img src='" + dr["salt"] + "' onerror=\"this.src='/Images/userface/noface.png';\"/></a></li>";
            }
            return MvcHtmlString.Create(result);
        }
        //--------------------Medals
        public MvcHtmlString GetMedalBtn(DataRow dr)
        {
            if (mu == null || mu.UserID <= 0) { return MvcHtmlString.Create(""); }//匿名用户与回复贴不能颁发勋章
            int medalnum = 0;
            string disclass = "";//禁用样式
            if (mediaDT != null)
            {
                mediaDT.DefaultView.RowFilter = "BarID=" + DataConvert.CLng(dr["ID"]);
                DataTable curDt = mediaDT.DefaultView.ToTable();
                medalnum = curDt.Rows.Count;
                curDt.DefaultView.RowFilter = "Sender=" + mu.UserID;
                disclass = curDt.DefaultView.Count > 0 ? "medal_btn_dis" : "";
            }
            return MvcHtmlString.Create("<li style='width:auto;'><a title='颁发勋章' onclick='AddMedal(this," + DataConvert.CLng(dr["ID"]) + ")' href='javascript:;' class='likeids_div_a " + disclass + "'><i class='fa fa-sun-o'></i><span class='likenum_span medalnum_btn'>(" + medalnum + ")</span></a></li>");
        }
        public MvcHtmlString ShowMedalList(DataRow dr)
        {
            if (mediaDT != null)
            {
                mediaDT.DefaultView.RowFilter = "BarID=" + DataConvert.CLng(dr["ID"]);
                DataTable curdt = mediaDT.DefaultView.ToTable();
                return MvcHtmlString.Create(medalBll.GetMedalIcon(curdt));
            }
            return MvcHtmlString.Create("");
        }
    }
}