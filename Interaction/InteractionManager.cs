using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cyber_Awareness_Chatbot.Properties;

namespace Cyber_Awareness_Chatbot.Interaction
{
    class InteractionManager
    {
        private  List<string> Questions = new()
        {
            "what is phishing?",
            "how can i spot a scam?",
            "what is malware?",
            "what is social engineering?",
            "how do i recognize a phishing email?",
            "what are some safe password practices?",
            "how can i identify a suspicious link?",
            "hey, how's your day going?",
            "do you think the internet is safe?",
            "what's the biggest cybersecurity threat today?"
        };

        private  List<string> Responses = new()
        {
            "Phishing is a cyber attack where scammers impersonate trusted entities to steal personal information. Be cautious of unexpected emails or messages asking for sensitive details, {name}.",
            "Scams often involve too-good-to-be-true offers, urgent requests for money, or messages from unknown contacts. Always verify sources before taking action!",
            "Malware is malicious software that can harm your device, steal data, or spy on your activity. Avoid downloading files from unknown sources and keep your security software updated.",
            "Social engineering is a psychological manipulation tactic used by cybercriminals to trick people into revealing confidential information. Always verify requests for sensitive data, {name}.",
            "A phishing email often contains urgent or threatening language, unexpected attachments, or links to fake websites. Check the sender’s email address and never click on suspicious links.",
            "Hey {name}, a strong password should be at least 12 characters long, include a mix of letters, numbers, and symbols, and be unique for each account. And don’t forget to turn on multi-factor authentication!",
            "A suspicious link may have slight misspellings, strange domain names, or unexpected redirects. Hover over links before clicking to see the actual URL, {name}!",
            "I’m just a program, but I hope your day is going great, {name}! Stay cyber-safe out there!",
            "The internet has amazing benefits, but it’s also full of risks. Staying informed and practicing cybersecurity habits makes it much safer!",
            "Right now, one of the biggest threats is ransomware—attackers lock your files and demand money to restore access. Always keep backups and be cautious of suspicious emails!"
        };

        private int warningCounter = 0;

        public void Chat()
        {
            // Receive the user from the main program.
            string name = Program.user.User;
            // Create an exit flag.
            bool exit = false;

            // Initialise the conversation with a personal touch.
            PrintSimulatedResponse($"Hey {name}, hope you are doing well! How can I help you today?", 75, ConsoleColor.White, true);

            // Decoding the curse words, and storing in an array for later use. This is to ensure that their is educational questions.
            string[] curseWords = Base64Decode(Resources.curse_words).Split(',');

            //  Chat loop. Will run until the user wants to exit!
            while (!exit)
            {
                // Get the users question, with their font color being blue.
                Console.ForegroundColor = ConsoleColor.Blue;
                // Make a little arrow so the user knows where they are typing.
                Console.Write("-> ");
                // Get the question of the user. Whilst ensuring there are no white spaces and it is lowercase to ensure better usability.
                string? question = Console.ReadLine()?.Trim().ToLower();
                Console.ResetColor();

                // If the string is not null or empty. SO VALID STRING. we can change the "exit" value depending on what the user entered.
                if (!string.IsNullOrEmpty(question))
                {
                    exit = ProcessQuestion(name, question, curseWords);
                }
            }
        }

        private bool ProcessQuestion(string name, string question, string[] curseWords)
        {
            // Cycle through curseWords to see if the question contains any of them.
            if (curseWords.Any(q => question.Contains(q)))
            {
                // Display the appropriate warning.
                HandleWarning(name);
                return false;
            }

            // Get a index of the question inside the Questions list.
            int location = Questions.IndexOf(question);
            // If the value is -1 the question asked doesnt exist! So we handle the warning for that here.
            if (location >= 0)
            {
                // The valid response.
                PrintSimulatedResponse(Responses[location].Replace("{name}", name), 50, ConsoleColor.Green, true);
            }
            else
            {
                // Check to see if user wants to exit.
                if (question.Split(" ").Any(text => text == "exit"))
                {
                    PrintSimulatedResponse($"It's sad to see you go {name}... But goodluck on your journey and stay cyber safe!", 50, ConsoleColor.Magenta, true);
                    return true;
                }
                else
                {
                    // The invalid response.
                    PrintSimulatedResponse($"I'm sorry, {name}. I couldn't understand that question.", 50, ConsoleColor.Yellow, true);
                }

                
            }
            return false;
        }

