using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// 添加命名空间
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.IO;
//using jmail;
using System.Text;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
namespace ZoomLaCMS.MIS.Mail
{
    public partial class Default : System.Web.UI.Page
    {
        // 定义邮件发送类
        protected SmtpClient smtpClient;
        protected TcpClient tcpClient;
        protected NetworkStream networkStream;
        protected StreamReader streamReader;
        protected StreamWriter streamWriter;

        // 定义接收邮件对象
        protected POP3Class popClient;

        // 定义邮件信息接口
        protected jmail.Message messageMail;

        // 定义邮件附件集合接口
        protected Attachments attachments;

        // 定义邮件附件接口
        protected jmail.Attachment attachment;
        B_MailSet bll = new B_MailSet();
        M_MailSet model = new M_MailSet();
        B_User buser = new B_User();
        int Eid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            buser.CheckIsLogin("/Mis/Mail/");
            DataTable dts = new DataTable();
            // 界面控件控制  
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request["Eid"]))
                {
                    Eid = DataConvert.CLng(Request["Eid"]);
                    dts = bll.Sel(Eid);
                }
                else
                {
                    dts = bll.SelByUid(buser.GetLogin().UserID, 1);
                }
                if (dts != null && dts.Rows.Count > 0)
                {
                    // 初始化界面
                    tbxUserMail.Value = dts.Rows[0]["UserMail"].ToString();
                    txbPassword.Value = dts.Rows[0]["Password"].ToString();
                    tbxSmtpServer.Value = dts.Rows[0]["SMTP"].ToString();
                    tbxPOP3Server.Value = dts.Rows[0]["POP3"].ToString();
                    getlogin();
                }
            }
        }
        protected void getlogin()
        {
            DataTable dt = new DataTable();


            //加载时登录
            // 与POP3服务器建立TCP连接
            // 建立连接后把服务器上的邮件下载到本地
            // 设置当前界面的光标为等待光标（就是我们看到的一个动的圆形）
            //lsttbxStatus.Items.Clear();
            txtMsgList.Text = "";

            // POP3服务器通过监听TCP110端口来提供POP3服务的
            // 向POP3服务器发出tcp请求 
            try
            {
                tcpClient = new TcpClient(tbxPOP3Server.Value, 110);
                txtMsgList.Text = "正在连接.. \n";
                //lsttbxStatus.Items.Add("正在连接...");
            }
            catch
            {
                // Response.Write("连接失败");
                //MessageBox.Show("连接失败", "错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                txtMsgList.Text += "连接失败.. \n"; //lsttbxStatus.Items.Add("连接失败！");
                return;
            }

            // 连接成功的情况
            networkStream = tcpClient.GetStream();
            streamReader = new StreamReader(networkStream, Encoding.Default);
            streamWriter = new StreamWriter(networkStream, Encoding.Default);
            streamWriter.AutoFlush = true;
            string str;
            // 读取服务器返回的响应连接信息
            str = GetResponse();
            if (CheckResponse(str) == false)
            {
                txtMsgList.Text += "服务器拒接了连接请求 \n";
                // lsttbxStatus.Items.Add("服务器拒接了连接请求");
                return;
            }
            // 如果服务器接收请求
            // 向服务器发送凭证——用户名和密码
            // 向服务器发送用户名，请求确认

            txtMsgList.Text += "核实用户名阶段.. \n";
            // lsttbxStatus.Items.Add("核实用户名阶段...");
            SendToServer("USER " + tbxUserMail.Value);
            str = GetResponse();
            if (CheckResponse(str) == false)
            {
                txtMsgList.Text += "用户名错误 \n";
                //lsttbxStatus.Items.Add("用户名错误.");
                return;
            }

            // 用户名审核通过后再发送密码等待确认
            // 向服务器发送密码，请求确认
            SendToServer("PASS " + txbPassword.Value);
            str = GetResponse();
            if (CheckResponse(str) == false)
            {
                txtMsgList.Text += "密码错误 \n";
                //  lsttbxStatus.Items.Add("密码错误！");
                return;
            }

            txtMsgList.Text += "身份验证成功，可以开始会话 \n";
            //lsttbxStatus.Items.Add("身份验证成功，可以开始会话");
            // 向服务器发送LIST 命令，请求获得邮件列表和大小
            txtMsgList.Text += "获取邮件.... \n";
            //lsttbxStatus.Items.Add("获取邮件....");
            SendToServer("LIST");
            str = GetResponse();
            if (CheckResponse(str) == false)
            {
                txtMsgList.Text += "获取邮件列表失败 \n";
                //lsttbxStatus.Items.Add("获取邮件列表失败");
                return;
            }
            txtMsgList.Text += "邮件获取成功 \n";
            // lsttbxStatus.Items.Add("邮件获取成功");

            // 窗口控件控制  
            // 登录成功后实例化邮件发送对象，以便后面完成发送邮件的操作
            // 实例化邮件发送类（SmtpClient）对象
            if (smtpClient == null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Host = tbxSmtpServer.Value; //tbxSmtpServer.Text;
                smtpClient.Port = 25;

                // 不使用默认凭证，即需要认证登录
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(tbxUserMail.Value, txbPassword.Value);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            }

            // 登录成功后，自动接收新邮件
            // 开始接收邮件 
            try
            {

                if (!string.IsNullOrEmpty(Request["type"]))
                {
                    if (Request["type"] == "no")
                    {
                        M_MailInfo mm = new M_MailInfo();
                        B_MailInfo bm = new B_MailInfo();
                        dt = bm.SelByStatus(0);
                        Page_list(dt);
                    }
                    else if (Request["type"] == "yes")
                    {
                        M_MailInfo mm = new M_MailInfo();
                        B_MailInfo bm = new B_MailInfo();
                        dt = bm.SelByStatus(1);
                        Page_list(dt);
                    }
                    else if (Request["type"] == "del")
                    {
                        M_MailInfo mm = new M_MailInfo();
                        B_MailInfo bm = new B_MailInfo();
                        dt = bm.SelByStatus(-1);
                        Page_list(dt);
                    }
                }
                else
                {
                    AddRow();
                }
            }
            catch
            {
                txtMsgList.Text += "读取邮件列表失败! \n";
                // MessageBox.Show("读取邮件列表失败！", "错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            txtMsgList.Text += "登录成功! \n";
        }

        #region 处理与POP3服务器交互事件
        // 获取服务器响应的信息
        protected string GetResponse()
        {
            string str = null;
            try
            {
                str = streamReader.ReadLine();
                if (str == null)
                {

                    txtMsgList.Text += "连接失败——服务器没有响应 \n";
                }
                else
                {
                    txtMsgList.Text += "收到：[" + str + "] \n";
                    if (str.StartsWith("-ERR"))
                    {
                        str = null;
                    }
                }
            }
            catch (Exception err)
            {
                txtMsgList.Text += "连接失败：[" + err.Message + "]\n";
                //lsttbxStatus.Items.Add("连接失败：[" + err.Message + "]");
            }

            return str;
        }

        // 检查响应信息
        protected bool CheckResponse(string responseString)
        {
            if (responseString == null)
            {
                return false;
            }
            else
            {
                if (responseString.StartsWith("+OK"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // 向服务器发送命令
        protected bool SendToServer(string str)
        {
            try
            {
                // 这里必须使用WriteLine方法的，因为POP3协议中定义的命令是以回车换行结束的
                // 如果客户端发送的命令没有以回车换行结束，POP3服务器就不能识别，也就不能响应客户端的请求了
                // 如果想用Write方法，则str输入的参数字符中必须包含“\r\n”,也就是回车换行字符串。
                streamWriter.WriteLine(str);
                streamWriter.Flush();

                txtMsgList.Text += "发送：[" + str + "]\n";
                //lsttbxStatus.Items.Add("发送：[" + str + "]");
                return true;
            }
            catch (Exception ex)
            {
                txtMsgList.Text += "发送失败：[" + ex.Message + "]\n";
                // lsttbxStatus.Items.Add("发送失败：[" + ex.Message + "]");
                return false;
            }
        }

        #endregion

        // 退出登录
        protected void btnLogout_Click_1(object sender, EventArgs e)
        {
            // 断开与POP3服务器的TCP连接
            txtMsgList.Text += "结束会话，进入更新状态...\n";
            // lsttbxStatus.Items.Add("结束会话，进入更新状态...");
            SendToServer("QUIT");
            txtMsgList.Text += "正在关闭连接...\n";
            //lsttbxStatus.Items.Add("正在关闭连接...");
            streamReader.Close();
            streamWriter.Close();
            networkStream.Close();
            tcpClient.Close();

            // SmtpClient 对象销毁
            if (smtpClient != null)
            {
                // smtpClient.Dispose();
                smtpClient.SendAsyncCancel();
            }

            // POP3Class 对象销毁
            // Dimac官网上下载的免费版的Jmail组件
            // 在调用Connect方法和Disconnect出现说这个方法没在该组件中定义的错误
            // 后面通过网上的答案说下载破解版的Jmail 4.4 Pro后解决了这个问题的。
            popClient.Disconnect();
            // lstViewMailList.Items.Clear();

            txtMsgList.Text += "退出登录.\n";
            tbxMailboxInfo.Text = "";
        }

        #region 邮件操作


        //// 回复邮件
        //protected void btnReplyCurrentMail_Click(object sender, EventArgs e)
        //{
        //    //int index = lstViewMailList.SelectedItems[0].Index;
        //    int index = lstViewMailList.SelectedIndex;
        //    messageMail = popClient.Messages[index + 1];

        //    // 使写信选项卡成为当前选项卡
        //    //  tabControlMyMailbox.SelectTab(tabPageWriteLetter);
        //    //txbSendTo.Text = lstViewMailList.SelectedItems[0].SubItems[1].Text;
        //    txbSendTo.Value = lstViewMailList.SelectedItem.Text;
        //    txbSubject.Value = "Re:" + messageMail.Subject;
        //    richtbxBody.Value = "";
        //    richtbxBody.Focus();
        //}


        ////// 删除邮件
        //protected void btnDeleteMail_Click(object sender, EventArgs e)
        //{
        ////    //if (lstViewMailList.SelectedItems.Count == 0)
        ////    //{
        ////    //    MessageBox.Show("请先选择阅读的邮件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ////    //    return;
        ////    //}

        ////    //int index = lstViewMailList.SelectedItems[0].Index;
        ////    //messageMail = popClient.Messages[index + 1];
        ////    //if (MessageBox.Show("确认要删除邮件" + messageMail.Subject + "吗？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        ////    //{
        ////    //    popClient.DeleteSingleMessage(index + 1);
        ////    //    popClient.Disconnect();
        ////    //    btnRefreshMailList.PerformClick();
        ////    //}
        //}

        // 刷新邮件列表
        protected void btnRefreshMailList_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        //// 删除当前预览邮件--
        //protected void btnDelCurrentMail_Click(object sender, EventArgs e)
        //{
        //    //int index = lstViewMailList.SelectedItems[0].Index;
        //    //messageMail = popClient.Messages[index + 1];
        //    //if (MessageBox.Show("确认要删除邮件" + messageMail.Subject + "吗？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //    //{
        //    //    popClient.DeleteSingleMessage(index + 1);
        //    //    tbxMailboxInfo.Text = "删除了主题为“"+messageMail.Subject+"”的邮件";
        //    //    popClient.Disconnect();
        //    //    richtbxMailContentReview.Text = "";
        //    //    btnReplyCurrentMail.Enabled = false;
        //    //    btnDelCurrentMail.Enabled = false;
        //    //}
        //}
        #endregion

        protected void AddRow()
        {
            // 实例化邮件接收类POP3Class
            popClient = new POP3Class();

            DataTable dts = new DataTable();
            dts = bll.SelByUid(buser.GetLogin().UserID);
            if (dts != null && dts.Rows.Count > 0)
            {
                // 连接服务器
                popClient.Connect(dts.Rows[0]["UserMail"].ToString(), dts.Rows[0]["Password"].ToString(), dts.Rows[0]["POP3"].ToString(), 110);
                // 连接服务器
                //  popClient.Connect(tbxUserMail.Value, txbPassword.Value, tbxPOP3Server.Value, 110);
                if (popClient != null)
                {
                    if (popClient.Count > 0)
                    {
                        tbxMailboxInfo.Text = popClient.Count.ToString();
                        ENum.Text = popClient.Count.ToString();
                        DataTable dt = new DataTable("NodeTree");//
                        DataColumn myDataColumn;
                        DataRow myDataRow;

                        myDataColumn = new DataColumn();
                        myDataColumn.DataType = System.Type.GetType("System.Int32");
                        myDataColumn.ColumnName = "ID";
                        dt.Columns.Add(myDataColumn);

                        myDataColumn = new DataColumn();
                        myDataColumn.DataType = System.Type.GetType("System.String");
                        myDataColumn.ColumnName = "From";
                        dt.Columns.Add(myDataColumn);

                        myDataColumn = new DataColumn();
                        myDataColumn.DataType = System.Type.GetType("System.String");
                        myDataColumn.ColumnName = "Subject";
                        dt.Columns.Add(myDataColumn);

                        myDataColumn = new DataColumn();
                        myDataColumn.DataType = System.Type.GetType("System.DateTime");
                        myDataColumn.ColumnName = "Time";
                        dt.Columns.Add(myDataColumn);

                        myDataColumn = new DataColumn();
                        myDataColumn.DataType = System.Type.GetType("System.String");
                        myDataColumn.ColumnName = "Count";
                        dt.Columns.Add(myDataColumn);
                        for (int i = 0; i < popClient.Count; i++)
                        {
                            messageMail = popClient.Messages[i + 1];
                            myDataRow = dt.NewRow();
                            myDataRow["ID"] = i;
                            myDataRow["From"] = messageMail.From;
                            myDataRow["Subject"] = messageMail.Subject;
                            myDataRow["Time"] = messageMail.Date;
                            attachments = messageMail.Attachments;
                            if (attachments.Count > 0)
                            {
                                myDataRow["Count"] = attachments.Count.ToString();
                            }
                            else
                            {
                                myDataRow["Count"] = "0";
                            }
                            dt.Rows.Add(myDataRow);


                            //ListViewItem item = new ListViewItem();
                            //item.SubItems.Add(messageMail.From);
                            //item.SubItems.Add(messageMail.Subject);
                            //attachments = messageMail.Attachments;
                            //if (attachments.Count > 0)
                            //{
                            //    item.SubItems.Add(attachments.Count.ToString());
                            //}
                            //else
                            //{
                            //    item.SubItems.Add("无");
                            //}

                            //item.SubItems.Add(messageMail.Date.ToString());
                            //lstViewMailList.Items.Add(item);

                        }
                        dt.DefaultView.Sort = "Time desc";
                        Page_list(dt);

                    }
                }
            }
        }

        #region 通用分页过程
        /// <summary>
        /// 通用分页过程　by h.
        /// </summary>
        /// <param name="Cll"></param>
        public void Page_list(DataTable Cll)
        {
            RPT.DataSource = Cll;
            RPT.DataBind();
        }
        #endregion
        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "View")
            {
                // 实例化邮件接收类POP3Class
                popClient = new POP3Class();

                // 连接服务器
                popClient.Connect(tbxUserMail.Value, txbPassword.Value, tbxPOP3Server.Value, 110);
                if (popClient != null)
                {
                    if (popClient.Count > 0)
                    {
                        int index = id;
                        messageMail = popClient.Messages[index + 1];
                        // richtbxMailContentReview.Text = messageMail.Body;
                    }
                }
            }

            if (e.CommandName == "Del")
            {
                //    //int index = lstViewMailList.SelectedItems[0].Index;
                //    //messageMail = popClient.Messages[index + 1];
                //    //if (MessageBox.Show("确认要删除邮件" + messageMail.Subject + "吗？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //    //{
                //    //    popClient.DeleteSingleMessage(index + 1);
                //    //    popClient.Disconnect();
                //    //    btnRefreshMailList.PerformClick();
                //    //}
            }
        }
        protected string getfile(int count)
        {
            if (count > 0)
            {
                return "<b class='mail_ico_le mail_annex_ico'> </b>";
            }
            return "";
        }
    }
}