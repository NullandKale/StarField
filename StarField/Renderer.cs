using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField
{
    public class Renderer
    {
        private static Renderer sharedInstance;

        public static Renderer getInstance()
        {
            if(sharedInstance == null)
            {
                sharedInstance = new Renderer();
            }

            return sharedInstance;
        }

        public readonly int windowHeight;
        public readonly int windowWidth;
        public vector2 worldPos;
        private Dictionary<vector2, char> renderBuffer;

        private Renderer()
        {
            worldPos = new vector2(0, 0);
            windowHeight = Console.WindowHeight - 1;
            windowWidth = Console.WindowWidth - 1;
            renderBuffer = new Dictionary<vector2, char>(new vector2HashCode());
        }

        public void register(vector2 pos, char c)
        {
            if(!renderBuffer.ContainsKey(pos))
            {
                renderBuffer.Add(pos, c);
            }
        }

        public void deregister(vector2 pos, char c)
        {
            if (renderBuffer.ContainsKey(pos))
            {
                renderBuffer.Remove(pos);
            }
        }

        public void renderFrame()
        {
            for(int y = 0; y < windowHeight; y++)
            {
                char[] toPrint = new char[windowWidth];
                for(int x = 0; x < windowWidth; x++)
                {
                    if(renderBuffer.ContainsKey())
                }
            }
        }
    }
}
