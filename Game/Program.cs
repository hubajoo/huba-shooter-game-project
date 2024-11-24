using DungeonCrawl.GameSetup;

namespace DungeonCrawl.Program;

/// <summary>
/// Class <c>Program</c> provides an entry point for the game.
/// </summary>
public class Program
{
  /// <summary>
  /// The entry point of the program.
  /// </summary>
  public static void Main()
  {
    SettingsReader settingsReader = new SettingsReader();
    string path = "Data/settings.txt";
    IGameSettings settings = settingsReader.ReadSettings(path);

    // Setup the engine and create the main window.
    SadConsole.Game.Create(settings.ViewPortWidth, settings.ViewPortHeight);

    // Creates Setup class
    Setup setup = new Setup(settings);

    // Hook the start event so we can add consoles to the system.
    SadConsole.Game.Instance.OnStart = setup.Init;

    // Start the game.
    SadConsole.Game.Instance.Run();
    SadConsole.Game.Instance.Dispose();
  }
}