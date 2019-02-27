using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;

public partial class User_Profile_Accountinfo : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Accountinfo baccount = new B_Accountinfo();
    private B_PayPlat bpay = new B_PayPlat();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Getddpay(); 
            lblTip.Visible = false;
            M_Accountinfo acc = baccount.GetSelectByuserId(buser.GetLogin().UserID);
            ViewState["id"] = acc.id;
            if (acc != null && acc.id > 0)
            {
                txtBank.Text = acc.BankOfDeposit;
                txtOfName.Text = acc.NameOfDeposit;
                txtAccount.Text = acc.Account;
                txtName.Text = acc.Name;
                txtCardID.Text = acc.CardID;
                hfLockId.Value = acc.Lock.ToString();
                ddpay.SelectedValue = acc.PayId.ToString();
                if (acc.Lock == 1)  //绑定
                {
                    btnLock.Visible = false;
                    txtName.Enabled = false;
                    divlock.Visible = true;
                }
                else
                {
                    btnLock.Visible = true;
                    txtName.Enabled = true;
                    divlock.Visible = false;
                }
            }
            else
            {
                btnLock.Visible = true;
                divlock.Visible = false;
            }
        }
    }

    private void Getddpay()
    {
        DataTable dt = bpay.GetPayPlatAll();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddpay.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListItem li = new ListItem();
                li.Text = dr["PayPlatName"].ToString();
                li.Value = dr["PayPlatID"].ToString();
                ddpay.Items.Add(li);
            }
        }
    }
    //绑定真实姓名
    protected void btnLock_Click(object sender, EventArgs e)
    {
        bool result = false;

        if (ViewState["id"] != null && DataConverter.CLng(ViewState["id"])>0)
        {
            int id = DataConverter.CLng(ViewState["id"]);
            result = baccount.GetUpdate(id, 1, txtName.Text.Trim());
        }
        else
        {
            M_Accountinfo account = new M_Accountinfo();
            account.BankOfDeposit = txtBank.Text.Trim();
            account.NameOfDeposit = txtOfName.Text.Trim();
            account.Name = txtName.Text.Trim();
            account.Account = txtAccount.Text.Trim();
            account.CardID = txtCardID.Text.Trim();
            account.UserId = buser.GetLogin().UserID;
            account.PayId =DataConverter.CLng(ddpay.SelectedValue);
            account.Lock = 1;
            int id = baccount.GetInsert(account);
            result = id > 0 ? true : false;
        }
        if (result)
        {
            hfLockId.Value = "1";
            btnLock.Visible = false;
            txtName.Enabled = false;
            divlock.Visible = true;
        }
        else
        {
            hfLockId.Value = "0";
        }

    }

    //修改
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_Accountinfo account = new M_Accountinfo();
        if (ViewState["id"] != null && DataConverter.CLng(ViewState["id"]) > 0)
        {
            int id = DataConverter.CLng(ViewState["id"]);
            account = baccount.GetSelect(id);
            account.BankOfDeposit = txtBank.Text.Trim();
            account.NameOfDeposit = txtOfName.Text.Trim();
            account.Name = txtName.Text.Trim();
            account.Account = txtAccount.Text.Trim();
            account.CardID = txtCardID.Text.Trim();
            account.PayId = DataConverter.CLng(ddpay.SelectedValue);
            bool result = baccount.GetUpdate(account);
            if (result)
            {
                lblTip.Text = "修改成功!";
                lblTip.Visible = true;
            }
            else
            {
                lblTip.Text = "修改失败!";
                lblTip.Visible = true;
            }
        }
        else
        {
            account.BankOfDeposit = txtBank.Text.Trim();
            account.NameOfDeposit = txtOfName.Text.Trim();
            account.Name = txtName.Text.Trim();
            account.Account = txtAccount.Text.Trim();
            account.UserId = buser.GetLogin().UserID;
            account.CardID = txtCardID.Text.Trim();
            account.PayId = DataConverter.CLng(ddpay.SelectedValue);
            account.Lock = DataConverter.CLng(hfLockId.Value);
            int aid = baccount.GetInsert(account);
            ViewState["id"] = aid;
            if (aid > 0)
            {
                lblTip.Text = "保存成功!";
                lblTip.Visible = true;
            }
            else
            {
                lblTip.Text = "保存失败!";
                lblTip.Visible = true;
            }
        }
    }

}
