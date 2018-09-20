using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField
{
    public class game
    {
        public static bool run = true;

        public static void runGame()
        {
            Renderer renderer = Renderer.getInstance();
            Updater updater = Updater.getInstance();

            while(run)
            {
                updater.doUpdate();
                renderer.renderFrame();
            }
        }
    }
}
