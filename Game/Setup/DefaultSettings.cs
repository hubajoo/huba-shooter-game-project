using System.Collections.Generic;
using SadConsole;
namespace Setup;

public class DefaultSettings
{
  private string[] settingStringArray = new string[] {
"UserName:Bob",
"StatsConsoleWidth:25",
"ViewPortWidth:100",
"ViewPortHeight:40",
"PlayerHealth:100",
"PlayerDamage:1",
"PlayerRange:5",
  };
  public GameSettings Settings { get; set; }

  public DefaultSettings()
  {
    Settings = new GameSettings(settingStringArray);
  }
}
