namespace rubiks_cube_simulator;

interface IGui
{
    public Cube getCube();
    public void setCube(Cube val);
    public void createWindow();
    public void queueAlgo(string algo);
    public void mainLoop();
}
