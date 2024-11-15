
using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;
public class Potion : Items
{
  public int Amount { get; private set; }
  public Potion(Point position, ScreenObjectManager screenObjectManager, Map map)
      : base(new ColoredGlyph(Color.Red, Color.Transparent, 3), position, screenObjectManager, map)
  {
    Amount = 25;
  }
  public override bool Touched(Player source)
  {

    source.BaseHealth += 25;
    //map.RemoveMapObject(this);
    return true;
  }
}