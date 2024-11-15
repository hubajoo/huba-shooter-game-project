using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// Class <c>Monster</c> models a hostile object in the game.
/// </summary>
public class Monster : GameObject
{
  public int Health { get; set; }
  public int DamageNumber { get; set; }

  public int FixActionDelay { get; set; }
  public int RandomActionDelayMax { get; set; } = 100;
  public int InactiveTime { get; set; } = 0;
  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="appearance"></param>
  /// <param name="position"></param>
  /// <param name="hostingSurface"></param>
  protected Monster(ColoredGlyph appearance, Point position, ScreenObjectManager screenObjectManager, int health, int damage)
      : base(appearance, position, screenObjectManager)
  {
    FixActionDelay = 0;
    Health = health;
    DamageNumber = damage;
  }
  public override bool TakeDamage(Map map, IGameObject source, int damage = 1)
  {
    Health -= damage;
    if (Health <= 0)
    {
      map.RemoveMapObject(this);
      map.UserControlledObject.Killed(this);
      map.DropLoot(this.Position);
    }

    return Health <= 0;
  }
  public override bool Touched(IGameObject source, Map map)
  {
    if (source is Projectile)
    {
      var p = source as Projectile;
      p.Direction = Direction.None;
      p.Touching(this);
      return this.TakeDamage(map, source, p.Damage);
    }
    return false;
  }

  public override void Update(Map map)
  {
    AIMove(map); //Triggers the movement behaviour
    AIAttack(map); //Triggers the attack behaviour
  }

  protected virtual void AIMove(Map map) //Default movement is random with a delay
  {
    if (InactiveTime >= FixActionDelay)
    {
      var direction = DirectionGeneration.GetRandomDirection();
      this.Move(this.Position + direction, map);
      InactiveTime = 0;
      InactiveTime -= RandomAction.RandomWait(RandomActionDelayMax);
    }
    else
    {
      InactiveTime++;
    }
  }
  protected virtual void AIAttack(Map map) //No default attack
  {

  }
}





