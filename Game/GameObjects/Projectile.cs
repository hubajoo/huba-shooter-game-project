using SadConsole.Components;

namespace DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Tiles;
using DungeonCrawl.Maps;

public class Projectile : GameObject
{
    
    private int _flownDistance = 0;
    private int _maxDistance;

    public Projectile(Point position, Direction direction, IScreenSurface hostingSurface, int damage, int maxDistance, Color color, int glyph=4)
        : base(new ColoredGlyph(color , Color.Transparent, glyph), position, hostingSurface)
    {
        _maxDistance = maxDistance;
        Damage = damage;
        Direction = direction;
    }

    protected override bool Touched(GameObject source, Map map)
    {
        this.Direction = Direction.None;
        return true;
    }
    
    public override void Update(Map map)
    {
        Fly(map);
    }
    public void Fly(Map map)
    {
        if (_flownDistance <= _maxDistance)
        {
            if (!this.Move(this.Position + this.Direction, map)) this.Direction=Direction.None;
            _flownDistance++;
            if (this.Direction== Direction.Up||this.Direction== Direction.Down)   _flownDistance++;
        }
        else
        {
            map.RemoveMapObject(this);
        }
        
    }

    
    public override void Touching(GameObject source)
    {
        this.Appearance = new ColoredGlyph(OriginalAppearance.Foreground, Appearance.Background, 15);
    }
}