using SadConsole;

namespace DungeonCrawl;

/// <summary>
/// Class <c>Program</c> provides an entry point for the game.
/// </summary>
public class Program
{
  private const int ViewPortWidth = 100;
  private const int ViewPortHeight = 40;

  //private Dictionary<string, string> Settings = new Dictionary<string, string>();

  /// <summary>
  /// The entry point of the program.
  /// </summary>
  public static void Main()
  {

    // Setup the engine and create the main window.
    Game.Create(ViewPortWidth, ViewPortHeight);

    // Creates Setup class
    Setup setup = new Setup();

    // Hook the start event so we can add consoles to the system.
    Game.Instance.OnStart = setup.Init;

    /*
        // Creates Setup class
        GameOver gameOver = new GameOver();

        // Hook the end event so we can add display the end screen.
        Game.Instance.OnEnd = gameOver.End;
    */
    // Start the game.
    Game.Instance.Run();
    Game.Instance.Dispose();
  }
}