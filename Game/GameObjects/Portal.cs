using DungeonCrawl.Maps;
using SadRogue.Primitives;
using SadConsole;

namespace DungeonCrawl.Tiles;

public class Portal : GameObject
{
    public Portal(Point position, IScreenSurface hostingSurface)
        : base(new ColoredGlyph(Color.Gray, Color.Red, 0), position, hostingSurface)
    {
    }
    protected override bool Touched(GameObject source, Map map)
    {
        source.Touching(this);
        return false;
    }
}