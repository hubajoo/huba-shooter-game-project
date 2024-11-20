using System.Collections.Generic;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Mechanics;
using System.Linq;
using DungeonCrawl.Maps;
using System.Reflection.Metadata.Ecma335;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// Class <c>Player</c> models a user controlled object in the game.
/// </summary>
public class Player : GameObject, IVulnerable, IMoving, IShooting, IGameEnding
{
  public List<Items> Inventory { get; private set; }

  public int Health { get; set; }

  public int Kills { get; private set; } = 0;

  public string Name { get; private set; }


  private System.Action<string, int> ScoreHandling;

  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="hostingSurface"></param>

  //public Direction Direction;
  public Player(string name, Point position, IScreenObjectManager screenObjectManager, IMap map, int health = 100, int damage = 1, int range = 5, System.Action<string, int> addToLeaderboard = null)
      : base(new ColoredGlyph(Color.Green, Color.Transparent, 2), position, screenObjectManager, map)
  {
    Inventory = new List<Items>();
    Health = health;
    Damage = damage;
    Range = range;
    Name = name;
    ScoreHandling = addToLeaderboard ?? ((name, score) => { });
  }
  /// <summary>
  /// Method <c>ChageDirection</c> changes the direction of the player.
  /// </summary>
  /// <param name="direction"></param>
  public void ChangeDirection(Direction direction)
  {
    Direction = direction;
  }

  /// <summary>
  /// Method <c>Shoot</c> creates a projectile in the direction of the player.
  /// </summary>
  public void Shoot()
  {
    CreateProjectile(Position, Direction, Color.Orange, Damage, Range, 4);
  }

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


  /// <summary>
  /// Method <c>TakeDamage</c> reduces the player's health when hit.
  /// </summary>
  /// <param name="damage"></param>
  public void TakeDamage(int damage)
  {
    Health -= damage;
    if (Health <= 0)
    {
      _map.RemoveMapObject(this);
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
    RemoveSelf();
    //Appearance.Foreground = Color.White;
    //_screenObjectManager.RefreshCell(_map, Position);
    _screenObjectManager.End();
    ScoreHandling(Name, Kills);
  }
}