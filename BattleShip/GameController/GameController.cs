public class GameController
{
    private List<IPlayer> _players;
    private Dictionary<IPlayer, IBoard> _playerBoards;
    private Dictionary<IPlayer, List<IShip>> _playerShips;
    private IPlayer _currentPlayer;
    private IPlayer _otherPlayer;
    private readonly IDisplay _display;
    private bool _runGame;

    // Events
    public Action<IPlayer>? OnInitializing;
    public Action<IPlayer, IShip>? OnHit;
    public Action<List<IPlayer>>? OnChangingTurn;
    public Action? OnEndingGame;

    public GameController(List<IPlayer> players, IDisplay display)
    {
        _players = players;
        _playerBoards = new Dictionary<IPlayer, IBoard>();
        _playerShips = new Dictionary<IPlayer, List<IShip>>();
        _runGame = false;
        _currentPlayer = players[0];
        _otherPlayer = players[1];
        _display = display;
    }

    // INITIALIZATION
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
        if (player == null || board == null)
            return false;
        _playerBoards[player] = board;
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
                new Ship(2, Orientation.Horizontal, "Destroyer"),
            };
            SetShips(player, defaultFleet);
        }
    }

    public Orientation RotateShip(Orientation orientation)
    {
        return orientation == Orientation.Horizontal
            ? Orientation.Vertical
            : Orientation.Horizontal;
    }

    public bool PlaceShip(IShip ship, Coordinate coordinate)
    {
        if (ship == null)
            return false;

        if (ship is Ship concreteShip)
        {
            concreteShip.Coordinates.Clear();
            for (int i = 0; i < ship.Length; i++)
            {
                int x =
                    concreteShip.Orientation == Orientation.Horizontal
                        ? coordinate.AxisX + i
                        : coordinate.AxisX;
                int y =
                    concreteShip.Orientation == Orientation.Vertical ? coordinate.AxisY + i : coordinate.AxisY;
                
                concreteShip.Coordinates.Add(new Coordinate(x, y));
            }
            
            return true;
        }
        return false;
    }
    
    public void SetupPlayerShips(IPlayer player, IDisplay display)
    {
        if (!_playerShips.ContainsKey(player)) return;
        
        var ships = _playerShips[player];
        var board = _playerBoards[player];

        foreach (var ship in ships)
        {   

            if (ship is not Ship concreteShip) continue;
            
            bool shipPlaced = false;
            
            while (!shipPlaced)
            {

                display.ShipPlacementPhase(player);
                display.ShowBoard(board);
                display.ShowShipPlacementInfo(concreteShip);

                string input = (Console.ReadLine() ?? string.Empty).ToUpper();

                if (input == "R")
                {
                    concreteShip.Orientation = RotateShip(concreteShip.Orientation);
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

                    if (CanPlaceShip(player, concreteShip, coordinate))
                    {
                        PlaceShipOnBoard(player, concreteShip, coordinate);
                        shipPlaced = true;
                        display.PlacementSuccess(concreteShip);
                    }
                    else
                    {
                        display.InvalidShipPlacement();
                    }
                }
                else
                {
                    display.InvalidFormat();
                }
            }
        }
        display.FinalShipPlacement(player, board);
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
         if (ship is Ship concreteShip)
        {
        // Check if ship fits within board bounds
        for (int i = 0; i < ship.Length; i++)
        {
            int x = concreteShip.Orientation == Orientation.Horizontal ? coordinate.AxisX + i : coordinate.AxisX;
            int y = concreteShip.Orientation == Orientation.Vertical ? coordinate.AxisY + i : coordinate.AxisY;
            
            // Check bounds
            if (x < 0 || x >= board.Size || y < 0 || y >= board.Size)
                return false;
            
            // Check if tile is already occupied
            if (board.Tiles[x, y] == Tile.Ship)
                return false;
        }
        
        return true;
        }
        return false;
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

    public IPlayer GetOtherPlayer()
    {
        return _otherPlayer;
    }
    // GAME FLOW
    public void Start()
    {
        _runGame = true;
        
        while (_runGame)
        {
            LaunchHit(_currentPlayer);
            
            if (IsFleetDestroyed(_playerShips[_otherPlayer]))
            {
                EndGame();
                break;
            }
            
            ChangeTurn(_players);
        }
    }

    public void LaunchHit(IPlayer player)
    {
        while(true){
            var targetPlayer = GetOtherPlayer();
            var targetBoard = _playerBoards[targetPlayer];

            _display.AttackPhase(player, targetPlayer, _playerShips, targetBoard);

            string input = Console.ReadLine() ?? "";
            string[] coords = input?.Split(' ') ?? Array.Empty<string>();
            if (coords?.Length == 2 && int.TryParse(coords[0], out int x) && int.TryParse(coords[1], out int y))
            {
                Coordinate coordinate = new Coordinate(x, y);

                if (!IsValidPlacement(targetPlayer, coordinate)){
                    _display.OutOfBounds();
                    continue;
                }
                if (IsHit(targetPlayer, coordinate))
                {
                    _display.AlreadyAttack();
                    continue;
                }
                if (!IsValidHit(player, coordinate))
                {
                    _display.InvalidAttack();
                    continue;
                }else
                {
                    Hit(player, new Coordinate(x, y));
                    break;
                }
            }
        }
        _display.NextPhase();

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
            
            OnHit?.Invoke(targetPlayer, ship);
            
            if (IsSunk(ship))
            {
                _display.ShipSunk(ship);
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
            _display.ShowMessage("Miss");
        }
    }

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
    // ACTIONS

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