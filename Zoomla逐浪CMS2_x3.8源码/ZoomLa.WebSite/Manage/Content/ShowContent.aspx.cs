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
using ZoomLa.Components;
using System.Collections.Generic;
using ZoomLa.SQLDAL;
using System.IO;
using System.Drawing;

public partial class Manage_I_Content_ShowContent : CustomerPageAction
{
    B_ModelField bfield = new B_ModelField();
    B_Content bc = new B_Content();
    B_Node nodeBll = new B_Node();
    B_Role RLL = new B_Role();
    B_Comment bco = new B_Comment();
    B_Model bm = new B_Model();
    B_Admin babll = new B_Admin();
    M_Node nodeMod = new M_Node();
    B_User buser = new B_User();
    B_ModelField fieldBll = new B_ModelField();
    public int Gid { get { return Convert.ToInt32(Request.QueryString["GID"]); } }
    public int ModelID { get { return DataConvert.CLng(ModelID_Hid.Value); } set { ModelID_Hid.Value = value.ToString(); } }
    public int NodeID { get { return DataConvert.CLng(ViewState["NodeID"]); } set { ViewState["NodeID"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Gid < 1) function.WriteErrMsg("参数出错");
            M_CommonData mc = bc.SelReturnModel(Gid);
            if (mc == null || mc.IsNull) { function.WriteErrMsg("[" + Gid + "]号文章不存在"); }
            M_ModelInfo modelMod = bm.GetModelById(mc.ModelID);
            M_Node nodeMod = nodeBll.SelReturnModel(mc.NodeID);
            ModelID = mc.ModelID;
            NodeID = mc.NodeID;
            if (modelMod.IsNull) { function.WriteErrMsg("该内容所绑定的模型[" + mc.ModelID + "]不存在"); }
            if (nodeMod.IsNull) { function.WriteErrMsg("该内容所绑定的节点[" + mc.NodeID + "]不存在"); }
            Call.SetBreadCrumb(Master, "<li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='ContentManage.aspx?NodeID=" + mc.NodeID + "'>" + nodeMod.NodeName + "</a></li><li class='active'>预览"+modelMod.ItemName+"</li><div class='pull-right hidden-xs'><span><a href='" + customPath2 + "Content/SchedTask.aspx' title='查看计划任务'><span class='fa fa-clock-o' style='color:#28b462;'></span></a>" + GetOpenView() + "<span onclick=\"opentitle('EditNode.aspx?NodeID=" + mc.NodeID + "','配置本节点');\" class='fa fa-cog' title='配置本节点' style='cursor:pointer;margin-left:5px;'></span></span></div>");
            //--------------
            Button5.Text = "修改" + modelMod.ItemName;
            Del_Btn.Text = "删除" + modelMod.ItemName;
            Reject_Btn.Enabled = (mc.Status != (int)ZLEnum.ConStatus.Reject);
            UnAudit_Btn.Enabled = (mc.Status == (int)ZLEnum.ConStatus.Audited);
            if (mc.EliteLevel == 1)
            {
                Elite_Btn.Visible = false;
                Button6.Visible = true;
            }
            else
            {
                Elite_Btn.Visible = true;
                Button6.Visible = false;
            }

            if (mc.Status == (int)ZLEnum.ConStatus.Recycle)
            {
                Del_Btn.Enabled = false;
                Reject_Btn.Enabled = false;
            }
            else
            {
                Del_Btn.Enabled = true;
            }
            NodeName_L.Text = nodeMod.NodeName;
            Gid_L.Text = Gid.ToString();
            DataTable fieldDT = fieldBll.SelByModelID(mc.ModelID, true);
            //自定义字段名称
            C_Title_L.Text = B_Content.GetFieldAlias("Title", fieldDT) + "：";
            //
            Title_L.Text = mc.Title;
            Inputer_L.Text = mc.Inputer;
            Hits_L.Text = mc.Hits.ToString();
            CreateTime_L.Text = mc.CreateTime.ToString();
            UpdateTime_L.Text = mc.UpDateTime.ToString();
            ConStatus_L.Text = ZLEnum.GetConStatus(mc.Status);
            Elite_L.Text = mc.EliteLevel == 1 ? "已推荐" : "未推荐";
            topimg_img.Src = mc.TopImg;
            DataTable contentDT = bc.GetContentByItems(mc.TableName, mc.GeneralID);
            Base_L.Text = bfield.InputallHtml(ModelID, NodeID, new ModelConfig()
            {
                ValueDT = contentDT,
                Mode = ModelConfig.SMode.PreView
            });
        }
    }
    public void MyBind()
    {
 
    }
    //批量审核
    protected void Button3_Click(object sender, EventArgs e)
    {
        M_AdminInfo ad = babll.GetAdminLogin();
        if (GetRole("Comments") || ("," + ad.RoleList + ",").Contains(",1,"))
        {
            string[] arrit = GetChecked();
            for (int i = 0; i < arrit.Length; i++)
            {
                bco.Update_ByAudited_ID(DataConverter.CLng(arrit[i]), true);
            }
            MyBind();
        }
        else
        {
            function.WriteSuccessMsg("你无权限管理评论信息", "ContentManage.aspx?NodeID=" + ViewState["NodeID"].ToString());
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        M_AdminInfo ad = babll.GetAdminLogin();
        if (GetRole("Comments") || ("," + ad.RoleList + ",").Contains(",1,"))
        {
            string[] arrit = GetChecked();
            for (int i = 0; i < arrit.Length; i++)
            {
                bco.Update_ByAudited_ID(DataConverter.CLng(arrit[i]), false);
            }
            MyBind();
        }
        else
        {
            function.WriteSuccessMsg("你无权限管理评论信息", "ContentManage.aspx?NodeID=" + ViewState["NodeID"].ToString());
        }
    }
    // 修改
    protected void Button5_Click(object sender, EventArgs e)
    {
        M_AdminInfo ad = babll.GetAdminLogin();
        if (GetRole("Modify") || ("," + ad.RoleList + ",").Contains(",1,"))
        {
            Response.Redirect("EditContent.aspx?GeneralID=" + Gid);
        }
        else
        {
            function.WriteErrMsg("你无权限修改信息", "ContentManage.aspx?NodeID=" + ViewState["NodeID"].ToString());
        }
    }
    // 删除
    protected void delete_Click(object sender, EventArgs e)
    {
        M_AdminInfo ad = babll.GetAdminLogin();
        if (GetRole("Modify") || ("," + ad.RoleList + ",").Contains(",1,"))
        {
            bc.SetDel(Gid);
            function.WriteSuccessMsg("已移入回收站", "ContentManage.aspx?NodeID=" + NodeID);
        }
        else
        {
            function.WriteSuccessMsg("你无权限删除信息", "ContentManage.aspx?NodeID=" + ViewState["NodeID"].ToString());
        }
    }
    // 直接退稿
    protected void Reject_Btn_Click(object sender, EventArgs e)
    {
        M_AdminInfo ad = babll.GetAdminLogin();
        if (GetRole("State") || ("," + ad.RoleList + ",").Contains(",1,"))
        {
            M_CommonData commdata = bc.GetCommonData(Gid);
            commdata.Status = -1;
            bc.Update(commdata);
            function.WriteSuccessMsg("退稿成功!", "ShowContent.aspx?GID=" + Gid );
        }
        else
        {
            function.WriteErrMsg("你无权限退稿信息", "ContentManage.aspx?NodeID=" + ViewState["NodeID"].ToString());
        }
    }
    // 取消审核
    protected void UnAudit_Btn_Click(object sender, EventArgs e)
    {
        M_CommonData commdata = bc.GetCommonData(Gid);
        commdata.Status = 0;
        bc.Update(commdata);
        function.WriteSuccessMsg("操作成功!", "ShowContent.aspx?GID=" + Gid );
    }
    public bool GetRole(string auth) { return true; }
    // 设为推荐
    protected void Elite_Btn_Click(object sender, EventArgs e)
    {
        M_AdminInfo ad = babll.GetAdminLogin();
        if (GetRole("State") || ("," + ad.RoleList + ",").Contains(",1,"))
        {
            M_CommonData commdata = bc.GetCommonData(Gid);
            commdata.EliteLevel = 1;
            bc.Update(commdata);
            function.WriteSuccessMsg("推荐成功!", "ShowContent.aspx?GID="+Gid);
        }
        else
        {
            function.WriteErrMsg("你无权限推荐信息", "ContentManage.aspx?NodeID=" + ViewState["NodeID"].ToString());
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        M_CommonData commdata = bc.GetCommonData(Gid);
        commdata.EliteLevel = 0;
        bc.Update(commdata);
        function.WriteSuccessMsg("操作成功!", "ShowContent.aspx?GID=" + Gid );
    }
    public string GetBaseLabel()
    {
        string str = "";
        DataTable dt = bfield.GetModelFieldListAll(ModelID);
        M_CommonData mc = bc.GetCommonData(Gid);
        DataTable contentDT = bc.GetContentByItems(mc.TableName, mc.GeneralID);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["FieldAlias"].ToString().Equals("附属图片"))//对某些字段特殊处理
            {
                string[] imgArr = contentDT.Rows[0][dt.Rows[i]["FieldName"].ToString()].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string value = "";
                string img = "<img src='{0}' />";
                string path = SiteConfig.SiteOption.UploadDir;
                foreach (string s in imgArr)
                {
                    value += string.Format(img, path+s);
                }
                str += "<tr><td>" + dt.Rows[i]["FieldAlias"].ToString() + "</td><td>" + value + "</td></tr>";
            }
            else
            {
                str += "<tr><td>" + dt.Rows[i]["FieldAlias"].ToString() + "</td><td>" + contentDT.Rows[0][dt.Rows[i]["FieldName"].ToString()].ToString() + "</td></tr>";
            }

        }
        return str;
    }
    private string[] GetChecked()
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            string[] chkArr = Request.Form["idchk"].Split(',');
            return chkArr;
        }
        else
            return null;
    }
    public string GetOpenView()
    {
        string outstr = " <a href='/Item/" + Gid + ".aspx' target='_blank'><span class='fa fa-eye'></span></a>";
        return outstr;
    }
}