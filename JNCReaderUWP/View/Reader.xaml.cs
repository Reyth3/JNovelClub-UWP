using JNCReaderUWP.Helpers;
using JNCReaderUWP.ViewModel.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class Reader : Page
    {
        public Reader()
        {
            this.InitializeComponent();
        }

        PartViewModel part;

        ObservableCollection<FrameworkElement> PagesCollection { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            part = e.Parameter as PartViewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (part == null)
                return;
            var data = await part.GetPartData();
            chapterText.SetHTML(data.dataHTML);
        }
    }
}
