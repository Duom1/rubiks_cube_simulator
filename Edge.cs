namespace rubiks_cube_simulator;

class Edge : Block
{
    public Edge() { }

    public Edge(Color[] colors, Vec3 v1, Vec3 v2)
    {
        _firstCol = colors[0];
        _secondCol = colors[1];
        _primaryVec = v1;
        _secondaryVec = v2;
    }
}
