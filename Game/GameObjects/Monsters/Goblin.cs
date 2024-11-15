using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.Gameobjects;

public class Goblin : Monster //The goblin moves fast and hunts the player - melee
{
  public Goblin(Point position, ScreenObjectManager screenObjectManager)
      : base(new ColoredGlyph(Color.DarkBlue, Color.Transparent, 1), position, screenObjectManager, health: 5, damage: 5)
  {
    Damage = 10;
    FixActionDelay = 10;
    RandomActionDelayMax = 5;
  }
  protected override void AIMove(Map map) //Movement is overwritten to approach player
  {
    if (InactiveTime >= FixActionDelay)
    {
      var direction = DirectionGeneration.AggressiveDirection(this.Position,
          map.UserControlledObject.Position);
      if (!RandomAction.weightedBool(4))
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