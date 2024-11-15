using DungeonCrawl.Gameobjects;
using SadConsole;
using SadRogue.Primitives;

public class ScreenObjectManager
{

  private IScreenSurface _screenSurface;

  public ScreenObjectManager(ScreenSurface screenSurface)
  {
    _screenSurface = screenSurface;
  }


  public void DrawScreenObject(GameObject gameObject)
  {
    gameObject.Appearance.CopyAppearanceTo(_screenSurface.Surface[gameObject.Position]);
    _screenSurface.IsDirty = true;
  }

  public ColoredGlyph GetScreenObject(Point position)
  {
    return _screenSurface.Surface[position];
  }

}