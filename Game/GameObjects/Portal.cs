using DungeonCrawl.Maps;
using SadRogue.Primitives;
using SadConsole;


namespace DungeonCrawl.GameObjects;

public class Portal : GameObject
{
  public Portal(Point position, ScreenObjectManager screenObjectManager)
      : base(new ColoredGlyph(Color.Gray, Color.Red, 0), position, screenObjectManager)
  {
  }
  public override bool Touched(IGameObject source, Map map)
  {
    source.Touching(this);
    return false;
  }
}