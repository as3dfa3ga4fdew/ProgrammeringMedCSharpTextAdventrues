using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventures.Data
{
    public class RoomObject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Item> Items { get; set; } = new ();

        public RoomObject()
        {

        }

        public RoomObject(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public RoomObject(string name, string description, List<Item> items)
        {
            Name = name;
            Description = description;
            Items = items;
        }

        public void AddItems(List<Item> items)
        {
            Items.AddRange(items);
        }

        public void Inspect()
        {
            //After lookin closely you notice that there is a : an 

            string info = "After looking closely you notice that there ";
            if (Items.Count == 0)
                info += "are no useful items there...";
            else if (Items.Count == 1)
                info += "is " + Items.First().Name.GetArticle() + " " + Items.First().Name;
            else if (Items.Count == 2)
                info += "is " + Items.First().Name.GetArticle() + " " + Items.First().Name + " and " + Items[^1].Name.GetArticle() + " " + Items[^1].Name;
            else
            {
                List<string> names = (from item in Items select item.Name).ToList();

                string lastName = names[^1];
                names.Remove(lastName);

                info = "is " + names.First().GetArticle() + " " + string.Join(", ", names) + " and " + lastName.GetArticle() + " " + lastName;
            }
            Console.WriteLine(info);
        }
    }
}
