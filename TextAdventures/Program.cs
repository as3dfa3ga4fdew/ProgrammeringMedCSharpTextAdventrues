using TextAdventures.Cmd;
using TextAdventures.Creations;
using TextAdventures.Data;

namespace TextAdventures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            KillTheDungeonDragon.Build();

            Handler.Initialize();
            Handler.Loop();
        }
    }
}