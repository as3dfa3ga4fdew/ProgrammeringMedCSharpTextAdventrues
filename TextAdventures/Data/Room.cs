using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventures.Data
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Point Point { get; set; }

        public bool HasEnemy { get { return Enemy != null && Enemy.IsKilled != true ? true : false; } }
        public Enemy Enemy { get; set; }

        public List<RoomObject> RoomObjects { get; set; } = new();

        public List<Room> Exits { get; set; } = new ();

        public bool IsLocked { get { return Key != null ? true : false; } }
        public Item Key { get; set; }

        public Room()
        {

        }

        public Room(string name, string description, Point point, Item key, Enemy enemy)
        {
            Name = name;
            Description = description;
            Point = point;
            Key = key;
            Enemy = enemy;
        }


        public void AddExits(List<Room> exits)
        {
            if (exits.Contains(this))
                exits.Remove(this);

            Exits.AddRange(exits);
        }
        public void AddRoomObjects(List<RoomObject> roomObjects)
        {
            RoomObjects.AddRange(roomObjects);
        }

        public bool Unlock(Item item)
        {
            if (Key != item)
                return false;

            Key = null;

            return true;
        }

        public void Look()
        {
            ShowDescription();
            Console.WriteLine();

            string roomObjectInfo = "";
            if (RoomObjects.Count == 0)
                roomObjectInfo = "There are no room items in the " + Name + "...";
            else if (RoomObjects.Count == 1)
                roomObjectInfo = "There is " + RoomObjects.First().Name.GetArticle() + " " + RoomObjects.First().Name;
            else if (RoomObjects.Count == 2)
                roomObjectInfo = "There is " + RoomObjects.First().Name.GetArticle() + " " + RoomObjects.First().Name + " and " + RoomObjects[^1].Name.GetArticle() + " " + RoomObjects[^1].Name;
            else
            {
                List<string> names = (from roomObject in RoomObjects select roomObject.Name).ToList();

                string firstName = names.First();
                string lastName = names[^1];

                names.RemoveAt(0);
                names.RemoveAt(names.Count - 1);

                roomObjectInfo = "There is " + firstName.GetArticle() + " " + firstName + ", " + string.Join(", ", names) + " and " + lastName.GetArticle() + " " + lastName;
            }

            Console.WriteLine(roomObjectInfo);

            string exitInfo = "";
            if (Exits.Count == 0)
                exitInfo = "There are no doors...";
            else if (Exits.Count == 1)
                exitInfo = "There is " + Exits.First().Name.GetArticle() + " " + Exits.First().Name + " door";
            else if (Exits.Count == 2)
                exitInfo = "There is " + Exits.First().Name.GetArticle() + " " + Exits.First().Name + " door and " + Exits[^1].Name.GetArticle() + " " + Exits[^1].Name + " door"; 
            else
            {
                List<string> names = (from exit in Exits select exit.Name).ToList();

                string firstName = names.First();
                string lastName = names[^1];

                names.RemoveAt(names.Count - 1);

                exitInfo = "There is " + firstName.GetArticle() + " " + string.Join(" door, ", names) + " door and " + lastName.GetArticle() + " " + lastName + " door";
            }

            Console.WriteLine(exitInfo);


            if(HasEnemy)
            {
                Console.WriteLine("There is " + Enemy.Name.GetArticle() + " " + Enemy.Name);
            }


            return;
        }

        public void ShowDescription()
        {
            Console.WriteLine(Description);
        }

        public void Inspect()
        {
            Console.WriteLine("Exit to the " + Name);
        }
    }

    public enum Point
    {
        Start,
        End,
        Default
    }
}
