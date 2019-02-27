/*
csc.exe /t:library StrConvHttpModule.cs /r:C:\windows\Microsoft.NET\Framework\v1.1.4322\Microsoft.VisualBasic.dll 
*/

/*
 *由于Html本地缓存,并非每次请求所有html内容,可能只是请求图片,css,该模块处理后,会成html无法显示.
 *改为前台JS与Cookies支持繁体化支持.
 */
namespace ZoomLa.HttpModules
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using ZoomLa.IOs;
    using System.IO;

    public class StrConvHttpModule : IHttpModule
    {
        public string ModuleName
        {
            get
            {
                return "StrConvHttpModule";
            }
        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            if (context.Request.Url.AbsolutePath.ToLower().Contains(".aspx"))
            {
                object o = System.Configuration.ConfigurationManager.AppSettings["TraditionalChinese"];
                if (o != null && o.ToString().ToLower().Equals("true"))
                {
                    if (System.Web.HttpContext.Current.Request.RawUrl.IndexOf("/WebResource.axd") == -1)
                        context.Response.Filter = new StrConvFilterStream(context.Response.Filter);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}

namespace ZoomLa.IOs
{
    using System;
    using System.IO;
    using System.Web;
    using System.Text;
    using System.Globalization;

    using Microsoft.VisualBasic;

    public class StrConvFilterStream : Stream
    {
        private Stream _sink;
        private long _position;

        public StrConvFilterStream(Stream sink)
        {
            this._sink = sink;
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override long Length
        {
            get
            {
                return 0;
            }
        }

        public override long Position
        {
            get
            {
                return this._position;
            }
            set
            {
                this._position = value;
            }
        }

        public override long Seek(long offset, SeekOrigin direction)
        {
            return this._sink.Seek(offset, direction);
        }

        public override void SetLength(long length)
        {
            this._sink.SetLength(length);
        }

        public override void Close()
        {
            this._sink.Close();
        }

        public override void Flush()
        {
            this._sink.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this._sink.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (HttpContext.Current.Response.ContentType == "text/html")
            {
                Encoding e = Encoding.GetEncoding(HttpContext.Current.Response.Charset);
                string s = e.GetString(buffer, offset, count);
                s = Strings.StrConv(s, VbStrConv.TraditionalChinese, CultureInfo.CurrentCulture.LCID);
                this._sink.Write(e.GetBytes(s), 0, e.GetByteCount(s));
            }
            else
            {
                this._sink.Write(buffer, offset, count);
            }
        }
    }
}