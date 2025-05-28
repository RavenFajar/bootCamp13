namespace BattleshipGame
{
    // Enumerations
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public enum Tile
    {
        Empty,
        Hit,
        Miss,
        Ship,
        SunkenShip
    }

    public enum FleetType
    {
        Carrier,
        Battleship,
        Cruiser,
        Submarine,
        Destroyer
    }

    // Coordinate struct
    public struct Coordinate
    {
        public int AxisX { get; }
        public int AxisY { get; }

        public Coordinate(int x, int y)
        {
            AxisX = x;
            AxisY = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate other && AxisX == other.AxisX && AxisY == other.AxisY;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AxisX, AxisY);
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"({AxisX}, {AxisY})";
        }
    }

    // Interfaces
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
        void ShowTile(Tile tile);
        void ShowFleet(List<IShip> ships);
        void ShowBoard(IBoard board, bool hideShips = false);
        Coordinate GetCoordinateInput();
        int GetMenuChoice();
        string GetPlayerName();
    }

    // Concrete Implementations
    public class Player : IPlayer
    {
        public string Name { get; }

        public Player(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }

    public class Board : IBoard
    {
        public int Size { get; }
        public Tile[,] Tiles { get; }

        public Board(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Board size must be positive", nameof(size));

            Size = size;
            Tiles = new Tile[size, size];
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
            if (length <= 0)
                throw new ArgumentException("Ship length must be positive", nameof(length));

            Length = length;
            Orientation = orientation;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Coordinates = new List<Coordinate>();
            HitCount = 0;
        }
    }

    public class Display : IDisplay
    {
        public void ShowMenu()
        {
            Console.WriteLine("\n=== BATTLESHIP GAME ===");
            Console.WriteLine("1. Start New Game");
            Console.WriteLine("2. Exit");
            Console.Write("Choose an option: ");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowTile(Tile tile)
        {
            char symbol = tile switch
            {
                Tile.Empty => '~',
                Tile.Ship => 'S',
                Tile.Hit => 'X',
                Tile.Miss => 'O',
                Tile.SunkenShip => '#',
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

        public void ShowBoard(IBoard board, bool hideShips = false)
        {
            Console.Write("  ");
            for (int x = 0; x < board.Size; x++)
            {
                Console.Write($"{x} ");
            }
            Console.WriteLine();

            for (int y = 0; y < board.Size; y++)
            {
                Console.Write($"{y} ");
                for (int x = 0; x < board.Size; x++)
                {
                    Tile tile = board.Tiles[x, y];
                    if (hideShips && tile == Tile.Ship)
                        tile = Tile.Empty;
                    
                    ShowTile(tile);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public Coordinate GetCoordinateInput()
        {
            while (true)
            {
                Console.Write("Enter coordinates (x,y): ");
                string input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input))
                    continue;

                string[] parts = input.Split(',');
                if (parts.Length == 2 && 
                    int.TryParse(parts[0].Trim(), out int x) && 
                    int.TryParse(parts[1].Trim(), out int y))
                {
                    return new Coordinate(x, y);
                }
                
                Console.WriteLine("Invalid format. Please enter as 'x,y' (e.g., '3,4')");
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

    // Main Game Controller
    public class GameController
    {
        private List<IPlayer> _players;
        private Dictionary<IPlayer, IBoard> _playerBoard;
        private Dictionary<IPlayer, List<IShip>> _playerShips;
        private IPlayer _currentPlayer;
        private IPlayer _otherPlayer;
        private bool _runGame;
        private IDisplay _display;

        // Events
        public event Action<IPlayer> OnInitializing;
        public event Action<IPlayer, IShip> OnHit;
        public event Action<List<IPlayer>> OnChangingTurn;
        public event Action OnEndingGame;

        public GameController(List<IPlayer> players, IDisplay display = null)
        {
            _players = players ?? throw new ArgumentNullException(nameof(players));
            _display = display ?? new Display();
            _playerBoard = new Dictionary<IPlayer, IBoard>();
            _playerShips = new Dictionary<IPlayer, List<IShip>>();
            _runGame = false;

            if (_players.Count != 2)
                throw new ArgumentException("Game requires exactly 2 players");
        }

        // Initialization Methods
        public void Setup()
        {
            foreach (var player in _players)
            {
                var board = new Board(10);
                var ships = CreateDefaultFleet();
                
                SetBoard(player, board);
                SetShips(player, ships);
                Initialize(player);
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

        public bool SetShips(IPlayer player, List<IShip> ships)
        {
            if (player == null || ships == null)
                return false;

            _playerShips[player] = ships;
            return true;
        }

        public void Initialize(IPlayer player)
        {
            OnInitializing?.Invoke(player);
            
            _display.ShowMessage($"\n{player.Name}, place your ships!");
            var board = _playerBoard[player];
            var ships = _playerShips[player];

            foreach (var ship in ships)
            {
                bool placed = false;
                while (!placed)
                {
                    _display.ShowBoard(board);
                    _display.ShowMessage($"\nPlacing {ship.Type} (Length: {ship.Length})");
                    
                    var coordinate = _display.GetCoordinateInput();
                    
                    Console.Write("Orientation (H for Horizontal, V for Vertical): ");
                    string orientationInput = Console.ReadLine()?.ToUpper();
                    var orientation = orientationInput == "V" ? Orientation.Vertical : Orientation.Horizontal;
                    
                    ship.Orientation = orientation;
                    
                    if (PlaceShip(ship, coordinate))
                    {
                        placed = true;
                        _display.ShowMessage($"{ship.Type} placed successfully!");
                    }
                    else
                    {
                        _display.ShowMessage("Invalid placement. Try again.");
                    }
                }
            }
        }

        public Orientation RotateShip(Orientation orientation)
        {
            return orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
        }

        public bool PlaceShip(IShip ship, Coordinate coordinate)
        {
            if (!IsValidPlacement(_currentPlayer, coordinate))
                return false;

            var board = _playerBoard[_currentPlayer];
            var coordinates = new List<Coordinate>();

            // Calculate ship coordinates
            for (int i = 0; i < ship.Length; i++)
            {
                int x = coordinate.AxisX + (ship.Orientation == Orientation.Horizontal ? i : 0);
                int y = coordinate.AxisY + (ship.Orientation == Orientation.Vertical ? i : 0);
                
                var shipCoord = new Coordinate(x, y);
                
                if (x < 0 || x >= board.Size || y < 0 || y >= board.Size ||
                    board.Tiles[x, y] != Tile.Empty)
                {
                    return false;
                }
                
                coordinates.Add(shipCoord);
            }

            // Place ship on board
            ship.Coordinates = coordinates;
            foreach (var coord in coordinates)
            {
                board.Tiles[coord.AxisX, coord.AxisY] = Tile.Ship;
            }

            return true;
        }

        // Getter/Setter Methods
        public Tile GetTile(IPlayer player, Coordinate coordinate)
        {
            var board = _playerBoard[player];
            if (coordinate.AxisX < 0 || coordinate.AxisX >= board.Size ||
                coordinate.AxisY < 0 || coordinate.AxisY >= board.Size)
                return Tile.Empty;

            return board.Tiles[coordinate.AxisX, coordinate.AxisY];
        }

        public void SetTile(IPlayer player, Coordinate coordinate, Tile tile)
        {
            var board = _playerBoard[player];
            if (coordinate.AxisX >= 0 && coordinate.AxisX < board.Size &&
                coordinate.AxisY >= 0 && coordinate.AxisY < board.Size)
            {
                board.Tiles[coordinate.AxisX, coordinate.AxisY] = tile;
            }
        }

        public IShip GetShip(IPlayer player, Coordinate coordinate)
        {
            var ships = _playerShips[player];
            return ships.FirstOrDefault(ship => ship.Coordinates.Contains(coordinate));
        }

        public void SetHitShip(IShip ship)
        {
            ship.HitCount++;
        }

        public IPlayer GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        public IPlayer GetOtherPlayer()
        {
            return _otherPlayer;
        }

        // Game Flow Methods
        public void Start()
        {
            _runGame = true;
            
            while (_runGame)
            {
                _display.ShowMessage($"\n{_currentPlayer.Name}'s turn!");
                _display.ShowMessage("Enemy board:");
                _display.ShowBoard(_playerBoard[_otherPlayer], true);
                
                LaunchHit(_currentPlayer);
                
                if (IsFleetDestroyed(_playerShips[_otherPlayer]))
                {
                    _display.ShowMessage($"\n{_currentPlayer.Name} wins!");
                    EndGame();
                }
                else
                {
                    ChangeTurn(_players);
                }
            }
        }

        public void LaunchHit(IPlayer player)
        {
            var coordinate = _display.GetCoordinateInput();
            Hit(player, coordinate);
        }

        public void Hit(IPlayer player, Coordinate coordinate)
        {
            if (!IsValidHit(player, coordinate))
            {
                _display.ShowMessage("Invalid target. Try again.");
                LaunchHit(player);
                return;
            }

            RegisterHit(player, coordinate);
        }

        public void RegisterHit(IPlayer player, Coordinate coordinate)
        {
            var target = _otherPlayer;
            var tile = GetTile(target, coordinate);
            
            if (tile == Tile.Ship)
            {
                var ship = GetShip(target, coordinate);
                SetHitShip(ship);
                SetTile(target, coordinate, Tile.Hit);
                
                _display.ShowMessage("Hit!");
                OnHit?.Invoke(target, ship);
                
                if (IsSunk(ship))
                {
                    _display.ShowMessage($"{ship.Type} sunk!");
                    foreach (var coord in ship.Coordinates)
                    {
                        SetTile(target, coord, Tile.SunkenShip);
                    }
                }
            }
            else
            {
                SetTile(target, coordinate, Tile.Miss);
                _display.ShowMessage("Miss!");
            }
        }

        // Validation Methods
        public bool IsValidPlacement(IPlayer player, Coordinate coordinate)
        {
            var board = _playerBoard[player];
            return coordinate.AxisX >= 0 && coordinate.AxisX < board.Size &&
                   coordinate.AxisY >= 0 && coordinate.AxisY < board.Size;
        }

        public bool IsValidHit(IPlayer player, Coordinate coordinate)
        {
            var board = _playerBoard[_otherPlayer];
            if (coordinate.AxisX < 0 || coordinate.AxisX >= board.Size ||
                coordinate.AxisY < 0 || coordinate.AxisY >= board.Size)
                return false;

            var tile = GetTile(_otherPlayer, coordinate);
            return tile != Tile.Hit && tile != Tile.Miss && tile != Tile.SunkenShip;
        }

        public bool IsHit(IPlayer player, Coordinate coordinate)
        {
            var tile = GetTile(player, coordinate);
            return tile == Tile.Hit || tile == Tile.SunkenShip;
        }

        public bool IsSunk(IShip ship)
        {
            return ship.HitCount >= ship.Length;
        }

        public bool IsFleetDestroyed(List<IShip> fleet)
        {
            return fleet.All(ship => IsSunk(ship));
        }

        // Other Actions
        public void ChangeTurn(List<IPlayer> players)
        {
            OnChangingTurn?.Invoke(players);
            (_currentPlayer, _otherPlayer) = (_otherPlayer, _currentPlayer);
        }

        public void EndGame()
        {
            _runGame = false;
            OnEndingGame?.Invoke();
        }

        // Helper Methods
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
    }

    // Program Entry Point
    public class Program
    {
        public static void Main(string[] args)
        {
            var display = new Display();
            
            while (true)
            {
                display.ShowMenu();
                int choice = display.GetMenuChoice();
                
                switch (choice)
                {
                    case 1:
                        StartNewGame(display);
                        break;
                    case 2:
                        display.ShowMessage("Thanks for playing!");
                        return;
                    default:
                        display.ShowMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void StartNewGame(IDisplay display)
        {
            display.ShowMessage("\nSetting up new game...");
            
            var players = new List<IPlayer>();
            for (int i = 1; i <= 2; i++)
            {
                display.ShowMessage($"Player {i}:");
                string name = display.GetPlayerName();
                players.Add(new Player(name));
            }

            var gameController = new GameController(players, display);
            
            gameController.OnInitializing += player => 
                display.ShowMessage($"Initializing {player.Name}...");
            
            gameController.OnHit += (player, ship) => 
                display.ShowMessage($"{player.Name}'s {ship.Type} was hit!");
            
            gameController.OnChangingTurn += players => 
                display.ShowMessage("Changing turns...");
            
            gameController.OnEndingGame += () => 
                display.ShowMessage("Game ended!");

            gameController.Setup();
            gameController.Start();
        }
    }
}