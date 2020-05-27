using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

/*
 * 显示权限控制,列出,需要控制处理显示,另分下,发件经办与抄送
 * 抄送和我们经办的
 * 待办:View=1:当前进行的步骤是我们有权办理的(同意||拒绝||需指定下一步骤的自由流程,不会在此显示)
 * 已办:View=2:我们曾经批复过的
 */

namespace ZoomLaCMS.Plat.OA
{
    public partial class AffairsList : System.Web.UI.Page
    {
        protected B_User buser = new B_User();
        protected B_OA_Document oaBll = new B_OA_Document();
        protected M_OA_Document oaMod = new M_OA_Document();
        /// <summary>
        /// 需要查看处理哪个状态的公文与事务
        /// </summary>
        public string CurrentView
        {
            get
            {
                if (ViewState["CurrentView"] == null)
                    ViewState["CurrentView"] = Request.QueryString["view"];
                return ViewState["CurrentView"] as string;
            }
            set
            {
                ViewState["CurrentView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            EGV.txtFunc = txtPageFunc;
            if (!IsPostBack)
            {
                switch (CurrentView)
                {
                    case "1":
                        Label1.Text = "待办事务";
                        break;
                    case "2":
                        Label1.Text = "已办事务";
                        break;
                    case "3":
                        Label1.Text = "收文管理";
                        break;
                    case "4":
                        Label1.Text = "传阅管理";
                        break;
                }
                DataBind();
            }
        }
        protected void DataBind(string keys = "")
        {
            DataTable dt = new DataTable();
            int userID = buser.GetLogin().UserID;

            string docType = Request.QueryString["DocType"];

            if (string.IsNullOrEmpty(docType) || docType.Equals("0"))//公文
            {
                switch (CurrentView)
                {
                    case "1"://待办有经办权限的
                        dt = oaBll.SelDocByUid(userID);
                        break;
                    case "2"://已办
                        dt = oaBll.SelByApproveID(userID, 0);
                        break;
                }
            }
            else//事务
            {
                switch (CurrentView)
                {
                    case "1"://待办有经办权限的
                        dt = oaBll.SelDocByUid(userID);
                        break;
                    case "2"://已办
                        dt = oaBll.SelByApproveID(userID, 1);
                        break;
                }
            }
            string key = SearchKey;
            if (!string.IsNullOrEmpty(key))
            {
                dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
                dt = dt.DefaultView.ToTable();
            }
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        private string SearchKey
        {
            get
            {
                return ViewState["SearchKey"] as string;
            }
            set
            {
                ViewState["SearchKey"] = value;
            }

        }
        //处理页码
        public void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = EGV.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = EGV.PageSize;
            }
            EGV.PageSize = pageSize;
            EGV.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del1":
                    //boad.DeleteByID(e.CommandArgument.ToString());
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功');", true);
                    //DataBind();
                    return;
            }
        }
        public string GetType(string type)
        {
            switch (type)
            {
                case "0":
                    return "未定义";
                case "1":
                    return "发文演示模版";
                case "2":
                    return "办公会抄告单";
                case "3":
                    return "党办发文模版";
                case "4":
                    return "党委会抄告单";
                case "5":
                    return "党委会议纪要";
                case "6":
                    return "纪检发文模版";
                case "7":
                    return "行政办公会抄告单";
                case "8":
                    return "行政公文模版(上行文)";
                case "9":
                    return "行政公文模版(下行文)";
                case "10":
                    return "院长办公会议纪要";
                default:
                    return "未定义";
            }
        }
        public string GetSecret(string Secret)
        {
            //1:一般,2:秘密,3:机密,4:绝密
            switch (Secret)
            {
                case "1":
                    return "一般";
                case "2":
                    return "秘密";
                case "3":
                    return "机密";
                case "4":
                    return "绝密";
                default:
                    return "";
            }
        }
        public string GetImport(string importance)
        {
            //1:一般,2:较重要,3:重要,4:非常重要
            switch (importance)
            {
                case "1":
                    return "一般";
                case "2":
                    return "较重要";
                case "3":
                    return "重要";
                case "4":
                    return "非常重要";
                default:
                    return "";
            }
        }
        public string GetUrgency(string Urgency)
        {
            //1:一般,2:较紧急,3:紧急,4:很紧急,5:非常紧急
            switch (Urgency)
            {
                case "1":
                    return "一般";
                case "2":
                    return "较紧急";
                case "3":
                    return "紧急";
                case "4":
                    return "很紧急";
                case "5":
                    return "非常紧急";
                default:
                    return "";
            }
        }
        public string GetStatus(string Status)
        {
            string flag = "";
            switch (Status)
            {
                case "-1":
                    flag = "<span style='color:red;'>不同意</span>";
                    break;
                case "2":
                    flag = "<span style='color:green;'>进行中</span>";
                    break;
                case "98":
                    flag = "<span style='color:blue;'>进行中</span>";
                    break;
                case "99":
                    flag = "<span style='color:blue;'>已完成</span>";
                    break;
                case "0":
                    flag = "<span style='color:green;'>进行中</span>";
                    break;
                default:
                    flag = "<span style='color:blue;'>进行中</span>";
                    break;
            }
            return flag;
        }

        Call commCall = new Call();
        protected void singleBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(pageData.Value.Split(':')[0]);
            int nodeID = Convert.ToInt32(pageData.Value.Split(':')[1]);
            oaMod = oaBll.SelReturnModel(id);
            commCall.AddContentToNode(oaMod, nodeID, OAConfig.ModelID);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('分发成功!!!');", true);
        }
    }
}