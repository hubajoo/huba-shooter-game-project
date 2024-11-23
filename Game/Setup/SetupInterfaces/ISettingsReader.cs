namespace DungeonCrawl.GameSetup;
public interface ISettingsReader
{
  public IGameSettings ReadSettings(string path);
}