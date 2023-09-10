using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventures.Data;

namespace TextAdventures.Cmd
{
    public class Handler
    {
        public static Mode Mode { get; set; } = Mode.Default;

        private static string _left { get; set; }
        public static string Left { get { return _left; } set { _left = "[" + value + "]"; } }
        public static string Right { get; set; } = "~$ ";

        public static string GetString()
        {
            return Left + Right;
        }

        public static void Initialize()
        {
            Game.Initialize();

            Console.Clear();
            Console.Title = "Text Adventures";
            Mode = Mode.Default;
            GameHandler.Game = null;
            BuildHandler.Game = null;
            Console.WriteLine(CmdResources.Welcome);
            Left = Mode.GetDisplayName();
        }

        public static void Loop()
        {
            Console.WriteLine();
            while (true)
            {
                Console.Write(GetString());
                string[] command = Parse(Console.ReadLine());
                ArgsSwitch(command);
                Console.WriteLine();
            }
        }

        public static void ArgsSwitch(string[] args)
        {
            switch (args.Length == 0 ? "" : args[0].ToLower())
            {
                case "help":
                    Console.WriteLine(CmdResources.Help);
                    return;
                case "select":
                    SelectMode(args);
                    return;
                case "show":
                    ShowParse(args);
                    return;
                case "clear":
                    ClearParse(args);
                    return;
                case "start":
                    GameHandler.StartParse(args);
                    return;
                case "go":
                    GameHandler.GoParse(args);
                    return;
                case "get":
                    GameHandler.GetFromParse(args);
                    return;
                case "use":
                    GameHandler.UseParse(args);
                    return;
                case "look":
                    GameHandler.LookParse(args);
                    return;
                case "inspect":
                    GameHandler.InspectParse(args);
                    return;
                case "drop":
                    GameHandler.DropParse(args);
                    return;
                case "view":
                    GameHandler.ViewParse(args);
                    return;
                default:
                    Console.WriteLine("Invalid args !");
                    return;
            }


        }

        private static void ShowParse(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if(Mode == Mode.Default)
            {
                Console.WriteLine(CmdResources.InvalidMode);
                return;
            }

            if (Mode.NewGame == Mode)
            {
                Console.WriteLine("Adventures available");
                Console.WriteLine(Game.GetAllBuildNamesAsString());
            }

            if (Mode.LoadGame == Mode)
            {
                Console.WriteLine("Saved adventures available");
                Console.WriteLine(Game.GetAllPlayThroughNamesAsString());
            }

            return;
        }

        public static void SelectMode(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if (!ModeHandler.StringIndexTryParse(args[1], out Mode result))
            {
                Console.WriteLine(CmdResources.InvalidArgs);
                return;
            }

            Mode = result;
            Left = result.GetDisplayName();
            Console.WriteLine("Mode: " + result.GetDisplayName());

            return;
        }

        public static void ClearParse(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            Initialize();
        }

        public static string[] Parse(string text = "")
        {
            if (!text.Contains(" "))
                return new string[] { text.Replace("\"", "") };

            bool specChar = false;
            string tempText = "";
            List<string> parsedText = new List<string>();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (c == '\"' && specChar == false)
                {
                    specChar = true;

                    continue;
                }

                if (c == '\"' && specChar == true)
                {
                    specChar = false;

                    parsedText.Add(tempText);
                    tempText = string.Empty;
                    i++;
                    continue;
                }

                if (specChar)
                {
                    tempText += c;
                    continue;
                }

                if (c == ' ')
                {
                    parsedText.Add(tempText);
                    tempText = string.Empty;
                    continue;
                }

                tempText += c;

            }

            if (tempText.Length != 0)
                parsedText.Add(tempText);

            return parsedText.ToArray();
        }
    }

    public enum Mode
    {
        [Display(Name = "Default")]
        Default = 0,
        [Display(Name = "New game")]
        NewGame = 1,
        [Display(Name = "Load game")]
        LoadGame = 2,
    }

}
