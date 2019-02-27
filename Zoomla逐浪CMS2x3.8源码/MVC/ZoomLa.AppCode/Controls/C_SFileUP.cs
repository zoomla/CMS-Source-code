using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Common;

namespace ZoomLa.Controls
{
    /// <summary>
    /// 用于上传控件,提交时将参数序列化给Action以完成配置交互
    /// 1,默认重命名文件
    /// </summary>
    public class C_SFileUP
    {
        private string _id = function.GetRandomString(6).ToLower();
        public string ID { get { return _id; } set { _id = value; } }//未定义则随机生成
        //是否加载js与css资源
        public bool LoadRes = true;
        //存储路径,只能使用类型,不可自由定义路径
        public string SaveType = "user";//user|admin|oa|plat等
        //根据FileType生成不同的检测,支持img|office|all|自定义后缀名等格式
        public string FileType = "all";
        //AJAX上传完成前,要执行的JS方法,传入方法名
        public string UP_Before = "";
        //AJAX上传完成后,需要执行的JS方法,传入方法名
        public string UP_After = "";
        //是否为Base64字符串
        public bool IsBase64 = false;
        //初始文本框中的值
        public string Value = "";
        //--------------------------图片相关
        //仅适用于图片,是否启用压缩
        public bool IsCompress = false;
        //图片最大宽高,非为则进行压缩处理与压缩选项只能有一个生效
        public int MaxWidth = 0;
        public int MaxHeight = 0;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
