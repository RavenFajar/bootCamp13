public interface IShip
{
    string Type { get; }
    int Length { get; }
    int HitCount { get; set; }
    // Orientation Orientation { get; set; }
    List<Coordinate> Coordinates { get; set; }
}