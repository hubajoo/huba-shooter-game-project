using SadRogue.Primitives;
using SadConsole;

namespace DungeonCrawl.Tiles;

public class Wall : GameObject
{
    public Wall(Point position, IScreenSurface hostingSurface)
        : base(new ColoredGlyph(Color.Gray, Color.White, 0), position, hostingSurface)
    {
    }
}