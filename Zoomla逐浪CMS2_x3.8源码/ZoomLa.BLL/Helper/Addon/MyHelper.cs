using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Components;


namespace ZoomLa.BLL.Helper
{
    public class MyHelper
    {
        //1,后台添加与修改内容时,如选定了缩略图(扩展图),则会自动压缩
        //2,压缩为新生成图片文件,图片名.thumb.jpg,图片路径存ZL_CommonModel.TopImg中
        //3,如果缩图已存在,或缩略图本身就符合条件,则不会再压缩
        //  3处需要修改

        /// <summary>
        ///  创建缩图文件
        /// </summary>
        /// <param name="vpath"></param>
        /// <returns></returns>
        public static string Thumb_Compress(string vpath)
        {
            vpath = (vpath ?? "").Replace(" ", "");
            if (string.IsNullOrEmpty(vpath)) { return vpath; }
            if (!File.Exists(function.VToP(vpath))) { return vpath; }
            if (!SafeSC.IsImage(vpath)) { return vpath; }
            //----------------------------
            string fname = Path.GetFileNameWithoutExtension(vpath);
            string ext = Path.GetExtension(vpath);
            string thumb_fname = fname + ".thumb" + ext;
            string thumb_vpath = vpath.Substring(0, vpath.LastIndexOf("/") + 1) + thumb_fname;
            //如路径已存在缩图,则不处理
            if (File.Exists(function.VToP(thumb_vpath))) { return thumb_vpath; }
            //如图片小于设定,则也不压缩
            int destWidth = SiteConfig.ThumbsConfig.ThumbsWidth;
            int destHeight = SiteConfig.ThumbsConfig.ThumbsHeight;
            if ((destWidth + destHeight) < 1) { return vpath; }
            System.Drawing.Image img = ImgHelper.ReadImgToMS(vpath);
            if (img.Width <= destWidth && img.Height <= destHeight) { return vpath; }
            //生成缩图保存
            ImgHelper imghelp = new ImgHelper();
            System.Drawing.Bitmap bmp = imghelp.ZoomImg(vpath, SiteConfig.ThumbsConfig.ThumbsHeight, SiteConfig.ThumbsConfig.ThumbsWidth);
            imghelp.SaveImg(thumb_vpath, bmp);
            return thumb_vpath;
        }
        /// <summary>
        /// 将路径转换后返回,以避免前端逻辑
        /// </summary>
        /// <param name="thumbPath">压缩后的图片路径</param>
        /// <returns></returns>
        public static string Thumb_ConverPath(string thumbPath)
        {
            //无压缩或为空则直返
            if (string.IsNullOrEmpty(thumbPath) || !thumbPath.Contains(".thumb.")) { return thumbPath; }
            return thumbPath.Replace(".thumb.", ".");
        }
    }
}
