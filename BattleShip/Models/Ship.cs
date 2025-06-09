public class Ship : IShip
{
    public string Type { get; }
    public int Length { get; }
    public int HitCount { get; set; }
    public Orientation Orientation { get; set; }
    public List<Coordinate> Coordinates { get; set; }

    public Ship(int length, Orientation orientation, string type)
    {
        Length = length;
        Orientation = orientation;
        Type = type;
        HitCount = 0;
        Coordinates = new List<Coordinate>();
    }
}