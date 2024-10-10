using System;
using System.Collections.Generic;

namespace Text_Adventure
{
    class Program
    {
        //Time for

        static Random random = new Random();

        static bool first = true;
        static ConsoleColor[] displayColors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Yellow };

        static string[] gameColors = new string[] {
            "[1] Red   ",
            "[2] Green ",
            "[3] Blue  ",
            "[4] Yellow",
        };

        static List<string> combo = new List<string>();
        static List<int> comboInt = new List<int>();

        static List<string> userCombo = new List<string>();

        static bool ctrls;

        static string[] busta = new string[] {
            "TWFuLCB3aGF0IHlvdSBkb2luPyBEaWdnaW5nIGdyYXZlcz8=",
            "VGhpcyB0aGUgYmVzdCB1IGdvdD8gZnJlYWtpbicgZm9vbCE=",
            "VSBnb3QgY2F1Z2h0IHNsaXBwaW4nIEch",
            "VSBnb3QgY2F1Z2h0IHNsaXBwaW4nIGFnYWluIQ==",
            "UHVsbCB1ciBzZWxmIHRvZ2V0aGVyIGZvb2wh",
            "Qydtb24gZm9vbCEgSXQncyBlbWJhcnJhc3NpbmcgdG8gYmUgc2VlbiBhcm91bmQgeW91IQ==",
            "dSBnb25uYSBoYXZlIHRvIHRyeSBhZ2FpbiBTdWNrYSE=",
            "dSBjb3VsZCBydW4gb3IgZ2V0IGEgYmVhdGluLSBlYXN5IGNob2ljZSBodWg/",
            "SSdkIGdpdmUgdXAgbm93IGlmIEkgd2FzIHUh",
            "eW8gd2lmZSBpcyBsb25lbHkuIA==",
            "a2VlcCBkb2luZyB0aGF0IHUgZ29ubmEgbWFrZSBiYWJpZXMgY3J5IHdoZW4gdGhleSBzZWUgdXIgZmFjZS4=",
            "R28gYm91bmNlIG9uIHlvIGRhZGRpZXMga25lZSBmb29sIQ==",
            "UGxheWFoLCB1IHJ1aW5pbicgbXkgaHVzdGxl",
            "WU9VIEJPUklORyBNRSBUTyBURUFSUyEh",
            "QWluJ3QgeW91IGdvdCBhIHJlYWRpbmcgbGVzc2lvbiBvciBzb21ldGhpbmc/",
            "b2ggeW91IHJlYWwgaGFyZCBodWg/IQ==",
            "ciB1IHRyeWluZyB0byBnZXQgbGF5ZWQgb3V0Pz8=",
            "SXQncyB5b3VyIEZ1bmVyYWwgRm9vbCE=",
            "ZGlkIGEgY2F0IHN0ZXAgb24geW8ga2V5Ym9yZD8=",
            "ciB1IHRyeWluZyB0byBnZXQgdXJzZWxmIHBpbmNoZWQ/IQ==",
            "U3RyYWlnaHQgQnVzdGE=",
            "R2V0IGEgaG9sZCBvZiB5b3Vyc2VsZiBGb29sIQ==",
        };


        //hfu pvu

        static void Main()
        {
            //Style(ConsoleColor.DarkRed);
            //WriteOut(AtobStr("R2V0IG91dA=="), 0.001f);
            Clear(null, true);

            StartUp(() => {
                NewColor();
                ListenInput();
            });
        }



        static void ListenInput()
        {
            //go away buffered key
            if (Console.KeyAvailable)
                Console.ReadKey(true);

            ctrls = true;

            while (ctrls) {
                var key = Console.ReadKey(true).Key;

                if (key != ConsoleKey.Escape)
                {
                    Dictionary<ConsoleKey, Action> keyColor = new Dictionary<ConsoleKey, Action>()
                    {
                        { ConsoleKey.D1, () => Respond(0) },
                        { ConsoleKey.D2, () => Respond(1) },
                        { ConsoleKey.D3, () => Respond(2) },
                        { ConsoleKey.D4, () => Respond(3) },
                    };

                    keyColor.TryGetValue(key, out Action action);

                    if (action != null)
                        action.Invoke();
                }
            }
        }

