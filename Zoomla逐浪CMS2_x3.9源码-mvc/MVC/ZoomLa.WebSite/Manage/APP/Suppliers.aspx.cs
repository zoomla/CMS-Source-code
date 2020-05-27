using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.BLL.Third;
using ZoomLa.Model.Third;


namespace ZoomLaCMS.Manage.APP
{
    public partial class Suppliers : CustomerPageAction
    {
        B_User buser = new B_User();
        B_Third_Info thirdBll = new B_Third_Info();
        //QQ APPID=Key,APPKey=Secret
        //Key:一串数字,Secret:一串字母
        //所有的存值都为 key和secret
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.IsSuperManage();
            if (!IsPostBack)
            {
                XmlDocument Xml = thirdBll.GetXmlDoc();
                //-------新浪
                {

                    M_Third_Info model = thirdBll.SelModelByName("Sina");
                    if (model != null)
                    {
                        ASina.Value = PlatConfig.SinaKey;
                        SSina.Value = PlatConfig.SinaSecret;
                        SSinaURL.Value = PlatConfig.SinaCallBack;
                        Sina_Enable_Chk.Checked = model.Enabled;
                    }
                }
                //-------QQ
                {
                    M_Third_Info model = thirdBll.SelModelByName("QQ");
                    if (model != null)
                    {
                        QQ_Login_APPID_T.Value = model.Key;
                        QQ_Login_Key_T.Value = model.Secret;
                        QQ_Login_CallBack_T.Value = model.CallBackUrl;
                        QQ_Enable_Chk.Checked = model.Enabled;
                    }
                }
                //-------微信
                {
                    M_Third_Info model = thirdBll.SelModelByName("wechat");
                    if (model != null)
                    {
                        WeChat_APPID_T.Text = model.Key;
                        WeChat_Secret_T.Text = model.Secret;
                        WeChat_URL_T.Text = model.CallBackUrl;
                        Wechat_Enable_Chk.Checked = model.Enabled;
                    }
                }
                //-------百度
                {
                    M_Third_Info model = thirdBll.SelModelByName("Baidu");
                    if (model != null)
                    {
                        ABaidu.Value = model.Key;
                        SBaidu.Value = model.Secret;
                        UBaidu.Value = model.CallBackUrl;
                        Baidu_Enable_Chk.Checked = model.Enabled;
                    }
                }
                //-------开心
                //{
                //    M_Third_Info model = thirdBll.SelModelByName("Kaixin");
                //    if (model != null)
                //    {
                //        AKaixin.Value = model.Key;
                //        SKaixin.Value = model.Secret;
                //        SKaixiuUrl.Value = model.CallBackUrl;
                //    }
                //}
                //-------搜狐畅言
                //XApp = Xml.SelectSingleNode("SuppliersList/SohuChat");
                //chat_AppIDT.Value = XApp.Attributes["Key"].Value;
                //chat_AppKeyT.Value = XApp.Attributes["Secret"].Value;
                //CkSohuChat.Checked = XApp.Attributes["Enable"].Value == "1" ? true : false;
                //-------百度翻译
                {
                    M_Third_Info model = thirdBll.SelModelByName("BaiduTrans");
                    if (model != null)
                    {
                        Baidu_Translate_Key_T.Text = model.Key;
                        Baidu_Translate_Secret_T.Text = model.Secret;
                    }
                }
                //-------飞印打印机
                {
                    M_Third_Info model = thirdBll.SelModelByName("printer");
                    if (model != null)
                    {
                        Printer_Key_T.Text = model.Key;//商户号
                        Printer_Secret_T.Text = model.Secret;//Secret
                    }
                }
                //-------飞印打印机
                //XApp = Xml.SelectSingleNode("SuppliersList/BaiduTrans");
                //BaiduKey_T.Text = XApp.Attributes["Key"].Value;
                //BaiduAppID_T.Text = XApp.Attributes["AppID"].Value;
                //Sina_Blog_Key_T.Text = PlatConfig.SinaKey;
                //Sina_Blog_Secret_T.Text = PlatConfig.SinaSecret;
                //Sina_Blog_CallBack_T.Text = PlatConfig.SinaCallBack;
                QQ_Blog_Key_T.Text = PlatConfig.QQKey;
                QQ_Blog_CallBack_T.Text = PlatConfig.QQCallBack;

                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>" + Resources.L.用户管理 + "</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>" + Resources.L.会员管理 + "</a></li><li>" + Resources.L.APP社会化登录 + "</li>" + Call.GetHelp(39));
            }
        }
        protected void sohuChatBtn_Click(object sender, EventArgs e)
        {
            //XmlNode XApp = null;
            //XmlDocument Xml = new XmlDocument();
            //Xml.Load(Server.MapPath("/config/Suppliers.xml"));
            //XApp = Xml.SelectSingleNode("SuppliersList/SohuChat");
            //XApp.Attributes["Key"].Value = chat_AppIDT.Value.Trim();
            //XApp.Attributes["Secret"].Value = chat_AppKeyT.Value.Trim();
            //Xml.Save(Server.MapPath("/config/Suppliers.xml"));
        }
        protected void Blog_Save_Btn_Click(object sender, EventArgs e)
        {
            //PlatConfig.SinaKey = Sina_Blog_Key_T.Text;
            //PlatConfig.SinaSecret = Sina_Blog_Secret_T.Text;
            //PlatConfig.SinaCallBack = Sina_Blog_CallBack_T.Text;
            PlatConfig.QQKey = QQ_Blog_Key_T.Text;
            PlatConfig.QQCallBack = QQ_Blog_CallBack_T.Text;
            PlatConfig.Update();
            function.WriteErrMsg("博客信息配置完成");
        }
        protected void Login_Save_Btn_Click(object sender, EventArgs e)
        {

            XmlNode XApp = null;
            XmlDocument Xml = thirdBll.GetXmlDoc();
            //----------------------------------------------------

            XApp = Xml.SelectSingleNode("SuppliersList/Baidu");
            XApp.Attributes["Key"].Value = ABaidu.Value;
            XApp.Attributes["Secret"].Value = SBaidu.Value;
            XApp.Attributes["CallBackUrl"].Value = UBaidu.Value;
            XApp.Attributes["Enable"].Value = Baidu_Enable_Chk.Checked ? "1" : "0";

            XApp = Xml.SelectSingleNode("SuppliersList/BaiduTrans");
            XApp.Attributes["Key"].Value = Baidu_Translate_Key_T.Text;
            XApp.Attributes["AppID"].Value = Baidu_Translate_Secret_T.Text;

            //XApp = Xml.SelectSingleNode("SuppliersList/Kaixin");
            //XApp.Attributes["Key"].Value = AKaixin.Value;
            //XApp.Attributes["Secret"].Value = SKaixin.Value;
            //XApp.Attributes["CallBackUrl"].Value = SKaixiuUrl.Value;

            XApp = Xml.SelectSingleNode("SuppliersList/QQ");
            XApp.Attributes["ID"].Value = QQ_Login_APPID_T.Value;
            XApp.Attributes["Key"].Value = QQ_Login_APPID_T.Value;
            XApp.Attributes["Secret"].Value = QQ_Login_Key_T.Value;
            XApp.Attributes["CallBackUrl"].Value = QQ_Login_CallBack_T.Value;
            XApp.Attributes["Enable"].Value = QQ_Enable_Chk.Checked ? "1" : "0";

            XApp = Xml.SelectSingleNode("SuppliersList/Sina");
            XApp.Attributes["Key"].Value = "";
            XApp.Attributes["Secret"].Value = "";
            XApp.Attributes["CallBackUrl"].Value = "";
            PlatConfig.SinaKey = ASina.Value;
            PlatConfig.SinaSecret = SSina.Value;
            PlatConfig.SinaCallBack = SSinaURL.Value;
            XApp.Attributes["Enable"].Value = Sina_Enable_Chk.Checked ? "1" : "0";

            XApp = Xml.SelectSingleNode("SuppliersList/wechat");
            XApp.Attributes["ID"].Value = WeChat_APPID_T.Text;
            XApp.Attributes["Key"].Value = WeChat_APPID_T.Text;
            XApp.Attributes["Secret"].Value = WeChat_Secret_T.Text;
            XApp.Attributes["CallBackUrl"].Value = WeChat_URL_T.Text;
            XApp.Attributes["Enable"].Value = Wechat_Enable_Chk.Checked ? "1" : "0";
            PlatConfig.Update();
            Xml.Save(thirdBll.xmlPath);
            function.WriteSuccessMsg("保存成功");
        }
        protected void Other_Save_Btn_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = thirdBll.GetXmlDoc();
            thirdBll.UpdateToXml(xmlDoc, "BaiduTrans", Baidu_Translate_Key_T.Text, Baidu_Translate_Secret_T.Text);
            thirdBll.UpdateToXml(xmlDoc, "printer", Printer_Key_T.Text, Printer_Secret_T.Text);
            xmlDoc.Save(thirdBll.xmlPath);
            function.WriteSuccessMsg("保存成功");
        }
    }
}