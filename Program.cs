using System;
using System.Diagnostics;

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

        public static bool operator ==(Vec3 left, Vec3 right) =>
            ((left.x == right.x) && (left.y == right.y) && (left.z == right.z));

        public static bool operator !=(Vec3 left, Vec3 right) =>
            ((left.x != right.x) || (left.y != right.y) || (left.z != right.z));

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

        public Vec3 primaryVec
        {
            get { return _primaryVec; }
        }
        public Vec3 secondaryVec
        {
            get { return _secondaryVec; }
        }

        public Block() { }

        public Block(Color[] colors, Vec3 v1, Vec3 v2)
        {
            _firstCol = colors[0];
            _secondCol = colors[1];
            _primaryVec = v1;
            _secondaryVec = v2;
        }

        public virtual Color[] getColors()
        {
            return new Color[] { _firstCol, _secondCol };
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

    class Corner : Block
    {
        private Color _thirdCol = Color.White;

        public Corner() { }

        public override Color[] getColors()
        {
            return new Color[] { _firstCol, _secondCol, _thirdCol };
        }

        public Corner(Color[] colors, Vec3 v1, Vec3 v2)
        {
            _firstCol = colors[0];
            _secondCol = colors[1];
            _thirdCol = colors[2];
            _primaryVec = v1;
            _secondaryVec = v2;
        }
    }

    class Cube
    {
        private Block[] _blocks = new Block[20];
        private string _face = "  ";

        public Block getBlock(int index)
        {
            return _blocks[index];
        }

        //TODO:
        public bool compare(Cube cube)
        {
            return false;
        }

        //TODO:
        public void doAlgo(string algo) { }

        public Cube(bool def = true)
        {
            if (def)
            {
                Color[][] defaultCubeCol =
                [
                    // first layer
                    [Color.White, Color.Blue],
                    [Color.White, Color.Blue, Color.Red],
                    [Color.White, Color.Red],
                    [Color.White, Color.Green, Color.Red],
                    [Color.White, Color.Green],
                    [Color.White, Color.Green, Color.Orange],
                    [Color.White, Color.Orange],
                    [Color.White, Color.Blue, Color.Orange],
                    // middle layer
                    [Color.Blue, Color.Red],
                    [Color.Green, Color.Red],
                    [Color.Green, Color.Orange],
                    [Color.Blue, Color.Orange],
                    // bottom layer
                    [Color.Yellow, Color.Blue],
                    [Color.Yellow, Color.Blue, Color.Red],
                    [Color.Yellow, Color.Red],
                    [Color.Yellow, Color.Green, Color.Red],
                    [Color.Yellow, Color.Green],
                    [Color.Yellow, Color.Green, Color.Orange],
                    [Color.Yellow, Color.Orange],
                    [Color.Yellow, Color.Blue, Color.Orange],
                ];
                float[][][] defaultCubeVec =
                [
                    // first layer
                    [
                        [1, 0, 0],
                        [0, -1, 0],
                    ],
                    [
                        [1, 0, 0],
                        [0, -1, 0],
                    ],
                    [
                        [1, 0, 0],
                        [0, 0, 1],
                    ],
                    [
                        [1, 0, 0],
                        [0, 1, 0],
                    ],
                    [
                        [1, 0, 0],
                        [0, 1, 0],
                    ],
                    [
                        [1, 0, 0],
                        [0, 1, 0],
                    ],
                    [
                        [1, 0, 0],
                        [0, 0, -1],
                    ],
                    [
                        [1, 0, 0],
                        [0, -1, 0],
                    ],
                    // middle layer
                    [
                        [0, -1, 0],
                        [0, 0, 1],
                    ],
                    [
                        [0, 1, 0],
                        [0, 0, 1],
                    ],
                    [
                        [0, 1, 0],
                        [0, 0, -1],
                    ],
                    [
                        [0, -1, 0],
                        [0, 0, -1],
                    ],
                    // bottom layer
                    [
                        [-1, 0, 0],
                        [0, -1, 0],
                    ],
                    [
                        [-1, 0, 0],
                        [0, -1, 0],
                    ],
                    [
                        [-1, 0, 0],
                        [0, 0, 1],
                    ],
                    [
                        [-1, 0, 0],
                        [0, 1, 0],
                    ],
                    [
                        [-1, 0, 0],
                        [0, 1, 0],
                    ],
                    [
                        [-1, 0, 0],
                        [0, 1, 0],
                    ],
                    [
                        [-1, 0, 0],
                        [0, 0, -1],
                    ],
                    [
                        [-1, 0, 0],
                        [0, -1, 0],
                    ],
                ];
                Debug.Assert(defaultCubeCol.Length == defaultCubeVec.Length);
                Debug.Assert(defaultCubeCol.Length == 20);
                for (int i = 0; i < defaultCubeCol.Length; ++i)
                {
                    var vi = defaultCubeVec[i][0];
                    var v1 = new Vec3(vi[0], vi[1], vi[2]);
                    vi = defaultCubeVec[i][1];
                    var v2 = new Vec3(vi[0], vi[1], vi[2]);
                    if (defaultCubeCol[i].Length == 2)
                    {
                        _blocks[i] = new Edge(defaultCubeCol[i], v1, v2);
                    }
                    else
                    {
                        _blocks[i] = new Corner(defaultCubeCol[i], v1, v2);
                    }
                }
            }
        }

        public void rotateUp()
        {
            // edges
            var tmp = _blocks[0];
            _blocks[0] = _blocks[6];
            _blocks[6] = _blocks[4];
            _blocks[4] = _blocks[2];
            _blocks[2] = tmp;

            // corners
            tmp = _blocks[1];
            _blocks[1] = _blocks[7];
            _blocks[7] = _blocks[5];
            _blocks[5] = _blocks[3];
            _blocks[3] = tmp;

            // rotate vectors
            for (int i = 0; i < 8; ++i)
            {
                _blocks[i].rotateX();
            }
        }

        public void rotateDown()
        {
            // edges
            var tmp = _blocks[12];
            _blocks[12] = _blocks[14];
            _blocks[14] = _blocks[16];
            _blocks[16] = _blocks[18];
            _blocks[18] = tmp;
            // corners
            tmp = _blocks[13];
            _blocks[13] = _blocks[15];
            _blocks[15] = _blocks[17];
            _blocks[17] = _blocks[19];
            _blocks[19] = tmp;
            // rotate vectors
            for (int i = 12; i < 20; ++i)
            {
                _blocks[i].rotateX(false);
            }
        }

        public void rotateLeft()
        {
            // edges
            var tmp = _blocks[6];
            _blocks[6] = _blocks[11];
            _blocks[11] = _blocks[18];
            _blocks[18] = _blocks[10];
            _blocks[10] = tmp;
            // corners
            tmp = _blocks[5];
            _blocks[5] = _blocks[7];
            _blocks[7] = _blocks[19];
            _blocks[19] = _blocks[17];
            _blocks[17] = tmp;
            // rotate vectors
            foreach (int i in new int[] { 5, 6, 7, 10, 11, 17, 18, 19 })
            {
                _blocks[i].rotateZ(false);
            }
        }

        public void rotateRight()
        {
            // edges
            var tmp = _blocks[2];
            _blocks[2] = _blocks[9];
            _blocks[9] = _blocks[14];
            _blocks[14] = _blocks[8];
            _blocks[8] = tmp;
            // corners
            tmp = _blocks[1];
            _blocks[1] = _blocks[3];
            _blocks[3] = _blocks[15];
            _blocks[15] = _blocks[13];
            _blocks[13] = tmp;
            // rotate vectors
            foreach (int i in new int[] { 1, 2, 3, 8, 9, 13, 14, 15 })
            {
                _blocks[i].rotateZ();
            }
        }

        public void rotateFront()
        {
            // edges
            var tmp = _blocks[4];
            _blocks[4] = _blocks[10];
            _blocks[10] = _blocks[16];
            _blocks[16] = _blocks[9];
            _blocks[9] = tmp;
            // corners
            tmp = _blocks[3];
            _blocks[3] = _blocks[5];
            _blocks[5] = _blocks[17];
            _blocks[17] = _blocks[15];
            _blocks[15] = tmp;
            // rotate vectors
            foreach (int i in new int[] { 3, 4, 5, 9, 10, 15, 16, 17 })
            {
                _blocks[i].rotateY();
            }
        }

        public void rotateBack()
        {
            // edges
            var tmp = _blocks[0];
            _blocks[0] = _blocks[8];
            _blocks[8] = _blocks[12];
            _blocks[12] = _blocks[11];
            _blocks[11] = tmp;
            // corners
            tmp = _blocks[7];
            _blocks[7] = _blocks[1];
            _blocks[1] = _blocks[13];
            _blocks[13] = _blocks[19];
            _blocks[19] = tmp;
            // rotate vectors
            foreach (int i in new int[] { 0, 1, 7, 8, 11, 12, 13, 19 })
            {
                _blocks[i].rotateY(false);
            }
        }

        public void rotateUpPrime()
        {
            for (int i = 0; i < 3; ++i)
            {
                this.rotateUp();
            }
        }

        public void rotateDownPrime()
        {
            for (int i = 0; i < 3; ++i)
            {
                this.rotateDown();
            }
        }

        public void rotateLeftPrime()
        {
            for (int i = 0; i < 3; ++i)
            {
                this.rotateLeft();
            }
        }

        public void rotateRightPrime()
        {
            for (int i = 0; i < 3; ++i)
            {
                this.rotateRight();
            }
        }

        public void rotateFrontPrime()
        {
            for (int i = 0; i < 3; ++i)
            {
                this.rotateFront();
            }
        }

        public void rotateBackPrime()
        {
            for (int i = 0; i < 3; ++i)
            {
                this.rotateBack();
            }
        }

        private void printColBlock(Color c)
        {
            var cc = ConsoleColor.Black;

            if (c == Color.White)
            {
                cc = ConsoleColor.White;
            }
            else if (c == Color.Yellow)
            {
                cc = ConsoleColor.Yellow;
            }
            else if (c == Color.Green)
            {
                cc = ConsoleColor.DarkGreen;
            }
            else if (c == Color.Blue)
            {
                cc = ConsoleColor.DarkBlue;
            }
            else if (c == Color.Red)
            {
                cc = ConsoleColor.DarkRed;
            }
            else if (c == Color.Orange)
            {
                cc = ConsoleColor.DarkYellow;
            }

            // Console.ResetColor();
            Console.BackgroundColor = cc;
            Console.Write(_face);
            Console.ResetColor();
        }

        private Color getFaceCol(int index, Vec3 dir)
        {
            var a = _blocks[index];
            var cols = a.getColors();

            if (cols.Length == 2)
            {
                if (a.primaryVec == dir)
                {
                    return cols[0];
                }
                else if (a.secondaryVec == dir)
                {
                    return cols[1];
                }
                else
                {
                    //TODO: throw an error
                    return Color.White;
                }
            }
            else if (cols.Length == 3)
            {
                if (a.primaryVec == dir)
                {
                    return cols[0];
                }
                else if (a.secondaryVec == dir)
                {
                    return cols[1];
                }
                else
                {
                    return cols[2];
                }
            }
            else
            {
                //TODO: throw an error
                return Color.White;
            }
        }

        public void print()
        {
            Console.ResetColor();

            // blue face
            Console.Write($"{_face}{_face}{_face}");
            this.printColBlock(this.getFaceCol(19, new Vec3(0, -1, 0)));
            this.printColBlock(this.getFaceCol(12, new Vec3(0, -1, 0)));
            this.printColBlock(this.getFaceCol(13, new Vec3(0, -1, 0)));
            Console.WriteLine("");

            Console.Write($"{_face}{_face}{_face}");
            this.printColBlock(this.getFaceCol(11, new Vec3(0, -1, 0)));
            this.printColBlock(Color.Blue);
            this.printColBlock(this.getFaceCol(8, new Vec3(0, -1, 0)));
            Console.WriteLine("");

            Console.Write($"{_face}{_face}{_face}");
            this.printColBlock(this.getFaceCol(7, new Vec3(0, -1, 0)));
            this.printColBlock(this.getFaceCol(0, new Vec3(0, -1, 0)));
            this.printColBlock(this.getFaceCol(1, new Vec3(0, -1, 0)));
            Console.WriteLine("");

            // orange face pt1
            this.printColBlock(this.getFaceCol(19, new Vec3(0, 0, -1)));
            this.printColBlock(this.getFaceCol(11, new Vec3(0, 0, -1)));
            this.printColBlock(this.getFaceCol(7, new Vec3(0, 0, -1)));
            // white face pt1
            this.printColBlock(this.getFaceCol(7, new Vec3(1, 0, 0)));
            this.printColBlock(this.getFaceCol(0, new Vec3(1, 0, 0)));
            this.printColBlock(this.getFaceCol(1, new Vec3(1, 0, 0)));
            // red face pt1
            this.printColBlock(this.getFaceCol(1, new Vec3(0, 0, 1)));
            this.printColBlock(this.getFaceCol(8, new Vec3(0, 0, 1)));
            this.printColBlock(this.getFaceCol(13, new Vec3(0, 0, 1)));
            // yellow face pt1
            this.printColBlock(this.getFaceCol(13, new Vec3(-1, 0, 0)));
            this.printColBlock(this.getFaceCol(12, new Vec3(-1, 0, 0)));
            this.printColBlock(this.getFaceCol(19, new Vec3(-1, 0, 0)));
            Console.WriteLine("");

            // orange face pt2
            this.printColBlock(this.getFaceCol(18, new Vec3(0, 0, -1)));
            this.printColBlock(Color.Orange);
            this.printColBlock(this.getFaceCol(6, new Vec3(0, 0, -1)));
            // white face pt2
            this.printColBlock(this.getFaceCol(6, new Vec3(1, 0, 0)));
            this.printColBlock(Color.White);
            this.printColBlock(this.getFaceCol(2, new Vec3(1, 0, 0)));
            // red face pt2
            this.printColBlock(this.getFaceCol(2, new Vec3(0, 0, 1)));
            this.printColBlock(Color.Red);
            this.printColBlock(this.getFaceCol(14, new Vec3(0, 0, 1)));
            // yellow face pt2
            this.printColBlock(this.getFaceCol(14, new Vec3(-1, 0, 0)));
            this.printColBlock(Color.Yellow);
            this.printColBlock(this.getFaceCol(18, new Vec3(-1, 0, 0)));
            Console.WriteLine("");

            // orange face pt3
            this.printColBlock(this.getFaceCol(17, new Vec3(0, 0, -1)));
            this.printColBlock(this.getFaceCol(10, new Vec3(0, 0, -1)));
            this.printColBlock(this.getFaceCol(5, new Vec3(0, 0, -1)));
            // white face pt3
            this.printColBlock(this.getFaceCol(5, new Vec3(1, 0, 0)));
            this.printColBlock(this.getFaceCol(4, new Vec3(1, 0, 0)));
            this.printColBlock(this.getFaceCol(3, new Vec3(1, 0, 0)));
            // red face pt3
            this.printColBlock(this.getFaceCol(3, new Vec3(0, 0, 1)));
            this.printColBlock(this.getFaceCol(9, new Vec3(0, 0, 1)));
            this.printColBlock(this.getFaceCol(15, new Vec3(0, 0, 1)));
            // yellow face pt3
            this.printColBlock(this.getFaceCol(15, new Vec3(-1, 0, 0)));
            this.printColBlock(this.getFaceCol(16, new Vec3(-1, 0, 0)));
            this.printColBlock(this.getFaceCol(17, new Vec3(-1, 0, 0)));
            Console.WriteLine("");

            // blue face
            Console.Write($"{_face}{_face}{_face}");
            this.printColBlock(this.getFaceCol(5, new Vec3(0, 1, 0)));
            this.printColBlock(this.getFaceCol(4, new Vec3(0, 1, 0)));
            this.printColBlock(this.getFaceCol(3, new Vec3(0, 1, 0)));
            Console.WriteLine("");

            Console.Write($"{_face}{_face}{_face}");
            this.printColBlock(this.getFaceCol(10, new Vec3(0, 1, 0)));
            this.printColBlock(Color.Green);
            this.printColBlock(this.getFaceCol(9, new Vec3(0, 1, 0)));
            Console.WriteLine("");

            Console.Write($"{_face}{_face}{_face}");
            this.printColBlock(this.getFaceCol(17, new Vec3(0, 1, 0)));
            this.printColBlock(this.getFaceCol(16, new Vec3(0, 1, 0)));
            this.printColBlock(this.getFaceCol(15, new Vec3(0, 1, 0)));
            Console.WriteLine("");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Cube cube = new Cube();
            cube.rotateRight();
            cube.print();
        }
    }
}
