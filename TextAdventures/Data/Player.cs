using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventures.Data
{
    public class Player
    {
        public string Name { get; set; }
        public List<Item> Inventory { get; set; }
        public Point Point { get; set; }
        public Room CurrentRoom { get; set; }

        public Player()
        {

        }

        public Player(string name, List<Item> inventory, Point point, Room currentRoom)
        {
            Name = name;
            Inventory = inventory;
            Point = point;
            CurrentRoom = currentRoom;
        }

        public void View()
        {
            if (Inventory.Count == 0)
            {
                Console.WriteLine("You have no items in your inventory...");
                return;
            }
                

            Console.WriteLine("Items in your inventory");
            Console.WriteLine(string.Join("\n", Inventory.Select(x => x.Name)));

            return;
        }
    }
}
