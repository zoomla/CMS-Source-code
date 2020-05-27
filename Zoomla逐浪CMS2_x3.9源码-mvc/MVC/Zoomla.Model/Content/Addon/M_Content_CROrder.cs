using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Content
{
    /// <summary>
    /// 对Json的模型化,数据由其他人购买了文章版权产生
    /// 实时获取服务端数据
    /// </summary>
    public class M_Content_CROrder
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 被授权方联系人
        /// </summary>
        public string LicenseeContact { get; set; }
        /// <summary>
        /// 被授权方电话
        /// </summary>
        public string LicenseeTel { get; set; }
        /// <summary>
        /// 被授权方名称
        /// </summary>
        public string LicenseeTitle { get; set; }
        /// <summary>
        /// 被授权方邮箱
        /// </summary>
        public string LicenseeEmail { get; set; }
        /// <summary>
        /// 授权方联系人
        /// </summary>
        public string LicensorContact { get; set; }
        /// <summary>
        /// 授权方电话
        /// </summary>
        public string LicensorTel { get; set; }
        /// <summary>
        /// 授权方名称
        /// </summary>
        public string LicensorTitle { get; set; }
        /// <summary>
        /// 授权方邮箱
        /// </summary>
        public string LicensorEmail { get; set; }
        /// <summary>
        /// 授权方式
        /// </summary>
        public string AuthTitle { get; set; }
        /// <summary>
        /// 授权价格
        /// </summary>
        public double AuthPrice { get; set; }
        /// <summary>
        /// 授权描述
        /// </summary>
        public string AuthComment { get; set; }
        /// <summary>
        /// 作品编号
        /// </summary>
        public string WorksID { get; set; }
        /// <summary>
        /// 作品标题
        /// </summary>
        public string WorksTitle { get; set; }

        public M_Content_CROrder GetModelFromJson(string json)
        {
            M_Content_CROrder orderMod = new M_Content_CROrder();
            //if (string.IsNullOrEmpty(json)) { return orderMod; }
            JObject obj = JsonConvert.DeserializeObject<JObject>(json);
            orderMod.Amount = Convert.ToDouble(obj["amount"]);
            orderMod.LicenseeContact = obj["licensee"]["contact"].ToString();
            orderMod.LicenseeTel = obj["licensee"]["tel"].ToString();
            orderMod.LicenseeTitle = obj["licensee"]["title"].ToString();
            orderMod.LicenseeEmail = obj["licensee"]["email"].ToString();
            orderMod.LicensorContact = obj["licensor"]["contact"].ToString();
            orderMod.LicensorTel = obj["licensor"]["tel"].ToString();
            orderMod.LicensorTitle = obj["licensor"]["title"].ToString();
            orderMod.LicensorEmail = obj["licensor"]["email"].ToString();
            orderMod.OrderID = obj["id"].ToString();
            orderMod.AuthPrice = Convert.ToDouble(obj["detail"][0]["worksLicense"]["price"]);
            orderMod.AuthComment = obj["detail"][0]["worksLicense"]["comment"].ToString();
            orderMod.AuthTitle = obj["detail"][0]["worksLicense"]["title"].ToString();
            orderMod.WorksID = obj["detail"][0]["worksId"].ToString();
            orderMod.WorksTitle = obj["detail"][0]["title"].ToString();
            orderMod.PayStatus = Convert.ToInt32(obj["payStatus"]);
            orderMod.Status = Convert.ToInt32(obj["status"]);
            orderMod.CreateDate = Convert.ToDateTime(obj["createDate"]);
            return orderMod;
        }
    }
}
