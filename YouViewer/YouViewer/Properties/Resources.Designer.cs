﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YouViewer.Properties {
    using System;
    
    
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("YouViewer.Properties.Resources", typeof(Resources).Assembly);
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
        
        internal static System.Drawing.Bitmap back_ipad {
            get {
                object obj = ResourceManager.GetObject("back_ipad", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;Menus xmlns=&quot;&quot;&gt;
        ///  &lt;MenuItem Header=&quot;Film&quot; Name=&quot;menuSubFilmBookmark&quot; Click=&quot;menuSubFilmBookmark_Click&quot;/&gt;
        ///  &lt;MenuItem Header=&quot;Music&quot; Name=&quot;menuSubMusicBookmark&quot; Click=&quot;menuSubMusicBookmark_Click&quot;/&gt;
        ///  &lt;MenuItem Header=&quot;Education&quot; Name=&quot;menuSubEducationBookmark&quot; Click=&quot;menuSubEducationBookmark_Click&quot;/&gt;
        ///  &lt;MenuItem Header=&quot;Tutorial&quot; Name=&quot;menuSubTutorialBookmark&quot; Click=&quot;menuSubTutorialBookmark_Click&quot;/&gt;
        ///  &lt;MenuItem Header=&quot;Other&quot; Name=&quot;menuSubOtherBookmark&quot; Click=&quot;m [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string BookmarkList {
            get {
                return ResourceManager.GetString("BookmarkList", resourceCulture);
            }
        }
    }
}
