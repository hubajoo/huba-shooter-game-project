using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

public class Orc : Monster //The orc is a slower, tankier melee opponent
{
  public Orc(Point position, ScreenObjectManager screenObjectManager, Map map)
      : base(new ColoredGlyph(Color.DarkGreen, Color.Transparent, 1), position, screenObjectManager, health: 10, damage: 10, map)
  {
    FixActionDelay = 10;
  }
}