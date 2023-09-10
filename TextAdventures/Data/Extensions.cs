using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextAdventures.Data
{
    public static class Extensions
    {
        

        private readonly static char[] Vowels = new char[] { 'a', 'e', 'i', 'o', 'u'};
        public static string GetArticle(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            if (Vowels.Contains(s[0]))
                return "an";
            else
                return "a";
        }

        public static bool TryGetByName<T>(this List<T> list, string name, out T result)
        {
            //fix
            result = default(T);

            foreach (var item in list)
            {
                if (item.GetType().GetProperty("Name") == null)
                    break;

                dynamic dynamicItem = item;

                if (dynamicItem.Name == name)
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }
    }
}
