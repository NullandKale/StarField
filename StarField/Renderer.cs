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

        public void save()
        {
            game.save.renderer_worldPos = worldPos;
        }

        public void load()
        {
            worldPos = game.save.renderer_worldPos;
        }

        public bool isPosEmpty(vector2 pos)
        {
            return !renderBuffer.ContainsKey(pos);
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

        public void clearScreen()
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < windowHeight; i++)
            {
                string toWrite = "";

                for (int j = 0; j < windowWidth; j++)
                {
                    toWrite += " ";
                }

                Console.WriteLine(toWrite);
            }

            Console.SetCursorPosition(0, 0);
        }

        public void renderFrame(bool fastMode)
        {
            if (fastMode)
            {
                vector2 currentLocation = new vector2();

                Console.SetCursorPosition(0, 0);

                for (int y = 0; y < windowHeight; y++)
                {
                    currentLocation.y = y + worldPos.y;
                    char[] toPrint = new char[windowWidth];

                    for (int x = 0; x < windowWidth; x++)
                    {
                        currentLocation.x = x + worldPos.x;
                        if (renderBuffer.ContainsKey(currentLocation))
                        {
                            toPrint[x] = renderBuffer[currentLocation];
                        }
                        else
                        {
                            toPrint[x] = getBackgroundChar(currentLocation);
                        }
                    }

                    Console.WriteLine(toPrint);
                }
            }
            else
            {
                vector2 currentLocation = new vector2();

                Console.SetCursorPosition(0, 0);

                for (int y = 0; y < windowHeight; y++)
                {
                    List<Char> toPrint = new List<char>();
                    currentLocation.y = y + worldPos.y;
                    for (int x = 0; x < windowWidth; x++)
                    {
                        currentLocation.x = x + worldPos.x;
                        if (renderBuffer.ContainsKey(currentLocation))
                        {
                            if (lastConsoleColor != ConsoleColor.Green)
                            {
                                Console.Write(toPrint.ToArray());
                                toPrint.Clear();
                                setConsoleColor(ConsoleColor.Green);
                                toPrint.Add(renderBuffer[currentLocation]);
                            }
                            else
                            {
                                toPrint.Add(renderBuffer[currentLocation]);
                            }
                        }
                        else
                        {
                            if (lastConsoleColor != ConsoleColor.DarkGray)
                            {
                                Console.Write(toPrint.ToArray());
                                toPrint.Clear();
                                setConsoleColor(ConsoleColor.DarkGray);
                                toPrint.Add(getBackgroundChar(currentLocation));
                            }
                            else
                            {
                                toPrint.Add(getBackgroundChar(currentLocation));
                            }
                        }
                    }
                    Console.WriteLine(toPrint.ToArray());
                }
            }
        }

        private ConsoleColor lastConsoleColor = ConsoleColor.White;

        private bool setConsoleColor(ConsoleColor toSet)
        {
            if(toSet != lastConsoleColor)
            {
                Console.ForegroundColor = toSet;
                lastConsoleColor = toSet;
                return true;
            }
            else
            {
                return false;
            }
        }

        private char getBackgroundChar(vector2 pos)
        {
            if (Simplex.Noise.CalcPixel3D(pos.x, pos.y, Program.randomSeed, 0.5f) > 222)
            {
                return '.';
            }
            else
            {
                return ' ';
            }
        }
    }
}
