using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    /*
     * 存放通用的枚举状态
     */ 
    public class ZLEnum
    {
        /// <summary>
        /// 文章|贴吧|OA,草稿,-3,-2:Recycle:删除,-1:Reject:退稿,0:待审核,1:己查看,98:等待确认,99:Audited:终审通过,105:归档(仅能被指定用户查看)其它为自定义
        /// </summary>
        public enum ConStatus
        {
            Draft = -3, Recycle = -2, Reject = -1, UnAudit = 0, Sured = 1, NotSure = 98, Audited = 99, Filed = 105
        }
        public static string GetConStatus(int status)
        {
            switch (status)
            {
                case (int)ConStatus.Draft:
                    return "草稿";
                case (int)ConStatus.Recycle:
                    return "回收站";
                case (int)ConStatus.Reject:
                    return "退稿";
                case (int)ConStatus.UnAudit:
                    return "未审核";
                case (int)ConStatus.NotSure:
                    return "等待确认";
                case (int)ConStatus.Audited:
                    return "已审核";
                case (int)ConStatus.Filed:
                    return "已归档";
                default:
                    return "未知[" + status + "]";
            }
        }
        /// <summary>
        /// 内容,上传,异常,标签异常,管理员登录,退出,普通日志
        /// </summary>
        public enum Log { content = 0, fileup = 1, exception = 2, labelex = 3, alogin = 4, safe = 5, pay = 6, general = 7 }
        // // string[] ownerlist = "model|模型节点,content|内容管理,crm|客户管理,label|模板标签,shop|商城管理,store|店铺管理,page|黄页管理,user|用户中心,other|其它配置".Split(',');
        public enum Auth { model, content, crm, label, shop, store, page, user, bar,pub ,other }
        /// <summary>
        /// Withdraw 提现
        /// </summary>
        public enum WDState { WaitDeal = 0, Dealed = 99, Rejected = -1 };
        /// <summary>
        /// 用于获取数据库中数据
        /// </summary>
        public enum DBStatus { All = -1, Enable = 1, Disabled = 0 };
        /// <summary>
        /// 通用状态
        /// </summary>
        public enum Common { Stop = -1, Normal = 0, Recommend = 1, Finished = 99 };
    }
}
