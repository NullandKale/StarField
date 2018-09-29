using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField.Components
{
    [Serializable]
    public class PlayerController : iComponent
    {
        public vector2 pos = new vector2();

        public void start(updateableObject gameObject)
        {
            pos = game.save.e_playerController_pos;
        }

        public void stop(updateableObject gameObject)
        {
            game.save.e_playerController_pos = pos;
        }

        public void update(updateableObject gameObject)
        {
            vector2 newPos = doInputCheck();
            if(gameObject.move(newPos))
            {
                pos = newPos;
            }
        }

        List<vector2> lastCircle = new List<vector2>();

        private vector2 doInputCheck()
        {
            vector2 newPos = new vector2(pos);
            if (game.input.IsKeyHeld(OpenTK.Input.Key.W))
            {
                newPos.y--;
            }

            if (game.input.IsKeyHeld(OpenTK.Input.Key.S))
            {
                newPos.y++;
            }

            if (game.input.IsKeyHeld(OpenTK.Input.Key.A))
            {
                newPos.x--;
            }

            if (game.input.IsKeyHeld(OpenTK.Input.Key.D))
            {
                newPos.x++;
            }

            if (game.input.IsKeyRising(OpenTK.Input.Key.Space))
            {
                for(int i = 0; i < lastCircle.Count; i++)
                {
                    game.renderer.deregisterBackground(lastCircle[i], '+');
                }
                lastCircle = utils.getCircleOn(pos.x, pos.y, 5, 1);
                for (int i = 0; i < lastCircle.Count; i++)
                {
                    game.renderer.registerBackground(lastCircle[i], '+');
                }
            }

            return newPos;
        }
    }
}
