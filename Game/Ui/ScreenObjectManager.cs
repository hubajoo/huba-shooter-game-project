using System;
using DungeonCrawl.GameObjects.ObjectInterfaces;
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
  private IScreenObject _screen;

  private IScreenObject _endScreen;

  private bool _endScreenSet = false;

  private PlayerStatsConsole _playerStatsConsole;


  private IScreenSurface _screenSurface;
  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="screenSurface"></param>
  public ScreenObjectManager(IScreenSurface screenSurface)
  {
    _screenSurface = screenSurface;
    _screen = new ScreenObject();
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
  /// <param name="appearance"></param>
  /// <param name="position"></param>
  public void DrawScreenObject(ColoredGlyph appearance, Point position)
  {
    appearance.CopyAppearanceTo(_screenSurface.Surface[position]);
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
  /// ScreenObject lifetime management.
  /// </summary>
  public void RemoveScreenObject(IScreenObject screenObject)
  {
    _screen.Children.Remove(screenObject);
  }

  /// <summary>
  /// Adds a screen object.
  /// </summary>
  /// <param name="screenObject"></param>
  public void AddScreenObject(IScreenObject screenObject)
  {
    _screen.Children.Add(screenObject);
  }

  /// <summary>
  /// Clears the screen.
  /// </summary>
  public void ClearScreen()
  {
    _screen.Children.Clear();
  }

  /// <summary>
  /// Sets the main screen.
  /// </summary>
  /// <param name="mainScreen"></param>
  public void SetMainScreen(IScreenObject mainScreen)
  {
    _screen = mainScreen;
  }

  /// <summary>
  /// Sets the console.
  /// </summary>
  /// <param name="console"></param>
  public void SetConsole(PlayerStatsConsole console)
  {
    _playerStatsConsole = console;
    _screen.Children.Add(console);
  }

  /// <summary>
  /// Switches to the end screen.
  /// </summary>
  public void End()
  {
    if (_endScreenSet)
    {
      AddScreenObject(_endScreen);
      Game.Instance.Screen = _endScreen;
    }
  }

  /// <summary>
  /// Sets the end screen.
  /// </summary>
  /// <param name="endScreen"></param>
  public void SetEndScreen(IScreenObject endScreen)
  {
    _endScreen = endScreen;
    endScreen.Update(new TimeSpan());
    _endScreenSet = true;
  }
}
