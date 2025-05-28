// Console.WriteLine("Hello, World!");

// public enum FleetType
// {
//     Carrier,
//     Battleship,
//     Cruiser,
//     Submarine,
//     Destroyer
// }
public enum Orientation
{
    Horizontal,
    Vertical
}
public enum TileType
{
    Empty,
    Hit,
    Miss,
    Ship,
    SunkenShip
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
}
public interface IDisplay
{
    public void ShowMenu();
    public void ShowMessage(string message);
    public void ShowTile(TileType tile);
    public void ShowFleet(List<IShip> ships);
}
public class Display : IDisplay
{
    public void ShowMenu()
    {
        Console.WriteLine("===============================");
        Console.WriteLine("   Welcome to Battleship!      ");
        Console.WriteLine("===============================");
        Console.WriteLine("1. Start New Game");
        Console.WriteLine("2. Exit");
        Console.WriteLine("Select an option:");
    }
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);

    }
    public void ShowTile(TileType tile)
    {
        char symbol = tile switch
        {
            TileType.Empty => '~',
            TileType.Ship => 'S',
            TileType.Hit => 'X',
            TileType.Miss => 'O',
            TileType.SunkenShip => '#',
            _ => '?'
        };
        Console.Write(symbol);

    }
    public void ShowFleet(List<IShip> ships)
    {
        Console.WriteLine("\nFleet Status:");
        foreach (var ship in ships)
        {
            string status = ship.HitCount >= ship.Length ? "SUNK" : $"Hits: {ship.HitCount}/{ship.Length}";
            Console.WriteLine($"{ship.Type} ({ship.Length}) - {status}");
        }
    }
    public int GetMenuChoice()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
                return choice;

            Console.Write("Please enter a valid number: ");
        }
    }

    public string GetPlayerName()
    {
        Console.Write("Enter player name: ");
        return Console.ReadLine() ?? "Player";
    }
}
public interface IPlayer
{
    public string Name { get; }
}
public class Player : IPlayer
{
    public string Name { get; }
    public Player(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
public interface IShip
{
    string Type { set; get; }
    int Length { get; }
    int HitCount { get; set; }
    Orientation Orientation { get; set; }
    List<Coordinate> Coordinates { get; set; }
}
public class Ship : IShip
{
    public string Type { set; get; }
    public int Length { get; }
    public int HitCount { get; set; }
    public Orientation Orientation { get; set; }
    public List<Coordinate> Coordinates { get; set; }
    public Ship(int length, Orientation orientation, string type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        if (length <= 0) throw new ArgumentNullException(nameof(length));
        Length = length;
        Orientation = orientation;
        HitCount = 0;
        Coordinates = new List<Coordinate>();
    }
}
public interface IBoard
{
    public int Size { set; get; }
    public TileType[,] Tiles { get; set; }
}
public class Board : IBoard
{
    public int Size { set; get; }
    public TileType[,] Tiles { get; set; }
    public Board(int size)
    {
        if (size <= 0 && size <= 10) throw new ArgumentOutOfRangeException(nameof(size), "Board size must positif and minimunn 10.");
        Size = size;
        Tiles = new TileType[size, size];

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Tiles[i, j] = TileType.Empty;
            }
        }
    }
}
public class GameController
{
    private readonly List<IPlayer> _players;
    private Dictionary<IPlayer, IBoard> _playerBoard;
    private Dictionary<IPlayer, List<IShip>> _playerShips;
    private IPlayer _currentPlayer;
    private IPlayer _otherPlayer;
    private bool _runGame;
    public Action<string> OnInitializing;
    public Action<string> OnHit;
    public Action<string> OnChangingTurn;
    public Action<string> OnEndingGame;

    public GameController(List<IPlayer> players)
    {
        _players = players ?? throw new ArgumentNullException(nameof(players))
        new Display();
        _playerBoard = new Dictionary<IPlayer, IBoard>();
        _playerShips = new Dictionary<IPlayer, List<IShip>>();
        _runGame = false;

        if (_players.Count != 2)
            throw new ArgumentException("Game requires exactly 2 players");
    }
    private List<IShip> CreateDefaultFleet()
        {
            return new List<IShip>
            {
                new Ship(5, Orientation.Horizontal, "Carrier"),
                new Ship(4, Orientation.Horizontal, "Battleship"),
                new Ship(3, Orientation.Horizontal, "Cruiser"),
                new Ship(3, Orientation.Horizontal, "Submarine"),
                new Ship(2, Orientation.Horizontal, "Destroyer")
            };
        }
    public void Setup()
    {
        foreach (var player in _players)
        {
            var board = new Board(10);
            var ships = CreateDefaultFleet();

            SetBoard(player, board){ }
            SetShips(player, ships);
            Initializing(player);
        }

        _currentPlayer = _players[0];
        _otherPlayer = _players[1];
    }
    public bool SetBoard(IPlayer player, IBoard board)
        {
            if (player == null || board == null)
                return false;

            _playerBoard[player] = board;
            return true;
        }
    public bool void SetShips(IPlayer player, List<IShip> ships) { 
        if (player == null || ships == null){
            return false}
            _playerShips[player] = ships;
            return true
    }
    public void Initializing(IPlayer player) { 
    }
    public RotateShip(Orientation orientation) { }
    public PlaceShip(IShip ship, Coordinate coordinate) { }
    public GetTile(IPlayer player, Coordinate coordinate) { }
    public void SetTile(IPlayer player, Coordinate coordinate, Tile tile) { }
    public GetShip(IPlayer player, Coordinate coordinate) { }
    public void SetHitShip(IShip ship) { }
    public GetCurrentPlayer() { }
    public GetOtherPlayer() { }
    public void Start() { }
    public void LaunchHit(IPlayer player) { }
    public void Hit(IPlayer player, Coordinate coordinate) { }
    public void RegisterHit(IPlayer player, Coordinate coordinate) { }
    public IsValidPlacement(IPlayer player, Coordinate coordinate) { }
    public IsValidHit(IPlayer player, Coordinate coordinate) { }
    public IsHit(IPlayer player, Coordinate coordinated) { }
    public IsSunk(IShip ship) { }
    public IsFleetDestroyed(List<IShip> fleet) { }
    public void ChangeTurn(List<IPlayer> players) { }
    public void EndGame() { }
}
