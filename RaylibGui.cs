using Raylib_cs;
using CubeColor = rubiks_cube_simulator.Color;
using RayColor = Raylib_cs.Color;

namespace rubiks_cube_simulator
{
    class RaylibGui
    {
        private Cube _cube = new Cube(true);

        public Cube cube
        {
            set { _cube = value; }
            get { return _cube; }
        }

        public RaylibGui() { }

        public void createWindow()
        {
            Raylib.InitWindow(800, 400, "Rubiks Cube");
            Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        }

        public void mainLoop()
        {
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(RayColor.White);
                Raylib.DrawText("Hello, world!", 15, 15, 30, RayColor.Black);

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}
