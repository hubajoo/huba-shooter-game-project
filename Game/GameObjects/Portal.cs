using DungeonCrawl.Maps;
using SadRogue.Primitives;
using SadConsole;


namespace DungeonCrawl.Gameobjects;

public class Portal : GameObject
{
  public Portal(Point position, ScreenObjectManager screenObjectManager)
      : base(new ColoredGlyph(Color.Gray, Color.Red, 0), position, screenObjectManager)
  {
  }
  protected override bool Touched(GameObject source, Map map)
  {
    source.Touching(this);
    return false;
  }
}