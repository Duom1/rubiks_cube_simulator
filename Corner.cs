using System;

namespace rubiks_cube_simulator
{
    class Corner : Block
    {
        private Color _thirdCol = Color.White;
        private Vec3 _thirdVec = new Vec3(0f, 0f, 0f);

        public Color thirdCol
        {
            set { _thirdCol = value; }
            get { return _thirdCol; }
        }

        public Vec3 thirdVec
        {
            set { _thirdVec = value; }
            get { return _thirdVec; }
        }

        public bool compare(Corner right)
        {
            Boolean cols =
                (this._firstCol == right._firstCol)
                && (this._secondCol == right._secondCol)
                && (this._thirdCol == right._thirdCol);
            Boolean vecs =
                (this._primaryVec == right._primaryVec)
                && (this._secondaryVec == right._secondaryVec);
            // Console.WriteLine($"cols: {cols}, vecs:{vecs}");
            return cols && vecs;
        }

        public Corner() { }

        public override Color getFaceColor(Vec3 dir)
        {
            if (this._primaryVec == dir)
            {
                return this.getColors()[0];
            }
            else if (this._secondaryVec == dir)
            {
                return this.getColors()[1];
            }
            else
            {
                return this.getColors()[2];
            }
        }

        public override Color[] getColors()
        {
            return new Color[] { _firstCol, _secondCol, _thirdCol };
        }

        public Corner(Color[] colors, Vec3 v1, Vec3 v2, Vec3 v3)
        {
            _firstCol = colors[0];
            _secondCol = colors[1];
            _thirdCol = colors[2];
            _primaryVec = v1;
            _secondaryVec = v2;
            _thirdVec = v3;
        }

        public override void rotateX(bool clockwise = true)
        {
            if (clockwise)
            {
                _primaryVec.rotateClockwiseX();
                _secondaryVec.rotateClockwiseX();
                _thirdVec.rotateClockwiseX();
            }
            else
            {
                _primaryVec.rotateCounterClockwiseX();
                _secondaryVec.rotateCounterClockwiseX();
                _thirdVec.rotateCounterClockwiseX();
            }
        }

        public override void rotateY(bool clockwise = true)
        {
            if (clockwise)
            {
                _primaryVec.rotateClockwiseY();
                _secondaryVec.rotateClockwiseY();
                _thirdVec.rotateClockwiseY();
            }
            else
            {
                _primaryVec.rotateCounterClockwiseY();
                _secondaryVec.rotateCounterClockwiseY();
                _thirdVec.rotateCounterClockwiseY();
            }
        }

        public override void rotateZ(bool clockwise = true)
        {
            if (clockwise)
            {
                _primaryVec.rotateClockwiseZ();
                _secondaryVec.rotateClockwiseZ();
                _thirdVec.rotateClockwiseZ();
            }
            else
            {
                _primaryVec.rotateCounterClockwiseZ();
                _secondaryVec.rotateCounterClockwiseZ();
                _thirdVec.rotateCounterClockwiseZ();
            }
        }
    }
}
