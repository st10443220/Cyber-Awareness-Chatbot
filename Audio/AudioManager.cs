using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Cyber_Awareness_Chatbot.Properties;

/*
 * https://stackoverflow.com/questions/1900707/how-to-play-audio-from-resource
 */

namespace Cyber_Awareness_Chatbot.Audio
{
    class AudioManager
    {
        public void PlayWelcomeMessage()
        {
            // Plays the audio file that is stored in the programs resources.
            PlayByteArray(Resources.about_time);
        }

        public void PlayByteArray(Byte[] audio)
        {
            // Converts the given byte[] to a stream.
            using (var audioStream = new MemoryStream(audio))
            {
                // Creates the sound player to play an audio stream.
                using (var player = new SoundPlayer(audioStream))
                {
                    // Plays the audio stream.
                    player.Play();
                }
            }
        }
    }
}
