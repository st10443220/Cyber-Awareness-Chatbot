using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Cyber_Awareness_Chatbot.User
{
    class UserManager
    {
        public string GetUsersName ()
        {
            // Initialise the value of user to null.
            string? User = null;

            // Keep asking the user for their name until it is valid.
            while (NameValid(User))
            {
                // Prompts the user for their name, while ensuring an "error" is said if they enter a null value.
                Console.Write($"{(!NameValid(User) ? "No name was entered!\n" : "")}Please provide your name for a more customised experience: ");
                User = Console.ReadLine();
            }

            // Returns the users name
            return User;
        }

        public Boolean NameValid(string? name)
        {
            // Returns true if name is null and false if it is valid
            return (name is null);
        }

    }
}
