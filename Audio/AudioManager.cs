using Cyber_Awareness_Chatbot.Properties;
using System.Diagnostics;
using System.Media;

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

            if (audio == null || audio.Length == 0)
            {
                Debug.WriteLine("AudioManager Error: Attempted to play invalid audio data (null or empty).");
                return; // Don't proceed if the input is obviously bad
            }

            try
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
            catch (Exception e)
            {
                Debug.WriteLine($"AudioManager Error: Failed to play audio stream. Error: {e.Message}");
            }
        }
    }
}
