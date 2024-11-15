using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

public class ScreenObjectManager
{

  private IScreenSurface _screenSurface;

  public ScreenObjectManager(ScreenSurface screenSurface)
  {
    _screenSurface = screenSurface;
  }


  public void DrawScreenObject(IGameObject gameObject, Point position)
  {
    gameObject.GetAppearance().CopyAppearanceTo(_screenSurface.Surface[position]);
    _screenSurface.IsDirty = true;
  }
  public void DrawScreenObject(ColoredGlyph apperance, Point position)
  {
    apperance.CopyAppearanceTo(_screenSurface.Surface[position]);
    _screenSurface.IsDirty = true;
  }
  public void DrawScreenObject(GameObject gameObject)
  {
    //gameObject.GetAppearance().CopyAppearanceTo(_screenSurface.Surface[gameObject.GetPosition()]);
    gameObject.Appearance.CopyAppearanceTo(_screenSurface.Surface[gameObject.Position]);
    _screenSurface.IsDirty = true;
  }

  public ColoredGlyph GetScreenObject(Point position)
  {
    return _screenSurface.Surface[position];
  }

  public void RefreshCell(Map map,Point position)
  {
    IGameObject gameObject;
    if (map.TryGetMapObject(position, out gameObject))
    {
      DrawScreenObject(gameObject, position);
    }
    DrawScreenObject(
      new ColoredGlyph(Color.Transparent, Color.Transparent, 0), position);
  }
}