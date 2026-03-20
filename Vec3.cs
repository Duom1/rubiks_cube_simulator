using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace rubiks_cube_simulator
{
    class Vec3
    {
        public float _x,
            _y,
            _z;

        public float x
        {
            set { _x = value; }
            get { return _x; }
        }
        public float y
        {
            set { _y = value; }
            get { return _y; }
        }
        public float z
        {
            set { _z = value; }
            get { return _z; }
        }

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

        public override bool Equals(object? obj)
        {
            if (obj is Vec3 other)
            {
                return ((this.x == other.x) && (this.y == other.y) && (this.z == other.z));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() + y.GetHashCode() + z.GetHashCode();
        }

        public string dbgStr()
        {
            return $"({x}, {y}, {z})";
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
}
