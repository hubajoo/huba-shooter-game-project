using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// The class <c>Dragon</c> models a ranged monster in the game.
/// </summary>
public class Dragon : Monster, IDamaging, IMoving
{
  /// <summary>
  /// Initializes a new instance of <c>Dragon</c> with a position, screen object manager, and map.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  public Dragon(Point position, ScreenObjectManager screenObjectManager, Map map)
      : base(new ColoredGlyph(Color.Red, Color.Transparent, 1), position, screenObjectManager, health: 20, damage: 20, map)
  {
    Health = 10;
  }
  protected override void AIAttack(Map map)
  {
    if (InactiveTime >= FixActionDelay)
    {
      var direction = DirectionGeneration.AggressiveDirection(Position,
          map.UserControlledObject.Position);
      CreateProjectile(Position, direction, Color.Red, Damage, 15);
    }
    else
    {
      InactiveTime++;
    }
  }
  protected override void AIMove(Map map) //Movement is overwritten to approach player
  {
    if (InactiveTime >= FixActionDelay)
    {
      var direction = DirectionGeneration.AggressiveDirection(this.Position,
          map.UserControlledObject.Position);
      if (!RandomAction.weightedBool(3))
      {
        direction = DirectionGeneration.GetRandomDirection();
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


  /// <summary>
  /// Creartes a custom projectile going in the required direction.
  /// </summary>
  /// <param name="attackerPosition">Origin position</param>
  /// <param name="direction"Direction of flight</param>
  /// <param name="color" Projectile color</param>
  public bool CreateProjectile(Point origin, Direction direction, Color color, int damage = 1, int maxDistance = 1, int glyph = 4)
  {
    Point spawnPosition = origin + direction;
    if (!_map.SurfaceObject.Surface.IsValidCell(spawnPosition.X, spawnPosition.Y)) return false;
    IGameObject foundObject;
    if (_map.TryGetMapObject(spawnPosition, out foundObject))
    {
      foundObject.Touched(this);
      return false;
    }

    Projectile projectile = new Projectile(spawnPosition, direction, _screenObjectManager, damage, maxDistance, color, _map, glyph);
    _map.AddMapObject(projectile);

    return true;
  }
}