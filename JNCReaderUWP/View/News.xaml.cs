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
    public sealed partial class News : Page
    {
        public News()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            pw.IsActive = true;
            var jnc = new JNClient();
            var eventsResponse = await jnc.Events.GetListOfEvents();
            DataContext = new
            {
                latest = eventsResponse.Result.Where(o => o.Date <= DateTime.Now).OrderByDescending(o => o.Date).Take(15).Select(o => new EventViewModel(o)),
                upcoming = eventsResponse.Result.Where(o => o.Date > DateTime.Now).OrderBy(o => o.Date).Take(10).Select(o => new EventViewModel(o))
            };
            pw.IsActive = false;
        }
    }
}
