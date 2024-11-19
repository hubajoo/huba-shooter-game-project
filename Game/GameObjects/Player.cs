using System.Collections.Generic;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Mechanics;
using System.Linq;
using DungeonCrawl.Maps;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// Class <c>Player</c> models a user controlled object in the game.
/// </summary>
public class Player : GameObject, IVulnerable, IMoving, IShooting, IGameEnding
{
  public List<Items> Inventory { get; private set; }

  public int Health { get; set; }

  public int Kills { get; private set; } = 0;


  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="hostingSurface"></param>

  //public Direction Direction;
  public Player(Point position, IScreenObjectManager screenObjectManager, IMap map, int health = 100, int damage = 1, int range = 5)
      : base(new ColoredGlyph(Color.Green, Color.Transparent, 2), position, screenObjectManager, map)
  {
    Inventory = new List<Items>();
    Health = health;
    Damage = damage;
    Range = range;
  }

  public void ChangeDirection(Direction direction)
  {
    Direction = direction;
  }
  public void Shoot()
  {
    CreateProjectile(Position, Direction, Color.Orange, Damage, Range, 4);
  }

  /// <summary>
  /// Method <c>Move</c> moves the player to a new position.
  /// </summary>
  /// <param name="items"></param>
  /*
  public void AddNewItemToInventory(Items items)
  {
    Inventory.Add(items);

    if (items is Potion potion)
    {
      if (BaseHealth == PLAYER_MAX_HEALTH)
      {
        Inventory.Add(potion);
      }
      else if (BaseHealth + potion.Healing > PLAYER_MAX_HEALTH)

      {
        BaseHealth = PLAYER_MAX_HEALTH;
      }
      else
      {
        BaseHealth += potion.Healing;
      }
    }
  }
*/
  /// <summary>
  /// Method <c>Touched</c> prevents other Objects from moving to the occupied position.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public override bool Touched(IGameObject source)
  {
    if (source is IDamaging damaging && !(source is Player))
    {
      damaging.Touching(this);
      TakeDamage(damaging.GetDamage());
    }
    source.Touching(this);
    return false;
  }

  public void TakeDamage(int damage)
  {
    Health -= damage;
    if (Health <= 0)
    {
      EndGame();
    }
  }

  /// <summary>
  /// Method <c>Killed</c> counts the players's kills.
  /// </summary>
  /// <param name="victim"></param>
  public void Killed(IGameObject victim)
  {
    Kills++;
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

  public void EndGame()
  {
    Appearance.Foreground = Color.White;
    _screenObjectManager.RefreshCell(_map, Position);
    _screenObjectManager.End();

  }
}