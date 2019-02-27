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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Xml;
using System.IO;

public partial class manage_AddCRM_CRMDictionary : CustomerPageAction
{
    protected int labe = 0;
    private XmlDocument myDoc;
    private string xmlPath = "/Config/CRM_Dictionary.xml";
    private M_CrmAuth crmModel = new M_CrmAuth();
    private DataTable authDT = new DataTable();//用来存权限信息
    private B_CrmAuth crmBll = new B_CrmAuth();
    private B_Admin badmin = new B_Admin();

    public manage_AddCRM_CRMDictionary()
    {
        //构造函数
        myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("/Config/CRM_Dictionary.xml"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //---------权限限制(增加修改权限)
            M_AdminInfo info = B_Admin.GetAdminByID(badmin.GetAdminLogin().AdminId);//info中有role信息
            authDT = crmBll.GetAuthTable(info.RoleList.Split(','));//
            if (!crmBll.IsHasAuth(authDT, "AllowOptionValue", info))//如果ID不是自己的ID或ID为空则跳转到自己的ID上
            {
                Response.Redirect("~/Prompt/NoPermissions.htm");
                Response.End();
            }

            liTitle.Text = UC_BI();
            DataTable dt = GetTableList();
            this.repeater1.DataSource = dt;
            this.repeater1.DataBind();
            labe = repeater1.Items.Count;
            string node_id = Request.QueryString["FieldName"].ToString();
            ViewState["node_id"] = node_id;

            switch (node_id)
            {
                case "Client_Area":
                    Response.Write("<script>document.title='客户区域表'</script>");
                    this.Label1.Text = "客户区域表";
                    this.Tit.Text = "客户区域表";
                    this.Tit0.Text = "添加客户区域";
                    break;
                case "Client_Calling":
                    Response.Write("<script>document.title='所属行业'</script>");
                    this.Label1.Text = "所属行业";
                    this.Tit.Text = "所属行业";
                    this.Tit0.Text = "添加所属行业";
                    break;
                case "Client_Value":
                    Response.Write("<script>document.title='价格评估'</script>");
                    this.Label1.Text = "价格评估";
                    this.Tit.Text = "价格评估";
                    this.Tit0.Text = "添加价格评估";
                    break;
                case "Client_Credit":
                    Response.Write("<script>document.title='信用等级'</script>");
                    this.Label1.Text = "信用等级";
                    this.Tit.Text = "信用等级";
                    this.Tit0.Text = "添加信用等级";
                    break;
                case "Client_Importance":
                    Response.Write("<script>document.title='重要程度'</script>");
                    this.Label1.Text = "重要程度";
                    this.Tit.Text = "重要程度";
                    this.Tit0.Text = "添加重要程度";
                    break;
                case "Client_Connection":
                    Response.Write("<script>document.title='关系等级'</script>");
                    this.Label1.Text = "关系等级";
                    this.Tit.Text = "关系等级";
                    this.Tit0.Text = "添加关系等级";
                    break;
                case "Client_Group":
                    Response.Write("<script>document.title='客户组别'</script>");
                    this.Label1.Text = "客户组别";
                    this.Tit.Text = "客户组别";
                    this.Tit0.Text = "添加客户组别";
                    break;
                case "Client_Source":
                    Response.Write("<script>document.title='客户来源'</script>");
                    this.Label1.Text = "客户来源";
                    this.Tit.Text = "客户来源";
                    this.Tit0.Text = "添加客户来源";
                    break;
                case "Client_Phase":
                    Response.Write("<script>document.title='阶段'</script>");
                    this.Label1.Text = "阶段";
                    this.Tit.Text = "阶段";
                    this.Tit0.Text = "添加阶段";
                    break;
                case "Co_Status":
                    Response.Write("<script>document.title='行业地位'</script>");
                    this.Label1.Text = "行业地位";
                    this.Tit.Text = "行业地位";
                    this.Tit0.Text = "添加行业地位";
                    break;
                case "Co_Size":
                    Response.Write("<script>document.title='公司规模'</script>");
                    this.Label1.Text = "公司规模";
                    this.Tit.Text = "公司规模";
                    this.Tit0.Text = "添加公司规模";
                    break;
                case "Co_Management":
                    Response.Write("<script>document.title='经营状态'</script>");
                    this.Label1.Text = "经营状态";
                    this.Tit.Text = "经营状态";
                    this.Tit0.Text = "添加经营状态";
                    break;
                case "Complain_Type":
                    Response.Write("<script>document.title='投诉记录'</script>");
                    this.Label1.Text = "投诉记录";
                    this.Tit.Text = "投诉记录";
                    this.Tit0.Text = "添加投诉记录";
                    break;
                case "Complain_Mode":
                    Response.Write("<script>document.title='服务方式'</script>");
                    this.Label1.Text = "服务方式";
                    this.Tit.Text = "服务方式";
                    this.Tit0.Text = "添加服务方式";
                    break;
                case "Complain_Urgency":
                    Response.Write("<script>document.title='紧急程度'</script>");
                    this.Label1.Text = "紧急程度";
                    this.Tit.Text = "紧急程度";
                    this.Tit0.Text = "添加紧急程度";
                    break;
                case "Linkman_Income":
                    Response.Write("<script>document.title='月收入'</script>");
                    this.Label1.Text = "月收入";
                    this.Tit.Text = "月收入";
                    this.Tit0.Text = "添加月收入";
                    break;
                case "Linkman_Education":
                    Response.Write("<script>document.title='学历'</script>");
                    this.Label1.Text = "学历";
                    this.Tit.Text = "学历";
                    this.Tit0.Text = "添加学历";
                    break;
                case "Service_Type":
                    Response.Write("<script>document.title='服务类型'</script>");
                    this.Label1.Text = "服务类型";
                    this.Tit.Text = "服务类型";
                    this.Tit0.Text = "添加服务类型";
                    break;
                case "Service_Mode":
                    Response.Write("<script>document.title='服务方式'</script>");
                    this.Label1.Text = "服务方式";
                    this.Tit.Text = "服务方式";
                    this.Tit0.Text = "服务方式";
                    break;
                case "Service_TakeTime":
                    Response.Write("<script>document.title='花费时间'</script>");
                    this.Label1.Text = "花费时间";
                    this.Tit.Text = "花费时间";
                    this.Tit0.Text = "添加花费时间";
                    break;
                case "Service_Result":
                    Response.Write("<script>document.title='服务结果'</script>");
                    this.Label1.Text = "服务结果";
                    this.Tit.Text = "服务结果";
                    this.Tit0.Text = "添加服务结果";
                    break;
                case "Service_Score":
                    Response.Write("<script>document.title='客户评价'</script>");
                    this.Label1.Text = "客户评价";
                    this.Tit.Text = "客户评价";
                    this.Tit0.Text = "添加客户评价";
                    break;
                default:
                    this.Tit.Text = "客户信息表";
                    break;
            }
        }
        Call.SetBreadCrumb(Master, "<li>CRM配置</li><li>" + liTitle.Text + "</li><li> <a href='CRMDictionary.aspx?FieldName=" + Request.QueryString["FieldName"] + "'>" + Label1.Text + "</a></li>" + Call.GetHelp(48));
    }

