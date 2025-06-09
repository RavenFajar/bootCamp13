using System;
using System.Collections.Generic;
using System.Linq;
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
        var game = new GameController(players, display);
        
        // Setup event handlers
        game.OnInitializing += (player) => Console.WriteLine($"Initializing {player.Name}...");
        game.OnHit += (player, ship) => Console.WriteLine($"{ship.Type} hit!");
        game.OnChangingTurn += (players) => Console.WriteLine("Turn changed!");
        game.OnEndingGame += () => {
            Console.Clear();
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
        game.Start();
    }
}