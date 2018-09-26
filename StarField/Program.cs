using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField
{
    class Program
    {
        public static string baseFolder = AppDomain.CurrentDomain.BaseDirectory + @"\saves";
        public static int randomSeed = 5;

        static void Main(string[] args)
        {
            game.runGame();
        }
    }
}
