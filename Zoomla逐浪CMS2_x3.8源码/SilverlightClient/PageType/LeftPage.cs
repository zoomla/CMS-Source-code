using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SilverlightClient.TypePage
{
    public class LeftPage : Canvas
    {
        //定义将在页面上显示的元素
        private Image imgPhoto;
        private Button btnPrevious;
        private Rectangle RecBorder;
        private TextBlock PageNum;
        private Button btnPreviousTop;
        private TextBlock PageNumTop;
        //构造函数
        public LeftPage()
        {
            //页面的设置
            this.Width = 579;
            this.Height = 700;
            this.Background = new SolidColorBrush(Colors.White);
            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
            //页面边框的设置
            RecBorder = new Rectangle();
            RecBorder.Width = 579;
            RecBorder.Height = 700;
            Canvas.SetLeft(RecBorder, 0);//设置页面边框在Canvas中的位置，下同。
            Canvas.SetTop(RecBorder, 0);
            RecBorder.Stroke = new SolidColorBrush(Colors.Black);
            RecBorder.StrokeThickness = 0;
            this.Children.Add(RecBorder);
            //照片的设置
            imgPhoto = new Image();
            imgPhoto.Width = 579;
            imgPhoto.Height = 700;
            Canvas.SetLeft(imgPhoto, 0);
            Canvas.SetTop(imgPhoto, 0);
            this.Children.Add(imgPhoto);
            //“前一页”按钮的设置
            btnPrevious = new Button();
            btnPrevious.Width = 100;
            btnPrevious.Height = 20;
            btnPrevious.Content = "<< 前一页";
            btnPrevious.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnPrevious.VerticalContentAlignment = VerticalAlignment.Center;
            btnPrevious.Cursor = Cursors.Hand;
           // btnPrevious.Background = ;
            Canvas.SetLeft(btnPrevious, 250);
            Canvas.SetTop(btnPrevious, 650);
            this.Children.Add(btnPrevious);
            //页码文本的设置
            PageNum = new TextBlock();
            PageNum.Width = 100;
            PageNum.Height = 20;
            PageNum.Text = "00 / 00";
            PageNum.TextAlignment = TextAlignment.Left;
            PageNum.VerticalAlignment = VerticalAlignment.Center;
            PageNum.FontFamily = new FontFamily("Comic sans MS");
            Canvas.SetLeft(PageNum, 10);
            Canvas.SetTop(PageNum, 650);
            this.Children.Add(PageNum);
            //top
            //“前一页”按钮的设置
            btnPreviousTop = new Button();
            btnPreviousTop.Width = 100;
            btnPreviousTop.Height = 20;
            btnPreviousTop.Content = "<< 前一页";
            btnPreviousTop.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnPreviousTop.VerticalContentAlignment = VerticalAlignment.Center;
            btnPreviousTop.Cursor = Cursors.Hand;
            // btnPrevious.Background = ;
            Canvas.SetLeft(btnPreviousTop, 250);
            Canvas.SetTop(btnPreviousTop, 20);
            this.Children.Add(btnPreviousTop);
            //页码文本的设置
            PageNumTop = new TextBlock();
            PageNumTop.Width = 100;
            PageNumTop.Height = 20;
            PageNumTop.Text = "00 / 00";
            PageNumTop.TextAlignment = TextAlignment.Left;
            PageNumTop.VerticalAlignment = VerticalAlignment.Center;
            PageNumTop.FontFamily = new FontFamily("Comic sans MS");
            Canvas.SetLeft(PageNumTop, 10);
            Canvas.SetTop(PageNumTop, 20);
            this.Children.Add(PageNumTop);
        }
        //设置图片路径
        public void setterimgPhoto(string path)
        {
            BitmapImage btm = new BitmapImage();
            btm.UriSource = new Uri(path, UriKind.Relative);
            imgPhoto.Source = btm;
        }
        //设置按钮是否可见
        public void setterDisplayBtnPrevious(bool YesNo)
        {
            if (YesNo)
            {
                btnPrevious.Visibility = Visibility.Visible;
                btnPreviousTop.Visibility = Visibility.Visible;
            }
            else
            {
                btnPrevious.Visibility = Visibility.Collapsed;
                btnPreviousTop.Visibility = Visibility.Collapsed;
            }
        }
        //设置页码
        public void setterPageNumber(string currentPageNum, string TotalPageNum)
        {
            PageNum.Text = currentPageNum + " / " + TotalPageNum;
            PageNumTop.Text = currentPageNum + "/" + TotalPageNum;
        }
        //返回按钮单击事件关联
        public Button getbtnPrevious()
        {
            return btnPrevious;
        }

        public Button getbtnPreviousTop()
        {
            return btnPreviousTop;
        }
    }
}