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

        public static SaveData save;
        public static Renderer renderer;
        public static Updater updater;
        public static InputManager input;
        public static Camera camera;

        private static Stopwatch frameTimer;
        private static Stopwatch gameTimer;

        private static ComputerConsole.Computer mainShipComputer;

        private static long frameTime = 0;
        private static long tick = 0;

        public static void initGame()
        {
            Console.WriteLine(Program.buildname + " " + Program.version + " Loading...");

            renderer = Renderer.getInstance();
            updater = Updater.getInstance();
            input = InputManager.getInstance();
            camera = new Camera(new vector2());
            gameTimer = new Stopwatch();
            frameTimer = new Stopwatch();
            mainShipComputer = new ComputerConsole.Computer();

            Console.Write("Enter Save Name: ");
            string toLoad = Console.ReadLine().Trim();
            if(SaveData.exists(toLoad))
            {
                save = SaveData.load(toLoad);
                loadGame();
            }
            else
            {
                save = new SaveData(toLoad);
                newGame();
                SaveData.save(save);
            }
        }

        public static void loadGame()
        {
            if(!save.version.Equals(Program.version))
            {
                Console.WriteLine("BAD SAVE VERSION CANNOT LOAD: " + SaveData.getFileLoc(save));
                throw new Exception("BAD SAVE");
            }

            Console.WriteLine("Loading Game");
            renderer.load();
            updater.load();
        }

        public static void newGame()
        {
            Console.WriteLine("Initializing Game");
            updater.init();
            Console.WriteLine("Saving New Game");
            renderer.save();
            updater.save();
        }

        public static void saveGame()
        {
            Console.WriteLine("Save Game");
            renderer.save();
            updater.save();

            SaveData.save(save);
        }

        public static void runGame()
        {

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
                    camera.update();
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
            saveGame();
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

    public class Camera
    {
        public vector2 currentCenter = new vector2();

        public vector2 CurrentCenter
        {
            get
            {
                return currentCenter;
            }
            private set
            {
                currentCenter = value;
                game.renderer.worldPos.x = value.x - (game.renderer.windowWidth / 2);
                game.renderer.worldPos.y = value.y - (game.renderer.windowHeight / 2);
            }
        }

        private readonly List<updateableObject> followTargetStack = new List<updateableObject>();

        public Camera(vector2 coc)
        {
            CurrentCenter = coc;
        }

        public void enqueueGameObject(updateableObject toEnqueue)
        {
            followTargetStack.Add(toEnqueue);
        }

        public void removeGameObject(updateableObject toRemove)
        {
            followTargetStack.Remove(toRemove);
        }

        public void centerOnCell(vector2 coc)
        {
            CurrentCenter = coc;
        }

        public void update()
        {
            if(followTargetStack.Count > 0)
            {
                CurrentCenter = followTargetStack.Last().pos;
            }
        }
    }
}
