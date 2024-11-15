
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;
public class Potion : Items
{
  public int Amount { get; private set; }
  public Potion(Point position, ScreenObjectManager screenObjectManager)
      : base(new ColoredGlyph(Color.Red, Color.Transparent, 3), position, screenObjectManager)
  {
    Amount = 25;
  }
  public override bool Touched(IGameObject source, Maps.Map map)
  {
    if (source is Player)
    {
      map.UserControlledObject.BaseHealth += 25;
      map.RemoveMapObject(this);
      return true;
    }
    //source.Touching(this);
    return true;
  }
}