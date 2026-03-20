using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace rubiks_cube_simulator
{
    class Corner : Block
    {
        private Color _thirdCol = Color.White;

        public Color thirdCol
        {
            set { _thirdCol = value; }
            get { return _thirdCol; }
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

        public Corner(Color[] colors, Vec3 v1, Vec3 v2)
        {
            _firstCol = colors[0];
            _secondCol = colors[1];
            _thirdCol = colors[2];
            _primaryVec = v1;
            _secondaryVec = v2;
        }
    }
}
