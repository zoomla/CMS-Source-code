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
using System.Text;
using System.Collections.Generic;
using ZoomLa.Components;
using System.Data.SqlClient;

public partial class User_UserShop_StoreEdit : System.Web.UI.Page
{
    #region 业务对象
    B_User ubll = new B_User();
    B_ModelField mfbll = new B_ModelField();
    B_Model bmll = new B_Model();
    B_Content cbll = new B_Content();

    M_UserInfo m_user = new M_UserInfo();
    protected B_Sensitivity sll = new B_Sensitivity();
    #endregion

    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["action"] = Request.QueryString["action"];
        if (!IsPostBack)
        {
            GetInit();
        }
    }
    #endregion

    #region 页面方法
    private int gid
    {
        get
        {
            if (ViewState["gid"] != null)
                return int.Parse(ViewState["gid"].ToString());
            else
                return 0;
        }
        set
        {
            gid = value;
        }
    }

    private void GetInit()
    {
        this.Label1.Text = "修改申请信息";
        string uname = ubll.GetLogin().UserName;
        DataTable shopinfo = mfbll.SelectTableName("ZL_CommonModel", "tablename like 'ZL_Store_%' and Inputer=@uname",new SqlParameter[]{new SqlParameter("uname",uname)});
        if (shopinfo.Rows.Count < 1)
        {
            Response.Redirect("StoreApply.aspx");
        }
        else
        {
            ViewState["gid"] = shopinfo.Rows[0]["GeneralID"].ToString();
            M_CommonData storedt = cbll.GetCommonData(gid);
            if (ViewState["action"] == null)//
            {
                this.add.Visible = false;
                this.Auditing.Visible = true;
            }
            else
            {
                this.add.Visible = true;
                this.Auditing.Visible = false;
                this.Nametxt.Text = storedt.Title.ToString();
                //绑定商铺类型
                DataTable list = bmll.GetListStore();
                if (list.Rows.Count > 0)
                {
                    this.DropDownList1.DataSource = list;
                    this.DropDownList1.DataTextField = "ModelName";
                    this.DropDownList1.DataValueField = "ModelID";
                    this.DropDownList1.DataBind();
                    this.DropDownList1.SelectedValue = storedt.InfoID.ToString();
                }
                else
                {
                    this.DropDownList1.Items.Add(new ListItem("无", ""));
                }
                DataTable dtinfo = mfbll.SelectTableName(storedt.TableName, "id=" + storedt.ItemID + "");
                if (dtinfo.Rows.Count > 0)
                {
                    ModelHtml.Text = mfbll.InputallHtml(DataConverter.CLng(dtinfo.Rows[0]["StoreModelID"]), 0, new ModelConfig()
                    {
                        ValueDT = dtinfo
                    });
                }
                else
                {
                    ModelHtml.Text = mfbll.InputallHtml(DataConverter.CLng(dtinfo.Rows[0]["StoreModelID"]), 0, new ModelConfig()
                    {
                        Source = ModelConfig.SType.Admin
                    });
                }
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DropDownList1.Text != "")
        {
           // string tophtml = "<table width=\"100%\"><tr><td width=\"15%\"></td><td width = \"85%\"></td></tr>";
            string endhtml = "</table>";
            ModelHtml.Text = mfbll.InputallHtml(DataConverter.CLng(this.DropDownList1.SelectedValue), 0, new ModelConfig()
            {
                Source = ModelConfig.SType.UserBase
            })+endhtml;
        }
    }

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {  
        try
        {
            DataTable dt = this.mfbll.GetModelFieldList(int.Parse(this.DropDownList1.Text));
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FieldName", typeof(string)));
            table.Columns.Add(new DataColumn("FieldType", typeof(string)));
            table.Columns.Add(new DataColumn("FieldValue", typeof(string)));
            foreach (DataRow dr in dt.Rows)
            {
                if (DataConverter.CBool(dr["IsNotNull"].ToString()))
                {
                    if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
                    {
                        function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
                    }
                }
                if (dr["FieldType"].ToString() == "FileType")
                {
                    string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                    bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                    string sizefield = Sett[1].Split(new char[] { '=' })[1];
                    if (chksize && sizefield != "")
                    {
                        DataRow row2 = table.NewRow();
                        row2[0] = sizefield;
                        row2[1] = "FileSize";
                        row2[2] = this.Page.Request.Form["txt_" + sizefield];
                        table.Rows.Add(row2);
                    }
                }
                if (dr["FieldType"].ToString() == "MultiPicType")
                {
                    string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                    bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                    string sizefield = Sett[1].Split(new char[] { '=' })[1];
                    if (chksize && sizefield != "")
                    {
                        if (string.IsNullOrEmpty(this.Page.Request.Form["txt_" + sizefield]))
                        {
                            function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                        }
                        DataRow row1 = table.NewRow();
                        row1[0] = sizefield;
                        row1[1] = "ThumbField";
                        row1[2] = this.Page.Request.Form["txt_" + sizefield];
                        table.Rows.Add(row1);
                    }
                }
                DataRow row = table.NewRow();
                row[0] = dr["FieldName"].ToString();
                string ftype = dr["FieldType"].ToString();
                row[1] = ftype;
                string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"].ToString()];
                if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
                {
                    fvalue = sll.ProcessSen(fvalue);
                }
                row[2] = fvalue;
                table.Rows.Add(row);
            }
            string uname = ubll.GetLogin().UserName;
            int userid = DataConverter.CLng(ubll.GetLogin().UserID);

            DataRow rs1 = table.NewRow();
            rs1[0] = "UserID";
            rs1[1] = "int";
            rs1[2] = userid;
            table.Rows.Add(rs1);

            DataRow rs2 = table.NewRow();
            rs2[0] = "UserName";
            rs2[1] = "TextType";
            rs2[2] = ubll.GetLogin().UserName;
            table.Rows.Add(rs2);

            DataRow rs3 = table.NewRow();
            rs3[0] = "StoreModelID";
            rs3[1] = "int";
            rs3[2] = DataConverter.CLng(this.DropDownList1.Text);
            table.Rows.Add(rs3);

            DataRow rs4 = table.NewRow();
            rs4[0] = "StoreName";
            rs4[1] = "TextType";
            rs4[2] = this.Nametxt.Text;
            table.Rows.Add(rs4);

            DataRow rs5 = table.NewRow();
            rs5[0] = "StoreState";
            rs5[1] = "int";
            rs5[2] = 0;
            table.Rows.Add(rs5);


            M_CommonData CData = cbll.GetCommonData(gid);
            CData.Title = this.Nametxt.Text;
            DataTable storedt = mfbll.SelectTableName(CData.TableName, "id=" + CData.ItemID);
            if (storedt.Rows.Count > 0)
            {
                cbll.UpdateContent(table, CData);
            }
            else
            {
                cbll.AddContent(table, CData);
                cbll.DelContent(gid);
            }
            Response.Write("<script language=javascript>alert('店铺申请提交成功，请等待管理员审核!');location.href='StoreEdit.aspx';</script>");
        }
        catch (Exception ee)
        {
            function.WriteErrMsg(ee.Message);
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("StoreEdit.aspx?action=Show");
    }
    #endregion
}
