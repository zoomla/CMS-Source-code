using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model.User;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Edit
{
    public partial class Statistics : System.Web.UI.Page
    {
        private B_Node bll = new B_Node();
        //B_EditWord b_EditWord = new B_EditWord();
        B_Group bGll = new B_Group();
        B_User buser = new B_User();
        public int GroupID = 0;
        public int Articles = 0;
        public int BEcount = 0;
        public int NodeBecount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            GroupID = buser.GetLogin().GroupID;
            string GroupName = bGll.GetByID(GroupID).GroupName;

            if (Request.QueryString["GID"] != null)
            {
                int gid = Convert.ToInt32(Request.QueryString["GID"]);
                int nid = Convert.ToInt32(Request.QueryString["NodeID"]);
                //节点配置文件信息
                M_Node node = this.bll.GetNodeXML(DataConverter.CLng(DataConverter.CLng(Request.QueryString["NodeID"])));

                #region 会员组包年浏览篇数
                XmlDocument Xml = new XmlDocument();
                try
                {
                    Xml.Load(Server.MapPath("/Config/Payment.xml"));
                }
                catch (Exception)
                {
                    function.WriteErrMsg("出现错误");
                }
                //读取用户组的可浏览篇数
                string GroupN = function.GetChineseFirstChar(GroupName);
                XmlNode Xn = Xml.SelectSingleNode("UserGroups/" + GroupN + "/Manner");

                //该会员组可查看篇数
                Articles = DataConverter.CLng(Xn.Attributes["Articles"].Value);
                #endregion
                //单个浏览文章信息


                //会员当月浏览总量
                //BEcount = b_EditWord.BnumCount("");
                //会员当月节点文章浏览总量
                //NodeBecount = b_EditWord.BnumCount("count(distinct(gid))| and nodeid=" + nid);
                //【当月浏览文章数】小于【该会员组可查看篇数】

                if (BEcount <= Articles)
                {
                    //无浏览记录
                    //if (Mb.ACid == 0)
                    //{//添加浏览文章
                    //    //【会员当月节点文章浏览总量】<【节点配置的会员组浏览总量】
                    //    Node(NodeBecount,node);
                    //}
                    //else
                    //{
                    //    #region 重复收费类型判断
                    //    //不重复收费
                    //    if (node.ConsumeType == 0)
                    //    {  //已看文章累加
                    //        Mb.browserNum = DataConverter.CLng(Mb.browserNum) + 1;
                    //        b_EditWord.UploadBnum(Mb);
                    //        Response.Write("0");
                    //        Response.End();
                    //    }//距离上次收费时间多少小时后重新收费
                    //    else if (node.ConsumeType == 1)
                    //    {
                    //        int Minu=b_EditWord.GetTime(Mb.browserTime.ToString("yyyy-MM-dd HH:mm"));
                    //        //单篇浏览时间超出按规定时间
                    //        if (Minu >= node.ConsumeTime * 60)
                    //        {
                    //            //【会员当月节点文章浏览总量】<【节点配置的会员组浏览总量】
                    //            Node(NodeBecount,node);
                    //        }
                    //        else  //次数累加
                    //        {
                    //            Mb.browserNum = DataConverter.CLng(Mb.browserNum) + 1;
                    //            b_EditWord.UploadBnum(Mb);
                    //            Response.Write("0");
                    //            Response.End();
                    //        }
                    //    }
                    //    //重复阅读内容多少次重新收费
                    //    else if (node.ConsumeType == 2)
                    //    {//单篇浏览次数超出规定次数
                    //        if (Mb.browserNum >= node.ConsumeCount)
                    //        {
                    //            //【会员当月节点文章浏览总量】<【节点配置的会员组浏览总量】
                    //            Node(NodeBecount, node);
                    //        }
                    //        else //次数累加
                    //        {
                    //            Mb.browserNum = DataConverter.CLng(Mb.browserNum) + 1;
                    //            b_EditWord.UploadBnum(Mb);
                    //            Response.Write("0");
                    //            Response.End();
                    //        }
                    //    }
                    //    //上述两者都满足时重新收费
                    //    else if (node.ConsumeType == 3)
                    //    {
                    //        int Minu = b_EditWord.GetTime(Mb.browserTime.ToString("yyyy-MM-dd HH:mm"));
                    //        //上述两者都满足时重新收费
                    //        if (Minu >= node.ConsumeTime * 60 && Mb.browserNum >= node.ConsumeCount)
                    //        {
                    //            //【会员当月节点文章浏览总量】<【节点配置的会员组浏览总量】
                    //            Node(NodeBecount, node);
                    //        }
                    //        else
                    //        {
                    //            Mb.browserNum = DataConverter.CLng(Mb.browserNum) + 1;
                    //            b_EditWord.UploadBnum(Mb);
                    //            Response.Write("0");
                    //            Response.End();
                    //        }
                    //    }
                    //    //上述两者任一个满足时就重新收费
                    //    else if (node.ConsumeType == 4)
                    //    {
                    //        int Minu = b_EditWord.GetTime(Mb.browserTime.ToString("yyyy-MM-dd HH:mm"));
                    //        //上述两者任一个满足时就重新收费
                    //        if (Minu >= node.ConsumeTime * 60 || Mb.browserNum >= node.ConsumeCount)
                    //        {
                    //            //【会员当月节点文章浏览总量】<【节点配置的会员组浏览总量】
                    //            Node(NodeBecount, node);
                    //        }
                    //        else
                    //        {
                    //            Mb.browserNum = DataConverter.CLng(Mb.browserNum) + 1;
                    //            b_EditWord.UploadBnum(Mb);
                    //            Response.Write("0");
                    //            Response.End();
                    //        }
                    //    }
                    //    //每阅读一次就重复收费一次（建议不要使用）
                    //    else if (node.ConsumeType == 5)
                    //    {
                    //        //【会员当月节点文章浏览总量】<【节点配置的会员组浏览总量】
                    //        Node(NodeBecount, node);
                    //    }
                    //    #endregion
                    //}
                }
                else
                {
                    Response.Write("2");
                    Response.End();
                }

            }
        }

        public bool Gn(int coo, int noid)
        {
            M_Node nod = this.bll.GetNodeXML(DataConverter.CLng(noid));
            //【会员当月节点文章浏览总量】<【节点配置的会员组浏览总量】

            if (coo < GetViewVl(nod.Viewinglimit, GroupID.ToString()))
            {
                //int nodeid = b_EditWord.GetPaNid(noid.ToString());
                ////不是顶级父节点,递归
                //if (nodeid != 0)
                //{
                //    Gn(coo, nodeid);
                //}
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Node(int co, M_Node node)
        {

            //【会员当月节点文章浏览总量】<【节点配置的会员组浏览总量】
            if (co < GetViewVl(node.Viewinglimit, GroupID.ToString()))
            {
                //int nodeid = b_EditWord.GetPaNid(Request.QueryString["NodeID"]);
                //    if (Gn(co, nodeid))
                //    {
                //        mbnum.browserTime = DateTime.Now;
                //        mbnum.Uid = DataConverter.CLng(buser.GetLogin().UserID);
                //        mbnum.browserNum = 1;
                //        mbnum.NodeID = DataConverter.CLng(Request.QueryString["NodeID"]);
                //        mbnum.Gid = DataConverter.CLng(Request.QueryString["GID"]);
                //        b_EditWord.AddBnum(mbnum);
                //        Response.Write("0");
                //        Response.End();
                //    }
                //    else
                //    {
                //        Response.Write("1");
                //        Response.End();
                //    }
            }
            else
            {
                Response.Write("1");
                Response.End();
            }
        }
        public int GetViewVl(string vimi, string Id)
        {
            string[] vimis = { };
            try
            {
                vimis = vimi.Split('|');
            }
            catch (Exception)
            {
                return 0;
            }
            string[] Valu = { };
            for (int i = 0; i < vimi.Length; i++)
            {
                Valu = vimis[i].Split('=');
                if (Id == Valu[0])
                {
                    return DataConverter.CLng(Valu[1]);
                }
            }
            return 0;
        }
    }
}