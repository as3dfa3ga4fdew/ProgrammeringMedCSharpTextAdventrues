using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventures.Cmd;

namespace TextAdventures.Data
{
    public partial class Game
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string EndMessage { get; set; }
        public bool EndMessageShown { get; set; }

        

        public Player Player { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Item> TreeItems { get; set; }


        public Game()
        {

        }

        public Game(string name, string description, string endMessage, List<Room> rooms, List<Item> treeItems)
        {
            Name = name;
            Description = description;
            EndMessage = endMessage;
            Rooms = rooms;
            TreeItems = treeItems;
        }

        public void SaveBuild()
        {
            FileHandler.Export(this, FileHandler.RootDirectory + "\\" + Name + "\\build.json");
        }
        public void SavePlayThrough()
        {
            FileHandler.Export(this, FileHandler.RootDirectory + "\\" + Name + "\\save.json");
        }

        public void CreateNewPlayer()
        {
            Console.Write("What would you like to name your character: ");

            Player = new Player(Console.ReadLine(), new List<Item>(), Point.Default, GetStartingRoom());
        }
        public void SetPlayerPoint(Point point)
        {
            Player.Point = point;

            if(Player.Point == Point.End && EndMessageShown == false)
            {
                EndMessageShown = true;
                Console.WriteLine(EndMessage);
                Console.WriteLine();
                Console.WriteLine(CmdResources.EndGame);
            }
        }
        private Room GetStartingRoom()
        {
            return (from room in Rooms where room.Point == Point.Start select room).First();
        }

        public bool ItemsTryCombine(List<Item> rootItems, out Item result)
        {
            result = null;

            foreach (var treeItem in TreeItems)
            {
                if (!treeItem.IsCombinable)
                    continue;

                string[] rootItemNames = (from rootItem in treeItem.RootItems select rootItem.Name).ToArray();
                string[] itemsToCombineNames = (from itemToCombine in rootItems select itemToCombine.Name).ToArray();

                if (rootItemNames.All(itemsToCombineNames.Contains) && rootItemNames.Length == itemsToCombineNames.Length)
                {
                    result = treeItem;
                    return true;
                }
            }

            return false;
        }

    }

    public partial class Game
    {
        public static List<Game> Builds { get; set; } //Base game
        public static List<Game> PlayThroughs { get; set; } //A save of base game

        public static void Initialize()
        {
            Builds = new List<Game>();
            PlayThroughs = new List<Game>();

            if (!Directory.Exists(FileHandler.RootDirectory))
                Directory.CreateDirectory(FileHandler.RootDirectory);

            foreach(var directory in Directory.GetDirectories(FileHandler.RootDirectory))
            {
                string buildPath = directory + "\\build.json";
                if (File.Exists(buildPath))
                {
                    Builds.Add(FileHandler.Import<Game>(buildPath, false));
                }

                string playThroughPath = directory + "\\save.json";
                if (File.Exists(playThroughPath))
                {
                    PlayThroughs.Add(FileHandler.Import<Game>(playThroughPath, true));
                }
            }
        }

        public static string GetAllBuildNamesAsString()
        {
            return string.Join("\n", (from build in Builds select build.Name).ToList());
        }
        public static string GetAllPlayThroughNamesAsString()
        {
            return string.Join("\n",(from playThrough in PlayThroughs select playThrough.Name).ToList());
        }
    }
}
