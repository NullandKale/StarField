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
            InputManager input = InputManager.getInstance();

            updateableObject o = new updateableObject(true, new vector2(), '!');

            while(run)
            {
                updater.doUpdate();
                renderer.renderFrame(false);
                input.Update();

                if (input.IsKeyHeld(OpenTK.Input.Key.Up))
                {
                    renderer.worldPos.y--;
                }

                if (input.IsKeyHeld(OpenTK.Input.Key.Down))
                {
                    renderer.worldPos.y++;
                }

                if (input.IsKeyHeld(OpenTK.Input.Key.Left))
                {
                    renderer.worldPos.x--;
                }

                if (input.IsKeyHeld(OpenTK.Input.Key.Right))
                {
                    renderer.worldPos.x++;
                }
            }
        }
    }
}
