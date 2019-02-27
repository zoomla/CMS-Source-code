using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_DaiGou_Default : System.Web.UI.Page
{
    /// <summary>
    /// 商品单价
    /// </summary>
    public double proMoney = 0.0;
    /// <summary>
    /// 商品重量
    /// </summary>
    public double proHeight = 0.0;
    /// <summary>
    /// 商品运费
    /// </summary>
    public double proFee = 0.0;
    /// <summary>
    /// 服务费
    /// </summary>
    public double proser = 0.0;
    /// <summary>
    /// 商品总计
    /// </summary>
    public double proAllMoney = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void feeBtn_Click(object sender, EventArgs e)
    {
        proMoney = Convert.ToDouble(this.txtProMoney.Text);
        proHeight = Convert.ToDouble(this.txtProHeight.Text);
        /*
         * 服务费:
         *0－500元的货品：50元 
         * 501－1000元的货品，10% 
         * 1001－2000元的货品，9% 
         * 2001－3000元的货品，8% 
         * 3000元以上的货品，7% 
         */
        if (proMoney > 0 && proMoney <= 500)
        {
            proser = 50;
        }
        else if (proMoney >= 501 && proMoney <= 1000)
        {
            proser = proMoney * 0.1;
        }
        else if (proMoney >= 1001 && proMoney <= 2000)
        {
            proser = proMoney * 0.09;
        }
        else if (proMoney >= 2001 && proMoney <= 3000)
        {
            proser = proMoney * 0.08;
        }
        else if (proMoney >= 3000)
        {
            proser = proMoney * 0.07;
        }

        /* 国际运费:
        *0-500G:110元
        *以后,每增加500G,增加40元，一直到5000G
        *5000G以上, 每增加1000G,增加50元
         *
         * 国内运费:
         * 0-1000G:15元
         * 以后,每增加1000G,增加10元 
        */
        //国内运费
        if (DropDownList1.SelectedValue=="13")
        {
            if (proHeight > 0 && proHeight<=1000)
            {
                proFee = 15;
            }
            else if (proHeight>1000)
            {
                if ((proHeight - 1000) % 1000 == 0)
                {
                    proFee = 15 + ((int)(proHeight - 1000) / 1000) * 10;
                }
                else {
                    proFee = 15 + ((int)(proHeight - 1000) / 1000) * 10 + 10;
                }                
            }
        }
        //国际运费

        //美国、加拿大
        else if (DropDownList1.SelectedValue == "1" )
        {
            if (proHeight > 0 && proHeight <= 500)
            {//0-500G 运费120元
                proFee = 120;               
            }
            else if (proHeight > 500)
            {//增加500G 运费加37.5元
                if((proHeight-500)%500==0){

                    proFee=120+((int)(proHeight-500)/500) * 37.5;
                }
                else
                {
                    proFee=120+((int)(proHeight-500)/500) *37.5 +37.5;
                   // Response.Write(""+Convert.ToString((proHeight - 500) / 500) + "<br> 多少加费："+ (proHeight - 500)+"<br>");
                }
            }

        }
        //法国,英国,荷兰,爱尔兰,德国,西欧
        else if (DropDownList1.SelectedValue == "3" ||DropDownList1.SelectedValue == "2" || DropDownList1.SelectedValue == "4" ||
            DropDownList1.SelectedValue == "6" || DropDownList1.SelectedValue == "10")
        {
            if (proHeight > 0 && proHeight <= 500)
            {//0-500G 运费140元
                proFee = 140;
            }
            else if (proHeight > 500)
            {//增加500G 运费增加37.5
                if ((proHeight - 500) % 500 == 0)
                {

                    proFee = 140 + ((int)(proHeight - 500) / 500) * 37.5;
                }
                else
                {
                    proFee = 140 + ((int)(proHeight - 500) / 500) * 37.5 + 37.5;
                }
            }

        }
         // 澳大利亚,新西兰,东南亚
        else if (DropDownList1.SelectedValue == "7" || DropDownList1.SelectedValue == "8"
            || DropDownList1.SelectedValue == "11")
        {
            if (proHeight > 0 && proHeight <= 500)
            {//0-500G 运费105元
                proFee = 105;
            }
            else if (proHeight > 500)
            {//增加500G 运费增加27.5
                if ((proHeight - 500) % 500 == 0)
                {

                    proFee = 105 + ((int)(proHeight - 500) / 500) * 27.5;
                }
                else
                {
                    proFee = 105 + ((int)(proHeight - 500) / 500) * 27.5 + 27.5;
                }
            }
        }
        else if (DropDownList1.SelectedValue == "0")
        {
            Response.Write("<script>alert('请选择运送地区') </script>");   
        }
        //其他国家
        else 
        {
            if (proHeight > 0 && proHeight <= 500)
            {
                proFee = 227.5;
            }
            else if (proHeight > 500)
            {
                if ((proHeight - 500) % 500 == 0)
                {

                    proFee = 227.5 + ((int)(proHeight - 500) / 500) * 60;
                }
                else
                {
                    proFee = 227.5 + ((int)(proHeight - 500) / 500) * 60 + 60;
                }
            }


        }

#region 改动前
        //else {
        //        if (proHeight > 0 && proHeight <= 500)
        //        {
        //            proFee = 110;
        //        }
        //        else if (proHeight > 500 && proHeight <= 5000)
        //        {
        //            if ((proHeight - 500) % 500 == 0)
        //            {
        //                proFee = 110 + Convert.ToInt32((proHeight - 500) / 500) * 40;
        //            }
        //            else {
        //                proFee = 110 + Convert.ToInt32((proHeight - 500) / 500) * 40 + 40;
        //            }
        //        }
        //        else if (proHeight > 5000)
        //        {
        //            if ((proHeight - 5000) % 1000 == 0)
        //            {
        //                proFee = 110 + (5000 - 500) / 500 * 40 + Convert.ToInt32((proHeight - 5000) / 1000) * 50;
        //            }
        //            else {
        //                proFee = 110 + (5000 - 500) / 500 * 40 + Convert.ToInt32((proHeight - 5000) / 1000) * 50 + 50;
        //            }                   
        //        }        
        //}
        #endregion
        //费用估算=商品价格+服务费+运费
        proAllMoney = proMoney + proser + proFee;
       // Response.Write("服务费:"+proser + "------<br>运费:" + proFee);
        test.Style["display"] = "block";
    }
}
