﻿using System.IO;
using System.Xml;
using System.Xml.Linq;
using FluentJdf.Utility;

namespace FluentJdf.LinqToJdf
{
    /// <summary>
    /// Contains global constants etc.
    /// </summary>
    public static class Globals
    {

        ///<summary>
        /// The XML namespace for JDF 1.1 and higher
        ///</summary>
        public static XNamespace JdfNamespace = "http://www.CIP4.org/JDFSchema_1_1";

        /// <summary>
        /// Gets the xsi schema namespace.
        /// </summary>
        public static XNamespace XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// Creates a unique id suitable for use as a resource id, job id etc.
        /// </summary>
        /// <param name="prefix">The prefix. (optional)</param>
        /// <returns>A unique id that starts with the prefix</returns>
        public static string CreateUniqueId(string prefix = "R_")
        {
            return prefix + UniqueGenerator.MakeUnique();
        }

        /// <summary>
        /// Build a fully-qualified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XName JdfName(string name) { return JdfNamespace + name; }

        /// <summary>
        /// Gets the namespace manager.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        public static XmlNamespaceManager GetNamespaceManager(this XElement document)
        {
            var reader = XmlReader.Create(new StringReader(document.ToString()));
            var nameTable = reader.NameTable;
            if (nameTable != null)
            {
                var namespaceManager = new XmlNamespaceManager(nameTable);
                namespaceManager.AddNamespace("ns", "http://www.CIP4.org/JDFSchema_1_1");
                return namespaceManager;
            }
            return null;
        }
    }
}
