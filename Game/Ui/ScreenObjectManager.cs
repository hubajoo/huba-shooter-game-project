using DungeonCrawl.GameObjects;
using SadConsole;
using SadRogue.Primitives;

public class ScreenObjectManager
{

  private IScreenSurface _screenSurface;

  public ScreenObjectManager(ScreenSurface screenSurface)
  {
    _screenSurface = screenSurface;
  }


  public void DrawScreenObject(GameObject gameObject, Point position)
  {
    gameObject.Appearance.CopyAppearanceTo(_screenSurface.Surface[position]);
    _screenSurface.IsDirty = true;
  }

  public ColoredGlyph GetScreenObject(Point position)
  {
    return _screenSurface.Surface[position];
  }

}