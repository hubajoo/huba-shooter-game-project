using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using DungeonCrawl.UI;
using SadConsole;
using SadRogue.Primitives;

public interface IScreenObjectManager
{
  public void AddScreenObject(IScreenObject screenObject);
  public void RemoveScreenObject(IScreenObject screenObject);
  public void RefreshCell(IMap map, Point position);
  public ColoredGlyph GetScreenObject(Point position);
  public void DrawScreenObject(IGameObject gameObject, Point position);
  public void ClearScreen();
  public void SetMainScreen(IScreenObject mainScreen);
  public void SetConsole(PlayerStatsConsole console);
  public void End();
  public void SetEndScreen(IScreenObject endScreen);
}