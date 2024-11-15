using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;


public class RangeBonus : Items
{
  public RangeBonus(Point position, ScreenObjectManager screenObjectManager)
      : base(new ColoredGlyph(Color.Cyan, Color.Transparent, 'R'), position, screenObjectManager)
  {
  }

  public override bool Touched(IGameObject source, Map map)
  {
    if (source is Player)
    {
      var p = source as Player;
      p.Range += 3;
      map.RemoveMapObject(this);
      return true;
    }
    //source.Touching(this);
    return true;
  }
}