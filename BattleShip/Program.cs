using System.Buffers;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");
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
    public void ShowTile(Tile tile);
    public void ShowFleet(List<IShip> ships);
}
public class Display : IDisplay
{
    public void ShowMenu() { }
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
    public void ShowTile(Tile tile) { }
    public void ShowFleet(List<IShip> ships) { }
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
    string Type { get; }
    int Length { get; }
    int HitCount { get; set; } // Ubah ke set agar bisa diupdate
    Orientation Orientation { get; set; } // Ubah ke set agar bisa dirotasi
    List<Coordinate> Coordinates { get; set; }
}
public class Ship : IShip
{
    public string Type { get; }
    public int Length { get; }
    public int HitCount { get; set; }
    public Orientation Orientation { get; set; }
    public List<Coordinate> Coordinates { get; set; }
    public Ship(int length, Orientation orientation, string Type) { }
}
public interface IBoard
{
    public int Size { set; get; }
    public Tile[,] Tiles { get; set; }
}
public class Board : IBoard
{
    public int Size { set; get; }
    public Tile[,] Tiles { get; set; }
    public Board(int size)
    {
        if (size <= 0 && size <= 10) throw new ArgumentOutOfRangeException(nameof(size), "Board size harus positif dan minimum size 10.");
        Size = size;
        Tiles = new Tile[size, size];

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Tiles[i, j] = Tile.Empty;
            }
        }
    }
}
// public class GameController
// {
//     private readonly List<IPlayer> _players;
//     private Dictionary<IPlayer, IBoard> _playerBoard;
//     private Dictionary<IPlayer, List<IShip>> _playerShips;
//     private IPlayer _currentPlayer;
//     private IPlayer _otherPlayer;
//     private bool _runGame;
//     public Action<string> OnInitializing;
//     public Action<string> OnHit;
//     public Action<string> OnChangingTurn;
//     public Action<string> OnEndingGame;

//     public GameController(List<IPlayer> players) { }
//     public void Setup() { }
//     public SetBoard(IPlayer player, IBoard board) { }
//     public void SetShips(IPlayer player, List<IShip> ships) { }
//     public void Initializing(IPlayer player) { }
//     public RotateShip(Orientation orientation) { }
//     public PlaceShip(IShip ship, Coordinate coordinate) { }
//     public GetTile(IPlayer player, Coordinate coordinate) { }
//     public void SetTile(IPlayer player, Coordinate coordinate, Tile tile) { }
//     public GetShip(IPlayer player, Coordinate coordinate) { }
//     public void SetHitShip(IShip ship) { }
//     public GetCurrentPlayer() { }
//     public GetOtherPlayer() { }
//     public void Start() { }
//     public void LaunchHit(IPlayer player) { }
//     public void Hit(IPlayer player, Coordinate coordinate) { }
//     public void RegisterHit(IPlayer player, Coordinate coordinate) { }
//     public IsValidPlacement(IPlayer player, Coordinate coordinate) { }
//     public IsValidHit(IPlayer player, Coordinate coordinate) { }
//     public IsHit(IPlayer player, Coordinate coordinated) { }
//     public IsSunk(IShip ship) { }
//     public IsFleetDestroyed(List<IShip> fleet) { }
//     public void ChangeTurn(List<IPlayer> players) { }
//     public void EndGame() { }


// }
