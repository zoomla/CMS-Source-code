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
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Text;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Common
{
    public partial class MultiDropList : System.Web.UI.Page
    {
        public int Direction { get { return DataConvert.CLng(Request.QueryString["Direction"]); } }
        B_GradeOption GradeBll = new B_GradeOption();
        public string FieldName { get { return Request.QueryString["FieldName"]; } }
        public string FValue { get { return Request.QueryString["FValue"]; } }
        public int CateID { get {return DataConverter.CLng(Request.QueryString["CateID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CateID <= 0 || string.IsNullOrEmpty(FieldName)) { function.WriteErrMsg("参数不规范"); }
                if (Direction == 1)
                {
                    string css = "  <style type=\"text/css\">"
                + ".text_200_auto {display:block;margin-bottom:3px;width:200px;}</style>";
                    CSS_L.Text = css;
                }
                string[] GradeCate = GradeBll.GetCate(CateID).GradeAlias.Split(new char[] { '|' });

                ListItem item1 = new ListItem();
                if (GradeCate != null && GradeCate.Length == 1)
                {
                    item1 = new ListItem(GradeCate[0], "0");
                    this.DDLGrade1.Visible = true;
                }
                ListItem item2 = new ListItem();
                if (GradeCate != null && GradeCate.Length == 2)
                {
                    item1 = new ListItem(GradeCate[0], "0");
                    this.DDLGrade1.Visible = true;
                    item2 = new ListItem(GradeCate[1], "0");
                    this.DDLGrade2.Visible = true;
                }
                ListItem item3 = new ListItem();
                if (GradeCate != null && GradeCate.Length == 3)
                {
                    item1 = new ListItem(GradeCate[0], "0");
                    this.DDLGrade1.Visible = true;
                    item2 = new ListItem(GradeCate[1], "0");
                    this.DDLGrade2.Visible = true;
                    item3 = new ListItem(GradeCate[2], "0");
                    this.DDLGrade3.Visible = true;
                }
                ListItem item4 = new ListItem();
                if (GradeCate != null && GradeCate.Length == 4)
                {
                    item1 = new ListItem(GradeCate[0], "0");
                    this.DDLGrade1.Visible = true;
                    item2 = new ListItem(GradeCate[1], "0");
                    this.DDLGrade2.Visible = true;
                    item3 = new ListItem(GradeCate[2], "0");
                    this.DDLGrade3.Visible = true;
                    item4 = new ListItem(GradeCate[3], "0");
                    this.DDLGrade4.Visible = true;
                }
                ListItem item5 = new ListItem();
                if (GradeCate != null && GradeCate.Length == 5)
                {
                    item1 = new ListItem(GradeCate[0], "0");
                    this.DDLGrade1.Visible = true;
                    item2 = new ListItem(GradeCate[1], "0");
                    this.DDLGrade2.Visible = true;
                    item3 = new ListItem(GradeCate[2], "0");
                    this.DDLGrade3.Visible = true;
                    item4 = new ListItem(GradeCate[3], "0");
                    this.DDLGrade4.Visible = true;
                    item5 = new ListItem(GradeCate[4], "0");
                    this.DDLGrade5.Visible = true;
                }
                ListItem item6 = new ListItem();
                if (GradeCate != null && GradeCate.Length == 6)
                {
                    item1 = new ListItem(GradeCate[0], "0");
                    this.DDLGrade1.Visible = true;
                    item2 = new ListItem(GradeCate[1], "0");
                    this.DDLGrade2.Visible = true;
                    item3 = new ListItem(GradeCate[2], "0");
                    this.DDLGrade3.Visible = true;
                    item4 = new ListItem(GradeCate[3], "0");
                    this.DDLGrade4.Visible = true;
                    item5 = new ListItem(GradeCate[4], "0");
                    this.DDLGrade5.Visible = true;
                    item6 = new ListItem(GradeCate[5], "0");
                    this.DDLGrade6.Visible = true;
                }

                BindGradeDrop(CateID, 0, item1, 1);//绑定一级下拉框
                if (!string.IsNullOrEmpty(FValue)) //传入值不为空时
                {
                    this.DDLGrade2.Items.Insert(0, item2);
                    if (GradeCate.Length == 3)
                    {
                        this.DDLGrade3.Items.Insert(0, item3);
                    }
                    if (GradeCate.Length == 4)
                    {
                        this.DDLGrade3.Items.Insert(0, item3);
                        this.DDLGrade4.Items.Insert(0, item4);
                    }
                    if (GradeCate.Length == 5)
                    {
                        this.DDLGrade3.Items.Insert(0, item3);
                        this.DDLGrade4.Items.Insert(0, item4);
                        this.DDLGrade5.Items.Insert(0, item5);
                    }
                    if (GradeCate.Length == 6)
                    {
                        this.DDLGrade3.Items.Insert(0, item3);
                        this.DDLGrade4.Items.Insert(0, item4);
                        this.DDLGrade5.Items.Insert(0, item5);
                        this.DDLGrade6.Items.Insert(0, item6);
                    }
                    string[] ValArr = FValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    int Gid = 0;
                    int Gid1 = 0;
                    int Gid2 = 0;
                    int Gid3 = 0;
                    int Gid4 = 0;
                    int Gid5 = 0;

                    string GName = "";
                    string GName1 = "";
                    string GName2 = "";
                    string GName3 = "";
                    string GName4 = "";
                    string GName5 = "";

                    switch (ValArr.Length)
                    {
                        case 1:
                            GName = ValArr[0];
                            Gid = B_GradeOption.GradeIDByName(CateID, 0, GName);
                            if (Gid > 0)
                            {
                                this.DDLGrade1.SelectedValue = Gid.ToString();
                                BindGradeDrop(CateID, Gid, item2, 2);//绑定二级下拉框
                            }
                            break;
                        case 2:
                            GName = ValArr[0];
                            Gid = B_GradeOption.GradeIDByName(CateID, 0, GName);
                            GName1 = ValArr[1];
                            Gid1 = B_GradeOption.GradeIDByName(CateID, Gid, GName1);
                            if (Gid > 0)
                            {
                                this.DDLGrade1.SelectedValue = Gid.ToString();
                                BindGradeDrop(CateID, Gid, item2, 2);//绑定二级下拉框
                                if (Gid1 > 0)
                                {
                                    this.DDLGrade2.SelectedValue = Gid1.ToString();
                                    if (GradeCate.Length == 3)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);//绑定三级下拉框
                                    }
                                }
                            }
                            break;
                        case 3:
                            GName = ValArr[0];
                            Gid = B_GradeOption.GradeIDByName(CateID, 0, GName);
                            GName1 = ValArr[1];
                            Gid1 = B_GradeOption.GradeIDByName(CateID, Gid, GName1);
                            GName2 = ValArr[2];
                            Gid2 = B_GradeOption.GradeIDByName(CateID, Gid1, GName2);
                            if (Gid > 0)
                            {
                                this.DDLGrade1.SelectedValue = Gid.ToString();
                                BindGradeDrop(CateID, Gid, item2, 2);//绑定二级下拉框
                                if (Gid1 > 0)
                                {
                                    this.DDLGrade2.SelectedValue = Gid1.ToString();
                                    if (GradeCate.Length == 3)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);//绑定三级下拉框
                                        if (Gid2 > 0)
                                        {
                                            this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        }
                                    }
                                }
                            }
                            break;
                        case 4:
                            GName = ValArr[0];
                            Gid = B_GradeOption.GradeIDByName(CateID, 0, GName);
                            GName1 = ValArr[1];
                            Gid1 = B_GradeOption.GradeIDByName(CateID, Gid, GName1);
                            GName2 = ValArr[2];
                            //throw new Exception(Gid+"|"+Gid1);389|408 
                            Gid2 = B_GradeOption.GradeIDByName(CateID, Gid1, GName2);
                            GName3 = ValArr[3];
                            Gid3 = B_GradeOption.GradeIDByName(CateID, Gid2, GName3);
                            if (Gid > 0)
                            {
                                this.DDLGrade1.SelectedValue = Gid.ToString();
                                BindGradeDrop(CateID, Gid, item2, 2);
                                if (Gid1 > 0)
                                {
                                    this.DDLGrade2.SelectedValue = Gid1.ToString();
                                    if (GradeCate.Length == 3)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);
                                        if (Gid2 > 0)
                                        {
                                            this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        }
                                    }
                                    if (GradeCate.Length == 4)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);
                                        this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        BindGradeDrop(CateID, Gid2, item4, 4);
                                        if (Gid3 > 0)
                                        {
                                            this.DDLGrade4.SelectedValue = Gid3.ToString();
                                        }
                                    }
                                }
                            }
                            break;
                        case 5:
                            GName = ValArr[0];
                            Gid = B_GradeOption.GradeIDByName(CateID, 0, GName);
                            GName1 = ValArr[1];
                            Gid1 = B_GradeOption.GradeIDByName(CateID, Gid, GName1);
                            GName2 = ValArr[2];
                            //throw new Exception(Gid+"|"+Gid1);389|408 
                            Gid2 = B_GradeOption.GradeIDByName(CateID, Gid1, GName2);
                            GName3 = ValArr[3];
                            Gid3 = B_GradeOption.GradeIDByName(CateID, Gid2, GName3);
                            GName4 = ValArr[4];
                            Gid4 = B_GradeOption.GradeIDByName(CateID, Gid3, GName4);
                            if (Gid > 0)
                            {
                                this.DDLGrade1.SelectedValue = Gid.ToString();
                                BindGradeDrop(CateID, Gid, item2, 2);
                                if (Gid1 > 0)
                                {
                                    this.DDLGrade2.SelectedValue = Gid1.ToString();
                                    if (GradeCate.Length == 3)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);
                                        if (Gid2 > 0)
                                        {
                                            this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        }
                                    }
                                    if (GradeCate.Length == 4)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);
                                        this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        BindGradeDrop(CateID, Gid2, item4, 4);
                                        if (Gid3 > 0)
                                        {
                                            this.DDLGrade4.SelectedValue = Gid3.ToString();
                                        }
                                    }
                                    if (GradeCate.Length == 5)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);
                                        this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        BindGradeDrop(CateID, Gid2, item4, 4);
                                        this.DDLGrade4.SelectedValue = Gid3.ToString();
                                        BindGradeDrop(CateID, Gid3, item5, 5);
                                        if (Gid4 > 0)
                                        {
                                            this.DDLGrade5.SelectedValue = Gid4.ToString();
                                        }
                                    }
                                }
                            }
                            break;
                        case 6:
                            GName = ValArr[0];
                            Gid = B_GradeOption.GradeIDByName(CateID, 0, GName);
                            GName1 = ValArr[1];
                            Gid1 = B_GradeOption.GradeIDByName(CateID, Gid, GName1);
                            GName2 = ValArr[2];
                            //throw new Exception(Gid+"|"+Gid1);389|408 
                            Gid2 = B_GradeOption.GradeIDByName(CateID, Gid1, GName2);
                            GName3 = ValArr[3];
                            Gid3 = B_GradeOption.GradeIDByName(CateID, Gid2, GName3);
                            GName4 = ValArr[4];
                            Gid4 = B_GradeOption.GradeIDByName(CateID, Gid3, GName4);
                            GName5 = ValArr[5];
                            Gid5 = B_GradeOption.GradeIDByName(CateID, Gid4, GName5);
                            if (Gid > 0)
                            {
                                this.DDLGrade1.SelectedValue = Gid.ToString();
                                BindGradeDrop(CateID, Gid, item2, 2);
                                if (Gid1 > 0)
                                {
                                    this.DDLGrade2.SelectedValue = Gid1.ToString();
                                    if (GradeCate.Length == 3)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);
                                        if (Gid2 > 0)
                                        {
                                            this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        }
                                    }
                                    if (GradeCate.Length == 4)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);
                                        this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        BindGradeDrop(CateID, Gid2, item4, 4);
                                        if (Gid3 > 0)
                                        {
                                            this.DDLGrade4.SelectedValue = Gid3.ToString();
                                        }
                                    }
                                    if (GradeCate.Length == 5)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);
                                        this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        BindGradeDrop(CateID, Gid2, item4, 4);
                                        this.DDLGrade4.SelectedValue = Gid3.ToString();
                                        BindGradeDrop(CateID, Gid3, item5, 5);
                                        if (Gid4 > 0)
                                        {
                                            this.DDLGrade5.SelectedValue = Gid4.ToString();
                                        }
                                    }
                                    if (GradeCate.Length == 6)
                                    {
                                        BindGradeDrop(CateID, Gid1, item3, 3);
                                        this.DDLGrade3.SelectedValue = Gid2.ToString();
                                        BindGradeDrop(CateID, Gid2, item4, 4);
                                        this.DDLGrade4.SelectedValue = Gid3.ToString();
                                        BindGradeDrop(CateID, Gid3, item5, 5);
                                        this.DDLGrade5.SelectedValue = Gid4.ToString();
                                        BindGradeDrop(CateID, Gid4, item6, 6);
                                        if (Gid5 > 0)
                                        {
                                            this.DDLGrade6.SelectedValue = Gid5.ToString();
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {
                    this.DDLGrade2.Items.Insert(0, item2);
                    if (GradeCate.Length == 3)
                    {
                        this.DDLGrade3.Items.Insert(0, item3);
                    }
                    if (GradeCate.Length == 4)
                    {
                        this.DDLGrade3.Items.Insert(0, item3);
                        this.DDLGrade4.Items.Insert(0, item4);
                    }
                    if (GradeCate.Length == 5)
                    {
                        this.DDLGrade3.Items.Insert(0, item3);
                        this.DDLGrade4.Items.Insert(0, item4);
                        this.DDLGrade5.Items.Insert(0, item5);
                    }
                    if (GradeCate.Length == 6)
                    {
                        this.DDLGrade3.Items.Insert(0, item3);
                        this.DDLGrade4.Items.Insert(0, item4);
                        this.DDLGrade5.Items.Insert(0, item5);
                        this.DDLGrade6.Items.Insert(0, item6);
                    }
                }
            }
        }

        private void BindGradeDrop(int CateID, int ParentID, ListItem item, int type)
        {
            if (type == 1)
            {
                this.DDLGrade1.DataSource = B_GradeOption.GetGradeList(CateID, ParentID);
                this.DDLGrade1.DataTextField = "GradeName";
                this.DDLGrade1.DataValueField = "GradeID";
                this.DDLGrade1.DataBind();
                this.DDLGrade1.Items.Insert(0, item);
            }
            else if (type == 2)
            {
                this.DDLGrade2.DataSource = B_GradeOption.GetGradeList(CateID, ParentID);
                this.DDLGrade2.DataTextField = "GradeName";
                this.DDLGrade2.DataValueField = "GradeID";
                this.DDLGrade2.DataBind();
                this.DDLGrade2.Items.Insert(0, item);
            }
            else if (type == 3)
            {
                this.DDLGrade3.DataSource = B_GradeOption.GetGradeList(CateID, ParentID);
                this.DDLGrade3.DataTextField = "GradeName";
                this.DDLGrade3.DataValueField = "GradeID";
                this.DDLGrade3.DataBind();
                this.DDLGrade3.Items.Insert(0, item);
            }
            else if (type == 4)
            {
                this.DDLGrade4.DataSource = B_GradeOption.GetGradeList(CateID, ParentID);
                this.DDLGrade4.DataTextField = "GradeName";
                this.DDLGrade4.DataValueField = "GradeID";
                this.DDLGrade4.DataBind();
                this.DDLGrade4.Items.Insert(0, item);
            }
            else if (type == 5)
            {
                this.DDLGrade5.DataSource = B_GradeOption.GetGradeList(CateID, ParentID);
                this.DDLGrade5.DataTextField = "GradeName";
                this.DDLGrade5.DataValueField = "GradeID";
                this.DDLGrade5.DataBind();
                this.DDLGrade5.Items.Insert(0, item);
            }
            else
            {
                this.DDLGrade6.DataSource = B_GradeOption.GetGradeList(CateID, ParentID);
                this.DDLGrade6.DataTextField = "GradeName";
                this.DDLGrade6.DataValueField = "GradeID";
                this.DDLGrade6.DataBind();
                this.DDLGrade6.Items.Insert(0, item);
            }
        }

        protected void DDL_Grade1ChangeIndex(object sender, EventArgs e)
        {
            string[] GradeCate = GradeBll.GetCate(CateID).GradeAlias.Split(new char[] { '|' });
            int Gid = DataConverter.CLng(this.DDLGrade1.SelectedValue);
            string GName = "";

            ListItem item2 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 2)
            {
                item2 = new ListItem(GradeCate[1], "0");
            }
            ListItem item3 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 3)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
            }
            ListItem item4 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 4)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
            }
            ListItem item5 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 5)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
            }
            ListItem item6 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 6)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
                item6 = new ListItem(GradeCate[5], "0");
            }
            if (Gid > 0)
            {
                GName = B_GradeOption.GetGradeOption(Gid).GradeName;
                BindGradeDrop(CateID, Gid, item2, 2);//绑定二级下拉框
                if (GradeCate.Length == 3)
                {
                    this.DDLGrade3.Items.Insert(0, item3);
                }
                if (GradeCate.Length == 4)
                {
                    this.DDLGrade3.Items.Insert(0, item3);
                    this.DDLGrade4.Items.Insert(0, item4);
                }
                if (GradeCate.Length == 5)
                {
                    this.DDLGrade3.Items.Insert(0, item3);
                    this.DDLGrade4.Items.Insert(0, item4);
                    this.DDLGrade5.Items.Insert(0, item5);
                }
                if (GradeCate.Length == 6)
                {
                    this.DDLGrade3.Items.Insert(0, item3);
                    this.DDLGrade4.Items.Insert(0, item4);
                    this.DDLGrade5.Items.Insert(0, item5);
                    this.DDLGrade6.Items.Insert(0, item6);
                }
                StringBuilder builder = new StringBuilder();
                builder.Append("<script type=\"text/javascript\">");
                builder.Append("parent.UpdateMultiDrop(\"" + GName + "\",\"txt_" + FieldName + "\");");
                builder.Append("</script>");
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
            }
        }

        protected void DDL_Grade2ChangeIndex(object sender, EventArgs e)
        {
            string[] GradeCate = GradeBll.GetCate(CateID).GradeAlias.Split(new char[] { '|' });
            int Gid = DataConverter.CLng(this.DDLGrade1.SelectedValue);
            int Gid1 = DataConverter.CLng(this.DDLGrade2.SelectedValue);
            string GName = B_GradeOption.GetGradeOption(Gid).GradeName;
            string GName1 = B_GradeOption.GetGradeOption(Gid1).GradeName;
            //if(GradeCate.Length>2)

            ListItem item2 = new ListItem();
            if (GradeCate != null && GradeCate.Length > 1)
            {
                item2 = new ListItem(GradeCate[1], "0");
            }
            ListItem item3;
            if (GradeCate.Length == 3)
            {
                item3 = new ListItem(GradeCate[2], "0");
                if (Gid1 > 0)
                {
                    BindGradeDrop(CateID, Gid1, item3, 3);//绑定三级下拉框
                }
            }
            ListItem item4;
            if (GradeCate.Length == 4)
            {
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                if (Gid1 > 0)
                {
                    BindGradeDrop(CateID, Gid1, item3, 3);
                }
            }
            ListItem item5 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 5)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
                BindGradeDrop(CateID, Gid1, item3, 3);
            }
            ListItem item6 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 6)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
                item6 = new ListItem(GradeCate[5], "0");
                BindGradeDrop(CateID, Gid1, item3, 3);
            }
            if (!string.IsNullOrEmpty(GName1))
                GName = GName + "|" + GName1;

            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("  parent.UpdateMultiDrop(\"" + GName + "\",\"txt_" + FieldName + "\");");
            builder.Append("</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        }

        protected void DDLGrade3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] GradeCate = GradeBll.GetCate(CateID).GradeAlias.Split(new char[] { '|' });
            int Gid = DataConverter.CLng(this.DDLGrade1.SelectedValue);
            int Gid1 = DataConverter.CLng(this.DDLGrade2.SelectedValue);
            int Gid2 = DataConverter.CLng(this.DDLGrade3.SelectedValue);

            string GName = B_GradeOption.GetGradeOption(Gid).GradeName;
            string GName1 = B_GradeOption.GetGradeOption(Gid1).GradeName;
            string GName2 = "";
            ListItem item2 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 2)
            {
                item2 = new ListItem(GradeCate[1], "0");
            }
            ListItem item3 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 3)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
            }
            ListItem item4 = new ListItem();
            if (GradeCate.Length == 4)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                BindGradeDrop(CateID, Gid2, item4, 4);
            }
            ListItem item5 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 5)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
                BindGradeDrop(CateID, Gid2, item4, 4);
            }
            ListItem item6 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 6)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
                item6 = new ListItem(GradeCate[5], "0");
                BindGradeDrop(CateID, Gid2, item4, 4);
            }

            if (Gid2 > 0)
                GName2 = B_GradeOption.GetGradeOption(Gid2).GradeName;
            if (!string.IsNullOrEmpty(GName1))
                GName = GName + "|" + GName1;
            if (!string.IsNullOrEmpty(GName2))
                GName = GName + "|" + GName2;
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("  parent.UpdateMultiDrop(\"" + GName + "\",\"txt_" + FieldName + "\");");
            builder.Append("</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        }

        protected void DDLGrade4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] GradeCate = GradeBll.GetCate(CateID).GradeAlias.Split(new char[] { '|' });
            int Gid = DataConverter.CLng(this.DDLGrade1.SelectedValue);
            int Gid1 = DataConverter.CLng(this.DDLGrade2.SelectedValue);
            int Gid2 = DataConverter.CLng(this.DDLGrade3.SelectedValue);
            int Gid3 = DataConverter.CLng(this.DDLGrade4.SelectedValue);

            string GName = B_GradeOption.GetGradeOption(Gid).GradeName;
            string GName1 = B_GradeOption.GetGradeOption(Gid1).GradeName;
            string GName2 = B_GradeOption.GetGradeOption(Gid2).GradeName;
            string GName3 = "";
            ListItem item2 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 2)
            {
                item2 = new ListItem(GradeCate[1], "0");
            }
            ListItem item3 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 3)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
            }
            ListItem item4 = new ListItem();
            if (GradeCate.Length == 4)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
            }
            ListItem item5 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 5)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
                BindGradeDrop(CateID, Gid3, item5, 5);
            }
            ListItem item6 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 6)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
                item6 = new ListItem(GradeCate[5], "0");
                BindGradeDrop(CateID, Gid3, item5, 5);
            }
            if (Gid3 > 0)
                GName3 = B_GradeOption.GetGradeOption(Gid3).GradeName;
            if (!string.IsNullOrEmpty(GName1))
                GName = GName + "|" + GName1;
            if (!string.IsNullOrEmpty(GName2))
                GName = GName + "|" + GName2;
            if (!string.IsNullOrEmpty(GName3))
                GName = GName + "|" + GName3;
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("  parent.UpdateMultiDrop(\"" + GName + "\",\"txt_" + FieldName + "\");");
            builder.Append("</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        }
        protected void DDLGrade5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] GradeCate = GradeBll.GetCate(CateID).GradeAlias.Split(new char[] { '|' });
            int Gid = DataConverter.CLng(this.DDLGrade1.SelectedValue);
            int Gid1 = DataConverter.CLng(this.DDLGrade2.SelectedValue);
            int Gid2 = DataConverter.CLng(this.DDLGrade3.SelectedValue);
            int Gid3 = DataConverter.CLng(this.DDLGrade4.SelectedValue);
            int Gid4 = DataConverter.CLng(this.DDLGrade5.SelectedValue);


            string GName = B_GradeOption.GetGradeOption(Gid).GradeName;
            string GName1 = B_GradeOption.GetGradeOption(Gid1).GradeName;
            string GName2 = B_GradeOption.GetGradeOption(Gid2).GradeName;
            string GName3 = B_GradeOption.GetGradeOption(Gid3).GradeName;
            string GName4 = "";
            ListItem item2 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 2)
            {
                item2 = new ListItem(GradeCate[1], "0");
            }
            ListItem item3 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 3)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
            }
            ListItem item4 = new ListItem();
            if (GradeCate.Length == 4)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
            }
            ListItem item5 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 5)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
            }
            ListItem item6 = new ListItem();
            if (GradeCate != null && GradeCate.Length == 6)
            {
                item2 = new ListItem(GradeCate[1], "0");
                item3 = new ListItem(GradeCate[2], "0");
                item4 = new ListItem(GradeCate[3], "0");
                item5 = new ListItem(GradeCate[4], "0");
                item6 = new ListItem(GradeCate[5], "0");
                BindGradeDrop(CateID, Gid4, item6, 6);
            }
            if (Gid4 > 0)
                GName4 = B_GradeOption.GetGradeOption(Gid4).GradeName;
            if (!string.IsNullOrEmpty(GName1))
                GName = GName + "|" + GName1;
            if (!string.IsNullOrEmpty(GName2))
                GName = GName + "|" + GName2;
            if (!string.IsNullOrEmpty(GName3))
                GName = GName + "|" + GName3;
            if (!string.IsNullOrEmpty(GName4))
                GName = GName + "|" + GName4;
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("  parent.UpdateMultiDrop(\"" + GName + "\",\"txt_" + FieldName + "\");");
            builder.Append("</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        }
        protected void DDLGrade6_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] GradeCate = GradeBll.GetCate(CateID).GradeAlias.Split(new char[] { '|' });
            int Gid = DataConverter.CLng(this.DDLGrade1.SelectedValue);
            int Gid1 = DataConverter.CLng(this.DDLGrade2.SelectedValue);
            int Gid2 = DataConverter.CLng(this.DDLGrade3.SelectedValue);
            int Gid3 = DataConverter.CLng(this.DDLGrade4.SelectedValue);
            int Gid4 = DataConverter.CLng(this.DDLGrade5.SelectedValue);
            int Gid5 = DataConverter.CLng(this.DDLGrade6.SelectedValue);


            string GName = B_GradeOption.GetGradeOption(Gid).GradeName;
            string GName1 = B_GradeOption.GetGradeOption(Gid1).GradeName;
            string GName2 = B_GradeOption.GetGradeOption(Gid2).GradeName;
            string GName3 = B_GradeOption.GetGradeOption(Gid3).GradeName;
            string GName4 = B_GradeOption.GetGradeOption(Gid4).GradeName;
            string GName5 = "";

            if (Gid5 > 0)
                GName5 = B_GradeOption.GetGradeOption(Gid5).GradeName;
            if (!string.IsNullOrEmpty(GName1))
                GName = GName + "|" + GName1;
            if (!string.IsNullOrEmpty(GName2))
                GName = GName + "|" + GName2;
            if (!string.IsNullOrEmpty(GName3))
                GName = GName + "|" + GName3;
            if (!string.IsNullOrEmpty(GName4))
                GName = GName + "|" + GName4;
            if (!string.IsNullOrEmpty(GName5))
                GName = GName + "|" + GName5;

            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");
            builder.Append("parent.UpdateMultiDrop(\"" + GName + "\",\"txt_" + FieldName + "\");");
            builder.Append("</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        }
    }
}