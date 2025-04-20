using Cyber_Awareness_Chatbot.Properties;

namespace Cyber_Awareness_Chatbot.User
{
    class UserManager
    {
        private string? _username;
        // Initialise the value of user to null.
        public string? Username
        {
            get
            {
                return _username;
            }
            set
            {
                // Show error message if the value entered is Null or Empty (whitespace).
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No valid name was entered! Please try again.");
                    Console.ResetColor();
                    return;
                }

                // Show message if the value exists in the badword list and show warning message.
                if (Resources.curse_words.Split(',').Any(badword => value == badword))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You cannot use a curse word as your name!");
                    Console.ResetColor();
                    return;
                }

                // Assign the value to the username if the value is valid.
                _username = value;
            }
        }

        public string GetUsersName()
        {
            // Keep asking the user for their name until it is valid.
            while (string.IsNullOrWhiteSpace(this.Username))
            {
                // Prompts the user for their name, while ensuring an error message if they enter a null or empty value.
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please provide your name for a more customized experience: ");
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.Blue;
                string? input = Console.ReadLine();

                // Here is where if an invalid input will trigger the error line
                this.Username = input;

                Console.ResetColor();
            }

            // Returns the user's name
            // ! tells the compiler that the this.Username is NOT NULL
            return this.Username!;
        }
    }
}
