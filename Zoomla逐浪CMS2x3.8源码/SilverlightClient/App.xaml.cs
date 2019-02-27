using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightClient
{
    public partial class App : Application
    {
        public string gFileMedia = "";
        public string gHeaderPage = "";
        public int gPageNumber = 0;
        public string gModeLocation = "";

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            int paramOk = 0;
            //recuperation des donnees dans le html
            if (e.InitParams.ContainsKey("gFile"))
            {
                gFileMedia = e.InitParams["gFile"];
                string[] pathss = gFileMedia.Split('|');
                gFileMedia = "Magazine/" + pathss[0].Split('=')[1] + "/" + pathss[1].Split('=')[1];
                paramOk++;
            }
            if (e.InitParams.ContainsKey("gHeaderPage"))
            {
                gHeaderPage = e.InitParams["gHeaderPage"];//5+1+a+s+p+x
                paramOk++;
            }
            if (e.InitParams.ContainsKey("gNum"))
            {
                string recup = e.InitParams["gNum"];
                gPageNumber = int.Parse(recup);
                paramOk++;
            }
            if (e.InitParams.ContainsKey("gLocation"))
            {
                gModeLocation = e.InitParams["gLocation"];
                paramOk++;
            }
            if (paramOk == 4)
            {
                //初始化MainPage
                MainPage maPage = new MainPage();
                maPage.fileMedia = gFileMedia;
                maPage.headerPage = gHeaderPage;
                maPage.maxiPageNum = gPageNumber;
                if (gModeLocation.CompareTo("local") == 0)
                {
                    maPage.modeLocation = MainPage.Location.local;
                }
                if (gModeLocation.CompareTo("web") == 0)
                {
                    maPage.modeLocation = MainPage.Location.web;
                }
                this.RootVisual = maPage;
            }
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
