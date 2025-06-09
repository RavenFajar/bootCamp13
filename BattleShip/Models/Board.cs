public class Board : IBoard
{
    public int Size { get; }
    public Tile[,] Tiles { get; }

    public Board(int size)
    {
        Size = size;
        Tiles = new Tile[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Tiles[i, j] = Tile.Empty;
            }
        }
    }
}
