using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using System.Text.RegularExpressions;
using ZoomLa.SQLDAL;
using ZoomLa.Common;

namespace ZoomLaCMS.Guest.Baike
{
    public partial class Default : System.Web.UI.Page
    {
        B_Baike bkBll = new B_Baike();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindType();//词条
                bindSee();//关注事件
                bindElite();//推荐
                bindUser();//百科之星
                bindAlter();
                bindCreat();
                bindBrief();
                Bindmans();
                BindTS();
                bindElitepicture();
                bindDateTimeNow();
            }
        }
        public string NoHTML(string content) { return StringHelper.StripHtml(content); }
        protected void bindType()
        {
            DataTable dt = B_GradeOption.GetGradeList(3, 0);
            BType_RPT.DataSource = dt;
            BType_RPT.DataBind();
        }

        protected void BType_RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)//指触发的类型为DadaList中的基本行或内容行
            {
                Repeater rep = e.Item.FindControl("Repeater2") as Repeater;//找到里层的repeater对象
                DataRowView rowv = (DataRowView)e.Item.DataItem;//找到分类Repeater关联的数据项 
                int Gid = DataConvert.CLng(rowv["GradeID"]);
                DataTable dt = B_GradeOption.GetGradeListTop(3, Gid, 3);
                rep.DataSource = dt;
                rep.DataBind();
            }
        }

        protected void bindSee()
        {
            DataTable dt = bkBll.SelectSee(10, "status>0", " AddTime desc");
            Repeater3.DataSource = dt;
            Repeater3.DataBind();
        }

        protected void bindElite()
        {
            DataTable dt = bkBll.SelectSee(1, " status>0 And Elite=1", " AddTime desc");
            Elite_RPT.DataSource = dt;
            Elite_RPT.DataBind();
        }

        protected void bindDateTimeNow()
        {
            DataTable dt = bkBll.SelectSee(1, " status>0 and tittle like '%" + DateTime.Now.Month + "%' and tittle like '%" + DateTime.Now.Day + "%'", " AddTime desc");
            Repeater9.DataSource = dt;
            Repeater9.DataBind();
        }

        protected void bindElitepicture()
        {
            DataTable dt = bkBll.SelectSee(1, " status>0 And Elite=3", " AddTime desc");
            picture.DataSource = dt;
            picture.DataBind();
        }

        protected void bindBrief()
        {
            DataTable dt = bkBll.SelectSee(10, "datediff(day, getdate(),AddTime)=0 and status>0", "AddTime desc");
            Youkown.DataSource = dt;
            Youkown.DataBind();
        }

        protected void bindUser()
        {
            DataTable dt = bkBll.SelectSee(10, "datediff(week,getdate(),AddTime)<1", " AddTime desc");
            B_User buser = new B_User();
            DataTable dt2 = new DataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt2 = buser.GetUserBaseByuserid(dt.Rows[i]["UserID"].ToString());
                }
                Repeater5.DataSource = dt2;
                Repeater5.DataBind();
            }
        }

        protected void bindAlter()
        {
            DataTable dt = bkBll.SelectSee(6, "status=2", " AddTime desc");
            Repeater6.DataSource = dt;
            Repeater6.DataBind();
        }

        protected void bindCreat()
        {
            DataTable dt = bkBll.SelectSee(15, "status=-1", " AddTime desc");
            Repeater7.DataSource = dt;
            Repeater7.DataBind();
        }

        protected void BindTS()
        {
            DataTable dt = bkBll.SelectSee(1, "status>0 and Elite=2", "AddTime desc");
            Repeater8.DataSource = dt;
            Repeater8.DataBind();
        }

        protected void Bindmans()
        {
            DataTable dt = bkBll.SelectSee(1, "status>0 and Classification='人物'", "AddTime desc");
            mans.DataSource = dt;
            mans.DataBind();
        }
        public string GetLeftString(string str)
        {
            if (str.Length <= 12)
                return str;
            return str.Substring(0, 12) + "..."; ;
        }
        protected string getstyle()
        {
            if (buser.CheckLogin())
            {
                return "display:inline-table";
            }
            else return "display:none";
        }
        protected string getstyles()
        {
            if (buser.CheckLogin())
            {
                return "display:none";
            }
            else return "display:inline-table";
        }
    }
}