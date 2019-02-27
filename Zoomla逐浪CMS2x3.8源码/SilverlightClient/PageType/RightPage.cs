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
    public class RightPage : Canvas
    {
        //定义将在页面上显示的元素
        private Image imgPhoto;
        private Button btnNext;
        private Rectangle RecBorder;
        private TextBlock PageNum;
        private Button btnTopNext;
        private TextBlock PageTopNum;

        //构造函数
        public RightPage()
        {
            //页面的设置
            this.Width = 579;
            this.Height = 700;
            this.Background = new SolidColorBrush(Colors.White);
            Canvas.SetLeft(this, 0);//设置页面边框在Canvas中的位置，下同。
            Canvas.SetTop(this, 0);
            //页面边框的设置
            RecBorder = new Rectangle();
            RecBorder.Width = 579;
            RecBorder.Height = 700;
            Canvas.SetLeft(RecBorder,0);
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
            //“后一页”按钮的设置
            btnNext = new Button();
            btnNext.Width = 100;
            btnNext.Height = 20;
            btnNext.Content = "后一页 >>";
            btnNext.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnNext.VerticalContentAlignment = VerticalAlignment.Center;
            btnNext.Cursor = Cursors.Hand;
            Canvas.SetLeft(btnNext, 250);
            Canvas.SetTop(btnNext, 650);
            this.Children.Add(btnNext);
            //页码文本的设置
            PageNum = new TextBlock();
            PageNum.Width = 100;
            PageNum.Height = 20;
            PageNum.Text = "00 / 00";
            PageNum.TextAlignment = TextAlignment.Right;
            PageNum.VerticalAlignment = VerticalAlignment.Center;
            PageNum.FontFamily = new FontFamily("Comic sans MS");
            Canvas.SetLeft(PageNum, 465);
            Canvas.SetTop(PageNum, 650);
            this.Children.Add(PageNum);
            //上
            //“后一页”按钮的设置
            btnTopNext = new Button();
            btnTopNext.Width = 100;
            btnTopNext.Height = 20;
            btnTopNext.Content = "后一页 >>";
            btnTopNext.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnTopNext.VerticalContentAlignment = VerticalAlignment.Center;
            btnTopNext.Cursor = Cursors.Hand;
            Canvas.SetLeft(btnTopNext, 250);
            Canvas.SetTop(btnTopNext, 20);
            this.Children.Add(btnTopNext);
            //页码文本的设置
            PageTopNum = new TextBlock();
            PageTopNum.Width = 100;
            PageTopNum.Height = 20;
            PageTopNum.Text = "00 / 00";
            PageTopNum.TextAlignment = TextAlignment.Right;
            PageTopNum.VerticalAlignment = VerticalAlignment.Center;
            PageTopNum.FontFamily = new FontFamily("Comic sans MS");
            Canvas.SetLeft(PageTopNum, 465);
            Canvas.SetTop(PageTopNum, 20);
            this.Children.Add(PageTopNum);
        }
        //设置图片路径
        public void setterimgPhoto(string path)
        {
            BitmapImage btm = new BitmapImage();
            btm.UriSource = new Uri(path, UriKind.Relative);
            imgPhoto.Source = btm;
        }
        //设置按钮是否可见
        public void setterDisplayBtnNext(bool YesNo)
        {
            if (YesNo)
            {
                btnNext.Visibility = Visibility.Visible;
                btnTopNext.Visibility = Visibility.Visible;
            }
            else
            {
                btnNext.Visibility = Visibility.Collapsed;
                btnTopNext.Visibility = Visibility.Collapsed;
            }
        }
        //设置页码
        public void setterPageNumber(string currentPageNum, string TotalPageNum)
        {
            PageNum.Text = currentPageNum + " / " + TotalPageNum;
            PageTopNum.Text=currentPageNum+"/"+TotalPageNum;
        }
        //返回按钮单击事件关联
        public Button getbtnNext()
        {
            return btnNext;
        }
        public Button getbtnTopNext()
        {
            return btnTopNext;
        }


    }
}