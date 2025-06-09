public class Display : IDisplay
{
    public void ShowMenu()
    {
        Console.WriteLine("=== BATTLESHIP GAME ===");
        Console.WriteLine("Press [ENTER] to Start New Game");
        Console.WriteLine("Press [ESC] to Exit");
        Console.WriteLine("=====================");
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowBoard(IBoard board)
    {
        Console.WriteLine("   " + string.Join(" ", Enumerable.Range(0, board.Size).Select(i => i.ToString().PadLeft(2))));
        for (int i = 0; i < board.Size; i++)
        {
            Console.Write(i.ToString().PadLeft(2) + "  ");
            for (int j = 0; j < board.Size; j++)
            {
                char symbol = board.Tiles[i, j] switch
                {
                    Tile.Empty => '~',
                    Tile.Ship => 'S',
                    Tile.Hit => 'X',
                    Tile.Miss => 'O',
                    Tile.SunkenShip => '#',
                    _ => '?',
                };
                Console.Write(symbol + "  ");
            }
            Console.WriteLine();
        }
    }

    public void ShowAttackBoard(IBoard board)
    {
        Console.WriteLine("   " + string.Join(" ", Enumerable.Range(0, board.Size).Select(i => i.ToString().PadLeft(2))));
        
        for (int i = 0; i < board.Size; i++)
        {
            Console.Write(i.ToString().PadLeft(2) + "  ");
            for (int j = 0; j < board.Size; j++)
            {
                char symbol = board.Tiles[i, j] switch
                {
                    Tile.Empty => '~',
                    Tile.Ship => '~', 
                    Tile.Hit => 'X',
                    Tile.Miss => 'O',
                    Tile.SunkenShip => '#',
                    _ => '?'
                };
                Console.Write(symbol + "  ");
            }
            Console.WriteLine();
        }
    }

    public void ShowFleet(List<IShip> ships)
    {
        Console.WriteLine("Fleet Status:");
        foreach (var ship in ships)
        {
            string status = ship.HitCount >= ship.Length ? "SUNK" : $"Hits: {ship.HitCount}/{ship.Length}";
            Console.WriteLine($"- {ship.Type} ({ship.Length}): {status}");
        }
    }

    public void ShowShipPlacementInfo(IShip ship)
    {
        Console.WriteLine($"\nPlacing {ship.Type} (Length: {ship.Length})");
        Console.WriteLine($"Current orientation: {ship.Orientation}");
        Console.WriteLine("Commands:");
        Console.WriteLine("- Enter coordinates (x y) to place ship");
        Console.WriteLine("- Press 'R' to rotate ship");
        Console.WriteLine("- Press 'C' to clear and restart placement");
    }
}