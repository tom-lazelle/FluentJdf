
using System;
using System.Xml.Linq;
using Infrastructure.Core.CodeContracts;

namespace FluentJdf.LinqToJdf.Builder.Jmf {
	/// <summary>
	/// Build attributes for SuspendQueueEntryCommandBuilder.
	/// </summary>
	public partial class SuspendQueueEntryCommandAttributeBuilder : JmfAttributeBuilderBase {
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="builder"></param>
		internal SuspendQueueEntryCommandAttributeBuilder(SuspendQueueEntryCommandBuilder builder)
			: base(builder) {
		}

		/// <summary>
		/// Sets any attribute.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public SuspendQueueEntryCommandAttributeBuilder Attribute(XName name, string value) {
			ParameterCheck.ParameterRequired(name, "name");

			Element.SetAttributeValue(name, value);
			return this;
		}

		/// <summary>
		/// Set the id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public SuspendQueueEntryCommandAttributeBuilder Id(string id) {

			Element.SetAttributeValue("ID", id);
			return this;
		}

		/// <summary>
		/// Sets a unique id
		/// </summary>
		/// <returns></returns>
		public SuspendQueueEntryCommandAttributeBuilder UniqueId() {
			return Id(Globals.CreateUniqueId(SuspendQueueEntryCommandBuilder.IdPrefix));
		}

		/// <summary>
		/// Sets the version of this JMF node.
		/// </summary>
		/// <param name="jdfVersion"></param>
		/// <returns></returns>
		public SuspendQueueEntryCommandAttributeBuilder JdfVersion(string jdfVersion) {
			ParameterCheck.StringRequiredAndNotWhitespace(jdfVersion, "jdfVersion");

			Element.SetVersion(jdfVersion);
			return this;
		}		
	}
}

