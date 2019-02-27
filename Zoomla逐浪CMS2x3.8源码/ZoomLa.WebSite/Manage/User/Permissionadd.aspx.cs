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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Text;
public partial class manage_Config_Permissionadd : CustomerPageAction
{
    B_Node nll = new B_Node();
    B_Model mll = new B_Model();
    B_ModelField fll = new B_ModelField();
    B_Permission pll = new B_Permission();
    B_Admin ba = new B_Admin();
    M_Permission pmodel = new M_Permission();
    B_UserPurview purBll = new B_UserPurview();
    string fieldlistvalues;
    protected string type = "添加角色";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "UserManage")) { function.WriteErrMsg("没有权限进行此项操作"); }
        string menu = Request.QueryString["menu"];
        int id = DataConverter.CLng(Request.QueryString["id"]);
        if (!IsPostBack)
        {
            nodelistload(0);
            tdtext.Visible = false;
            tdlist.Visible = false;
            if (menu == "edit")
            {
                M_Permission pinfo = pll.GetSelect(id);
                this.txtID.Value = pinfo.ID.ToString();
                this.txtRoleName.Text = pinfo.RoleName.ToString();
                this.txtInfo.Text = pinfo.Info.ToString();
                this.txtPrecedence.Text = pinfo.Precedence.ToString();
                DataSet ds = function.XmlToTable(pinfo.Perlist);
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
                    Modellistload();
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
                        SelectList(cblMonth, Month);
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
                if (pinfo.IsTrue == true)
                {
                    this.txtIsTrue.SelectedValue = "1";
                }
                else
                {
                    this.txtIsTrue.SelectedValue = "0";
                }
                this.type = "修改角色";
                this.Button1.Text = "修改角色";
            }
            else
            {
                Modellistload();
            }
            this.txtID.Value = id.ToString();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='PermissionInfo.aspx'>角色管理</a></li><li>添加角色</li>");
        }
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
        int id = DataConverter.CLng(Request.QueryString["id"]);
        M_Permission pinfo = pll.GetSelect(id);
        string fieldlistvalues = "";// pinfo.Fieldlist;
        string tempfield = "," + fieldlistvalues + ",";
        DataTable mtable = mll.GetList();
        for (int i = 0; i < mtable.Rows.Count; i++)
        {
            DataList_LB.Items.Add(new ListItem(mtable.Rows[i]["ModelName"].ToString(), mtable.Rows[i]["ModelID"].ToString()));
        }
        string fields = "";
        DataTable mtables = mll.GetList();
        for (int i = 0; i < mtables.Rows.Count; i++)
        {
            fields += "<optgroup label=\"" + mtable.Rows[i]["ModelName"].ToString() + "\">";
            DataTable modefields = fll.GetModelFieldList(DataConverter.CLng(mtable.Rows[i]["ModelID"].ToString()));
            for (int ii = 0; ii < modefields.Rows.Count; ii++)
            {
                //字段默认选中
                string selecttxt = "";
                if (tempfield.IndexOf("," + modefields.Rows[ii]["FieldID"].ToString() + ",") > -1)
                {
                    selecttxt = "selected=\"selected\"";
                }
                fields += "<option " + selecttxt + " value=\"" + modefields.Rows[ii]["FieldID"].ToString() + "\">" + modefields.Rows[ii]["FieldAlias"].ToString() + "</option>";
            }
        }
        fields = "<select multiple=\"multiple\"  class=\"tdbg\" name=\"Fieldlist\" size=\"4\" style=\"height:284px;width:216px;\">" + fields + "</select>";
        Label2.Text = fields;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        pmodel.ID = DataConverter.CLng(txtID.Value);
        pmodel.Info = txtInfo.Text;
        pmodel.IsTrue = DataConverter.CBool(txtIsTrue.SelectedValue);
        pmodel.Precedence = DataConverter.CLng(txtPrecedence.Text);
        pmodel.RoleName = Server.HtmlEncode(txtRoleName.Text);
        pmodel.RoleImg = "";
        pmodel.UserGroup = pll.GetSelect(DataConverter.CLng(this.txtID.Value)) == null ? "" : pll.GetSelect(DataConverter.CLng(this.txtID.Value)).UserGroup;
        StringBuilder sb = new StringBuilder();
        //sb.Append("<SiteList>" + Request.Form["SiteList"] + "</SiteList>");//子站
        //sb.Append("<Nodelist>" + Request.Form["Nodelist"] + "</Nodelist>");//节点
        //sb.Append("<DataList>" + Request.Form["DataList"] + "</DataList>");//模型表
        //sb.Append("<Fieldlist>" + Request.Form["Fieldlist"] + "</Fieldlist>");//模型字段
        //sb.Append("<Time>" + Request.Form["rblTime"] + "</Time>");//浏览时间
        //sb.Append("<Month>" + check(cblMonth) + "</Month>");//设置月
        //sb.Append("<Day>" + check(cblDay) + "</Day>");//设置日
        //sb.Append("<Hour>" + check(cblHour) + "</Hour>");//设置小时
        //sb.Append("<Weeks>" + check(cblWeeks) + "</Weeks>");//设置星期
        //sb.Append("<ViewContent>" + Request.Form["ViewContent"] + "</ViewContent>");//允许内容浏览
        //sb.Append("<ListContent>" + Request.Form["ListContent"] + "</ListContent>");//允许列表浏览
        //sb.Append("<AddContent>" + Request.Form["AddContent"] + "</AddContent>");//允许新增发布
        //sb.Append("<ModefiyContent>" + Request.Form["ModefiyContent"] + "</ModefiyContent>");//允许编辑修改
        //sb.Append("<DeleteContent>" + Request.Form["DeleteContent"] + "</DeleteContent>");//允许删除内容
        //sb.Append("<AddComm>" + Request.Form["AddComm"] + "</AddComm>");//允许评论权限
        pmodel.Perlist = sb.ToString();
        if (pmodel.ID == 0)
        {
            DataTable dt = pll.SelByRole(pmodel.RoleName);
            if (dt != null && dt.Rows.Count > 0)
            {
                function.WriteErrMsg("已经存在此角色，不能添加同名的角色!");
            }
            else
            {
                pll.GetInsert(pmodel);
                function.WriteSuccessMsg("添加成功", "PermissionInfo.aspx");
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        else
        {
            pll.InsertUpdate(pmodel);
            function.WriteSuccessMsg("修改成功", "PermissionInfo.aspx");
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

}
