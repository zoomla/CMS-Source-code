using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ZoomLa.Model.Plat
{
    public class M_Plat_Content
    {
        public int id = 0;
        public string title = "";
        public string topimg = "";
        //封面图片(暂定为取第一张图)
        public string pic = "";
        //背景音乐
        public string mp3 = "";
        public string ParticIDS = "";
        public string ParticNames = "";
        public string EDate = "";
        ////成员控件列表,用于后期修改
        public List<M_Plat_Component> comlist = new List<M_Plat_Component>();
    }
    public class M_Plat_Component
    {
        public string id = "";
        public string content = "";
        public string type = "";
        public string videoType = "";
        public string text = "";
        public bool openText = false;
        public string title = "";
        public int orderID = 0;
    }
}
