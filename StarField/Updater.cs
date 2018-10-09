using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField
{
    public class Updater
    {
        private static Updater sharedInstance;

        public static Updater getInstance()
        {
            if (sharedInstance == null)
            {
                sharedInstance = new Updater();
            }

            return sharedInstance;
        }

        public void init()
        {
            //Setup New Game
            updateableObject pc = new gameObjects.playerGameObject(new vector2(0, 0));
            pc.components.Add(new Components.PlayerController());

            doStart();
        }

        public void save()
        {
            game.save.updater_gameObjects = gameObjects;
        }

        public void load()
        {
            gameObjects = game.save.updater_gameObjects;
            doStart();
        }

        private Updater()
        {
            gameObjects = new Dictionary<vector2, updateableObject>();
        }

        private Dictionary<vector2, updateableObject> gameObjects;

        public void doStart()
        {
            foreach (KeyValuePair<vector2, updateableObject> kvp in gameObjects)
            {
                kvp.Value.start();
                kvp.Value.move(kvp.Value.pos);
            }
        }


        public void doUpdate()
        {
            foreach(KeyValuePair<vector2, updateableObject> kvp in gameObjects)
            {
                kvp.Value.update();
            }
        }

        public void registerUpdateableObject(updateableObject toRegister)
        {
            if (!gameObjects.ContainsKey(toRegister.pos))
            {
                gameObjects.Add(toRegister.pos, toRegister);
                Renderer.getInstance().register(toRegister.pos, toRegister.toDisplay);
            }
        }

        public void deregisterUpdateableObject(updateableObject toDeregister)
        {
            if(gameObjects.ContainsKey(toDeregister.pos))
            {
                gameObjects.Remove(toDeregister.pos);
                Renderer.getInstance().deregister(toDeregister.pos, toDeregister.toDisplay);
            }
        }
    }

    [Serializable]
    public class updateableObject
    {
        private bool isActive = false;
        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                if(value)
                {
                    Updater.getInstance().registerUpdateableObject(this);
                    isActive = true;
                }
                else
                {
                    Updater.getInstance().deregisterUpdateableObject(this);
                    isActive = false;
                }
            }
        }

        public List<iComponent> components;
        public vector2 pos;
        public char toDisplay;

        public updateableObject(vector2 pos)
        {
            this.toDisplay = ' ';
            this.pos = pos;
            components = new List<iComponent>();
            IsActive = true;
        }

        public updateableObject(bool active, vector2 pos, char toDisplay)
        {
            this.toDisplay = toDisplay;
            this.pos = pos;
            components = new List<iComponent>();
            IsActive = active;
        }

        public virtual bool move(vector2 newPos)
        {
            if (game.renderer.isPosEmpty(newPos))
            {
                game.renderer.deregister(pos, toDisplay);
                pos = newPos;
                game.renderer.register(pos, toDisplay);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void start()
        {
            foreach (iComponent c in components)
            {
                c.start(this);
            }
        }

        public virtual void update()
        {
            foreach (iComponent c in components)
            {
                c.update(this);
            }
        }

        public virtual void stop()
        {
            foreach (iComponent c in components)
            {
                c.stop(this);
            }
        }
    }
}
