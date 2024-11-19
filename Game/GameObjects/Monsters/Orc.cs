using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;
/// <summary>
/// The class <c>Orc</c> models a slow, tankier, melee monster in the game.
/// </summary>
public class Orc : Monster
{
  /// <summary>
  /// Initializes a new instance of <c>Orc</c> with a position, screen object manager, and map.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  public Orc(Point position, IScreenObjectManager screenObjectManager, IMap map)
      : base(new ColoredGlyph(Color.DarkGreen, Color.Transparent, 1), position, screenObjectManager, health: 10, damage: 10, map)
  {
    FixActionDelay = 10;
  }
}