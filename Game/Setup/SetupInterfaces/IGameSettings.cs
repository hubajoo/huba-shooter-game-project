namespace GameSetup;

public interface IGameSettings
{
  public string UserName { get; set; }
  public int ViewPortWidth { get; set; }
  public int ViewPortHeight { get; set; }
  public int StatsConsoleWidth { get; set; }
  public int PlayerHealth { get; set; }
  public int PlayerDamage { get; set; }
  public int PlayerRange { get; set; }
  public string ServerUrl { get; set; }
}