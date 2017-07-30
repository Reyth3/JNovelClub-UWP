using JNCReaderUWP.ViewModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace JNCReaderUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OpenCloseHamburger(object sender, RoutedEventArgs e)
        {
            hamburgerMenu.IsPaneOpen = !hamburgerMenu.IsPaneOpen;
            hamburgerMenu.DataContext = new HamburgerMenuItemViewModel[]
            {
                new HamburgerMenuItemViewModel('\uE128', "News & Updates", typeof(View.News)),
            };
        }

        private void HamburgerItemSelected(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as HamburgerMenuItemViewModel;
            if (item.PageType != null)
                frame.Navigate(item.PageType);
            else item.SecondaryAction?.Invoke();
            hamburgerMenu.IsPaneOpen = false;
        }
    }
}