    public static DataTable MakeTable()
    {
        // Create a DataTable. 
        DataTable table = new DataTable();
        table.Columns.Add("sort", typeof(System.String));
        table.Columns.Add("default_", typeof(System.Boolean));
        table.Columns.Add("enable", typeof(System.Boolean));
        table.Columns.Add("content", typeof(System.String));
        return table;
    }

    public DataTable GetTableList()
    {
        string node_id = "";
        node_id = Request.QueryString["FieldName"].ToString();
        XmlNodeList list = myDoc.SelectNodes("/CRM_Dictionary/" + node_id);
        XmlElement xmle = null;
        DataTable dt = MakeTable();
        for (int i = 0; i < list.Count; i++)
        {
            xmle = (XmlElement)list[i];
            DataRow dr = dt.NewRow();
            dr["sort"] = xmle.SelectSingleNode("sort").InnerText;
            dr["default_"] = xmle.SelectSingleNode("default_").InnerText;
            dr["enable"] = xmle.SelectSingleNode("enable").InnerText;
            dr["content"] = xmle.SelectSingleNode("content").InnerText;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    public void WriteXML(string node_text, int sort, string default_, string enable, string content)
    {
        //添加元素--UserCode
        XmlElement ele = myDoc.CreateElement("sort");
        XmlText text = myDoc.CreateTextNode(sort.ToString());

        //添加元素--UserName
        XmlElement ele1 = myDoc.CreateElement("default_");
        XmlText text1 = myDoc.CreateTextNode(default_);

        //添加元素--UserPwd
        XmlElement ele2 = myDoc.CreateElement("enable");
        XmlText text2 = myDoc.CreateTextNode(enable);

        XmlElement ele3 = myDoc.CreateElement("content");
        XmlText text3 = myDoc.CreateTextNode(content);
        //添加节点 node_text要对应我们xml文件中的节点名字
        XmlNode newElem = myDoc.CreateNode("element", node_text, "");

        //在节点中添加元素
        newElem.AppendChild(ele);
        newElem.LastChild.AppendChild(text);
        newElem.AppendChild(ele1);
        newElem.LastChild.AppendChild(text1);
        newElem.AppendChild(ele2);
        newElem.LastChild.AppendChild(text2);
        newElem.AppendChild(ele3);
        newElem.LastChild.AppendChild(text3);

        //将节点添加到文档中
        XmlElement root = myDoc.DocumentElement;
        root.AppendChild(newElem);

        myDoc.Save(Server.MapPath("/Config/CRM_Dictionary.xml"));//保存.xml文件 
    }

    public void DeleteNode(string node_id)
    {
        XmlNode root = myDoc.SelectSingleNode("CRM_Dictionary");
        while (myDoc.GetElementsByTagName(node_id).Count > 0)
        {
            root.RemoveChild(myDoc.GetElementsByTagName(node_id)[0]);
        }
        myDoc.Save(Server.MapPath(xmlPath)); 
    }

    public void WriteNode(string node_id)
    {
        string default_ = "False";
        string enable = "False";
        string content = "";
        for (int i = 0; i < repeater1.Items.Count; i++)
        {
            RadioButton rb = repeater1.Items[i].FindControl("RadioButton1") as RadioButton;
            if (rb != null)
            {
                default_ = rb.Checked.ToString();
            }

            int raa = DataConverter.CLng(Request.Form["raa"]);
            if (raa == i)
            {
                default_ = "True";
            }
            else
            {
                default_ = "False";
            }


            CheckBox cb = repeater1.Items[i].FindControl("CheckBox1") as CheckBox;
            if (cb != null)
            {
                enable = cb.Checked.ToString();
            }

            TextBox tb = repeater1.Items[i].FindControl("TextBox1") as TextBox;
            if (tb != null)
            {
                content = tb.Text;
            }
            if (content != "")
                WriteXML(node_id, i, default_, enable, content);
        }
        int num = repeater1.Items.Count;
        string e = this.tex.Text;
        if (e.EndsWith("~~"))
        {
            e = e.Substring(0, e.Length - 1);
        }
        if (!string.IsNullOrEmpty(e))
        {
            string[] sd = e.Split(new string[] { "~~" }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < sd.Length; j++)
            {
                string[] kk = sd[j].Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries);
                default_ = kk[0];
                enable = kk[1];
                content = kk[2];
                WriteXML(node_id, num, default_, enable, content);
                num++;
            }
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        string node_name = Request.QueryString["FieldName"].ToString();
        ////DeleteNode(node_name);
        ////WriteNode(node_name);
        for (int i = 0; i < repeater1.Items.Count; i++)
        {
            string sort = (repeater1.Items[i].FindControl("nodeSort") as Label).Text.Trim();
            XmlNode node= myDoc.SelectSingleNode("/CRM_Dictionary/" + node_name + "[sort="+sort+"]");
            node.SelectSingleNode("content").InnerText=(repeater1.Items[i].FindControl("TextBox1") as TextBox).Text.Trim();
            node.SelectSingleNode("default_").InnerText =DataConverter.CLng(Request.Form["raa"]) == i ? "True" : "False";
            node.SelectSingleNode("enable").InnerText =(repeater1.Items[i].FindControl("enableCK") as CheckBox).Checked.ToString();
        }
        myDoc.Save(Server.MapPath(xmlPath));
        function.WriteSuccessMsg("保存成功!");
    }

    protected void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            XmlNode root = myDoc.SelectSingleNode("CRM_Dictionary");
            string node_id = Request.QueryString["FieldName"].ToString();
            string sort = e.CommandArgument.ToString();
            XmlNodeList list = myDoc.SelectNodes("/CRM_Dictionary/" + node_id);
            foreach (XmlNode xn in list)
            {
                if (sort == xn.SelectSingleNode("sort").InnerText)
                {
                    root.RemoveChild(xn);
                    break;
                }
            }
            myDoc.Save(Server.MapPath("/Config/CRM_Dictionary.xml"));//保存.xml文件
            function.WriteSuccessMsg("删除成功!", "CRMDictionary.aspx?FieldName=" + ViewState["node_id"]);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string node_id = Request.QueryString["FieldName"].ToString();
        if (node_id != "")
        {
            int sort = 0;
            DataSet table = new DataSet();
            table.ReadXml(Server.MapPath("/Config/CRM_Dictionary.xml"));
            DataTable ctable = table.Tables[node_id];
            if (ctable != null && ctable.Rows.Count > 0)
                sort = ctable.Rows.Count;
            WriteXML(node_id, sort, Request.Form["ra"].ToString(), this.txt_enable.Checked ? "True" : "False", this.txt_content.Text);
            function.WriteSuccessMsg("保存成功!", "CRMDictionary.aspx?FieldName=" + ViewState["node_id"]);
        }
        
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        this.Panel1.Visible = false;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        this.Panel1.Visible = true;
    }

    private string UC_BI()
    {
        XmlDocument xmlDoc2 = new XmlDocument();
        xmlDoc2.Load(Server.MapPath("/Config/AppSettings.config"));
        XmlNodeList amde = xmlDoc2.SelectNodes("appSettings/add");
        int val = 0;
        foreach (XmlNode xn in amde)
        {
            if (xn.Attributes["key"].Value.ToString() == "OAconfig")
                val = DataConverter.CLng(xn.Attributes["value"].Value);
        }
        //0、企业办公，1、个人办公，2、政府办公，3、教育办公，4、门户办公
        if (val == 0)
        {
            return "企业办公";
        }
        if (val == 1)
        {
            return "个人办公";
        }
        if (val == 2)
        {
            return "政府办公";
        }
        if (val == 3)
        {
            return "教育办公";
        }
        if (val == 4)
        {
            return "门户办公";
        }
        else
        {
            return "BI应用";
        }
    }

}
