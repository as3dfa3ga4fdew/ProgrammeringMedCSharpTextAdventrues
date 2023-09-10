using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextAdventures.Data
{
    public class FileHandler
    {
        public static readonly string RootDirectory = "gamedata";

        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        public static T Import<T>(string filePath, bool referenceHandle)
        {
            if (!File.Exists(filePath))
                throw new Exception("Import - File does not exist.");

            string data = "";
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.Peek() >= 0)
                {
                    data += (char)reader.Read();
                }
            }

            return JsonConvert.DeserializeObject<T>(data ,JsonSettings);
        }
        public static void Export<T>(T data, string filePath)
        {
            //Create all missing directories in the provided filePath

            string directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(JsonConvert.SerializeObject(data, Formatting.Indented, JsonSettings));
            }
        }
    }
}
