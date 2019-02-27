using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;


public partial class manage_AddCRM_AddOption : CustomerPageAction
{
    /*
     * 这里content代表tagname
     * 
     */ 
    private XmlDocument myDoc=new XmlDocument();
    private string xmlPath = "/Config/CRM_Dictionary.xml";
    private M_CrmAuth crmModel = new M_CrmAuth();
    private DataTable authDT = new DataTable();//用来存权限信息
    private B_CrmAuth crmBll = new B_CrmAuth();
    private B_Admin badmin = new B_Admin();
    public string TagName { get { return string.IsNullOrEmpty(Request.QueryString["tagname"]) ? "" : Request.QueryString["tagname"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //---------权限限制(Excel导入权与可查看所有用户权)分页中限制显示哪些
            M_AdminInfo info = B_Admin.GetAdminByID(badmin.GetAdminLogin().AdminId);//info中有role信息
            authDT = crmBll.GetAuthTable(info.RoleList.Split(','));//
            if (!crmBll.IsHasAuth(authDT, "AllowOption", info))//如果ID不是自己的ID或ID为空则跳转到自己的ID上
            {
                function.WriteErrMsg("无权访问该页面!");
            }
            AdminName_T.Text = badmin.GetAdminLogin().AdminName;
            if (!string.IsNullOrEmpty(TagName))
            { MyBind(); }
            Call.SetBreadCrumb(Master, "<li><a href='"+ CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='CustomerList.aspx?usertype=0'>CRM配置</a></li><li><a href='OptionManage.aspx'>选项管理</a></li><li>添加选项</li>");
        }
    }
    public void MyBind()
    {
        Save_Btn.Text = "修改";
        DataRow datadr = GetDataByTagName(TagName);
        txt_content.Text = datadr["displayName"].ToString();
        isDefault.Checked = DataConverter.CBool(datadr["default_"].ToString());
        txt_enable.Checked = DataConverter.CBool(datadr["enable"].ToString());
        BuildMethod.SelectedValue = datadr["buildMethod"].ToString();
        switch (BuildMethod.SelectedValue)
        {
            case "4":
            case "5":
                if (string.IsNullOrEmpty(datadr["option"].ToString())) { break; }
                string[] temparr = datadr["option"].ToString().Split(',');
                TxtWidth_T.Text = temparr[0];
                TxtHeight_T.Text = temparr[1];
                break;
            default:
                Option_T.Text = datadr["option"].ToString();
                break;

        }
        AdminName_T.Text = datadr["AdminName"].ToString();
    }
    public DataRow GetDataByTagName(string name)
    {
        string xmlPath = HttpContext.Current.Server.MapPath("~/Config/CRM_Dictionary.xml");
        DataSet ds = new DataSet();
        ds.ReadXml(xmlPath);
        for (int i = 1; i < ds.Tables.Count; i++)
        {
            ds.Tables[0].Merge(ds.Tables[i]);
        }
        if (ds.Tables[0].Columns["isAdd"] == null)
        {
            ds.Tables[0].Columns.Add("isAdd", typeof(string));
        }
        DataTable dt = ds.Tables[0];
        dt.DefaultView.RowFilter = "isAdd=1 AND tagName='" + name + "'";
        if (dt.DefaultView.ToTable().Rows.Count <= 0) { function.WriteErrMsg("该数据不存在!"); }
       return dt.DefaultView.ToTable().Rows[0];
    }
    public manage_AddCRM_AddOption()
    {
        myDoc.Load(Server.MapPath(xmlPath));
    }
    ///*
    // * isadd为1表示这是一个附加表的
    // * buildMethod:1:下拉,2单选,3多选
    // * 
    // */

    protected void Add2_Click(object sender, EventArgs e)
    {
        string adminname = string.IsNullOrEmpty(AdminName_T.Text) ? badmin.GetAdminLogin().AdminName : AdminName_T.Text;
        if (string.IsNullOrEmpty(TagName))
        {
            string node_id = txt_content.Text.Trim();
            if (!string.IsNullOrEmpty(node_id))
            {
                int sort = 0;
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath(xmlPath));
                DataTable ctable = ds.Tables[node_id];
                if (ctable != null && ctable.Rows.Count > 0)
                { sort = ctable.Rows.Count; }
                
                WriteAddXML(node_id, sort, isDefault.Checked.ToString(), this.txt_enable.Checked ? "True" : "False", this.txt_content.Text, "1", BuildMethod.SelectedValue, GetOption(), adminname);
            }
            
        }
        else
        {
            string nodestr = "/CRM_Dictionary/" + TagName + "/";
            myDoc.SelectSingleNode(nodestr + "default_").InnerText = isDefault.Checked.ToString();
            myDoc.SelectSingleNode(nodestr + "enable").InnerText = txt_enable.Checked.ToString();
            myDoc.SelectSingleNode(nodestr + "buildMethod").InnerText = BuildMethod.SelectedValue;
            myDoc.SelectSingleNode(nodestr + "option").InnerText = GetOption();
            myDoc.SelectSingleNode(nodestr + "content").InnerText = txt_content.Text;
            myDoc.SelectSingleNode(nodestr + "AdminName").InnerText = adminname;
            myDoc.Save(Server.MapPath(xmlPath));
        }
        function.WriteSuccessMsg("保存成功!","OptionManage.aspx");

    }
    public string GetOption()
    {
        string option = "";
        switch (BuildMethod.SelectedValue)
        {
            case "4":
            case "5":
                option = TxtWidth_T.Text + "," + TxtHeight_T.Text;
                break;
            default:
                option = Option_T.Text;
                break;

        }
        return option;
    }
    //添加一个新节点,只需要第一个标识即可了
    public void WriteAddXML(string node_text, int sort, string default_, string enable, string content, string isAdd, string buildMethod,string option="",string adminname="")
    {
        //添加元素--UserCode
        XmlElement ele = myDoc.CreateElement("sort");
        XmlText text = myDoc.CreateTextNode(sort.ToString());

        //添加元素
        XmlElement ele1 = myDoc.CreateElement("default_");
        XmlText text1 = myDoc.CreateTextNode(default_);

        //添加元素
        XmlElement ele2 = myDoc.CreateElement("enable");
        XmlText text2 = myDoc.CreateTextNode(enable);

        XmlElement ele3 = myDoc.CreateElement("content");
        XmlText text3 = myDoc.CreateTextNode(content);
        XmlElement ele4 = myDoc.CreateElement("isAdd");
        XmlText text4 = myDoc.CreateTextNode(isAdd);

        XmlElement ele5 = myDoc.CreateElement("buildMethod");
        XmlText text5 = myDoc.CreateTextNode(buildMethod);
        XmlElement ele6 = myDoc.CreateElement("tagName");//用于指定自己的标记名，不提供修改
        XmlText text6 = myDoc.CreateTextNode(content);
        XmlElement ele7 = myDoc.CreateElement("displayName");
        XmlText text7 = myDoc.CreateTextNode(content);
        XmlElement optionEle = myDoc.CreateElement("option");
        XmlText optionText = myDoc.CreateTextNode(option);
        XmlElement adminEle = myDoc.CreateElement("AdminName");
        XmlText adminText = myDoc.CreateTextNode(adminname);
        //添加节点 node_text要对应我们xml文件中的节点名字
        XmlNode newElem = myDoc.CreateNode("element", node_text, "");//第三个参是其内置属性,给所有的值
       

        //在节点中添加元素
        newElem.AppendChild(ele);
        newElem.LastChild.AppendChild(text);
        newElem.AppendChild(ele1);
        newElem.LastChild.AppendChild(text1);
        newElem.AppendChild(ele2);
        newElem.LastChild.AppendChild(text2);
        newElem.AppendChild(ele3);
        newElem.LastChild.AppendChild(text3);
        newElem.AppendChild(ele4);
        newElem.LastChild.AppendChild(text4);
        newElem.AppendChild(ele5);
        newElem.LastChild.AppendChild(text5);
        newElem.AppendChild(ele6);
        newElem.LastChild.AppendChild(text6);
        newElem.AppendChild(ele7);
        newElem.LastChild.AppendChild(text7);
        newElem.AppendChild(optionEle);
        newElem.LastChild.AppendChild(optionText);
        newElem.AppendChild(adminEle);
        newElem.LastChild.AppendChild(adminText);
        //将节点添加到文档中
        XmlElement root = myDoc.DocumentElement;
        root.AppendChild(newElem);
        myDoc.Save(Server.MapPath(xmlPath));//保存.xml文件 
    }
    protected void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            XmlNode root = myDoc.SelectSingleNode("CRM_Dictionary");
            string node_id = e.CommandArgument.ToString();
            string sort = e.CommandArgument.ToString();
            XmlNodeList list = myDoc.SelectNodes("/CRM_Dictionary/" + node_id);
            for (int i = 0; i < list.Count; i++)
            {
                root.RemoveChild(list[i]);
            }
            myDoc.Save(Server.MapPath("/Config/CRM_Dictionary.xml"));//保存.xml文件
            function.WriteSuccessMsg("删除成功!");
        }
    }

    protected void repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        //if (e.Item.ItemType ==ListItemType.Item )//alter等其他不改
        //{
        DropDownList dp1 = e.Item.FindControl("BuildMethod") as DropDownList;//生成方式
        dp1.SelectedValue = (e.Item.FindControl("BuildValue") as HiddenField).Value;
        dp1.DataBind();
        //}
    }
}