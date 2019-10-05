using System;
using System.Linq;
using System.Windows.Media;

namespace MyGameLineBattles_WPF
{
    public class GameMusic
    {
        private MediaPlayer mp;
        private string march;
        private string sailor;
        private string gothic;
        private int number;
        public bool musicOn = true;

        public GameMusic()
        {
            ThreadProc();
            number = 0;
        }
        public void SetMusicOnOf()
        {
            if (musicOn)
            {
                mp.Pause();
                musicOn = false;
            }
            else
            {
                mp.Play();
                musicOn = true;
            }
        }
        public void ThreadProc()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            march = path + @"\The British Grenadiers fife and drum.wav";
            sailor = path + @"\Drunken Sailer - Irish Rovers.wav";
            gothic = path + @"\Gothic 2 Dark Saga music - Pirates camp.wav";
            mp = new MediaPlayer();
            mp.Volume = 0.2;
            PlaySongOne();
        }
        private void PlaySongOne()
        {
            mp.MediaEnded += OnMediaEnded;
            mp.Open(new Uri(march));
            mp.Play();

        }
        private void OnMediaEnded(object sender, EventArgs e)
        {
            number++;
            if (number == 1)
                PlaySongTwo();
            else if (number == 2)
                PlaySongThree();
            else
            {
                PlaySongOne();
                number = 0;
            }
        }
        private void PlaySongTwo()
        {
            mp.MediaEnded += OnMediaEnded;
            mp.Open(new Uri(sailor));
            mp.Play();
        }
        private void PlaySongThree()
        {
            mp.MediaEnded += OnMediaEnded;
            mp.Open(new Uri(gothic));
            mp.Play();
        }

    }
}
