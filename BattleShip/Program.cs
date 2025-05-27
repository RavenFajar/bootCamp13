using System.Buffers;
using System.Data;
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
struct Coordinate
{
    public int AxisX { get; }
    public int AxisY { get; }
    public Coordinate(int X, int Y)
    {
        AxisX = X;
        AxisY = Y;
    }
}
interface IDisplay
{
    public void ShowMenu();
    public void ShowMessage(string message);
    public void ShowBoard(/*IBoard board*/);
    public void ShowFleet(/*List<IShip> ships*/);
}
public class Display : IDisplay
{
    public void ShowMenu() { }
    public void ShowMessage(string message) { }
    public void ShowBoard(/*Tile tile*/) { }
    public void ShowFleet() { }
}
interface IPlayer
{
    // public string name;
}
public class Player : IPlayer
{
    public string? name;
    public Player(string name)
    {

    }
}
interface IShip
{
    public string Type { get; }
    public string Length { set; get; }
    public string hitCount { set; get; }
    public enum Orientation { orientation }
    // List<Coordinate> Coordinates { get; set; }



}
public class Ship : IShip
{
    public string Type { get; }
    public string Length { set; get; }
    public string hitCount { set; get; }
    // List<Coordinate> Coordinates {}

    // +Orientation Orientation
    // +List~Coordinate~ Coordinates
    public Ship(int length, Orientation orientation, string Type) { }

    interface IBoard
    {
        // public int Size;
        // public Tile[,] Tiles { get; set; }
    }
    public class Board : IBoard
    {
        public int Size;
        public Tile[,] Tiles { get; set; }
        public Board(int size) { }
    }
    public class GameController
    {
        List<IPlayer> _players;
        Dictionary<IPlayer, IBoard> _playerBoard;
        Dictionary<IPlayer, List<IShip>> _playerShips;
        IPlayer _currentPlayer;
        IPlayer _otherPlayer;
        bool _runGame;
        Action<string> OnInitializing;
        Action<string> OnHit;
        Action<string> OnChangingTurn;
        Action<string> OnEndingGame;

        GameController(List<IPlayer> players) { }
        // public void Setup() { }
        // public SetBoard(IPlayer player, IBoard board){}
        void SetShips(IPlayer player, List<IShip> ships) { }
        void Initializing(IPlayer player) { }
        // RotateShip(Orientation orientation)
        // {
        //     return orientation;
        // }
    //     PlaceShip(IShip ship, Coordinate coordinate) { }
    //     GetTile(IPlayer player, Coordinate coordinate) : Tile
    // +SetTile(IPlayer player, Coordinate coordinate, Tile tile) : void
    // +GetShip(IPlayer player, Coordinate coordinate) : IShip
    // +SetHitShip(IShip ship) : void
    // +GetCurrentPlayer() : IPlayer
    // +GetOtherPlayer() : IPlayer

    }
}