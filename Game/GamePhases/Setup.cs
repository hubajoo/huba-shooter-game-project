
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

public class Setup
{
  /// <summary>
  /// Initializes the game.
  /// </summary>
  private Dictionary<string, string> Settings = new Dictionary<string, string>(){
      { "name", "Huba" },
      {"statsConsoleWidth", "25"},
      {"difficulty", "0"}
  };
  public void Init()
  {
    // Converts Settings Dictionary to variables
    int statsConsoleWidth = Int32.Parse(Settings["statsConsoleWidth"]);
    int mapWidth = Game.Instance.ScreenCellsX - statsConsoleWidth;
    int mapHeight = Game.Instance.ScreenCellsY;
    int difficulty = Int32.Parse(Settings["difficulty"]);


    // Creates screensurface
    var screenSurface = new ScreenSurface(mapWidth, mapHeight);
    screenSurface.UseMouse = false;

    // Drawing logic setup
    var screenObjectManager = new ScreenObjectManager(screenSurface);



    // Creates the map
    Map map = new Map(screenSurface, screenObjectManager);
    map.SurfaceObject.Position = new Point(statsConsoleWidth, 0);

    // Creates player
    Player player = new Player(screenSurface.Surface.Area.Center, screenObjectManager, map);
    map.AddUserControlledObject(player);


    //
    MonsterWave wave = new MonsterWave(map, screenObjectManager, difficulty);

    // Creates UI elements
    var PlayerStatsConsole = new PlayerStatsConsole(statsConsoleWidth, mapHeight, player, Settings["name"])
    {
      Position = new Point(0, 0)
    };
    var leaderBoard = new LeaderBoard(statsConsoleWidth, mapHeight, player, Settings["name"])
    {
      Position = new Point(0, 0)
    };
    var rootScreen = new RootScreen(map, PlayerStatsConsole, leaderBoard);
    //map.SurfaceObject.Position = new Point(statsConsoleWidth, 0);

    //Starts monster Waves
    //map.CreateMonsterWave();


    // Sets up game over logic in advance.
    //var gameOver = new GameOver(rootScreen, leaderBoard);


    Game.Instance.Screen = rootScreen;
    Game.Instance.Screen.IsFocused = true;




    // This is needed because we replaced the initial screen object with our own.
    Game.Instance.DestroyDefaultStartingConsole();
  }

}