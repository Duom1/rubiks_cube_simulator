namespace rubiks_cube_simulator
{
    interface IGui
    {
        public void createWindow();
        public void addCube(Cube cube);
        public void mainLoop();
    }
}
