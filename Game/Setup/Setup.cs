
using DungeonCrawl.Maps;
using DungeonCrawl.GameObjects;
using DungeonCrawl.Ui;
using DungeonCrawl.UI;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Mechanics;
using System.Xml.XPath;
namespace Setup;

public class GameSetup
{
  /// <summary>
  /// Initializes the game.
  /// </summary>
  private IGameSettings _settings;

  public GameSetup(IGameSettings settings)
  {
    _settings = settings;
  }

  public void Init()
  {
    // Creates the leader board handler
    ILeaderBoardHandler leaderBoardHandler = new LeaderBoardHandler(_settings);
    leaderBoardHandler.FetchLeaderboard().Wait();
    leaderBoardHandler.ReadLeaderBoard();

    // Creates screensurface
    var screenSurface = new ScreenSurface(_settings.ViewPortWidth - _settings.StatsConsoleWidth, _settings.ViewPortHeight);
    screenSurface.UseMouse = false;

    // Drawing logic setup
    var screenObjectManager = new ScreenObjectManager(screenSurface);

    // Creates the map
    Map map = new Map(screenSurface, screenObjectManager);
    map.SurfaceObject.Position = new Point(_settings.StatsConsoleWidth, 0);

    // Creates player
    Player player = new Player(_settings.UserName, screenSurface.Surface.Area.Center, screenObjectManager, map, 
    _settings.PlayerHealth, _settings.PlayerDamage, _settings.PlayerRange, leaderBoardHandler.AddToLeaderboard);
    map.AddUserControlledObject(player);

    // Creates monster types
    MonsterTypes monsterTypes = new MonsterTypes(MonsterCreation.CreateMonsterTypes());

    // Creates spawn logic
    var spawnScrip = new SpawnScript();

    // Creates wave and sets spawn logic
    MonsterWave wave = new MonsterWave(map, screenObjectManager, monsterTypes, spawnScrip);
    map.SetSpawnLogic(wave);

    // Creates UI elements
    var PlayerStatsConsole = new PlayerStatsConsole(_settings.StatsConsoleWidth, _settings.ViewPortHeight, player, _settings.UserName, 
    leaderBoardHandler.ReadLeaderBoard().ToArray())
    {
      Position = new Point(0, 0)
    };
    var EndScreen = new EndScreen(_settings, leaderBoardHandler)
    {
      Position = new Point(0, 0)
    };
    var rootScreen = new RootScreen(map);
    screenObjectManager.SetMainScreen(rootScreen);
    screenObjectManager.SetConsole(PlayerStatsConsole);
    screenObjectManager.SetEndScreen(EndScreen);
    map.SurfaceObject.Position = new Point(_settings.StatsConsoleWidth, 0);

    // Sets the main screen
    Game.Instance.Screen = rootScreen;
    Game.Instance.Screen.IsFocused = true;



    // This is needed because we replaced the initial screen object with our own.
    Game.Instance.DestroyDefaultStartingConsole();
  }

}