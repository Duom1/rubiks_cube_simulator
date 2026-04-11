using System;
using System.Text.Json.Serialization;

namespace rubiks_cube_simulator;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Corner), "Corner")]
[JsonDerivedType(typeof(Edge), "Edge")]
class Block
{
    protected Color _firstCol = Color.White;
    protected Color _secondCol = Color.White;
    protected Vec3 _primaryVec = new Vec3(0, 0, 0);
    protected Vec3 _secondaryVec = new Vec3(0, 0, 0);

    public Color firstCol
    {
        set { _firstCol = value; }
        get { return _firstCol; }
    }
    public Color secondCol
    {
        set { _secondCol = value; }
        get { return _secondCol; }
    }
    public Vec3 primaryVec
    {
        set { _primaryVec = value; }
        get { return _primaryVec; }
    }
    public Vec3 secondaryVec
    {
        set { _secondaryVec = value; }
        get { return _secondaryVec; }
    }

    public bool compare(Block right)
    {
        Boolean cols = (this._firstCol == right._firstCol) && (this._secondCol == right._secondCol);
        Boolean vecs =
            (this._primaryVec == right._primaryVec) && (this._secondaryVec == right._secondaryVec);
        // Console.WriteLine($"cols: {cols}, vecs:{vecs}");
        return cols && vecs;
    }

    public Block() { }

    public Block(Color[] colors, Vec3 v1, Vec3 v2)
    {
        _firstCol = colors[0];
        _secondCol = colors[1];
        _primaryVec = v1;
        _secondaryVec = v2;
    }

    public virtual string dbgStr()
    {
        return $"{_primaryVec.dbgStr()}, {_secondaryVec.dbgStr()}, {string.Join(", ", this.getColors())}";
    }

    public virtual Color getFaceColor(Vec3 dir)
    {
        if (this.primaryVec == dir)
        {
            return this.getColors()[0];
        }
        else if (this.secondaryVec == dir)
        {
            return this.getColors()[1];
        }
        else
        {
            throw new InvalidOperationException($"Unable to get proper color for face");
        }
    }

    public virtual Color[] getColors()
    {
        return new Color[] { _firstCol, _secondCol };
    }

    public virtual void rotateX(bool clockwise = true)
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

    public virtual void rotateY(bool clockwise = true)
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

    public virtual void rotateZ(bool clockwise = true)
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
