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

namespace TimeTeller
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            UpdateSpokenText();
            SpeakTime();
            // Application.Current.Terminate();
        }

        private void UpdateSpokenText()
        {
            DateTime Now = DateTime.Now;
            SpokenText.Text = "Now is " + Now.Minute + " minutes past " + Now.Hour + ".";
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
    }
}