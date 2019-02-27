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
using ZoomLa.BLL.Helper;

public partial class User_UserShop_StoreApply : System.Web.UI.Page
{
    #region 业务对象
    B_User bubll = new B_User();
    B_UserStoreTable ustbll = new B_UserStoreTable();
    protected B_ModelField mfbll = new B_ModelField();
    B_Model bmll = new B_Model();
    B_Content cbll = new B_Content();
    B_StoreStyleTable sstbll = new B_StoreStyleTable();
    B_User buser = new B_User();
    B_Product bproduct = new B_Product();
    B_ModelField model = new B_ModelField();
    B_Content bcontent = new B_Content();
    B_ModelField bmodel = new B_ModelField();
    protected B_Sensitivity sll = new B_Sensitivity();
    #endregion
    private int uid
    {
        get
        {
            if (HttpContext.Current.Request.Cookies["UserState"]["UserID"] != null)
                return int.Parse(HttpContext.Current.Request.Cookies["UserState"]["UserID"].ToString());
            else
                return 0;
        }
        set
        {
            uid = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["action"] = Request.QueryString["action"];
        if (!IsPostBack)
        {
            GetInit();
        }
    }
    #region 页面方法
    //初始化
    private void GetInit()
    {
        this.Label1.Text = "申请开通店铺";
        //绑定商铺类型
        DataTable moddt = bmll.GetListStore();
        if (moddt.Rows.Count < 1) { function.WriteErrMsg("管理员未指定店铺申请模型"); }
        if (moddt.Rows.Count > 0)
        {

            this.DropDownList1.DataSource = moddt;
            this.DropDownList1.DataTextField = "ModelName";
            this.DropDownList1.DataValueField = "ModelID";
            this.DropDownList1.DataBind();
            this.ModelHtml.Text = ModelHtml.Text = mfbll.InputallHtml(DataConverter.CLng(this.DropDownList1.SelectedValue), 0, new ModelConfig()
            {
                Source = ModelConfig.SType.Admin
            });

        }
        else
        {
            function.WriteErrMsg("后台没有添加店铺申请模型!");
        }

    }

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_StoreStyleTable sst = sstbll.GetNewStyle(int.Parse(this.DropDownList1.SelectedValue));
        if (sst.ID==0)
        {
            function.WriteErrMsg("后台没有为该模型绑定可用的模板!");
        }
        else
        {
            DataTable dt = this.mfbll.GetModelFieldList(int.Parse(this.DropDownList1.SelectedValue));
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
                string fvalue = this.Page.Request.Form["txt_" + dr["FieldName"]];
                row[2] = fvalue;
                table.Rows.Add(row);
            }

            string uname = bubll.GetLogin().UserName;
            int userid = DataConverter.CLng(bubll.GetLogin().UserID);

            DataRow rs1 = table.NewRow();
            rs1[0] = "UserID";
            rs1[1] = "int";
            rs1[2] = userid;
            table.Rows.Add(rs1);

            DataRow rs2 = table.NewRow();
            rs2[0] = "UserName";
            rs2[1] = "TextType";
            rs2[2] = uname;
            table.Rows.Add(rs2);

            DataRow rs3 = table.NewRow();
            rs3[0] = "StoreModelID";
            rs3[1] = "int";
            rs3[2] = DataConverter.CLng(this.DropDownList1.Text);
            table.Rows.Add(rs3);

            if (!string.IsNullOrEmpty(sst.StyleName))
            {

                DataRow rs5 = table.NewRow();
                rs5[0] = "StoreStyleID";
                rs5[1] = "int";
                rs5[2] = sst.ID;
                table.Rows.Add(rs5);

                DataRow rs4 = table.NewRow();
                rs4[0] = "StoreName";
                rs4[1] = "TextType";
                rs4[2] = this.Nametxt.Text;
                table.Rows.Add(rs4);


                M_CommonData CData = new M_CommonData();
                CData.ModelID = int.Parse(this.DropDownList1.Text);
                CData.NodeID = 0;
                CData.TableName = bmll.GetModelById(CData.ModelID).TableName;
                CData.Title = this.Nametxt.Text;
                CData.Inputer = uname;
                CData.EliteLevel = 0;
                CData.Status = 0;
                CData.InfoID = "";
                CData.SpecialID = "";
                CData.Template = sst.StyleUrl;
                CData.TagKey = "";
                CData.PdfLink = "";
                CData.Titlecolor = "";
                CData.IP = IPScaner.GetUserIP();
                CData.UpDateTime = DateTime.Now;
                CData.CreateTime = DateTime.Now;
                int styless = CData.ModelID;
                cbll.AddContent(table, CData);

                //bproduct.DeleteByInputer(bubll.GetLogin().UserID);//删除商品

                //bcontent.DeCommonModel(bubll.GetLogin().UserName);//删除内容信息

                //bmodel.DeModelFieldById(DataConverter.CLng(this.DropDownList1.SelectedValue));//删除字段
                function.WriteSuccessMsg("提交成功", "StoreEdit.aspx");
            }
            
        }


    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DropDownList1.Text != "")
        {
            string tophtml = "<table width=\"100%\"><tr><td width=\"15%\"></td><td width = \"85%\"></td></tr>";
            string endhtml = "</table>";
            this.ModelHtml.Text = tophtml + this.mfbll.GetInputHtmlUser(int.Parse(this.DropDownList1.SelectedValue), 0) + endhtml;
        }
    }
    #endregion
}
