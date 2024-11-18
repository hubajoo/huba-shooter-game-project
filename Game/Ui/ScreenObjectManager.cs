using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;
/// <summary>
/// Class <c>ScreenObjectManager</c> manages screen objects.
/// </summary>
public class ScreenObjectManager
{

  private IScreenSurface _screenSurface;
  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="screenSurface"></param>
  public ScreenObjectManager(ScreenSurface screenSurface)
  {
    _screenSurface = screenSurface;
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
  public void DrawScreenObject(GameObject gameObject)
  {
    gameObject.GetAppearance().CopyAppearanceTo(_screenSurface.Surface[gameObject.GetPosition()]);
    //gameObject.Appearance.CopyAppearanceTo(_screenSurface.Surface[gameObject.Position]); 
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
  public void RefreshCell(Map map, Point position)
  {
    IGameObject gameObject;
    
    // Check if there is a game object at that position
    if (map.TryGetMapObject(position, out gameObject))
    {
      // Draw the game object
      DrawScreenObject(gameObject, position);
    }
    //DrawScreenObject(gameObject, position);
    
    // If there is no game object at that position
    else
    {
      // Draw a transparent cell
      DrawScreenObject(
        new ColoredGlyph(Color.Transparent, Color.Transparent, 0), position);
    }
    
  }
}