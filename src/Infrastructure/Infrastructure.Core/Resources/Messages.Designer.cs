﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.431
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Core.Resources {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Infrastructure.Core.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot find component with name {0} of type {1}.
        /// </summary>
        internal static string NameSelectorCannotFindComponent {
            get {
                return ResourceManager.GetString("NameSelectorCannotFindComponent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The parameter {0} is required..
        /// </summary>
        internal static string ParameterCheck_GetParameterRequiredErrorMessage {
            get {
                return ResourceManager.GetString("ParameterCheck_GetParameterRequiredErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The parameter {0} must not have a zero value.
        /// </summary>
        internal static string ParameterCheck_IntParameterIsNonZero {
            get {
                return ResourceManager.GetString("ParameterCheck_IntParameterIsNonZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enumerable {0} must contain at least one member.
        /// </summary>
        internal static string ParameterCheck_ListMustContainAtLeastOne {
            get {
                return ResourceManager.GetString("ParameterCheck_ListMustContainAtLeastOne", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The string parameter {0} must not be null, empty or contain only whitespace..
        /// </summary>
        internal static string ParameterCheck_StringRequiredAndNotWhitespace {
            get {
                return ResourceManager.GetString("ParameterCheck_StringRequiredAndNotWhitespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string String1 {
            get {
                return ResourceManager.GetString("String1", resourceCulture);
            }
        }
    }
}