        private void HandleWarning(string name)
        {
            // Create an array for storing the warnings.
            string[] warnings =
            {
                $"{name}, this language is not appropriate. Consider this your first warning.",
                $"This is your final warning, {name}. Refrain from using inappropriate language!"
            };

            // Check if the user has used up all their warnings.
            if (warningCounter < warnings.Length)
            {
                // Display the warning message according to how many warnings they have.
                PrintResponse(warnings[warningCounter], ConsoleColor.Red);
            }
            else
            {
                // If the are above their limit, display the final warning.
                PrintFinalWarning();
            }
            // Increment warning count.
            warningCounter++;
        }

        private void PrintFinalWarning()
        {
            string warningText = "THATS IT I AM SICK OF YOU GOODBYE";

            // Cycle through the warning text and display each CHARACTER 1 by 1.
            foreach (char c in warningText)
            {
                // String for loading in between spaces.
                string dots = "...";

                // If the character is a space, display the dots in the string called "dots"
                if (c.Equals(' '))
                {
                    // Display simulated dots.    
                    PrintSimulatedResponse(dots, 200, ConsoleColor.Red, false);
                }
                else
                {
                    // Display simulated text.
                    PrintSimulatedResponse(c, 50, ConsoleColor.Yellow);
                }
            }
            Environment.Exit(0);
        }
        // Overloaded methods so i can accept both string and char, and depending on which type is parsed differents actions will happen
        private void PrintSimulatedResponse(char c, int delay, ConsoleColor color)
        {
            // Change the color of the text.
            Console.ForegroundColor = color;
            // Add a simulated delay.
            Thread.Sleep(delay);
            // Display the character.
            Console.Write(c);
            // Reset color back to default.
            Console.ResetColor();

        }
        private void PrintSimulatedResponse(string text, int delay, ConsoleColor color, Boolean newLine)
        {
            // Change the color of the text.
            Console.ForegroundColor = color;
            // Cycle through the give text.
            foreach (var c in text)
            {
                // Add a simulated delay.
                Thread.Sleep(delay);
                // Display the character.
                Console.Write(c);
            }
            // Reset color back to default.
            Console.ResetColor();

            // If we parse through true into the method, we write a new line.
            // This is done so we dont have a whole bunch of Console.WriteLine(); around in the program code.
            // Also only some need a new line.
            if (newLine)
            {
                Console.WriteLine();
            }
        }

        private void PrintResponse(string text, ConsoleColor color)
        {
            // Change the color of the text.
            Console.ForegroundColor = color;
            // Display the text.
            Console.WriteLine(text);
            // Reset color back to default.
            Console.ResetColor();
        }

        public static string Base64Decode(string base64EncodedData)
        {
            // Convert the Base64 encoded string into a byte array.
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            // Convert the byte array into a UTF-8 encoded string and return the result.
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public void DisplayBanner()
        {
            // Create a color scheme for the ASCII art.
            ConsoleColor[] colors = { ConsoleColor.Cyan, ConsoleColor.White, ConsoleColor.Magenta };
            int index = 0;
            
            // Cycle through the ASCII arts text.
            foreach (char c in Resources.hacker_logo)
            {
                // If we find a newline character within the text, we change the color.
                if (c == '\n')
                {
                    // Cycles through the colors array by using % to ensure the index stays within the bounds of the colors array.
                    Console.ForegroundColor = colors[index % colors.Length];
                    index++;
                }
                // Display the character.
                Console.Write(c);
            }
            // Reset color back to default.
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
