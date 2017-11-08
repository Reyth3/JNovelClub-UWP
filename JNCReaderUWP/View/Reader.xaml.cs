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
using Windows.Media.SpeechSynthesis;
using Windows.Storage;
using Windows.System.Display;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
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
            this.SetUpTransitions();
        }

        PartViewModel part;
        string scrollOffsetKey;

        ObservableCollection<FrameworkElement> PagesCollection { get; set; }
        DisplayRequest display;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            part = e.Parameter as PartViewModel;
            scrollOffsetKey = $"part-{part.Id}-scroll";
            display = new DisplayRequest();
            display.RequestActive();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var roaming = ApplicationData.Current.RoamingSettings;
            var offset = sv.VerticalOffset / sv.ScrollableHeight;
            if (roaming.Values.ContainsKey(scrollOffsetKey))
                roaming.Values[scrollOffsetKey] = offset;
            else roaming.Values.Add(scrollOffsetKey, offset);
            if(display != null)
            {
                display.RequestRelease();
                display = null;
            }
            base.OnNavigatedFrom(e);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var roaming = ApplicationData.Current.RoamingSettings;
            if (roaming.Values.ContainsKey("readerTheme"))
                RequestedTheme = (ElementTheme)((int)roaming.Values["readerTheme"]);
            var fontFamily = "readerFontFamily";
            if (roaming.Values.ContainsKey(fontFamily))
                chapterText.FontFamily = new FontFamily(roaming.Values[fontFamily].ToString());
            if (part == null)
                return;
            var data = await part.GetPartData();
            if(data == null)
            {
                var error = new MessageDialog("This part is not available.", "Error!");
                await error.ShowAsync();
                Frame.GoBack();
                return;
            }
            chapterText.SetHTML(data.dataHTML);
            await Task.Delay(1000);
            if (roaming.Values.ContainsKey(scrollOffsetKey))
                sv.ChangeView(null, (double)roaming.Values[scrollOffsetKey] * sv.ScrollableHeight, null);
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            if (RequestedTheme == ElementTheme.Light)
                RequestedTheme = ElementTheme.Dark;
            else if (RequestedTheme == ElementTheme.Default)
                RequestedTheme = ElementTheme.Dark;
            else RequestedTheme = ElementTheme.Light;
            var roaming = ApplicationData.Current.RoamingSettings;
            if (roaming.Values.ContainsKey("readerTheme"))
                roaming.Values["readerTheme"] = (int)RequestedTheme;
            else roaming.Values.Add("readerTheme", (int)RequestedTheme);
        }

        private void ChangeFontSize(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            var paragraphs = chapterText.Blocks.OfType<Paragraph>();
            var change = b.Name[0] == 'i' ? 1 : -1;
            foreach (var paragraph in paragraphs)
                if((change == -1 && paragraph.FontSize >= 10) || (change == 1 && paragraph.FontSize <= 100))
                    paragraph.FontSize += change;
        }

        int readerPosition;
        SpeechSynthesizer speech;

        async Task ReadParagraph()
        {
            if(speech == null)
            {
                speech = new SpeechSynthesizer();
                speech.Voice = SpeechSynthesizer.DefaultVoice;
            }
            if (readerPosition >= chapterText.Blocks.Count)
                return;
            var block = chapterText.Blocks[readerPosition];
            var text = block.GetRawText();
            if (text == "" || !text.IsPronouncable())
            {
                readerPosition++;
                await ReadParagraph();
                return;
            }
            else if (text == null)
                return;
            chapterText.Select((block as Paragraph).ContentStart, (block as Paragraph).ContentEnd);
            var stream = await speech.SynthesizeTextToStreamAsync(text);
            speechPlayer.SetSource(stream, stream.ContentType);
            speechPlayer.Play();
        }

        private async void StartTTSButtonClick(object sender, RoutedEventArgs e)
        {
            readerPosition = 0;
            await ReadParagraph();
        }

        private async void SpeechEnded(object sender, RoutedEventArgs e)
        {
            readerPosition++;
            await ReadParagraph();
        }

        private async void ChangeFont(object sender, RoutedEventArgs e)
        {
            var fs = new Dialogs.FontSelector();
            await fs.ShowAsync();
            if(fs.Result != null)
            {
                var roaming = ApplicationData.Current.RoamingSettings;
                var key = "readerFontFamily";
                if (roaming.Values.ContainsKey(key))
                    roaming.Values[key] = fs.Result;
                else roaming.Values.Add(key, fs.Result);
                chapterText.FontFamily = new FontFamily(fs.Result);
            }
        }
    }
}
