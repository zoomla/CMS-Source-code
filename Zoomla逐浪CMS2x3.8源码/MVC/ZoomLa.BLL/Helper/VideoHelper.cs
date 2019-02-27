using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLa.BLL.Helper
{
    public class VideoHelper
    {
        private long multi = 1000 * 10000;
        //从完整视频中截取出指定时间内的视频(0-50)
        public void CutVideo(string infile, string outfile)
        {
            infile = function.VToP(infile);
            outfile = function.VToP(outfile);
            //-r 提取图像的频率，-ss 开始时间，-t 持续时间
            string param = " -ss 0:0:00 -t 0:0:50 -i " + infile + " -vcodec copy -acodec copy " + outfile;
            RunProcess(param);
        }
        //截掉指定时间内的视频(可从前,或从后)
        public void CutFromVideo(string infile, string outfile, int seconds, int seconds2)
        {
            //0:1:00
            infile = function.VToP(infile);
            outfile = function.VToP(outfile);
            VideoFile video = GetVideoInfo(infile);
            TimeSpan stime = new TimeSpan(seconds * multi);
            TimeSpan etime = new TimeSpan((long)video.Duration.TotalSeconds * multi - (seconds2 * multi));
            //-r 提取图像的频率，-ss 开始时间，-t 持续时间
            string param = " -ss " + stime.ToString() + " -t " + etime.ToString() + " -i " + infile + " -vcodec copy -acodec copy " + outfile;//剪切视频
            RunProcess(param);
        }
        /// <summary>
        /// 从视频中获取指定时间段内的视频
        /// </summary>
        /// <param name="infile">输入视频文件</param>
        /// <param name="imgpath">办理出视频文件</param>
        /// <param name="seconds">从第多少秒开始</param>
        public void GetFromVideo(string infile, string outfile, int seconds)
        {
            infile = function.VToP(infile);
            outfile = function.VToP(outfile);
            TimeSpan etime = new TimeSpan(seconds * multi);
            string param = " -ss 00:00:00 -t " + etime.ToString() + " -i " + infile + " -vcodec copy -acodec copy " + outfile;
            RunProcess(param);
        }
        /// <summary>
        /// 视频截图
        /// </summary>
        /// <param name="videofile">视频文件路径</param>
        /// <param name="imgfile">输出图片路径</param>
        /// <param name="seconds">第多少秒的图</param>
        public void CutImgFromVideo(string videofile, string imgfile, int seconds = 10)
        {
            videofile = function.VToP(videofile);
            imgfile = function.VToP(imgfile);
            TimeSpan etime = new TimeSpan(seconds * multi);
            //param = "ffmpeg.exe -i " + videofile + " -y -f image2 -ss 1 -t 0.001 -s 240*180 " + imgfile;//截图并指定大小
            //ffmpeg -i test.asf -vframes 30 -y -f gif a.gif   将前30帧截为Gif,vframes是帧的意思
            string param = " -i " + videofile + " -y -f image2 -ss " + etime.ToString() + " -t 0.001  " + imgfile;//截取指定时间的图片,-ss后是时间,-t后是持续时间,我们可以延长来截Gif
            RunProcess(param);
        }
        //添加水印,fileName视频地址,imgFile水印图片地址,outputFile输出地址  
        /// <summary>
        /// 1,图片文件必须在工作目录下,不能带/,如果要同时加多个或加在其他位置,建议直接用Png透明图
        /// 2,水印图片不能大于视频
        /// </summary>
        /// <param name="imgfile">水印图片路径</param>
        /// <param pos="位置">1:左上,2:右上,3:左下,4:右下</param>
        public string WaterMark(string infile, string outfile, string imgfile, int pos = 1)
        {
            //if (imgfile.Contains("\\") || imgfile.Contains("/")) { throw new Exception("水印图片路径错误"); }
            infile = function.VToP(infile);
            outfile = function.VToP(outfile);
            imgfile = function.VToP(imgfile);
            if (!File.Exists(imgfile)) { throw new Exception(imgfile + ",不存在"); }
            if (File.Exists(outfile)) { SafeSC.DelFile(function.PToV(outfile)); }
            string imgname = Path.GetFileName(imgfile);
            File.Copy(imgfile, WorkingPath + imgname, true);//图片必须指定工作路径,否则无法加上,图片也不能传全路径
            //string param = " -i " + infile + " -vf \"movie=" + imgfile + " [watermark]; [in][watermark] overlay=0:0 [out]\" " + outfile;

            string param = "";
            switch (pos)
            {
                case 2://右上
                    param = " -i {0} -vf \"movie={1} [watermark]; [in][watermark] overlay=main_w-overlay_w-10:10 [out]\" {2}";
                    break;
                case 3://左下
                    param = " -i {0} -vf \"movie={1} [watermark]; [in][watermark] overlay=10:main_h-overlay_h-10 [out]\" {2}";
                    break;
                case 4://右下
                    param = " -i {0} -vf \"movie={1} [watermark]; [in][watermark] overlay=main_w-overlay_w-10:main_h-overlay_h-10 [out]\" {2}";
                    break;
                default://左上
                    param = " -i {0} -vf \"movie={1} [watermark]; [in][watermark] overlay=0:0 [out]\" {2}";
                    break;
            }
            param = string.Format(param, infile, imgname, outfile);
            //Top left corner
            //ffmpeg -i inputvideo.avi -vf "movie=watermarklogo.png [watermark]; [in][watermark] overlay=10:10 [out]" outputvideo.flv

            //Top right corner
            //ffmpeg -i inputvideo.avi -vf "movie=watermarklogo.png [watermark]; [in][watermark] overlay=main_w-overlay_w-10:10 [out]" outputvideo.flv

            //Bottom left corner
            //ffmpeg -i inputvideo.avi -vf "movie=watermarklogo.png [watermark]; [in][watermark] overlay=10:main_h-overlay_h-10 [out]" outputvideo.flv

            //Bottom right corner
            //ffmpeg -i inputvideo.avi -vf "movie=watermarklogo.png [watermark]; [in][watermark] overlay=main_w-overlay_w-10:main_h-overlay_h-10 [out]" outputvideo.flv

            RunProcess(param, WorkingPath);//第二个参数为物理路径
            return outfile;
        }
        /// <summary>
        /// 视频合并,其会等待上面的处理完成再处理下一步
        /// </summary>
        /// <param name="outfile">输出位置</param>
        /// <param name="infile">需要合并的文件</param>
        public void ComBineVideo(string outfile, params string[] infile)
        {
            string[] ofile = new string[infile.Length];//输出的mpg文件位置,mpg是可以合并的文件格式,所以必须先转为mpeg
            string ofname = Path.GetFileNameWithoutExtension(outfile);
            for (int i = 0; i < infile.Length; i++)
            {
                string name = Path.GetFileNameWithoutExtension(infile[i]);
                ofile[i] = function.VToP(WorkingPath + name + ".mpeg");
                string conparam = " -i " + function.VToP(infile[i]) + " -f mpeg  " + ofile[i];//将格式全转为mpeg
                RunProcess(conparam);
            }
            //将多个mpeg视频合并为一个
            string files = "";
            foreach (string file in ofile)//copy /b "D:/result1.mpg"+"D:/result1.mpg" "D:/result.mpge"
            {
                files += file + "|";
            }
            files = files.TrimEnd('|');
            //ffmpeg -i concat:"intermediate1.mpg|intermediate2.mpg" -c copy intermediate_all.mpg
            string combinefile = "/test/" + ofname + "_all.mpeg";
            string combineparam = " -i concat:\"" + files + "\" -c copy " + function.VToP(combinefile);
            RunProcess(combineparam);
            ConvertToFLV(combinefile);
        }
        public void writemsg(string str)
        {
            throw new Exception(ffExe + " " + str);
        }
        public void ConverToMP4(string infile, string outfile)
        {
            //http://downloads.sourceforge.net/faac/faac-1.28.tar.gz  ,需要下载libfaac格式器
            infile = function.VToP(infile);
            outfile = function.VToP(outfile);
            string param = "-y -i " + infile + " -f mp4 -async 1 -s 480x320 -acodec libfaac -vcodec libxvid -qscale 7 -dts_delta_threshold 1 " + outfile;
            RunProcess(param);
        }
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="ffmpegExePath">ffmpeg.exe物理路径</param>
        /// <param name="workPath">工作目录虚拟路径</param>
        public VideoHelper(string ffmpegExePath, string workPath) { ffExe = ffmpegExePath; WorkingPath = function.VToP(workPath); }
        //ffmpeg.exe路径
        public string ffExe { get; set; }
        public string WorkingPath { get; set; }
        private string GetWorkingFile()
        {
            if (File.Exists(ffExe))
            {
                return ffExe;
            }
            if (File.Exists(Path.GetFileName(ffExe)))
            {
                return Path.GetFileName(ffExe);
            }
            return null;
        }
        public static System.Drawing.Image LoadImageFromFile(string fileName)
        {
            System.Drawing.Image theImage = null;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open,
            FileAccess.Read))
            {
                byte[] img;
                img = new byte[fileStream.Length];
                fileStream.Read(img, 0, img.Length);
                fileStream.Close();
                theImage = System.Drawing.Image.FromStream(new MemoryStream(img));
                img = null;
            }
            GC.Collect();
            return theImage;
        }

        public static MemoryStream LoadMemoryStreamFromFile(string fileName)
        {
            MemoryStream ms = null;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open,
            FileAccess.Read))
            {
                byte[] fil;
                fil = new byte[fileStream.Length];
                fileStream.Read(fil, 0, fil.Length);
                fileStream.Close();
                ms = new MemoryStream(fil);
            }
            GC.Collect();
            return ms;
        }
        //Running the ffMpeg Process
        private string RunProcess(string Parameters, string workdir = "")
        {
            //create a process info object so we can run our app
            ProcessStartInfo oInfo = new ProcessStartInfo(this.ffExe, Parameters);
            oInfo.UseShellExecute = false;
            oInfo.CreateNoWindow = true;
            //oInfo.WorkingDirectory = WorkingPath;

            //so we are going to redirect the output and error so that we can parse the return
            oInfo.RedirectStandardOutput = true;
            oInfo.RedirectStandardError = true;
            if (!string.IsNullOrEmpty(workdir)) { oInfo.WorkingDirectory = workdir; }
            //Create the output and streamreader to get the output
            string output = null; StreamReader srOutput = null;
            try
            {
                Process proc = System.Diagnostics.Process.Start(oInfo);
                //proc.WaitForExit(1000);//有无法退出的应用?测试下在其他机器下是否正常
                srOutput = proc.StandardError;//其转换的都输出在Error流中,不是在Stand中
                output = srOutput.ReadToEnd();
                proc.Close();
            }
            catch (Exception ex)
            {
                output = string.Empty;
                throw new Exception(ex.Message);
            }
            finally
            {
                //now, if we succeded, close out the streamreader
                if (srOutput != null)
                {
                    srOutput.Close();
                    srOutput.Dispose();
                }
            }
            return output;
        }
        //Getting the Details(Video)
        public VideoFile GetVideoInfo(MemoryStream inputFile, string Filename)
        {
            //Create a temporary file for our use in ffMpeg
            string tempfile = Path.Combine(this.WorkingPath, System.Guid.NewGuid().ToString() + Path.GetExtension(Filename));
            FileStream fs = File.Create(tempfile);

            //write the memory stream to a file and close our the stream so it can be used again.
            inputFile.WriteTo(fs);
            fs.Flush();
            fs.Close();
            GC.Collect();

            //Video File is a class you will see further down this post.  It has some basic information about the video
            VideoFile vf = new VideoFile(tempfile);

            //And, without adieu, a call to our main method for this functionality.
            GetVideoInfo(vf);
            File.Delete(tempfile);

            return vf;
        }
        public VideoFile GetVideoInfo(string inputPath)
        {
            VideoFile vf = new VideoFile(inputPath);
            GetVideoInfo(vf);
            return vf;
        }

        //And now the important code for the GetVideoInfo
        public void GetVideoInfo(VideoFile input)
        {
            //set up the parameters for video info -- these will be passed into ffMpeg.exe
            string Params = string.Format("-i {0}", input.Path);
            string output = RunProcess(Params);
            input.RawInfo = output;

            //Use a regular expression to get the different properties from the video parsed out.
            Regex re = new Regex("[D|d]uration:.((\\d|:|\\.)*)");
            Match m = re.Match(input.RawInfo);

            if (m.Success)
            {
                string duration = m.Groups[1].Value;
                string[] timepieces = duration.Split(new char[] { ':', '.' });
                if (timepieces.Length == 4)
                {
                    input.Duration = new TimeSpan(0, Convert.ToInt16(timepieces[0]), Convert.ToInt16(timepieces[1]), Convert.ToInt16(timepieces[2]), Convert.ToInt16(timepieces[3]));
                }
            }

            //get audio bit rate
            re = new Regex("[B|b]itrate:.((\\d|:)*)");
            m = re.Match(input.RawInfo);
            double kb = 0.0;
            if (m.Success)
            {
                Double.TryParse(m.Groups[1].Value, out kb);
            }
            input.BitRate = kb;

            //get the audio format
            re = new Regex("[A|a]udio:.*");
            m = re.Match(input.RawInfo);
            if (m.Success)
            {
                input.AudioFormat = m.Value;
            }

            //get the video format
            re = new Regex("[V|v]ideo:.*");
            m = re.Match(input.RawInfo);
            if (m.Success)
            {
                input.VideoFormat = m.Value;
            }

            //get the video format
            re = new Regex("(\\d{2,3})x(\\d{2,3})");
            m = re.Match(input.RawInfo);
            if (m.Success)
            {
                int width = 0; int height = 0;
                int.TryParse(m.Groups[1].Value, out width);
                int.TryParse(m.Groups[2].Value, out height);
                input.Width = width;
                input.Height = height;
            }
            input.infoGathered = true;
        }
        /// <summary>
        /// 开始转换
        /// </summary>
        /// <param name="inputFile">需转换的路径,转换为同名但后缀名不同的视频</param>
        /// <returns></returns>
        public OutputPackage ConvertToFLV(string vpath)
        {
            VideoFile vf = new VideoFile(function.VToP(vpath));
            OutputPackage oo = ConvertToFLV(vf, vpath);
            return oo;
        }
        public OutputPackage ConvertToFLV(MemoryStream inputFile, string Filename)
        {
            string tempfile = Path.Combine(this.WorkingPath, System.Guid.NewGuid().ToString() + Path.GetExtension(Filename));
            FileStream fs = File.Create(tempfile);
            inputFile.WriteTo(fs);
            fs.Flush();
            fs.Close();
            GC.Collect();

            VideoFile vf = new VideoFile(tempfile);
            OutputPackage oo = ConvertToFLV(vf, Filename);
            File.Delete(tempfile);
            return oo;
        }
        //The actually important code, rather than an overload.
        public OutputPackage ConvertToFLV(VideoFile input, string fname)
        {
            fname = Path.GetFileNameWithoutExtension(fname);
            if (!input.infoGathered)
            {
                GetVideoInfo(input);
            }
            OutputPackage ou = new OutputPackage();
            string imgname = fname + ".jpg";
            string flvname = fname + ".flv";
            int secs;
            secs = (int)Math.Round(TimeSpan.FromTicks(input.Duration.Ticks / 3).TotalSeconds, 0);

            string finalpath = Path.Combine(this.WorkingPath, imgname);
            string Params = string.Format("-i {0} {1} -vcodec mjpeg -ss {2} -vframes 1 -an -f rawvideo", input.Path, finalpath, secs);//截图
            //string output = RunProcess(Params);
            //ou.RawOutput = output;
            //if (File.Exists(finalpath))//生成预览图
            //{
            //    //load that file into our output package and attempt to delete the file
            //    //since we no longer need it.
            //    //ou.PreviewImage = LoadImageFromFile(finalpath);
            //    //File.Delete(finalpath);
            //}
            //else
            //{
            //    Params = string.Format("-i {0} {1} -vcodec mjpeg -ss {2} -vframes 1 -an -f rawvideo", input.Path, finalpath, 1);
            //    output = RunProcess(Params);
            //    ou.RawOutput = output;
            //    if (File.Exists(finalpath))
            //    {
            //        //ou.PreviewImage = LoadImageFromFile(finalpath);
            //        //File.Delete(finalpath);
            //    }
            //}

            finalpath = Path.Combine(this.WorkingPath, flvname);
            Params = string.Format("-i {0} -y -ar 22050 -ab 64 -f flv {1}", input.Path, finalpath);//转换文件为flv
            string output = RunProcess(Params);//返回的提示信息
            if (File.Exists(finalpath))
            {
                ou.VideoStream = LoadMemoryStreamFromFile(finalpath);
                //File.Delete(finalpath);
            }
            else { function.WriteErrMsg("转换失败,原因:" + output); }
            ou.VPath = function.PToV(finalpath);
            return ou;
        }
    }
}
