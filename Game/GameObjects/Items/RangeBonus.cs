using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;


public class RangeBonus : Items
{
  public RangeBonus(Point position, ScreenObjectManager screenObjectManager, Map map)
      : base(new ColoredGlyph(Color.Cyan, Color.Transparent, 'R'), position, screenObjectManager, map)
  {
  }

  public override bool Touched(Player source)
  {

    source.Range += 3;
    //map.RemoveMapObject(this);
    return true;
  }
}