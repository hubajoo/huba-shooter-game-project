using DungeonCrawl.Maps;
using SadRogue.Primitives;
using SadConsole;


namespace DungeonCrawl.GameObjects;

public class Portal : GameObject
{
  public Portal(Point position, IScreenObjectManager screenObjectManager, IMap map)
      : base(new ColoredGlyph(Color.Gray, Color.Red, 0), position, screenObjectManager, map)
  {
  }
}