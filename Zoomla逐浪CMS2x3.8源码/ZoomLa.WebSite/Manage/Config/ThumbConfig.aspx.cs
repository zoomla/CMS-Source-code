using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class Manage_I_Config_ThumbConfig : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!IsPostBack)
        {
            Isuse.Checked = WaterModuleConfig.WaterConfig.IsUsed;
            ThumbWidth_T.Text = SiteConfig.ThumbsConfig.ThumbsWidth.ToString();
            ThumbHeight_T.Text = SiteConfig.ThumbsConfig.ThumbsHeight.ToString();

            RadioButtonList1.SelectedValue = WaterModuleConfig.WaterConfig.WaterClassType.ToString();
            waterType.SelectedValue = WaterModuleConfig.WaterConfig.WaterClass.ToString();
            ILogo_UP.FileUrl = WaterModuleConfig.WaterConfig.imgLogo;
            iAlp.Text = WaterModuleConfig.WaterConfig.imgAlph.ToString();
            waterWord.Text = WaterModuleConfig.WaterConfig.WaterWord.ToString();
            WordType.SelectedValue = WaterModuleConfig.WaterConfig.WaterWordType.ToString();
            WordStyle.SelectedValue = WaterModuleConfig.WaterConfig.WaterWordStyle.ToString();
            txt_dfg.Text = WaterModuleConfig.WaterConfig.WaterWordColor.ToString();
            localP.SelectedValue = WaterModuleConfig.WaterConfig.lopostion;
            //PX.Text = WaterModuleConfig.WaterConfig.loX.ToString();
            //PY.Text = WaterModuleConfig.WaterConfig.loY.ToString();
            WordSize.Text = WaterModuleConfig.WaterConfig.WaterWordSize.ToString();
            txt_waterimgs.Value = WaterModuleConfig.WaterConfig.WaterImgs;
            
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\">缩略图配置</li>" + Call.GetHelp(6));
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        WaterModuleConfigInfo dd = WaterModuleConfig.ConfigInfo();
        dd.WaterConfig.WaterClassType = DataConverter.CLng(RadioButtonList1.SelectedValue);
        dd.WaterConfig.WaterClass = waterType.SelectedValue;
        if (ILogo_UP.HasFile)
        {
            ILogo_UP.SaveFile();
            dd.WaterConfig.imgLogo = ILogo_UP.FileUrl;
        }
        else //未指定文件,则以文本框中的为准
        {
            dd.WaterConfig.imgLogo = ILogo_UP.FVPath;
        }
        dd.WaterConfig.imgAlph = DataConverter.CLng(iAlp.Text);
        dd.WaterConfig.WaterWord = waterWord.Text;
        dd.WaterConfig.WaterWordType = WordType.SelectedValue;
        dd.WaterConfig.WaterWordStyle = WordStyle.SelectedValue;
        dd.WaterConfig.WaterWordColor = txt_dfg.Text;
        dd.WaterConfig.lopostion = localP.SelectedValue;
        //dd.WaterConfig.loX = DataConverter.CLng(PX.Text);
        //dd.WaterConfig.loY = DataConverter.CLng(PY.Text);
        dd.WaterConfig.WaterWordSize = DataConverter.CLng(WordSize.Text);
        dd.WaterConfig.IsUsed = Isuse.Checked;
        dd.WaterConfig.WaterImgs = txt_waterimgs.Value;
        WaterModuleConfig cc = new WaterModuleConfig();
        cc.Update(dd);
        SiteConfig.ThumbsConfig.ThumbsWidth = DataConverter.CLng(ThumbWidth_T.Text.Trim());
        SiteConfig.ThumbsConfig.ThumbsHeight = DataConverter.CLng(ThumbHeight_T.Text.Trim());
        SiteConfig.Update();
        function.WriteSuccessMsg("配置保存成功!", Request.RawUrl);
    }
}