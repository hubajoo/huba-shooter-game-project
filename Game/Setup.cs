
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DungeonCrawl.Maps;
using DungeonCrawl.Tiles;
using DungeonCrawl.Ui;
using DungeonCrawl.UI;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives;
using Console = System.Console;

public class Setup
{
  /// <summary>
  /// Initializes the game.
  /// </summary>
  private Dictionary<string, string> Settings = new Dictionary<string, string>(){
      { "name", "Huba" },
      {"statsConsoleWidth", "25"}
  };
  public void Init()
  {
    // Converts Settings Dictionary to variables
    int statsConsoleWidth = Int32.Parse(Settings["statsConsoleWidth"]);
    int mapWidth = Game.Instance.ScreenCellsX - statsConsoleWidth;
    int mapHeight = Game.Instance.ScreenCellsY;

    // Creates the map
    Map map = new Map(mapWidth, mapHeight);
    map.SurfaceObject.Position = new Point(statsConsoleWidth, 0);

    // Creates player
    Player player = map.UserControlledObject;

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



    Game.Instance.Screen = rootScreen;
    Game.Instance.Screen.IsFocused = true;

    GameOver gameOver = new GameOver(rootScreen, leaderBoard);


    // This is needed because we replaced the initial screen object with our own.
    Game.Instance.DestroyDefaultStartingConsole();
  }

}