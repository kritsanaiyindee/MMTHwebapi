﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechLineCaseAPI.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("64cad534-b380-4ac3-8732-00d293c691b4")]
        public string client_id_HMS {
            get {
                return ((string)(this["client_id_HMS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://tha-adfsqa.mmthqa.com/adfs/oauth2/token")]
        public string adfs_url_HMS {
            get {
                return ((string)(this["adfs_url_HMS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://tha-adfsqa.mmthqa.com/adfs/oauth2/token")]
        public string client_id_MMTH {
            get {
                return ((string)(this["client_id_MMTH"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://thcrmweb01/mmthqas/XRMServices/2011/Organization.svc")]
        public string SoapOrgServiceUri {
            get {
                return ((string)(this["SoapOrgServiceUri"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ad-mmth\\crmapp")]
        public string UserName {
            get {
                return ((string)(this["UserName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("P@$$w0rd")]
        public string Password {
            get {
                return ((string)(this["Password"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://tha-adfsqa.mmthqa.com/adfs/oauth2/token")]
        public string adfs_url_MMTH {
            get {
                return ((string)(this["adfs_url_MMTH"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Server=phoebe.hms-cloud.com;Database=mmthapi;user id=sa;password=pass@word1;")]
        public string ConStringHMS {
            get {
                return ((string)(this["ConStringHMS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Server=phoebe.hms-cloud.com;Database=mmthapi;user id=sa;password=pass@word1;")]
        public string ConString {
            get {
                return ((string)(this["ConString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://pre-mmth.dms-ccp.com/WS_InterfaceCRM/WS_InterfaceCRM.asmx?op=WSCheckHistor" +
            "yRO")]
        public string RO_WS {
            get {
                return ((string)(this["RO_WS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DEV {
            get {
                return ((bool)(this["DEV"]));
            }
        }
    }
}