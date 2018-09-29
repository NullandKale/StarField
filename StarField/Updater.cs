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
        }

        public void save()
        {
            game.save.updater_gameObjects = gameObjects;
        }

        public void load()
        {
            gameObjects = game.save.updater_gameObjects;
        }

        private Updater()
        {
            gameObjects = new Dictionary<vector2, updateableObject>();
        }

        private Dictionary<vector2, updateableObject> gameObjects;

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

        public updateableObject(bool active, vector2 pos, char toDisplay)
        {
            this.toDisplay = toDisplay;
            this.pos = pos;
            components = new List<iComponent>();
            IsActive = active;
        }

        public void start()
        {
            foreach (iComponent c in components)
            {
                c.start();
            }
        }

        public void update()
        {
            foreach (iComponent c in components)
            {
                c.update();
            }
        }

        public void stop()
        {
            foreach (iComponent c in components)
            {
                c.stop();
            }
        }
    }
}
