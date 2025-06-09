public interface IBoard
{
    int Size { get; }
    Tile[,] Tiles { get; }
}