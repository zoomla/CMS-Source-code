using System;
using System.Collections.Generic;
using System.DirectoryServices;

using System.Text;

namespace IIS
{
    public class WebSite
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ServerComment { get; set; }        
        public string IP { get; set; }
        public string Port { get; set; }
        public string Domain { get; set; }
        public VirtualDirectory Root { get; set; }
        public string Serverbindings
        {
            get { return String.Format("{0}:{1}:{2}", IP, Port, Domain); }
        }
        public string SecureBindings { get; set; }

        public WebSite(string serverComment, string physicsPath,string Port,string IP,string Domain)
        {
            this.ServerComment = serverComment;
            this.IP = IP;
            this.Port = Port;
            this.Domain = Domain;
            this.SecureBindings =string.Empty;


            Root = new VirtualDirectory("Root", physicsPath);
            Root.ApplicatonName = serverComment;
        }

    }
}
