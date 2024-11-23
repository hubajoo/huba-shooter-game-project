using SadConsole.Components;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Maps;

namespace DungeonCrawl.GameObjects;

/// <summary>
/// Projectile class is a GameObject that can move in a direction and damage other objects.
/// </summary>
public class Projectile : GameObject, IDamaging
{

  // _flownDistance is the distance the projectile has flown.
  private int _flownDistance = 0;

  // _maxDistance is the maximum distance the projectile can fly.
  private int _maxDistance;

  /// <summary>
  /// Projectile constructor.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="direction"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="damage"></param>
  /// <param name="maxDistance"></param>
  /// <param name="color"></param>
  /// <param name="map"></param>
  /// <param name="glyph"></param>
  /// <returns></returns>
  public Projectile(Point position, Direction direction, IScreenObjectManager screenObjectManager, int damage, int maxDistance, Color color, IMap map, int glyph = 4)
      : base(new ColoredGlyph(color, Color.Transparent, glyph), position, screenObjectManager, map)
  {
    _maxDistance = maxDistance;
    Damage = damage;
    Direction = direction;
  }

  /// <summary>
  /// Touched method changes the direction of the projectile when it touches another object.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public override bool Touched(IGameObject source)
  {
    Direction = Direction.None;
    return true;
  }
  //

  public override void Update()
  {
    Fly();
  }

  public void Fly()
  {
    if (_flownDistance <= _maxDistance)
    {
      if (!Move(Position + Direction, _map)) Direction = Direction.None;
      _flownDistance++;
      if (Direction == Direction.Up || Direction == Direction.Down) _flownDistance++;
    }
    else
    {
      RemoveSelf();
    }

  }

  /// <summary>
  /// Touching method changes the appearance of the projectile when it touches another object.
  /// </summary>
  public override void Touching(IGameObject source)
  {
    Appearance = new ColoredGlyph(OriginalAppearance.Foreground, Appearance.Background, 15);
    _screenObjectManager.RefreshCell(_map, Position);
    Direction = Direction.None;
  }

  /// <summary>
  /// GetDamage method returns the damage of the projectile.
  /// </summary>
  public int GetDamage()
  {
    return Damage;
  }
}