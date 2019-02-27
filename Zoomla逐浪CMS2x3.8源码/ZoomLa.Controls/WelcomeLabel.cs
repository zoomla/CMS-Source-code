using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZoomLa.Controls
{
    [
    AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("Text"),
    ToolboxData("<{0}:WelcomeLabel runat=\"server\"> </{0}:WelcomeLabel>")
    ]
    public class WelcomeLabel : WebControl
    {
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The welcome message text."),
        Localizable(true)
        ]
        public virtual string Text
        {
            get
            {
                string s = (string)ViewState["Text"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["Text"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.WriteEncodedText(Text);
            if (Context != null)
            {
                string s = Context.User.Identity.Name;
                if (s != null && s != String.Empty)
                {
                    string[] split = s.Split('\\');
                    int n = split.Length - 1;
                    if (split[n] != String.Empty)
                    {
                        writer.Write(", ");
                        writer.Write(split[n]);
                    }
                }
            }
            writer.Write("!");
        }
    }
}
