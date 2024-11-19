using System;
using System.Collections.Generic;
using System.IO;
namespace Setup;

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
    catch (FileNotFoundException) // If file not found
    {
      return new DefaultSettings().Settings; // Returns default settings
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      return new DefaultSettings().Settings;
    }
  }
}