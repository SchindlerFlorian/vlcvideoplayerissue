using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibVLCSharp.Forms.Shared;
using LibVLCSharp.Shared;
using Xamarin.Forms;

namespace videoplayertesting
{
    public partial class MainPage : ContentPage
    {
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;
        float length;

        public MainPage()
        {
            InitializeComponent();
            Core.Initialize();
        }

        protected  override void OnAppearing()
        {
            base.OnAppearing();

            Core.Initialize();

            _libVLC = new LibVLC("--verbose=2");
            videoView.MediaPlayer = new MediaPlayer(_libVLC)
            {
                EnableHardwareDecoding = true,                              
            };

            videoView.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
            videoView.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged; ;
            videoView.MediaPlayer.LengthChanged += MediaPlayer_LengthChanged;

            var media = new Media(_libVLC, new Uri("https://customurl.blob.core.windows.net/7894b4f3f2e2/B0C554314DB0-dcc74a22dd030c5c9c34345d80c0147d.mp4?sv=2015-12-11&sr=c&sp=r&se=2021-04-15T11:06:01Z&st=2021-04-15T10:21:01Z&sig=zrK%2Fjzh10rKdN%2BwQAaQ9t9TJzhyy3ILXSVup1g3%2B4gM%3D"));
            videoView.MediaPlayer.Play(media);


        }

        private void MediaPlayer_PositionChanged(object sender, MediaPlayerPositionChangedEventArgs e)
        {
            Console.WriteLine("Current Video Position: " + e.Position);
        }

        private void MediaPlayer_LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {
            length = e.Length;
            Console.WriteLine("New Length: " + length);
        }

        private void MediaPlayer_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            Console.WriteLine("Current Video Time: " + (e.Time/1000)+"| Total Video length: "+(length/1000));
        }
    }
}
