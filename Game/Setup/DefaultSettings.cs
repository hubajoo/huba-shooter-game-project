using System.Collections.Generic;
using System;
using SadConsole;
namespace DungeonCrawl.GameSetup;

/// <summary>
/// Class <c>DefaultSettings</c> provides default settings for the game.
/// </summary>
public class DefaultSettings
{
  /// Stores the settings for the game
  public IGameSettings Settings { get; set; }

  // Array of strings that represent the default settings
  private string[] settingStringArray = new string[] {
    "StatsConsoleWidth=25",
    "ViewPortWidth=100",
    "ViewPortHeight=40",
    "PlayerHealth=100",
    "PlayerDamage=1",
    "PlayerRange=5",
    "UserName=Bob",
    "ServerUrl=https://localhost:8090"
  };

  /// <summary>
  /// Constructor for DefaultSettings
  /// </summary>
  public DefaultSettings()
  {
    // Create a new GameSettings object from the settingStringArray
    Settings = new GameSettings(settingStringArray);
  }
}
