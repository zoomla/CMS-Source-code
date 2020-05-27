using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class manage_Shop_profileConfige_HonorConfige : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li>后台管理</li><li>商城管理</li><li>返利信息配置</li><li>兑现信息配置</li>");
        if (!IsPostBack)
        {
            ReadXml();
        }
    }

    /// <summary>
    /// 读取xml内容
    /// </summary>
    /// <returns></returns>
    private void ReadXml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("/Config/ProjectConfige.xml"));
        XmlNodeList list = doc.SelectNodes("confige");

        foreach (XmlNode node in list)
        {
            XmlNodeList honor = node.SelectNodes("data");
            for (int i = 0; i < honor.Count; i++)
            {
                if (honor[i].Attributes["type"].Value == "honor")
                {
                    txtsite.Text = honor[i].SelectSingleNode("sitecash").InnerText;
                    txtAcou.Text = honor[i].SelectSingleNode("cash").InnerText;
                    txtEmail.Text = honor[i].SelectSingleNode("meil").InnerText;
                    info.Value = honor[i].SelectSingleNode("important").InnerText;
                    txtMobile.Text = honor[i].SelectSingleNode("mobile").InnerText;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 添加xml节点
    /// </summary>
    /// <param name="audit"></param>
    private void AddXmlNode()
    {
        XmlDocument xmlDoc = new XmlDocument();
        string path = "../../../Config/ProjectConfige.xml";
        xmlDoc.Load(Server.MapPath(path));
        XmlNode root = xmlDoc.SelectSingleNode("confige");//查找<content>
        XmlElement xe1 = xmlDoc.CreateElement("data");//创建一个<cont>节点 
        xe1.SetAttribute("type", "honor");//设置该节点type属性 

        XmlElement conf = xmlDoc.CreateElement("important");
        conf.InnerText = info.Value;
        xe1.AppendChild(conf);  //将important节点添加到<data>节点中

        XmlElement cash = xmlDoc.CreateElement("cash");
        cash.InnerText = txtAcou.Text;
        xe1.AppendChild(cash);  //将cash节点添加到<data>节点中

        XmlElement sitecash = xmlDoc.CreateElement("sitecash");
        cash.InnerText = txtsite.Text;
        xe1.AppendChild(sitecash);  //将sitecash节点添加到<data>节点中

        XmlElement meil = xmlDoc.CreateElement("meil");
        meil.InnerText = txtEmail.Text;
        xe1.AppendChild(meil);  //将meil节点添加到<data>节点中

        XmlElement mobile = xmlDoc.CreateElement("mobile");
        mobile.InnerText = txtMobile.Text;
        xe1.AppendChild(mobile);  //将mobile节点添加到<data>节点中

        root.AppendChild(xe1);//将data节点添加到<confige>节点中 
        xmlDoc.Save(Server.MapPath(path));
    }

    /// <summary>
    ///修改xml节点属性
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool UpdateNode()
    {
        bool ret = false;
        XmlDocument doc = new XmlDocument();
        string path = "../../../Config/ProjectConfige.xml";
        doc.Load(Server.MapPath(path));
        XmlNodeList list = doc.SelectNodes("confige/data");
        foreach (XmlNode node in list)
        {
            if (node.Attributes["type"].Value.Trim() == "honor")
            {
                XmlNode imppr = node.SelectSingleNode("important");
                imppr.InnerText = info.Value.Trim();

                XmlNode sitecash = node.SelectSingleNode("sitecash");
                sitecash.InnerText = txtsite.Text.Trim();

                XmlNode cash = node.SelectSingleNode("cash");
                cash.InnerText = txtAcou.Text.Trim();

                XmlNode Email = node.SelectSingleNode("meil");
                Email.InnerText = txtEmail.Text.Trim();

                XmlNode mobile = node.SelectSingleNode("mobile");
                mobile.InnerText = txtMobile.Text.Trim();
                ret = true;
                break;
            }
            else
            {
                ret = false;
            }
        }
        doc.Save(Server.MapPath(path));
        return ret;
    }


    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        if (txtMobile.Text.Trim().Length>70)
        {
            function.WriteErrMsg("手机短信内容不能超过70个字!");
        }
        bool result = UpdateNode();  //修改
        if (!result)  //不存在
        {
            AddXmlNode();  //添加
            function.WriteSuccessMsg("添加成功!");
        }
        else
        {
            function.WriteSuccessMsg("修改成功!");
        }
    }
}
