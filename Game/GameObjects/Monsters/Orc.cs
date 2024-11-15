using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.Tiles;

public class Orc : Monster //The orc is a slower, tankier melee opponent
{
    public Orc(Point position, IScreenSurface hostingSurface)
        : base(new ColoredGlyph(Color.DarkGreen, Color.Transparent, 1), position, hostingSurface, health: 10, damage: 10)
    {
        FixActionDelay = 10;
    }
}