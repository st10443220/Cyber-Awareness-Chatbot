using Cyber_Awareness_Chatbot.Audio;

namespace Cyber_Awareness_Chatbot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Audio.Audio audio = new Audio.Audio();
            audio.LoadWelcomeMessageAudio();
            Console.ReadKey();
        }
    }
}
