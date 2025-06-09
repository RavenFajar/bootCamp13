public struct Coordinate
{
    public int AxisX { get; }
    public int AxisY { get; }

    public Coordinate(int x, int y)
    {
        AxisX = x;
        AxisY = y;
    }

    public override string ToString()
    {
        return $"({AxisX}, {AxisY})";
    }
}