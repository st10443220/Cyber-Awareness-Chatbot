using Cyber_Awareness_Chatbot.Audio;
using Cyber_Awareness_Chatbot.Interaction;
using Cyber_Awareness_Chatbot.User;

namespace Cyber_Awareness_Chatbot
{
    internal class Program
    {
        static AudioManager audio = new AudioManager();
        public static UserManager user = new UserManager();
        static InteractionManager interaction = new InteractionManager();
        static void Main(string[] args)
        {
            // Play the welcome message!
            audio.PlayWelcomeMessage();

            // Display the ASCII art upon launch.
            interaction.DisplayBanner();

            // Prompt user for their name for a customised experience.
            user.GetUsersName();

            // Start the chat with the user
            interaction.Chat();

            Console.ReadKey();
        }
    }
}
