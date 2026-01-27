using System;

namespace rubiks_cube_simulator
{
    class Vec3
    {
        public float x,
            y,
            z;

        public Vec3() { }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void rotateCounterClockwiseX()
        {
            (this.y, this.z) = (-this.z, this.y);
        }

        public void rotateCounterClockwiseY()
        {
            (this.x, this.z) = (this.z, -this.x);
        }

        public void rotateCounterClockwiseZ()
        {
            (this.x, this.y) = (-this.y, this.x);
        }

        public void rotateClockwiseX()
        {
            (this.y, this.z) = (this.z, -this.y);
        }

        public void rotateClockwiseY()
        {
            (this.x, this.z) = (-this.z, this.x);
        }

        public void rotateClockwiseZ()
        {
            (this.x, this.y) = (this.y, -this.x);
        }
    }

    enum Color
    {
        White,
        Yellow,
        Green,
        Blue,
        Red,
        Orange,
    }

    class Block
    {
        protected Color _firstCol = Color.White;
        protected Color _secondCol = Color.White;
        protected Vec3 _primaryVec = new Vec3(0, 0, 0);
        protected Vec3 _secondaryVec = new Vec3(0, 0, 0);

        public Block() { }

        public Block(Color[] colors)
        {
            _firstCol = colors[0];
            _secondCol = colors[1];
        }

        public void rotateX(bool clockwise = true)
        {
            if (clockwise)
            {
                _primaryVec.rotateClockwiseX();
                _secondaryVec.rotateClockwiseX();
            }
            else
            {
                _primaryVec.rotateCounterClockwiseX();
                _secondaryVec.rotateCounterClockwiseX();
            }
        }

        public void rotateY(bool clockwise = true)
        {
            if (clockwise)
            {
                _primaryVec.rotateClockwiseY();
                _secondaryVec.rotateClockwiseY();
            }
            else
            {
                _primaryVec.rotateCounterClockwiseY();
                _secondaryVec.rotateCounterClockwiseY();
            }
        }

        public void rotateZ(bool clockwise = true)
        {
            if (clockwise)
            {
                _primaryVec.rotateClockwiseZ();
                _secondaryVec.rotateClockwiseZ();
            }
            else
            {
                _primaryVec.rotateCounterClockwiseZ();
                _secondaryVec.rotateCounterClockwiseZ();
            }
        }
    }

    class Edge : Block { }

    class Corner : Block
    {
        private Color _thirdCol = Color.White;

        public Corner() { }

        public Corner(Color[] colors)
        {
            _firstCol = colors[0];
            _secondCol = colors[1];
            _thirdCol = colors[2];
        }
    }

    class Cube
    {
        private Block[] _blocks = new Block[20];

        public Block getBlock(int index)
        {
            return _blocks[index];
        }

        public bool compare(Cube cube)
        {
            return false;
        }

        public void doAlgo(string algo) { }

        public Cube(bool def = true) { }

        public void rotateUp() { }

        public void rotateDown() { }

        public void rotateLeft() { }

        public void rotateRight() { }

        public void rotateFront() { }

        public void rotateBack() { }

        public void rotateUpPrime() { }

        public void rotateDownPrime() { }

        public void rotateLeftPrime() { }

        public void rotateRightPrime() { }

        public void rotateFrontPrime() { }

        public void rotateBackPrime() { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = new Vec3(1, 0, 0);
            a.rotateClockwiseZ();
            a.rotateClockwiseX();
            a.rotateCounterClockwiseY();
            Console.WriteLine($"{a.x}, {a.y}, {a.z}");
            Console.WriteLine("shound be 1, 0, 0");
        }
    }
}
