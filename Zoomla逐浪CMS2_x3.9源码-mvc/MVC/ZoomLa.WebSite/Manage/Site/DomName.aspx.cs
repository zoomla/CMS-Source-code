using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Site
{
    public partial class DomName : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();

        public string addProduct = "AddProduct.aspx?ModelID=6&NodeID=198";
        protected void Page_Load(object sender, EventArgs e)
        {
            badmin.CheckIsLogin();
            if (!IsPostBack)
            {
                addUrl.Value = "AddProduct.aspx?ModelID=" + StationGroup.ModelID + "&NodeID=" + StationGroup.NodeID;
            }
            Call.HideBread(Master);
        }
        #region
        ////检测是否有效
        //private void DisChkResult(Hashtable ht) 
        //{
        //    int num = DataConvert.CLng(ht["num"].ToString());
        //    List<string> list = new List<string>();
        //    for (int i = 1; i <= num; i++)
        //    {
        //        remind.Text += ht["name" + i].ToString();
        //        if (ht["chk" + i].Equals("100"))
        //        {
        //            remind.Text = "<span style='color:green;'>" + remind.Text + "可以正常注册";
        //        }
        //        else if (ht["err"]!=null)//返回错误
        //        {
        //            remind.Text = "<span style='color:red;'>";
        //            switch(ht["err"].ToString())
        //            {
        //                case "client-null":
        //                    remind.Text += ht["err"]+ ":未配置新网帐号!";
        //                    break;
        //                case "auth-failure":
        //                    remind.Text += ht["err"] + ":验证失败,请检查新网帐号与API密码是否正确!";
        //                    break;
        //                default:
        //                    remind.Text +="错误:"+ ht["err"];
        //                    break;
        //            }
        //        }
        //        else//已被注册
        //        {
        //            remind.Text = "<span style='color:red;'>" + remind.Text + "无法注册";
        //        }

        //        remind.Text += "</span><br />";
        //    }
        //}
        //注册
        //protected void registerBtn_Click(object sender, EventArgs e)
        //{
        //    //GetProductKey
        //    //url       URL转发[通用网址需要,如果不填默认为www.xinnet.com]这个起何作用
        //    if (string.IsNullOrEmpty("")) 
        //    {
        //        function.WriteErrMsg("请先输入网址");
        //    }
        //    remind.Text = "";
        //    string[] suffix = Request.Form["domNameChk"].Split(',');

        //    for (int i = 0; i < suffix.Length; i++)
        //    {
        //        string url = "" + suffix[i];
        //        string uname1 = Request.Form["uname1"];
        //        string uname2 = Request.Form["uname2"];
        //        string ucity1 = Request.Form["cityText"];
        //        string aemail = Request.Form["aemail"];
        //        string checksum = DomNameHelper.MD5("Register" + clientID + apiPasswd + url + aemail + uname2, 32);//以32位
        //        List<QueryParam> param = new List<QueryParam>();
        //        param.Add(new QueryParam("dn", url));//域名
        //        param.Add(new QueryParam("enc", "E"));
        //        param.Add(new QueryParam("client", clientID));
        //        //param.Add(new QueryParam("period", "1"));//1-10年,不填默认1年
        //        //param.Add(new QueryParam("checksum", checksum));//***MD5加密摘要,,

        //        //英文必须有空格
        //        //----注册信信息
        //        param.Add(new QueryParam("uname1", uname1));//注册人|单位名称 中|英名称    [国内域名必填]|[国际域名必须]
        //        param.Add(new QueryParam("uname2", uname2));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
        //        param.Add(new QueryParam("rname1", Request.Form["rname1"]));//注册人|单位负责人 中|英名称    [国内域名必填]|[国际域名必须]
        //        param.Add(new QueryParam("rname2", Request.Form["rname2"]));
        //        param.Add(new QueryParam("aname1", Request.Form["rname1"]));//管理联系人 中|英名称   [国内域名必填]|[国际域名必须],与上方用同一信息
        //        param.Add(new QueryParam("aname2", Request.Form["rname2"]));
        //        param.Add(new QueryParam("aemail", aemail));//管理联系人电子邮件地址                [必须]
        //        param.Add(new QueryParam("ucity1", ucity1));//注册人城市名称 中|英名称    [国内域名必填]|[国际域名必须]
        //        param.Add(new QueryParam("ucity2", "Cheng shi"));
        //        param.Add(new QueryParam("uaddr1", Request.Form["uaddr1"]));//通讯地址,中|英 [国内域名必填]|[国际域名必须]
        //        param.Add(new QueryParam("uaddr2", Request.Form["uaddr2"]));
        //        param.Add(new QueryParam("uzip", Request.Form["uzip"]));//注册人邮政编码                    [必须]
        //        param.Add(new QueryParam("uteln", Request.Form["uteln"]));//注册人电话号码
        //        param.Add(new QueryParam("ateln", Request.Form["ateln"]));
        //        param.Add(new QueryParam("ufaxa", Request.Form["ufaxa"]));   //传真区号
        //        param.Add(new QueryParam("ufaxn", Request.Form["ufaxn"]));//不能超过8位,与API的不能超过12位不同

        //        //DomNameHelper _XinNet = new DomNameHelper(ApiType.Register, param);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(),"","alert('"+_XinNet.Result+"');",true);
        //    }
        //}
        #endregion
    }
}