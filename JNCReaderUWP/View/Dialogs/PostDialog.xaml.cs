using JNCReaderUWP.ViewModel.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class PostDialog : ContentDialog
    {
        public PostDialog(PostViewModel post)
        {
            this.InitializeComponent();
            this.post = post;
            this.DataContext = post;
            this.Title = post.Title;
        }

        PostViewModel post;

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            await Launcher.LaunchUriAsync(new Uri(new Uri("https://forums.j-novel.club/"), post.ForumLink));
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
