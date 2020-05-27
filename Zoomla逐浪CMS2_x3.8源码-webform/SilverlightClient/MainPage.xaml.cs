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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using SLMitsuControls;
using System.IO;
using System.Xml.Linq;

namespace SilverlightClient
{
    public partial class MainPage : UserControl, IDataProvider
    {
        //定义全局变量
        private List<object> PageObjectList;//页面对象列表
        public enum PageType { right, left };//页面类型
        public string fileMedia = "";//文件媒体
        public string headerPage = "";//首页
        public int maxiPageNum = 0;//最大页数
        public enum Location { local, web };//枚举应用程序所在
        public Location modeLocation;
        private int pageDownload = 0;//下载的页面数
        private string uriResources = "";//Uri地址
        public List<project> _projects = new List<project>();

        //构造函数
        public MainPage()
        {
            InitializeComponent();
            PageObjectList = new List<object>();
        }

        private void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //OutputTextBlock.Text = e.Error.Message;
                return;
            }
            using (Stream s = e.Result)
            {
                // XmlReader reader = XmlReader.Create(new StringReader(e.Result.ToString()));
                XDocument document = XDocument.Load(s);
                var projects = from f in document.Descendants("project")
                               select new project
                               {
                                   id = f.Attribute("name").Value
                               };

                //maxiPageNum = _projects.Count;
                _projects.AddRange(projects);
                //遍历
                for (int i = 0; i < _projects.Count; i++)
                {
                    _projects[i].id.ToString();
                }

                if (modeLocation == Location.local)
                {
                    this.canvChanging.Visibility = Visibility.Collapsed;
                    this.canvasBook.Visibility = Visibility.Visible;
                    //填充页面列表
                    FillPagesList();
                }
                if (modeLocation == Location.web)
                {
                    this.canvChanging.Visibility = Visibility.Visible;
                    this.canvasBook.Visibility = Visibility.Collapsed;
                    //开始下载页面
                    DownloadPages();
                }
            }
        }


        public class project
        {
            public string id { get; set; }
        } 
        //UserControl事件触发处理
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += wc_OpenReadCompleted;
            client.OpenReadAsync(new Uri("../ClientBin/" + fileMedia + "/FileName.xml", UriKind.Relative));
        }

        //开始将页面填充至List中
        private void FillPagesList()
        {
            //用页面填充列表
            for (int xx = 1; xx <= maxiPageNum; xx++)
            {
                if (xx % 2 != 0)
                {
                    //前一页即奇数页
                    AddPageToList(PageType.right, fileMedia + "/" + _projects[xx - 1].id.ToString(), xx.ToString(), maxiPageNum.ToString(), true);
                }
                else
                {
                    //后一页即偶数页
                    AddPageToList(PageType.left, fileMedia + "/" + _projects[xx - 1].id.ToString(), xx.ToString(), maxiPageNum.ToString(), true);
                }
            }

            //移除最后一页的按钮
            TypePage.RightPage page = PageObjectList[maxiPageNum - 1] as TypePage.RightPage;
            page.setterDisplayBtnNext(false);

            //为翻页按钮指派事件触发处理
            for (int xx = 1; xx < maxiPageNum; xx++)
            {
                if (xx % 2 != 0)
                {
                    //前一页即奇数页
                    TypePage.RightPage pp = PageObjectList[xx - 1] as TypePage.RightPage;
                    Button btnNext = pp.getbtnNext();
                    btnNext.Click += new RoutedEventHandler(btnNext_Click);
                    Button btnTopNext = pp.getbtnTopNext();
                    btnTopNext.Click += new RoutedEventHandler(btnTopNext_Click); ;
                }
                else
                {
                    //后一页即偶数页
                    TypePage.LeftPage pp = PageObjectList[xx - 1] as TypePage.LeftPage;
                    Button btnPrevious = pp.getbtnPrevious();
                    btnPrevious.Click += new RoutedEventHandler(btnPrevious_Click);
                    Button btnPreviousTop = pp.getbtnPreviousTop();
                    btnPreviousTop.Click += btnPreviousTop_Click;

                }
            }
            //为Book设置数据内容
            book.SetData(this);
        }
        //向页面列表中添加具体页面
        private void AddPageToList(PageType pageType, string pathImage, string numPage, string numMaxiPage,
          bool showBtnYesNo)
        {
            switch (pageType)
            {
                case PageType.right:
                    TypePage.RightPage pcd = new SilverlightClient.TypePage.RightPage();
                    pcd.setterimgPhoto(pathImage);
                    pcd.setterPageNumber(numPage, numMaxiPage);
                    pcd.setterDisplayBtnNext(showBtnYesNo);
                    PageObjectList.Add(pcd);
                    break;
                case PageType.left:
                    TypePage.LeftPage pcg = new SilverlightClient.TypePage.LeftPage();
                    pcg.setterimgPhoto(pathImage);
                    pcg.setterPageNumber(numPage, numMaxiPage);
                    pcg.setterDisplayBtnPrevious(showBtnYesNo);
                    PageObjectList.Add(pcg);
                    break;
            }
        }
        //“下一页”按钮事件触发处理
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            book.AnimateToNextPage(500);
        }
        void btnTopNext_Click(object sender, RoutedEventArgs e)
        {
            book.AnimateToNextPage(500);
        }

        //“上一页”按钮事件触发处理
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            book.AnimateToPreviousPage(500);
        }
        void btnPreviousTop_Click(object sender, RoutedEventArgs e)
        {
            book.AnimateToPreviousPage(500);
        }

        //从网络上下载页面
        private void DownloadPages()
        {
            this.canvChanging.Visibility = Visibility.Visible;
            this.uriResources = Application.Current.Host.Source.AbsoluteUri;
            int index = uriResources.IndexOf("SilverlightClient.xap");
            uriResources = uriResources.Substring(0, index);
            this.changingProgressBar.Minimum = 0;
            this.changingProgressBar.Maximum = maxiPageNum - 1;
            string theResources = uriResources + fileMedia + "/" + _projects[pageDownload].id.ToString();
            string theResourcesNum = _projects[pageDownload].id.ToString();
            AsynchronouslyDownloadPage(theResources, theResourcesNum);
        }

        //异步下载页面
        private void AsynchronouslyDownloadPage(string path, string num)
        {
            WebClient unWeb = new WebClient();
            unWeb.DownloadStringCompleted += new DownloadStringCompletedEventHandler(unWeb_DownloadStringCompleted);
            unWeb.DownloadStringAsync(new Uri(path));
            this.changingText.Text = "正在下载 : " + num;
            this.changingProgressBar.Value = this.pageDownload;
        }

        //异步下载页面完成事件触发处理
        private void unWeb_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            this.pageDownload++;
            if (this.pageDownload < this.maxiPageNum)//持续不断下载页面直到所有页面都下完
            {
                string theResources = uriResources + fileMedia + "/" + _projects[pageDownload].id.ToString();
                string theResourcesNum = _projects[pageDownload].id.ToString();
                AsynchronouslyDownloadPage(theResources, theResourcesNum);
            }
            else
            {
                FillPagesList();
                this.canvChanging.Visibility = Visibility.Collapsed;
                this.canvasBook.Visibility = Visibility.Visible;
            }
        }

        //强制声明接口
        #region IDataProvider Members
        public object GetItem(int index)
        {
            return PageObjectList[index];
        }
        public int GetCount()
        {
            return PageObjectList.Count;
        }
        #endregion
    }
}