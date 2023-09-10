using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventures.Data;

namespace TextAdventures.Creations
{
    public static class KillTheDungeonDragon
    {
        public static void Build()
        {
            Item key = new Item("Key", "This key is able to unlock a door...");
            Item knife = new Item("Knife", "This knife can kill smaller enemies...");
            Item bow = new Item("Super bow", "This bow is useless on its own but can kill anything together with an arrow...");
            Item arrow = new Item("Super arrow", "This arrow needs a bow...");
            Item arrowBow = new Item("Loaded super bow", "This bow can kill anything but breaks after one use...", new List<Item>() { arrow, bow });


            RoomObject shelf = new RoomObject("Shelf", "A small shelf", new List<Item>() { key });
            RoomObject desk = new RoomObject("Desk", "A small deskt", new List<Item>() { knife });
            RoomObject safe = new RoomObject("Safe", "A huge safe", new List<Item>() { bow });


            Enemy zombie = new Enemy("Zombie", "The zombie makes unhuman noises...", Point.Default, knife, arrow);
            Enemy dragon = new Enemy("Dragon", "A huge dragon that spits fire...", Point.End, arrowBow);

            Room bedroom = new Room("Bedroom", "You are standing in your bedroom after waking up to strange noises...", Point.Start, null, null);
            Room kitchen = new Room("Kitchen", "The kitchen is small and you hear the strange noises even louder than before...", Point.Default, key, null);
            Room livingroom = new Room("Living room", "The strange noice from before is not as loud in the living room...", Point.Default, null, null);
            Room corridor = new Room("Corridor", "A long room, the noises are getting louder...", Point.Default, null, null);
            Room basement = new Room("Basement", "The basement is dark and you can hear the strange noises as if they were coming infront of you...", Point.Default, null, zombie);
            Room dungeon = new Room("Dungeon", "Its hot and dark in the dungeon you hear even louder noises than before...", Point.Default, null, dragon);

            bedroom.AddExits(new List<Room>() { kitchen });
            kitchen.AddExits(new List<Room>() { bedroom, livingroom, corridor });
            livingroom.AddExits(new List<Room>() { kitchen });
            corridor.AddExits(new List<Room>() { kitchen, basement });
            basement.AddExits(new List<Room>() { corridor, dungeon });
            dungeon.AddExits(new List<Room>() { basement });

            bedroom.AddRoomObjects(new List<RoomObject>() { shelf });
            kitchen.AddRoomObjects(new List<RoomObject>() { desk });
            livingroom.AddRoomObjects(new List<RoomObject>() { safe });

            Game build = new Game("Investigate the strange noises", 
                "You woke up from strange noises and got angry so you started following the noice and what you discovered felt like a nightmare...",
                "As you look at the dead dragon you feel relieved that you can finally get back to sleep.",
                new List<Room>() { bedroom, kitchen, livingroom, corridor, basement, dungeon},
                new List<Item>() { arrowBow});

            build.SaveBuild();
        }

        
    }
}
