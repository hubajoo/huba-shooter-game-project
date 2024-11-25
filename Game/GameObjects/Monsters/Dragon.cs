using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.Mechanics.Randomisation;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Mechanics;


namespace DungeonCrawl.GameObjects.Monsters;

/// <summary>
/// The class <c>Dragon</c> models a ranged monster in the game.
/// </summary>
public class Dragon : Monster
{

  /// <summary>
  /// Initializes a new instance of <c>Dragon</c> with a position, screen object manager, and map.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  /// <param name="directionChoice"></param>
  public Dragon(Point position, IScreenObjectManager screenObjectManager, IMap map, IDirectionChoiche directionChoice = null)
      : base(new ColoredGlyph(Color.Red, Color.Transparent, 1), position, screenObjectManager, health: 20, damage: 20, map, directionChoice)
  {
    Health = 10; // The health of the dragon
  }

  /// <summary>
  /// Method <c>AIAttack</c> is called when the dragon attacks.
  /// </summary>
  /// <param name="map"></param>
  protected override void AiAttack(IMap map)
  {
    if (InactiveTime >= FixActionDelay)
    {
      // Get the direction of the player
      var direction = DirectionChoice.GetDirection(Position, map.UserControlledObject.Position);
      // Shoot a projectile in the direction of the player
      CreateProjectile(Position, direction, Color.Red, Damage, 15);
    }
    else
    {
      // Wait
      InactiveTime++;
    }
  }

  /// <summary>
  /// Method <c>AIMove</c> is called when the dragon moves.
  /// </summary>
  /// <param name="map"></param>
  protected override void AiMove(IMap map)
  {
    if (InactiveTime >= FixActionDelay) // If the dragon is not inactive
    {
      var direction = DirectionChoice.GetDirection(Position,
          map.UserControlledObject.Position); // Get the direction of the player
      if (!RandomAction.WeightedBool(3)) // Chanche to move randomly
      {
        direction = DirectionChoice.GetDirection(); // Get a random direction
      }
      Move(Position + direction, map); // Move in the direction
      InactiveTime = 0; // Reset the inactive time
      InactiveTime -= RandomAction.RandomWait(RandomActionDelayMax); // Add a random wait
    }
    else
    {
      InactiveTime++; // Wait
    }
  }


  /// <summary>
  /// Method <c>CreateProjectile</c> creates a projectile in the direction of the player.
  /// </summary>
  /// <param name="origin"></param>
  /// <param name="direction"></param>
  /// <param name="color"></param>
  /// <param name="damage"></param>
  /// <param name="maxDistance"></param>
  /// <param name="glyph"></param>
  /// <returns></returns>
  public bool CreateProjectile(Point origin, Direction direction, Color color, int damage = 1, int maxDistance = 1, int glyph = 4)
  {
    Point spawnPosition = origin + direction; // Calculate the spawn position

    // If the spawn position is not valid, return false
    if (!_map.SurfaceObject.Surface.IsValidCell(spawnPosition.X, spawnPosition.Y)) return false;

    // If there is an object at the spawn position, call the Touched method
    IGameObject foundObject;
    if (_map.TryGetMapObject(spawnPosition, out foundObject))
    {
      foundObject.Touched(this);
      return false;
    }

    // Create a new projectile
    Projectile projectile = new Projectile(spawnPosition, direction, ScreenObjectManager, damage, maxDistance, color, _map, glyph);
    _map.AddMapObject(projectile);

    return true;
  }
}