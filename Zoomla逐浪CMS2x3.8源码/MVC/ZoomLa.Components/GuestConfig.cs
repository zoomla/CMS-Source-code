using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Web;
using System.Web.Caching;
using System.IO;
using System.Security;

namespace ZoomLa.Components
{
    public class GuestConfig
    {
        private string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Guest.config";
        public GuestConfig()
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                this.filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Guest.config";
            }
        }
        public static GuestConfigInfo ConfigReadFromFile()
        {
            using (Stream stream = new FileStream(new GuestConfig().filePath, FileMode.OpenOrCreate))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GuestConfigInfo));
                return (GuestConfigInfo)serializer.Deserialize(stream);
            }
        }
        public void Update(GuestConfigInfo config)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GuestConfigInfo));
                using (Stream stream = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    serializer.Serialize(stream, config);
                    stream.Close();
                    GuestConfig.ConfigReadFromFile();
                }
            }
            catch (SecurityException exception)
            {
                throw new SecurityException(exception.Message, exception.DenySetInstance, exception.PermitOnlySetInstance, exception.Method, exception.Demanded, exception.FirstPermissionThatFailed);
            }
            finally
            {

            }

        }
        private static GuestConfigInfo _guestoption;
        public static GuestConfigInfo GuestOption
        {
            get 
            {
                if (_guestoption == null)
                    _guestoption = ConfigReadFromFile();
                return _guestoption == null ? new GuestConfigInfo() : _guestoption;
            }
        }
    }
    [Serializable]
    public class GuestConfigInfo
    {
        public WDOption WDOption { get; set; }

        public BKOption BKOption { get; set; }
        public List<BarOption> BarOption { get; set; }

        public GuestConfigInfo()
        {
            WDOption = new WDOption();
            BKOption = new BKOption();
            BarOption = new List<BarOption>();
        }
    }
}
