﻿#pragma checksum "..\..\BuyerMainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7D97D0AAFC14CDC47D27BED6095B100109700E6D1CC1917137BFB8EE751ED020"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using TMSMainWindow;


namespace TMSMainWindow {
    
    
    /// <summary>
    /// BuyerMainWindow
    /// </summary>
    public partial class BuyerMainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\BuyerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox NewCustomerListBox;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\BuyerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox AcceptedCustomerListBox;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\BuyerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AcceptSelectedCustomerButton;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\BuyerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox MarketplaceOrdersListBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\BuyerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateNewOrderButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\BuyerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SelectCityToAddDropdown;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\BuyerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddCityToOrderButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\BuyerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GenerateInvoiceButton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\BuyerMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox CompletedOrdersListBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TMSMainWindow;component/buyermainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BuyerMainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NewCustomerListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.AcceptedCustomerListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.AcceptSelectedCustomerButton = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\BuyerMainWindow.xaml"
            this.AcceptSelectedCustomerButton.Click += new System.Windows.RoutedEventHandler(this.AcceptSelectedCustomerButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MarketplaceOrdersListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 21 "..\..\BuyerMainWindow.xaml"
            this.MarketplaceOrdersListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MarketplaceOrdersListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CreateNewOrderButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\BuyerMainWindow.xaml"
            this.CreateNewOrderButton.Click += new System.Windows.RoutedEventHandler(this.CreateNewOrderButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SelectCityToAddDropdown = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.AddCityToOrderButton = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\BuyerMainWindow.xaml"
            this.AddCityToOrderButton.Click += new System.Windows.RoutedEventHandler(this.AddCityToOrderButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.GenerateInvoiceButton = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.CompletedOrdersListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

