using SadRogue.Primitives;
using SadConsole;
using DungeonCrawl.Maps;


namespace DungeonCrawl.GameObjects;

public class Wall : GameObject
{

  public Wall(Point position, ScreenObjectManager screenObjectManager, Map map)
      : base(new ColoredGlyph(Color.Gray, Color.White, 0), position, screenObjectManager, map)
  {
  }

}
