using DungeonCrawl.Mechanics.Randomisation;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;



namespace DungeonCrawl.GameObjects.Monsters;
/// <summary>
/// The class <c>Goblin</c> models a fast, agressive, melee monster in the game.
/// </summary>
public class Goblin : Monster
{
  /// <summary>
  /// Initializes a new instance of <c>Goblin</c> with a position, screen object manager, and map.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  /// <param name="directionChoice"></param>
  public Goblin(Point position, IScreenObjectManager screenObjectManager, IMap map, IDirectionChoiche directionChoice = null)
      : base(new ColoredGlyph(Color.Cyan, Color.Transparent, 1), position, screenObjectManager, health: 5, damage: 10, map, directionChoice)
  {
    FixActionDelay = 10; // The time the goblin waits between actions
    RandomActionDelayMax = 5; // The maximum time the goblin waits between actions
  }
  /// <summary>
  /// Overwrites the AIMove method to make the goblin chase the player.
  /// </summary>
  /// <param name="map"></param>
  protected override void AiMove(IMap map)
  {
    if (InactiveTime >= FixActionDelay) // If the goblin is not inactive
    {
      var direction = DirectionChoice.GetDirection(Position, map.UserControlledObject.Position); // Get the direction of the player
      if (!RandomAction.WeightedBool(4)) // Chanche to move randomly
      {
        direction = DirectionChoice.GetDirection(); // Get a random direction
      }
      Move(Position + direction, map);// Move in the direction
      InactiveTime = 0; // Reset the inactive time
      InactiveTime -= RandomAction.RandomWait(RandomActionDelayMax); // Randomly wait
    }
    else
    {
      InactiveTime++; // Wait
    }
  }
}