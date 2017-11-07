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
            if(data == null)
            {
                var error = new MessageDialog("This part is not available.", "Error!");
                await error.ShowAsync();
                Frame.GoBack();
                return;
            }
            chapterText.SetHTML(data.dataHTML);
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            if (RequestedTheme == ElementTheme.Light)
                RequestedTheme = ElementTheme.Dark;
            else if (RequestedTheme == ElementTheme.Default)
                RequestedTheme = ElementTheme.Dark;
            else RequestedTheme = ElementTheme.Light;
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
            if (text == "")
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
    }
}
