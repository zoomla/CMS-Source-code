using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ZoomLa.Components
{
     [Serializable, XmlRoot("PlatAPI")]
    public class PlatModel
    {
         public string SinaKey { get; set; }
         public string SinaSecret { get; set; }
         public string SinaCallBack { get; set; }
         public string QQKey { get; set; }
         public string QQCallBack { get; set; }
         //-------------------微信APP支付需要独立的商户号与APPID等信息
         public string WXPay_APPID { get; set; }
         public string WXPay_APPSecret { get; set; }
         public string WXPay_MCHID { get; set; }
         public string WXPay_Key { get; set; }
    }
}
