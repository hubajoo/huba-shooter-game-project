using SadConsole.Components;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Maps;

namespace DungeonCrawl.GameObjects;

public class Projectile : GameObject, IDamaging
{

  private int _flownDistance = 0;
  private int _maxDistance;

  public Projectile(Point position, Direction direction, ScreenObjectManager screenObjectManager, int damage, int maxDistance, Color color, Map map, int glyph = 4)
      : base(new ColoredGlyph(color, Color.Transparent, glyph), position, screenObjectManager, map)
  {
    _maxDistance = maxDistance;
    Damage = damage;
    Direction = direction;
    //map.AddMapObject(this);
  }

  public override bool Touched(IGameObject source)
  {
    Direction = Direction.None;
    return false;
  }

  public override void Update()
  {
    Fly(_map);
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
      RemoveSelf();
    }

  }

  public void Touching()
  {
    Appearance = new ColoredGlyph(OriginalAppearance.Foreground, Appearance.Background, 15);
  }

  public int GetDamage()
  {
    return Damage;
  }
}