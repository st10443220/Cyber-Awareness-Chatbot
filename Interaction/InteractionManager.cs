using Cyber_Awareness_Chatbot.Properties;

/*
 * https://www.youtube.com/watch?v=YyD1MRJY0qI
 * https://vslapp.wordpress.com/wp-content/uploads/2011/11/linq-cheatsheet.pdf
 * https://stackoverflow.com/questions/1731704/how-to-find-a-first-value-in-a-dictionary
 * https://stackoverflow.com/questions/498686/is-string-contains-faster-than-string-indexof
 * https://www.fortinet.com/resources/cyberglossary/types-of-cyber-attacks
 * https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread.sleep?view=net-9.0
 */

namespace Cyber_Awareness_Chatbot.Interaction
{
    class InteractionManager
    {
        private Dictionary<string, string> Questions = new()
        {
            { "help", @"Ask me about any of these topics:
> Phishing
> Scam
> Malware
> Social Engineering
> Phishing Email
> Password Safety
> Suspicious Links
> Internet Safety
> Cybersecurity Threats" },
            { "how are you", "I'm functioning perfectly, thank you! I’m here to assist you with any cybersecurity-related questions, {name}." },
            { "are you okay", "Yes, I’m operating smoothly! Let me know how I can assist you today, {name}." },
            { "whats your purpose", "My purpose is to help you stay safe online by educating you about cybersecurity threats and best practices, {name}." },
            { "what is your purpose", "I'm here to raise awareness about cyber threats and promote safer digital habits, {name}." },
            { "why are you here", "I exist to assist you with learning about cybersecurity and how to protect yourself online, {name}." },
            { "what can i ask you", @"Great question, {name}! You can ask me about:
> Phishing
> Scam
> Malware
> Social Engineering
> Phishing Email
> Password Safety
> Suspicious Links
> Internet Safety
> Cybersecurity Threats" },
            { "what do you know", @"I specialize in cybersecurity awareness. You can ask me about:
> Phishing
> Scam
> Malware
> Social Engineering
> Phishing Email
> Password Safety
> Suspicious Links
> Internet Safety
> Cybersecurity Threats" },
            { "topics", @"Here are the topics I can help you with:
> Phishing
> Scam
> Malware
> Social Engineering
> Phishing Email
> Password Safety
> Suspicious Links
> Internet Safety
> Cybersecurity Threats" },
            { "phishing", "Phishing is a cyber attack where scammers impersonate trusted entities to steal personal information. Be cautious of unexpected emails or messages asking for sensitive details, {name}." },
            { "scam", "Scams often involve too-good-to-be-true offers, urgent requests for money, or messages from unknown contacts. Always verify sources before taking action!" },
            { "malware", "Malware is malicious software that can harm your device, steal data, or spy on your activity. Avoid downloading files from unknown sources and keep your security software updated." },
            { "social", "Social engineering is a psychological manipulation tactic used by cybercriminals to trick people into revealing confidential information. Always verify requests for sensitive data, {name}." },
            { "email", "A phishing email often contains urgent or threatening language, unexpected attachments, or links to fake websites. Check the sender’s email address and never click on suspicious links." },
            { "password", "Hey {name}, a strong password should be at least 12 characters long, include a mix of letters, numbers, and symbols, and be unique for each account. And don’t forget to turn on multi-factor authentication!" },
            { "link", "A suspicious link may have slight misspellings, strange domain names, or unexpected redirects. Hover over links before clicking to see the actual URL, {name}!" },
            { "internet", "The internet has amazing benefits, but it’s also full of risks. Staying informed and practicing cybersecurity habits makes it much safer!" },
            { "threat", "Right now, one of the biggest threats is ransomware—attackers lock your files and demand money to restore access. Always keep backups and be cautious of suspicious emails!" }
        };

        private int warningCounter = 0;

        public void Chat()
        {
            // Receive the user from the main program.
            string? name = Program.user?.Username ?? "User";

            // Create an exit flag.
            bool exit = false;

            // Initialise the conversation with a personal touch.
            PrintSimulatedResponse($"Hey {name}, hope you are doing well! How can I help you today?", 30, ConsoleColor.White);

            // Simulate a short delay before showing the "help" response
            Thread.Sleep(500);

            // Display the "help" response at the beginning
            PrintSimulatedResponse(Questions["help"].Replace("{name}", name), 5, ConsoleColor.Cyan);

            // Decoding the curse words, and storing in an array for later use. This is to ensure that their is educational questions.
            string[] curseWords = (Resources.curse_words).Split(',');

            // Chat loop. Will run until the user wants to exit!
            while (!exit)
            {
                // Get the users question, with their font color being blue.
                Console.ForegroundColor = ConsoleColor.Blue;
                // Make a little arrow so the user knows where they are typing.
                Console.Write("-> ");
                // Get the question of the user. Whilst ensuring there are no white spaces and it is lowercase to ensure better usability.
                string? question = Console.ReadLine()?.Trim().ToLower();
                Console.ResetColor();


                exit = ProcessQuestion(name!, question, curseWords);
            }
        }

        private bool ProcessQuestion(string name, string? question, string[] curseWords)
        {

            // If the string is not null or empty. SO VALID STRING. we can change the "exit" value depending on what the user entered.
            if (string.IsNullOrEmpty(question))
            {
                PrintSimulatedResponse($"Please enter a valid question, {name}.", 30, ConsoleColor.Yellow);
                return false;
            }

            // Check input length (e.g., max 200 characters)
            if (question!.Length > 200)
            {
                PrintSimulatedResponse($"Input is too long, {name}. Please keep it under 200 characters.", 30, ConsoleColor.Yellow);
                return false;
            }

            // Check for invalid characters (e.g., only allow alphanumeric, spaces, and common punctuation)
            if (!question!.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || "!?.@#$%^&*()_+-=[]{}|;:,.<>?".Contains(c)))
            {
                PrintSimulatedResponse($"Invalid characters detected, {name}. Please use letters, numbers, or common punctuation.", 30, ConsoleColor.Yellow);
                return false;
            }

            // Cycle through curseWords to see if the question contains any of them.
            if (question.Split(" ").Any(word => curseWords.Contains(word)))
            {
                // Display the appropriate warning.
                HandleWarning(name);
                return false;
            }


            // Find the keyword in the user's question
            string? matchingKey = Questions.Keys.FirstOrDefault(key => question.Contains(key, StringComparison.OrdinalIgnoreCase));

            if (matchingKey != null)
            {
                // Get the response from the Questions dictionary
                string response = Questions[matchingKey].Replace("{name}", name);

                // Print the response
                PrintSimulatedResponse(response, 20, ConsoleColor.Green);

                return false;
            }
            else
            {
                string[] leaveKeywords = { "exit", "leave", "goodbye", "bye" };

                // Check to see if user wants to exit.
                if (question.Split(" ").Any(word => leaveKeywords.Contains(word)))
                {
                    PrintSimulatedResponse($"It's sad to see you go {name}... But goodluck on your journey and stay cyber safe!", 30, ConsoleColor.Magenta);
                    return true;
                }
                else
                {
                    // The invalid response.
                    PrintSimulatedResponse($"I'm sorry, {name}. I couldn't understand that topic.", 30, ConsoleColor.Yellow);
                    return false;
                }
            }
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
                    PrintSimulatedResponse(dots, 200, ConsoleColor.Red, false, false, false);
                }
                else
                {
                    // Display simulated text.
                    PrintSimulatedResponse(c, 50, ConsoleColor.Yellow, false);
                }
            }
            Environment.Exit(0);
        }

        // Overloaded methods so i can accept both string and char, and depending on which type is parsed differents actions will happen
        private void PrintSimulatedResponse(char c, int delay, ConsoleColor color, Boolean sectionHeader = true)
        {
            if (sectionHeader)
            {
                DisplaySectioner();
            }

            // Change the color of the text.
            Console.ForegroundColor = color;
            // Add a simulated delay.
            Thread.Sleep(delay);
            // Display the character.
            Console.Write(c);
            // Reset color back to default.
            Console.ResetColor();

            if (sectionHeader)
            {
                DisplaySectioner();
            }
        }

        private void PrintSimulatedResponse(string text, int delay, ConsoleColor color, Boolean newLine = true, Boolean typingIndicator = true, Boolean sectionHeader = true)
        {
            if (typingIndicator)
            {
                ShowTypingIndicator(); // Show typing animation before response
            }

            if (sectionHeader)
            {
                DisplaySectioner();
            }

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

            if (sectionHeader)
            {
                DisplaySectioner();
            }
        }

        private void PrintResponse(string text, ConsoleColor color, Boolean sectionHeader = true)
        {
            ShowTypingIndicator(); // Show typing animation before response
                                   // Change the color of the text.

            DisplaySectioner();

            Console.ForegroundColor = color;
            // Display the text.
            Console.WriteLine(text);
            // Reset color back to default.
            Console.ResetColor();

            DisplaySectioner();
        }

        public void DisplaySectioner()
        {
            // Display line to be used as a sectioner.
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("<------------------------------------------------------------------------------->");
            Console.ResetColor();
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

        private void ShowTypingIndicator()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < 3; i++)
            {
                Console.Write("\rBot is thinking" + new string('.', i + 1));
                Thread.Sleep(300);
            }
            Console.ResetColor();
            Console.WriteLine();
        }

    }
}