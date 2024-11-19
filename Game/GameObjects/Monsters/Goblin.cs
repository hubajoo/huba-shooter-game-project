using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// The class <c>Goblin</c> models a fast, agressive, melee monster in the game.
/// </summary>
public class Goblin : Monster, IDamaging, IMoving
{
  /// <summary>
  /// Initializes a new instance of <c>Goblin</c> with a position, screen object manager, and map.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  public Goblin(Point position, IScreenObjectManager screenObjectManager, IMap map, IDirectionChoiche directionChoice = null)
      : base(new ColoredGlyph(Color.Blue, Color.Transparent, 1), position, screenObjectManager, health: 5, damage: 5, map, directionChoice)
  {
    Damage = 10;
    FixActionDelay = 10;
    RandomActionDelayMax = 5;
  }
  /// <summary>
  /// Overwrites the AIMove method to make the goblin chase the player.
  /// </summary>
  /// <param name="map"></param>
  protected override void AIMove(IMap map)
  {
    if (InactiveTime >= FixActionDelay)
    {
      var direction = _directionChoice.GetDirection(Position, map.UserControlledObject.Position);
      if (!RandomAction.weightedBool(4))
      {
        direction = _directionChoice.GetDirection();
      }
      this.Move(this.Position + direction, map);
      InactiveTime = 0;
      InactiveTime -= RandomAction.RandomWait(RandomActionDelayMax);
    }
    else
    {
      InactiveTime++;
    }
  }
}