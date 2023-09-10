using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventures.Data
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public bool IsCombinable { get { return RootItems != null && RootItems.Count > 0 ? true : false; } }
        public List<Item> RootItems { get; set; } = new ();

        public Item()
        {

        }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Item(string name, string description, List<Item> rootItems)
        {
            Name = name;
            Description = description;
            
            AddRootItems(rootItems);
        }

        public void AddRootItems(List<Item> rootItems)
        {
            if (rootItems.Contains(this))
                rootItems.Remove(this);

            RootItems.AddRange(rootItems);    
        }

        public void ShowDescription()
        {
            Console.WriteLine(Description);
        }
    }
}
