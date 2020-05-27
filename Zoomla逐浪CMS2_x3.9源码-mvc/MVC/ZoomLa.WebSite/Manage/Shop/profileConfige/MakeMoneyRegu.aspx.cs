using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Shop.profileConfige
{
    public partial class MakeMoneyRegu : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>商城管理</li><li>返利信息配置</li><li>赚钱计划信息配置</li>");
            if (!IsPostBack)
            {
                ReadXml();
            }
        }



        #region private function

        #region add
        private void Add()
        {
            XmlDocument doc = new XmlDocument();
            string path = "../../../Config/ProjectConfige.xml";
            doc.Load(Server.MapPath(path));
            XmlNodeList list = doc.SelectNodes("confige");

            foreach (XmlNode node in list)
            {
                XmlNodeList RedEnvs = node.SelectNodes("data");
                bool res = true;
                int index = 0;
                XmlElement conf = doc.CreateElement("profect");
                conf.SetAttribute("MinNum", txtMinNum.Text.Trim());
                conf.SetAttribute("MaxNum", txtMaxNum.Text.Trim());
                conf.SetAttribute("money", txtAward.Text.Trim());

                for (int i = 0; i < RedEnvs.Count; i++)
                {
                    if (RedEnvs[i].Attributes["type"].Value == "MakeMoney")
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
        #endregion


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
                    if (RedEnvs[i].Attributes["type"].Value == "MakeMoney")
                    {
                        XmlNodeList RedEnv = RedEnvs[i].SelectNodes("profect");
                        if (RedEnv != null && RedEnv.Count > 0)
                        {
                            ddIds.Items.Clear();
                            txtHonorinfo.Value = "";
                            for (int j = 0; j < RedEnv.Count; j++)
                            {
                                int id = DataConverter.CLng(RedEnv[j].Attributes["id"].Value);
                                string MinNum = RedEnv[j].Attributes["MinNum"].Value;
                                string MaxNum = RedEnv[j].Attributes["MaxNum"].Value;
                                string money = RedEnv[j].Attributes["money"].Value;
                                if (MaxNum != "-1")
                                {
                                    txtHonorinfo.Value += (j + 1).ToString() + "、推荐" + MinNum + " - " + MaxNum + "名会员，朋友注册后，会员所得奖励" + money + "元/名<br/>";
                                }
                                else
                                {
                                    txtHonorinfo.Value += (j + 1).ToString() + "、推荐" + MinNum + "名会员以上，朋友注册后，会员所得奖励" + money + "元/名<br/>";
                                }
                                ListItem li = new ListItem();
                                li.Text = "第" + (j + 1).ToString() + "条";
                                li.Value = RedEnv[j].Attributes["id"].Value;
                                ddIds.Items.Add(li);
                            }
                        }
                        XmlNode profects = RedEnvs[i].SelectSingleNode("profects");
                        string moneys = "";
                        if (profects != null)
                        {
                            moneys = profects.Attributes["money"].Value;
                            txtShopMoney.Text = moneys;
                            txtHonorinfo.Value += (RedEnv.Count + 1).ToString() + "、" + "推荐会员购物后,会员得奖励:" + moneys + "元/名";
                        }
                        info.Value = RedEnvs[i].SelectSingleNode("important").InnerText;
                        ListItem lis = new ListItem();
                        lis.Text = "最高金额";
                        lis.Value = "-1";
                        ddIds.Items.Add(lis);
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
            xe1.SetAttribute("type", "MakeMoney");//设置该节点type属性 

            XmlElement conf = doc.CreateElement("important");
            conf.InnerText = info.Value;
            xe1.AppendChild(conf);  //将important节点添加到<data>节点中

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
            string path = "../../../Config/ProjectConfige.xml";
            doc.Load(Server.MapPath(path));
            XmlNodeList list = doc.SelectNodes("confige/data");
            foreach (XmlNode node in list)
            {
                if (node.Attributes["type"].Value.Trim() == "MakeMoney")
                {
                    XmlNode imppr = node.SelectSingleNode("important");
                    imppr.InnerText = info.Value.Trim();

                    XmlNode profects = node.SelectSingleNode("profects");
                    profects.Attributes["money"].Value = txtShopMoney.Text.Trim();
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
        private void Updata()
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
                    if (RedEnvs[i].Attributes["type"].Value == "MakeMoney")
                    {
                        XmlNodeList RedEnv = RedEnvs[i].SelectNodes("profect");
                        if (RedEnv != null && RedEnv.Count > 0)
                        {
                            for (int j = 0; j < RedEnv.Count; j++)
                            {
                                int id = DataConverter.CLng(RedEnv[j].Attributes["id"].Value);
                                if (id == DataConverter.CLng(ddIds.SelectedValue))
                                {
                                    RedEnv[j].Attributes["MinNum"].Value = txtMinNum.Text.Trim();
                                    if (id == -1)
                                    {
                                        RedEnv[j].Attributes["MaxNum"].Value = "-1";
                                    }
                                    else
                                    {
                                        RedEnv[j].Attributes["MaxNum"].Value = txtMaxNum.Text.Trim();
                                    }
                                    RedEnv[j].Attributes["money"].Value = txtAward.Text.Trim();
                                    break;
                                }
                            }
                        }
                        RedEnvs[i].SelectSingleNode("important").InnerText = info.Value;
                    }
                    doc.Save(Server.MapPath(path)); //将conf节点添加到<data>节点中
                }
            }
            ReadXml();
        }

        //删除
        private void Del()
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
                    if (RedEnvs[i].Attributes["type"].Value == "MakeMoney")
                    {
                        XmlNodeList RedEnv = RedEnvs[i].SelectNodes("profect");
                        if (RedEnv != null && RedEnv.Count > 0)
                        {
                            for (int j = 0; j < RedEnv.Count; j++)
                            {
                                int id = DataConverter.CLng(RedEnv[j].Attributes["id"].Value);
                                if (id == DataConverter.CLng(ddIds.SelectedValue))
                                {
                                    RedEnvs[i].RemoveChild(RedEnv[j]);
                                    break;
                                }
                            }
                        }
                    }
                    doc.Save(Server.MapPath(path));
                }
            }
            ReadXml();
        }


        protected void btn_Click(object sender, EventArgs e)
        {
            string menu = hfmenu.Value;
            if (menu == "add")
            {
                Add();
            }
            if (menu == "updata")
            {
                Updata();
            }
            if (menu == "del")
            {
                Del();
            }
        }
    }
}