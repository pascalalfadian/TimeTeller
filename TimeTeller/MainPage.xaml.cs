using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TimeTeller.Resources;
using Windows.Phone.Speech.Synthesis;
using Windows.Phone.Speech.VoiceCommands;
using Microsoft.Phone.Tasks;

namespace TimeTeller
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            InitializeVoiceCommand();
        }

        private async void InitializeVoiceCommand()
        {
            await VoiceCommandService.InstallCommandSetsFromFileAsync(new Uri("ms-appx:///TimeTellerVCD.xml"));
        }

        private async void SpeakTime()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            String ssml = "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"en-US\">";
            ssml += SpokenText.Text;
            ssml += "<mark name=\"done\"/>";
            ssml += "</speak>";
            synth.BookmarkReached += synth_BookmarkReached;
            await synth.SpeakSsmlAsync(ssml);
        }

        private void synth_BookmarkReached(object sender, SpeechBookmarkReachedEventArgs e)
        {
            if (e.Bookmark == "done")
            {
                Application.Current.Terminate();
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Is this a new activation or a resurrection from tombstone?
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.New)
            {

                // Was the app launched using a voice command?
                if (NavigationContext.QueryString.ContainsKey("voiceCommandName"))
                {

                    // If so, get the name of the voice command.
                    string voiceCommandName = NavigationContext.QueryString["voiceCommandName"];
                    TimeToTextConverter converter = new TimeToTextConverter(DateTime.Now);

                    // Define app actions for each voice command name.
                    switch (voiceCommandName)
                    {
                        case "Now":
                            SpokenText.Text = converter.getCurrentTime();
                            break;
                        case "Today":
                            SpokenText.Text = converter.getCurrentDate();
                            break;
                        default:
                            SpokenText.Text = "Sorry, please try again with now or today.";
                            break;
                    }
                    SpeakTime();
                }
                else
                {
                    // TODO Go to settings
                }
            }
        }

        private void sponsorButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri("http://kiri.travel", UriKind.Absolute);
            webBrowserTask.Show();
        }
    }
}