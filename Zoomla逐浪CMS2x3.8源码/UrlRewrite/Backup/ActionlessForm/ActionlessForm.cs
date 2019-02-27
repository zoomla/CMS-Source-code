using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace ActionlessForm
{
	/// <summary>
	/// The Form class extends the HtmlForm HTML control by overriding its RenderAttributes()
	/// method and NOT emitting an action attribute.
	/// </summary>
	public class Form : System.Web.UI.HtmlControls.HtmlForm
	{
		/// <summary>
		/// The RenderAttributes method adds the attributes to the rendered &lt;form&gt; tag.
		/// We override this method so that the action attribute is not emitted.
		/// </summary>
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			// write the form's name
			writer.WriteAttribute("name", this.Name);
			base.Attributes.Remove("name");

			// write the form's method
			writer.WriteAttribute("method", this.Method);
			base.Attributes.Remove("method");

			// remove the action attribute
			base.Attributes.Remove("action");

			// finally write all other attributes
			this.Attributes.Render(writer);

			if (base.ID != null)
				writer.WriteAttribute("id", base.ClientID);
		}

	}
}
