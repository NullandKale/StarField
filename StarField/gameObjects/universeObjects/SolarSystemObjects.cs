using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField.gameObjects.universeObjects
{
    [Serializable]
    public class Sun : CircleGameObject
    {
        public Sun(bool active, vector2 pos, char toDisplay, int radius) : base(active, pos, toDisplay, radius)
        {

        }
    }

    [Serializable]
    public class Planet : updateableObject
    {
        private bool isOrbitDrawn = false;
        public List<vector2> orbit;

        public Planet(bool active, vector2 pos, char toDisplay, Sun sun, List<vector2> orbit) : base(active, pos, toDisplay)
        {
            this.orbit = orbit;
            pos = utils.RandomValueFromList(orbit);
        }

        public void drawOrbitOnBackground()
        {
            if(!isOrbitDrawn)
            {
                for (int i = 0; i < orbit.Count; i++)
                {
                    if (game.renderer.isPosEmptyBackground(orbit[i]))
                    {
                        game.renderer.registerBackground(orbit[i], '-');
                    }
                }
                isOrbitDrawn = true;
            }
            else
            {
                for (int i = 0; i < orbit.Count; i++)
                {
                    if (!game.renderer.isPosEmptyBackground(orbit[i]))
                    {
                        game.renderer.deregisterBackground(orbit[i], '-');
                    }
                }
                isOrbitDrawn = false;
            }
        }

        public override void update()
        {
            if(playerGameObject.pc.pos.dist(pos) < 100)
            {
                if(!isOrbitDrawn)
                {
                    drawOrbitOnBackground();
                }
            }
            else
            {
                if (isOrbitDrawn)
                {
                    drawOrbitOnBackground();
                }
            }

            base.update();
        }

        public override bool move(vector2 newPos)
        {
            return base.move(newPos);
        }
    }

    [Serializable]
    public class CircleGameObject : updateableObject
    {
        public List<vector2> matter;
        public int radius;

        public CircleGameObject(bool active, vector2 pos, char toDisplay, int radius) : base(active, pos, toDisplay)
        {
            this.radius = radius;
            matter = utils.getCircleOn(pos.x, pos.y, radius, -1);
        }

        public override bool move(vector2 newPos)
        {
            for(int i = 0; i < matter.Count; i++)
            {
                if(!game.renderer.isPosEmpty(pos))
                {
                    return false;
                }
            }

            for (int i = 0; i < matter.Count; i++)
            {
                game.renderer.deregister(matter[i], toDisplay);
            }

            matter = utils.getCircleOn(pos.x, pos.y, radius, -1);

            for (int i = 0; i < matter.Count; i++)
            {
                game.renderer.register(matter[i], toDisplay);
            }

            return true;
        }
    }
}
