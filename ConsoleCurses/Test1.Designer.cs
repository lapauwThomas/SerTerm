
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.0.20.0
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace TerminalCurses {
    using System;
    using Terminal.Gui;
    
    
    public partial class Test1 : Terminal.Gui.Window {
        
        private Terminal.Gui.TextView textView;
        
        private Terminal.Gui.TextField textField;
        
        private Terminal.Gui.Button button;
        
        private void InitializeComponent() {
            this.button = new Terminal.Gui.Button();
            this.textField = new Terminal.Gui.TextField();
            this.textView = new Terminal.Gui.TextView();
            this.Width = Dim.Fill(0);
            this.Height = Dim.Fill(0);
            this.X = 0;
            this.Y = 0;
            this.Modal = false;
            this.Border.BorderStyle = Terminal.Gui.BorderStyle.Single;
            this.Border.BorderBrush = Terminal.Gui.Color.Blue;
            this.Border.Effect3D = false;
            this.Border.Effect3DBrush = null;
            this.Border.DrawMarginFrame = true;
            this.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Title = "";
            this.textView.Width = 107;
            this.textView.Height = 23;
            this.textView.X = 0;
            this.textView.Y = 0;
            this.textView.AllowsTab = true;
            this.textView.AllowsReturn = true;
            this.textView.WordWrap = false;
            this.textView.Data = "textView";
            this.textView.Text = "\033[31;1;4mHello\033[0m";
            this.textView.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.textView.ReadOnly = true;
            this.textView.DesiredCursorVisibility = CursorVisibility.Invisible;
            this.Add(this.textView);
            this.textField.Width = 101;
            this.textField.Height = 1;
            this.textField.X = -2;
            this.textField.Y = 24;
            this.textField.Secret = false;
            this.textField.Data = "textField";
            this.textField.Text = "";
            this.textField.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.textField);
            this.button.Width = 8;
            this.button.Height = 1;
            this.button.X = 100;
            this.button.Y = 24;
            this.button.Data = "button";
            this.button.Text = "Send";
            this.button.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.button.IsDefault = false;
            this.Add(this.button);
        }
    }
}
