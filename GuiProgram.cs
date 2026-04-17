namespace rubiks_cube_simulator;

class GuiProgram
{
    private Cube cube = new(true);
    private IGui gui = new RaylibGui();

    public void run()
    {
        gui.setCube(cube);
        gui.createWindow();
        gui.mainLoop();
    }
}
