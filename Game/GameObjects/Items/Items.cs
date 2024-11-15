using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.Gameobjects;

public abstract class Items : GameObject
{
  private IScreenSurface screenSurface;
  protected ScreenObjectManager screenObjectManager;
  protected Items(ColoredGlyph appearance, Point position, ScreenObjectManager screenObjectManager) : base(appearance, position, screenObjectManager)
  {

  }

  public override bool Touched(GameObject source, Map map)
  {
    if (source == map.UserControlledObject)
    {
      map.UserControlledObject.AddNewItemToInventory(this);
      map.RemoveMapObject(this);
      return true;
    }
    return false;
  }
}
