using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TextAdventures.Data;

namespace TextAdventures.Cmd
{
    public static class GameHandler
    {
        public static Game Game { get; set; }


        public static void ViewParse(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if (Game == null)
            {
                Console.WriteLine(CmdResources.GameNotStarted);
                return;
            }

            if (args[1].ToLower() != "inventory")
            {
                Console.WriteLine(CmdResources.InvalidArgs);
                return;
            }

            Game.Player.View();
        }

        public static void DropParse(string[] args)
        {
            //Drop [Item name] - Drops the item from your inventory on the exit floor

            if (args.Length != 2)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if (Game == null)
            {
                Console.WriteLine(CmdResources.GameNotStarted);
                return;
            }

            if (!Game.Player.Inventory.TryGetByName(args[1], out Item result))
            {
                Console.WriteLine("You tried to take " + args[1] + " from your inventory but you had none...");
                return;
            }

            //create or add the item to floor exit object
            if(Game.Player.CurrentRoom.RoomObjects.TryGetByName("Floor", out RoomObject floor))
            {
                floor.Items.Add(result);
                Game.Player.Inventory.Remove(result);

                Console.WriteLine("You dropped your item on the exit floor...");
                Game.SavePlayThrough();
                return;
            }


            RoomObject roomObject = new RoomObject("Floor", "The rooms floor...");
            roomObject.Items.Add(result);
            Game.Player.Inventory.Remove(result);
            Game.Player.CurrentRoom.RoomObjects.Add(roomObject);
            Game.SavePlayThrough();
            Console.WriteLine("You dropped your item on the exit floor...");

            return;
        }

