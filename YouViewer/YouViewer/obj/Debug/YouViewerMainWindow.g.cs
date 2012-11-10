﻿#pragma checksum "..\..\YouViewerMainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D3D65D6342525397AA0CB3F16DBB3E1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
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
using WPF.JoshSmith.Controls;
using YouViewer;


namespace YouViewer {
    
    
    /// <summary>
    /// YouViewerMainWindow
    /// </summary>
    public partial class YouViewerMainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\YouViewerMainWindow.xaml"
        internal System.Windows.Controls.TextBox txtKeyWord;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\YouViewerMainWindow.xaml"
        internal WPF.JoshSmith.Controls.DragCanvas dragCanvas;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\YouViewerMainWindow.xaml"
        internal System.Windows.Controls.MenuItem menuDraggingState;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\YouViewerMainWindow.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtKeyWord = ((System.Windows.Controls.TextBox)(target));
            
            #line 48 "..\..\YouViewerMainWindow.xaml"
            this.txtKeyWord.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtKeyWord_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dragCanvas = ((WPF.JoshSmith.Controls.DragCanvas)(target));
            return;
            case 3:
            
            #line 57 "..\..\YouViewerMainWindow.xaml"
            ((System.Windows.Controls.ContextMenu)(target)).AddHandler(System.Windows.Controls.MenuItem.ClickEvent, new System.Windows.RoutedEventHandler(this.OnMenuItemClick));
            
            #line default
            #line hidden
            return;
            case 4:
            this.menuDraggingState = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 5:
            this.viewer = ((YouViewer.Viewer)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
