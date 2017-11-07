using JNCReaderUWP.ViewModel;
using JNovelClub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            frame.Navigate(typeof(View.Posts));
        }

        public Frame ContentFrame { get { return frame; } }

        private void OpenCloseHamburger(object sender, RoutedEventArgs e)
        {
            hamburgerMenu.IsPaneOpen = !hamburgerMenu.IsPaneOpen;
            hamburgerMenu.DataContext = new HamburgerMenuItemViewModel[]
            {
                new HamburgerMenuItemViewModel('\uE70E', "Announcements", typeof(View.Posts)),
                new HamburgerMenuItemViewModel('\uE14C', "Novel Updates", typeof(View.News)),
                new HamburgerMenuItemViewModel('\uE1D3', "Light Novels", typeof(View.LightNovels)),
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

        private void UserFlyoutOpening(object sender, object e)
        {
            var isLoggedIn = ApplicationData.Current.RoamingSettings.Values.ContainsKey("userAuth");
            if (isLoggedIn)
            {
                flyoutLogInPanel.Visibility = Visibility.Collapsed;
                flyoutLogOutPanel.Visibility = Visibility.Visible;
            }
            else
            {
                flyoutLogOutPanel.Visibility = Visibility.Collapsed;
                flyoutLogInPanel.Visibility = Visibility.Visible;

            }
        }

        private async void FlyoutLogInButtonClick(object sender, RoutedEventArgs e)
        {
            if (lUser.Text == "" || lPass.Password == "")
                return;
            flyoutPw.IsActive = true;
            var jnc = new JNClient();
            var res = await jnc.Auth.Authenticate(lUser.Text.Trim(), lPass.Password);
            if(!res.Success)
            {
                var error = new MessageDialog("Login attempt failed! Check your username and password or try again later.", "Error!");
                await error.ShowAsync();
                return;
            }
            lUser.Text = "";
            lPass.Password = "";
            var key = "userAuth";
            var roaming = ApplicationData.Current.RoamingSettings.Values;
            var token = JsonConvert.SerializeObject(res.Result);
            if (roaming.ContainsKey(key))
                roaming[key] = token;
            else roaming.Add(key, token);
            flyoutPw.IsActive = false;
            flyoutLogInPanel.Visibility = Visibility.Collapsed;
            flyoutLogOutPanel.Visibility = Visibility.Visible;
        }

        private void FlyoutLogOutButtonClick(object sender, RoutedEventArgs e)
        {
            var key = "userAuth";
            var roaming = ApplicationData.Current.RoamingSettings.Values;
            if (roaming.ContainsKey(key))
                roaming.Remove(key);
            flyoutLogInPanel.Visibility = Visibility.Visible;
            flyoutLogOutPanel.Visibility = Visibility.Collapsed;
        }

        private async void JNCWebsiteClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://j-novel.club"));
        }

        private async void XTZSupportServerItemClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://discord.gg/GugNgeM"));
        }

        private async void SendFeedbackItemClick(object sender, RoutedEventArgs e)
        {
            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            }
            else
            {
                MessageDialog md = new MessageDialog("The Feedback Hub is not installed on your device.", "Error!");
                await md.ShowAsync();
            }
        }

        private async void ReviewAppItemClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri($"ms-windows-store://review/?PFN={Package.Current.Id.FamilyName}"));
        }

        private async void SubredditItemClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://reddit.com/r/xyzapps"));
        }
    }
}
