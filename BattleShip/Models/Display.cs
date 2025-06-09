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

    public void ShipPlacementPhase(IPlayer player)
    {
        Console.Clear();
        Console.WriteLine($"\n=== {player.Name} - Ship Placement Phase ===");
        Console.WriteLine("\nYour current board:");
    }
    public void ShowShipPlacementInfo(Ship ship)
    {
        Console.WriteLine($"\nPlacing {ship.Type} (Length: {ship.Length})");
        Console.WriteLine($"Current orientation: {ship.Orientation}");
        Console.WriteLine("Commands:");
        Console.WriteLine("- Enter coordinates (x y) to place ship");
        Console.WriteLine("- Press 'R' to rotate ship");
        Console.WriteLine("- Press 'C' to clear and restart placement");
    }

    public void FinalShipPlacement(IPlayer player, IBoard board)
    {
        Console.Clear();
        Console.WriteLine($"\n=== {player.Name} - Final Board ===");
        ShowBoard(board);
        Console.WriteLine("Ship placement complete!");
        NextPhase();
    }

    public void InvalidShipPlacement()
    {
        Console.WriteLine("Cannot place ship here! Try another position.");
        NextPhase();
    }

    public void InvalidFormat()
    {
        Console.WriteLine("Invalid input format! Please enter coordinates as 'x y' (e.g., '3 4').");
        NextPhase();
    }

    public void PlacementSuccess(IShip ship)
    {
        Console.WriteLine($"{ship.Type} placed successfully!");

    }

    public void AttackPhase(IPlayer player, IPlayer targetPlayer, Dictionary<IPlayer, List<IShip>> _playerShips, IBoard targetBoard)
    {
        Console.Clear();
        Console.WriteLine($"\n=== {player.Name}'s Turn ===");
        Console.WriteLine($"Attacking {targetPlayer.Name}'s fleet:");
        ShowAttackBoard(targetBoard);
        Console.WriteLine("\nYour fleet status:");
        ShowFleet(_playerShips[player]);
        Console.WriteLine($"\nEnter coordinates to attack (x y):");
    }

    public void OutOfBounds()
    {
        Console.WriteLine("Attack coordinates are out of bounds! Please try again.");
        NextPhase();
    }

    public void AlreadyAttack()
    {
        Console.WriteLine("You've already attacked this tile! Try another.");
        NextPhase();
    }
    public void InvalidAttack()
    {
        Console.WriteLine("Invalid attack! You cannot attack this tile.");
        NextPhase();
    }

    public void NextPhase()
    {
        Console.WriteLine("Press any key to proceed to the next phase...");
        Console.ReadKey();
    }

    public void ShipSunk(IShip ship)
    {
        Console.WriteLine($"The {ship.Type} has been sunk!");
        NextPhase();
    }
}