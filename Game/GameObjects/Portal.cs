using DungeonCrawl.Maps;
using SadRogue.Primitives;
using SadConsole;


namespace DungeonCrawl.GameObjects;

public class Portal : GameObject
{
  public Portal(Point position, ScreenObjectManager screenObjectManager, Map map)
      : base(new ColoredGlyph(Color.Gray, Color.Red, 0), position, screenObjectManager, map)
  {
  }
  public override bool Touched(IGameObject source)
  {
    source.Touching(this);
    return false;
  }
}