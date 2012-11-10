﻿#pragma checksum "..\..\YouViewerMainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "16B40E43BFD6D85ED46FB7EE6D14E060"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WPF.JoshSmith.Controls;
using YouViewer;


namespace YouViewer {
    
    
    /// <summary>
    /// YouViewerMainWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class YouViewerMainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\YouViewerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtKeyWord;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\YouViewerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHistoryView;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\YouViewerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbmBookmark;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\YouViewerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClear;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\YouViewerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMode;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\YouViewerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WPF.JoshSmith.Controls.DragCanvas dragCanvas;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\YouViewerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal YouViewer.Viewer viewer;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/YouViewer;component/youviewermainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\YouViewerMainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 6 "..\..\YouViewerMainWindow.xaml"
            ((YouViewer.YouViewerMainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtKeyWord = ((System.Windows.Controls.TextBox)(target));
            
            #line 52 "..\..\YouViewerMainWindow.xaml"
            this.txtKeyWord.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtKeyWord_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnHistoryView = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\YouViewerMainWindow.xaml"
            this.btnHistoryView.Click += new System.Windows.RoutedEventHandler(this.btnHistoryView_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbmBookmark = ((System.Windows.Controls.ComboBox)(target));
            
            #line 54 "..\..\YouViewerMainWindow.xaml"
            this.cbmBookmark.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbmBookmark_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnClear = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\YouViewerMainWindow.xaml"
            this.btnClear.Click += new System.Windows.RoutedEventHandler(this.btnClear_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnMode = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\YouViewerMainWindow.xaml"
            this.btnMode.Click += new System.Windows.RoutedEventHandler(this.btnMode_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dragCanvas = ((WPF.JoshSmith.Controls.DragCanvas)(target));
            return;
            case 8:
            this.viewer = ((YouViewer.Viewer)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

