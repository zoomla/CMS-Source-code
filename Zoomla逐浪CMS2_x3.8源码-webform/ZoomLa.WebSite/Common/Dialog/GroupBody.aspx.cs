using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;


public partial class Common_Dialog_GroupBody : System.Web.UI.Page
{
    B_Group groupBll = new B_Group();
    B_Plat_Group pgroupBll = new B_Plat_Group();
    B_Structure struBll = new B_Structure();
    public string Source { get { return Request.QueryString["source"] ?? "user"; } }
    string hasChild_tlp = "<div class='userlist_item group_item' data-gid='@groupid'><div class='item_face pull-left'>@depth<img src='/Images/TreeLineImages/groups.gif'></div><div class='pull-left item_name'>@groupname</div><div class='pull-right item_add'><a href='javascript:;' title='查看用户'><i class='fa fa-user' style='font-size:16px;'></i> 查看用户</a></div></div><div data-pid='@pid' style='display:none;'>@childs</div>";
    string childs_tlp = "<div class='userlist_item group_item' data-gid='@groupid'><div class='item_face pull-left'>@depth<img src='/Images/TreeLineImages/group.gif'></div><div class='pull-left item_name'>@groupname</div><div class='pull-right item_add'><a href='javascript:;' title='查看用户'><i class='fa fa-user' style='font-size:16px;'></i> 查看用户</a></div></div>";
    //---------------------
    //string plat_HasChild_tlp = "<div class='userlist_item group_item' data-gid='@groupid'><div class='item_face pull-left'>@depth<img src='/Images/TreeLineImages/groups.gif'></div><div class='pull-left item_name'>@groupname</div><div class='pull-right item_add'><a href='javascript:;' title='查看用户'><i class='fa fa-user' style='font-size:16px;'></i> 查看用户</a></div></div><div data-pid='@pid' style='display:none;'>@childs</div>";
    //string plat_childs_tlp = "<div class='userlist_item group_item' data-gid='@groupid'><div class='item_face pull-left'>@depth<img src='/Images/TreeLineImages/group.gif'></div><div class='pull-left item_name'>@groupname</div><div class='pull-right item_add'><a href='javascript:;' title='查看用户'><i class='fa fa-user' style='font-size:16px;'></i> 查看用户</a></div></div>";
    //---------------------
    string Pid_Field = "";
    string ID_Field = "";
    string Name_Field = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        DataTable dt = new DataTable();
        if (Source.Equals("plat"))//能力中心
        {
            Pid_Field = "PGroup";
            ID_Field = "ID";
            Name_Field = "GroupName";
            dt = pgroupBll.SelByCompID(B_User_Plat.GetLogin().CompID);
            string attlp = "<label style='margin-bottom:0px;'>@groupname<input type='checkbox' name='plat_group_chk' data-gid='@groupid' data-gname='@groupname' style='margin-left:2px;'></label>";
            hasChild_tlp = "";
            childs_tlp = "<div class='userlist_item group_item' data-gid='@groupid'><div class='item_face pull-left'>@depth<img src='/Images/TreeLineImages/group.gif'></div><div class='pull-left item_name'>"+attlp+"</div><div class='pull-right item_add'><a href='javascript:;' title='查看用户'><i class='fa fa-user' style='font-size:16px;'></i> 查看用户</a></div><div style='clear:both;'></div></div>";
        }
        else if (Source.Equals("oa"))//oa组织结构
        {
            Pid_Field = "ParentID";
            ID_Field = "ID";
            Name_Field = "Name";
            dt = struBll.Sel();
        }
        else
        {
            Pid_Field = "ParentGroupID";
            ID_Field = "GroupID";
            Name_Field = "GroupName";
            dt = groupBll.Select_All();
        }
        Group_Lit.Text = GetAllDT(dt, 0);
    }
    public string GetAllDT(DataTable GroupDt, int pid, int depth = 0)
    {
        string html = "";
        string depthicon = "";
        for (int j = 0; j < depth; j++)//深度
        {
            depthicon += "<img src='/Images/TreeLineImages/tree_line4.gif' />";
        }
        GroupDt.DefaultView.RowFilter = Pid_Field + "=" + pid;//抽出父节点
        DataTable parents = GroupDt.DefaultView.ToTable();
        for (int i = 0; i < parents.Rows.Count; i++)
        {
            DataRow item = parents.Rows[i];
            if (GroupDt.Select(Pid_Field + "=" + item[ID_Field]).Length > 0)
            {
                html += TlpReplace(hasChild_tlp, parents, item)
                    .Replace("@childs", GetAllDT(GroupDt, Convert.ToInt32(item[ID_Field]), ++depth)).Replace("@depth", depthicon);
            }
            else
            {
                html += TlpReplace(childs_tlp, parents, item).Replace("@depth", depthicon);

            }
        }
        return html;
    }
    private string TlpReplace(string tlp, DataTable dt, DataRow dr)
    {
        tlp = tlp.Replace("@groupid", dr[ID_Field].ToString()).Replace("@groupname", dr[Name_Field].ToString()).Replace("@pid", dr[ID_Field].ToString());
        return tlp;
    }
}