using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*
 * 本页仅用于展示并让用户选取控件
 * 点击后相当于直接确定,将组件添加进去,然后用户拖动
 * 每一个组件的设置页,是一个单独的html,通过ajax将其加载进来
 * 
 * *不同的组件ID,可以代表相同的组件不同的参数
 */

namespace ZoomLaCMS.Design.Diag
{
    public partial class AddComp : System.Web.UI.Page
    {
        //需要展示的组件类型
        public string SType { get { return Request.QueryString["stype"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            DataTable dt = GetCateByType(SType);
            CateRPT.DataSource = dt;
            CateRPT.DataBind();
        }
        private DataTable GetCateByType(string stype)
        {
            //盒子:box,外形边框:shape,列表:list,博客:blog,店铺:store,组件市场:more,音乐:music,社交:social,联系方式:contact
            string compArr = "";

            DataTable dt = new DataTable();
            dt.Columns.Add("Alias", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            switch (stype)
            {
                case "mobile":
                    compArr = "基础:mb_basic,展示:mb_view,互动:mb_pub,高级:mb_adv";
                    break;
                case "ipad":
                    break;
                case "scence":
                default:
                    compArr = "文字:text,布局:div,图文:ueditor,按钮:button,图片:image,数据:list,相册:gallery,菜单:menu,互动:pub,";
                    compArr += "视频:video,音乐:music,地图:map,分享:social,形状:shape,其它:other";
                    break;
            }
            foreach (string comp in compArr.Split(','))
            {
                if (string.IsNullOrEmpty(comp)) continue;
                DataRow dr = dt.NewRow();
                dr["Alias"] = comp.Split(':')[0];
                dr["Name"] = comp.Split(':')[1];
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}