using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Data;
using FluentJdf.Resources;
using Infrastructure.Core.Logging;

namespace FluentJdf.TemplateEngine
{
	/// <summary>
	/// An item within a template.
	/// </summary>
	public abstract class TemplateItem {
	    readonly ILog logger = LogManager.GetLogger(typeof (TemplateItem));
		/// <summary>
		/// The name of the item.
		/// </summary>
		protected string name;

		/// <summary>
		/// This item's line number within the xml template file.
		/// </summary>
		protected int lineNumber;

		/// <summary>
		/// This item's column position within the xml template file.
		/// </summary>
		protected int positionInLine;

		/// <summary>
		/// The template item that contains this item.  May be null.
		/// </summary>
		protected TemplateItem parent;

		/// <summary>
		/// A collection of all template items contained by this item.  May be empty.
		/// </summary>
		protected TemplateItemCollection children = new TemplateItemCollection();

		/// <summary>
		/// If this template item replaces based on a table, this is the table.
		/// </summary>
		protected TableTemplateItem parentTableItem;

		/// <summary>
		/// Construct an instance.
		/// </summary>
		/// <param name="parent">The template item that contains this item.</param>
		/// <param name="name">The name of this item.</param>
		/// <param name="lineNumber">This item's line number position within the xml template file.</param>
		/// <param name="positionInLine">This item's column position within the xml template file.</param>
		protected internal TemplateItem(TemplateItem parent, string name, int lineNumber, int positionInLine)
		{
			this.name = name;
			this.lineNumber = lineNumber;
			this.positionInLine = positionInLine;
			this.parent = parent;
			if (this.parent != null)
			{
				this.parent.children.Add(this);
			}

			if (name.IndexOf(".") != -1)
			{
				string [] parts = name.Split('.');
				if (parts.Length > 2) {
				    string mess =
				        string.Format(
				            Messages.TemplateItem_TemplateItem_VariableNameIsNotLegal,
				            name);
                    logger.Error(string.Format(Messages.ErrorAtLineAndColumn, mess, this.lineNumber, this.positionInLine));
                    throw new TemplateExpansionException(this.lineNumber, this.positionInLine, mess);
				}

				if (parts.Length == 2)
				{
					TemplateItem currentParent = this;
					while (parentTableItem == null && currentParent.Parent != null)
					{
						currentParent = currentParent.Parent;
						if (currentParent is TableTemplateItem)
						{
							if (((TableTemplateItem)currentParent).IsTableOwner(parts[0]))
							{
								parentTableItem = (TableTemplateItem)currentParent;
							}
						}
					}

					this.name = parts[1];
				}
			}
		}

		/// <summary>
		/// Gets the TemplateItemCollection that contains the 
		/// child TemplateItems
		/// </summary>
		public TemplateItemCollection Children
		{
			get
			{
				return children;
			}
		}

		/// <summary>
		/// Gets the parent of this item.
		/// </summary>
		/// <value>The parent of this item.</value>
		public TemplateItem Parent
		{
			get
			{
				return parent;
			}
		}

		/// <summary>
		/// Gets the name of this item.
		/// </summary>
		/// <value>The name of this item.</value>
		public string Name
		{
			get
			{
				return name;
			}
		}

		/// <summary>
		/// Gets the line number position of this item within the xml template file.
		/// </summary>
		/// <value>The line number position of this item within the xml template file.</value>
		public int LineNumber
		{
			get
			{
				return lineNumber;
			}
		}

		/// <summary>
		/// Gets the column number position of this item within the xml template file.
		/// </summary>
		/// <value>The column number position of this item within the xml template file.</value>
		public int PositionInLine
		{
			get
			{
				return positionInLine;
			}
		}

		/// <summary>
		/// Writes text to the output instance document based on this item.
		/// </summary>
		/// <param name="writer">The writer that will receive the data.</param>
		/// <param name="vars">A Dictionary{string,object} of name/value pairs for simple replacement fields.</param>
		/// <returns>True if the replacement took place.</returns>
		protected internal abstract bool Generate(TextWriter writer, Dictionary<string, object> vars);

	}
}
