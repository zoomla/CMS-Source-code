using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Data;

public partial class manage_User_NodeScript : CustomerPageAction
{
    protected B_Node nll = new B_Node();
    protected B_UserPromotions ups = new B_UserPromotions();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable nodelist = nll.Sel();
            this.NodeList.DataSource = nodelist;
            this.NodeList.DataTextField = "NodeName";
            this.NodeList.DataValueField = "NodeID";
            this.NodeList.DataBind();
        }

        if (Request.QueryString["pid"] != null)
        {
            Button1.Text = "修改";
            string pid = Request.QueryString["pid"].ToString();
            string GroupIDs = Request.QueryString["GroupID"].ToString();
            if (pid != "")
            {
                if (pid.IndexOf("|") > -1)
                {
                    string[] ppidarr = pid.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ppidarr.Length; i++)
                    {
                        for (int c = 0; c < this.NodeList.Items.Count; c++)
                        {
                            if (this.NodeList.Items[c].Value == ppidarr[i].ToString())
                            {
                                this.NodeList.Items[c].Selected = true;
                            }

                            //M_UserPromotions pinfos = ups.GetSelect(DataConverter.CLng(this.NodeList.Items[c].Value), DataConverter.CLng(GroupIDs));
                            //for (int ii = 0; ii < CheckBoxList1.Items.Count; ii++)
                            //{
                            //    if (pinfos.look == 1)
                            //    {
                            //        CheckBoxList1.Items[0].Selected = true;
                            //    }

                            //    if (pinfos.addTo == 1)
                            //    {
                            //        CheckBoxList1.Items[1].Selected = true;
                            //    }

                            //    if (pinfos.Modify == 1)
                            //    {
                            //        CheckBoxList1.Items[2].Selected = true;
                            //    }

                            //    if (pinfos.Deleted == 1)
                            //    {
                            //        CheckBoxList1.Items[3].Selected = true;
                            //    }

                            //    if (pinfos.Columns == 1)
                            //    {
                            //        CheckBoxList1.Items[4].Selected = true;
                            //    }

                            //    if (pinfos.Comments == 1)
                            //    {
                            //        CheckBoxList1.Items[5].Selected = true;
                            //    }
                            //}
                        }
                    }
                }
                else
                {
                    int ppid = DataConverter.CLng(pid);
                    for (int c = 0; c < this.NodeList.Items.Count; c++)
                    {
                        if (this.NodeList.Items[c].Value == ppid.ToString())
                        {
                            this.NodeList.Items[c].Selected = true;
                            M_UserPromotions pinfos = ups.GetSelect(ppid, DataConverter.CLng(GroupIDs));
                            for (int ii = 0; ii < CheckBoxList1.Items.Count; ii++)
                            {
                                if (pinfos.look == 1)
                                {
                                    CheckBoxList1.Items[0].Selected = true;
                                }

                                if (pinfos.addTo == 1)
                                {
                                    CheckBoxList1.Items[1].Selected = true;
                                }

                                if (pinfos.Modify == 1)
                                {
                                    CheckBoxList1.Items[2].Selected = true;
                                }

                                if (pinfos.Deleted == 1)
                                {
                                    CheckBoxList1.Items[3].Selected = true;
                                }

                                if (pinfos.Columns == 1)
                                {
                                    CheckBoxList1.Items[4].Selected = true;
                                }

                                if (pinfos.Comments == 1)
                                {
                                    CheckBoxList1.Items[5].Selected = true;
                                }
                            }

                        }
                    }
                }
            }
        }
        else {
            Button1.Text = "添加";
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.NodeList.Items.Count; i++)
        {
            if (this.NodeList.Items[i].Selected == true)
            {
                M_UserPromotions upsinfo = new M_UserPromotions();
                upsinfo.GroupID = DataConverter.CLng(Request.QueryString["GroupID"]);

                if (this.CheckBoxList1.Items[0].Selected == true)
                {
                    upsinfo.look = 1;
                }
                else
                {
                    upsinfo.look = 0;
                }

                if (this.CheckBoxList1.Items[1].Selected == true)
                {
                    upsinfo.addTo = 1;
                }
                else
                {
                    upsinfo.addTo = 0;
                }

                if (this.CheckBoxList1.Items[2].Selected == true)
                {
                    upsinfo.Modify = 1;
                }
                else
                {
                    upsinfo.Modify = 0;
                }
                if (this.CheckBoxList1.Items[3].Selected == true)
                {
                    upsinfo.Deleted = 1;
                }
                else
                {
                    upsinfo.Deleted = 0;
                }
                if (this.CheckBoxList1.Items[4].Selected == true)
                {
                    upsinfo.Columns = 1;
                }
                else
                {
                    upsinfo.Columns = 0;
                }
                if (this.CheckBoxList1.Items[5].Selected == true)
                {
                    upsinfo.Comments = 1;
                }
                else
                {
                    upsinfo.Comments = 0;
                }
                upsinfo.NodeID = DataConverter.CLng(this.NodeList.Items[i].Value);
                ups.GetInsertOrUpdate(upsinfo);

            }
        }
        function.Script(this,"alert('设置成功!');GotoWirteddd();");
    }
}
