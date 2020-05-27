namespace ZoomLa.WebSite.User.Content
{
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
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.Model;
    
    using System.Xml;

    public partial class EditContentpage : System.Web.UI.Page
    {
        protected B_Node bnode = new B_Node();
        protected B_Model bmode = new B_Model();
        protected B_ContentField bshow = new B_ContentField();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Content bll = new B_Content();
        B_Product butbll = new B_Product();
        B_Product bpbll = new B_Product();
        protected int NodeID;
        protected int ModelID;
        protected int GeneralID;
        public M_UserInfo UserInfo;
        private B_User buser = new B_User();
        protected B_Sensitivity sll = new B_Sensitivity();
        //protected B_UserShopClass cll = new B_UserShopClass();
        protected B_User ull = new B_User();
        protected B_UserPromotions ups = new B_UserPromotions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DataTable table = cll.SelectByUserName(ull.GetLogin().UserName, ull.GetLogin().GroupID);
                //DropDownList1.DataSource = table;
                //DropDownList1.DataTextField = "Classname";
                //DropDownList1.DataValueField = "id";
                //DropDownList1.DataBind();


                this.DropDownList1.Items.Insert(0, new ListItem("请选择分类", ""));
                this.UserInfo = buser.GetLogin();

                if (string.IsNullOrEmpty(base.Request.QueryString["GeneralID"]))
                {
                    function.WriteErrMsg("没有指定要修改的内容ID!");
                }
                else
                {
                    this.GeneralID = DataConverter.CLng(base.Request.QueryString["GeneralID"]);
                }
                M_CommonData Cdata = this.bll.GetCommonData(this.GeneralID);
                if (UserInfo.UserName != Cdata.Inputer)
                {
                    function.WriteErrMsg("不能编辑不属于自己输入的内容!");
                }
                this.NodeID = Cdata.NodeID;
                this.ModelID = Cdata.ModelID;
                DropDownList1.SelectedValue = Cdata.OrederClass.ToString();

                if (bmode.GetModelById(this.ModelID).ModelType == 2)
                {
                    string i = "";
                    DataTable dt = bpbll.ProductSearch(7, GeneralID.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        i = dt.Rows[0]["ID"].ToString();
                    }
                    Response.Redirect("addproduct.aspx?menu=edit&ID=" + i + "&ModelID=" + this.ModelID + "&NodeID=" + this.NodeID);//跳转到商城
                }
                if (bmode.GetModelById(this.ModelID).ModelType == 5)
                {
                    string i = "";
                    DataTable dt = butbll.ProductSearch(7, GeneralID.ToString(), buser.GetLogin().UserID);
                    if (dt.Rows.Count > 0)
                    {
                        i = dt.Rows[0]["ID"].ToString();
                    }
                    Response.Redirect("../UserShop/ProductAdd.aspx?menu=edit&ID=" + i + "&ModelID=" + this.ModelID + "&NodeID=" + this.NodeID);//跳转到店铺
                }

                string ModelName = this.bmode.GetModelById(this.ModelID).ModelName;
                string NodeName = this.bnode.GetNodeXML(this.NodeID).NodeName;
                this.NodeName_L.Text = "修改" + NodeName;
                this.EBtnSubmit.Text += this.bmode.GetModelById(this.ModelID).ItemName;
                this.Label2.Text = NodeName;
                this.txtTitle.Text = Cdata.Title;
                this.TxtTagKey.Text = Cdata.TagKey;

                this.pronum.Text = Cdata.Pronum.ToString();
                this.proweek.Text = Cdata.ProWeek.ToString();
                this.BidType.SelectedValue = Cdata.BidType.ToString();
                this.bidmoney.Text = Cdata.BidMoney.ToString("F2");

                this.HdnItem.Value = this.GeneralID.ToString();

                DataTable dtContent = this.bll.GetContent(this.GeneralID);
                this.ModelHtml.Text = this.bfield.InputallHtml(this.ModelID, NodeID, new ModelConfig()
                {
                    Source = ModelConfig.SType.UserContent,
                    ValueDT = dtContent
                });
                DataTable ds = bfield.SelByModelID(this.ModelID,true);
                this.Title_L.Text = GetAlias("Title",ds);
            }

            M_UserInfo infos = buser.GetLogin();
            M_UserPromotions upsinfo = ups.GetSelect(this.NodeID, infos.GroupID);

            #region 权限
            GetNodePreate(this.NodeID);
            #endregion
        }

        public string GetAlias(string field, DataTable dt)
        {
            DataRow[] drs = dt.Select("FieldName='" + field + "'");
            return drs.Length > 0 ? drs[0]["FieldAlias"].ToString() : "未定义";
        }
        private void GetNodePreate(int prentid)
        {
            M_Node nodes = bnode.GetNodeXML(prentid);
            GetMethod(nodes);
            if (nodes.ParentID > 0)
            {
                GetNodePreate(nodes.ParentID);
            }
        }

        private void GetMethod(M_Node nodeinfo)
        {
            if (nodeinfo.Purview != null && nodeinfo.Purview != "")
            {
                DataRow auitdr = bnode.GetNodeAuitDT(nodeinfo.Purview).Rows[0];
                string input_v = auitdr["input"].ToString();
                if (input_v != null && input_v != "")
                {
                    string tmparr = "," + input_v + ",";

                    switch (this.UserInfo.Status)
                    {
                        case 0://已认证
                            if (tmparr.IndexOf("," + this.UserInfo.GroupID.ToString() + ",") == -1)
                            {
                                if (tmparr.IndexOf(",-1,") == -1)
                                {
                                    function.WriteErrMsg("很抱歉！您没有权限在该栏目下修改信息！");
                                }
                            }
                            break;
                        default://未认证
                            if (tmparr.IndexOf(",-2,") == -1)
                            {
                                function.WriteErrMsg("很抱歉！您没有权限在该栏目下修改信息！");
                            }
                            break;
                    }
                }
                else
                {
                    //为空
                    function.WriteErrMsg("很抱歉！您没有权限在该栏目下修改信息！");
                }
            }
        }

        public string returnnodelist = "";
        /// <summary>
        /// 获得父级树
        /// </summary>
        /// <returns></returns>
        public string GetParentTree(int NodeID)
        {
            M_Node nodelist = bnode.GetNodeXML(NodeID);
            returnnodelist = NodeID + "," + returnnodelist;
            if (nodelist.NodeID > 0 && nodelist.ParentID > 0)
            {
                GetParentTree(nodelist.ParentID);
            }

            if (returnnodelist != "")
            {
                if (BaseClass.Left(returnnodelist, 1) != ",")
                {
                    returnnodelist = "," + returnnodelist;
                }
                if (BaseClass.Right(returnnodelist, 1) != ",")
                {
                    returnnodelist = returnnodelist + ",";
                }

            }
            return returnnodelist;
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                this.GeneralID = DataConverter.CLng(this.HdnItem.Value);
                M_CommonData CData = this.bll.GetCommonData(this.GeneralID);
                this.NodeID = CData.NodeID;
                this.ModelID = CData.ModelID;
                CData.Title = BaseClass.CheckInjection(this.txtTitle.Text);
                CData.GeneralID = this.GeneralID;
                //CData.EliteLevel = this.ChkAudit.Checked ? 1 : 0;
                CData.InfoID = "";
                CData.SpecialID = "";

                CData.ProWeek = DataConverter.CLng(this.proweek.Text);
                CData.Pronum = DataConverter.CLng(this.pronum.Text);
                CData.BidType = DataConverter.CLng(this.BidType.SelectedValue);
                //CData.IsBid = (CData.BidType > 0) ? 1 : 0;
                CData.BidMoney = DataConverter.CDouble(this.bidmoney.Text);
                CData.OrederClass = DataConverter.CLng(DropDownList1.SelectedValue.ToString());
                CData.FirstNodeID = GetFriestNode(this.NodeID);
                CData.ParentTree = GetParentTree(this.NodeID);
                CData.TopImg = "";//首页图片

                DataTable dt = this.bfield.GetModelFieldList(this.ModelID);
                Call commonCall = new Call();
                DataTable table = commonCall.GetDTFromPage(dt,this.Page,ViewState);
                string Keyword = BaseClass.CheckInjection(this.TxtTagKey.Text.Trim());
                string OldKey = CData.TagKey;
                CData.TagKey = BaseClass.CheckInjection(Keyword);

                this.bll.UpdateContent(table, CData);

                B_KeyWord kll = new B_KeyWord();
                if (!string.IsNullOrEmpty(Keyword))
                {
                    if (string.IsNullOrEmpty(OldKey))
                    {
                        string[] arrKey = Keyword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int tt = 0; tt < arrKey.Length; tt++)
                        {
                            if (kll.IsExist(arrKey[tt]))
                            {
                                M_KeyWord kinfo = kll.GetKeyByName(arrKey[tt]);
                                kinfo.QuoteTimes++;
                                kinfo.LastUseTime = DateTime.Now;
                                if (string.IsNullOrEmpty(kinfo.ArrGeneralID))
                                    kinfo.ArrGeneralID = CData.GeneralID.ToString() + ",";
                                else
                                    kinfo.ArrGeneralID = kinfo.ArrGeneralID + CData.GeneralID.ToString() + ",";
                                kll.Update(kinfo);
                            }
                            else
                            {
                                M_KeyWord kinfo1 = new M_KeyWord();
                                kinfo1.KeyWordID = 0;
                                kinfo1.KeywordText = BaseClass.CheckInjection(arrKey[tt]);
                                kinfo1.KeywordType = 1;
                                kinfo1.LastUseTime = DateTime.Now;
                                kinfo1.Hits = 0;
                                kinfo1.Priority = 10;
                                kinfo1.QuoteTimes = 1;
                                kinfo1.ArrGeneralID = "," + CData.GeneralID.ToString() + ",";
                                kll.Add(kinfo1);
                            }
                        }
                    }
                    else
                    {
                        string[] arrKey1 = Keyword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] arrOld = OldKey.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int it = 0; it < arrKey1.Length; it++)
                        {
                            if (!FindInArr(arrOld, arrKey1[it]))
                            {
                                if (kll.IsExist(arrKey1[it]))
                                {
                                    M_KeyWord kinfo = kll.GetKeyByName(arrKey1[it]);
                                    kinfo.QuoteTimes++;
                                    kinfo.LastUseTime = DateTime.Now;
                                    if (string.IsNullOrEmpty(kinfo.ArrGeneralID))
                                        kinfo.ArrGeneralID = CData.GeneralID.ToString() + ",";
                                    else
                                        kinfo.ArrGeneralID = kinfo.ArrGeneralID +  CData.GeneralID.ToString() + ",";
                                    kll.Update(kinfo);
                                }
                                else
                                {
                                    M_KeyWord kinfo1 = new M_KeyWord();
                                    kinfo1.KeyWordID = 0;
                                    kinfo1.KeywordText = BaseClass.CheckInjection(arrKey1[it]);
                                    kinfo1.KeywordType = 1;
                                    kinfo1.LastUseTime = DateTime.Now;
                                    kinfo1.Hits = 0;
                                    kinfo1.Priority = 10;
                                    kinfo1.QuoteTimes = 1;
                                    kinfo1.ArrGeneralID = "," + CData.GeneralID.ToString() + ",";
                                    kll.Add(kinfo1);
                                }
                            }
                        }
                    }
                }
                Response.Redirect("MyContent.aspx?NodeID=" + this.NodeID);
            }
        }
        private bool FindInArr(string[] OldArr, string key)
        {
            bool flag = false;
            for (int i = 0; i < OldArr.Length; i++)
            {
                if (key == OldArr[i])
                    return true;
            }
            return flag;
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyContent.aspx?NodeID=" + this.NodeID);
        }
        /// <summary>
        /// 获得第一级节点ID
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public int GetFriestNode(int NodeID)
        {
            GetNo(NodeID);
            return FNodeID;
        }

        protected int FNodeID = 0;

        public void GetNo(int NodeID)
        {
            M_Node nodeinfo = bnode.GetNodeXML(NodeID);
            int ParentID = nodeinfo.ParentID;
            if (DataConverter.CLng(nodeinfo.ParentID) > 0)
            {
                GetNo(nodeinfo.ParentID);
            }
            else
            {
                FNodeID = nodeinfo.NodeID;
            }
        }
    }
}