using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Config
{
    public partial class CitizenXmlConfig : CustomerPageAction
    {
        //Convert.ToInt32(Level), PreGrade
        private string xmlPPath { get { return Server.MapPath("/Config/MenuData.xml"); } }
        //当前层级,父节点名称
        public int Level
        {
            get
            {
                if (DataConverter.CLng(LblLevel.Text) < 1)
                { int _num = DataConverter.CLng(Request.QueryString["Level"]); _num = _num < 1 ? 1 : _num; LblLevel.Text = _num.ToString(); }
                return DataConverter.CLng(LblLevel.Text);
            }
            set
            {
                LblLevel.Text = value.ToString();
            }
        }
        public string PreGrade
        {
            get
            {
                if (string.IsNullOrEmpty(LblPreGrade.Text))
                {
                    LblPreGrade.Text = Request.QueryString["PreGrade"];
                }
                return LblPreGrade.Text;
            }
            set { LblPreGrade.Text = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind(Level, PreGrade);
            }
        }
        //层级,上级名称
        public void DataBind(int num, string higherup)
        {
            txtGradeName.Text = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPPath);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));
            XmlNodeList listnode = null;
            string ItemName = "";
            if (num == 1)
            {
                listnode = xmlDoc.GetElementsByTagName("Country");
                ItemName = "gname";
                PreGrade = "";
            }
            else if (num == 2)
            {
                listnode = getXMLNode(xmlDoc.GetElementsByTagName("Country"), higherup, "gname").ChildNodes;
                PreGrade = getXMLNode(xmlDoc.GetElementsByTagName("Country"), higherup, "gname").Attributes.GetNamedItem("gname").InnerText.ToString();
                ItemName = "pname";
            }
            else if (num == 3)
            {
                HdnCountry.Value = PreGrade.ToString();
                XmlNodeList pNodes = getXMLNode(xmlDoc.GetElementsByTagName("Country"), PreGrade.ToString(), "gname").ChildNodes;
                listnode = getXMLNode(pNodes, higherup, "pname").ChildNodes;
                PreGrade = getXMLNode(pNodes, higherup, "pname").Attributes.GetNamedItem("pname").InnerText.ToString();
                ItemName = "cname";
            }
            for (int i = 0; i < listnode.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["FileName"] = listnode[i].Attributes.GetNamedItem(ItemName).Value;
                dt.Rows.Add(dr);
            }
            Level = num;
            EGV.DataSource = dt.DefaultView;
            EGV.DataBind();
            string bread = "<li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='" + CustomerPageAction.customPath2 + "Addon/DictionaryManage.aspx'>数据字典</a></li>";
            if (num > 1)
            {
                bread += "<li><a href='CitizenXmlConfig.aspx?Level=" + (--num) + "&PreGrade=" + PreGrade + "'>" + PreGrade + "</a></li>";
            }
            bread += "<li class='active'>国籍字典管理</li>";
            Call.SetBreadCrumb(Master, bread);
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.EGV.PageIndex = e.NewPageIndex;
            DataBind(Convert.ToInt32(Level), PreGrade);
        }
        protected void Gdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtn = (LinkButton)e.Row.FindControl("LinkButton3");
                if (Level.ToString().Equals("3"))
                {
                    lbtn.Visible = false;
                    return;
                }
                int i = e.Row.RowIndex + 1;
                e.Row.Attributes["ondblclick"] = "javascript:showdown('" + i + "')"; ;
            }
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string FileName = e.CommandArgument.ToString();
                DelCountrys(FileName);
            }
            else if (e.CommandName == "Edit1")
            {
                string FileName = e.CommandArgument.ToString();
                this.txtGradeName.Text = FileName;
                this.HdnFileName.Value = FileName;
                this.btnSave.Text = "修改";
            }
            else if (e.CommandName == "DicList")
            {
                int num = Convert.ToInt32(Level);
                DataBind(++num, e.CommandArgument.ToString());
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string num = Level.ToString();
            if (btnSave.Text.ToString().Equals("修改"))
            {
                EidtCountrys();
                this.btnSave.Text = "添加";
            }
            else
            {
                AddCountrys();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string num = Level.ToString();
            string higherup = PreGrade;
            if (num == "1")
            {
                DataBind(1, higherup);
            }
            else if (num == "2")
                DataBind(1, higherup);
            else if (num == "3")
                DataBind(2, higherup);
        }
        // 根据条件获取节点
        public XmlNode getXMLNode(XmlNodeList listNode, string strName, string ItemName)
        {
            int num = 0;
            for (int i = 0; i < listNode.Count; i++)
            {
                if (listNode[i].Attributes.GetNamedItem(ItemName).InnerText == strName)
                {
                    num = i;
                    break;
                }
            }
            return listNode[num];
        }
        //编辑地址
        public void EidtCountrys()
        {
            string num = Level.ToString();
            string strName = txtGradeName.Text.Trim();
            string FileName = HdnFileName.Value.ToString();
            string higherup = PreGrade.ToString();
            string CountryFileName = HdnCountry.Value.ToString();
            if (strName == "" || strName == null)
            {
                function.WriteErrMsg("请输入修改内容!");
                return;
            }
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPPath);
                if (num == "1")
                {
                    XmlNodeList listCountry = xmlDoc.GetElementsByTagName("Country");
                    XmlNode odlnode = getXMLNode(listCountry, FileName, "gname");
                    odlnode.Attributes.GetNamedItem("gname").InnerText = strName;
                }
                else if (num == "2")
                {
                    XmlNodeList listProvince = getXMLNode(xmlDoc.GetElementsByTagName("Country"), CountryFileName, "gname").ChildNodes;
                    XmlNode odlnode = getXMLNode(listProvince, FileName, "pname");
                    odlnode.Attributes.GetNamedItem("pname").InnerText = strName;
                }
                else if (num == "3")
                {
                    XmlNodeList listProvince = getXMLNode(xmlDoc.GetElementsByTagName("Country"), CountryFileName, "gname").ChildNodes;
                    XmlNodeList listCity = getXMLNode(listProvince, higherup, "pname").ChildNodes;
                    XmlNode odlnode = getXMLNode(listCity, FileName, "cname");
                    odlnode.Attributes.GetNamedItem("cname").InnerText = strName;
                }
                xmlDoc.Save(xmlPPath);
                function.WriteSuccessMsg("编辑成功!");
                this.txtGradeName.Text = "";
            }
            catch
            {
                function.WriteErrMsg("编辑失败!");
            }
            DataBind(Convert.ToInt32(Level), PreGrade);
        }
        //添加失败
        public void AddCountrys()
        {
            string strName = txtGradeName.Text.Trim();
            if (strName == "")
            {
                function.WriteErrMsg("请输入添加内容!");
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPPath);
            if (Level.ToString().Equals("1"))
            {
                XmlNodeList listCountry = xmlDoc.GetElementsByTagName("Countrys");
                XmlNode root = listCountry[0];//查找 
                XmlElement xe1 = xmlDoc.CreateElement("Country");//创建一个节点 
                xe1.SetAttribute("gname", strName);//设置该节点genre属性 
                xe1.InnerText = "";//设置文本节点         
                root.AppendChild(xe1);//添加到节点中 
            }
            else if (Level.ToString().Equals("2"))
            {
                XmlNodeList listCountry = xmlDoc.GetElementsByTagName("Country");
                XmlNode root = getXMLNode(listCountry, PreGrade.ToString(), "gname");//查找 
                XmlElement xe1 = xmlDoc.CreateElement("Province");//创建一个节点 
                xe1.SetAttribute("pname", strName);//设置该节点pname属性 
                xe1.InnerText = "";//设置文本节点 
                root.AppendChild(xe1);//添加到节点中 
            }
            else if (Level.ToString().Equals("3"))
            {
                XmlNodeList listCountry = xmlDoc.GetElementsByTagName("Country");
                XmlNode root = getXMLNode(getXMLNode(listCountry, HdnCountry.Value.ToString(), "gname").ChildNodes, PreGrade.ToString(), "pname");//查找 
                XmlElement xe1 = xmlDoc.CreateElement("City");//创建一个节点 
                xe1.SetAttribute("cname", strName);//设置该节点genre属性 
                root.AppendChild(xe1);//添加到节点中 
            }
            xmlDoc.Save(xmlPPath);
            DataBind(Convert.ToInt32(Level), PreGrade);
        }
        // 删除地址
        public void DelCountrys(string FileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPPath);
            if (Level.ToString().Equals("1"))
            {
                XmlNodeList listCountrys = xmlDoc.GetElementsByTagName("Countrys");
                XmlNodeList listCountry = xmlDoc.GetElementsByTagName("Country");
                XmlNode root = getXMLNode(listCountry, FileName, "gname");
                listCountrys[0].RemoveChild(root);
            }
            else if (Level.ToString().Equals("2"))
            {
                XmlNodeList listCountry = xmlDoc.GetElementsByTagName("Country");
                XmlNode CountryNode = getXMLNode(listCountry, PreGrade, "gname");
                XmlNode root = getXMLNode(getXMLNode(listCountry, PreGrade, "gname").ChildNodes, FileName, "pname");
                CountryNode.RemoveChild(root);
            }
            else if (Level.ToString().Equals("3"))
            {
                XmlNodeList listCountry = xmlDoc.GetElementsByTagName("Country");
                XmlNode CountryNode = getXMLNode(listCountry, HdnCountry.Value.ToString(), "gname");
                XmlNode root = getXMLNode(getXMLNode(listCountry, HdnCountry.Value.ToString(), "gname").ChildNodes, PreGrade, "pname");
                XmlNode CityNode = getXMLNode(root.ChildNodes, FileName, "cname");
                root.RemoveChild(CityNode);
            }
            xmlDoc.Save(xmlPPath);
            DataBind(Convert.ToInt32(Level), PreGrade);
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string fnames = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(fnames))
            {
                foreach (string fname in fnames.Split(','))
                {
                    DelCountrys(fname);
                }
            }
        }
    }
}