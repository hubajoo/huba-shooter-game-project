using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

public abstract class Items : GameObject
{
  protected Items(ColoredGlyph appearance, Point position, ScreenObjectManager screenObjectManager, Map map) : base(appearance, position, screenObjectManager, map)
  {

  }

  public virtual bool Touched(Player source)
  {
    //source.AddNewItemToInventory(this);
    _map.RemoveMapObject(this);
    return true;
  }
  public bool Touched(Projectile source)
  {
    return true;
  }
  public override bool Touched(IGameObject source)
  {
    if (source is Player player)
    {
      var p = source as Player;
      // p.AddNewItemToInventory(this);
      _map.RemoveMapObject(this);
      return Touched(player);
    }
    return true;
  }
}
