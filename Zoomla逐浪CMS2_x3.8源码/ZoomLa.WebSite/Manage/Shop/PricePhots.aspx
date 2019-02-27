<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PricePhots.aspx.cs" Inherits="manage_Shop_PricePhots" ContentType="image/jpeg" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<!DOCTYPE HTML>
<html>
<head >
<title>价格对比</title>

<script language="C#" runat="server">   
class LineChart
{
public Bitmap b ;
public string Title = "团购价格对比图" ;
public ArrayList chartValues = new ArrayList ( ) ;
public float Xorigin = 0 , Yorigin = 0 ;
public float ScaleX , ScaleY ;
public float Xdivs = 2 , Ydivs = 2 ;
private int Width , Height ;
private Graphics g ;
private Page p ;
struct datapoint {
    public float x ;
    public float y ;
    public bool valid ;
}
public LineChart ( int myWidth , int myHeight , Page myPage ) {
    Width = myWidth ; Height = myHeight ;
    ScaleX = myWidth ; ScaleY = myHeight ;
    b = new Bitmap ( myWidth , myHeight ) ;
    g = Graphics . FromImage ( b ) ;
    p = myPage ;
}
public void AddValue ( int x , int y ) {
    datapoint myPoint ;
    myPoint . x = x ;
    myPoint . y = y ;
    myPoint . valid = true ;
    chartValues . Add ( myPoint ) ;
}
public void Draw ( ) {   
    int i ;
    float x , y , x0 , y0 ;
    string myLabel ;
    Pen blackPen = new Pen ( Color . Blue , 2 ) ;
    Brush blackBrush = new SolidBrush ( Color . Black ) ;
    Font axesFont = new Font ( "arial" , 10 ) ;
    //首先要创建图片的大小
    p . Response . ContentType = "image/jpeg" ;
    g . FillRectangle ( new SolidBrush ( Color . LightGreen ) , 0 , 0 , Width , Height ) ;
    int ChartInset = 50 ;
    int ChartWidth = Width - ( 2 * ChartInset ) ;
    int ChartHeight = Height - ( 2 * ChartInset ) ;
    g . DrawRectangle ( new Pen ( Color . Black , 1 ) , ChartInset , ChartInset , ChartWidth , ChartHeight ) ;
    //写出图片上面的图片内容文字
    g . DrawString ( Title , new Font ( "arial" , 14 ) , blackBrush , Width / 3 , 10 ) ;
    //沿X坐标写入X标签
    for ( i = 0 ; i<= Xdivs ; i++ ) {
    x = ChartInset + ( i * ChartWidth ) / Xdivs ;
    y = ChartHeight + ChartInset ;
    myLabel = ( Xorigin + ( ScaleX * i / Xdivs ) ) . ToString ( ) ;
    g . DrawString ( myLabel , axesFont , blackBrush , x - 4 , y + 10 ) ;
    g . DrawLine ( blackPen , x , y + 2 , x , y - 2 ) ;
}
    //沿Y坐标写入Y标签
    for ( i = 0 ; i<= Ydivs ; i++ )
    {
    x = ChartInset ;
    y = ChartHeight + ChartInset - ( i * ChartHeight / Ydivs ) ;
    myLabel = ( Yorigin + ( ScaleY * i / Ydivs ) ) . ToString ( ) ;
    g . DrawString ( myLabel , axesFont , blackBrush , 5 , y - 6 ) ;
    g . DrawLine ( blackPen , x + 2 , y , x - 2 , y ) ;
    }
    g . RotateTransform ( 180 ) ;
    g . TranslateTransform ( 0 , - Height ) ;
    g . TranslateTransform ( - ChartInset , ChartInset ) ;
    g . ScaleTransform ( - 1 , 1 ) ;    
    //画出图表中的数据
    datapoint prevPoint = new datapoint ( ) ;
    prevPoint . valid = false ;
    foreach (datapoint myPoint in chartValues)
    {
        if (prevPoint.valid == true)
        {
            x0 = ChartWidth * (prevPoint.x - Xorigin) / ScaleX;
            y0 = ChartHeight * (prevPoint.y - Yorigin) / ScaleY;
            x = ChartWidth * (myPoint.x - Xorigin) / ScaleX;
            y = ChartHeight * (myPoint.y - Yorigin) / ScaleY;
            g.DrawLine(blackPen, x0, y0, x, y);
            g.FillEllipse(blackBrush, x0 - 2, y0 - 2, 4, 4);
            g.FillEllipse(blackBrush, x - 2, y - 2, 4, 4);
        }
        prevPoint = myPoint;
    }
    //最后以图片形式来浏览
    b.Save ( p.Response.OutputStream ,ImageFormat.Jpeg ) ;
    }

    LineChart ( ) {
    g . Dispose ( ) ;
    b . Dispose ( ) ;
    }
 }
 new void Page_Load ( Object sender , EventArgs e ) 
    {    
            LineChart c = new LineChart ( 400 , 300 , Page ) ;          
            c . Title = " 团购价格对比图" ;
            c.Xorigin = 0; c.ScaleX = 1000; c.Xdivs = 5;//人数
            c.Yorigin = 0; c.ScaleY = 100; c.Ydivs = 5;//价格
            c.AddValue(500, 60);
            c.AddValue(100, 70);
            c.AddValue(50, 80);
            c.AddValue(10, 100);
            c . Draw ( ) ;
    }
</script>
</head>
<body>
</body>
</html>
