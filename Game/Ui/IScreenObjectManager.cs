using DungeonCrawl.GameObjects;
using DungeonCrawl.UI;
using SadConsole;
using SadRogue.Primitives;
using System;

/// <summary>
/// Interface <c>IScreenObjectManager</c> models a screen object manager.
/// </summary>
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