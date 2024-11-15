using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.Gameobjects;


public class RangeBonus : Items
{
  public RangeBonus(Point position, ScreenObjectManager screenObjectManager)
      : base(new ColoredGlyph(Color.Cyan, Color.Transparent, 'R'), position, screenObjectManager)
  {
  }

  public override bool Touched(GameObject source, Map map)
  {
    if (source is Player)
    {
      source.Range += 3;
      map.RemoveMapObject(this);
      return true;
    }
    //source.Touching(this);
    return true;
  }
}