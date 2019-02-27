using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Diagnostics;
/// <summary> 
/// CompressedViewState 的摘要说明 
/// </summary> 
public class CompressViewState : System.Web.UI.Page
{
    /// <summary> 
    /// 设定序列化后的字符串长度为多少后才启用压缩，此处设置为5K 
    /// </summary> 
    private static Int32 LimitLength = 5120;
    /// <summary> 
    /// 设定压缩比率，压缩比率越高性消耗也将增大 
    /// </summary> 
    private static Int32 ZipLevel = ICSharpCode.SharpZipLib.Zip.Compression.Deflater.BEST_COMPRESSION;
    /// <summary> 
    /// 重写保存页的所有视图状态信息 
    /// </summary> 
    /// <param name="pViewState">要在其中存储视图状态信息的对象</param> 
    protected override void SavePageStateToPersistenceMedium(Object pViewState)
    {
        //实现一个用于将信息写入字符串的 TextWriter 
        StringWriter mWriter = new StringWriter();
        //序列化 Web 窗体页的视图状态 
        LosFormatter mFormat = new LosFormatter();
        //将有限对象序列化 (LOS) 格式化的对象转换为视图状态值 
        mFormat.Serialize(mWriter, pViewState);
        //将序列化对象转成Base64字符串 
        String vStateStr = mWriter.ToString();
        //设置是否启用了压缩方式，默认情况下为不启用 
        Boolean mUseZip = false;
        //判断序列化对象的字符串长度是否超出定义的长度界限 
        if (vStateStr.Length > LimitLength)
        {
            //对于长度超出阶线的进行压缩，同时将状态设为压缩方式 
            mUseZip = true;
            Byte[] pBytes = Compress(vStateStr);
            //将字节数组转换为Base64字符串 
            vStateStr = System.Convert.ToBase64String(pBytes);
        }
        //注册在页面储存ViewState状态的隐藏文本框，并将内容写入这个文本框 
        ClientScript.RegisterHiddenField("__MSPVSTATE", vStateStr);
        //注册在页面储存是否启用压缩状态的文本框，并将启用状态写入这个文本框 
        ClientScript.RegisterHiddenField("__MSPVSTATE_ZIP", mUseZip.ToString().ToLower());
    }
    /**/
    /// <summary> 
    /// 对字符串进行压缩 
    /// </summary> 
    /// <param name="pViewState">ViewState字符串</param> 
    /// <returns>返回流的字节数组</returns> 
    private static Byte[] Compress(String pViewState)
    {
        //将存储状态的Base64字串转换为字节数组 
        Byte[] pBytes = System.Convert.FromBase64String(pViewState);
        //创建支持内存存储的流 
        MemoryStream mMemory = new MemoryStream();
        Deflater mDeflater = new Deflater(ZipLevel);
        ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream mStream = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream(mMemory, mDeflater, 131072);
        mStream.Write(pBytes, 0, pBytes.Length);
        mStream.Close();
        return mMemory.ToArray();
    }
    /// <summary> 
    /// 重写将所有保存的视图状态信息加载到页面对象 
    /// </summary> 
    /// <returns>保存的视图状态</returns> 
    protected override Object LoadPageStateFromPersistenceMedium()
    {
        //使用Request方法获取序列化的ViewState字符串 
        String mViewState = this.Request.Form.Get("__MSPVSTATE");
        //使和Request方法获取当前的ViewState是否启用了压缩 
        String mViewStateZip = this.Request.Form.Get("__MSPVSTATE_ZIP");
        Byte[] pBytes;
        if (mViewStateZip == "true")
        {
            pBytes = DeCompress(mViewState);
        }
        else
        {
            //将ViewState的Base64字符串转换成字节 
            pBytes = System.Convert.FromBase64String(mViewState);
        }
        //序列化 Web 窗体页的视图状态 
        LosFormatter mFormat = new LosFormatter();
        //将指定的视图状态值转换为有限对象序列化 (LOS) 格式化的对象 
        return mFormat.Deserialize(System.Convert.ToBase64String(pBytes));
    }
    /// <summary> 
    /// 解压缩ViewState字符串 
    /// </summary> 
    /// <param name="pViewState">ViewState字符串</param> 
    /// <returns>返回流的字节数组</returns> 
    private static Byte[] DeCompress(String pViewState)
    {
        //将Base64字符串转换为字节数组 
        Byte[] pBytes = System.Convert.FromBase64String(pViewState);
        ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream mStream = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream(new MemoryStream(pBytes));
        //创建支持内存存储的流 
        MemoryStream mMemory = new MemoryStream();
        Int32 mSize;
        Byte[] mWriteData = new Byte[4096];
        while (true)
        {
            mSize = mStream.Read(mWriteData, 0, mWriteData.Length);
            if (mSize > 0)
            {
                mMemory.Write(mWriteData, 0, mSize);
            }
            else
            {
                break;
            }
        }
        mStream.Close();
        return mMemory.ToArray();
    }
    public void UnZip(string[] args)
    {
        ZipInputStream s = new ZipInputStream(File.OpenRead(args[0]));

        ZipEntry theEntry;
        while ((theEntry = s.GetNextEntry()) != null)
        {

            string directoryName = Path.GetDirectoryName(args[1]);
            string fileName = Path.GetFileName(theEntry.Name);

            //生成解压目录
            Directory.CreateDirectory(directoryName);

            if (fileName != String.Empty)
            {
                //解压文件到指定的目录
                FileStream streamWriter = File.Create(args[1] + theEntry.Name);

                int size = 2048;
                byte[] data = new byte[2048];
                while (true)
                {
                    size = s.Read(data, 0, data.Length);
                    if (size > 0)
                    {
                        streamWriter.Write(data, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }

                streamWriter.Close();
            }
        }
        s.Close();
    }
    public void ZipFile(string FileToZip, string ZipedFile, int CompressionLevel, int BlockSize)
    {
        //如果文件没有找到，则报错
        if (!System.IO.File.Exists(FileToZip))
        {
            throw new System.IO.FileNotFoundException("The specified file " + FileToZip + " could not be found. Zipping aborderd");
        }

        System.IO.FileStream StreamToZip = new System.IO.FileStream(FileToZip, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        System.IO.FileStream ZipFile = System.IO.File.Create(ZipedFile);
        ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
        ZipEntry ZipEntry = new ZipEntry("ZippedFile");
        ZipStream.PutNextEntry(ZipEntry);
        ZipStream.SetLevel(CompressionLevel);
        byte[] buffer = new byte[BlockSize];
        System.Int32 size = StreamToZip.Read(buffer, 0, buffer.Length);
        ZipStream.Write(buffer, 0, size);
        try
        {
            while (size < StreamToZip.Length)
            {
                int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                ZipStream.Write(buffer, 0, sizeRead);
                size += sizeRead;
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        ZipStream.Finish();
        ZipStream.Close();
        StreamToZip.Close();
    }

    public void ZipFileMain(string[] args)
    {
        string[] filenames = Directory.GetFiles(args[0]);

        Crc32 crc = new Crc32();
        ZipOutputStream s = new ZipOutputStream(File.Create(args[1]));

        s.SetLevel(6); // 0 - store only to 9 - means best compression

        foreach (string file in filenames)
        {
            //打开压缩文件
            FileStream fs = File.OpenRead(file);

            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            ZipEntry entry = new ZipEntry(file);

            entry.DateTime = DateTime.Now;

            // set Size and the crc, because the information
            // about the size and crc should be stored in the header
            // if it is not set it is automatically written in the footer.
            // (in this case size == crc == -1 in the header)
            // Some ZIP programs have problems with zip files that don't store
            // the size and crc in the header.
            entry.Size = fs.Length;
            fs.Close();

            crc.Reset();
            crc.Update(buffer);

            entry.Crc = crc.Value;

            s.PutNextEntry(entry);

            s.Write(buffer, 0, buffer.Length);

        }

        s.Finish();
        s.Close();
    }


    /// <summary>
    /// 解压文件
    /// </summary>
    /// <param name="unRarPatch">rar所在目录</param>
    /// <param name="rarPatch">要解压到的目录</param>
    /// <param name="rarName">rar文件名</param>
    /// <param name="deleteAfterUnCompress">解压结束后是否删除原文件</param>
    /// <returns></returns>
    public static bool UnCompressRAR(string unRarPatch, string rarPatch, string rarName, bool deleteAfterUnCompress)
    {
        string the_Info;
        try
        {

            #region 开始解压文件前的检查和准备

            //验证传输人的参数不为null和空值
            if (unRarPatch == null || rarPatch == null || rarName == null || unRarPatch == string.Empty || rarPatch == string.Empty || rarName == string.Empty)
            {
                return false;
            }

            if (File.Exists(unRarPatch + rarName) == false)
            {
                return false;//要解压的文件夹不存在
            }

            if (Directory.Exists(unRarPatch) == false)
            {
                Directory.CreateDirectory(unRarPatch);
            }

            #endregion

            #region 开始解压文件

            the_Info = "x \"" + rarName + "\" \"" + unRarPatch + "\" -y";

            ProcessStartInfo the_StartInfo = new ProcessStartInfo();
            the_StartInfo.RedirectStandardOutput = true;
            the_StartInfo.UseShellExecute = false;
            //the_StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"bin\rar.exe";
            the_StartInfo.FileName = @"E:\C#\GlobalError\UnitTest\RarHelperTest\bin\Debug\rar.exe";
            the_StartInfo.Arguments = the_Info;
            the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            the_StartInfo.WorkingDirectory = rarPatch;//获取压缩包路径


            Process the_Process = new Process();
            the_Process.StartInfo = the_StartInfo;
            the_Process.Start();
            the_Process.WaitForExit();
            the_Process.Close();

            #endregion

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        //解压后删除
        if (deleteAfterUnCompress)
        {
            if (File.Exists(rarPatch + "\\" + rarName))
                File.Delete(rarPatch + "\\" + rarName);
        }

        return true;
    }


    /// <summary>
    /// 压缩文件夹
    /// </summary>
    /// <param name="compressPath">要压缩的文件夹名</param>
    /// <param name="rarName">压缩后的文件名</param>
    /// <param name="rarPath">压缩后所在的路径名</param>
    /// <returns></returns>
    public static bool CompressRAR(string compressPath, string rarName, string rarPath)
    {
        string the_Info;
        try
        {

            #region 开始压缩文件前的检查和准备

            //验证传输人的参数不为null和空值
            if (compressPath == null || rarName == null || rarPath == null || compressPath == string.Empty || rarName == string.Empty || rarPath == string.Empty)
            {
                return false;
            }
            if (Directory.Exists(compressPath) == false)
            {
                return false;//要压缩的文件夹不存在
            }
            if (Directory.Exists(rarPath) == false)
            {
                Directory.CreateDirectory(rarPath);
            }

            #endregion

            #region 开始压缩文件

            the_Info = @" a -k -r -s -m1 {0} {1} ";
            the_Info = String.Format(the_Info, rarPath + rarName, compressPath);

            ProcessStartInfo the_StartInfo = new ProcessStartInfo();
            the_StartInfo.RedirectStandardOutput = true;
            the_StartInfo.UseShellExecute = false;
            //the_StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"bin\rar.exe";
            the_StartInfo.FileName = @"E:\C#\GlobalError\UnitTest\RarHelperTest\bin\Debug\rar.exe";
            the_StartInfo.Arguments = the_Info;
            the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //the_StartInfo.WorkingDirectory = rarPatch;//获取压缩包路径

            Process the_Process = new Process();
            the_Process.StartInfo = the_StartInfo;
            the_Process.Start();
            the_Process.WaitForExit();
            the_Process.Close();

            #endregion

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    #region url重写更改action
    #region Render

    /// <summary>
    ///  重写默认的HtmlTextWriter方法，修改form标记中的value属性，使其值为重写的URL而不是真实URL。
    /// </summary>
    /// <param name="writer"></param>
    protected override void Render(HtmlTextWriter writer)
    {

        if (writer is System.Web.UI.Html32TextWriter)
        {
            writer = new FormFixerHtml32TextWriter(writer.InnerWriter);
        }
        else
        {
            writer = new FormFixerHtmlTextWriter(writer.InnerWriter);
        }

        base.Render(writer);
    }
    #endregion

    #region FormFixers

    #region FormFixerHtml32TextWriter
    internal class FormFixerHtml32TextWriter : System.Web.UI.Html32TextWriter
    {
        private string _url; // 假的URL

        internal FormFixerHtml32TextWriter(TextWriter writer)
            : base(writer)
        {
            _url = HttpContext.Current.Request.RawUrl;
        }

        public override void WriteAttribute(string name, string value, bool encode)
        {
            // 如果当前输出的属性为form标记的action属性，则将其值替换为重写后的虚假URL
            if (_url != null && string.Compare(name, "action", true) == 0)
            {
                value = _url;
            }
            base.WriteAttribute(name, value, encode);
        }
    }
    #endregion

    #region FormFixerHtmlTextWriter
    internal class FormFixerHtmlTextWriter : System.Web.UI.HtmlTextWriter
    {
        private string _url;
        internal FormFixerHtmlTextWriter(TextWriter writer)
            : base(writer)
        {
            _url = HttpContext.Current.Request.RawUrl;
        }

        public override void WriteAttribute(string name, string value, bool encode)
        {
            if (_url != null && string.Compare(name, "action", true) == 0)
            {
                value = _url;
            }

            base.WriteAttribute(name, value, encode);
        }
    }
    #endregion

    #endregion
    #endregion
}