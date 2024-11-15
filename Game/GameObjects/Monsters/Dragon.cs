using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

public class Dragon : Monster, IDamaging, IMoving
{
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
      map.CreateProjectile(this.Position, direction, Color.Red, Damage, 15);
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

}