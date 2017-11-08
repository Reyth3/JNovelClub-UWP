using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JNCReaderUWP.View.Dialogs
{
    public sealed partial class FontSelector : ContentDialog
    {
        public FontSelector()
        {
            this.InitializeComponent();
            DataContext = FontNames;
        }

        private static string[] FontNames = {
            "Arial", "Calibri", "Cambria", "Cambria Math", "Comic Sans MS", "Courier New",
            "Ebrima", "Gadugi", "Georgia",
            "Javanese Text Regular Fallback font for Javanese script", "Leelawadee UI",
            "Lucida Console", "Malgun Gothic", "Microsoft Himalaya", "Microsoft JhengHei",
            "Microsoft JhengHei UI", "Microsoft New Tai Lue", "Microsoft PhagsPa",
            "Microsoft Tai Le", "Microsoft YaHei", "Microsoft YaHei UI",
            "Microsoft Yi Baiti", "Mongolian Baiti", "MV Boli", "Myanmar Text",
            "Nirmala UI", "Segoe MDL2 Assets", "Segoe Print", "Segoe UI", "Segoe UI Emoji",
            "Segoe UI Historic", "Segoe UI Symbol", "SimSun", "Times New Roman",
            "Trebuchet MS", "Verdana", "Webdings", "Wingdings", "Yu Gothic",
            "Yu Gothic UI"
        };

        public string Result { get; set; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Result = fontsListView.SelectedItem as string;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
