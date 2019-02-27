using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Data;
using System.Text;

namespace ZoomLaCMS.Manage.User
{
    public partial class UserSitePermission : CustomerPageAction
    {
        B_Node nll = new B_Node();
        B_Model mll = new B_Model();
        B_ModelField fll = new B_ModelField();
        private B_Structure strBll = new B_Structure();
        private M_Structure strMod = new M_Structure();
        protected string type = "添加角色";
        protected string Username;
        B_User bll = new B_User();
        B_Admin ba = new B_Admin();
        int UserID;
        string fieldlistvalues;
        protected void Page_Load(object sender, EventArgs e)
        {
            string menu = Request.QueryString["menu"];
            UserID = DataConverter.CLng(Request.QueryString["uid"]);
            if (!IsPostBack)
            {
                M_UserInfo muser = bll.GetUserByUserID(UserID);
                Username = muser.UserName;
                rbl.SelectedIndex = 1;
                rbl_SelectedIndexChanged(null, null);
                nodelistload(0);
                Modellistload();
                DataSet ds = new DataSet();
                if (ds.Tables.Count > 0)
                {
                    #region 设置选择节点
                    string nodelistvalues = ds.Tables[0].Rows[0]["Nodelist"].ToString();
                    nodelistvalues = nodelistvalues.TrimEnd(new char[] { ',' });
                    nodelistvalues = nodelistvalues.TrimStart(new char[] { ',' });
                    //节点默认选中
                    SelectList(Nodelist_LB, nodelistvalues);
                    #endregion

                    #region 设置选择模型字段
                    fieldlistvalues = ds.Tables[0].Rows[0]["Fieldlist"].ToString();
                    fieldlistvalues = fieldlistvalues.TrimEnd(new char[] { ',' });
                    fieldlistvalues = fieldlistvalues.TrimStart(new char[] { ',' });
                    #endregion

                    #region 设置模型表
                    string datalistvalues = ds.Tables[0].Rows[0]["DataList"].ToString();
                    datalistvalues = datalistvalues.TrimEnd(new char[] { ',' });
                    datalistvalues = datalistvalues.TrimStart(new char[] { ',' });
                    //数据库列表默认选中
                    SelectList(DataList_LB, datalistvalues);
                    #endregion

                    #region 设置时段浏览
                    if (DataConverter.CLng(ds.Tables[0].Rows[0]["Time"].ToString()) == 1)
                    {
                        rblTime.SelectedIndex = 0;
                        rblTime_SelectedIndexChanged(null, null);
                        //月份
                        string Month = ds.Tables[0].Rows[0]["Month"].ToString();
                        Month = Month.TrimEnd(new char[] { ',' });
                        Month = Month.TrimStart(new char[] { ',' });
                        SelectList(CblMonth, Month);
                        //日期
                        string Day = ds.Tables[0].Rows[0]["Day"].ToString();
                        Day = Day.TrimEnd(new char[] { ',' });
                        Day = Day.TrimStart(new char[] { ',' });
                        SelectList(cblDay, Day);
                        //小时
                        string Hour = ds.Tables[0].Rows[0]["Hour"].ToString();
                        Hour = Hour.TrimEnd(new char[] { ',' });
                        Hour = Hour.TrimStart(new char[] { ',' });
                        SelectList(cblHour, Hour);
                        //星期
                        string Weeks = ds.Tables[0].Rows[0]["Weeks"].ToString();
                        Weeks = Weeks.TrimEnd(new char[] { ',' });
                        Weeks = Weeks.TrimStart(new char[] { ',' });
                        SelectList(cblWeeks, Weeks);
                    }
                    else
                    {
                        rblTime.SelectedIndex = 1;
                        rblTime_SelectedIndexChanged(null, null);
                    }


                    #endregion

                    #region 设置内容操作
                    //允许内容浏览
                    if (ds.Tables[0].Rows[0]["ViewContent"].ToString() == "1")
                    {
                        this.ViewContent.SelectedValue = "1";
                    }
                    else
                    {
                        this.ViewContent.SelectedValue = "0";
                    }
                    //允许列表浏览
                    if (ds.Tables[0].Rows[0]["ListContent"].ToString() == "1")
                    {
                        this.ListContent.SelectedValue = "1";
                    }
                    else
                    {
                        this.ListContent.SelectedValue = "0";
                    }
                    //允许新增发布
                    if (ds.Tables[0].Rows[0]["AddContent"].ToString() == "1")
                    {
                        this.AddContent.SelectedValue = "1";
                    }
                    else
                    {
                        this.AddContent.SelectedValue = "0";
                    }
                    //允许编辑修改
                    if (ds.Tables[0].Rows[0]["ModefiyContent"].ToString() == "1")
                    {
                        this.ModefiyContent.SelectedValue = "1";
                    }
                    else
                    {
                        this.ModefiyContent.SelectedValue = "0";
                    }
                    //允许删除内容
                    if (ds.Tables[0].Rows[0]["DeleteContent"].ToString() == "1")
                    {
                        this.DeleteContent.SelectedValue = "1";
                    }
                    else
                    {
                        this.DeleteContent.SelectedValue = "0";
                    }
                    //允许评论权限
                    if (ds.Tables[0].Rows[0]["AddComm"].ToString() == "1")
                    {
                        this.AddComm.SelectedValue = "1";
                    }
                    else
                    {
                        this.AddComm.SelectedValue = "0";
                    }
                    #endregion
                }
            }
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>用户管理</li><li>会员管理</li><li><a href='UserManage.aspx'>会员列表</a></li><li>设置 <strong>" + Username + "</strong> 会员权限</li>");
        }

