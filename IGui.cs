namespace rubiks_cube_simulator;

interface IGui
{
    public Cube getCube();
    public void setCube(Cube val);
    public void createWindow();
    public void addCube(Cube cube);
    public void mainLoop();
}
