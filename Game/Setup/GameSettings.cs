using System;
using System.Collections.Generic;

namespace DungeonCrawl.GameSetup;


/// <summary>
/// Interface <c>IGameSettings</c> defines the properties of the game settings.
/// </summary>
public class GameSettings : IGameSettings
{
  private Dictionary<string, string> settingsDict = new Dictionary<string, string>();
  public string UserName { get; set; }
  public int ViewPortWidth { get; set; }
  public int ViewPortHeight { get; set; }
  public int StatsConsoleWidth { get; set; }
  public int PlayerHealth { get; set; }
  public int PlayerDamage { get; set; }
  public int PlayerRange { get; set; }
  public string ServerUrl { get; set; }

  /// <summary>
  /// Constructor for GameSettings
  /// </summary>
  /// <param name="lines"></param>
  public GameSettings(string[] lines)
  {
    try
    {

      foreach (string line in lines) // Loops through lines
      {
        string[] parts = line.Split('='); // Splits line by colon
        settingsDict.Add(parts[0], parts[1]); // Adds key-value pair to dictionary
      }

      UserName = settingsDict["UserName"];
      ViewPortWidth = Int32.Parse(settingsDict["ViewPortWidth"]);
      ViewPortHeight = Int32.Parse(settingsDict["ViewPortHeight"]);
      StatsConsoleWidth = Int32.Parse(settingsDict["StatsConsoleWidth"]);
      PlayerHealth = Int32.Parse(settingsDict["PlayerHealth"]);
      PlayerDamage = Int32.Parse(settingsDict["PlayerDamage"]);
      PlayerRange = Int32.Parse(settingsDict["PlayerRange"]);
      ServerUrl = settingsDict["ServerUrl"];
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
    }
  }
}