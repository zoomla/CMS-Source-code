using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Components;
/// <summary>
/// 仅用于内容管理,缩略图
/// </summary>
namespace ZoomLa.BLL.Helper
{
    public class ThumbHelper
    {
        /// <summary>
        ///  创建缩图文件
        /// </summary>
        /// <param name="vpath">topimg的路径</param>
        /// <param name="thumb">压图字段的值</param>
        /// <returns>处理后的图片路径</returns>
        public static string Thumb_Compress(string vpath, string thumb, int destWidth, int destHeight)
        {
            if (string.IsNullOrEmpty(vpath)) { return thumb; }
            if (!File.Exists(function.VToP(vpath))) { return thumb; }
            if (!SafeSC.IsImage(vpath)) { return thumb; }
            //----------------------------
            ImgHelper imghelp = new ImgHelper();
            //为空或不存在,则重新生成缩图名称
            if (string.IsNullOrEmpty(thumb) || !File.Exists(function.VToP(thumb)))
            {
                string fname = Path.GetFileNameWithoutExtension(vpath);
                string ext = Path.GetExtension(vpath);
                string thumb_fname = fname + ".thumb" + ext;
                thumb = vpath.Substring(0, vpath.LastIndexOf("/") + 1) + thumb_fname;
            }
            else { return thumb; }
            //如图片小于设定,则也不压缩,直接返回其路径即可
            //int destWidth = SiteConfig.ThumbsConfig.ThumbsWidth;
            //int destHeight = SiteConfig.ThumbsConfig.ThumbsHeight;
            //生成缩图保存
            System.Drawing.Image img = ImgHelper.ReadImgToMS(vpath);
            if ((destWidth + destHeight) < 1) { ImgHelper.SaveImage(thumb, img); }
            else if (img.Width <= destWidth && img.Height <= destHeight) { ImgHelper.SaveImage(thumb, img); }
            else
            {
                System.Drawing.Bitmap bmp = imghelp.ZoomImg(vpath, destHeight, destWidth);
                imghelp.SaveImg(thumb, bmp);
            }
            return thumb;
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
