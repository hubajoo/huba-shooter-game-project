using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.Tiles;

public class Goblin : Monster //The goblin moves fast and hunts the player - melee
{
    public Goblin(Point position, IScreenSurface hostingSurface)
        : base(new ColoredGlyph(Color.DarkBlue, Color.Transparent, 1), position, hostingSurface, health: 5, damage: 5)
    {
        Damage = 10;
        FixActionDelay = 10;
        RandomActionDelayMax = 5;
    }
    protected override void AIMove(Map map) //Movement is overwritten to approach player
    {
            if (InactiveTime >= FixActionDelay)
            {
                var direction = Movements.AggressiveDirection(this.Position,
                    map.UserControlledObject.Position);
                if (!RandomAction.weightedBool(4))
                {
                    direction = Movements.GetRandomDirection();
                }
                this.Move(this.Position+direction, map);
                InactiveTime = 0;
                InactiveTime -= RandomAction.RandomWait(RandomActionDelayMax);
            }
            else
            {
               InactiveTime++;
            }
    }
}