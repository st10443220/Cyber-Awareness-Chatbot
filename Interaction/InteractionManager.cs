using Cyber_Awareness_Chatbot.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyber_Awareness_Chatbot;
using Cyber_Awareness_Chatbot.Properties;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;

namespace Cyber_Awareness_Chatbot.Interaction
{
    class InteractionManager
    {
        private List<string> Questions = new List<string>
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

        private List<string> Responses = new List<string>
{
    "phishing is a cyber attack where scammers impersonate trusted entities to steal personal information. be cautious of unexpected emails or messages asking for sensitive details, {name}.",
    "scams often involve too-good-to-be-true offers, urgent requests for money, or messages from unknown contacts. always verify sources before taking action!",
    "malware is malicious software that can harm your device, steal data, or spy on your activity. avoid downloading files from unknown sources and keep your security software updated.",
    "social engineering is a psychological manipulation tactic used by cybercriminals to trick people into revealing confidential information. always verify requests for sensitive data, {name}.",
    "a phishing email often contains urgent or threatening language, unexpected attachments, or links to fake websites. check the sender’s email address and never click on suspicious links.",
    "hey {name}, a strong password should be at least 12 characters long, include a mix of letters, numbers, and symbols, and be unique for each account. and don’t forget to turn on multi-factor authentication!",
    "a suspicious link may have slight misspellings, strange domain names, or unexpected redirects. hover over links before clicking to see the actual url, {name}!",
    "i’m just a program, but i hope your day is going great, {name}! stay cyber-safe out there!",
    "the internet has amazing benefits, but it’s also full of risks. staying informed and practicing cybersecurity habits makes it much safer!",
    "right now, one of the biggest threats is ransomware—attackers lock your files and demand money to restore access. always keep backups and be cautious of suspicious emails!"
};

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public void Chat()
        {
            string name = Program.user.User;
            Boolean exit = false;
            int warningCounter = 0;

            PrintResponse($"Hey {name}, hope you are doing well! How can I help you today?", ConsoleColor.White);


            string[] curseWords = Base64Decode(Resources.curse_words).Split(",");


            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                string question = Console.ReadLine();
                Console.ResetColor();

                int location = Questions.IndexOf(question.ToLower());
                //Console.WriteLine(location);



                if (curseWords.Any(q => question.ToLower().Contains(q)))
                {
                    //Console.WriteLine(warningCounter);
                    if (warningCounter == 0)
                    {
                        PrintResponse($"{name}, this is so profanic of you. This is not allowed. This is a warning, if you proceed to interact in this manner, I will take action against you.", ConsoleColor.Red);
                    }
                    else if (warningCounter == 1)
                    {
                        PrintResponse($"How dare you, this is your last warning {name}. Do it again and you will suffer the consequences!", ConsoleColor.Red);
                    }
                    warningCounter++;
                }
                else
                {
                    if (location >= 0)
                    {
                        PrintResponse(Responses[location], ConsoleColor.Green);
                    }
                    else
                    {
                        PrintResponse($"I'm so sorry {name} I failed you, I could not understand what you said.", ConsoleColor.Yellow);
                    }
                }

                if (warningCounter > 2)
                {
                    string warningText = "THATS IT I AM SICK OF YOU GOODBYE";

                    foreach (char c in warningText)
                    {
                        string dots = "...";

                        if (c.Equals(' '))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            foreach (var item in dots)
                            {
                                Thread.Sleep(200);
                                Console.Write(item);
                            }
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Thread.Sleep(100);
                            Console.Write(c);
                            Console.ResetColor();
                        }
                    }

                    Environment.Exit(0);
                }

            }

        }

        private void PrintResponse(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void DisplayBanner()
        {
            ConsoleColor[] colors =
            {
                ConsoleColor.Green,
                ConsoleColor.Yellow,
                ConsoleColor.Red,
                ConsoleColor.Cyan,
                ConsoleColor.Magenta,
                ConsoleColor.White
            };

            ConsoleColor[] trans =
            {
                ConsoleColor.Cyan,
                ConsoleColor.White,
                ConsoleColor.Magenta
            };

            int index = 0;
            foreach (var c in Resources.hacker_logo)
            {

                if (c == '\n')
                {
                    //Console.WriteLine(colors[index % colors.Length]);
                    Console.ForegroundColor = trans[index % trans.Length];
                    index++;
                }
                Console.Write(c);
            }
            Console.ResetColor();

            Console.WriteLine();
        }

    }
}
