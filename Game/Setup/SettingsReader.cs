using System;
using System.Collections.Generic;
using System.IO;

namespace DungeonCrawl.GameSetup;

/// <summary>
/// Class <c>SettingsReader</c> reads settings from a file.
/// </summary>
public class SettingsReader : ISettingsReader
{
  /// <summary>
  /// Reads settings from a file.
  /// </summary>
  /// <param name="path"></param>
  /// <returns></returns>
  public IGameSettings ReadSettings(string path)
  {
    try
    {
      string[] lines = File.ReadAllLines(path); // Reads all lines from file
      return new GameSettings(lines); // Returns settings
    }
    catch (Exception e) // If any other exception
    {
      throw new Exception(e.Message);
    }
  }
}