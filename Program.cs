using System;

namespace rubiks_cube_simulator
{
    class Vec3
    {
        public float x, y, z;
        Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public void rotateClockwiseX();
        public void rotateClockwiseY();
        public void rotateClockwiseZ();
    }
    enum Color
    {
        White,
        Yellow,
        Green,
        Blue,
        Red,
        Orange
    }
    class Block
    {
        private Color _firstCol;
        private Color _secondCol;
        private Vec3 primaryVec;
        private Vec3 secondaryVec;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
