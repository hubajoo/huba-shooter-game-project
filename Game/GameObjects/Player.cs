using System.Collections.Generic;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.GameObjects.Items;
using DungeonCrawl.GameObjects.ObjectInterfaces;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// Class <c>Player</c> models a user controlled object in the .
/// </summary>
public class Player : GameObject, IVulnerable, IMoving, IShooting, IGameEnding
{
  public List<Item> Inventory { get; private set; }

  public int Health { get; set; }

  public int Kills { get; private set; }

  public string Name { get; private set; }


  private readonly System.Action<string, int> _scoreHandling;

  /// <summary>
  /// Constructor for Player
  /// </summary>
  /// <param name="name"></param>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  /// <param name="health"></param>
  /// <param name="damage"></param>
  /// <param name="range"></param>
  /// <param name="addToLeaderboard"></param>
  public Player(string name, Point position, IScreenObjectManager screenObjectManager, IMap map, int health = 100, int damage = 1, int range = 5, System.Action<string, int> addToLeaderboard = null)
      : base(new ColoredGlyph(Color.Green, Color.Transparent, 2), position, screenObjectManager, map)
  {
    Inventory = new List<Item>();
    Health = health;
    Damage = damage;
    Range = range;
    Name = name;
    Kills = 0;
    _scoreHandling = addToLeaderboard ?? ((name, score) => { });
  }
  /// <summary>
  /// Method <c>ChangeDirection</c> changes the direction of the player.
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
      EndGame();
    }
  }

  /// <summary>
  /// Method <c>Killed</c> counts the player's kills.
  /// </summary>
  /// <param name="victim"></param>
  public void Killed(IGameObject victim)
  {
    Kills++;
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
    Point spawnPosition = origin + direction;
    if (!_map.SurfaceObject.Surface.IsValidCell(spawnPosition.X, spawnPosition.Y)) return false;
    IGameObject foundObject;
    if (_map.TryGetMapObject(spawnPosition, out foundObject))
    {
      foundObject.Touched(this);
      return false;
    }

    Projectile projectile = new Projectile(spawnPosition, direction, ScreenObjectManager, damage, maxDistance, color, _map, glyph);
    _map.AddMapObject(projectile);

    return true;
  }

  /// <summary>
  /// Method <c>EndGame</c> ends the game.
  /// </summary>
  public void EndGame()
  {
    RemoveSelf();
    ScreenObjectManager.End();
    _scoreHandling(Name, Kills);
  }
}