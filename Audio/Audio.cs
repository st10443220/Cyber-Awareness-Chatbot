using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace Cyber_Awareness_Chatbot.Audio
{
    class Audio
    {
        public void LoadWelcomeMessageAudio()
        {
            string audioPath = "./Assets/about_time.wav";

            using (SoundPlayer player = new SoundPlayer(audioPath))
            {
                try
                {
                    player.Load();

                    player.Play();

                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
