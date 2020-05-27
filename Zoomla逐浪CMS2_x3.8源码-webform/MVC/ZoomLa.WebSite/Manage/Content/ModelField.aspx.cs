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
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.Content
{
    public partial class ModelField : CustomerPageAction
    {
        B_Model modBll = new B_Model();
        B_ModelField bll = new B_ModelField();
        public string modelName = "";
        public string tabNameStr = "";
        public int ModelID { get { return DataConverter.CLng(Request.QueryString["ModelID"]); } }
        public int ModelType { get { return DataConverter.CLng(Request.QueryString["ModelType"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "ModelEdit");
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "orderup":
                        M_ModelField curmod = bll.SelReturnModel(DataConverter.CLng(Request.Form["curid"]));
                        curmod.OrderID = DataConverter.CLng(Request.Form["curorder"]);
                        M_ModelField tagmod = bll.SelReturnModel(DataConverter.CLng(Request.Form["tagid"]));
                        tagmod.OrderID = DataConverter.CLng(Request.Form["tagorder"]);
                        bll.UpdateOrder(curmod, tagmod);
                        result = "1";
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                return;
            }
            if (!IsPostBack)
            {
                //负数为其它固定表的模型
                if (ModelID == 0) { function.WriteErrMsg("没有指定模块ID"); }
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='ModelManage.aspx?ModelType=" + ModelType + "'>" + modelName + "</a></li><li class='active'><a href='ModelField.aspx" + Request.Url.Query + "'> 字段列表</a> " + tabNameStr + " <a id='ShowLink' class='reds' href='javascript:ShowList()'>[显示所有字段]</a> <a href='AddModelField.aspx" + Request.Url.Query + "'  class='reds'>[添加字段]</a></li>" + Call.GetHelp(63));
            }
        }
        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {
            int Id = DataConverter.CLng(e.CommandArgument.ToString().Split('|')[0].Trim());
            M_ModelField nowFieldinfo = this.bll.GetModelByID(ModelID.ToString(), Id);
            string col = e.CommandArgument.ToString().Split('|')[1].Trim();
            switch (col)
            {
                case "Null":
                    nowFieldinfo.IsNotNull = !nowFieldinfo.IsNotNull;
                    break;
                case "Show":
                    nowFieldinfo.ShowList = !nowFieldinfo.ShowList;
                    //nowFieldinfo.IsShow = !nowFieldinfo.IsShow;
                    break;
                case "Copy":
                    nowFieldinfo.IsCopy ^= 1;
                    break;
                case "BatchAdd":
                    nowFieldinfo.Islotsize = !nowFieldinfo.Islotsize;
                    break;
            }
            bll.Update(nowFieldinfo);
        }
        public string GetStyleTrue(string strflag)
        {
            if (DataConverter.CBool(strflag))
            {
                return "<font color=\"green\">√</font>";
            }
            else
            {
                return "<font color=\"red\">×</font>";
            }
        }

        public string GetFieldType(string TypeName)
        {
            return bll.GetFieldType(TypeName);
        }

        public bool Get_sum(string Sys_type)
        {
            return !DataConverter.CBool(Sys_type);
        }
        public string IsSysField()
        {
            if (DataConverter.CBool(Eval("Sys_type", "")))
            {
                return "<span style='color:green;'>系统</span>";
            }
            else { return "自定义"; }
        }
        public new string IsValid(string value, string type = "")
        {
            string check = "<i class='fa fa-check rd_green'></i>";
            string error = "<i class='fa fa-remove rd_red'></i>";
            if (type.Equals("iscopy"))
            {
                return value.Equals("-1") ? error : check;
            }
            else
            {
                return DataConverter.CBool(value) ? check : error;
            }
        }

        // 设定模板
        protected void SetTemplate(object sender, EventArgs e)
        {
            modBll.UpdateTemplate(this.TxtTemplate_hid.Value, ModelID);
            function.WriteSuccessMsg("设定模板成功", "ModelField.aspx?ModelID=" + ModelID.ToString() + "&ModelType=" + ModelType);
        }
        private DataTable SysField_Product(DataTable dt)
        {
            dt.DefaultView.RowFilter = "sys_type=0";
            dt = dt.DefaultView.ToTable();
            string[] fields = "商品编号:ProCode,品名:Proname,简介:Proinfo,商品详情:Procontent,缩略图:Thumbnails,添加时间:AddTime,单位:Prounit".Split(',');
            foreach (string field in fields)
            {
                DataRow dr = dt.NewRow();
                dr["sys_type"] = 1;
                dr["FieldAlias"] = field.Split(':')[0];
                dr["FieldName"] = field.Split(':')[1];
                dr["FieldType"] = "TextType";
                dr["OrderID"] = 0;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable SysField_Pub(DataTable dt)
        {
            dt.DefaultView.RowFilter = "sys_type=0";
            dt = dt.DefaultView.ToTable();
            string[] fields = "互动编号:Pubupid,用户名:PubUserName,用户ID:PubUserID,内容ID:PubContentid,录入者:PubInputer,父级ID:Parentid,IP地址:PubIP,互动数量:Pubnum,开始:Pubstart,互动标题:PubTitle,互动内容:PubContent,添加时间:PubAddTime,评价:Optimal".Split(',');
            foreach (string field in fields)
            {
                DataRow dr = dt.NewRow();
                dr["sys_type"] = 1;
                dr["FieldAlias"] = field.Split(':')[0];
                dr["FieldName"] = field.Split(':')[1];
                dr["FieldType"] = "TextType";
                dr["OrderID"] = 0;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable SysField_ApplyStore(DataTable dt)
        {
            dt.DefaultView.RowFilter = "sys_type=0";
            dt = dt.DefaultView.ToTable();
            string[] fields = "用户ID:UserID,用户名:UserName,店铺名称:StoreName,店铺信誉:StoreCredit,店铺申请状态:StoreCommendState,店铺状态:StoreState,店铺风格ID:StoreStyleID,店铺模型ID:StoreModelID,店铺风格:StoreStyle,添加时间:AddTime".Split(',');
            foreach (string field in fields)
            {
                DataRow dr = dt.NewRow();
                dr["sys_type"] = 1;
                dr["FieldAlias"] = field.Split(':')[0];
                dr["FieldName"] = field.Split(':')[1];
                dr["FieldType"] = "TextType";
                dr["OrderID"] = 0;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public void MyBind()
        {
            M_ModelInfo modeli = modBll.GetModelById(ModelID);
            tabNameStr = "当前表:" + modeli.TableName;
            modelName = modeli.ModelName;
            this.TxtTemplate_hid.Value = modeli.ContentModule;
            DataTable tablelist = bll.SelByModelID(ModelID, true);
            switch (ModelType.ToString())
            {
                case "1"://内容模型
                    this.RPT.DataSource = tablelist;
                    break;
                case "2"://商城模型
                    tablelist = SysField_Product(tablelist);
                    this.RPT.DataSource = tablelist;
                    break;
                case "3"://用户模型和黄页申请模型 
                    this.RPT.DataSource = tablelist;
                    break;
                case "4"://黄页内容模型
                    this.RPT.DataSource = tablelist;
                    break;
                case "5"://店铺商品模型 修改
                    this.RPT.DataSource = SysField_Product(tablelist);
                    break;
                case "6"://店铺申请模型  修改
                    this.RPT.DataSource = SysField_ApplyStore(tablelist);
                    break;
                case "7"://互动模型  修改
                    this.RPT.DataSource = SysField_Pub(tablelist);
                    break;
                case "8"://功能模型
                    this.RPT.DataSource = bll.GetModelFieldListall(ModelID);
                    break;
                case "11"://CRM模型
                    this.RPT.DataSource = tablelist;
                    break;
                case "12"://OA办公模型
                    this.RPT.DataSource = tablelist;
                    break;
            }
            //this.rptSystemModel.DataBind();//系统字段
            this.RPT.DataBind();//自定义字段
        }

        protected void rptModelField_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int Id = DataConverter.CLng(e.CommandArgument.ToString().Split('|')[0].Trim());

            M_ModelField nowFieldinfo = this.bll.GetModelByID(ModelID.ToString(), Id);

            //if (e.CommandName == "UpMove")//上移
            //{
            //    if (nowFieldinfo.OrderID <= this.bll.GetMinOrder(nowFieldinfo.ModelID))
            //        return;
            //    M_ModelField topFieldPre = this.bll.GetPreField(nowFieldinfo.ModelID, nowFieldinfo.OrderID);
            //    int nowID = nowFieldinfo.OrderID;
            //    int preID = topFieldPre.OrderID;
            //    nowFieldinfo.OrderID = preID;
            //    topFieldPre.OrderID = nowID;
            //    this.bll.UpdateOrder(nowFieldinfo, topFieldPre);
            //    Response.Redirect(Request.RawUrl);
            //}
            //if (e.CommandName == "DownMove")//下移
            //{
            //    if (nowFieldinfo.OrderID >= this.bll.GetMaxOrder(nowFieldinfo.ModelID))
            //        return;
            //    M_ModelField fieldNext = this.bll.GetNextField(nowFieldinfo.ModelID, nowFieldinfo.OrderID);
            //    int nowID = nowFieldinfo.OrderID;
            //    int nextID = fieldNext.OrderID;
            //    nowFieldinfo.OrderID = nextID;
            //    fieldNext.OrderID = nowID;
            //    this.bll.UpdateOrder(fieldNext, nowFieldinfo);
            //    Response.Redirect(Request.RawUrl);
            //}
            if (e.CommandName == "IsPlay")
            {
                if (nowFieldinfo.ShowList == false)
                {
                    nowFieldinfo.ShowList = true;
                }
                else
                    nowFieldinfo.ShowList = false;
                this.bll.UpdateShowList(nowFieldinfo);
                (e.CommandSource as LinkButton).Text = nowFieldinfo.ShowList ? "不显示" : "显示";
            }
            if (e.CommandName == "Delete")
            {
                bll.DelByFieldID(Id);
            }
            if (e.CommandName == "ChangeValue")
            {
                string col = e.CommandArgument.ToString().Split('|')[1].Trim();
                switch (col)
                {
                    case "Null":
                        nowFieldinfo.IsNotNull = !nowFieldinfo.IsNotNull;
                        break;
                    case "Show":
                        nowFieldinfo.ShowList = !nowFieldinfo.ShowList;
                        (e.Item.FindControl("lbtnShow") as LinkButton).Text = nowFieldinfo.ShowList ? "不显示" : "显示";
                        break;
                    case "Copy":
                        nowFieldinfo.IsCopy ^= 1;
                        break;
                    case "BatchAdd":
                        nowFieldinfo.Islotsize = !nowFieldinfo.Islotsize;
                        break;
                }
                bll.Update(nowFieldinfo);
            }
            MyBind();
        }
        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            string imgurl = ibtn.ImageUrl;
            string imgName = imgurl.Substring(imgurl.LastIndexOf('/') + 1, imgurl.Length - imgurl.LastIndexOf('.') - 1);
            // Label1.Text = imgName;

            if (imgName == "yes")
            {
                ibtn.ImageUrl = "~/Images/no.gif";
            }
            else
            {
                ibtn.ImageUrl = "~/Images/yes.gif";
            }
        }
        protected void Order_B_Click(object sender, EventArgs e)
        {
            string[] ordervalues = Order_Hid.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string value in ordervalues)
            {
                if (string.IsNullOrWhiteSpace(value.Split('|')[0])) { continue; }
                int fid = Convert.ToInt32(value.Split('|')[0]);
                int orderid = Convert.ToInt32(value.Split('|')[1]);
                M_ModelField modfield = bll.GetModelByID(ModelID.ToString(), fid);
                modfield.OrderID = orderid;
                bll.UpdateOrder(modfield);
            }
            function.WriteSuccessMsg("保存排序成功");
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                bll.DelByFieldID(ids);
                function.WriteSuccessMsg("删除成功");
            }
            MyBind();
        }
        public string GetChk()
        {
            if (DataConverter.CBool(Eval("sys_type", ""))) { return ""; }
            else { return "<input type='checkbox' name='idchk' value='" + Eval("FieldID") + "' />"; }
        }
    }
}