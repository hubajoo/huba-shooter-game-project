using SadConsole.Components;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Maps;

namespace DungeonCrawl.GameObjects;

public class Projectile : GameObject
{

  private int _flownDistance = 0;
  private int _maxDistance;

  public Projectile(Point position, Direction direction, ScreenObjectManager screenObjectManager, int damage, int maxDistance, Color color, int glyph = 4)
      : base(new ColoredGlyph(color, Color.Transparent, glyph), position, screenObjectManager)
  {
    _maxDistance = maxDistance;
    Damage = damage;
    Direction = direction;
  }

  public override bool Touched(IGameObject source, Map map)
  {
    Direction = Direction.None;
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
      if (!Move(Position + Direction, map)) Direction = Direction.None;
      _flownDistance++;
      if (Direction == Direction.Up || Direction == Direction.Down) _flownDistance++;
    }
    else
    {
      map.RemoveMapObject(this);
    }

  }


  public override void Touching(IGameObject source)
  {
    Appearance = new ColoredGlyph(OriginalAppearance.Foreground, Appearance.Background, 15);
  }
}