        static void Respond(int colorKey)
        {
            userCombo.Add(gameColors[colorKey]);

            Shine("", colorKey, 0.05f);


            int numberPlrColors = userCombo.Count - 1;

            //loser
            if (userCombo[numberPlrColors] != combo[numberPlrColors])
            {
                Clear(null, true);
               
                WriteOut("BAD BOY! ");
                Wait(0.375f);
                WriteOut("It was ", 0.001f);
                Style(displayColors[comboInt[userCombo.Count - 1]]);
                WriteOut(combo[numberPlrColors].ToUpper(), 0.001f); //reveal color

                Style(ConsoleColor.Red);
                int failureYou = GimmeRandom(0, busta.Length - 1);

                for (int i = 0; i < 3; i++)
                    WriteOut("\n" + AtobStr(busta[failureYou]) + (i > 1 ? "               " : ""), 0.001f);
               
                Style();
                Console.WriteLine("\n\nu got to level " + combo.Count + "!");

                combo.Clear();
                comboInt.Clear();

                StartUp(() => {
                    userCombo.Clear();
                    NewColor();
                });
            }

            //good
            if (userCombo.Count == combo.Count)
                NewColor();
        }

        static void NewColor()
        {
            ctrls = false;

            Clear(null, true);
           
            userCombo.Clear();
           
            if (combo.Count > 0)
                WriteOut("GOOD BOY.", 0.001f);

            Clear();

            Wait(0.375f);

            //add new random color
            int randomColor = GimmeRandom(0, gameColors.Length);
            combo.Add(gameColors[randomColor]);
            comboInt.Add(randomColor);

            //small wait
            Shine();
            Wait(0.15f);

            //animate
            for (int i = 0; i < combo.Count; i++)
            {
                Wait(0.15f);
                Shine(combo[i]);
            }

            ListenInput();
        }

        static void Shine(string comboColor = "", int colorKey = -1, float timeout = 0.375f)
        {
            ConsoleColor flashColor = ConsoleColor.White;

            bool directName = (comboColor != "");
            bool validKey = (colorKey >= 0);

            Clear();

            Console.WriteLine("Level "+ combo.Count);

            for (int i = 0; i < gameColors.Length; i++)
            {
                bool match = (!validKey) ?
                    comboColor.ToLower() == gameColors[i].ToLower() : //check using direct name
                    gameColors[colorKey].ToLower() == gameColors[i].ToLower(); //check using index

                Style(match ? flashColor : displayColors[i]); //color
                string label = (match ? "      " : "    ") + (!match ? gameColors[i] : gameColors[i].ToUpper()); //string display

                Console.WriteLine(label);
            }

            if (!directName && !validKey) return;
            Clear();

            float faster = (directName ? (0.0375f * Math.Clamp(combo.Count, 0, 5)) : 0); //speed handler

            Wait(timeout - faster);
            Shine();
        }


        static void StartUp(Action action)
        {
            Inform();

            while (true)
            {
                var input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.Enter || input == ConsoleKey.Spacebar) {
                    Clear(action); //clear and run play action
                    break;
                }
            }

            void Inform()
            {
                Style(ConsoleColor.Cyan);
                if (first)
                    Console.WriteLine("\nPlease type the corresponding number to respond.\n\n");
               
                Console.WriteLine("Press [Enter] to Play" + (!first ? " again." : "."));

                first = false;
            }
        }


        static void Clear(Action? action = null, bool hard = false)
        {
            if (hard)
            {
                Console.Clear();
            }
            else
            {
                for (int i = 0; i < 5; i++) {
                    Console.WriteLine(new string(System.Convert.ToChar(" "), Console.WindowWidth));
                }

                Console.SetCursorPosition(0, 0);
            }
           

            Style();

            if (action != null)
                action.Invoke();
        }

        static void Style(ConsoleColor color = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black) {
            Console.ForegroundColor = color;
            Console.BackgroundColor = background;
        }

        static void WriteOut(string str, float timeout = 0.05f)
        {
            for (int i = 0; i < str.Length; i++) {
                Wait(timeout);
                Console.Write(str[i]);
            }
        }

        static int GimmeRandom(int min, int max) {
            return random.Next(min, max);
        }

        static void Wait(float timeout = 0.01f) {
            System.Threading.Thread.Sleep((int)(timeout * 1000));
        }

        static string AtobStr(string str)
        {
            byte[] tt = System.Convert.FromBase64String(str);
            string encodedStr = System.Text.Encoding.UTF8.GetString(tt);

            return encodedStr;
        }
    }
}