using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Mechanics;


namespace DungeonCrawl.GameObjects.Monsters;

/// <summary>
/// The class <c>Orc</c> models a slow, tankier, melee monster in the game.
/// </summary>
public class Orc : Monster
{

  /// <summary>
  /// Initializes a new instance of <c>Orc</c> with a position, screen object manager, map, and direction choice.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  /// <param name="directionChoice"></param>
  public Orc(Point position, IScreenObjectManager screenObjectManager, IMap map, IDirectionChoiche directionChoice = null)
      : base(new ColoredGlyph(Color.DarkGreen, Color.Transparent, 1), position, screenObjectManager, health: 5, damage: 10, map, directionChoice)
  {
    FixActionDelay = 10; // The time the orc waits between actions
  }
}