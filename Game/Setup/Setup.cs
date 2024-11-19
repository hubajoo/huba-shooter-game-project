
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DungeonCrawl.Maps;
using DungeonCrawl.GameObjects;
using DungeonCrawl.Ui;
using DungeonCrawl.UI;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives;
using DungeonCrawl.Mechanics;
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

    ILeaderBoardHandler leaderBoardHandler = new LeaderBoardHandler(_settings.ServerUrl, _settings.UserName);

    // Creates screensurface
    var screenSurface = new ScreenSurface(_settings.ViewPortWidth, _settings.ViewPortHeight);
    screenSurface.UseMouse = false;

    // Drawing logic setup
    var screenObjectManager = new ScreenObjectManager(screenSurface);

    // Creates the map
    Map map = new Map(screenSurface, screenObjectManager);
    map.SurfaceObject.Position = new Point(_settings.StatsConsoleWidth, 0);

    // Creates player
    Player player = new Player(screenSurface.Surface.Area.Center, screenObjectManager, map, _settings.PlayerHealth, _settings.PlayerDamage, _settings.PlayerRange);
    map.AddUserControlledObject(player);



    // Creates monster types
    MonsterTypes monsterTypes = new MonsterTypes(MonsterCreation.CreateMonsterTypes());

    // Creates spawn logic
    var spawnScrip = new SpawnScript();

    // Creates wave and sets spawn logic
    MonsterWave wave = new MonsterWave(map, screenObjectManager, monsterTypes, spawnScrip);
    map.SetSpawnLogic(wave);



    // Creates UI elements
    var PlayerStatsConsole = new PlayerStatsConsole(_settings.StatsConsoleWidth, _settings.ViewPortHeight, player, _settings.UserName)
    {
      Position = new Point(0, 0)
    };
    var EndScreen = new LeaderBoard(_settings.ViewPortWidth, _settings.ViewPortHeight, player, _settings.UserName)
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