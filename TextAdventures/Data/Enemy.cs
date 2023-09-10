using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventures.Data
{
    public class Enemy
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool HasDrop { get { return Drop != null ? true : false; } }
        public Item Drop { get; set; }

        public Point Point { get; set; }

        public Item Counter { get; set; }
        public bool IsKilled { get; set; }


        public Enemy()
        {

        }
        public Enemy(string name, string description, Point point, Item counter)
        {
            Name = name;
            Description = description;
            Point = point;
            Counter = counter;
        }
        public Enemy(string name, string description, Point point, Item counter, Item drop)
        {
            Name = name;
            Description = description;
            Point = point;
            Counter = counter;
            Drop = drop;
        }

        public void Inspect()
        {
            Console.WriteLine(Description);
        }
    }
}
