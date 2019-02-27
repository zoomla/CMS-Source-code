using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class manage_Shop_AddInvoType : CustomerPageAction
{
    B_InvtoType bll = new B_InvtoType();
    B_Admin badmin = new B_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            int ids = DataConverter.CLng(Request.QueryString["id"]);
            if (ids > 0)
            {
                M_InvtoType minvtype = bll.GetSelect(ids);
                this.invType.Text = "修改发票类型";
                string [] inv= minvtype.InvtoType.Split('[');
                deliname.Text = inv[0];
                deliinfo.Text = minvtype.Remark;
                shuilv.Text = minvtype.Invto.ToString();
                ID_H.Value = ids.ToString();
            }
            else
            {
                this.invType.Text = "添加发票类型";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='InvtoManage.aspx'>发票类型管理</a></li><li>添加发票类型</li>");
        }
    }

    //保存
    protected void Button1_Click(object sender, EventArgs e)
    {
        int ids = DataConverter.CLng(ID_H.Value);
        M_InvtoType minvtype = new M_InvtoType();
        minvtype.ID = ids;
        minvtype.InvtoType = deliname.Text + "["+shuilv.Text + "%税率]";
        minvtype.Remark = deliinfo.Text;
        minvtype.Invto = DataConverter.CFloat(shuilv.Text);
        minvtype.Addtime = DateTime.Now;
        minvtype.AdminID = badmin.GetAdminLogin().AdminId;
        if (ids > 0)
        {
            bool result = bll.GetUpdate(minvtype);
            if (result)
            {
                function.WriteSuccessMsg("修改成功", "InvtoManage.aspx");
            }
            else
            {
                function.WriteErrMsg("修改失败");
            }
        }
        else
        {
            int mid =bll.GetInsert(minvtype);
            if (mid > 0)
            {
                function.WriteSuccessMsg("添加成功", "InvtoManage.aspx");
            }
            else
            {
                function.WriteErrMsg("添加失败");
            }
        }
    }
}