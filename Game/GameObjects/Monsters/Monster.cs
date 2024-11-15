using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// Class <c>Monster</c> models a hostile object in the game.
/// </summary>
public class Monster : GameObject, IDamaging, IMoving//, IVulnerable
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
  protected Monster(ColoredGlyph appearance, Point position, ScreenObjectManager screenObjectManager, int health, int damage, Map map)
      : base(appearance, position, screenObjectManager, map)
  {
    FixActionDelay = 0;
    Health = health;
    DamageNumber = damage;
  }

  public bool TakeDamage(Map map, int damage = 1)
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
  public override bool Touched(IGameObject source)
  {
    return false;
  }

  public bool Touched(IDamaging source)
  {

    var p = source as Projectile;
    p.Direction = Direction.None;
    p.Touching(this);
    //TakeDamage(source as IGameObject, p.Damage);
    return false;
  }
  public void Touching()
  {
    throw new System.NotImplementedException();
  }

  public override void Update()
  {
    AIMove(_map); //Triggers the movement behaviour
    AIAttack(_map); //Triggers the attack behaviour
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
  public int GetDamage()
  {
    return Damage;
  }
}



