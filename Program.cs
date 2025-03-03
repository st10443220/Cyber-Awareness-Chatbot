using Cyber_Awareness_Chatbot.Audio;
using Cyber_Awareness_Chatbot.Properties;
using Cyber_Awareness_Chatbot.User;

namespace Cyber_Awareness_Chatbot
{
    internal class Program
    {
        static AudioManager audio = new AudioManager();
        static UserManager user = new UserManager();
        static void Main(string[] args)
        {
            // Play the welcome message!
            audio.PlayWelcomeMessage();

            // Display the ASCII art upon launch.
            Console.WriteLine(Resources.hacker_logo);

            // Prompt user for their name for a customised experience.
            user.GetUsersName();


            Console.ReadKey();
        }
    }
}
