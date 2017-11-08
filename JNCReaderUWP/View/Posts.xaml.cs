using JNCReaderUWP.Helpers;
using JNCReaderUWP.ViewModel.API;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JNCReaderUWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Posts : Page
    {
        public Posts()
        {
            this.InitializeComponent();
            this.SetUpTransitions();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            pw.IsActive = true;
            var jnc = JNCHelper.InstantiateClient();
            var posts = await jnc.Posts.GetList();
            if(posts.Success)
            {
                DataContext = posts.Result.Select(o => new PostViewModel(o)).OrderByDescending(o => o.Date);
            }
            pw.IsActive = false;
        }

        private async void PostItemClicked(object sender, ItemClickEventArgs e)
        {
            var pd = new Dialogs.PostDialog(e.ClickedItem as PostViewModel);
            await pd.ShowAsync();
        }
    }
}
