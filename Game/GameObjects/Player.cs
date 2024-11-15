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
public class Player : GameObject
{
  public static int PLAYER_MAX_HEALTH = 100;
  public static int PLAYER_BASE_SHIELD = 0;
  public static int PLAYER_BASE_DAMAGE = 1;
  public List<Items> Inventory { get; private set; }

  public int BaseDamage { get; private set; }

  public int BaseHealth { get; set; }

  public int BaseShield { get; private set; }

  public int Kills { get; private set; } = 0;
  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="hostingSurface"></param>

  //public Direction Direction;
  public Player(Point position, ScreenObjectManager screenObjectManager)
      : base(new ColoredGlyph(Color.Green, Color.Transparent, 2), position, screenObjectManager)
  {
    Inventory = new List<Items>();
    BaseDamage = PLAYER_BASE_DAMAGE;
    BaseHealth = PLAYER_MAX_HEALTH;
    BaseShield = PLAYER_BASE_SHIELD;
    Range = 5;

  }

  public void ChangeDirection(Direction direction)
  {
    Direction = direction;
  }
  public void Shoot(Map map)
  {
    map.CreateProjectile(this.Position, this.Direction, Color.Orange, PLAYER_BASE_DAMAGE, Range, 4);
  }
  public void AddNewItemToInventory(Items items)
  {
    Inventory.Add(items);

    if (items is Potion potion)
    {
      if (BaseHealth == PLAYER_MAX_HEALTH)
      {
        Inventory.Add(potion);
      }
      else if (BaseHealth + potion.Amount > PLAYER_MAX_HEALTH)

      {
        BaseHealth = PLAYER_MAX_HEALTH;
      }
      else
      {
        BaseHealth += potion.Amount;
      }
    }
  }

  public bool Touched(Monster source, Map map)
  {
    source.Touching(this);
    if (source is Monster || source is Projectile)
    {
      BaseHealth -= source.Damage;
      source.Touching(this);
      if (BaseHealth <= 0)
      {

        //Game.Instance.Dispose();
      }
      return TakeDamage(map, source, source.Damage);
    }
    return false;
  }
  public bool Touched(Projectile source, Map map)
  {
    source.Touching(this);
    if (source is Monster || source is Projectile)
    {
      BaseHealth -= source.Damage;
      source.Touching(this);
      if (BaseHealth <= 0)
      {

        //Game.Instance.Dispose();
      }
      return TakeDamage(map, source, source.Damage);
    }
    return false;
  }

  public override bool Touched(IGameObject source, Map map)
  {
    return false;
  }

  public void Killed(IGameObject victim)
  {
    Kills++;
  }
}