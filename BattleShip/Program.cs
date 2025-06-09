using System;
using System.Collections.Generic;
using System.Linq;

 #region ENUMS AND STRUCTS
public enum Orientation
{
    Horizontal,
    Vertical,
}

public enum Tile
{
    Empty,
    Hit,
    Miss,
    Ship,
    SunkenShip,
}

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
#endregion

#region INTERFACES
public interface IPlayer
{
    string Name { get; }
}

public interface IBoard
{
    int Size { get; }
    Tile[,] Tiles { get; }
}

public interface IShip
{
    string Type { get; }
    int Length { get; }
    int HitCount { get; set; }
    Orientation Orientation { get; set; }
    List<Coordinate> Coordinates { get; set; }
}

public interface IDisplay
{
    void ShowMenu();
    void ShowMessage(string message);
    void ShowBoard(IBoard board);
    void ShowAttackBoard(IBoard board);
    void ShowFleet(List<IShip> ships);
    void ShowShipPlacementInfo(IShip ship);
}
#endregion

#region  IMPLEMENTATION
public class Player : IPlayer
{
    public string Name { get; }

    public Player(string name)
    {
        Name = name;
    }
}

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
#endregion

public class GameController
{   
    private List<IPlayer> _players;
    private Dictionary<IPlayer, IBoard> _playerBoards;
    private Dictionary<IPlayer, List<IShip>> _playerShips;
    private IPlayer _currentPlayer;
    private IPlayer _otherPlayer;
    private bool _runGame;

    // Events
    public event Action<IPlayer>? OnInitializing;
    public event Action<IPlayer, IShip>? OnHit;
    public event Action<List<IPlayer>>? OnChangingTurn;
    public event Action? OnEndingGame;

    public GameController(List<IPlayer> players)
    {
        _players = players;
        _playerBoards = new Dictionary<IPlayer, IBoard>();
        _playerShips = new Dictionary<IPlayer, List<IShip>>();
        _runGame = false;
        
        if (players.Count >= 2)
        {
            _currentPlayer = players[0];
            _otherPlayer = players[1];
        }
    }

    // ===============================
    // INITIALIZATION
    // ===============================

    public void Setup()
    {
        foreach (var player in _players)
        {
            OnInitializing?.Invoke(player);
            Initialize(player);
        }
    }

    public bool SetBoard(IPlayer player, IBoard board)
    {
        if (player == null || board == null) return false;
        
        _playerBoards[player] = board;
        return true;
    }

    public bool SetShips(IPlayer player, List<IShip> ships)
    {
        if (player == null || ships == null) return false;
        
        _playerShips[player] = ships;
        return true;
    }

    public void Initialize(IPlayer player)
    {
        // Create default board
        if (!_playerBoards.ContainsKey(player))
        {
            SetBoard(player, new Board(10));
        }

        // Create default fleet
        if (!_playerShips.ContainsKey(player))
        {
            var defaultFleet = new List<IShip>
            {
                new Ship(5, Orientation.Horizontal, "Carrier"),
                new Ship(4, Orientation.Horizontal, "Battleship"),
                new Ship(3, Orientation.Horizontal, "Cruiser"),
                new Ship(3, Orientation.Horizontal, "Submarine"),
                new Ship(2, Orientation.Horizontal, "Destroyer")
            };
            SetShips(player, defaultFleet);
        }
    }

    public Orientation RotateShip(Orientation orientation)
    {
        return orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
    }

    public bool PlaceShip(IShip ship, Coordinate coordinate)
    {
        if (ship == null) return false;

        ship.Coordinates.Clear();
        
        for (int i = 0; i < ship.Length; i++)
        {
            int x = ship.Orientation == Orientation.Horizontal ? coordinate.AxisX + i : coordinate.AxisX;
            int y = ship.Orientation == Orientation.Vertical ? coordinate.AxisY + i : coordinate.AxisY;
            
            ship.Coordinates.Add(new Coordinate(x, y));
        }
        
        return true;
    }
    
