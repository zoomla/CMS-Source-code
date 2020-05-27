using System;
using System.Web;

namespace URLRewriter
{
	/// <summary>
	/// The base class for module rewriting.  This class is abstract, and therefore must be derived from.
	/// </summary>
	/// <remarks>Provides the essential base functionality for a rewriter using the HttpModule approach.</remarks>
	public abstract class BaseModuleRewriter : IHttpModule
	{
		/// <summary>
		/// Executes when the module is initialized.
		/// </summary>
		/// <param name="app">A reference to the HttpApplication object processing this request.</param>
		/// <remarks>Wires up the HttpApplication's AuthorizeRequest event to the
		/// <see cref="BaseModuleRewriter_AuthorizeRequest"/> event handler.</remarks>
		public virtual void Init(HttpApplication app)
		{
            // WARNING!  This does not work with Windows authentication!
			// If you are using Windows authentication, change to app.BeginRequest
			app.AuthorizeRequest += new EventHandler(this.BaseModuleRewriter_AuthorizeRequest);
		}

		public virtual void Dispose() {}

		/// <summary>
		/// Called when the module's AuthorizeRequest event fires.
		/// </summary>
		/// <remarks>This event handler calls the <see cref="Rewrite"/> method, passing in the
		/// <b>RawUrl</b> and HttpApplication passed in via the <b>sender</b> parameter.</remarks>
		protected virtual void BaseModuleRewriter_AuthorizeRequest(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication) sender;
			Rewrite(app.Request.Path, app);
		}

		/// <summary>
		/// The <b>Rewrite</b> method must be overriden.  It is where the logic for rewriting an incoming
		/// URL is performed.
		/// </summary>
		/// <param name="requestedRawUrl">The requested RawUrl.  (Includes full path and querystring.)</param>
		/// <param name="app">The HttpApplication instance.</param>
		protected abstract void Rewrite(string requestedPath, HttpApplication app);
	}
}
