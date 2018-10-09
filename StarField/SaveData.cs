using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StarField
{
    [Serializable]
    public struct SaveData
    {
        public string name;
        public string version;

        //game.universe
        public universe.UniverseData universe_data;

        //game.renderer
        public vector2 renderer_worldPos;

        //game.updater
        public Dictionary<vector2, updateableObject> updater_gameObjects;

        //e.Player
        public vector2 e_playerController_pos;

        public SaveData(string name)
        {
            this.name = name;
            version = Program.version;
            universe_data = new universe.UniverseData();
            renderer_worldPos = new vector2();
            e_playerController_pos = new vector2();
            updater_gameObjects = new Dictionary<vector2, updateableObject>();
        }

        public static string getFileLoc(SaveData save)
        {
            return Program.baseFolder + save.name + "-" + save.version + "-" + ".sfs";
        }

        public static bool exists(string name)
        {
            return File.Exists(Program.baseFolder + name + "-" + Program.version + "-" + ".sfs");
        }

        public static List<String> getFiles()
        {
            string fileLoc = Program.baseFolder;

            List<String> saves = new List<string>();

            String[] files = Directory.GetFiles(fileLoc);

            for(int i = 0; i < files.Length; i++)
            {
                string[] split = files[i].Split('\\').Last().Split('-');

                if(split.Length == 3)
                {
                    if(split[2] == ".sfs")
                    {
                        saves.Add("(" + saves.Count + ") : " + split[0] + " version: " + split[1]);
                    }
                }
            }

            return saves;
        }

        public static SaveData load(string name)
        {
            string fileLoc = Program.baseFolder + name + "-" + Program.version + "-" + ".sfs";

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileLoc, FileMode.Open, FileAccess.Read, FileShare.Read);
            SaveData obj = (SaveData)formatter.Deserialize(stream);
            stream.Close();

            return obj;
        }

        public static void save(SaveData save)
        {
            IFormatter formatter = new BinaryFormatter();

            Directory.CreateDirectory(Program.baseFolder);

            Stream stream = new FileStream(getFileLoc(save), FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, save);
            stream.Close();
        }
    }
}
