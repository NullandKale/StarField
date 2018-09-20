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

        static void Main(string[] args)
        {
            game.runGame();
        }
    }
}
