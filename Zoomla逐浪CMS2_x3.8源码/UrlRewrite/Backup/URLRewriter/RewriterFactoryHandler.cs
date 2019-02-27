using System;
using System.IO;
using System.Web.UI;
using System.Web;
using URLRewriter.Config;
using System.Text.RegularExpressions;

namespace URLRewriter
{
	/// <summary>
	/// Provides an HttpHandler that performs redirection.
	/// </summary>
	/// <remarks>The RewriterFactoryHandler checks the rewriting rules, rewrites the path if needed, and then
	/// delegates the responsibility of processing the ASP.NET page to the <b>PageParser</b> class (the same one
	/// used by the <b>PageHandlerFactory</b> class).</remarks>
	public class RewriterFactoryHandler : IHttpHandlerFactory
	{
		/// <summary>
		/// GetHandler is executed by the ASP.NET pipeline after the associated HttpModules have run.  The job of
		/// GetHandler is to return an instance of an HttpHandler that can process the page.
		/// </summary>
		/// <param name="context">The HttpContext for this request.</param>
		/// <param name="requestType">The HTTP data transfer method (<b>GET</b> or <b>POST</b>)</param>
		/// <param name="url">The RawUrl of the requested resource.</param>
		/// <param name="pathTranslated">The physical path to the requested resource.</param>
		/// <returns>An instance that implements IHttpHandler; specifically, an HttpHandler instance returned
		/// by the <b>PageParser</b> class, which is the same class that the default ASP.NET PageHandlerFactory delegates
		/// to.</returns>
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			// log info to the Trace object...
			context.Trace.Write("RewriterFactoryHandler", "Entering RewriterFactoryHandler");

			string sendToUrl = url;
			string filePath = pathTranslated;

			// get the configuration rules
			RewriterRuleCollection rules = RewriterConfiguration.GetConfig().Rules;

			// iterate through the rules
			for(int i = 0; i < rules.Count; i++)
			{
				// Get the pattern to look for (and resolve its URL)
				string lookFor = "^" + RewriterUtils.ResolveUrl(context.Request.ApplicationPath, rules[i].LookFor) + "$"; 

				// Create a regular expression object that ignores case...
				Regex re = new Regex(lookFor, RegexOptions.IgnoreCase);

				// Check to see if we've found a match
				if (re.IsMatch(url))
				{
					// do any replacement needed
					sendToUrl = RewriterUtils.ResolveUrl(context.Request.ApplicationPath, re.Replace(url, rules[i].SendTo));
					
					// log info to the Trace object...
					context.Trace.Write("RewriterFactoryHandler", "Found match, rewriting to " + sendToUrl);

					// Rewrite the path, getting the querystring-less url and the physical file path
					string sendToUrlLessQString;
					RewriterUtils.RewriteUrl(context, sendToUrl, out sendToUrlLessQString, out filePath);

					// return a compiled version of the page
					context.Trace.Write("RewriterFactoryHandler", "Exiting RewriterFactoryHandler");	// log info to the Trace object...
					return PageParser.GetCompiledPageInstance(sendToUrlLessQString, filePath, context);
				}
			}


			// if we reached this point, we didn't find a rewrite match
			context.Trace.Write("RewriterFactoryHandler", "Exiting RewriterFactoryHandler");	// log info to the Trace object...
			return PageParser.GetCompiledPageInstance(url, filePath, context);
		}

		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
