using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.push.mode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model.Mobile;

namespace ZoomLa.PdoApi.JPush
{
    public class C_JPush
    {
        //private String app_key = "4273022a6f4eaea232ecb878";
        //private String master_secret = "c1be920354532e3e292f5008";
        private String app_key, master_secret;
        private JPushClient client = null;
        private int DELAY_TIME = 1;
        //public static List<M_Mobile_PushAPI> APIList = new List<M_Mobile_PushAPI>();
        //public static M_Mobile_PushAPI GetAPI(int id)
        //{

        //}
        public C_JPush(M_Mobile_PushAPI apiMod)
        {
            app_key = apiMod.APPKey;
            master_secret = apiMod.APPSecret;
            client = new JPushClient(app_key, master_secret);
        }
        /// <summary>
        /// 发送推送,消息内容不能超过72个字
        /// </summary>
        public string SendPush(M_Mobile_PushMsg msgMod)
        {
            MessageResult result = new MessageResult();
            switch (msgMod.PushType)
            {
                case "sms":
                    result = SendSMS(msgMod);
                    break;
                case "alter":
                default:
                    result = SendAlter(msgMod);
                    break;
            }
            if (result.isResultOK()) { return "成功"; }
            else { return "失败,原因:" + result.ResponseResult.exceptionString; }
        }
        public MessageResult SendAlter(M_Mobile_PushMsg msgMod)
        {
            PushPayload payload = PushObject_All_All_Alert(msgMod.MsgContent);
            return client.SendPush(payload);
        }
        /// <summary>
        /// 以手机短信的形式发送通知,会产生相关费用
        /// </summary>
        public MessageResult SendSMS(M_Mobile_PushMsg msgMod)
        {
            PushPayload payload = PushSendSmsMessage(msgMod.MsgContent, msgMod.MsgContent);
            return client.SendPush(payload);
        }
        //---------------用于填充需要推送的消息模型
        private PushPayload PushObject_All_All_Alert(string msg, string[] alias = null)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            if (alias != null) { pushPayload.audience = Audience.s_alias(alias); }
            else { pushPayload.audience = Audience.all(); }
            pushPayload.notification = new Notification().setAlert(msg);
            return pushPayload;
        }
        private PushPayload PushSendSmsMessage(string msg, string smsMsg, string[] alias = null)
        {
            var pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            if (alias != null) { pushPayload.audience = Audience.s_alias(alias); }
            else { pushPayload.audience = Audience.all(); }
            pushPayload.notification = new Notification().setAlert(msg);
            SmsMessage sms_message = new SmsMessage();
            sms_message.setContent(smsMsg);
            sms_message.setDelayTime(DELAY_TIME);
            pushPayload.sms_message = sms_message;
            return pushPayload;
        }
    }
}
