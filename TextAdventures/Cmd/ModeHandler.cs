using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventures.Cmd
{
    public static class ModeHandler
    {

        public static string GetDisplayName(this Mode enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
        public static bool DisplayNameTryParse(string displayName, out Mode result)
        {
            result = Mode.Default;

            foreach (var mode in GetAllModes())
            {
                if (mode.GetDisplayName().ToLower() == displayName.ToLower())
                {
                    result = mode;
                    return true;
                }
            }

            return false;
        }

        public static bool IndexTryParse(int index, out Mode result)
        {
            result = Mode.Default;

            foreach (var mode in GetAllModes())
            {
                if ((int)mode == index)
                {
                    result = mode;
                    return true;
                }
            }

            return false;
        }

        public static bool StringIndexTryParse(string index, out Mode result)
        {
            result = Mode.Default;

            if (!int.TryParse(index, out int i))
            {
                return false;
            }

            foreach (var mode in GetAllModes())
            {
                if ((int)mode == i)
                {
                    result = mode;
                    return true;
                }
            }

            return false;
        }

        public static Mode[] GetAllModes()
        {
            return (Mode[])Enum.GetValues(typeof(Mode));
        }

        
    }
}
