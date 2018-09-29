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
        public vector2 renderer_worldPos;

        public SaveData(string name)
        {
            this.name = name;
            version = Program.version;
            renderer_worldPos = new vector2();
        }

        public static string getFileLoc(SaveData save)
        {
            return Program.baseFolder + @"\saves\" + save.name + "-" + save.version + "-" + ".sfs";
        }

        public static bool exists(string name)
        {
            return File.Exists(Program.baseFolder + @"\saves\" + name + "-" + Program.version + "-" + ".sfs");
        }

        public static SaveData load(string name)
        {
            string fileLoc = Program.baseFolder + @"\saves\" + name + "-" + Program.version + "-" + ".sfs";

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileLoc, FileMode.Open, FileAccess.Read, FileShare.Read);
            SaveData obj = (SaveData)formatter.Deserialize(stream);
            stream.Close();

            return obj;
        }

        public static void save(SaveData save)
        {
            IFormatter formatter = new BinaryFormatter();

            Directory.CreateDirectory(Program.baseFolder + @"\saves\");

            Stream stream = new FileStream(getFileLoc(save), FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, save);
            stream.Close();
        }
    }
}
