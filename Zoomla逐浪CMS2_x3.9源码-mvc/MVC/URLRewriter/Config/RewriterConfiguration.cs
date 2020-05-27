using System;
using System.Web;
using System.Web.Caching;
using System.Configuration;
using System.Xml.Serialization;


namespace URLRewriter.Config
{
	/// <summary>
	/// Specifies the configuration settings in the Web.config for the RewriterRule.
	/// </summary>
	/// <remarks>This class defines the structure of the Rewriter configuration file in the Web.config file.
	/// Currently, it allows only for a set of rewrite rules; however, this approach allows for customization.
	/// For example, you could provide a ruleset that <i>doesn't</i> use regular expression matching; or a set of
	/// constant names and values, which could then be referenced in rewrite rules.
	/// <p />
	/// The structure in the Web.config file is as follows:
	/// <code>
	/// &lt;configuration&gt;
	/// 	&lt;configSections&gt;
	/// 		&lt;section name="RewriterConfig" 
	/// 		            type="URLRewriter.Config.RewriterConfigSerializerSectionHandler, URLRewriter" /&gt;
	///		&lt;/configSections&gt;
	///		
	///		&lt;RewriterConfig&gt;
	///			&lt;Rules&gt;
	///				&lt;RewriterRule&gt;
	///					&lt;LookFor&gt;<i>pattern</i>&lt;/LookFor&gt;
	///					&lt;SendTo&gt;<i>replace with</i>&lt;/SendTo&gt;
	///				&lt;/RewriterRule&gt;
	///				&lt;RewriterRule&gt;
	///					&lt;LookFor&gt;<i>pattern</i>&lt;/LookFor&gt;
	///					&lt;SendTo&gt;<i>replace with</i>&lt;/SendTo&gt;
	///				&lt;/RewriterRule&gt;
	///				...
	///				&lt;RewriterRule&gt;
	///					&lt;LookFor&gt;<i>pattern</i>&lt;/LookFor&gt;
	///					&lt;SendTo&gt;<i>replace with</i>&lt;/SendTo&gt;
	///				&lt;/RewriterRule&gt;
	///			&lt;/Rules&gt;
	///		&lt;/RewriterConfig&gt;
	///		
	///		&lt;system.web&gt;
	///			...
	///		&lt;/system.web&gt;
	///	&lt;/configuration&gt;
	/// </code>
	/// </remarks>
	[Serializable()]
	[XmlRoot("RewriterConfig")]
	public class RewriterConfiguration
	{
		// private member variables
		private RewriterRuleCollection rules;			// an instance of the RewriterRuleCollection class...

		/// <summary>
		/// GetConfig() returns an instance of the <b>RewriterConfiguration</b> class with the values populated from
		/// the Web.config file.  It uses XML deserialization to convert the XML structure in Web.config into
		/// a <b>RewriterConfiguration</b> instance.
		/// </summary>
		/// <returns>A <see cref="RewriterConfiguration"/> instance.</returns>
		public static RewriterConfiguration GetConfig()
		{
			if (HttpContext.Current.Cache["RewriterConfig"] == null)
				HttpContext.Current.Cache.Insert("RewriterConfig", ConfigurationSettings.GetConfig("RewriterConfig"));
            
			return (RewriterConfiguration) HttpContext.Current.Cache["RewriterConfig"];
		}

        /// <summary>
        /// 返回Url重写的目录名
        /// </summary>
        /// <returns></returns>
        public static RewriterRule GetBaseConfig()
        {
            CryptoHelper cr = new CryptoHelper();
            URLRewriter.Config.RewriterConfiguration.GetConfig().Rules[0].LookFor = cr.Decrypt(URLRewriter.Config.RewriterConfiguration.GetConfig().Rules[0].LookFor);
            URLRewriter.Config.RewriterConfiguration.GetConfig().Rules[0].SendTo = cr.Decrypt(URLRewriter.Config.RewriterConfiguration.GetConfig().Rules[0].SendTo);
            return URLRewriter.Config.RewriterConfiguration.GetConfig().Rules[0];
        }

		#region Public Properties
		/// <summary>
		/// A <see cref="RewriterRuleCollection"/> instance that provides access to a set of <see cref="RewriterRule"/>s.
		/// </summary>
		public RewriterRuleCollection Rules
		{
			get
			{
				return rules;
			}
			set
			{
				rules = value;
			}
		}
		#endregion
	}
}