    public void SetupPlayerShips(IPlayer player, IDisplay display)
    {
        if (!_playerShips.ContainsKey(player)) return;

        Console.Clear();
        Console.WriteLine($"\n=== {player.Name} - Ship Placement Phase ===");
        
        var ships = _playerShips[player];
        var board = _playerBoards[player];

        foreach (var ship in ships)
        {
            bool shipPlaced = false;
            
            while (!shipPlaced)
            {
                Console.Clear();
                Console.WriteLine($"\n=== {player.Name} - Ship Placement Phase ===");
                Console.WriteLine("\nYour current board:");
                display.ShowBoard(board);
                display.ShowShipPlacementInfo(ship);

                string input = (Console.ReadLine() ?? string.Empty).ToUpper();

                if (input == "R")
                {
                    ship.Orientation = RotateShip(ship.Orientation);
                    continue;
                }
                else if (input == "C")
                {
                    // Clear all ships and restart
                    ClearAllShips(player);
                    SetupPlayerShips(player, display);
                    return;
                }

                // Try to parse coordinates
                string[] coords = (input?.Split(' ')) ?? Array.Empty<string>();
                if (coords?.Length == 2 && int.TryParse(coords[0], out int x) && int.TryParse(coords[1], out int y))
                {
                    var coordinate = new Coordinate(x, y);
                    
                    if (CanPlaceShip(player, ship, coordinate))
                    {
                        PlaceShipOnBoard(player, ship, coordinate);
                        shipPlaced = true;
                        Console.WriteLine($"{ship.Type} placed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Cannot place ship here! Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        Console.Clear();
        Console.WriteLine($"\n=== {player.Name} - Final Board ===");
        display.ShowBoard(board);
        Console.WriteLine("Ship placement complete! Press any key to continue...");
        Console.ReadKey();
    }

    private void ClearAllShips(IPlayer player)
    {
        var board = _playerBoards[player];
        for (int i = 0; i < board.Size; i++)
        {
            for (int j = 0; j < board.Size; j++)
            {
                if (board.Tiles[i, j] == Tile.Ship)
                {
                    board.Tiles[i, j] = Tile.Empty;
                }
            }
        }

        // Reset ship coordinates
        if (_playerShips.ContainsKey(player))
        {
            foreach (var ship in _playerShips[player])
            {
                ship.Coordinates.Clear();
                ship.HitCount = 0;
            }
        }
    }

    private bool CanPlaceShip(IPlayer player, IShip ship, Coordinate coordinate)
    {
        var board = _playerBoards[player];
        
        // Check if ship fits within board bounds
        for (int i = 0; i < ship.Length; i++)
        {
            int x = ship.Orientation == Orientation.Horizontal ? coordinate.AxisX + i : coordinate.AxisX;
            int y = ship.Orientation == Orientation.Vertical ? coordinate.AxisY + i : coordinate.AxisY;
            
            // Check bounds
            if (x < 0 || x >= board.Size || y < 0 || y >= board.Size)
                return false;
            
            // Check if tile is already occupied
            if (board.Tiles[x, y] == Tile.Ship)
                return false;
        }
        
        return true;
    }

    private void PlaceShipOnBoard(IPlayer player, IShip ship, Coordinate coordinate)
    {
        PlaceShip(ship, coordinate);
        
        // Mark ship positions on board
        foreach (var coord in ship.Coordinates)
        {
            SetTile(player, coord, Tile.Ship);
        }
    }



    // ===============================
    // GETTERS AND SETTERS
    // ===============================

    public Tile GetTile(IPlayer player, Coordinate coordinate)
    {
        if (!_playerBoards.ContainsKey(player)) return Tile.Empty;
        
        var board = _playerBoards[player];
        if (coordinate.AxisX < 0 || coordinate.AxisX >= board.Size || 
            coordinate.AxisY < 0 || coordinate.AxisY >= board.Size)
            return Tile.Empty;
        
        return board.Tiles[coordinate.AxisX, coordinate.AxisY];
    }

    public void SetTile(IPlayer player, Coordinate coordinate, Tile tile)
    {
        if (!_playerBoards.ContainsKey(player)) return;
        
        var board = _playerBoards[player];
        if (coordinate.AxisX < 0 || coordinate.AxisX >= board.Size || 
            coordinate.AxisY < 0 || coordinate.AxisY >= board.Size)
            return;
        
        board.Tiles[coordinate.AxisX, coordinate.AxisY] = tile;
    }

    public IShip? GetShip(IPlayer player, Coordinate coordinate)
    {
        if (!_playerShips.ContainsKey(player)) return null;
        
        foreach (var ship in _playerShips[player])
        {
            if (ship.Coordinates.Contains(coordinate))
                return ship;
        }
        
        return null;
    }

    public void SetHitShip(IShip ship)
    {
        if (ship != null)
        {
            ship.HitCount++;
        }
    }

    public IPlayer GetCurrentPlayer()
    {
        return _currentPlayer;
    }

    public IPlayer GetOtherPlayer()
    {
        return _otherPlayer;
    }

    // ===============================
    // GAME FLOW
    // ===============================

    public void Start(IDisplay display)
    {
        _runGame = true;
        
        while (_runGame)
        {
            LaunchHit(_currentPlayer, display);
            
            if (IsFleetDestroyed(_playerShips[_otherPlayer]))
            {
                EndGame();
                break;
            }
            
            ChangeTurn(_players);
        }
    }

    public void LaunchHit(IPlayer player, IDisplay display)
    {
        while(true){
        Console.Clear();
        Console.WriteLine($"\n=== {player.Name}'s Turn ===");
        
        var targetPlayer = GetOtherPlayer();
        var targetBoard = _playerBoards[targetPlayer];
        
        Console.WriteLine($"Attacking {targetPlayer.Name}'s fleet:");
        display.ShowAttackBoard(targetBoard);
        
        Console.WriteLine("\nYour fleet status:");
        display.ShowFleet(_playerShips[player]);
        
            Console.WriteLine($"\nEnter coordinates to attack (x y):");
            
            string input = Console.ReadLine() ?? "";
            string[] coords = input?.Split(' ') ?? Array.Empty<string>();
            if (coords?.Length == 2 && int.TryParse(coords[0], out int x) && int.TryParse(coords[1], out int y))
            {
                Coordinate coordinate = new Coordinate(x, y);

                if (!IsValidPlacement(targetPlayer, coordinate)){
                    Console.WriteLine("Coordinates are out of bounds! Try again.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }
                if (IsHit(targetPlayer, coordinate))
                {
                    Console.WriteLine("You've already attacked this tile! Try another.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }
                if (!IsValidHit(player, coordinate))
                {
                    Console.WriteLine("Invalid target! Try again.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }else
                {
                    Hit(player, new Coordinate(x, y));
                    break;
                }
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

    }

    public void Hit(IPlayer player, Coordinate coordinate)
    {        
        RegisterHit(player, coordinate);
    }

    public void RegisterHit(IPlayer player, Coordinate coordinate)
    {
        var targetPlayer = GetOtherPlayer();
        var ship = GetShip(targetPlayer, coordinate);
        
        if (ship != null)
        {
            SetHitShip(ship);
            SetTile(targetPlayer, coordinate, Tile.Hit);
            
            Console.WriteLine("HIT!");
            OnHit?.Invoke(targetPlayer, ship);
            
            if (IsSunk(ship))
            {
                Console.WriteLine($"{ship.Type} has been sunk!");
                
                // Mark all ship coordinates as sunken
                foreach (var coord in ship.Coordinates)
                {
                    SetTile(targetPlayer, coord, Tile.SunkenShip);
                }
            }
        }
        else
        {
            SetTile(targetPlayer, coordinate, Tile.Miss);
            Console.WriteLine("MISS!");
        }
    }

    // ===============================
    // CHECKS
    // ===============================

    public bool IsValidPlacement(IPlayer player, Coordinate coordinate)
    {
        if (!_playerBoards.ContainsKey(player)) return false;
        
        var board = _playerBoards[player];
        return coordinate.AxisX >= 0 && coordinate.AxisX < board.Size && 
               coordinate.AxisY >= 0 && coordinate.AxisY < board.Size;
    }

    public bool IsValidHit(IPlayer player, Coordinate coordinate)
    {
        var targetPlayer = GetOtherPlayer();
        var tile = GetTile(targetPlayer, coordinate);
        
        return tile == Tile.Empty || tile == Tile.Ship;
    }

    public bool IsHit(IPlayer player, Coordinate coordinate)
    {
        var tile = GetTile(player, coordinate);
        return tile == Tile.Hit || tile == Tile.Miss || tile == Tile.SunkenShip;
    }

    public bool IsSunk(IShip ship)
    {
        return ship != null && ship.HitCount >= ship.Length;
    }

    public bool IsFleetDestroyed(List<IShip> fleet)
    {
        return fleet != null && fleet.All(ship => IsSunk(ship));
    }

    // ===============================
    // ACTIONS
    // ===============================

    public void ChangeTurn(List<IPlayer> players)
    {
        var temp = _currentPlayer;
        _currentPlayer = _otherPlayer;
        _otherPlayer = temp;
        
        OnChangingTurn?.Invoke(players);
    }

    public void EndGame()
    {
        _runGame = false;
        Console.WriteLine($"\nGame Over! {_currentPlayer.Name} wins!");
        OnEndingGame?.Invoke();
    }
}


public class Program
{
    public static void Main()
    {
        var display = new Display();
        
        while (true)
        {
            Console.Clear();
            display.ShowMenu();
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                StartNewGame(display);
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Thanks for playing! Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid key! Press ENTER to play or ESC to exit.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    private static void StartNewGame(IDisplay display)
    {
        Console.Clear();
        Console.WriteLine("=== NEW GAME ===");
        
        // Get player names
        Console.Write("Enter Player 1 name: ");
        string player1Name;
        do
        {
            Console.Write("Enter Player 1 name (cannot be empty): ");
            player1Name = Console.ReadLine() ?? "";
        } while (string.IsNullOrWhiteSpace(player1Name));
        if (string.IsNullOrWhiteSpace(player1Name)) player1Name = "Player 1";
        
        Console.Write("Enter Player 2 name: ");
        string player2Name;
        do
        {
            Console.Write("Enter Player 2 name (cannot be empty): ");
            player2Name = Console.ReadLine() ?? "";
        } while (string.IsNullOrWhiteSpace(player2Name));
        if (string.IsNullOrWhiteSpace(player2Name)) player2Name = "Player 2";
        
        // Create players
        var players = new List<IPlayer>
        {
            new Player(player1Name),
            new Player(player2Name)
        };
        
        // Create game controller
        var game = new GameController(players);
        
        // Setup event handlers
        game.OnInitializing += (player) => Console.WriteLine($"Initializing {player.Name}...");
        game.OnHit += (player, ship) => Console.WriteLine($"{ship.Type} hit!");
        game.OnChangingTurn += (players) => Console.WriteLine("Turn changed!");
        game.OnEndingGame += () => {
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        };
        
        // Setup game
        game.Setup();
        
        // Let each player setup their ships
        foreach (var player in players)
        {
            game.SetupPlayerShips(player, display);
        }
        
        Console.Clear();
        Console.WriteLine("Both players have set up their fleets!");
        Console.WriteLine("Starting battle...");
        Console.WriteLine("Press any key to begin...");
        Console.ReadKey();
        
        // Start the game
        game.Start(display);
    }
}