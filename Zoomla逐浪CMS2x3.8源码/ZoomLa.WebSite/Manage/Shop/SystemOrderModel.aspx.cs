using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;
using System.Text;
using System.Configuration;

public partial class manage_Shop_SystemOrderModel : CustomerPageAction
{
    B_Label labelBll = new B_Label();
    B_OrderBaseField bbf = new B_OrderBaseField();
    B_ModelField bmf = new B_ModelField();
    public int SType { get { return DataConverter.CLng(Request.QueryString["Type"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            MyBind();
        }
        
    }
    public void MyBind()
    {
        DataTable sysdt = new DataTable();
        DataTable extdt = bbf.Select_Type(SType);
        string str = "";
        switch (SType)
        {
            case 0:
                sysdt = labelBll.GetTableField("ZL_Orderinfo");
                for (int i = 0; i < extdt.Rows.Count; i++)
                {
                    if (extdt.Rows[i]["FieldName"] != null)
                    {
                        str += "'" + extdt.Rows[i]["FieldName"].ToString() + "',";
                    }
                }
                str = str.TrimEnd(',');
                if (sysdt != null && !string.IsNullOrEmpty(str))//系统字段RPT
                {
                    sysdt.DefaultView.RowFilter = " FieldName Not IN(" + str + ")";
                    sysdt = sysdt.DefaultView.ToTable();
                }
                break;
            case 1:
                sysdt = labelBll.GetTableField("ZL_Cart");
                for (int i = 0; i < extdt.Rows.Count; i++)
                {
                    if (extdt.Rows[i]["FieldName"] != null)
                    {
                        str += "'" + extdt.Rows[i]["FieldName"].ToString() + "',";
                    }
                }
                str = str.TrimEnd(',');
                if (sysdt != null && !string.IsNullOrEmpty(str))//系统字段RPT
                {
                    sysdt.DefaultView.RowFilter = " FieldName Not IN(" + str + ")";
                    sysdt = sysdt.DefaultView.ToTable();
                }
                break;
        }
        this.RepSystemModel.DataSource = sysdt;
        this.RepSystemModel.DataBind();
        this.RepModelField.DataSource = extdt;
        this.RepModelField.DataBind();
        string add = "<a href='AddOrderModel.aspx?Type=" + SType + "' style='color: Red'>[添加字段]</a>";
        Call.SetBreadCrumb(Master, "<a href='ProductManage.aspx'>商城管理</a></li><li><a href='OrderList.aspx'>订单管理</a></li><li class='active'>字段管理<a id='ShowLink' href='javascript:ShowList()'>[显示所有字段]</a>" + add + "</li>");
    }
    public string GetStyleTrue(string flag)
    {
        if (DataConverter.CBool(flag))
        {
            return "<font color=\"green\">√</font>";
        }
        else
        {
            return "<font color=\"red\">×</font>";
        }
    }

    protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        //上移,下移
        if (e.CommandName == "UpMove")
        {
            int Id = DataConverter.CLng(e.CommandArgument);
            M_OrderBaseField mubf = this.bbf.GetSelect(Id);
            M_OrderBaseField FieldPre = this.bbf.GetPreField(mubf.OrderId);
            if (mubf.OrderId != FieldPre.OrderId)
            {
                int CurrOrder = mubf.OrderId;
                mubf.OrderId = FieldPre.OrderId;
                FieldPre.OrderId = CurrOrder;
                this.bbf.GetUpdate(mubf);
                this.bbf.GetUpdate(FieldPre);
            }
        }
        if (e.CommandName == "DownMove")
        {
            int Id = DataConverter.CLng(e.CommandArgument);
            M_OrderBaseField mubf = this.bbf.GetSelect(Id);
            M_OrderBaseField FieldNext = this.bbf.GetNextField(mubf.OrderId);
            if (mubf.OrderId != FieldNext.OrderId)
            {
                int CurrOrder = mubf.OrderId;
                mubf.OrderId = FieldNext.OrderId;
                FieldNext.OrderId = CurrOrder;
                this.bbf.GetUpdate(mubf);
                this.bbf.GetUpdate(FieldNext);
            }
        }
        //删除
        if (e.CommandName == "Delete")
        {
            M_OrderBaseField orderMod = bbf.SelReturnModel(Convert.ToInt32(e.CommandArgument.ToString()));
            bbf.DeleteByGroupID(DataConverter.CLng(e.CommandArgument));
            bbf.DelFields("ZL_Orderinfo", orderMod.FieldName);
        }
        MyBind();
    }
    public string GetFieldType(string TypeName)
    {
        return bmf.GetFieldType(TypeName);
    }
}