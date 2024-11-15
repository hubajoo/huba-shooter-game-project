using System;
using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.Gameobjects;

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
  public override bool TakeDamage(Map map, GameObject source, int damage = 1)
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
  public override bool Touched(GameObject source, Map map)
  {
    if (source is Projectile)
    {
      source.Direction = Direction.None;
      source.Touching(this);
      return this.TakeDamage(map, source, source.Damage);
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





