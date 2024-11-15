using SadRogue.Primitives;
using SadConsole;


namespace DungeonCrawl.GameObjects;

public class Wall : GameObject
{

  public Wall(Point position, ScreenObjectManager screenObjectManager)
      : base(new ColoredGlyph(Color.Gray, Color.White, 0), position, screenObjectManager)
  {
  }

}
