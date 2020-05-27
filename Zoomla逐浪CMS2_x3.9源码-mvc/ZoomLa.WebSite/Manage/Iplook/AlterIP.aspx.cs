using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.Iplook
{
    public partial class AlterIP : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!IsPostBack)
            {
                B_IPOperation b_ipoperation = new B_IPOperation();
                DataTable datatable = b_ipoperation.searchAllClass();
                class_ID.DataSource = datatable;
                class_ID.DataBind();
                int IP_ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                M_IP_para m_IP_para = b_ipoperation.searchIPByID(IP_ID);
                class_ID.SelectedValue = m_IP_para.class_ID.ToString();
                startIP.Text = getIP(Convert.ToInt32(m_IP_para.startIP));
                endIp.Text = getIP(Convert.ToInt32(m_IP_para.endIP));
                pro_name.Text = m_IP_para.pro_name;
                city_name.Text = m_IP_para.city_name;
            }
        }
        protected void submit_Click(object sender, EventArgs e)
        {
            B_IPOperation b_ipoperation = new B_IPOperation();
            M_IP_para m_IP_para = new M_IP_para();
            m_IP_para.IP_ID = Convert.ToInt32(Request.QueryString["ID"]);
            m_IP_para.class_ID = Convert.ToInt32(class_ID.SelectedValue);
            m_IP_para.startIP = b_ipoperation.getSumIp(startIP.Text.Trim());
            m_IP_para.endIP = b_ipoperation.getSumIp(endIp.Text.Trim());
            m_IP_para.pro_name = pro_name.Text;
            m_IP_para.city_name = city_name.Text;
            b_ipoperation.updateIP(m_IP_para);
        }
        private string getIP(int sumIP)
        {
            string IP = "";
            int ip2 = sumIP;
            int split2 = 3;
            int IPnode = sumIP;
            for (int j = 3; j >= 0; j--)
            {
                IPnode = Convert.ToInt32(ip2 / Math.Pow(255, split2));
                if (split2 == 0)
                {
                    IP = IP + IPnode;
                }
                else
                {
                    IP = IP + IPnode + ".";
                }

                ip2 = Convert.ToInt32(ip2 % Math.Pow(255, split2));
                split2--;
            }
            return IP;
        }
    }
}