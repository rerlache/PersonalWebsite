﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlazorServerApp.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class PrivacyRes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PrivacyRes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BlazorServerApp.Resources.PrivacyRes", typeof(PrivacyRes).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Robin Erlacher&lt;br /&gt;
        ///Höttinger Auffahrt 1a&lt;br /&gt;
        ///6020 Innsbruck&lt;br /&gt;&lt;br /&gt;
        ///Phone: +43 (0)676 381 55 53&lt;br /&gt;
        ///Email: &lt;a href=&quot;mailto:main@robinerlacher.online&quot;&gt;main[at]robinerlacher.online&lt;/a&gt;.
        /// </summary>
        public static string ImprintContent {
            get {
                return ResourceManager.GetString("ImprintContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to I &lt;b&gt;DO NOT&lt;/b&gt; store any of your data, period..
        /// </summary>
        public static string PrivacyContent {
            get {
                return ResourceManager.GetString("PrivacyContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Imprint.
        /// </summary>
        public static string TitleImprint {
            get {
                return ResourceManager.GetString("TitleImprint", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Privacy-Policy.
        /// </summary>
        public static string TitlePrivacy {
            get {
                return ResourceManager.GetString("TitlePrivacy", resourceCulture);
            }
        }
    }
}
