using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///P_carlist业务实体
    /// </summary>
    [Serializable]
    public class P_CarList
    {
        #region 字段定义

        ///<summary>
        ///
        ///</summary>
        private int pid;

        ///<summary>
        ///车名
        ///</summary>
        private string p_car_name = String.Empty;

        ///<summary>
        ///车数量
        ///</summary>
        private int p_car_num;

        ///<summary>
        ///
        ///</summary>
        private int p_car_surplus;

        ///<summary>
        ///车价格
        ///</summary>
        private int p_car_money;

        ///<summary>
        ///车耗损度
        ///</summary>
        private int p_car_old;

        ///<summary>
        ///车图片
        ///</summary>
        private string p_car_img = String.Empty;

        ///<summary>
        ///车Logo
        ///</summary>
        private string p_car_img_logo = String.Empty;

        ///<summary>
        ///车介绍
        ///</summary>
        private string p_car_content = String.Empty;

        ///<summary>
        ///车状态
        ///</summary>
        private int p_car_check;


        #endregion

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public P_CarList()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public P_CarList
        (
            int pid,
            string p_car_name,
            int p_car_num,
            int p_car_surplus,
            int p_car_money,
            int p_car_old,
            string p_car_img,
            string p_car_img_logo,
            string p_car_content,
            int p_car_check
        )
        {
            this.pid = pid;
            this.p_car_name = p_car_name;
            this.p_car_num = p_car_num;
            this.p_car_surplus = p_car_surplus;
            this.p_car_money = p_car_money;
            this.p_car_old = p_car_old;
            this.p_car_img = p_car_img;
            this.p_car_img_logo = p_car_img_logo;
            this.p_car_content = p_car_content;
            this.p_car_check = p_car_check;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public int Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        ///<summary>
        ///车名
        ///</summary>
        public string P_car_name
        {
            get { return p_car_name; }
            set { p_car_name = value; }
        }

        ///<summary>
        ///车数量
        ///</summary>
        public int P_car_num
        {
            get { return p_car_num; }
            set { p_car_num = value; }
        }

        ///<summary>
        ///
        ///</summary>
        public int P_car_surplus
        {
            get { return p_car_surplus; }
            set { p_car_surplus = value; }
        }

        ///<summary>
        ///车价格
        ///</summary>
        public int P_car_money
        {
            get { return p_car_money; }
            set { p_car_money = value; }
        }

        ///<summary>
        ///车耗损度
        ///</summary>
        public int P_car_old
        {
            get { return p_car_old; }
            set { p_car_old = value; }
        }

        ///<summary>
        ///车图片
        ///</summary>
        public string P_car_img
        {
            get { return p_car_img; }
            set { p_car_img = value; }
        }

        ///<summary>
        ///车Logo
        ///</summary>
        public string P_car_img_logo
        {
            get { return p_car_img_logo; }
            set { p_car_img_logo = value; }
        }

        ///<summary>
        ///车介绍
        ///</summary>
        public string P_car_content
        {
            get { return p_car_content; }
            set { p_car_content = value; }
        }

        ///<summary>
        ///车状态
        ///</summary>
        public int P_car_check
        {
            get { return p_car_check; }
            set { p_car_check = value; }
        }

        #endregion

    }
}
