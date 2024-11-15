using SadConsole;
using DungeonCrawl.Maps;
using SadRogue.Primitives;
using DungeonCrawl.Tiles;
public class ObjectDrawer
{

  private IScreenSurface screenSurface;


  protected void DrawGameObject(IScreenSurface screenSurface, GameObject gameObject)
  {
    gameObject.Appearance.CopyAppearanceTo(screenSurface.Surface[gameObject.Position]);
    screenSurface.IsDirty = true;
  }
}