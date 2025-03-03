using Cyber_Awareness_Chatbot.Audio;
using Cyber_Awareness_Chatbot.Properties;

namespace Cyber_Awareness_Chatbot
{
    internal class Program
    {
        static AudioManager audio = new AudioManager();
        static void Main(string[] args)
        {
            // Play the welcome message!
            audio.PlayWelcomeMessage();

            // Display the ASCII art upon launch.
            Console.WriteLine(Resources.hacker_logo);

            Console.ReadKey();
        }
    }
}