        public static void InspectParse(string[] args)
        {
            //Inspect [Room object name/Item name/Exit name] - Shows a detailed description
            if (args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if (Game == null)
            {
                Console.WriteLine(CmdResources.GameNotStarted);
                return;
            }



            if (Game.Player.CurrentRoom.RoomObjects.TryGetByName(args[1], out RoomObject roomObject))
            {
                roomObject.Inspect();
                return;
            }

            if (Game.Player.Inventory.TryGetByName(args[1], out Item item))
            {
                item.ShowDescription();
                return;
            }

            if (Game.Player.CurrentRoom.Exits.TryGetByName(args[1], out Room exit) && args.Length == 3 && args[2].ToLower() == "door") 
            {
                exit.Inspect();
                return;
            }

            if (Game.Player.CurrentRoom.HasEnemy && Game.Player.CurrentRoom.Enemy.Name == args[1])
            {
                Game.Player.CurrentRoom.Enemy.Inspect();
                return;
            }

            Console.WriteLine("You tried to inspect something that does not exit...");
        }

        public static void LookParse(string[] args)
        {
            //Look - Shows all exit objects and exits in the current exit
            if (args.Length != 1)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if (Game == null)
            {
                Console.WriteLine(CmdResources.GameNotStarted);
                return;
            }

            Game.Player.CurrentRoom.Look();
            
            return;
        }

        public static void UseParse(string[] args)
        {
            //UseParse [Item] on [Room name: door/Item name/Enemy name] - 
            //Uses an item from your inventory to unlock the next exit, combine items or kill and enemy

            if(args.Length < 4 || args.Length > 5)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if (Game == null)
            {
                Console.WriteLine(CmdResources.GameNotStarted);
                return;
            }

            if (args[0] != "use" && args[2].ToLower() != "on")
            {
                Console.WriteLine(CmdResources.InvalidArgs);
                return;
            }

            if (!Game.Player.Inventory.TryGetByName(args[1], out Item arg1Item))
            {
                Console.WriteLine("You tried to take " + args[1].GetArticle() + " " + args[1] + " from your inventory but you had none...");
                return;
            }

            //unlock exit handle
            if (Game.Player.CurrentRoom.Exits.TryGetByName(args[3], out Room room) && args.Length == 5 && args[4].ToLower() == "door")
            {
                if(!room.IsLocked)
                {
                    Console.WriteLine("You tried to unlock the exit to the " + room.Name + " but it's already unlocked...");
                    return;
                }

                if(!room.Unlock(arg1Item))
                {
                    Console.WriteLine("Hmmm seems to be the wrong key...");
                    return;
                }

                Game.Player.Inventory.Remove(arg1Item);
                Game.SavePlayThrough();
                Console.WriteLine("You unlocked the " + room.Name + " !");

                return;
            }

            //combine items handle
            if(Game.Player.Inventory.TryGetByName(args[3], out Item arg3Item))
            {
                if(!Game.ItemsTryCombine(new List<Item>() { arg1Item, arg3Item }, out Item result))
                {
                    Console.WriteLine("You tried to combine " + arg1Item.Name + " and " + arg3Item.Name + " but it didn't work...");
                    return;
                }

                Game.Player.Inventory.Remove(arg1Item);
                Game.Player.Inventory.Remove(arg3Item);

                Game.Player.Inventory.Add(result);
                Game.SavePlayThrough();
                Console.WriteLine("You successfully combined " + arg1Item.Name + " with " + arg3Item.Name + " to get " + result.Name.GetArticle() + " " + result.Name + " !");
                return;
            }

            //Enemy handle
            if (Game.Player.CurrentRoom.HasEnemy)
            {
                Enemy enemy = Game.Player.CurrentRoom.Enemy;

                if (arg1Item.Name != enemy.Counter.Name)
                {
                    Console.WriteLine("You tried to kill the enemy but it seems like you used the wrong item...");
                    return;
                }

                Game.Player.Inventory.Remove(arg1Item);
                enemy.IsKilled = true;

                Console.WriteLine("You killed the " + enemy.Name + " !");
                Console.WriteLine("It dropped " + (enemy.HasDrop ? enemy.Drop.Name.GetArticle() + " " + enemy.Drop.Name : "nothing..."));

                if (enemy.HasDrop)
                    Game.Player.Inventory.Add(enemy.Drop);

                Game.SetPlayerPoint(enemy.Point);
                Game.SavePlayThrough();
                return;
            }

            Console.WriteLine("You can't use " + args[1] + " on " + args[3] + " !");
        }

        public static void GetFromParse(string[] args)
        {
            //Get [Item name] from [Room object name] - Your character picks up an item from a exit object and places it in your inventory
            if (args.Length != 4)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if (Game == null)
            {
                Console.WriteLine(CmdResources.GameNotStarted);
                return;
            }

            if (args[0] != "get" && args[2].ToLower() != "from")
            {
                Console.WriteLine(CmdResources.InvalidArgs);
                return;
            }

            if (!Game.Player.CurrentRoom.RoomObjects.TryGetByName(args[3], out RoomObject roomObject))
            {
                Console.WriteLine(args[3].GetArticle().ToUpper() + " " +  args[3] + " does not exist in the exit !");
                return;
            }

            if (!roomObject.Items.TryGetByName(args[1], out Item item))
            {
                Console.WriteLine("The item is not there !");
                return;
            }

            roomObject.Items.Remove(item);

            //Special exit object floor
            if(roomObject.Name == "Floor" && roomObject.Items.Count == 0)
            {
                Game.Player.CurrentRoom.RoomObjects.Remove(roomObject);
            }

            Game.Player.Inventory.Add(item);

            Game.SavePlayThrough();

            Console.WriteLine("You picked up the item...");

            return;
        }

        public static void GoParse(string[] args)
        {
            //Go to [Room name] - Moves your character to the next exit - Ex. Go to kitchen

            if (args.Length != 3)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if(Game == null)
            {
                Console.WriteLine(CmdResources.GameNotStarted);
                return;
            }
;
            if (args[0] != "go" && args[1].ToLower() != "to")
            {
                Console.WriteLine(CmdResources.InvalidArgs);
                return;
            }

            if (!Game.Player.CurrentRoom.Exits.TryGetByName(args[2], out Room room))
            {
                Console.WriteLine("Room \"" + args[2] +"\" does not exist !");
                return;
            }

            if(room.IsLocked)
            {
                Console.WriteLine("The door to the " + room.Name + " is locked...");
                return;
            }

            Game.Player.CurrentRoom = room;
            Game.SetPlayerPoint(room.Point);

            Game.Player.CurrentRoom.ShowDescription();

            Game.SavePlayThrough();

            return;
        }

        public static void StartParse(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(CmdResources.InvalidLength);
                return;
            }

            if(Handler.Mode == Mode.Default)
            {
                Console.WriteLine(CmdResources.InvalidMode);
                return;
            }

            if(Handler.Mode == Mode.NewGame)
            {
                if (!Game.Builds.TryGetByName(args[1], out Game result))
                {
                    Console.WriteLine("Game \"" + args[1] + "\" does not exit !");
                    return;
                }

                Game = result;

                Game.CreateNewPlayer();

                Handler.Left = Game.Player.Name;

                Game.Player.CurrentRoom.ShowDescription();

                Game.SavePlayThrough();

                return;
            }


            if(Handler.Mode == Mode.LoadGame)
            {
                if (!Game.PlayThroughs.TryGetByName(args[1], out Game result))
                {
                    Console.WriteLine("Playthorugh \"" + args[1] +"\" does not exit !");
                    return;
                }

                Game = result;
                Handler.Left = Game.Player.Name;

                return;
            }

            Console.WriteLine(CmdResources.InvalidArgs);
        }
    }
}
