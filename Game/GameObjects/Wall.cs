using SadRogue.Primitives;
using SadConsole;


namespace DungeonCrawl.GameObjects;

public class Wall : GameObject
{

  public Wall(ColoredGlyph apperance, Point position, IScreenObjectManager screenObjectManager, IMap map)
      : base(apperance = new ColoredGlyph(Color.Gray, Color.White, 0), position, screenObjectManager, map)
  {
  }

}
