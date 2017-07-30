using JNCReaderUWP.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace JNCReaderUWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NovelDetails : Page
    {
        public NovelDetails()
        {
            this.InitializeComponent();
        }

        SeriesViewModel series;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = null;
            base.OnNavigatedTo(e);
            series = e.Parameter as SeriesViewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(series == null)
            {
                var error = new MessageDialog("There was a problem with loading the novel.", "Error");
                await error.ShowAsync();
                Frame.GoBack();
                return;
            }
            DataContext = series;
        }
    }
}