        private void SelectList(ListBox lb, string str)
        {
            if (str != "" && str != ",,")
            {
                if (str.IndexOf(",") > 0)
                {
                    string[] nodearr = str.Split(new string[] { "," }, StringSplitOptions.None);
                    for (int i = 0; i < nodearr.Length; i++)
                    {
                        for (int p = 0; p < lb.Items.Count; p++)
                        {
                            if (nodearr[i].ToString() == lb.Items[p].Value)
                            {
                                lb.Items[p].Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    lb.SelectedValue = str;
                }
            }
        }

        private void SelectList(CheckBoxList cb, string str)
        {
            if (str != "" && str != ",,")
            {
                if (str.IndexOf(",") > 0)
                {
                    string[] nodearr = str.Split(new string[] { "," }, StringSplitOptions.None);
                    for (int i = 0; i < nodearr.Length; i++)
                    {
                        for (int p = 0; p < cb.Items.Count; p++)
                        {
                            if (nodearr[i].ToString() == cb.Items[p].Value)
                            {
                                cb.Items[p].Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    cb.SelectedValue = str;
                }
            }
        }

        private string check(CheckBoxList cb)
        {
            string str = "";
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.Items[i].Selected)
                {
                    str += cb.Items[i].Value + ",";
                }
            }
            return str;
        }

        int ss = -1;
        /// <summary>
        ///　递归读取节点列表
        /// </summary>
        /// <param name="parid">节点ID</param>
        protected void nodelistload(int parid)
        {
            ss += 1;
            #region 加载列表
            DataTable ntable = nll.GetNodeChildList(parid);
            for (int i = 0; i < ntable.Rows.Count; i++)
            {
                string spanstr = new string('　', ss);
                Nodelist_LB.Items.Add(new ListItem(spanstr + ntable.Rows[i]["NodeName"].ToString(), ntable.Rows[i]["NodeID"].ToString()));
                nodelistload(DataConverter.CLng(ntable.Rows[i]["NodeID"].ToString()));
            }
            ss -= 1;
            #endregion
        }

        /// <summary>
        /// 列举模型字段
        /// </summary>
        protected void Modellistload()
        {
            //M_Permission pinfo = pll.GetSelect(UserID);
            //string tempfield = "," + fieldlistvalues + ",";

            //DataTable mtable = mll.GetList();
            //for (int i = 0; i < mtable.Rows.Count; i++)
            //{
            //    DataList.Items.Add(new ListItem(mtable.Rows[i]["ModelName"].ToString(), mtable.Rows[i]["ModelID"].ToString()));
            //}
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            M_UserInfo mui = bll.GetUserByUserID(UserID);
            StringBuilder sb = new StringBuilder();
            if (bll.UpDateUser(mui))
            {
                Response.Write("<script language=javascript>alert('成功设置用户权限!');location.href='UserManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('用户权限设置失败!');location.href='UserManage.aspx';</script>");
            }
        }

        protected void rblTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblTime.SelectedIndex == 0)
            {
                trTime.Visible = true;
            }
            else
            {
                trTime.Visible = false;
            }
        }

        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rbl.SelectedIndex == 0)
            //{
            //    this.tdPermission.Visible = true;
            //}
            //else
            //{
            //    tdPermission.Visible = false;
            //}
        }
    }
}