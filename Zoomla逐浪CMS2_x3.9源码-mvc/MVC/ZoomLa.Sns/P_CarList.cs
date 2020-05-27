using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Sns
{
    /// <summary>
    ///P_carlistҵ��ʵ��
    /// </summary>
    [Serializable]
    public class P_CarList
    {
        #region �ֶζ���

        ///<summary>
        ///
        ///</summary>
        private int pid;

        ///<summary>
        ///����
        ///</summary>
        private string p_car_name = String.Empty;

        ///<summary>
        ///������
        ///</summary>
        private int p_car_num;

        ///<summary>
        ///
        ///</summary>
        private int p_car_surplus;

        ///<summary>
        ///���۸�
        ///</summary>
        private int p_car_money;

        ///<summary>
        ///�������
        ///</summary>
        private int p_car_old;

        ///<summary>
        ///��ͼƬ
        ///</summary>
        private string p_car_img = String.Empty;

        ///<summary>
        ///��Logo
        ///</summary>
        private string p_car_img_logo = String.Empty;

        ///<summary>
        ///������
        ///</summary>
        private string p_car_content = String.Empty;

        ///<summary>
        ///��״̬
        ///</summary>
        private int p_car_check;


        #endregion

        #region ���캯��

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

        #region ���Զ���

        ///<summary>
        ///
        ///</summary>
        public int Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        ///<summary>
        ///����
        ///</summary>
        public string P_car_name
        {
            get { return p_car_name; }
            set { p_car_name = value; }
        }

        ///<summary>
        ///������
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
        ///���۸�
        ///</summary>
        public int P_car_money
        {
            get { return p_car_money; }
            set { p_car_money = value; }
        }

        ///<summary>
        ///�������
        ///</summary>
        public int P_car_old
        {
            get { return p_car_old; }
            set { p_car_old = value; }
        }

        ///<summary>
        ///��ͼƬ
        ///</summary>
        public string P_car_img
        {
            get { return p_car_img; }
            set { p_car_img = value; }
        }

        ///<summary>
        ///��Logo
        ///</summary>
        public string P_car_img_logo
        {
            get { return p_car_img_logo; }
            set { p_car_img_logo = value; }
        }

        ///<summary>
        ///������
        ///</summary>
        public string P_car_content
        {
            get { return p_car_content; }
            set { p_car_content = value; }
        }

        ///<summary>
        ///��״̬
        ///</summary>
        public int P_car_check
        {
            get { return p_car_check; }
            set { p_car_check = value; }
        }

        #endregion

    }
}
