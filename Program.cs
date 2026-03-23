namespace rubiks_cube_simulator;

class Program
{
    static void Main(string[] args)
    {
        var cube = new Cube(true);
        var gui = new RaylibGui();
        gui.setCube(cube);
        gui.createWindow();
        gui.mainLoop();
    }
}
