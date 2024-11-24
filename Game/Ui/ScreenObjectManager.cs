using System;
using DungeonCrawl.GameObjects;
using DungeonCrawl.UI;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.UI;

/// <summary>
/// Class <c>ScreenObjectManager</c> manages screen objects.
/// </summary>


/// <summary>
/// Class <c>ScreenObjectManager</c> manages screen objects.
/// </summary>
public class ScreenObjectManager : ScreenObject, IScreenObjectManager
{
  private IScreenObject _Screen;

  private IScreenObject _EndScreen;

  private bool _EndScreenSet = false;

  private PlayerStatsConsole _PlayerStatsConsole;


  private IScreenSurface _screenSurface;
  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="screenSurface"></param>
  public ScreenObjectManager(IScreenSurface screenSurface)
  {
    _screenSurface = screenSurface;
    _Screen = new ScreenObject();
  }

  /// <summary>
  /// Draws a screen object at a position.
  /// </summary>
  /// <param name="gameObject"></param>
  /// <param name="position"></param>
  public void DrawScreenObject(IGameObject gameObject, Point position)
  {
    gameObject.GetAppearance().CopyAppearanceTo(_screenSurface.Surface[position]);
    _screenSurface.IsDirty = true;
  }

  /// <summary>
  /// Draws a ColoredGlyph at given position.
  /// </summary>
  /// <param name="apperance"></param>
  /// <param name="position"></param>
  public void DrawScreenObject(ColoredGlyph apperance, Point position)
  {
    apperance.CopyAppearanceTo(_screenSurface.Surface[position]);
    _screenSurface.IsDirty = true;
  }

  /// <summary>
  /// Draws a game object on the screen.
  /// </summary>
  /// <param name="gameObject"></param>
  public void DrawScreenObject(IGameObject gameObject)
  {
    gameObject.GetAppearance().CopyAppearanceTo(_screenSurface.Surface[gameObject.GetPosition()]);
    _screenSurface.IsDirty = true;
  }

  /// <summary>
  /// Gets a screen object at a position.
  /// </summary>
  /// <param name="position"></param>
  /// <returns></returns>
  public ColoredGlyph GetScreenObject(Point position)
  {
    return _screenSurface.Surface[position];
  }

  /// <summary>
  /// Refreshes a cell on the screen.
  /// </summary>
  /// <param name="map"></param>
  /// <param name="position"></param>
  public void RefreshCell(IMap map, Point position)
  {
    IGameObject gameObject;

    // Check if there is a game object at that position
    if (map.TryGetMapObject(position, out gameObject))
    {
      // Draw the game object
      DrawScreenObject(gameObject, position);
    }

    // If there is no game object at that position
    else
    {
      // Draw a transparent cell
      DrawScreenObject(
        new ColoredGlyph(Color.Transparent, Color.Transparent, 0), position);
    }

  }

  /// <summary>
  /// ScreenObject lifetime managementt.
  /// </summary>
  public void RemoveScreenObject(IScreenObject screenObject)
  {
    _Screen.Children.Remove(screenObject);
  }

  /// <summary>
  /// Adds a screen object.
  /// </summary>
  /// <param name="screenObject"></param>
  public void AddScreenObject(IScreenObject screenObject)
  {
    _Screen.Children.Add(screenObject);
  }

  /// <summary>
  /// Clears the screen.
  /// </summary>
  public void ClearScreen()
  {
    _Screen.Children.Clear();
  }

  /// <summary>
  /// Sets the main screen.
  /// </summary>
  /// <param name="mainScreen"></param>
  public void SetMainScreen(IScreenObject mainScreen)
  {
    _Screen = mainScreen;
  }

  /// <summary>
  /// Sets the console.
  /// </summary>
  /// <param name="console"></param>
  public void SetConsole(PlayerStatsConsole console)
  {
    _PlayerStatsConsole = console;
    _Screen.Children.Add(console);
  }

  /// <summary>
  /// Switches to the end screen.
  /// </summary>
  public void End()
  {
    if (_EndScreenSet)
    {
      AddScreenObject(_EndScreen);
      Game.Instance.Screen = _EndScreen;
    }
  }

  /// <summary>
  /// Sets the end screen.
  /// </summary>
  /// <param name="endScreen"></param>
  public void SetEndScreen(IScreenObject endScreen)
  {
    _EndScreen = endScreen;
    endScreen.Update(new TimeSpan());
    _EndScreenSet = true;
  }
}
