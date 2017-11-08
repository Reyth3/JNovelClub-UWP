using JNCReaderUWP.Helpers;
using JNCReaderUWP.ViewModel;
using JNCReaderUWP.ViewModel.API;
using JNovelClub;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace JNCReaderUWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LightNovels : Page
    {
        public LightNovels()
        {
            this.InitializeComponent();
            this.SetUpTransitions();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            DataContext = null;
        }

        List<SeriesViewModel> SeriesList;

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (SeriesList == null)
            {
                pw.IsActive = true;
                var jnc = JNCHelper.InstantiateClient();
                var seriesResponse = await jnc.Series.GetListOfSeries(null, "parts");
                SeriesList = seriesResponse.Result.OrderBy(o => o.Title).Select(o => new SeriesViewModel(o)).ToList();
                DataContext = SeriesList;
                pw.IsActive = false;
            }
        }

        private void NovelClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(View.NovelDetails), e.ClickedItem);
        }
    }
}
