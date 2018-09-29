using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace StarField
{
    public class game
    {
        public static bool run = true;
        public static bool look = false;

        public static Renderer renderer;
        public static Updater updater;
        public static InputManager input;
        public static updateableObject o;

        private static Stopwatch frameTimer;
        private static Stopwatch gameTimer;

        private static ComputerConsole.Computer mainShipComputer;

        private static long frameTime = 0;
        private static long tick = 0;

        public static void runGame()
        {
            renderer = Renderer.getInstance();
            updater = Updater.getInstance();
            input = InputManager.getInstance();
            gameTimer = new Stopwatch();
            frameTimer = new Stopwatch();
            o = new updateableObject(true, new vector2(), '!');
            mainShipComputer = new ComputerConsole.Computer();
            mainShipComputer.Boot();
            tick = 0;

            gameTimer.Start();

            while (run)
            {
                tick++;

                if(look)
                {
                    frameTimer.Restart();
                    updater.doUpdate();
                    renderer.renderFrame(false);
                    input.Update();
                    doTestInput();
                    frameTimer.Stop();

                    frameTime = frameTimer.ElapsedMilliseconds;

                    if (frameTime < 16)
                    {
                        if (frameTime > 0)
                        {
                            System.Threading.Thread.Sleep(16 - (int)frameTime);
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(15);
                        }
                    }
                    printFps();
                }
                else
                {
                    mainShipComputer.doConsoleInput();
                }
            }

        }

        public static void printFps()
        {
            Console.Write(Program.buildname + ": " + Program.version + 
                        " AVG FPS: " + (int)((((double)gameTimer.ElapsedMilliseconds / (double)tick))) + 
                        " IFT: " + frameTime + 
                        " lookingAt: " + renderer.worldPos.toString() + "          ");
        }

        public static void doTestInput()
        {
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

            if(input.IsKeyHeld(OpenTK.Input.Key.Escape))
            {
                look = false;
                renderer.clearScreen();
            }
        }
    }
}
