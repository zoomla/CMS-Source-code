using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Shop.profileConfige
{
    public partial class RedEnvlop : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>商城管理</li><li>返利信息配置</li><li>红包信息配置</li>");
            if (!IsPostBack)
            {
                ReadXml();
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string path = "/Config/ProjectConfige.xml";
            doc.Load(Server.MapPath(path));
            XmlNodeList list = doc.SelectNodes("confige");

            foreach (XmlNode node in list)
            {
                XmlNodeList RedEnvs = node.SelectNodes("data");
                bool res = true;
                int index = 0;
                XmlElement conf = doc.CreateElement("profect");
                conf.SetAttribute("proVal", txtHonorMoney.Text.Trim());
                conf.SetAttribute("money", txtMoney.Text.Trim());
                conf.SetAttribute("isCon", DropDownList2.SelectedValue);

                for (int i = 0; i < RedEnvs.Count; i++)
                {
                    if (RedEnvs[i].Attributes["type"].Value == "redEnv")
                    {
                        XmlNodeList RedEnv = RedEnvs[i].SelectNodes("profect");
                        if (RedEnv != null && RedEnv.Count > 0)
                        {
                            for (int j = 0; j < RedEnv.Count; j++)
                            {
                                int id = DataConverter.CLng(RedEnv[j].Attributes["id"].Value);
                                if (index < id)
                                {
                                    index = id;
                                }
                            }
                        }
                        conf.SetAttribute("id", (index + 1).ToString());
                        RedEnvs[i].AppendChild(conf);  //将conf节点添加到<data>节点中
                        doc.Save(Server.MapPath(path));
                        res = false;
                        break;
                    }
                }
                if (res)
                {
                    conf.SetAttribute("id", (index + 1).ToString());
                    AddXmlNode(doc, conf, path); //不存在
                }
            }
            ReadXml();
        }

        #region private function

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
                XmlNodeList RedEnvs = node.SelectNodes("data");
                for (int i = 0; i < RedEnvs.Count; i++)
                {
                    if (RedEnvs[i].Attributes["type"].Value == "redEnv")
                    {
                        XmlNodeList RedEnv = RedEnvs[i].SelectNodes("profect");
                        if (RedEnv != null && RedEnv.Count > 0)
                        {
                            ids.Items.Clear();
                            ddid.Items.Clear();
                            txtHonorinfo.Value = "";
                            for (int j = 0; j < RedEnv.Count; j++)
                            {
                                int id = DataConverter.CLng(RedEnv[j].Attributes["id"].Value);
                                string proVal = RedEnv[j].Attributes["proVal"].Value;
                                string money = RedEnv[j].Attributes["money"].Value;
                                string cont = RedEnv[j].Attributes["isCon"].Value == "0" ? "含" : "不含";
                                txtHonorinfo.Value += (j + 1).ToString() + "、一次性兑现" + proVal + "元返利者(" + cont + proVal + "元以上)，额外奖励" + money + "元现金红包；<br/>";
                                ListItem li = new ListItem();
                                li.Text = "第" + (j + 1).ToString() + "条";
                                li.Value = RedEnv[j].Attributes["id"].Value;
                                ids.Items.Add(li);
                                ddid.Items.Add(li);
                            }
                        }
                        XmlNode profects = RedEnvs[i].SelectSingleNode("profects");
                        string proVals = "";
                        string moneys = "";
                        if (profects != null)
                        {
                            proVals = profects.Attributes["proVal"].Value;
                            moneys = profects.Attributes["money"].Value;
                            txtHonorinfo.Value += (RedEnv.Count + 1).ToString() + "、" + "一次性兑现为" + proVals + "元的N倍者，将奖励" + moneys + "元的N倍作为红包，不设上限";
                        }
                        info.Value = RedEnvs[i].SelectSingleNode("important").InnerText;
                        txtHonor.Value = RedEnvs[i].SelectSingleNode("regurl").InnerText;
                        ListItem lis = new ListItem();
                        lis.Text = "最高金额";
                        lis.Value = "-1";
                        ids.Items.Add(lis);
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// 添加xml节点
        /// </summary>
        /// <param name="audit"></param>
        private void AddXmlNode(XmlDocument doc, XmlElement confs, string path)
        {
            doc.Load(Server.MapPath(path));
            XmlNode root = doc.SelectSingleNode("confige");//查找<content>
            XmlElement xe1 = doc.CreateElement("data");//创建一个<cont>节点 
            xe1.SetAttribute("type", "redEnv");//设置该节点type属性 

            XmlElement conf = doc.CreateElement("important");
            conf.InnerText = info.Value;
            xe1.AppendChild(conf);  //将important节点添加到<data>节点中

            XmlElement cash = doc.CreateElement("regurl");
            cash.InnerText = txtHonor.Value.Trim();
            xe1.AppendChild(cash);  //将cash节点添加到<data>节点中

            xe1.AppendChild(confs);  //将confs节点添加到<data>节点中
            root.AppendChild(xe1);//将data节点添加到<confige>节点中 
            doc.Save(Server.MapPath(path));
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
            string path = "/Config/ProjectConfige.xml";
            doc.Load(Server.MapPath(path));
            XmlNodeList list = doc.SelectNodes("confige/data");
            foreach (XmlNode node in list)
            {
                if (node.Attributes["type"].Value.Trim() == "redEnv")
                {
                    XmlNode imppr = node.SelectSingleNode("important");
                    imppr.InnerText = info.Value.Trim();

                    XmlNode cash = node.SelectSingleNode("regurl");
                    cash.InnerText = txtHonor.Value.Trim();
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
            XmlDocument xmlDoc = new XmlDocument();
            string path = "/Config/ProjectConfige.xml";
            bool result = UpdateNode();  //修改
            if (!result)  //不存在
            {
                AddXmlNode(xmlDoc, null, path);  //添加
                function.WriteSuccessMsg("添加成功!");
            }
            else
            {
                function.WriteSuccessMsg("修改成功!");
            }
        }

        #endregion

        //修改
        protected void btnUpdata_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string path = "/Config/ProjectConfige.xml";
            doc.Load(Server.MapPath(path));
            XmlNodeList list = doc.SelectNodes("confige");

            foreach (XmlNode node in list)
            {
                XmlNodeList RedEnvs = node.SelectNodes("data");
                for (int i = 0; i < RedEnvs.Count; i++)
                {
                    if (RedEnvs[i].Attributes["type"].Value == "redEnv")
                    {
                        if (DataConverter.CLng(ids.SelectedValue) == -1)
                        {
                            XmlNode pros = RedEnvs[i].SelectSingleNode("profects");
                            pros.Attributes["proVal"].Value = TextBox1.Text.Trim();
                            pros.Attributes["money"].Value = txtBonus.Text.Trim();
                        }
                        else
                        {
                            XmlNodeList RedEnv = RedEnvs[i].SelectNodes("profect");
                            if (RedEnv != null && RedEnv.Count > 0)
                            {
                                for (int j = 0; j < RedEnv.Count; j++)
                                {
                                    int id = DataConverter.CLng(RedEnv[j].Attributes["id"].Value);
                                    if (id == DataConverter.CLng(ids.SelectedValue))
                                    {
                                        RedEnv[j].Attributes["proVal"].Value = TextBox1.Text.Trim();
                                        RedEnv[j].Attributes["money"].Value = txtBonus.Text.Trim();
                                        RedEnv[j].Attributes["isCon"].Value = ddCont.SelectedValue;
                                        break;
                                    }
                                }
                            }
                        }
                        RedEnvs[i].SelectSingleNode("regurl").InnerText = txtHonor.Value;
                        RedEnvs[i].SelectSingleNode("important").InnerText = info.Value;
                    }
                    doc.Save(Server.MapPath(path)); //将conf节点添加到<data>节点中
                }
            }
            ReadXml();
        }

        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string path = "/Config/ProjectConfige.xml";
            doc.Load(Server.MapPath(path));
            XmlNodeList list = doc.SelectNodes("confige");

            foreach (XmlNode node in list)
            {
                XmlNodeList RedEnvs = node.SelectNodes("data");
                for (int i = 0; i < RedEnvs.Count; i++)
                {
                    if (RedEnvs[i].Attributes["type"].Value == "redEnv")
                    {
                        XmlNodeList RedEnv = RedEnvs[i].SelectNodes("profect");
                        if (RedEnv != null && RedEnv.Count > 0)
                        {
                            for (int j = 0; j < RedEnv.Count; j++)
                            {
                                int id = DataConverter.CLng(RedEnv[j].Attributes["id"].Value);
                                if (id == DataConverter.CLng(ddid.SelectedValue))
                                {
                                    RedEnvs[i].RemoveChild(RedEnv[j]);
                                    break;
                                }
                            }
                        }
                    }
                    doc.Save(Server.MapPath(path)); //将conf节点添加到<data>节点中
                }
            }
            ReadXml();
        }
    }
}